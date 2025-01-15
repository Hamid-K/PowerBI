using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;

namespace Microsoft.ReportingServices.Diagnostics.Utilities
{
	// Token: 0x020000B4 RID: 180
	internal class RSTraceInternal : IRSTraceInternal
	{
		// Token: 0x060005BA RID: 1466 RVA: 0x00011268 File Offset: 0x0000F468
		private static RSTraceInternal CreateTraceInternal()
		{
			RSTraceInternal rstraceInternal = null;
			if (AppDomain.CurrentDomain.DomainManager != null && AppDomain.CurrentDomain.DomainManager.GetType().AssemblyQualifiedName == ProcessingContext.RsAppDomainManagerTypeName)
			{
				rstraceInternal = new RSServiceTraceInternal();
			}
			else if (ProcessingContext.Configuration != null && (ProcessingContext.Configuration.CurrentApplication == RunningApplication.WebService || ProcessingContext.Configuration.CurrentApplication == RunningApplication.WindowsService || ProcessingContext.Configuration.CurrentApplication == RunningApplication.PowerBIWebApp || ProcessingContext.Configuration.CurrentApplication == RunningApplication.OfficeWebApp))
			{
				rstraceInternal = new RSTraceInternal.RSWPTraceInternal();
			}
			if (rstraceInternal == null)
			{
				rstraceInternal = new RSTraceInternal();
			}
			return rstraceInternal;
		}

		// Token: 0x060005BB RID: 1467 RVA: 0x000112F5 File Offset: 0x0000F4F5
		private static void ThrowAssertion(string message)
		{
			throw new InternalCatalogException(message);
		}

