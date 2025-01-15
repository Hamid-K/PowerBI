using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001E8 RID: 488
	internal sealed class DeleteRoleActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000536 RID: 1334
		// (get) Token: 0x060010BC RID: 4284 RVA: 0x0003A5F2 File Offset: 0x000387F2
		// (set) Token: 0x060010BD RID: 4285 RVA: 0x0003A5FA File Offset: 0x000387FA
		public string RoleName
		{
			get
			{
				return this.m_roleName;
			}
			set
			{
				this.m_roleName = value;
			}
		}

		// Token: 0x17000537 RID: 1335
		// (get) Token: 0x060010BE RID: 4286 RVA: 0x0003A603 File Offset: 0x00038803
		internal override string InputTrace
		{
			get
			{
				return this.RoleName;
			}
		}

		// Token: 0x060010BF RID: 4287 RVA: 0x0003A60B File Offset: 0x0003880B
		internal override void Validate()
		{
			if (this.RoleName == null)
			{
				throw new MissingParameterException("Name");
			}
		}

		// Token: 0x0400066C RID: 1644
		private string m_roleName;
	}
}
