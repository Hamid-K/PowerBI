using System;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001F4 RID: 500
	internal sealed class SetSystemPoliciesActionParameters : RSSoapActionParameters
	{
		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x060010FF RID: 4351 RVA: 0x0003AD71 File Offset: 0x00038F71
		// (set) Token: 0x06001100 RID: 4352 RVA: 0x0003AD79 File Offset: 0x00038F79
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

		// Token: 0x06001101 RID: 4353 RVA: 0x0003AD82 File Offset: 0x00038F82
		internal override void Validate()
		{
			if (this.Policies == null)
			{
				throw new MissingParameterException("Policies");
			}
		}

		// Token: 0x0400067A RID: 1658
		private string m_policies;
	}
}
