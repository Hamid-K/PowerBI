using System;
using Microsoft.Exchange.WebServices.Data;

namespace Microsoft.Exchange.WebServices.Autodiscover
{
	// Token: 0x0200000B RID: 11
	public abstract class AutodiscoverResponse
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00002722 File Offset: 0x00001722
		internal AutodiscoverResponse()
		{
			this.errorCode = AutodiscoverErrorCode.NoError;
			this.errorMessage = Strings.NoError;
		}

		// Token: 0x06000029 RID: 41 RVA: 0x00002744 File Offset: 0x00001744
		internal virtual void LoadFromXml(EwsXmlReader reader, string endElementName)
		{
			string localName;
			if ((localName = reader.LocalName) != null)
			{
				if (localName == "ErrorCode")
				{
					this.ErrorCode = reader.ReadElementValue<AutodiscoverErrorCode>();
					return;
				}
				if (!(localName == "ErrorMessage"))
				{
					return;
				}
				this.ErrorMessage = reader.ReadElementValue();
			}
		}

		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600002A RID: 42 RVA: 0x0000278F File Offset: 0x0000178F
		// (set) Token: 0x0600002B RID: 43 RVA: 0x00002797 File Offset: 0x00001797
		public AutodiscoverErrorCode ErrorCode
		{
			get
			{
				return this.errorCode;
			}
			internal set
			{
				this.errorCode = value;
			}
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600002C RID: 44 RVA: 0x000027A0 File Offset: 0x000017A0
		// (set) Token: 0x0600002D RID: 45 RVA: 0x000027A8 File Offset: 0x000017A8
		public string ErrorMessage
		{
			get
			{
				return this.errorMessage;
			}
			internal set
			{
				this.errorMessage = value;
			}
		}

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600002E RID: 46 RVA: 0x000027B1 File Offset: 0x000017B1
		// (set) Token: 0x0600002F RID: 47 RVA: 0x000027B9 File Offset: 0x000017B9
		internal Uri RedirectionUrl
		{
			get
			{
				return this.redirectionUrl;
			}
			set
			{
				this.redirectionUrl = value;
			}
		}

		// Token: 0x04000015 RID: 21
		private AutodiscoverErrorCode errorCode;

		// Token: 0x04000016 RID: 22
		private string errorMessage;

		// Token: 0x04000017 RID: 23
		private Uri redirectionUrl;
	}
}
