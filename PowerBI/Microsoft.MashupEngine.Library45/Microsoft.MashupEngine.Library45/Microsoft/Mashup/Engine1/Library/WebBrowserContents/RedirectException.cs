using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.WebBrowserContents
{
	// Token: 0x02002044 RID: 8260
	[Serializable]
	public sealed class RedirectException : Exception
	{
		// Token: 0x06011353 RID: 70483 RVA: 0x003B3BAB File Offset: 0x003B1DAB
		public RedirectException(string oldUrl, string newUrl)
		{
			this.oldUrl = oldUrl;
			this.newUrl = newUrl;
		}

		// Token: 0x06011354 RID: 70484 RVA: 0x003B3BC1 File Offset: 0x003B1DC1
		private RedirectException(SerializationInfo info, StreamingContext context)
			: base(info, context)
		{
		}

		// Token: 0x17002DF1 RID: 11761
		// (get) Token: 0x06011355 RID: 70485 RVA: 0x003B3BCB File Offset: 0x003B1DCB
		public string OldUrl
		{
			get
			{
				return this.oldUrl;
			}
		}

		// Token: 0x17002DF2 RID: 11762
		// (get) Token: 0x06011356 RID: 70486 RVA: 0x003B3BD3 File Offset: 0x003B1DD3
		public string NewUrl
		{
			get
			{
				return this.newUrl;
			}
		}

		// Token: 0x06011357 RID: 70487 RVA: 0x003B3BDB File Offset: 0x003B1DDB
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			info.AddValue("oldUrl", this.oldUrl);
			info.AddValue("newUrl", this.newUrl);
			base.GetObjectData(info, context);
		}

		// Token: 0x04006859 RID: 26713
		private readonly string oldUrl;

		// Token: 0x0400685A RID: 26714
		private readonly string newUrl;
	}
}
