using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.DataShaping.InternalContracts.DataShapeQuery;

namespace Microsoft.ReportingServices.DataShapeQueryTranslation.Annotations
{
	// Token: 0x02000250 RID: 592
	internal sealed class DataIntersectionAnnotation
	{
		// Token: 0x06001484 RID: 5252 RVA: 0x0004E884 File Offset: 0x0004CA84
		internal DataIntersectionAnnotation(IEnumerable<Limit> limits, bool areContentsIncludedInOutput)
		{
			this.m_limits = limits;
			this.AreContentsIncludedInOutput = areContentsIncludedInOutput;
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001485 RID: 5253 RVA: 0x0004E89A File Offset: 0x0004CA9A
		public Limit Limit
		{
			get
			{
				if (this.m_limits != null)
				{
					return this.m_limits.SingleOrDefault<Limit>();
				}
				return null;
			}
		}

		// Token: 0x170003B7 RID: 951
		// (get) Token: 0x06001486 RID: 5254 RVA: 0x0004E8B1 File Offset: 0x0004CAB1
		public bool AreContentsIncludedInOutput { get; }

		// Token: 0x0400091C RID: 2332
		private readonly IEnumerable<Limit> m_limits;
	}
}
