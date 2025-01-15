using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000034 RID: 52
	[Serializable]
	internal sealed class RDLSandboxingConfiguration : IRdlSandboxConfig
	{
		// Token: 0x060000F3 RID: 243 RVA: 0x000040F0 File Offset: 0x000022F0
		internal static void Parse(XmlNode node, ConfigurationPropertyBag properties)
		{
			RDLSandboxingPropertyBag rdlsandboxingPropertyBag = new RDLSandboxingPropertyBag();
			properties.Add(ConfigurationProperty.RDLSandboxing, new ConfigurationPropertyInfo
			{
				Value = rdlsandboxingPropertyBag
			});
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string name = xmlNode.Name;
				if (!(name == "MaxExpressionLength") && !(name == "MaxResourceSize") && !(name == "MaxStringResultLength") && !(name == "MaxArrayResultLength"))
				{
					if (!(name == "Types"))
					{
						if (!(name == "Members"))
						{
							if (xmlNode.NodeType != XmlNodeType.Comment)
							{
								XmlParseExceptions.ThrowInvalidFormat(xmlNode.Name);
							}
						}
						else
						{
							RDLSandboxingConfiguration.ParseMembers(xmlNode, rdlsandboxingPropertyBag);
						}
					}
					else
					{
						RDLSandboxingConfiguration.ParseTypes(xmlNode, rdlsandboxingPropertyBag);
					}
				}
				else
				{
					rdlsandboxingPropertyBag.Add(xmlNode.Name, RDLSandboxingConfiguration.ReadNodeText(xmlNode));
				}
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00004200 File Offset: 0x00002400
		private static void ParseMembers(XmlNode node, RDLSandboxingPropertyBag properties)
		{
			List<string> list = new List<string>();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string name = xmlNode.Name;
				if (name == "Deny")
				{
					list.Add(RDLSandboxingConfiguration.ReadNodeText(xmlNode));
				}
				else if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					XmlParseExceptions.ThrowInvalidFormat(xmlNode.Name);
				}
			}
			properties.Add("Members", new ConfigurationPropertyInfo
			{
				Value = list
			});
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000042AC File Offset: 0x000024AC
		internal static void ParseTypes(XmlNode node, RDLSandboxingPropertyBag properties)
		{
			List<RDLSandboxingPropertyBag> list = new List<RDLSandboxingPropertyBag>();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				string name = xmlNode.Name;
				if (name == "Allow")
				{
					list.Add(RDLSandboxingTypeInfo.Parse(xmlNode));
				}
				else if (xmlNode.NodeType != XmlNodeType.Comment)
				{
					XmlParseExceptions.ThrowInvalidFormat(xmlNode.Name);
				}
			}
			properties.Add("Types", new ConfigurationPropertyInfo
			{
				Value = list
			});
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x00004358 File Offset: 0x00002558
		internal static string ReadNodeText(XmlNode node)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (object obj in node.ChildNodes)
			{
				XmlNode xmlNode = (XmlNode)obj;
				XmlNodeType nodeType = xmlNode.NodeType;
				if (nodeType <= XmlNodeType.Comment)
				{
					if (nodeType - XmlNodeType.Text > 1)
					{
						if (nodeType != XmlNodeType.Comment)
						{
							goto IL_0050;
						}
						continue;
					}
				}
				else
				{
					if (nodeType == XmlNodeType.Whitespace)
					{
						continue;
					}
					if (nodeType != XmlNodeType.SignificantWhitespace)
					{
						goto IL_0050;
					}
				}
				stringBuilder.Append(xmlNode.Value);
				continue;
				IL_0050:
				XmlParseExceptions.ThrowInvalidFormat(xmlNode.Name);
			}
			return stringBuilder.ToString().Trim();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x000043FC File Offset: 0x000025FC
		internal static void Validate(IParameterSource paramSource, RSTrace tracer, RDLSandboxingPropertyBag properties)
		{
			tracer.Trace(TraceLevel.Info, "RDL Sandbox mode enabled");
			foreach (KeyValuePair<string, ConfigurationPropertyInfo> keyValuePair in properties)
			{
				string key = keyValuePair.Key;
				ConfigurationPropertyInfo value = keyValuePair.Value;
				if (!(key == "MaxExpressionLength"))
				{
					if (!(key == "MaxResourceSize"))
					{
						if (!(key == "MaxStringResultLength"))
						{
							if (!(key == "MaxArrayResultLength"))
							{
								if (key == "Types")
								{
									foreach (RDLSandboxingPropertyBag rdlsandboxingPropertyBag in ((List<RDLSandboxingPropertyBag>)value.Value))
									{
										RDLSandboxingTypeInfo.Validate(paramSource, tracer, rdlsandboxingPropertyBag);
									}
								}
							}
							else
							{
								value.Value = new IntParameter(paramSource, tracer, key, value.SpecifiedValue, 100, "items")
								{
									MinValue = 1,
									MaxValue = int.MaxValue
								}.Value;
							}
						}
						else
						{
							value.Value = new IntParameter(paramSource, tracer, key, value.SpecifiedValue, 1000, "characters")
							{
								MinValue = 1,
								MaxValue = int.MaxValue
							}.Value;
						}
					}
					else
					{
						value.Value = new IntParameter(paramSource, tracer, key, value.SpecifiedValue, 100, "kilobytes")
						{
							MinValue = 1,
							MaxValue = int.MaxValue
						}.Value;
					}
				}
				else
				{
					value.Value = new IntParameter(paramSource, tracer, key, value.SpecifiedValue, 1000, "characters")
					{
						MinValue = 1,
						MaxValue = int.MaxValue
					}.Value;
				}
			}
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x00004620 File Offset: 0x00002820
		internal void Load(RDLSandboxingPropertyBag properties)
		{
			foreach (KeyValuePair<string, ConfigurationPropertyInfo> keyValuePair in properties)
			{
				string key = keyValuePair.Key;
				ConfigurationPropertyInfo value = keyValuePair.Value;
				if (!(key == "MaxExpressionLength"))
				{
					if (!(key == "MaxResourceSize"))
					{
						if (!(key == "MaxStringResultLength"))
						{
							if (!(key == "MaxArrayResultLength"))
							{
								if (!(key == "Types"))
								{
									if (!(key == "Members"))
									{
										continue;
									}
								}
								else
								{
									this.m_allowedTypes = new List<IRdlSandboxTypeInfo>();
									using (List<RDLSandboxingPropertyBag>.Enumerator enumerator2 = ((List<RDLSandboxingPropertyBag>)value.Value).GetEnumerator())
									{
										while (enumerator2.MoveNext())
										{
											RDLSandboxingPropertyBag rdlsandboxingPropertyBag = enumerator2.Current;
											RDLSandboxingTypeInfo rdlsandboxingTypeInfo = new RDLSandboxingTypeInfo();
											rdlsandboxingTypeInfo.Load(rdlsandboxingPropertyBag);
											this.m_allowedTypes.Add(rdlsandboxingTypeInfo);
										}
										continue;
									}
								}
								this.m_deniedMembers = (List<string>)value.Value;
							}
							else
							{
								this.m_maxArrayResultLength = (int)value.Value;
							}
						}
						else
						{
							this.m_maxStringResultLength = (int)value.Value;
						}
					}
					else
					{
						this.m_maxResourceSizeKB = (int)value.Value;
					}
				}
				else
				{
					this.m_maxExpressionLength = (int)value.Value;
				}
			}
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060000F9 RID: 249 RVA: 0x000047BC File Offset: 0x000029BC
		public int MaxExpressionLength
		{
			get
			{
				return this.m_maxExpressionLength;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060000FA RID: 250 RVA: 0x000047C4 File Offset: 0x000029C4
		public int MaxResourceSizeKB
		{
			get
			{
				return this.m_maxResourceSizeKB;
			}
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060000FB RID: 251 RVA: 0x000047CC File Offset: 0x000029CC
		public int MaxStringResultLength
		{
			get
			{
				return this.m_maxStringResultLength;
			}
		}

		// Token: 0x1700006D RID: 109
		// (get) Token: 0x060000FC RID: 252 RVA: 0x000047D4 File Offset: 0x000029D4
		public int MaxArrayResultLength
		{
			get
			{
				return this.m_maxArrayResultLength;
			}
		}

		// Token: 0x1700006E RID: 110
		// (get) Token: 0x060000FD RID: 253 RVA: 0x000047DC File Offset: 0x000029DC
		public IList<string> DeniedMembers
		{
			get
			{
				return this.m_deniedMembers;
			}
		}

		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060000FE RID: 254 RVA: 0x000047E4 File Offset: 0x000029E4
		public IList<IRdlSandboxTypeInfo> AllowedTypes
		{
			get
			{
				return this.m_allowedTypes;
			}
		}

		// Token: 0x0400012E RID: 302
		private int m_maxExpressionLength = 1000;

		// Token: 0x0400012F RID: 303
		private int m_maxResourceSizeKB = 100;

		// Token: 0x04000130 RID: 304
		private int m_maxStringResultLength = 1000;

		// Token: 0x04000131 RID: 305
		private int m_maxArrayResultLength = 100;

		// Token: 0x04000132 RID: 306
		private List<string> m_deniedMembers;

		// Token: 0x04000133 RID: 307
		private List<IRdlSandboxTypeInfo> m_allowedTypes;
	}
}
