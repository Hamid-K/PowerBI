using System;
using Microsoft.Mashup.Engine1.Language.Query;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x02001644 RID: 5700
	internal abstract class OptimizableTableValue : TableValue
	{
		// Token: 0x1700259E RID: 9630
		// (get) Token: 0x06008FB2 RID: 36786 RVA: 0x001A8D0C File Offset: 0x001A6F0C
		public override IQueryDomain QueryDomain
		{
			get
			{
				return new OptimizableTableQueryDomain(this);
			}
		}
	}
}
