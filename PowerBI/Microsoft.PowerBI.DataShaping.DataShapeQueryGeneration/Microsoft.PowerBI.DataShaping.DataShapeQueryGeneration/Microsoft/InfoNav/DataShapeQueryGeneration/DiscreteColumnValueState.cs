using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Data.Contracts.Internal;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000029 RID: 41
	internal sealed class DiscreteColumnValueState
	{
		// Token: 0x0600019A RID: 410 RVA: 0x00009619 File Offset: 0x00007819
		internal DiscreteColumnValueState(IConceptualProperty property)
		{
			this._property = property;
			this._values = new HashSet<ResolvedQueryExpression>();
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600019B RID: 411 RVA: 0x00009633 File Offset: 0x00007833
		internal IConceptualProperty Property
		{
			get
			{
				return this._property;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x0600019C RID: 412 RVA: 0x0000963B File Offset: 0x0000783B
		internal HashSet<ResolvedQueryExpression> Values
		{
			get
			{
				return this._values;
			}
		}

		// Token: 0x040000C9 RID: 201
		private readonly IConceptualProperty _property;

		// Token: 0x040000CA RID: 202
		private readonly HashSet<ResolvedQueryExpression> _values;
	}
}
