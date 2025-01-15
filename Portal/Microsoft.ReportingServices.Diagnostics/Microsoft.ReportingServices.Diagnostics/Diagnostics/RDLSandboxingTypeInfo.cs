using System;
using System.Collections.Generic;
using System.Xml;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x02000037 RID: 55
	[Serializable]
	internal sealed class RDLSandboxingTypeInfo : IRdlSandboxTypeInfo
	{
		// Token: 0x06000102 RID: 258 RVA: 0x00002E32 File Offset: 0x00001032
		internal RDLSandboxingTypeInfo()
		{
		}

		// Token: 0x06000103 RID: 259 RVA: 0x00004820 File Offset: 0x00002A20
		internal static RDLSandboxingPropertyBag Parse(XmlNode node)
		{
			RDLSandboxingPropertyBag rdlsandboxingPropertyBag = new RDLSandboxingPropertyBag();
			XmlAttribute xmlAttribute = node.Attributes["Namespace"];
			if (xmlAttribute == null || string.IsNullOrEmpty(xmlAttribute.Value))
			{
				XmlParseExceptions.ThrowElementMissing("Namespace");
			}
			rdlsandboxingPropertyBag.Add("Namespace", xmlAttribute.Value);
			XmlAttribute xmlAttribute2 = node.Attributes["AllowNew"];
			if (xmlAttribute2 != null)
			{
				rdlsandboxingPropertyBag.Add("AllowNew", xmlAttribute2.Value);
			}
			rdlsandboxingPropertyBag.Add("Name", RDLSandboxingConfiguration.ReadNodeText(node));
			return rdlsandboxingPropertyBag;
		}

		// Token: 0x06000104 RID: 260 RVA: 0x000048A8 File Offset: 0x00002AA8
		internal static void Validate(IParameterSource paramSource, RSTrace tracer, RDLSandboxingPropertyBag properties)
		{
			foreach (KeyValuePair<string, ConfigurationPropertyInfo> keyValuePair in properties)
			{
				string key = keyValuePair.Key;
				ConfigurationPropertyInfo value = keyValuePair.Value;
				if (!(key == "AllowNew"))
				{
					if (!(key == "Namespace"))
					{
						if (key == "Name")
						{
							string specifiedValue = value.SpecifiedValue;
							if (string.IsNullOrEmpty(specifiedValue))
							{
								XmlParseExceptions.ThrowInvalidFormat("Allow");
							}
							value.Value = specifiedValue;
						}
					}
					else
					{
						string specifiedValue2 = value.SpecifiedValue;
						if (string.IsNullOrEmpty(specifiedValue2))
						{
							XmlParseExceptions.ThrowInvalidFormat("Allow");
						}
						value.Value = specifiedValue2;
					}
				}
				else
				{
					value.Value = new BooleanParameter(paramSource, tracer, key, value.SpecifiedValue, false, "")
					{
						TraceSuccess = false
					}.Value;
				}
			}
		}

		// Token: 0x06000105 RID: 261 RVA: 0x000049A8 File Offset: 0x00002BA8
		internal void Load(RDLSandboxingPropertyBag properties)
		{
			foreach (KeyValuePair<string, ConfigurationPropertyInfo> keyValuePair in properties)
			{
				string key = keyValuePair.Key;
				ConfigurationPropertyInfo value = keyValuePair.Value;
				if (!(key == "Namespace"))
				{
					if (!(key == "AllowNew"))
					{
						if (key == "Name")
						{
							this.m_name = (string)value.Value;
						}
					}
					else
					{
						this.m_allowNew = (bool)value.Value;
					}
				}
				else
				{
					this.m_namespace = (string)value.Value;
				}
			}
		}

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x06000106 RID: 262 RVA: 0x00004A64 File Offset: 0x00002C64
		public string Namespace
		{
			get
			{
				return this.m_namespace;
			}
		}

		// Token: 0x17000071 RID: 113
		// (get) Token: 0x06000107 RID: 263 RVA: 0x00004A6C File Offset: 0x00002C6C
		public bool AllowNew
		{
			get
			{
				return this.m_allowNew;
			}
		}

		// Token: 0x17000072 RID: 114
		// (get) Token: 0x06000108 RID: 264 RVA: 0x00004A74 File Offset: 0x00002C74
		public string Name
		{
			get
			{
				return this.m_name;
			}
		}

		// Token: 0x04000144 RID: 324
		private string m_namespace;

		// Token: 0x04000145 RID: 325
		private bool m_allowNew;

		// Token: 0x04000146 RID: 326
		private string m_name;
	}
}
