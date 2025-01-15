using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Threading;
using System.Xml.Schema;
using System.Xml.Serialization;

namespace Microsoft.Cis.Eventing
{
	// Token: 0x02000486 RID: 1158
	[DebuggerStepThrough]
	[XmlRoot(Namespace = "", IsNullable = false)]
	[GeneratedCode("xsd", "2.0.50727.312")]
	[DesignerCategory("code")]
	[XmlType(AnonymousType = true)]
	[Serializable]
	public class EventPriorityMappings
	{
		// Token: 0x06002830 RID: 10288 RVA: 0x00079240 File Offset: 0x00077440
		static EventPriorityMappings()
		{
			EventPriorityMappings.Debug("Current working directory: " + EventPriorityMappings.CWD);
			EventPriorityMappings.ConfDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
			EventPriorityMappings.Debug("Assembly directory: " + EventPriorityMappings.ConfDir);
			EventPriorityMappings.ConfPath = Path.Combine(EventPriorityMappings.ConfDir, EventPriorityMappings.ConfFile);
			if (!File.Exists(EventPriorityMappings.ConfPath) && File.Exists(Path.Combine(EventPriorityMappings.CWD, EventPriorityMappings.ConfFile)))
			{
				EventPriorityMappings.ConfDir = EventPriorityMappings.CWD;
				EventPriorityMappings.ConfPath = Path.Combine(EventPriorityMappings.ConfDir, EventPriorityMappings.ConfFile);
			}
			EventPriorityMappings.Load(EventPriorityMappings.ConfPath);
			EventPriorityMappings.StartConfigChangeMonitor();
		}

		// Token: 0x170007F3 RID: 2035
		// (get) Token: 0x06002831 RID: 10289 RVA: 0x00079343 File Offset: 0x00077543
		// (set) Token: 0x06002832 RID: 10290 RVA: 0x0007934B File Offset: 0x0007754B
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string CriticalIdList
		{
			get
			{
				return this.criticalIdList;
			}
			set
			{
				this.criticalIdList = value;
			}
		}

		// Token: 0x170007F4 RID: 2036
		// (get) Token: 0x06002833 RID: 10291 RVA: 0x00079354 File Offset: 0x00077554
		// (set) Token: 0x06002834 RID: 10292 RVA: 0x0007935C File Offset: 0x0007755C
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string ErrorIdList
		{
			get
			{
				return this.errorIdList;
			}
			set
			{
				this.errorIdList = value;
			}
		}

		// Token: 0x170007F5 RID: 2037
		// (get) Token: 0x06002835 RID: 10293 RVA: 0x00079365 File Offset: 0x00077565
		// (set) Token: 0x06002836 RID: 10294 RVA: 0x0007936D File Offset: 0x0007756D
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string WarningIdList
		{
			get
			{
				return this.warningIdList;
			}
			set
			{
				this.warningIdList = value;
			}
		}

		// Token: 0x170007F6 RID: 2038
		// (get) Token: 0x06002837 RID: 10295 RVA: 0x00079376 File Offset: 0x00077576
		// (set) Token: 0x06002838 RID: 10296 RVA: 0x0007937E File Offset: 0x0007757E
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string InformationIdList
		{
			get
			{
				return this.informationIdList;
			}
			set
			{
				this.informationIdList = value;
			}
		}

		// Token: 0x170007F7 RID: 2039
		// (get) Token: 0x06002839 RID: 10297 RVA: 0x00079387 File Offset: 0x00077587
		// (set) Token: 0x0600283A RID: 10298 RVA: 0x0007938F File Offset: 0x0007758F
		[XmlElement(Form = XmlSchemaForm.Unqualified)]
		public string VerboseIdList
		{
			get
			{
				return this.verboseIdList;
			}
			set
			{
				this.verboseIdList = value;
			}
		}

		// Token: 0x170007F8 RID: 2040
		// (get) Token: 0x0600283B RID: 10299 RVA: 0x00079398 File Offset: 0x00077598
		public static string AbsConfPath
		{
			get
			{
				return EventPriorityMappings.ConfPath;
			}
		}

		// Token: 0x0600283C RID: 10300 RVA: 0x000793A0 File Offset: 0x000775A0
		public static TraceEventType GetEventPriority(int eventId, TraceEventType currentPriority)
		{
			TraceEventType traceEventType;
			if (EventPriorityMappings.mappingsInstance != null && EventPriorityMappings.mappingsInstance.idMap.TryGetValue(eventId, out traceEventType))
			{
				EventPriorityMappings.Debug(string.Concat(new object[] { "Event ", eventId, " priority has been changed to ", traceEventType }));
				return traceEventType;
			}
			return currentPriority;
		}

