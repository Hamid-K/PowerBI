using System;
using System.Collections;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Security.AccessControl;
using System.Security.Permissions;
using System.Security.Principal;
using System.Text;
using Microsoft.Win32;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x02000851 RID: 2129
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class DrdaTraceListener : BaseDrdaTraceListener
	{
		// Token: 0x060043D9 RID: 17369 RVA: 0x000E4575 File Offset: 0x000E2775
		public DrdaTraceListener(string fileName)
		{
			this.fileName = fileName;
		}

		// Token: 0x060043DA RID: 17370 RVA: 0x000E4584 File Offset: 0x000E2784
		public override void Close()
		{
			if (this.writer != null)
			{
				this.writer.Close();
			}
			this.writer = null;
		}

		// Token: 0x060043DB RID: 17371 RVA: 0x000E45A0 File Offset: 0x000E27A0
		public override void Flush()
		{
			if (this.writer != null)
			{
				this.writer.Flush();
			}
		}

		// Token: 0x060043DC RID: 17372 RVA: 0x0007C375 File Offset: 0x0007A575
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.Close();
			}
		}

		// Token: 0x060043DD RID: 17373 RVA: 0x000E45B8 File Offset: 0x000E27B8
		internal bool EnsureWriter()
		{
			if (this.writer != null)
			{
				return true;
			}
			bool flag = true;
			lock (this)
			{
				if (this.writer != null)
				{
					return flag;
				}
				flag = false;
				if (this.fileName == null)
				{
					return flag;
				}
				bool flag3 = true;
				if (!string.IsNullOrWhiteSpace(base.TraceFileFolder))
				{
					flag3 = false;
					DirectoryInfo directoryInfo = new DirectoryInfo(base.TraceFileFolder);
					if (!directoryInfo.Exists)
					{
						try
						{
							directoryInfo.Create();
						}
						catch (Exception)
						{
							flag3 = true;
							Logger.LogEventWithParams(Constants.EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_CREATE_SPECIFIED_TRACE_DIRECTORY, directoryInfo.FullName);
						}
					}
					if (!flag3)
					{
						this.fullPath = (base.TraceFileFolder.EndsWith("\\") ? (base.TraceFileFolder + this.fileName) : (base.TraceFileFolder + "\\" + this.fileName));
					}
				}
				if (flag3)
				{
					RegistryKey registryKey = Registry.LocalMachine.OpenSubKey("Software\\Microsoft\\Host Integration Server");
					this.fullPath = this.fileName;
					if (registryKey != null)
					{
						string text = (string)registryKey.GetValue("InstallPath");
						if (text != null)
						{
							text = (text.EndsWith("\\") ? (text + "traces") : (text + "\\traces"));
							this.fullPath = text + "\\" + this.fileName;
						}
					}
					else
					{
						this.fullPath = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);
						this.fullPath = this.fullPath.Substring(0, this.fullPath.LastIndexOf("\\")) + "\\traces\\" + this.fileName;
					}
				}
				if (this.ensureWriterCalled)
				{
					int num = -1;
					string text2 = string.Empty;
					if (this.fileName.Contains("_"))
					{
						if (!int.TryParse(this.fileName.Substring(this.fileName.IndexOf("_") + 1, this.fileName.Length - 6 - this.fileName.IndexOf("_")), out num))
						{
							num = -1;
						}
						text2 = this.fullPath.Substring(0, this.fullPath.IndexOf("_"));
					}
					else
					{
						text2 = this.fullPath.Substring(0, this.fullPath.Length - 5);
					}
					if (num + 2 >= base.MaxTraceFiles)
					{
						this.fullPath = text2 + ".DSTF";
					}
					else
					{
						this.fullPath = text2 + "_" + (num + 1).ToString() + ".DSTF";
					}
				}
				else
				{
					this.ensureWriterCalled = true;
				}
				try
				{
					FileInfo fileInfo = new FileInfo(this.fullPath);
					if (fileInfo.Exists)
					{
						fileInfo.Delete();
					}
					DirectoryInfo directory = fileInfo.Directory;
					if (!directory.Exists)
					{
						directory.Create();
					}
					ReadOnlyCollectionBase accessRules = Directory.GetAccessControl(Path.GetDirectoryName(this.fullPath)).GetAccessRules(true, true, typeof(SecurityIdentifier));
					WindowsIdentity current = WindowsIdentity.GetCurrent();
					foreach (object obj in accessRules)
					{
						FileSystemAccessRule fileSystemAccessRule = (FileSystemAccessRule)obj;
						if ((current.Groups.Contains(fileSystemAccessRule.IdentityReference) || current.Owner == fileSystemAccessRule.IdentityReference) && (FileSystemRights.Write & fileSystemAccessRule.FileSystemRights) == FileSystemRights.Write && fileSystemAccessRule.AccessControlType == AccessControlType.Allow)
						{
							flag = true;
							break;
						}
					}
					if (flag)
					{
						Encoding encodingWithFallback = DrdaTraceListener.GetEncodingWithFallback(new UTF8Encoding(false));
						FileStream fileStream = new FileStream(this.fullPath, FileMode.Create, FileAccess.Write, FileShare.Read);
						this.writer = TextWriter.Synchronized(new StreamWriter(fileStream, encodingWithFallback, 4096, true));
					}
				}
				catch (IOException ex)
				{
					Logger.LogEventWithParams(Constants.EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_CREATE_TRACE_FILE, this.fullPath, ex.Message);
				}
				catch (UnauthorizedAccessException ex2)
				{
					Logger.LogEventWithParams(Constants.EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_CREATE_TRACE_FILE, this.fullPath, ex2.Message);
				}
				catch (Exception ex3)
				{
					Logger.LogEventWithParams(Constants.EVENTLOG_MESSAGE_ID_SERVICE_FAILED_TO_CREATE_TRACE_FILE, this.fullPath, ex3.Message);
				}
				if (!flag)
				{
					this.fileName = null;
				}
				else
				{
					this.fileName = Path.GetFileName(this.fullPath);
				}
			}
			return flag;
		}

		// Token: 0x060043DE RID: 17374 RVA: 0x000B971C File Offset: 0x000B791C
		private static Encoding GetEncodingWithFallback(Encoding encoding)
		{
			Encoding encoding2 = (Encoding)encoding.Clone();
			encoding2.EncoderFallback = EncoderFallback.ReplacementFallback;
			encoding2.DecoderFallback = DecoderFallback.ReplacementFallback;
			return encoding2;
		}

		// Token: 0x060043DF RID: 17375 RVA: 0x000E4A7C File Offset: 0x000E2C7C
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			this._traceCount += 1L;
			if (eventType != TraceEventType.Error)
			{
				if (eventType != TraceEventType.Warning)
				{
					if (eventType == TraceEventType.Information)
					{
						this.writer.WriteLine(string.Format("Information:{0}:{1}", id, message));
					}
				}
				else
				{
					this.writer.WriteLine(string.Format("Warning:{0}:{1}", id, message));
				}
			}
			else
			{
				this.writer.WriteLine(string.Format("Error:{0}:{1}", id, message));
			}
			if (this._autoFlush)
			{
				this.writer.Flush();
			}
			if (this._traceCount > this._maxtraceentries)
			{
				this.writer.Close();
				this.writer = null;
				this._traceCount = 0L;
			}
		}

		// Token: 0x060043E0 RID: 17376 RVA: 0x000E4B48 File Offset: 0x000E2D48
		public override void TraceEvent(TraceEventType eventType, int id, string message)
		{
			if (!this.EnsureWriter())
			{
				return;
			}
			this._traceCount += 1L;
			if (eventType != TraceEventType.Error)
			{
				if (eventType != TraceEventType.Warning)
				{
					if (eventType == TraceEventType.Information)
					{
						this.writer.WriteLine(string.Format("Information:{0}:{1}", id, message));
					}
				}
				else
				{
					this.writer.WriteLine(string.Format("Warning:{0}:{1}", id, message));
				}
			}
			else
			{
				this.writer.WriteLine(string.Format("Error:{0}:{1}", id, message));
			}
			if (this._autoFlush)
			{
				this.writer.Flush();
			}
			if (this._traceCount > this._maxtraceentries && this.writer != null)
			{
				this.writer.Close();
				this.writer = null;
				this._traceCount = 0L;
			}
		}

		// Token: 0x04002FB6 RID: 12214
		private string fileName;

		// Token: 0x04002FB7 RID: 12215
		internal TextWriter writer;

		// Token: 0x04002FB8 RID: 12216
		private string fullPath;

		// Token: 0x04002FB9 RID: 12217
		private bool ensureWriterCalled;
	}
}
