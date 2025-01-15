using System;
using System.Collections.Generic;
using System.ComponentModel;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x02000022 RID: 34
	[EditorBrowsable(1)]
	internal abstract class ConfigurationSettingsBase
	{
		// Token: 0x06000164 RID: 356 RVA: 0x0000719D File Offset: 0x0000619D
		internal ConfigurationSettingsBase()
		{
		}

		// Token: 0x06000165 RID: 357 RVA: 0x000071A5 File Offset: 0x000061A5
		internal virtual bool TryReadCurrentXmlElement(EwsXmlReader reader)
		{
			if (reader.LocalName == "Error")
			{
				this.error = AutodiscoverError.Parse(reader);
				return true;
			}
			return false;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x000071C8 File Offset: 0x000061C8
		internal void LoadFromXml(EwsXmlReader reader)
		{
			reader.ReadStartElement(XmlNamespace.NotSpecified, "Autodiscover");
			reader.ReadStartElement(XmlNamespace.NotSpecified, "Response");
			do
			{
				reader.Read();
				if (reader.IsStartElement() && !this.TryReadCurrentXmlElement(reader))
				{
					reader.SkipCurrentElement();
				}
			}
			while (!reader.IsEndElement(XmlNamespace.NotSpecified, "Response"));
			reader.ReadEndElement(XmlNamespace.NotSpecified, "Autodiscover");
		}

		// Token: 0x06000167 RID: 359
		internal abstract string GetNamespace();

		// Token: 0x06000168 RID: 360
		internal abstract void MakeRedirectionResponse(Uri redirectUrl);

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000169 RID: 361
		internal abstract AutodiscoverResponseType ResponseType { get; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600016A RID: 362
		internal abstract string RedirectTarget { get; }

		// Token: 0x0600016B RID: 363
		internal abstract GetUserSettingsResponse ConvertSettings(string smtpAddress, List<UserSettingName> requestedSettings);

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600016C RID: 364 RVA: 0x00007224 File Offset: 0x00006224
		internal AutodiscoverError Error
		{
			get
			{
				return this.error;
			}
		}

		// Token: 0x0400007F RID: 127
		private AutodiscoverError error;
	}
}
