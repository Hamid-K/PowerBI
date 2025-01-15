using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.HostIntegration.StrictResources.TracingConfiguration;

namespace Microsoft.HostIntegration.Tracing
{
	// Token: 0x02000675 RID: 1653
	public class TraceConfigurationRuntime
	{
		// Token: 0x06003799 RID: 14233 RVA: 0x000BB7A4 File Offset: 0x000B99A4
		public static TraceTree AddConfigurationFile(ITraceContainer container, string configurationFileName)
		{
			bool flag = !string.IsNullOrEmpty(container.InstanceName);
			List<string> list = new List<string>();
			XmlNode xmlNode = null;
			if (!string.IsNullOrEmpty(configurationFileName))
			{
				XmlNodeList xmlNodeList = TraceConfigurationRuntime.ReadConfiguration(configurationFileName);
				if (xmlNodeList != null)
				{
					foreach (object obj in xmlNodeList)
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
								if (flag)
								{
									if (string.Equals(xmlAttribute2.Value, container.InstanceName, StringComparison.InvariantCulture))
									{
										xmlNode = xmlNode2;
										break;
									}
								}
								else
								{
									list.Add(xmlAttribute2.Value);
								}
							}
							else
							{
								if (xmlAttribute2 != null)
								{
									throw new TraceException(SR.ContainerNodeInstanceNameAttributeIllegal);
								}
								xmlNode = xmlNode2;
								break;
							}
						}
					}
				}
			}
			if (container.SupportsInstances && !flag)
			{
				container.InstanceNamesInConfigurationFile = list;
			}
			container.IsInConfigurationFile = xmlNode != null;
			return (TraceTree)new TraceTree(container, xmlNode).Clone();
		}

		// Token: 0x0600379A RID: 14234 RVA: 0x000BB8F8 File Offset: 0x000B9AF8
		private static XmlNodeList ReadConfiguration(string configurationFileName)
		{
			XmlReader xmlReader = XmlReader.Create(configurationFileName, new XmlReaderSettings
			{
				DtdProcessing = DtdProcessing.Prohibit,
				XmlResolver = null
			});
			XmlDocument xmlDocument = new XmlDocument();
			xmlDocument.XmlResolver = null;
			try
			{
				xmlDocument.Load(xmlReader);
			}
			finally
			{
				xmlReader.Close();
			}
			if (!xmlDocument.HasChildNodes)
			{
				return null;
			}
			return xmlDocument.SelectNodes("containers/container");
		}
	}
}
