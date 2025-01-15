using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001F2 RID: 498
	internal sealed class SetPoliciesActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x060010F4 RID: 4340 RVA: 0x0003ABD5 File Offset: 0x00038DD5
		// (set) Token: 0x060010F5 RID: 4341 RVA: 0x0003ABDD File Offset: 0x00038DDD
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

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x060010F6 RID: 4342 RVA: 0x0003ABE6 File Offset: 0x00038DE6
		// (set) Token: 0x060010F7 RID: 4343 RVA: 0x0003ABEE File Offset: 0x00038DEE
		public string Policies
		{
			get
			{
				return this.m_policies;
			}
			set
			{
				this.m_policies = value;
			}
		}

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x060010F8 RID: 4344 RVA: 0x0003ABF7 File Offset: 0x00038DF7
		internal override string InputTrace
		{
			get
			{
				return this.ItemPath;
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0003ABFF File Offset: 0x00038DFF
		internal override void Validate()
		{
			if (this.ItemPath == null)
			{
				throw new MissingParameterException(CallingEndpoint.Is2010Endpoint ? "ItemPath" : "Item");
			}
		}

		// Token: 0x04000678 RID: 1656
		private string m_itemPath;

		// Token: 0x04000679 RID: 1657
		private string m_policies;
	}
}
