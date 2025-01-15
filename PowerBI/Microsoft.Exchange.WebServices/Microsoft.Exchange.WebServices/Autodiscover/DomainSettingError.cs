using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000013 RID: 19
	public sealed class DomainSettingError
	{
		// Token: 0x060000CC RID: 204 RVA: 0x0000549C File Offset: 0x0000449C
		internal DomainSettingError()
		{
		}

		// Token: 0x060000CD RID: 205 RVA: 0x000054A4 File Offset: 0x000044A4
		internal void LoadFromXml(EwsXmlReader reader)
		{
			do
			{
				reader.Read();
				string localName;
				if (reader.NodeType == 1 && (localName = reader.LocalName) != null)
				{
					if (!(localName == "ErrorCode"))
					{
						if (!(localName == "ErrorMessage"))
						{
							if (localName == "SettingName")
							{
								this.settingName = reader.ReadElementValue();
							}
						}
						else
						{
							this.errorMessage = reader.ReadElementValue();
						}
					}
					else
					{
						this.errorCode = reader.ReadElementValue<AutodiscoverErrorCode>();
					}
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Autodiscover, "DomainSettingError"));
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000CE RID: 206 RVA: 0x0000552A File Offset: 0x0000452A
		public AutodiscoverErrorCode ErrorCode
		{
			get
			{
				return this.errorCode;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000CF RID: 207 RVA: 0x00005532 File Offset: 0x00004532
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000D0 RID: 208 RVA: 0x0000553A File Offset: 0x0000453A
		public string SettingName
		{
			get
			{
				return this.settingName;
			}
		}

		// Token: 0x04000055 RID: 85
		private AutodiscoverErrorCode errorCode;

		// Token: 0x04000056 RID: 86
		private string errorMessage;

		// Token: 0x04000057 RID: 87
		private string settingName;
	}
}
