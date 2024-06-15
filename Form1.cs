using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using System.ComponentModel;

namespace Lab_31_Danylko
{
    public partial class Form1 : Form
    {
        private ProcessInfo selectedProcessInfo;

        public Form1()
        {
            InitializeComponent();
            InitializeDataGridView();
            SetupContextMenu();
            this.Shown += Form1_Shown;
        }

        private void InitializeDataGridView()
        {
            Console.WriteLine("Initializing DataGridView...");
            dataGridViewProcesses.Columns.Add("ProcessName", "Process Name");
            dataGridViewProcesses.Columns.Add("ProcessId", "Process ID");
            Console.WriteLine("DataGridView initialized.");
        }

        private void Form1_Shown(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void LoadProcesses()
        {
            Task.Run(async () =>
            {
                try
                {
                    var processList = new List<ProcessInfo>();

                    foreach (var process in Process.GetProcesses())
                    {
                        try
                        {
                            var processInfo = new ProcessInfo(process);
                            processList.Add(processInfo);
                            Console.WriteLine($"Successfully added process {process.ProcessName} (ID: {process.Id}) to the list.");
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Failed to get info for process {process.ProcessName} (ID: {process.Id}): {ex.Message}");
                        }
                    }

                    await Task.Delay(1000);

                    if (IsHandleCreated)
                    {
                        Invoke(new Action(() =>
                        {
                            dataGridViewProcesses.Rows.Clear();
                            foreach (var processInfo in processList)
                            {
                                AddProcessToGrid(processInfo);
                            }
                        }));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"An error occurred while loading processes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            });
        }

        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);
            LoadProcesses();
        }

        private void AddProcessToGrid(ProcessInfo processInfo)
        {
            if (InvokeRequired)
            {
                if (IsHandleCreated)
                {
                    Invoke(new Action(() => AddProcessToGrid(processInfo)));
                }
                return;
            }

            try
            {
                dataGridViewProcesses.Rows.Add(processInfo.Name, processInfo.Id);
                Console.WriteLine($"Added process {processInfo.Name} (ID: {processInfo.Id}) to grid.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to add process {processInfo.Name} (ID: {processInfo.Id}) to grid: {ex.Message}");
            }
        }

        private void dataGridViewProcesses_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                var processId = (int)dataGridViewProcesses.Rows[e.RowIndex].Cells[1].Value;
                LoadProcessDetails(processId);
            }
        }

        private async void LoadProcessDetails(int processId)
        {
            try
            {
                var process = Process.GetProcessById(processId);
                selectedProcessInfo = new ProcessInfo(process);
                await selectedProcessInfo.LoadDetailsAsync();
                DisplayProcessDetails(selectedProcessInfo);
                EnableProcessButtons();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to load process details: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DisplayProcessDetails(ProcessInfo processInfo)
        {
            richTextBoxProcessInfo.Text = $"Process Name: {processInfo.Name}\r\n" +
                                      $"ID: {processInfo.Id}\r\n" +
                                      $"CPU Usage: {processInfo.CpuUsage} %\r\n" +
                                      $"Memory Usage: {processInfo.MemoryUsage} MB\r\n" +
                                      $"Network Usage: {processInfo.NetworkUsage} KB/s\r\n" +
                                      $"Priority: {processInfo.Priority}";
        }

        private void EnableProcessButtons()
        {
            buttonKillProcess.Enabled = true;
            buttonLoadProcessInfo.Enabled = true;
            buttonShowThreadsModules.Enabled = true;
        }

        private void buttonLoadProcessInfo_Click(object sender, EventArgs e)
        {
            if (selectedProcessInfo != null)
            {
                DisplayProcessDetails(selectedProcessInfo);
            }
        }

        private void buttonKillProcess_Click(object sender, EventArgs e)
        {
            if (selectedProcessInfo != null)
            {
                try
                {
                    var process = Process.GetProcessById(selectedProcessInfo.Id);
                    process.Kill();
                    LoadProcesses();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to kill process: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void buttonRefresh_Click(object sender, EventArgs e)
        {
            LoadProcesses();
        }

        private void buttonExport_Click(object sender, EventArgs e)
        {
            try
            {
                var sb = new StringBuilder();
                foreach (DataGridViewRow row in dataGridViewProcesses.Rows)
                {
                    sb.AppendLine($"{row.Cells[0].Value}, {row.Cells[1].Value}");
                }

                using (var saveFileDialog = new SaveFileDialog())
                {
                    saveFileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    {
                        File.WriteAllText(saveFileDialog.FileName, sb.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Failed to export processes: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void SetupContextMenu()
        {
            var infoMenuItem = new ToolStripMenuItem("Information");
            infoMenuItem.Click += InfoMenuItem_Click;

            var killMenuItem = new ToolStripMenuItem("Kill Process");
            killMenuItem.Click += KillMenuItem_Click;

            var freezeMenuItem = new ToolStripMenuItem("Freeze Process");
            freezeMenuItem.Click += FreezeMenuItem_Click;

            var resumeMenuItem = new ToolStripMenuItem("Resume Process");
            resumeMenuItem.Click += ResumeMenuItem_Click;

            var threadsModulesMenuItem = new ToolStripMenuItem("Threads and Modules");
            threadsModulesMenuItem.Click += ThreadsModulesMenuItem_Click;

            contextMenuStrip.Items.Add(infoMenuItem);
            contextMenuStrip.Items.Add(killMenuItem);
            contextMenuStrip.Items.Add(freezeMenuItem);
            contextMenuStrip.Items.Add(resumeMenuItem);
            contextMenuStrip.Items.Add(threadsModulesMenuItem);

            dataGridViewProcesses.ContextMenuStrip = contextMenuStrip;
        }

        private void InfoMenuItem_Click(object sender, EventArgs e)
        {
            ShowProcessInfo();
        }

        private void KillMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewProcesses.SelectedRows.Count > 0)
            {
                try
                {
                    var processId = (int)dataGridViewProcesses.SelectedRows[0].Cells[1].Value;
                    var process = Process.GetProcessById(processId);
                    process.Kill();
                    LoadProcesses();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to kill process: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void FreezeMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewProcesses.SelectedRows.Count > 0)
            {
                try
                {
                    var processId = (int)dataGridViewProcesses.SelectedRows[0].Cells[1].Value;
                    var process = Process.GetProcessById(processId);
                    SuspendProcess(process);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to freeze process: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ResumeMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridViewProcesses.SelectedRows.Count > 0)
            {
                try
                {
                    var processId = (int)dataGridViewProcesses.SelectedRows[0].Cells[1].Value;
                    var process = Process.GetProcessById(processId);
                    ResumeProcess(process);
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to resume process: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ThreadsModulesMenuItem_Click(object sender, EventArgs e)
        {
            ShowThreadsModulesInfo();
        }

        private void buttonShowThreadsModules_Click(object sender, EventArgs e)
        {
            ShowThreadsModulesInfo();
        }

        private void ShowThreadsModulesInfo()
        {
            if (dataGridViewProcesses.SelectedRows.Count > 0)
            {
                try
                {
                    var processId = (int)dataGridViewProcesses.SelectedRows[0].Cells[1].Value;
                    var process = Process.GetProcessById(processId);
                    var info = new StringBuilder();

                    info.AppendLine("Threads:");
                    foreach (ProcessThread thread in process.Threads)
                    {
                        info.AppendLine($"Thread ID: {thread.Id}, State: {thread.ThreadState}, Start Time: {thread.StartTime}, Total Processor Time: {thread.TotalProcessorTime}");
                    }

                    info.AppendLine("\nModules:");
                    try
                    {
                        foreach (ProcessModule module in process.Modules)
                        {
                            info.AppendLine($"Module: {module.ModuleName}, Path: {module.FileName}");
                        }
                    }
                    catch (Win32Exception ex)
                    {
                        if (ex.NativeErrorCode == 299) // ERROR_PARTIAL_COPY
                        {
                            info.AppendLine("Cannot access modules of a 64-bit process from a 32-bit process.");
                        }
                        else
                        {
                            throw;
                        }
                    }

                    var threadsModulesForm = new ThreadsModulesForm();
                    threadsModulesForm.SetInfo(info.ToString());
                    threadsModulesForm.Show();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to retrieve threads and modules: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void ShowProcessInfo()
        {
            if (dataGridViewProcesses.SelectedRows.Count > 0)
            {
                try
                {
                    var processId = (int)dataGridViewProcesses.SelectedRows[0].Cells[1].Value;
                    var process = Process.GetProcessById(processId);
                    var info = new StringBuilder();

                    info.AppendLine($"Process Name: {process.ProcessName}");
                    info.AppendLine($"ID: {process.Id}");
                    info.AppendLine($"Start Time: {process.StartTime}");
                    info.AppendLine($"Total Processor Time: {process.TotalProcessorTime}");
                    info.AppendLine($"Priority: {process.PriorityClass}");
                    info.AppendLine($"Memory Usage: {process.WorkingSet64 / 1024.0 / 1024.0} MB");

                    try
                    {
                        var cpuCounter = new PerformanceCounter("Process", "% Processor Time", process.ProcessName, true);
                        cpuCounter.NextValue();
                        System.Threading.Thread.Sleep(1000);
                        info.AppendLine($"CPU Usage: {cpuCounter.NextValue() / Environment.ProcessorCount} %");
                    }
                    catch
                    {
                        info.AppendLine("CPU Usage: Access denied");
                    }

                    try
                    {
                        var networkCounter = new PerformanceCounter("Process", "IO Data Bytes/sec", process.ProcessName, true);
                        networkCounter.NextValue();
                        System.Threading.Thread.Sleep(1000);
                        info.AppendLine($"Network Usage: {networkCounter.NextValue() / 1024} KB/s");
                    }
                    catch
                    {
                        info.AppendLine("Network Usage: Access denied");
                    }

                    info.AppendLine("\nThreads:");
                    foreach (ProcessThread thread in process.Threads)
                    {
                        info.AppendLine($"Thread ID: {thread.Id}, State: {thread.ThreadState}, Start Time: {thread.StartTime}, Total Processor Time: {thread.TotalProcessorTime}");
                    }

                    info.AppendLine("\nModules:");
                    try
                    {
                        foreach (ProcessModule module in process.Modules)
                        {
                            info.AppendLine($"Module: {module.ModuleName}, Path: {module.FileName}");
                        }
                    }
                    catch (Win32Exception ex)
                    {
                        if (ex.NativeErrorCode == 299) // ERROR_PARTIAL_COPY
                        {
                            info.AppendLine("Cannot access modules of a 64-bit process from a 32-bit process.");
                        }
                        else
                        {
                            throw;
                        }
                    }

                    richTextBoxProcessInfo.Text = info.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Failed to show process information: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        [DllImport("kernel32.dll")]
        private static extern IntPtr OpenThread(ThreadAccess dwDesiredAccess, bool bInheritHandle, uint dwThreadId);

        [DllImport("kernel32.dll")]
        private static extern uint SuspendThread(IntPtr hThread);

        [DllImport("kernel32.dll")]
        private static extern uint ResumeThread(IntPtr hThread);

        [Flags]
        public enum ThreadAccess : int
        {
            TERMINATE = 0x0001,
            SUSPEND_RESUME = 0x0002,
            GET_CONTEXT = 0x0008,
            SET_CONTEXT = 0x0010,
            SET_INFORMATION = 0x0020,
            QUERY_INFORMATION = 0x0040,
            SET_THREAD_TOKEN = 0x0080,
            IMPERSONATE = 0x0100,
            DIRECT_IMPERSONATION = 0x0200
        }

        private void SuspendProcess(Process process)
        {
            try
            {
                foreach (ProcessThread thread in process.Threads)
                {
                    IntPtr openThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);

                    if (openThread == IntPtr.Zero)
                    {
                        continue;
                    }

                    SuspendThread(openThread);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while suspending the process: {ex.Message}\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void ResumeProcess(Process process)
        {
            try
            {
                foreach (ProcessThread thread in process.Threads)
                {
                    IntPtr openThread = OpenThread(ThreadAccess.SUSPEND_RESUME, false, (uint)thread.Id);

                    if (openThread == IntPtr.Zero)
                    {
                        continue;
                    }

                    ResumeThread(openThread);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"An error occurred while resuming the process: {ex.Message}\n{ex.StackTrace}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}