using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Cloud.Platform.Configuration;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.ConfigurationManagement
{
	// Token: 0x02000403 RID: 1027
	public abstract class FileBasedConfigurationProvider : ConfigurationProviderBase
	{
		// Token: 0x06001F68 RID: 8040 RVA: 0x00075C80 File Offset: 0x00073E80
		protected Dictionary<Type, IConfigurationClass> GetConfiguration(string configurationFilesDirectory, Dictionary<string, string> configurationOverrides = null)
		{
			Dictionary<Type, IConfigurationClass> dictionary2;
			using (base.Owner.ConfigurationManagerHost.ActivityFactory.CreateSyncActivity(SingletonActivityType<CcsConfigurationChangeNotificationActivity>.Instance))
			{
				TraceSourceBase<ConfigurationTrace>.Tracer.Trace(TraceVerbosity.Info, "Ccs OnConfigurationChange callback called");
				TraceSourceBase<ConfigurationTrace>.Tracer.Trace(TraceVerbosity.Info, "Configuration path is {0}", new object[] { configurationFilesDirectory });
				string[] files = Directory.GetFiles(configurationFilesDirectory, ConfigurationConstants.ConfigurationClassSuffixFormat, SearchOption.TopDirectoryOnly);
				Dictionary<Type, IConfigurationClass> dictionary = new Dictionary<Type, IConfigurationClass>();
				foreach (string text in files)
				{
					TraceSourceBase<ConfigurationTrace>.Tracer.TraceInformation("Reading configuration file {0}", new object[] { text });
					Pair<Type, IConfigurationClass> pair = ConfigurationManagerUtilities.ReadXml(text);
					dictionary.Add(pair.First, pair.Second);
				}
				if (configurationOverrides != null)
				{
					foreach (KeyValuePair<string, string> keyValuePair in configurationOverrides)
					{
						TraceSourceBase<ConfigurationTrace>.Tracer.TraceInformation("Reading config override for {0}", new object[] { keyValuePair.Key });
						Pair<Type, IConfigurationClass> pair2 = ConfigurationManagerUtilities.ReadXmlFromContent(keyValuePair.Value);
						if (!string.Equals(pair2.First.Name, keyValuePair.Key, StringComparison.OrdinalIgnoreCase))
						{
							throw new CcsConfigurationChangeNotificationException(string.Format("Configuration override change differs between type {0} and key {1}", pair2.First.Name, keyValuePair.Key));
						}
						dictionary[pair2.First] = pair2.Second;
					}
				}
				dictionary2 = dictionary;
			}
			return dictionary2;
		}

		// Token: 0x06001F69 RID: 8041 RVA: 0x00075E34 File Offset: 0x00074034
		protected void Update(string configurationFilesDirectory, Dictionary<string, string> configurationOverrides = null)
		{
			Exception ex = TopLevelHandler.Run(this, TopLevelHandlerOption.SwallowNonfatal, delegate
			{
				this.Owner.UpdateConfiguration(this, this.GetConfiguration(configurationFilesDirectory, configurationOverrides), NotificationOptions.Async);
			});
			if (ex != null)
			{
				throw new CcsConfigurationChangeNotificationException(null, ex);
			}
		}
	}
}
