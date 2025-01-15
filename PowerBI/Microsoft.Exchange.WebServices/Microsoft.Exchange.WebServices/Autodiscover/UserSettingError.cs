using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x0200001F RID: 31
	public sealed class UserSettingError
	{
		// Token: 0x06000151 RID: 337 RVA: 0x00006F98 File Offset: 0x00005F98
		internal UserSettingError()
		{
		}

		// Token: 0x06000152 RID: 338 RVA: 0x00006FA0 File Offset: 0x00005FA0
		internal UserSettingError(AutodiscoverErrorCode errorCode, string errorMessage, string settingName)
		{
			this.ErrorCode = errorCode;
			this.ErrorMessage = errorMessage;
			this.SettingName = settingName;
		}

		// Token: 0x06000153 RID: 339 RVA: 0x00006FC0 File Offset: 0x00005FC0
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
								this.SettingName = reader.ReadElementValue();
							}
						}
						else
						{
							this.ErrorMessage = reader.ReadElementValue();
						}
					}
					else
					{
						this.ErrorCode = reader.ReadElementValue<AutodiscoverErrorCode>();
					}
				}
			}
			while (!reader.IsEndElement(XmlNamespace.Autodiscover, "UserSettingError"));
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00007046 File Offset: 0x00006046
		// (set) Token: 0x06000155 RID: 341 RVA: 0x0000704E File Offset: 0x0000604E
		public AutodiscoverErrorCode ErrorCode { get; internal set; }

		// Token: 0x1700005A RID: 90
		// (get) Token: 0x06000156 RID: 342 RVA: 0x00007057 File Offset: 0x00006057
		// (set) Token: 0x06000157 RID: 343 RVA: 0x0000705F File Offset: 0x0000605F
		public string ErrorMessage { get; internal set; }

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x06000158 RID: 344 RVA: 0x00007068 File Offset: 0x00006068
		// (set) Token: 0x06000159 RID: 345 RVA: 0x00007070 File Offset: 0x00006070
		public string SettingName { get; internal set; }
	}
}
