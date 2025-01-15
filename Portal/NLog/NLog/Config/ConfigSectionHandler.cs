using System;
using System.Configuration;
using System.Xml;
using NLog.Common;
using NLog.Internal.Fakeables;

namespace NLog.Config
{
	// Token: 0x0200017E RID: 382
	public sealed class ConfigSectionHandler : IConfigurationSectionHandler
	{
		// Token: 0x06001183 RID: 4483 RVA: 0x0002D5F0 File Offset: 0x0002B7F0
		private object Create(XmlNode section, IAppDomain appDomain)
		{
			object obj;
			try
			{
				string configurationFile = appDomain.ConfigurationFile;
				obj = new XmlLoggingConfiguration((XmlElement)section, configurationFile);
			}
			catch (Exception ex)
			{
				InternalLogger.Error(ex, "ConfigSectionHandler error.");
				throw;
			}
			return obj;
		}

		// Token: 0x06001184 RID: 4484 RVA: 0x0002D630 File Offset: 0x0002B830
		object IConfigurationSectionHandler.Create(object parent, object configContext, XmlNode section)
		{
			return this.Create(section, LogFactory.CurrentAppDomain);
		}
	}
}