		// Token: 0x17000206 RID: 518
		// (get) Token: 0x060005BC RID: 1468 RVA: 0x00011300 File Offset: 0x0000F500
		internal static RSTraceInternal Current
		{
			get
			{
				if (RSTraceInternal.m_traceInternal == null)
				{
					RSTraceInternal rstraceInternal = RSTraceInternal.CreateTraceInternal();
					Interlocked.CompareExchange<RSTraceInternal>(ref RSTraceInternal.m_traceInternal, rstraceInternal, null);
				}
				return RSTraceInternal.m_traceInternal;
			}
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x00011330 File Offset: 0x0000F530
		internal static void Cleanup()
		{
			IDisposable disposable = RSTraceInternal.m_traceInternal as IDisposable;
			if (disposable != null)
			{
				disposable.Dispose();
			}
			RSTraceInternal.m_traceInternal = null;
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x00011357 File Offset: 0x0000F557
		internal static void SetAsTrace()
		{
			RSTrace.SetTrace(RSTraceInternal.Current);
		}

		// Token: 0x060005BF RID: 1471 RVA: 0x00011363 File Offset: 0x0000F563
		public virtual void Trace(string componentName, string message)
		{
			this.Trace(TraceLevel.Off, componentName, message);
		}

		// Token: 0x060005C0 RID: 1472 RVA: 0x0001136E File Offset: 0x0000F56E
		public virtual void Trace(TraceLevel traceLevel, string componentName, string message)
		{
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00011370 File Offset: 0x0000F570
		public virtual void TraceWithDetails(TraceLevel traceLevel, string componentName, string message, string details)
		{
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00011372 File Offset: 0x0000F572
		public virtual void TraceException(TraceLevel traceLevel, string componentName, string message)
		{
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00011374 File Offset: 0x0000F574
		public virtual void Trace(string componentName, string format, params object[] arg)
		{
			this.Trace(TraceLevel.Off, componentName, format, arg);
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x00011380 File Offset: 0x0000F580
		public virtual void Trace(TraceLevel traceLevel, string componentName, string format, params object[] arg)
		{
			this.Trace(traceLevel, componentName, string.Format(CultureInfo.InvariantCulture, format, arg));
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x00011397 File Offset: 0x0000F597
		public virtual void TraceWithNoEventLog(TraceLevel traceLevel, string componentName, string format, params object[] arg)
		{
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x00011399 File Offset: 0x0000F599
		internal static string DefaultAssertMessage(string componentName)
		{
			return string.Format(CultureInfo.InvariantCulture, "Un-named assertion fired for component {0}", componentName);
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000113AB File Offset: 0x0000F5AB
		public virtual void Fail(string componentName)
		{
			RSTraceInternal.ThrowAssertion(RSTraceInternal.DefaultAssertMessage(componentName));
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000113B8 File Offset: 0x0000F5B8
		public virtual void Fail(string componentName, string message)
		{
			RSTraceInternal.ThrowAssertion(message);
		}

		// Token: 0x17000207 RID: 519
		// (get) Token: 0x060005C9 RID: 1481 RVA: 0x000113C0 File Offset: 0x0000F5C0
		public virtual string TraceDirectory
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x17000208 RID: 520
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x000113C7 File Offset: 0x0000F5C7
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x000113CA File Offset: 0x0000F5CA
		public virtual bool AutoFlush
		{
			get
			{
				return true;
			}
			set
			{
			}
		}

		// Token: 0x17000209 RID: 521
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x000113CC File Offset: 0x0000F5CC
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x000113CF File Offset: 0x0000F5CF
		public virtual bool BufferOutput
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x1700020A RID: 522
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x000113D1 File Offset: 0x0000F5D1
		public bool IsTraceInitialized
		{
			get
			{
				return RSTraceInternal.m_traceInitialized > 0;
			}
		}

		// Token: 0x060005CF RID: 1487 RVA: 0x000113DD File Offset: 0x0000F5DD
		public virtual void ClearBuffer()
		{
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x000113DF File Offset: 0x0000F5DF
		public virtual void WriteBuffer()
		{
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x000113E1 File Offset: 0x0000F5E1
		public virtual bool GetTraceLevel(string componentName, out TraceLevel componentLevel)
		{
			componentLevel = TraceLevel.Off;
			return false;
		}

		// Token: 0x1700020B RID: 523
		// (get) Token: 0x060005D2 RID: 1490 RVA: 0x000113E7 File Offset: 0x0000F5E7
		public virtual string CurrentTraceFilePath
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000113EE File Offset: 0x0000F5EE
		public virtual string GetDefaultTraceLevel()
		{
			return "0";
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000113F5 File Offset: 0x0000F5F5
		public virtual void EnsureTraceInitializedCorrectly()
		{
		}

		// Token: 0x0400032C RID: 812
		private const string RsReportServerConfigFileName = "ReportServer\\bin\\ReportingServicesService.exe.config";

		// Token: 0x0400032D RID: 813
		private const string ReportServerPathAppKey = "ReportServerPath";

		// Token: 0x0400032E RID: 814
		private static RSTraceInternal m_traceInternal;

		// Token: 0x0400032F RID: 815
		internal static volatile int m_traceInitialized;

		// Token: 0x04000330 RID: 816
		private const string AssertFormat = "Un-named assertion fired for component {0}";

		// Token: 0x02000104 RID: 260
		internal class RSWPTraceInternal : RSTraceInternal
		{
			// Token: 0x06000807 RID: 2055 RVA: 0x00014F6C File Offset: 0x0001316C
			internal RSWPTraceInternal()
			{
				Process currentProcess = Process.GetCurrentProcess();
				this.m_CachedProcessID = currentProcess.Id.ToString("x", CultureInfo.InvariantCulture);
				try
				{
					this.m_CachedProcessName = currentProcess.MainModule.ModuleName;
				}
				catch (Win32Exception)
				{
					this.m_CachedProcessName = "Unknown";
				}
				this.m_CachedAppDomain = AppDomain.CurrentDomain.FriendlyName;
				if (StringSupport.EndsWith(this.m_CachedProcessName, ".exe", true, CultureInfo.InvariantCulture))
				{
					this.m_CachedProcessName = this.m_CachedProcessName.Substring(0, this.m_CachedProcessName.Length - 4);
				}
				this.ReadTraceParameters();
				this.CreateTraceListeners();
				RSTraceInternal.m_traceInitialized = 1;
			}

			// Token: 0x06000808 RID: 2056 RVA: 0x000150C8 File Offset: 0x000132C8
			private void WriteTraceHeader(XmlWriter writer, string filePath)
			{
				writer.WriteStartElement("Header");
				writer.WriteElementString("Product", ProcessingContext.Configuration.ServerProductNameAndVersion);
				writer.WriteElementString("Locale", CultureInfo.InstalledUICulture.ToString());
				TimeZone currentTimeZone = TimeZone.CurrentTimeZone;
				writer.WriteElementString("TimeZone", currentTimeZone.IsDaylightSavingTime(DateTime.Now) ? currentTimeZone.DaylightName : currentTimeZone.StandardName);
				writer.WriteElementString("Path", filePath);
				writer.WriteElementString("SystemName", Environment.MachineName);
				OperatingSystem osversion = Environment.OSVersion;
				writer.WriteElementString("OSName", osversion.ToString());
				Version version = osversion.Version;
				writer.WriteElementString("OSVersion", version.ToString());
				writer.WriteEndElement();
			}

			// Token: 0x06000809 RID: 2057 RVA: 0x00015188 File Offset: 0x00013388
			private string GetTraceHeader(string fileName)
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				this.WriteTraceHeader(new XmlTextWriter(stringWriter)
				{
					Formatting = Formatting.Indented
				}, fileName);
				return stringWriter.ToString();
			}

			// Token: 0x0600080A RID: 2058 RVA: 0x000151BA File Offset: 0x000133BA
			public override void Trace(string componentName, string message)
			{
				if (this.m_ListenersToInstall > 0U && this.IsTracingEnabled(TraceLevel.Info, componentName))
				{
					this.TraceInternal(TraceLevel.Info, componentName, message, false, false, true);
				}
			}

			// Token: 0x0600080B RID: 2059 RVA: 0x000151DB File Offset: 0x000133DB
			public override void Trace(TraceLevel traceLevel, string componentName, string message)
			{
				if (this.m_ListenersToInstall > 0U && this.IsTracingEnabled(traceLevel, componentName))
				{
					this.TraceInternal(traceLevel, componentName, message, false, false, true);
				}
			}

			// Token: 0x0600080C RID: 2060 RVA: 0x000151FC File Offset: 0x000133FC
			public override void TraceException(TraceLevel traceLevel, string componentName, string message)
			{
				if (this.m_ListenersToInstall > 0U && this.IsTracingEnabled(TraceLevel.Error, componentName))
				{
					this.TraceInternal(TraceLevel.Error, componentName, message, false, true, true);
				}
			}

			// Token: 0x0600080D RID: 2061 RVA: 0x0001521D File Offset: 0x0001341D
			public override void Trace(string componentName, string format, params object[] arg)
			{
				if (this.m_ListenersToInstall > 0U && this.IsTracingEnabled(TraceLevel.Info, componentName))
				{
					this.TraceInternal(TraceLevel.Info, componentName, string.Format(CultureInfo.InvariantCulture, format, arg), false, false, true);
				}
			}

			// Token: 0x0600080E RID: 2062 RVA: 0x00015249 File Offset: 0x00013449
			public override void Trace(TraceLevel traceLevel, string componentName, string format, params object[] arg)
			{
				if (this.m_ListenersToInstall > 0U && this.IsTracingEnabled(traceLevel, componentName))
				{
					this.TraceInternal(traceLevel, componentName, string.Format(CultureInfo.InvariantCulture, format, arg), false, false, true);
				}
			}

			// Token: 0x0600080F RID: 2063 RVA: 0x00015276 File Offset: 0x00013476
			public override void TraceWithNoEventLog(TraceLevel traceLevel, string componentName, string format, params object[] arg)
			{
				if (this.m_ListenersToInstall > 0U && this.IsTracingEnabled(traceLevel, componentName))
				{
					this.TraceInternal(traceLevel, componentName, string.Format(CultureInfo.InvariantCulture, format, arg), false, false, false);
				}
			}

			// Token: 0x06000810 RID: 2064 RVA: 0x000152A3 File Offset: 0x000134A3
			public override void Fail(string componentName)
			{
				this.FailInternal(componentName, "Un-named assertion fired for component " + componentName);
			}

			// Token: 0x06000811 RID: 2065 RVA: 0x000152B7 File Offset: 0x000134B7
			public override void Fail(string componentName, string message)
			{
				this.FailInternal(componentName, message);
			}

			// Token: 0x170002D8 RID: 728
			// (get) Token: 0x06000812 RID: 2066 RVA: 0x000152C1 File Offset: 0x000134C1
			// (set) Token: 0x06000813 RID: 2067 RVA: 0x000152C9 File Offset: 0x000134C9
			public override bool AutoFlush
			{
				get
				{
					return this.m_IsAutoFlush;
				}
				set
				{
					this.m_IsAutoFlush = value;
				}
			}

			// Token: 0x170002D9 RID: 729
			// (get) Token: 0x06000814 RID: 2068 RVA: 0x000152D2 File Offset: 0x000134D2
			// (set) Token: 0x06000815 RID: 2069 RVA: 0x000152DC File Offset: 0x000134DC
			public override bool BufferOutput
			{
				get
				{
					return this.m_bufferOutput;
				}
				set
				{
					this.m_bufferOutput = value;
				}
			}

			// Token: 0x06000816 RID: 2070 RVA: 0x000152E8 File Offset: 0x000134E8
			public override void ClearBuffer()
			{
				object bufferLockObject = this.m_bufferLockObject;
				lock (bufferLockObject)
				{
					this.m_buffer.Length = 0;
				}
			}

			// Token: 0x06000817 RID: 2071 RVA: 0x00015330 File Offset: 0x00013530
			public override void WriteBuffer()
			{
				try
				{
					string text = null;
					object bufferLockObject = this.m_bufferLockObject;
					lock (bufferLockObject)
					{
						text = this.m_buffer.ToString();
						this.ClearBuffer();
					}
					if (!string.IsNullOrEmpty(text))
					{
						this.WriteToListners(text);
					}
				}
				catch
				{
					RSEventLog.Current.WriteWarning(Event.FailedTraceWrite, Array.Empty<object>());
				}
			}

			// Token: 0x170002DA RID: 730
			// (get) Token: 0x06000818 RID: 2072 RVA: 0x000153B0 File Offset: 0x000135B0
			private string ReportingServicesBaseInstallPath
			{
				get
				{
					string text = Path.GetDirectoryName(Assembly.GetExecutingAssembly().CodeBase);
					if (ProcessingContext.Configuration.CurrentApplication == RunningApplication.ReportServerWebApp || ProcessingContext.Configuration.CurrentApplication == RunningApplication.PowerBIWebApp || ProcessingContext.Configuration.CurrentApplication == RunningApplication.OfficeWebApp)
					{
						if (!string.IsNullOrEmpty(ConfigurationManager.AppSettings["ReportServerPath"]))
						{
							DirectoryInfo directoryInfo = new DirectoryInfo(ConfigurationManager.AppSettings["ReportServerPath"]);
							if (directoryInfo.Parent != null)
							{
								text = directoryInfo.Parent.FullName;
							}
						}
						else
						{
							FileInfo fileInfo = new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
							if (fileInfo.Directory.Parent != null)
							{
								text = fileInfo.Directory.Parent.FullName;
							}
						}
					}
					else
					{
						FileInfo fileInfo2 = new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
						if (fileInfo2.Directory.Parent != null && fileInfo2.Directory.Parent.Parent != null)
						{
							text = fileInfo2.Directory.Parent.Parent.FullName;
						}
					}
					return text;
				}
			}

			// Token: 0x170002DB RID: 731
			// (get) Token: 0x06000819 RID: 2073 RVA: 0x000154C8 File Offset: 0x000136C8
			protected virtual RSTraceInternal.RSWPTraceInternal.RSTraceConfig TraceConfig
			{
				get
				{
					if (this.m_config == null)
					{
						RSTraceInternal.RSWPTraceInternal.RSTraceConfig config = null;
						string configFileName = null;
						RevertImpersonationContext.Run(delegate
						{
							configFileName = Path.Combine(this.ReportingServicesBaseInstallPath, "ReportServer\\bin\\ReportingServicesService.exe.config");
							if (File.Exists(configFileName))
							{
								XmlDocument xmlDocument = XmlUtil.CreateXmlDocumentWithNullResolver();
								xmlDocument.LoadWithNullResolver(configFileName);
								config = new RSTraceInternal.RSWPTraceInternal.RSTraceConfig(xmlDocument);
							}
							if (config == null)
							{
								config = new RSTraceInternal.RSWPTraceInternal.RSTraceConfig();
							}
						});
						this.m_config = config;
					}
					return this.m_config;
				}
			}

			// Token: 0x170002DC RID: 732
			// (get) Token: 0x0600081A RID: 2074 RVA: 0x0001551C File Offset: 0x0001371C
			public override string TraceDirectory
			{
				get
				{
					if (string.IsNullOrEmpty(this.m_traceDirectory))
					{
						this.m_traceDirectory = this.TraceConfig.Directory;
						if (string.IsNullOrEmpty(this.m_traceDirectory))
						{
							this.m_traceDirectory = Path.Combine(this.ReportingServicesBaseInstallPath, "LogFiles");
						}
					}
					return this.m_traceDirectory;
				}
			}

			// Token: 0x0600081B RID: 2075 RVA: 0x00015570 File Offset: 0x00013770
			private void ReadTraceParameters()
			{
				try
				{
					string text = this.TraceConfig.TraceListeners;
					this.m_ListenersToInstall = 0U;
					if (-1 != text.IndexOf("debugwindow", StringComparison.Ordinal))
					{
						this.m_ListenersToInstall |= 1U;
					}
					if (-1 != text.IndexOf("file", StringComparison.Ordinal))
					{
						this.m_ListenersToInstall |= 2U;
					}
					if (-1 != text.IndexOf("stdout", StringComparison.Ordinal))
					{
						this.m_ListenersToInstall |= 4U;
					}
					this.m_traceFileName = this.TraceConfig.FileName;
					if (this.TraceConfig.FileSizeLimitMB < 1)
					{
						this.m_maxFileSizeBytes = 1048576;
					}
					else
					{
						this.m_maxFileSizeBytes = checked(this.TraceConfig.FileSizeLimitMB * 1024 * 1024);
					}
					RSTrace.ConfigManagerTracer.Trace(TraceLevel.Verbose, "Setting max file size to {0} bytes", new object[] { this.m_maxFileSizeBytes });
					try
					{
						if (this.TraceConfig.KeepFilesForDays != null)
						{
							int num = int.Parse(this.TraceConfig.KeepFilesForDays, CultureInfo.InvariantCulture);
							if (num < 1)
							{
								num = 1;
							}
							this.m_keepFileForDays = num;
						}
						RSTrace.ConfigManagerTracer.Trace(TraceLevel.Verbose, "Setting preserve files to {0} days", new object[] { this.m_keepFileForDays });
					}
					catch (FormatException)
					{
						RSTrace.ConfigManagerTracer.Trace(TraceLevel.Error, "FormatException parsing KeepFilesForDays in RSTrace section of config file.");
					}
					catch (OverflowException)
					{
						RSTrace.ConfigManagerTracer.Trace(TraceLevel.Error, "OverflowException parsing KeepFilesForDays in RSTrace section of config file.");
					}
					text = this.TraceConfig.Prefix;
					if (-1 != text.IndexOf("time", StringComparison.Ordinal))
					{
						this.m_IsTimePrefixed = true;
					}
					if (-1 != text.IndexOf("pid", StringComparison.Ordinal))
					{
						this.m_IsPidPrefixed = true;
					}
					if (-1 != text.IndexOf("tid", StringComparison.Ordinal))
					{
						this.m_IsTidPrefixed = true;
					}
					if (-1 != text.IndexOf("appdomain", StringComparison.Ordinal))
					{
						this.m_IsAppDomainPrefixed = true;
					}
					RSTrace.ConfigManagerTracer.Trace(TraceLevel.Verbose, "Setting prefixes (Time:{0}, PID:{1}, ThreadId:{2}, AppDomain:{3})", new object[] { this.m_IsTimePrefixed, this.m_IsPidPrefixed, this.m_IsTidPrefixed, "appdomain" });
					text = this.TraceConfig.TraceFileMode;
					if (!(text == "overwrite"))
					{
						if (!(text == "append"))
						{
							this.m_TraceFileMode = RSTraceInternal.RSWPTraceInternal.TraceFileMode.Unique;
						}
						else
						{
							this.m_TraceFileMode = RSTraceInternal.RSWPTraceInternal.TraceFileMode.Append;
						}
					}
					else
					{
						this.m_TraceFileMode = RSTraceInternal.RSWPTraceInternal.TraceFileMode.Overwrite;
					}
					RSTrace.ConfigManagerTracer.Trace(TraceLevel.Verbose, "Setting trace file mode to {0}", new object[] { this.m_TraceFileMode });
					try
					{
						this.m_defaultTraceLevel = (TraceLevel)Enum.Parse(this.m_defaultTraceLevel.GetType(), this.TraceConfig.DefaultTraceSwitch, true);
					}
					catch
					{
					}
					this.FillComponentTable(this.TraceConfig.Components);
					RevertImpersonationContext.Run(delegate
					{
						RSTrace.ConfigManagerTracer.Trace(TraceLevel.Verbose, "Setting trace directory {0}", new object[] { this.TraceDirectory });
						try
						{
							if (!Directory.Exists(this.TraceDirectory))
							{
								RSTrace.ConfigManagerTracer.Trace(TraceLevel.Info, "Creating trace directory {0}", new object[] { this.TraceDirectory });
								Directory.CreateDirectory(this.TraceDirectory);
							}
							else
							{
								string text2 = this.TraceDirectory + "\\_flag_" + Guid.NewGuid().ToString();
								using (new FileStream(text2, FileMode.CreateNew, FileAccess.Write))
								{
								}
								File.Delete(text2);
							}
						}
						catch (UnauthorizedAccessException)
						{
							RSTrace.ConfigManagerTracer.Trace(TraceLevel.Error, "Cannot write to log directory at {0}", new object[] { this.TraceDirectory });
							this.m_traceDirectory = Environment.GetEnvironmentVariable("TEMP");
							this.m_traceDirectory = Path.Combine(this.m_traceDirectory, "LogFiles");
							if (!Directory.Exists(this.m_traceDirectory))
							{
								Directory.CreateDirectory(this.m_traceDirectory);
							}
							this.m_traceFileInNewLocation = true;
						}
					});
				}
				catch (Exception)
				{
					throw;
				}
			}

			// Token: 0x0600081C RID: 2076 RVA: 0x000158A0 File Offset: 0x00013AA0
			private void FillComponentTable(string componentsString)
			{
				string[] array = componentsString.Split(new char[] { ',', ';' });
				for (int i = 0; i < array.Length; i++)
				{
					string text = array[i].Trim().ToLower(CultureInfo.InvariantCulture);
					string[] array2 = text.Split(new char[] { ':' });
					if (array2.Length > 2)
					{
						throw new ServerConfigurationErrorException("bad configuration element" + text);
					}
					string text2 = array2[0];
					if (array2.Length == 2 && array2[1] != null)
					{
						string text3 = array2[1];
						TraceLevel traceLevel = this.m_defaultTraceLevel;
						try
						{
							traceLevel = (TraceLevel)Enum.Parse(traceLevel.GetType(), text3, true);
						}
						catch (Exception ex)
						{
							throw new ServerConfigurationErrorException(ex, "bad component trace level in configuration file" + text3);
						}
						this.m_Components.Add(text2, traceLevel);
					}
					else
					{
						this.m_Components.Add(text, this.m_defaultTraceLevel);
					}
				}
				string text4 = RSTrace.TraceComponents.PowerView.ToString().ToLowerInvariant();
				this.m_Components.Remove(text4);
				this.m_Components.Add(text4, TraceLevel.Off);
				string text5 = RSTrace.TraceComponents.RdlEngineHost.ToString().ToLowerInvariant();
				this.m_Components.Remove(text5);
				this.m_Components.Add(text5, TraceLevel.Off);
			}

			// Token: 0x0600081D RID: 2077 RVA: 0x00015A0C File Offset: 0x00013C0C
			public override bool GetTraceLevel(string componentName, out TraceLevel componentLevel)
			{
				componentLevel = this.GetComponentTraceLevel(componentName);
				return true;
			}

			// Token: 0x0600081E RID: 2078 RVA: 0x00015A18 File Offset: 0x00013C18
			private TraceLevel GetComponentTraceLevel(string componentName)
			{
				TraceLevel defaultTraceLevel;
				if (!this.m_Components.TryGetValue(componentName, out defaultTraceLevel) && !this.m_Components.TryGetValue("all", out defaultTraceLevel))
				{
					defaultTraceLevel = this.m_defaultTraceLevel;
				}
				return defaultTraceLevel;
			}

			// Token: 0x0600081F RID: 2079 RVA: 0x00015A54 File Offset: 0x00013C54
			private void CreateTraceListeners()
			{
				try
				{
					if ((this.m_ListenersToInstall & 1U) != 0U)
					{
						this.m_Listeners.Add(new DefaultTraceListener());
					}
					if ((this.m_ListenersToInstall & 4U) != 0U)
					{
						TextWriterTraceListener textWriterTraceListener = new TextWriterTraceListener(Console.Out);
						this.m_Listeners.Add(textWriterTraceListener);
					}
					if ((this.m_ListenersToInstall & 2U) != 0U && this.m_TraceFilePath.Length > 0)
					{
						TextWriterTraceListener textWriterTraceListener2 = this.CreateFileListener();
						if (textWriterTraceListener2 != null)
						{
							this.m_Listeners.Add(textWriterTraceListener2);
						}
					}
				}
				catch (Exception)
				{
					throw;
				}
			}

			// Token: 0x06000820 RID: 2080 RVA: 0x00015AE4 File Offset: 0x00013CE4
			private void CleanOldFiles(int daysToKeep)
			{
				try
				{
					string[] files = Directory.GetFiles(this.TraceDirectory, this.m_traceFileName + "*.log");
					if (files != null)
					{
						for (int i = 0; i < files.Length; i++)
						{
							try
							{
								string text = Path.Combine(this.TraceDirectory, files[i]);
								if (File.GetLastWriteTimeUtc(text) < DateTime.UtcNow.AddDays((double)(-(double)daysToKeep)))
								{
									File.Delete(text);
								}
							}
							catch (SystemException)
							{
							}
						}
					}
				}
				catch (SystemException)
				{
				}
			}

			// Token: 0x06000821 RID: 2081 RVA: 0x00015B7C File Offset: 0x00013D7C
			private TextWriterTraceListener CreateFileListener()
			{
				TextWriterTraceListener listener = null;
				RevertImpersonationContext.Run(delegate
				{
					if (this.m_traceFileName.Length == 0)
					{
						this.m_traceFileName = this.m_CachedProcessName;
					}
					if (this.m_TraceFileMode == RSTraceInternal.RSWPTraceInternal.TraceFileMode.Unique)
					{
						this.CleanOldFiles(this.m_keepFileForDays);
						DateTime now = DateTime.Now;
						this.m_TraceFilePath = string.Format(CultureInfo.InvariantCulture, "{0}\\{1}_{2}_{3:00}_{4:00}_{5:00}_{6:00}_{7:00}.log", new object[] { this.TraceDirectory, this.m_traceFileName, now.Year, now.Month, now.Day, now.Hour, now.Minute, now.Second });
					}
					else
					{
						this.m_TraceFilePath = string.Format(CultureInfo.InvariantCulture, "{0}\\{1}.log", this.TraceDirectory, this.m_traceFileName);
					}
					FileMode fileMode = FileMode.Create;
					FileStream fileStream = null;
					if (this.m_TraceFileMode == RSTraceInternal.RSWPTraceInternal.TraceFileMode.Append)
					{
						fileMode = FileMode.Append;
					}
					try
					{
						fileStream = File.Open(this.m_TraceFilePath, fileMode, FileAccess.Write, FileShare.ReadWrite);
					}
					catch
					{
						try
						{
							if (this.m_TraceFileMode == RSTraceInternal.RSWPTraceInternal.TraceFileMode.Overwrite)
							{
								fileStream = File.Open(this.m_TraceFilePath, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
							}
						}
						catch
						{
						}
					}
					this.m_startDate = DateTime.Today;
					if (fileStream != null)
					{
						StreamWriter streamWriter = new StreamWriter(fileStream, Encoding.Unicode);
						streamWriter.BaseStream.Seek(0L, SeekOrigin.End);
						TextWriter textWriter = TextWriter.Synchronized(streamWriter);
						textWriter.WriteLine(this.GetTraceHeader(this.m_TraceFilePath));
						TextWriterTraceListener textWriterTraceListener = new TextWriterTraceListener(textWriter);
						listener = textWriterTraceListener;
						return;
					}
					this.m_createdFileTraceCorrectly = false;
				});
				return listener;
			}

			// Token: 0x06000822 RID: 2082 RVA: 0x00015BA8 File Offset: 0x00013DA8
			private static string GetApplicationIdFromAppDomain()
			{
				string friendlyName = AppDomain.CurrentDomain.FriendlyName;
				string text = string.Empty;
				Guid empty = Guid.Empty;
				Match match = new Regex("/[^/]*/[^/]*/[^/]*/[^/]*/([^-]*)-([^-]*)-([^-]*)").Match(friendlyName);
				if (match.Groups.Count == 4)
				{
					text = match.Groups[1].Value;
					empty = new Guid(text);
				}
				return empty.ToString();
			}

			// Token: 0x170002DD RID: 733
			// (get) Token: 0x06000823 RID: 2083 RVA: 0x00015C12 File Offset: 0x00013E12
			public override string CurrentTraceFilePath
			{
				get
				{
					return this.m_TraceFilePath;
				}
			}

			// Token: 0x06000824 RID: 2084 RVA: 0x00015C1C File Offset: 0x00013E1C
			public override void EnsureTraceInitializedCorrectly()
			{
				if (!this.m_createdFileTraceCorrectly)
				{
					RSEventLog.Current.WriteError(Event.CouldNotCreateTraceFile, new object[]
					{
						RSEventLog.Current.SourceName,
						this.m_TraceFilePath
					});
				}
				if (this.m_traceFileInNewLocation)
				{
					RSEventLog.Current.WriteWarning(Event.TraceNotDefaultLocation, new object[] { this.TraceDirectory });
				}
			}

			// Token: 0x06000825 RID: 2085 RVA: 0x00015C7C File Offset: 0x00013E7C
			private void TraceInternal(TraceLevel traceLevel, string componentName, string message, bool isAssert, bool isException, bool allowEventlogWrite)
			{
				try
				{
					StringBuilder stringBuilder = new StringBuilder(128);
					stringBuilder.Append(this.m_CachedProcessName);
					stringBuilder.AppendFormat("!{0}", componentName);
					if (this.m_IsAppDomainPrefixed)
					{
						stringBuilder.AppendFormat("!{0}", this.m_CachedAppDomain);
					}
					if (this.m_IsPidPrefixed)
					{
						stringBuilder.AppendFormat("!{0}", this.m_CachedProcessID);
					}
					if (this.m_IsTidPrefixed)
					{
						stringBuilder.AppendFormat("!{0}", Thread.CurrentThread.ManagedThreadId.ToString("x", CultureInfo.InvariantCulture));
					}
					if (this.m_IsTimePrefixed)
					{
						DateTime now = DateTime.Now;
						stringBuilder.AppendFormat(CultureInfo.InvariantCulture, "!{0}-{1:00}:{2:00}:{3:00}", new object[]
						{
							now.Date.ToString("d", CultureInfo.InvariantCulture),
							now.Hour,
							now.Minute,
							now.Second
						});
					}
					stringBuilder.Append("::");
					if (isAssert)
					{
						stringBuilder.Append(" a ASSERT: ");
					}
					else
					{
						switch (traceLevel)
						{
						case TraceLevel.Error:
							stringBuilder.Append(" e ERROR: ");
							break;
						case TraceLevel.Warning:
							stringBuilder.Append(" w WARN: ");
							break;
						case TraceLevel.Info:
							stringBuilder.Append(" i INFO: ");
							break;
						case TraceLevel.Verbose:
							stringBuilder.Append(" v VERBOSE: ");
							break;
						default:
							stringBuilder.Append(" ");
							break;
						}
					}
					stringBuilder.Append(message);
					if (this.m_bufferOutput)
					{
						object bufferLockObject = this.m_bufferLockObject;
						lock (bufferLockObject)
						{
							this.m_buffer.Append(stringBuilder.ToString());
							this.m_buffer.Append("\r\n");
						}
					}
					else
					{
						this.WriteToListners(stringBuilder.ToString());
					}
				}
				catch
				{
					if (allowEventlogWrite)
					{
						RSEventLog.Current.WriteWarning(Event.FailedTraceWrite, Array.Empty<object>());
					}
				}
			}

			// Token: 0x06000826 RID: 2086 RVA: 0x00015EB0 File Offset: 0x000140B0
			private bool IsTracingEnabled(TraceLevel traceLevel, string componentName)
			{
				return this.GetComponentTraceLevel(componentName) >= traceLevel;
			}

			// Token: 0x06000827 RID: 2087 RVA: 0x00015EC0 File Offset: 0x000140C0
			private void WriteToListners(string text)
			{
				if (text != null)
				{
					this.m_sizeSoFar += text.Length * 2;
				}
				RevertImpersonationContext.Run(delegate
				{
					for (int i = 0; i < this.m_Listeners.Count; i++)
					{
						TraceListener traceListener = (TraceListener)this.m_Listeners[i];
						if (traceListener is TextWriterTraceListener && (this.m_startDate != DateTime.Today || this.m_sizeSoFar > this.m_maxFileSizeBytes))
						{
							object createFileLock = this.m_createFileLock;
							lock (createFileLock)
							{
								if (this.m_startDate != DateTime.Today || this.m_sizeSoFar > this.m_maxFileSizeBytes)
								{
									TextWriterTraceListener textWriterTraceListener = this.CreateFileListener();
									if (textWriterTraceListener != null)
									{
										this.m_sizeSoFar = 0;
										traceListener.Close();
										traceListener = textWriterTraceListener;
										this.m_Listeners[i] = traceListener;
									}
								}
								else
								{
									traceListener = this.m_Listeners[i] as TextWriterTraceListener;
								}
							}
						}
						traceListener.WriteLine(text);
						if (this.m_IsAutoFlush)
						{
							traceListener.Flush();
						}
					}
				});
			}

			// Token: 0x06000828 RID: 2088 RVA: 0x00015F14 File Offset: 0x00014114
			private void FailInternal(string componentName, string message)
			{
				if (this.m_ListenersToInstall > 0U && this.IsTracingEnabled(this.m_defaultTraceLevel, componentName))
				{
					StackTrace stackTrace = new StackTrace(true);
					StringBuilder stringBuilder = new StringBuilder(message, 1024);
					stringBuilder.Append("   Call stack:");
					StackTraceUtil.StackTraceToString(stackTrace, 3, stringBuilder);
					string text = stringBuilder.ToString();
					this.TraceInternal(TraceLevel.Error, componentName, text, true, false, true);
				}
				throw new InternalCatalogException(message);
			}

			// Token: 0x04000514 RID: 1300
			internal const string m_DebugWindow = "debugwindow";

			// Token: 0x04000515 RID: 1301
			internal const string m_File = "file";

			// Token: 0x04000516 RID: 1302
			internal const string m_StdOut = "stdout";

			// Token: 0x04000517 RID: 1303
			internal const string m_Unique = "unique";

			// Token: 0x04000518 RID: 1304
			internal const string m_Overwrite = "overwrite";

			// Token: 0x04000519 RID: 1305
			internal const string m_Append = "append";

			// Token: 0x0400051A RID: 1306
			internal const string m_Tid = "tid";

			// Token: 0x0400051B RID: 1307
			internal const string m_Pid = "pid";

			// Token: 0x0400051C RID: 1308
			internal const string m_Time = "time";

			// Token: 0x0400051D RID: 1309
			internal const string m_AppDomain = "appdomain";

			// Token: 0x0400051E RID: 1310
			private int m_sizeSoFar;

			// Token: 0x0400051F RID: 1311
			private int m_maxFileSizeBytes = 33554432;

			// Token: 0x04000520 RID: 1312
			private int m_keepFileForDays = 14;

			// Token: 0x04000521 RID: 1313
			private volatile bool m_bufferOutput;

			// Token: 0x04000522 RID: 1314
			private bool m_IsAutoFlush = true;

			// Token: 0x04000523 RID: 1315
			private ArrayList m_Listeners = new ArrayList();

			// Token: 0x04000524 RID: 1316
			private string m_CachedProcessID = string.Empty;

			// Token: 0x04000525 RID: 1317
			private string m_CachedProcessName = string.Empty;

			// Token: 0x04000526 RID: 1318
			private string m_CachedAppDomain = string.Empty;

			// Token: 0x04000527 RID: 1319
			private DateTime m_startDate;

			// Token: 0x04000528 RID: 1320
			private bool m_createdFileTraceCorrectly = true;

			// Token: 0x04000529 RID: 1321
			private bool m_traceFileInNewLocation;

			// Token: 0x0400052A RID: 1322
			private string m_traceFileName;

			// Token: 0x0400052B RID: 1323
			private string m_TraceFilePath = "trace.log";

			// Token: 0x0400052C RID: 1324
			private uint m_ListenersToInstall;

			// Token: 0x0400052D RID: 1325
			private bool m_IsTimePrefixed = true;

			// Token: 0x0400052E RID: 1326
			private bool m_IsPidPrefixed;

			// Token: 0x0400052F RID: 1327
			private bool m_IsTidPrefixed = true;

			// Token: 0x04000530 RID: 1328
			private bool m_IsAppDomainPrefixed;

			// Token: 0x04000531 RID: 1329
			private RSTraceInternal.RSWPTraceInternal.TraceFileMode m_TraceFileMode;

			// Token: 0x04000532 RID: 1330
			private const string AllComponentsNameString = "all";

			// Token: 0x04000533 RID: 1331
			private TraceLevel m_defaultTraceLevel = TraceLevel.Info;

			// Token: 0x04000534 RID: 1332
			private StringBuilder m_buffer = new StringBuilder();

			// Token: 0x04000535 RID: 1333
			private readonly object m_bufferLockObject = new object();

			// Token: 0x04000536 RID: 1334
			private Dictionary<string, TraceLevel> m_Components = new Dictionary<string, TraceLevel>();

			// Token: 0x04000537 RID: 1335
			private RSTraceInternal.RSWPTraceInternal.RSTraceConfig m_config;

			// Token: 0x04000538 RID: 1336
			private string m_traceDirectory;

			// Token: 0x04000539 RID: 1337
			private object m_createFileLock = new object();

			// Token: 0x0200011F RID: 287
			internal enum TraceListeners : uint
			{
				// Token: 0x0400060C RID: 1548
				DebugWindow = 1U,
				// Token: 0x0400060D RID: 1549
				File,
				// Token: 0x0400060E RID: 1550
				StdOut = 4U
			}

			// Token: 0x02000120 RID: 288
			internal enum TraceFileMode
			{
				// Token: 0x04000610 RID: 1552
				Unique,
				// Token: 0x04000611 RID: 1553
				Overwrite,
				// Token: 0x04000612 RID: 1554
				Append
			}

			// Token: 0x02000121 RID: 289
			internal class RSTraceConfig
			{
				// Token: 0x06000847 RID: 2119 RVA: 0x000165DE File Offset: 0x000147DE
				internal RSTraceConfig()
				{
					this.SetDefaultValues();
				}

				// Token: 0x06000848 RID: 2120 RVA: 0x000165EC File Offset: 0x000147EC
				private void SetDefaultValues()
				{
					this.m_TraceListeners = "debugwindow, file";
					this.m_Prefix = "tid, time";
					this.m_Directory = string.Empty;
					this.m_FileName = string.Empty;
					this.m_TraceFileMode = "unique";
					this.m_Components = "all:3";
				}

				// Token: 0x06000849 RID: 2121 RVA: 0x0001663C File Offset: 0x0001483C
				internal RSTraceConfig(XmlDocument configDoc)
				{
					this.SetDefaultValues();
					if (configDoc == null)
					{
						throw new ArgumentNullException("configDoc");
					}
					XmlNode xmlNode = configDoc.SelectSingleNode("/configuration/system.diagnostics/switches");
					if (xmlNode != null)
					{
						foreach (object obj in xmlNode.ChildNodes)
						{
							XmlNode xmlNode2 = (XmlNode)obj;
							string text = xmlNode2.Attributes["name"].Value.ToUpperInvariant();
							string text2 = xmlNode2.Attributes["value"].Value.ToLower(CultureInfo.InvariantCulture);
							if (text == "DEFAULTTRACESWITCH")
							{
								this.DefaultTraceSwitch = text2;
							}
						}
					}
					XmlNode xmlNode3 = configDoc.SelectSingleNode("/configuration/RStrace");
					if (xmlNode3 != null)
					{
						foreach (object obj2 in xmlNode3.ChildNodes)
						{
							XmlNode xmlNode4 = (XmlNode)obj2;
							string text3 = xmlNode4.Attributes["name"].Value.ToUpperInvariant();
							string text4 = xmlNode4.Attributes["value"].Value.ToLower(CultureInfo.InvariantCulture);
							RSTrace.ConfigManagerTracer.Trace(TraceLevel.Verbose, "Reading trace settings {0}={1}", new object[] { text3, text4 });
							if (text3 != null)
							{
								switch (text3.Length)
								{
								case 6:
									if (text3 == "PREFIX")
									{
										this.Prefix = text4;
									}
									break;
								case 9:
									if (text3 == "DIRECTORY")
									{
										this.Directory = text4;
									}
									break;
								case 10:
									if (text3 == "COMPONENTS")
									{
										this.Components = text4;
									}
									break;
								case 13:
									if (text3 == "TRACEFILEMODE")
									{
										this.TraceFileMode = text4;
									}
									break;
								case 14:
									if (text3 == "TRACELISTENERS")
									{
										this.TraceListeners = text4;
									}
									break;
								case 15:
									if (text3 == "FILESIZELIMITMB")
									{
										int num;
										int.TryParse(text4, NumberStyles.Integer, CultureInfo.InvariantCulture, out num);
										this.FileSizeLimitMB = ((num > 0) ? num : 1);
									}
									break;
								case 16:
									if (text3 == "KEEPFILESFORDAYS")
									{
										this.KeepFilesForDays = text4;
									}
									break;
								}
							}
						}
					}
				}

				// Token: 0x170002E0 RID: 736
				// (get) Token: 0x0600084A RID: 2122 RVA: 0x00016900 File Offset: 0x00014B00
				// (set) Token: 0x0600084B RID: 2123 RVA: 0x00016908 File Offset: 0x00014B08
				internal string DefaultTraceSwitch
				{
					get
					{
						return this.m_DefaultTraceSwitch;
					}
					set
					{
						this.m_DefaultTraceSwitch = value;
					}
				}

				// Token: 0x170002E1 RID: 737
				// (get) Token: 0x0600084C RID: 2124 RVA: 0x00016911 File Offset: 0x00014B11
				// (set) Token: 0x0600084D RID: 2125 RVA: 0x00016919 File Offset: 0x00014B19
				internal string TraceListeners
				{
					get
					{
						return this.m_TraceListeners;
					}
					set
					{
						this.m_TraceListeners = value;
					}
				}

				// Token: 0x170002E2 RID: 738
				// (get) Token: 0x0600084E RID: 2126 RVA: 0x00016922 File Offset: 0x00014B22
				// (set) Token: 0x0600084F RID: 2127 RVA: 0x0001692A File Offset: 0x00014B2A
				internal string Prefix
				{
					get
					{
						return this.m_Prefix;
					}
					set
					{
						this.m_Prefix = value;
					}
				}

				// Token: 0x170002E3 RID: 739
				// (get) Token: 0x06000850 RID: 2128 RVA: 0x00016933 File Offset: 0x00014B33
				// (set) Token: 0x06000851 RID: 2129 RVA: 0x0001693B File Offset: 0x00014B3B
				internal string Directory
				{
					get
					{
						return this.m_Directory;
					}
					set
					{
						this.m_Directory = value;
					}
				}

				// Token: 0x170002E4 RID: 740
				// (get) Token: 0x06000852 RID: 2130 RVA: 0x00016944 File Offset: 0x00014B44
				// (set) Token: 0x06000853 RID: 2131 RVA: 0x0001694C File Offset: 0x00014B4C
				internal string FileName
				{
					get
					{
						return this.m_FileName;
					}
					set
					{
						this.m_FileName = value;
					}
				}

				// Token: 0x170002E5 RID: 741
				// (get) Token: 0x06000854 RID: 2132 RVA: 0x00016955 File Offset: 0x00014B55
				// (set) Token: 0x06000855 RID: 2133 RVA: 0x0001695D File Offset: 0x00014B5D
				internal string TraceFileMode
				{
					get
					{
						return this.m_TraceFileMode;
					}
					set
					{
						this.m_TraceFileMode = value;
					}
				}

				// Token: 0x170002E6 RID: 742
				// (get) Token: 0x06000856 RID: 2134 RVA: 0x00016966 File Offset: 0x00014B66
				// (set) Token: 0x06000857 RID: 2135 RVA: 0x0001696E File Offset: 0x00014B6E
				internal string Components
				{
					get
					{
						return this.m_Components;
					}
					set
					{
						this.m_Components = value;
					}
				}

				// Token: 0x170002E7 RID: 743
				// (get) Token: 0x06000858 RID: 2136 RVA: 0x00016977 File Offset: 0x00014B77
				// (set) Token: 0x06000859 RID: 2137 RVA: 0x0001697F File Offset: 0x00014B7F
				internal int FileSizeLimitMB
				{
					get
					{
						return this.m_FileSizeLimitMB;
					}
					set
					{
						this.m_FileSizeLimitMB = value;
					}
				}

				// Token: 0x170002E8 RID: 744
				// (get) Token: 0x0600085A RID: 2138 RVA: 0x00016988 File Offset: 0x00014B88
				// (set) Token: 0x0600085B RID: 2139 RVA: 0x00016990 File Offset: 0x00014B90
				internal string KeepFilesForDays
				{
					get
					{
						return this.m_KeepFilesForDays;
					}
					set
					{
						this.m_KeepFilesForDays = value;
					}
				}

				// Token: 0x04000613 RID: 1555
				private int m_FileSizeLimitMB;

				// Token: 0x04000614 RID: 1556
				private string m_TraceListeners;

				// Token: 0x04000615 RID: 1557
				private string m_Prefix;

				// Token: 0x04000616 RID: 1558
				private string m_Directory;

				// Token: 0x04000617 RID: 1559
				private string m_FileName;

				// Token: 0x04000618 RID: 1560
				private string m_TraceFileMode;

				// Token: 0x04000619 RID: 1561
				private string m_Components;

				// Token: 0x0400061A RID: 1562
				private string m_KeepFilesForDays;

				// Token: 0x0400061B RID: 1563
				private string m_DefaultTraceSwitch;
			}
		}
	}
}
