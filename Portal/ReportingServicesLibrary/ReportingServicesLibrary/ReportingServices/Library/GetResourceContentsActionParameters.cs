using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001CE RID: 462
	internal sealed class GetResourceContentsActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004FF RID: 1279
		// (get) Token: 0x06001026 RID: 4134 RVA: 0x000393F6 File Offset: 0x000375F6
		// (set) Token: 0x06001027 RID: 4135 RVA: 0x000393FE File Offset: 0x000375FE
		public string ItemPath
		{
			get
			{
				return this.m_itemPath;
			}
			set
			{
				this.m_itemPath = value;
			}
		}

		// Token: 0x17000500 RID: 1280
		// (get) Token: 0x06001028 RID: 4136 RVA: 0x00039407 File Offset: 0x00037607
		// (set) Token: 0x06001029 RID: 4137 RVA: 0x0003940F File Offset: 0x0003760F
		public byte[] Content
		{
			get
			{
				return this.m_content;
			}
			set
			{
				this.m_content = value;
			}
		}

		// Token: 0x17000501 RID: 1281
		// (get) Token: 0x0600102A RID: 4138 RVA: 0x00039418 File Offset: 0x00037618
		// (set) Token: 0x0600102B RID: 4139 RVA: 0x00039420 File Offset: 0x00037620
		public string MimeType
		{
			get
			{
				return this.m_mimeType;
			}
			set
			{
				this.m_mimeType = value;
			}
		}

		// Token: 0x17000502 RID: 1282
		// (get) Token: 0x0600102C RID: 4140 RVA: 0x00039429 File Offset: 0x00037629
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x17000503 RID: 1283
		// (get) Token: 0x0600102D RID: 4141 RVA: 0x00039431 File Offset: 0x00037631
		internal override string OutputTrace
		{
			get
			{
				return this.MimeType;
			}
		}

		// Token: 0x0600102E RID: 4142 RVA: 0x00039439 File Offset: 0x00037639
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Resource");
			}
		}

		// Token: 0x0400064F RID: 1615
		private string m_itemPath;

		// Token: 0x04000650 RID: 1616
		private byte[] m_content;

		// Token: 0x04000651 RID: 1617
		private string m_mimeType;
	}
}
