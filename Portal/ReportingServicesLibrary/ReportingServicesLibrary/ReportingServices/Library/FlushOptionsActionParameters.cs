using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001AE RID: 430
	internal sealed class FlushOptionsActionParameters : RSSoapActionParameters
	{
		// Token: 0x170004C7 RID: 1223
		// (get) Token: 0x06000F77 RID: 3959 RVA: 0x0003781D File Offset: 0x00035A1D
		// (set) Token: 0x06000F78 RID: 3960 RVA: 0x00037825 File Offset: 0x00035A25
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

		// Token: 0x06000F79 RID: 3961 RVA: 0x0003782E File Offset: 0x00035A2E
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Report");
			}
		}

		// Token: 0x0400062E RID: 1582
		private string m_itemPath;
	}
}