		// Token: 0x0600283D RID: 10301 RVA: 0x00079400 File Offset: 0x00077600
		public static EventPriorityMappings LoadData(string fileName)
		{
			EventPriorityMappings eventPriorityMappings2;
			lock (EventPriorityMappings.lockObject)
			{
				EventPriorityMappings.Debug("Reloading " + fileName);
				EventPriorityMappings eventPriorityMappings = null;
				try
				{
					EventPriorityMappings.CreateConfigFileIfNoneExists(fileName);
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(EventPriorityMappings));
					using (StreamReader streamReader = new StreamReader(fileName))
					{
						eventPriorityMappings = (EventPriorityMappings)xmlSerializer.Deserialize(streamReader);
						streamReader.Close();
					}
					if (eventPriorityMappings != null)
					{
						eventPriorityMappings.init();
						EventPriorityMappings.Debug("Events mapping table has been reinitialized.");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Failed to load event map table from file {0} : {1}", fileName, ex.ToString());
				}
				eventPriorityMappings2 = eventPriorityMappings;
			}
			return eventPriorityMappings2;
		}

		// Token: 0x0600283E RID: 10302 RVA: 0x000794D4 File Offset: 0x000776D4
		public static void Save(string fileName, EventPriorityMappings map)
		{
			lock (EventPriorityMappings.lockObject)
			{
				EventPriorityMappings.Debug("Saving configuration file: " + fileName);
				XmlSerializer xmlSerializer = new XmlSerializer(typeof(EventPriorityMappings));
				using (StreamWriter streamWriter = new StreamWriter(fileName, false))
				{
					xmlSerializer.Serialize(streamWriter, map);
					streamWriter.Flush();
					streamWriter.Close();
					EventPriorityMappings.Debug("Configuration file saved!");
				}
			}
		}

		// Token: 0x0600283F RID: 10303 RVA: 0x0007956C File Offset: 0x0007776C
		private void init()
		{
			string[] array = new string[] { this.criticalIdList, this.errorIdList, this.warningIdList, this.informationIdList, this.verboseIdList };
			EventPriorityMappings.Debug("Building event priority map table.");
			for (int i = 0; i < array.Length; i++)
			{
				string[] array2 = array[i].Split(new char[] { ',', ' ', '\t', ';' }, StringSplitOptions.RemoveEmptyEntries);
				if (array2 != null && array2.Length != 0)
				{
					for (int j = 0; j < array2.Length; j++)
					{
						EventPriorityMappings.Debug("Adding event " + array2[j]);
						int num;
						if (int.TryParse(array2[j], out num))
						{
							if (!this.idMap.ContainsKey(num))
							{
								this.idMap.Add(num, EventPriorityMappings.eventTypes[i]);
							}
						}
						else
						{
							Console.WriteLine("Failed to parse event {0}", array2[j]);
						}
					}
				}
			}
		}

		// Token: 0x06002840 RID: 10304 RVA: 0x0007965C File Offset: 0x0007785C
		private static void StopConfigMonitor()
		{
			if (EventPriorityMappings.watcher != null)
			{
				EventPriorityMappings.Debug("Stopping previous instance of config change monitor...");
				EventPriorityMappings.watcher.Changed -= EventPriorityMappings.OnChangedCB;
				EventPriorityMappings.watcher.EnableRaisingEvents = false;
				EventPriorityMappings.watcher.Dispose();
				EventPriorityMappings.Debug("Previous instance of config change monitor stopped.");
			}
		}

		// Token: 0x06002841 RID: 10305 RVA: 0x000796A8 File Offset: 0x000778A8
		private static void StartConfigChangeMonitor()
		{
			EventPriorityMappings.Debug(string.Concat(new string[]
			{
				"Starting config change monitor (ConfDir=",
				EventPriorityMappings.ConfDir,
				"; ConfFile=",
				EventPriorityMappings.ConfFile,
				") ..."
			}));
			EventPriorityMappings.StopConfigMonitor();
			EventPriorityMappings.watcher = new FileSystemWatcher();
			EventPriorityMappings.watcher.Path = EventPriorityMappings.ConfDir;
			EventPriorityMappings.watcher.NotifyFilter = NotifyFilters.LastWrite;
			EventPriorityMappings.watcher.Filter = EventPriorityMappings.ConfFile;
			EventPriorityMappings.watcher.Changed += EventPriorityMappings.OnChangedCB;
			EventPriorityMappings.watcher.EnableRaisingEvents = true;
			EventPriorityMappings.Debug("Config change monitor started.");
		}

