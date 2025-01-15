using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200003D RID: 61
	internal class RSConfigurationFileWatcherManager : RSConfigurationFileManager, IDisposable
	{
		// Token: 0x060001FD RID: 509 RVA: 0x0000A1E4 File Offset: 0x000083E4
		public RSConfigurationFileWatcherManager(string configFileName, string configLocation)
			: base(configFileName, configLocation)
		{
			if (this.m_configLocation != null && this.m_configFileName != null && Directory.Exists(this.m_configLocation))
			{
				this.m_watcher = new FileSystemWatcher(this.m_configLocation);
				this.m_watcher.EnableRaisingEvents = true;
				this.m_watcher.Changed += this.ConfigFileChanged;
				this.m_watcher.Created += this.ConfigFileChanged;
				this.m_watcher.Renamed += new RenamedEventHandler(this.ConfigFileChanged);
			}
		}

		// Token: 0x17000123 RID: 291
		// (get) Token: 0x060001FE RID: 510 RVA: 0x0000A294 File Offset: 0x00008494
		// (set) Token: 0x060001FF RID: 511 RVA: 0x0000A2A1 File Offset: 0x000084A1
		public override bool EnableRaisingEvents
		{
			get
			{
				return this.m_watcher.EnableRaisingEvents;
			}
			set
			{
				this.m_watcher.EnableRaisingEvents = value;
			}
		}

		// Token: 0x06000200 RID: 512 RVA: 0x0000A2B0 File Offset: 0x000084B0
		private void ConfigFileChanged(object sender, FileSystemEventArgs args)
		{
			try
			{
				if ((args.ChangeType & (WatcherChangeTypes.Created | WatcherChangeTypes.Changed | WatcherChangeTypes.Renamed)) != (WatcherChangeTypes)0 && StringComparer.OrdinalIgnoreCase.Compare(args.Name, this.m_configFileName) == 0)
				{
					if (!(this.m_lastChangedTime != DateTime.MinValue) || !(DateTime.Now - this.m_lastChangedTime < this.m_minTime))
					{
						this.m_lastChangedTime = DateTime.Now;
						if (RSConfigurationFileManager.m_Tracer.TraceInfo)
						{
							RSConfigurationFileManager.m_Tracer.Trace(TraceLevel.Info, "Handling File Changes");
						}
						object configChangeLockObject = this.m_configChangeLockObject;
						lock (configChangeLockObject)
						{
							if (Globals.CurrentApplication != RunningApplication.ReportServerWebApp)
							{
								NativeXEMethods.AlterXESessions();
							}
							ConfigurationPropertyBag configurationPropertyBag = this.LoadConfiguration();
							if (!this.PropertiesAreEqual(configurationPropertyBag, this.m_properties))
							{
								this.m_properties = configurationPropertyBag;
								RSEventLog.Current.WriteInformation(Event.ConfigFileChanged, new object[] { this.m_configFileName });
								this.ChangeConfiguration(configurationPropertyBag, true, this);
							}
							else if (RSConfigurationFileManager.m_Tracer.TraceInfo)
							{
								RSConfigurationFileManager.m_Tracer.Trace(TraceLevel.Info, "Properties are unchanged from previous settings.");
							}
						}
					}
				}
			}
			catch (Exception ex)
			{
				if (RSConfigurationFileManager.m_Tracer.TraceError)
				{
					RSConfigurationFileManager.m_Tracer.Trace(TraceLevel.Error, "Error occured with a config file change. New config file will not be used. Error: {0}", new object[] { ex.ToString() });
				}
			}
		}

		// Token: 0x06000201 RID: 513 RVA: 0x0000A430 File Offset: 0x00008630
		private bool PropertiesAreEqual(ConfigurationPropertyBag properties1, ConfigurationPropertyBag properties2)
		{
			if (properties1.Count != properties2.Count)
			{
				return false;
			}
			foreach (KeyValuePair<ConfigurationProperty, ConfigurationPropertyInfo> keyValuePair in properties1)
			{
				ConfigurationPropertyInfo configurationPropertyInfo;
				if (!properties2.TryGetValue(keyValuePair.Key, out configurationPropertyInfo))
				{
					return false;
				}
				object value = keyValuePair.Value.Value;
				object value2 = configurationPropertyInfo.Value;
				if (value == null)
				{
					if (value2 != null)
					{
						return false;
					}
				}
				else if (value is string || !value.GetType().IsClass)
				{
					if (!value.Equals(value2))
					{
						return false;
					}
				}
				else if (!this.ObjectsAreEqual(value, value2))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x0000A4F8 File Offset: 0x000086F8
		private bool ObjectsAreEqual(object value1, object value2)
		{
			MemoryStream memoryStream = new MemoryStream();
			MemoryStream memoryStream2 = new MemoryStream();
			BinaryFormatter binaryFormatter = new BinaryFormatter();
			binaryFormatter.Serialize(memoryStream, value1);
			binaryFormatter.Serialize(memoryStream2, value2);
			if (memoryStream.Length != memoryStream2.Length)
			{
				return false;
			}
			memoryStream.Seek(0L, SeekOrigin.Begin);
			memoryStream2.Seek(0L, SeekOrigin.Begin);
			long num = memoryStream.Length;
			do
			{
				long num2 = num;
				num = num2 - 1L;
				if (num2 <= 0L)
				{
					return true;
				}
			}
			while (memoryStream.ReadByte() == memoryStream2.ReadByte());
			return false;
		}

		// Token: 0x06000203 RID: 515 RVA: 0x0000A56D File Offset: 0x0000876D
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000204 RID: 516 RVA: 0x0000A576 File Offset: 0x00008776
		private void Dispose(bool disposing)
		{
			if (disposing && this.m_watcher != null)
			{
				this.m_watcher.Dispose();
				this.m_watcher = null;
			}
		}

		// Token: 0x040001EF RID: 495
		private FileSystemWatcher m_watcher;

		// Token: 0x040001F0 RID: 496
		private DateTime m_lastChangedTime = DateTime.MinValue;

		// Token: 0x040001F1 RID: 497
		private TimeSpan m_minTime = new TimeSpan(5000000L);
	}
}
