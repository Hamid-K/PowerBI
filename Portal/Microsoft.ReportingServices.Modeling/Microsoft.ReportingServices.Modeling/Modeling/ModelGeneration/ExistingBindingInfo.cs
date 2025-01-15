using System;

namespace Microsoft.ReportingServices.Modeling.ModelGeneration
{
	// Token: 0x020000DA RID: 218
	internal abstract class ExistingBindingInfo
	{
		// Token: 0x06000BB2 RID: 2994 RVA: 0x000269A4 File Offset: 0x00024BA4
		public ExistingBindingInfo()
		{
		}

		// Token: 0x170002B3 RID: 691
		// (get) Token: 0x06000BB3 RID: 2995 RVA: 0x000269AC File Offset: 0x00024BAC
		// (set) Token: 0x06000BB4 RID: 2996 RVA: 0x000269B4 File Offset: 0x00024BB4
		public bool EvaluateDependentRules
		{
			get
			{
				return this.m_evaluateDependentRules;
			}
			set
			{
				this.m_evaluateDependentRules = value;
			}
		}

		// Token: 0x040004D0 RID: 1232
		private bool m_evaluateDependentRules;
	}
}
