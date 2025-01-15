using System;
using System.Collections.Generic;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.DataTransformBypass
{
	// Token: 0x020000BC RID: 188
	internal sealed class DataTransformColumnInliningInfo
	{
		// Token: 0x0600081D RID: 2077 RVA: 0x0001F58B File Offset: 0x0001D78B
		internal DataTransformColumnInliningInfo()
		{
			this.m_referrers = new List<ExpressionReference>();
		}

		// Token: 0x17000145 RID: 325
		// (get) Token: 0x0600081E RID: 2078 RVA: 0x0001F59E File Offset: 0x0001D79E
		internal IReadOnlyList<ExpressionReference> Referrers
		{
			get
			{
				return this.m_referrers;
			}
		}

		// Token: 0x0600081F RID: 2079 RVA: 0x0001F5A6 File Offset: 0x0001D7A6
		internal void AddReferrer(ExpressionReference referrer)
		{
			this.m_referrers.Add(referrer);
		}

		// Token: 0x04000403 RID: 1027
		private readonly List<ExpressionReference> m_referrers;
	}
}
