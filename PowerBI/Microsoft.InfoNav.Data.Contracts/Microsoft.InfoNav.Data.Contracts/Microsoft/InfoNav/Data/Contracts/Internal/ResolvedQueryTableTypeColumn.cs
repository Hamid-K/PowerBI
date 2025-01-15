using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000259 RID: 601
	[ImmutableObject(true)]
	public sealed class ResolvedQueryTableTypeColumn : IEquatable<ResolvedQueryTableTypeColumn>
	{
		// Token: 0x06001215 RID: 4629 RVA: 0x0001FF3C File Offset: 0x0001E13C
		public ResolvedQueryTableTypeColumn(string name, ResolvedQueryExpression expression)
		{
			this.Name = name;
			this.Expression = expression;
		}

		// Token: 0x17000457 RID: 1111
		// (get) Token: 0x06001216 RID: 4630 RVA: 0x0001FF52 File Offset: 0x0001E152
		public string Name { get; }

		// Token: 0x17000458 RID: 1112
		// (get) Token: 0x06001217 RID: 4631 RVA: 0x0001FF5A File Offset: 0x0001E15A
		public ResolvedQueryExpression Expression { get; }

		// Token: 0x06001218 RID: 4632 RVA: 0x0001FF62 File Offset: 0x0001E162
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResolvedQueryTableTypeColumn);
		}

		// Token: 0x06001219 RID: 4633 RVA: 0x0001FF70 File Offset: 0x0001E170
		public bool Equals(ResolvedQueryTableTypeColumn other)
		{
			return DefaultResolvedQueryExpressionEqualityComparer.Instance.Equals(this, other);
		}

		// Token: 0x0600121A RID: 4634 RVA: 0x0001FF7E File Offset: 0x0001E17E
		public override int GetHashCode()
		{
			return DefaultResolvedQueryExpressionEqualityComparer.Instance.GetHashCode(this);
		}
	}
}
