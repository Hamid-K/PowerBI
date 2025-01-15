using System;
using System.Collections.Specialized;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020001FA RID: 506
	internal sealed class GetSystemPermissionsActionParameters : RSSoapActionParameters
	{
		// Token: 0x17000554 RID: 1364
		// (get) Token: 0x0600111A RID: 4378 RVA: 0x0003B0AE File Offset: 0x000392AE
		// (set) Token: 0x0600111B RID: 4379 RVA: 0x0003B0B6 File Offset: 0x000392B6
		public StringCollection Operations
		{
			get
			{
				return this.m_operations;
			}
			set
			{
				this.m_operations = value;
			}
		}

		// Token: 0x0600111C RID: 4380 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal override void Validate()
		{
		}

		// Token: 0x0400067E RID: 1662
		private StringCollection m_operations;
	}
}
