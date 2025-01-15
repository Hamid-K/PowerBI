using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000260 RID: 608
	[ImmutableObject(true)]
	public sealed class ResolvedQueryTransformTableColumn : IEquatable<ResolvedQueryTransformTableColumn>
	{
		// Token: 0x06001241 RID: 4673 RVA: 0x000201D1 File Offset: 0x0001E3D1
		internal ResolvedQueryTransformTableColumn(string name, string role, ResolvedQueryExpression expression)
		{
			this._name = name;
			this._role = role;
			this._expression = expression;
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001242 RID: 4674 RVA: 0x000201EE File Offset: 0x0001E3EE
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001243 RID: 4675 RVA: 0x000201F6 File Offset: 0x0001E3F6
		public string Role
		{
			get
			{
				return this._role;
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001244 RID: 4676 RVA: 0x000201FE File Offset: 0x0001E3FE
		public ResolvedQueryExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x06001245 RID: 4677 RVA: 0x00020206 File Offset: 0x0001E406
		public bool Equals(ResolvedQueryTransformTableColumn other)
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.Equals(this, other);
		}

		// Token: 0x06001246 RID: 4678 RVA: 0x00020214 File Offset: 0x0001E414
		public override int GetHashCode()
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.GetHashCode(this);
		}

		// Token: 0x06001247 RID: 4679 RVA: 0x00020221 File Offset: 0x0001E421
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResolvedQueryTransformTableColumn);
		}

		// Token: 0x040007BC RID: 1980
		private readonly string _name;

		// Token: 0x040007BD RID: 1981
		private readonly string _role;

		// Token: 0x040007BE RID: 1982
		private readonly ResolvedQueryExpression _expression;
	}
}
