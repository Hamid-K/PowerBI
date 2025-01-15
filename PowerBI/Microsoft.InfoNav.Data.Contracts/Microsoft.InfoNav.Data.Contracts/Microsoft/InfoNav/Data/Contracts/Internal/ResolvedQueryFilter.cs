using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000236 RID: 566
	[ImmutableObject(true)]
	public sealed class ResolvedQueryFilter : IEquatable<ResolvedQueryFilter>
	{
		// Token: 0x0600112D RID: 4397 RVA: 0x0001F4B2 File Offset: 0x0001D6B2
		internal ResolvedQueryFilter(IReadOnlyList<ResolvedQueryExpression> target, ResolvedQueryExpression condition, FilterAnnotations annotations)
		{
			this._target = target;
			this._condition = condition;
			this._annotations = annotations;
		}

		// Token: 0x1700041A RID: 1050
		// (get) Token: 0x0600112E RID: 4398 RVA: 0x0001F4CF File Offset: 0x0001D6CF
		public IReadOnlyList<ResolvedQueryExpression> Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x1700041B RID: 1051
		// (get) Token: 0x0600112F RID: 4399 RVA: 0x0001F4D7 File Offset: 0x0001D6D7
		public ResolvedQueryExpression Condition
		{
			get
			{
				return this._condition;
			}
		}

		// Token: 0x1700041C RID: 1052
		// (get) Token: 0x06001130 RID: 4400 RVA: 0x0001F4DF File Offset: 0x0001D6DF
		public FilterAnnotations Annotations
		{
			get
			{
				return this._annotations;
			}
		}

		// Token: 0x06001131 RID: 4401 RVA: 0x0001F4E7 File Offset: 0x0001D6E7
		public bool Equals(ResolvedQueryFilter other)
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.Equals(this, other);
		}

		// Token: 0x06001132 RID: 4402 RVA: 0x0001F4F5 File Offset: 0x0001D6F5
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResolvedQueryFilter);
		}

		// Token: 0x06001133 RID: 4403 RVA: 0x0001F503 File Offset: 0x0001D703
		public override int GetHashCode()
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.GetHashCode(this);
		}

		// Token: 0x04000775 RID: 1909
		private readonly IReadOnlyList<ResolvedQueryExpression> _target;

		// Token: 0x04000776 RID: 1910
		private readonly ResolvedQueryExpression _condition;

		// Token: 0x04000777 RID: 1911
		private readonly FilterAnnotations _annotations;
	}
}