		// Token: 0x06002842 RID: 10306 RVA: 0x0007974C File Offset: 0x0007794C
		private static void OnChanged(object source, FileSystemEventArgs e)
		{
			EventPriorityMappings.Debug("Event mappings file has changed.");
			EventPriorityMappings.StopConfigMonitor();
			EventPriorityMappings.Load(e.FullPath);
			EventPriorityMappings.StartConfigChangeMonitor();
		}

		// Token: 0x06002843 RID: 10307 RVA: 0x00079770 File Offset: 0x00077970
		private static void Load(string fileName)
		{
			lock (EventPriorityMappings.lockObject)
			{
				EventPriorityMappings.Debug("Reloading " + fileName);
				try
				{
					EventPriorityMappings eventPriorityMappings = null;
					EventPriorityMappings.CreateConfigFileIfNoneExists(fileName);
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(EventPriorityMappings));
					using (StreamReader streamReader = new StreamReader(fileName))
					{
						eventPriorityMappings = (EventPriorityMappings)xmlSerializer.Deserialize(streamReader);
						streamReader.Close();
					}
					if (eventPriorityMappings != null)
					{
						eventPriorityMappings.init();
						Interlocked.Exchange<EventPriorityMappings>(ref EventPriorityMappings.mappingsInstance, eventPriorityMappings);
						EventPriorityMappings.Debug("Events mapping table has been reinitialized.");
					}
				}
				catch (Exception ex)
				{
					Console.WriteLine("Failed to load event map table from file {0} : {1}", fileName, ex.ToString());
				}
			}
		}

		// Token: 0x06002844 RID: 10308 RVA: 0x00079848 File Offset: 0x00077A48
		private static void CreateConfigFileIfNoneExists(string fileName)
		{
			if (File.Exists(fileName))
			{
				EventPriorityMappings.Debug(fileName + " already exists");
				return;
			}
			EventPriorityMappings.Save(fileName, new EventPriorityMappings
			{
				CriticalIdList = " ",
				ErrorIdList = " ",
				WarningIdList = " ",
				InformationIdList = " ",
				VerboseIdList = " "
			});
		}

		// Token: 0x06002845 RID: 10309 RVA: 0x000036A9 File Offset: 0x000018A9
		private static void Debug(string msg)
		{
		}

		// Token: 0x0400179C RID: 6044
		private string criticalIdList;

		// Token: 0x0400179D RID: 6045
		private string errorIdList;

		// Token: 0x0400179E RID: 6046
		private string warningIdList;

		// Token: 0x0400179F RID: 6047
		private string informationIdList;

		// Token: 0x040017A0 RID: 6048
		private string verboseIdList;

		// Token: 0x040017A1 RID: 6049
		private readonly Dictionary<int, TraceEventType> idMap = new Dictionary<int, TraceEventType>();

		// Token: 0x040017A2 RID: 6050
		private static readonly string ConfDir;

		// Token: 0x040017A3 RID: 6051
		private static readonly string ConfPath;

		// Token: 0x040017A4 RID: 6052
		private static readonly string ConfFile = "EventsMappings.xml";

		// Token: 0x040017A5 RID: 6053
		private static readonly string CWD = Environment.CurrentDirectory;

		// Token: 0x040017A6 RID: 6054
		private static EventPriorityMappings mappingsInstance = null;

		// Token: 0x040017A7 RID: 6055
		private static FileSystemWatcher watcher;

		// Token: 0x040017A8 RID: 6056
		private static FileSystemEventHandler OnChangedCB = new FileSystemEventHandler(EventPriorityMappings.OnChanged);

		// Token: 0x040017A9 RID: 6057
		private static object lockObject = new object();

		// Token: 0x040017AA RID: 6058
		private static readonly TraceEventType[] eventTypes = new TraceEventType[]
		{
			TraceEventType.Critical,
			TraceEventType.Error,
			TraceEventType.Warning,
			TraceEventType.Information,
			TraceEventType.Verbose
		};
	}
}
