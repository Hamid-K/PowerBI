using System;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Security;
using System.Security.Permissions;
using System.Text;
using Microsoft.Win32;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x0200066B RID: 1643
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	internal class HisTextFileTraceListener : TraceListener
	{
		// Token: 0x060036EE RID: 14062 RVA: 0x000B93BC File Offset: 0x000B75BC
		public HisTextFileTraceListener()
		{
		}

		// Token: 0x060036EF RID: 14063 RVA: 0x000B93E5 File Offset: 0x000B75E5
		public HisTextFileTraceListener(string name)
			: base(name)
		{
		}

		// Token: 0x17000C0F RID: 3087
		// (get) Token: 0x060036F0 RID: 14064 RVA: 0x000B940F File Offset: 0x000B760F
		// (set) Token: 0x060036F1 RID: 14065 RVA: 0x000B9417 File Offset: 0x000B7617
		public string FileNamePreamble
		{
			get
			{
				return this.fileNamePreamble;
			}
			set
			{
				this.fileNamePreamble = value;
			}
		}

		// Token: 0x17000C10 RID: 3088
		// (get) Token: 0x060036F2 RID: 14066 RVA: 0x000B9420 File Offset: 0x000B7620
		// (set) Token: 0x060036F3 RID: 14067 RVA: 0x000B9540 File Offset: 0x000B7740
		public string TraceFileFolder
		{
			get
			{
				if (!string.IsNullOrWhiteSpace(this.traceFileFolder))
				{
					this.traceFileFolder = Environment.ExpandEnvironmentVariables(this.traceFileFolder);
					DirectoryInfo directoryInfo = new DirectoryInfo(this.traceFileFolder);
					if (!directoryInfo.Exists)
					{
						try
						{
							directoryInfo.Create();
						}
						catch (Exception)
						{
							this.traceFileFolder = null;
						}
					}
					if (this.traceFileFolder != null)
					{
						this.traceFileFolder = (this.traceFileFolder.EndsWith("\\") ? this.traceFileFolder : (this.traceFileFolder + "\\"));
					}
				}
				if (string.IsNullOrWhiteSpace(this.traceFileFolder))
				{
					RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
					RegistryKey registryKey2 = registryKey.OpenSubKey("\\Software\\Microsoft\\Host Integration Server");
					if (registryKey2 != null)
					{
						string text = (string)registryKey2.GetValue("InstallPath");
						if (text != null)
						{
							this.traceFileFolder = (text.EndsWith("\\") ? (text + "traces\\") : (text + "\\traces\\"));
						}
						registryKey2.Close();
						registryKey2.Dispose();
					}
					registryKey.Close();
					registryKey.Dispose();
				}
				return this.traceFileFolder;
			}
			set
			{
				this.traceFileFolder = value;
			}
		}

		// Token: 0x17000C11 RID: 3089
		// (get) Token: 0x060036F4 RID: 14068 RVA: 0x000B9549 File Offset: 0x000B7749
		// (set) Token: 0x060036F5 RID: 14069 RVA: 0x000B9551 File Offset: 0x000B7751
		public bool AutoFlush { get; set; }

		// Token: 0x17000C12 RID: 3090
		// (get) Token: 0x060036F6 RID: 14070 RVA: 0x000B955A File Offset: 0x000B775A
		// (set) Token: 0x060036F7 RID: 14071 RVA: 0x000B9562 File Offset: 0x000B7762
		public bool AllowNonHisTracingToCreateFile { get; set; }

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x060036F8 RID: 14072 RVA: 0x000B956B File Offset: 0x000B776B
		// (set) Token: 0x060036F9 RID: 14073 RVA: 0x000B9573 File Offset: 0x000B7773
		public int MaxTraceEntries
		{
			get
			{
				return this.maxTraceEntries;
			}
			set
			{
				if (value < 1000)
				{
					throw new ArgumentOutOfRangeException("maxTraceEntries");
				}
				this.maxTraceEntries = value;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x060036FA RID: 14074 RVA: 0x00002B16 File Offset: 0x00000D16
		public override bool IsThreadSafe
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060036FB RID: 14075 RVA: 0x000B9590 File Offset: 0x000B7790
		public override void Write(string message)
		{
			object obj = this.flushLock;
			lock (obj)
			{
				if (this.EnsureWriter())
				{
					bool needIndent = base.NeedIndent;
					if (needIndent)
					{
						this.WriteIndent();
					}
					this.writer.Write(message);
					base.NeedIndent = needIndent;
				}
			}
		}

		// Token: 0x060036FC RID: 14076 RVA: 0x000B95F8 File Offset: 0x000B77F8
		public override void WriteLine(string message)
		{
			object obj = this.flushLock;
			lock (obj)
			{
				if (this.EnsureWriter())
				{
					bool needIndent = base.NeedIndent;
					if (needIndent)
					{
						this.WriteIndent();
					}
					this.writer.WriteLine(message);
					base.NeedIndent = needIndent;
					this.numberOfLinesWritten += 1U;
					if ((ulong)this.numberOfLinesWritten > (ulong)((long)this.MaxTraceEntries))
					{
						this.Rollover();
					}
				}
			}
		}

		// Token: 0x060036FD RID: 14077 RVA: 0x000B9684 File Offset: 0x000B7884
		public override void Close()
		{
			object obj = this.flushLock;
			lock (obj)
			{
				this.InternalClose();
			}
			base.Close();
		}

		// Token: 0x060036FE RID: 14078 RVA: 0x000B96CC File Offset: 0x000B78CC
		public override void Flush()
		{
			object obj = this.flushLock;
			lock (obj)
			{
				if (this.writer != null)
				{
					this.writer.Flush();
				}
			}
		}

		// Token: 0x060036FF RID: 14079 RVA: 0x000B971C File Offset: 0x000B791C
		private static Encoding GetEncodingWithFallback(Encoding encoding)
		{
			Encoding encoding2 = (Encoding)encoding.Clone();
			encoding2.EncoderFallback = EncoderFallback.ReplacementFallback;
			encoding2.DecoderFallback = DecoderFallback.ReplacementFallback;
			return encoding2;
		}

		// Token: 0x06003700 RID: 14080 RVA: 0x000B973F File Offset: 0x000B793F
		private bool EnsureWriter()
		{
			return this.writer != null || this.InternalOpen(false);
		}

		// Token: 0x06003701 RID: 14081 RVA: 0x000B9752 File Offset: 0x000B7952
		private void Rollover()
		{
			this.InternalClose();
			this.InternalOpen(true);
		}

		// Token: 0x06003702 RID: 14082 RVA: 0x000B9764 File Offset: 0x000B7964
		private bool InternalOpen(bool partOfARollover)
		{
			if (this.TraceFileFolder == null)
			{
				return false;
			}
			if (this.inTearDown)
			{
				return false;
			}
			bool flag = false;
			if (partOfARollover)
			{
				flag = true;
			}
			if (this.hisTracingHasCalledOpen)
			{
				flag = true;
			}
			if (this.AllowNonHisTracingToCreateFile)
			{
				flag = true;
			}
			if (!flag)
			{
				return false;
			}
			string text = Process.GetCurrentProcess().Id.ToString(CultureInfo.InvariantCulture);
			string text2 = DateTime.Now.ToString("MMM_dd_yyyy_HH_mm_ss_fff", CultureInfo.InvariantCulture);
			string text3 = string.Concat(new string[] { this.FileNamePreamble, "_", text, "_", text2, ".HITF" });
			text3 = text3.ToUpperInvariant();
			string text4 = this.TraceFileFolder + text3;
			try
			{
				FileStream fileStream = new FileStream(text4, FileMode.CreateNew, FileAccess.Write, FileShare.Read);
				Encoding encodingWithFallback = HisTextFileTraceListener.GetEncodingWithFallback(new UTF8Encoding(false));
				this.writer = new StreamWriter(fileStream, encodingWithFallback, 4096);
				this.numberOfLinesWritten = 0U;
				if (!this.registeredForAppDomainUnload)
				{
					try
					{
						AppDomain.CurrentDomain.DomainUnload += this.HandleDomainUnload;
						this.registeredForAppDomainUnload = true;
					}
					catch (SecurityException)
					{
					}
				}
				if (!this.registeredForProcessExit)
				{
					try
					{
						AppDomain.CurrentDomain.ProcessExit += this.HandleProcessExit;
						this.registeredForProcessExit = true;
					}
					catch (SecurityException)
					{
					}
				}
				return true;
			}
			catch (IOException)
			{
			}
			catch (UnauthorizedAccessException)
			{
			}
			catch (Exception)
			{
			}
			return false;
		}

		// Token: 0x06003703 RID: 14083 RVA: 0x000B98FC File Offset: 0x000B7AFC
		private void InternalClose()
		{
			if (this.writer != null)
			{
				this.writer.Flush();
				this.writer.Close();
				this.writer.Dispose();
			}
			this.writer = null;
		}

		// Token: 0x06003704 RID: 14084 RVA: 0x000B9930 File Offset: 0x000B7B30
		public bool HisTracingOpen()
		{
			object obj = this.flushLock;
			bool flag2;
			lock (obj)
			{
				this.hisTracingHasCalledOpen = true;
				flag2 = this.InternalOpen(false);
			}
			return flag2;
		}

		// Token: 0x06003705 RID: 14085 RVA: 0x000B997C File Offset: 0x000B7B7C
		public void HisTracingClose()
		{
			object obj = this.flushLock;
			lock (obj)
			{
				this.hisTracingHasCalledOpen = false;
				this.InternalClose();
			}
		}

		// Token: 0x06003706 RID: 14086 RVA: 0x000B99C4 File Offset: 0x000B7BC4
		public void HandleDomainUnload(object sender, EventArgs evt)
		{
			object obj = this.flushLock;
			lock (obj)
			{
				this.inTearDown = true;
				this.InternalClose();
			}
		}

		// Token: 0x06003707 RID: 14087 RVA: 0x000B9A0C File Offset: 0x000B7C0C
		public void HandleProcessExit(object sender, EventArgs evt)
		{
			object obj = this.flushLock;
			lock (obj)
			{
				this.inTearDown = true;
				this.InternalClose();
			}
		}

		// Token: 0x04001F81 RID: 8065
		private string fileNamePreamble = "HisTracing";

		// Token: 0x04001F82 RID: 8066
		private string traceFileFolder;

		// Token: 0x04001F83 RID: 8067
		private int maxTraceEntries = 1000000;

		// Token: 0x04001F84 RID: 8068
		private uint numberOfLinesWritten;

		// Token: 0x04001F85 RID: 8069
		private StreamWriter writer;

		// Token: 0x04001F86 RID: 8070
		private object flushLock = new object();

		// Token: 0x04001F87 RID: 8071
		private bool hisTracingHasCalledOpen;

		// Token: 0x04001F88 RID: 8072
		private bool registeredForAppDomainUnload;

		// Token: 0x04001F89 RID: 8073
		private bool registeredForProcessExit;

		// Token: 0x04001F8A RID: 8074
		private bool inTearDown;
	}
}
