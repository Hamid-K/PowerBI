using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200002C RID: 44
	[Serializable]
	internal sealed class MapTileServerConfiguration : IMapTileServerConfiguration
	{
		// Token: 0x060000AA RID: 170 RVA: 0x0000350C File Offset: 0x0000170C
		internal static void Parse(XmlNode node, ConfigurationPropertyBag properties)
		{
			ConfigurationPropertyInfo configurationPropertyInfo = new ConfigurationPropertyInfo();
			MapTileServerConfigurationPropertyBag mapTileServerConfigurationPropertyBag = new MapTileServerConfigurationPropertyBag();
			configurationPropertyInfo.Value = mapTileServerConfigurationPropertyBag;
			properties.Add(ConfigurationProperty.MapTileServerConfiguration, configurationPropertyInfo);
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string name = xmlNode.Name;
				if (name == "MaxConnections" || name == "Timeout" || name == "AppID" || name == "CacheLevel")
				{
					mapTileServerConfigurationPropertyBag.Add(xmlNode.Name, xmlNode.InnerText.Trim());
				}
				else if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					XmlParseExceptions.ThrowInvalidFormat(xmlNode.Name);
				}
			}
		}

		// Token: 0x060000AB RID: 171 RVA: 0x000035F0 File Offset: 0x000017F0
		internal static void Validate(IParameterSource paramSource, RSTrace tracer, MapTileServerConfigurationPropertyBag properties)
		{
			foreach (KeyValuePair<string, ConfigurationPropertyInfo> keyValuePair in properties)
			{
				string key = keyValuePair.Key;
				ConfigurationPropertyInfo value = keyValuePair.Value;
				if (!(key == "MaxConnections"))
				{
					if (!(key == "Timeout"))
					{
						if (!(key == "AppID"))
						{
							if (key == "CacheLevel")
							{
								EnumParameter<MapTileCacheLevel> enumParameter = new EnumParameter<MapTileCacheLevel>(paramSource, tracer, key, value.SpecifiedValue, MapTileCacheLevel.Default, "");
								value.Value = enumParameter.Value;
							}
						}
						else
						{
							StringParameter stringParameter = new StringParameter(paramSource, tracer, key, value.SpecifiedValue, "(Default)", "");
							value.Value = stringParameter.Value;
						}
					}
					else
					{
						value.Value = new IntParameter(paramSource, tracer, key, value.SpecifiedValue, 10, "second(s)")
						{
							MinValue = 1,
							MaxValue = int.MaxValue
						}.Value;
					}
				}
				else
				{
					value.Value = new IntParameter(paramSource, tracer, key, value.SpecifiedValue, 2, "")
					{
						MinValue = 1,
						MaxValue = int.MaxValue
					}.Value;
				}
			}
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000376C File Offset: 0x0000196C
		internal void Load(MapTileServerConfigurationPropertyBag properties)
		{
			foreach (KeyValuePair<string, ConfigurationPropertyInfo> keyValuePair in properties)
			{
				string key = keyValuePair.Key;
				ConfigurationPropertyInfo value = keyValuePair.Value;
				if (!(key == "MaxConnections"))
				{
					if (!(key == "Timeout"))
					{
						if (!(key == "AppID"))
						{
							if (key == "CacheLevel")
							{
								this.m_cacheLevel = (MapTileCacheLevel)value.Value;
							}
						}
						else
						{
							this.m_appID = (string)value.Value;
						}
					}
					else
					{
						this.m_timeout = (int)value.Value;
					}
				}
				else
				{
					this.m_maxConnections = (int)value.Value;
				}
			}
		}

		// Token: 0x1700004C RID: 76
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000384C File Offset: 0x00001A4C
		public int MaxConnections
		{
			get
			{
				return this.m_maxConnections;
			}
		}

		// Token: 0x1700004D RID: 77
		// (get) Token: 0x060000AE RID: 174 RVA: 0x00003854 File Offset: 0x00001A54
		public int Timeout
		{
			get
			{
				return this.m_timeout;
			}
		}

		// Token: 0x1700004E RID: 78
		// (get) Token: 0x060000AF RID: 175 RVA: 0x0000385C File Offset: 0x00001A5C
		public string AppID
		{
			get
			{
				return this.m_appID;
			}
		}

		// Token: 0x1700004F RID: 79
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x00003864 File Offset: 0x00001A64
		public bool Enabled
		{
			get
			{
				return this.m_Enabled;
			}
		}

		// Token: 0x17000050 RID: 80
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x0000386C File Offset: 0x00001A6C
		public MapTileCacheLevel CacheLevel
		{
			get
			{
				return this.m_cacheLevel;
			}
		}

		// Token: 0x040000F5 RID: 245
		private int m_maxConnections = 2;

		// Token: 0x040000F6 RID: 246
		private int m_timeout = 10;

		// Token: 0x040000F7 RID: 247
		private string m_appID = "(Default)";

		// Token: 0x040000F8 RID: 248
		private bool m_Enabled = true;

		// Token: 0x040000F9 RID: 249
		private MapTileCacheLevel m_cacheLevel;
	}
}
