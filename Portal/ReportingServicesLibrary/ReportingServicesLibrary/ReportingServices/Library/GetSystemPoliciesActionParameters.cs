using System;
using Microsoft.ReportingServices.Library.Soap;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001F0 RID: 496
	internal sealed class GetSystemPoliciesActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000548 RID: 1352
		// (get) Token: 0x060010ED RID: 4333 RVA: 0x0003AB54 File Offset: 0x00038D54
		// (set) Token: 0x060010EE RID: 4334 RVA: 0x0003AB5C File Offset: 0x00038D5C
		public Policy[] Policies
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

		// Token: 0x060010EF RID: 4335 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal override void Validate()
		{
		}

		// Token: 0x04000677 RID: 1655
		private Policy[] m_policies;
	}
}
