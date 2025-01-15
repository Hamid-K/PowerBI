using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000019 RID: 25
	public sealed class GetDomainSettingsResponse : AutodiscoverResponse
	{
		// Token: 0x0600011B RID: 283 RVA: 0x00006722 File Offset: 0x00005722
		public GetDomainSettingsResponse()
		{
			this.domain = string.Empty;
			this.settings = new Dictionary<DomainSettingName, object>();
			this.domainSettingErrors = new Collection<DomainSettingError>();
		}

		// Token: 0x17000048 RID: 72
		// (get) Token: 0x0600011C RID: 284 RVA: 0x0000674B File Offset: 0x0000574B
		// (set) Token: 0x0600011D RID: 285 RVA: 0x00006753 File Offset: 0x00005753
		public string Domain
		{
			get
			{
				return this.domain;
			}
			internal set
			{
				this.domain = value;
			}
		}

		// Token: 0x17000049 RID: 73
		// (get) Token: 0x0600011E RID: 286 RVA: 0x0000675C File Offset: 0x0000575C
		public string RedirectTarget
		{
			get
			{
				return this.redirectTarget;
			}
		}

		// Token: 0x1700004A RID: 74
		// (get) Token: 0x0600011F RID: 287 RVA: 0x00006764 File Offset: 0x00005764
		public IDictionary<DomainSettingName, object> Settings
		{
			get
			{
				return this.settings;
			}
		}

		// Token: 0x1700004B RID: 75
		// (get) Token: 0x06000120 RID: 288 RVA: 0x0000676C File Offset: 0x0000576C
		public Collection<DomainSettingError> DomainSettingErrors
		{
			get
			{
				return this.domainSettingErrors;
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x00006774 File Offset: 0x00005774
		internal override void LoadFromXml(EwsXmlReader reader, string endElementName)
		{
			do
			{
				reader.Read();
				if (reader.NodeType == 1)
				{
					string localName;
					if ((localName = reader.LocalName) != null)
					{
						if (localName == "RedirectTarget")
						{
							this.redirectTarget = reader.ReadElementValue();
							goto IL_006A;
						}
						if (localName == "DomainSettingErrors")
						{
							this.LoadDomainSettingErrorsFromXml(reader);
							goto IL_006A;
						}
						if (localName == "DomainSettings")
						{
							this.LoadDomainSettingsFromXml(reader);
							goto IL_006A;
						}
					}
					base.LoadFromXml(reader, endElementName);
				}
				IL_006A:;
			}
			while (!reader.IsEndElement(XmlNamespace.Autodiscover, endElementName));
		}

		// Token: 0x06000122 RID: 290 RVA: 0x000067F8 File Offset: 0x000057F8
		internal void LoadDomainSettingsFromXml(EwsXmlReader reader)
		{
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.NodeType == 1 && reader.LocalName == "DomainSetting")
					{
						string text = reader.ReadAttributeValue(XmlNamespace.XmlSchemaInstance, "type");
						string text2;
						if ((text2 = text) != null && text2 == "DomainStringSetting")
						{
							this.ReadSettingFromXml(reader);
						}
						else
						{
							EwsUtilities.Assert(false, "GetDomainSettingsResponse.LoadDomainSettingsFromXml", string.Format("Invalid setting class '{0}' returned", text));
						}
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Autodiscover, "DomainSettings"));
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000687C File Offset: 0x0000587C
		private void ReadSettingFromXml(EwsXmlReader reader)
		{
			DomainSettingName? domainSettingName = default(DomainSettingName?);
			object obj = null;
			do
			{
				reader.Read();
				string localName;
				if (reader.NodeType == 1 && (localName = reader.LocalName) != null)
				{
					if (!(localName == "Name"))
					{
						if (localName == "Value")
						{
							obj = reader.ReadElementValue();
						}
					}
					else
					{
						domainSettingName = new DomainSettingName?(reader.ReadElementValue<DomainSettingName>());
					}
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Autodiscover, "DomainSetting"));
			EwsUtilities.Assert(domainSettingName != null, "GetDomainSettingsResponse.ReadSettingFromXml", "Missing name element in domain setting");
			this.settings.Add(domainSettingName.Value, obj);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x00006918 File Offset: 0x00005918
		private void LoadDomainSettingErrorsFromXml(EwsXmlReader reader)
		{
			if (!reader.IsEmptyElement)
			{
				do
				{
					reader.Read();
					if (reader.NodeType == 1 && reader.LocalName == "DomainSettingError")
					{
						DomainSettingError domainSettingError = new DomainSettingError();
						domainSettingError.LoadFromXml(reader);
						this.domainSettingErrors.Add(domainSettingError);
					}
				}
				while (!reader.IsEndElement(XmlNamespace.Autodiscover, "DomainSettingErrors"));
			}
		}

		// Token: 0x04000068 RID: 104
		private string domain;

		// Token: 0x04000069 RID: 105
		private string redirectTarget;

		// Token: 0x0400006A RID: 106
		private Dictionary<DomainSettingName, object> settings;

		// Token: 0x0400006B RID: 107
		private Collection<DomainSettingError> domainSettingErrors;
	}
}
