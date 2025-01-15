using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.IO;
using System.Security.AccessControl;
using System.Security.Principal;
using System.Threading;
using System.Xml;
using Microsoft.HostIntegration.ConfigurationSectionHandlers.Tracing;
using Microsoft.HostIntegration.StrictResources.TracingRuntime;
using Microsoft.Win32;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x02000674 RID: 1652
	public class TraceRuntime
	{
		// Token: 0x0600378E RID: 14222 RVA: 0x000BAF08 File Offset: 0x000B9108
		public static void SetConfiguration(TracingConfigurationSectionHandler programmaticSection)
		{
			TraceRuntime.ReadConfigurationIfNeeded(programmaticSection);
		}

		// Token: 0x0600378F RID: 14223 RVA: 0x000BAF10 File Offset: 0x000B9110
		public static void AddContainer(ITraceContainer container)
		{
			TraceRuntime.ReadConfigurationIfNeeded(null);
			List<ITraceContainer> list = TraceRuntime.traceContainers;
			lock (list)
			{
				TraceRuntime.traceContainers.Add(container);
				XmlNode xmlNode = TraceRuntime.FindXmlForContainer(container);
				container.IsInConfigurationFile = xmlNode != null;
				TraceTree traceTree = new TraceTree(container, xmlNode);
				container.TraceTree = traceTree;
				TraceRuntime.CheckTracing();
			}
		}

		// Token: 0x06003790 RID: 14224 RVA: 0x000BAF80 File Offset: 0x000B9180
		private static XmlNode FindXmlForContainer(ITraceContainer container)
		{
			XmlNode xmlNode = null;
			if (TraceRuntime.xmlOfContainers != null)
			{
				foreach (object obj in TraceRuntime.xmlOfContainers)
				{
					XmlNode xmlNode2 = (XmlNode)obj;
					XmlAttribute xmlAttribute = xmlNode2.Attributes["name"];
					if (xmlAttribute == null)
					{
						throw new TraceException(SR.ContainerNodeNameAttribute);
					}
					if (string.Equals(xmlAttribute.Value, container.Name, StringComparison.InvariantCulture))
					{
						XmlAttribute xmlAttribute2 = xmlNode2.Attributes["instanceName"];
						if (container.SupportsInstances)
						{
							if (xmlAttribute2 == null)
							{
								throw new TraceException(SR.ContainerNodeInstanceNameAttribute);
							}
							if (!string.Equals(xmlAttribute2.Value, container.InstanceName, StringComparison.InvariantCulture))
							{
								continue;
							}
						}
						else if (xmlAttribute2 != null)
						{
							throw new TraceException(SR.ContainerNodeInstanceNameAttributeIllegal);
						}
						xmlNode = xmlNode2;
						break;
					}
				}
			}
			return xmlNode;
		}

		// Token: 0x06003791 RID: 14225 RVA: 0x000BB068 File Offset: 0x000B9268
		private static void ReadConfigurationIfNeeded(TracingConfigurationSectionHandler programmaticSection)
		{
			if (TraceRuntime.readConfiguration)
			{
				return;
			}
			object obj = TraceRuntime.lockObject;
			lock (obj)
			{
				if (!TraceRuntime.readConfiguration)
				{
					TraceRuntime.ReadConfiguration(programmaticSection);
					TraceRuntime.readConfiguration = true;
				}
			}
		}

		// Token: 0x06003792 RID: 14226 RVA: 0x000BB0C0 File Offset: 0x000B92C0
		private static string CanWriteToTraceFileFolder(string traceFileFolder)
		{
			if (!string.IsNullOrWhiteSpace(traceFileFolder))
			{
				traceFileFolder = Environment.ExpandEnvironmentVariables(traceFileFolder);
			}
			if (!string.IsNullOrWhiteSpace(traceFileFolder))
			{
				DirectoryInfo directoryInfo = null;
				try
				{
					directoryInfo = new DirectoryInfo(traceFileFolder);
				}
				catch (Exception)
				{
					return SR.DirectoryNameInvalid(traceFileFolder);
				}
				if (!directoryInfo.Exists)
				{
					return SR.DirectoryDoesntExist(traceFileFolder);
				}
				traceFileFolder = directoryInfo.FullName;
				traceFileFolder = (traceFileFolder.EndsWith("\\") ? traceFileFolder : (traceFileFolder + "\\"));
			}
			if (string.IsNullOrWhiteSpace(traceFileFolder))
			{
				try
				{
					RegistryKey registryKey = RegistryKey.OpenBaseKey(RegistryHive.LocalMachine, RegistryView.Registry64);
					RegistryKey registryKey2 = registryKey.OpenSubKey("\\Software\\Microsoft\\Host Integration Server");
					if (registryKey2 != null)
					{
						string text = (string)registryKey2.GetValue("InstallPath");
						if (text != null)
						{
							traceFileFolder = (text.EndsWith("\\") ? (text + "traces\\") : (text + "\\traces\\"));
						}
						registryKey2.Close();
						registryKey2.Dispose();
					}
					registryKey.Close();
					registryKey.Dispose();
				}
				catch (Exception)
				{
					traceFileFolder = null;
				}
			}
			if (string.IsNullOrWhiteSpace(traceFileFolder))
			{
				return SR.EmptyFolderName;
			}
			try
			{
				ReadOnlyCollectionBase accessRules = Directory.GetAccessControl(traceFileFolder).GetAccessRules(true, true, typeof(SecurityIdentifier));
				WindowsIdentity current = WindowsIdentity.GetCurrent();
				foreach (object obj in accessRules)
				{
					FileSystemAccessRule fileSystemAccessRule = (FileSystemAccessRule)obj;
					if ((current.Groups.Contains(fileSystemAccessRule.IdentityReference) || current.Owner == fileSystemAccessRule.IdentityReference) && (FileSystemRights.Write & fileSystemAccessRule.FileSystemRights) == FileSystemRights.Write && fileSystemAccessRule.AccessControlType == AccessControlType.Allow)
					{
						return null;
					}
				}
			}
			catch (Exception)
			{
			}
			return SR.WritePermissions(traceFileFolder);
		}

		// Token: 0x06003793 RID: 14227 RVA: 0x000BB2A4 File Offset: 0x000B94A4
		private static void ReadConfiguration(TracingConfigurationSectionHandler programmaticSection)
		{
			TraceRuntime.section = ((programmaticSection == null) ? ((TracingConfigurationSectionHandler)ConfigurationManager.GetSection("hostIntegration.tracing")) : programmaticSection);
			if (TraceRuntime.section == null)
			{
				return;
			}
			if (TraceRuntime.section.TraceOptions.WriteTraceFile)
			{
				string text = TraceRuntime.CanWriteToTraceFileFolder(TraceRuntime.section.TraceOptions.TraceFileFolder);
				if (text != null)
				{
					throw new TraceException(SR.ProblemsWithTraceFileFolder(TraceRuntime.section.TraceOptions.TraceFileFolder, text));
				}
			}
			if (string.IsNullOrEmpty(TraceRuntime.section.TraceOptions.TraceDefinitionFile))
			{
				return;
			}
			string text2 = Environment.ExpandEnvironmentVariables(TraceRuntime.section.TraceOptions.TraceDefinitionFile);
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			XmlReader xmlReader = null;
			try
			{
				xmlReader = XmlReader.Create(text2, new XmlReaderSettings
				{
					DtdProcessing = DtdProcessing.Prohibit,
					XmlResolver = null
				});
				xmlDocument.Load(xmlReader);
			}
			catch (Exception ex)
			{
				throw new TraceException(SR.CantOpenHitd(text2, ex.Message));
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
			}
			FileInfo fileInfo = new FileInfo(text2);
			TraceRuntime.watcher = new FileSystemWatcher();
			TraceRuntime.watcher.Path = fileInfo.DirectoryName;
			TraceRuntime.watcher.NotifyFilter = NotifyFilters.LastWrite;
			TraceRuntime.watcher.Filter = fileInfo.Name;
			TraceRuntime.watcher.Changed += TraceRuntime.OnConfigurationFileChanged;
			TraceRuntime.watcher.EnableRaisingEvents = true;
			if (!xmlDocument.HasChildNodes)
			{
				return;
			}
			TraceRuntime.xmlOfContainers = xmlDocument.SelectNodes("containers/container");
		}

		// Token: 0x06003794 RID: 14228 RVA: 0x000BB434 File Offset: 0x000B9634
		private static void OnConfigurationFileChanged(object source, FileSystemEventArgs e)
		{
			if (e.ChangeType == WatcherChangeTypes.Changed)
			{
				TraceRuntime.watcher.EnableRaisingEvents = false;
				ThreadPool.QueueUserWorkItem(new WaitCallback(TraceRuntime.ThreadProcToReloadConfigurationFile), e.FullPath);
			}
		}

		// Token: 0x06003795 RID: 14229 RVA: 0x000BB464 File Offset: 0x000B9664
		private static void ThreadProcToReloadConfigurationFile(object fileName)
		{
			string text = fileName as string;
			XmlReader xmlReader = null;
			List<ITraceContainer> list;
			try
			{
				XmlDocument xmlDocument = new XmlDocument();
				xmlDocument.XmlResolver = null;
				bool flag = false;
				while (!flag)
				{
					try
					{
						xmlReader = XmlReader.Create(text, new XmlReaderSettings
						{
							DtdProcessing = DtdProcessing.Prohibit,
							XmlResolver = null
						});
						xmlDocument.Load(xmlReader);
						flag = true;
					}
					catch (IOException)
					{
					}
					finally
					{
						if (xmlReader != null)
						{
							xmlReader.Close();
							xmlReader = null;
						}
						if (!flag)
						{
							Thread.Sleep(1000);
						}
					}
				}
				TraceRuntime.xmlOfContainers = xmlDocument.SelectNodes("containers/container");
				list = TraceRuntime.traceContainers;
				lock (list)
				{
					foreach (ITraceContainer traceContainer in TraceRuntime.traceContainers)
					{
						XmlNode xmlNode = TraceRuntime.FindXmlForContainer(traceContainer);
						TraceTree traceTree = new TraceTree(traceContainer, xmlNode);
						traceContainer.TraceTree = traceTree;
					}
				}
			}
			catch (Exception)
			{
			}
			finally
			{
				if (xmlReader != null)
				{
					xmlReader.Close();
				}
			}
			list = TraceRuntime.traceContainers;
			lock (list)
			{
				TraceRuntime.CheckTracing();
			}
			TraceRuntime.watcher.EnableRaisingEvents = true;
		}

		// Token: 0x06003796 RID: 14230 RVA: 0x000BB5E8 File Offset: 0x000B97E8
		private static void CheckTracing()
		{
			if (!TraceRuntime.checkedForTraceListener)
			{
				if (TraceRuntime.section != null && TraceRuntime.section.TraceOptions.WriteTraceFile)
				{
					TraceRuntime.ourTraceListener = new HisTextFileTraceListener("HisTextTraceListener");
					TraceRuntime.ourTraceListener.AllowNonHisTracingToCreateFile = TraceRuntime.section.TraceOptions.AllowNonHisTracingToCreateFile;
					TraceRuntime.ourTraceListener.AutoFlush = TraceRuntime.section.TraceOptions.AutoFlush;
					TraceRuntime.ourTraceListener.TraceFileFolder = TraceRuntime.section.TraceOptions.TraceFileFolder;
					TraceRuntime.ourTraceListener.FileNamePreamble = TraceRuntime.section.TraceOptions.FileNamePreamble;
					TraceRuntime.ourTraceListener.MaxTraceEntries = TraceRuntime.section.TraceOptions.MaxTraceEntries;
					Trace.Listeners.Add(TraceRuntime.ourTraceListener);
				}
				TraceRuntime.checkedForTraceListener = true;
			}
			bool flag = false;
			using (List<ITraceContainer>.Enumerator enumerator = TraceRuntime.traceContainers.GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.HasSomeTracingSet)
					{
						flag = true;
						break;
					}
				}
			}
			if (!flag && TraceRuntime.traceListenerOpen)
			{
				TraceRuntime.ourTraceListener.HisTracingClose();
				TraceRuntime.traceListenerOpen = false;
				return;
			}
			if (!TraceRuntime.traceListenerOpen && TraceRuntime.ourTraceListener != null)
			{
				TraceRuntime.ourTraceListener.HisTracingOpen();
				TraceRuntime.traceListenerOpen = true;
			}
			foreach (ITraceContainer traceContainer in TraceRuntime.traceContainers)
			{
				traceContainer.HasHisListener = TraceRuntime.ourTraceListener != null;
			}
		}

		// Token: 0x04001FCB RID: 8139
		private static bool readConfiguration;

		// Token: 0x04001FCC RID: 8140
		private static object lockObject = new object();

		// Token: 0x04001FCD RID: 8141
		private static List<ITraceContainer> traceContainers = new List<ITraceContainer>();

		// Token: 0x04001FCE RID: 8142
		private static HisTextFileTraceListener ourTraceListener;

		// Token: 0x04001FCF RID: 8143
		private static bool traceListenerOpen;

		// Token: 0x04001FD0 RID: 8144
		private static bool checkedForTraceListener;

		// Token: 0x04001FD1 RID: 8145
		private static XmlNodeList xmlOfContainers;

		// Token: 0x04001FD2 RID: 8146
		private static TracingConfigurationSectionHandler section;

		// Token: 0x04001FD3 RID: 8147
		private static FileSystemWatcher watcher;
	}
}
