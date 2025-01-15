using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.DataShaping.InternalContracts.DataShapeQuery
{
	// Token: 0x020000AD RID: 173
	internal sealed class WindowLimitOperator : LimitOperator
	{
		// Token: 0x1700010E RID: 270
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x000076FE File Offset: 0x000058FE
		// (set) Token: 0x06000413 RID: 1043 RVA: 0x00007706 File Offset: 0x00005906
		public List<RestartToken> RestartTokens { get; set; }

		// Token: 0x1700010F RID: 271
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x0000770F File Offset: 0x0000590F
		// (set) Token: 0x06000415 RID: 1045 RVA: 0x00007717 File Offset: 0x00005917
		public RestartMatchingBehavior? RestartMatchingBehavior { get; set; }

		// Token: 0x17000110 RID: 272
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00007720 File Offset: 0x00005920
		public override ObjectType ObjectType
		{
			get
			{
				return ObjectType.WindowLimitOperator;
			}
		}

		// Token: 0x06000417 RID: 1047 RVA: 0x00007724 File Offset: 0x00005924
		public override TResult Accept<TResult>(LimitVisitor<TResult> visitor)
		{
			return visitor.Visit(this);
		}
	}
}
