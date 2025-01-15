using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200025E RID: 606
	[ImmutableObject(true)]
	public sealed class ResolvedQueryTransformParameter : IEquatable<ResolvedQueryTransformParameter>
	{
		// Token: 0x06001234 RID: 4660 RVA: 0x000200C9 File Offset: 0x0001E2C9
		internal ResolvedQueryTransformParameter(string name, ResolvedQueryExpression expression)
		{
			this._name = name;
			this._expression = expression;
		}

		// Token: 0x17000461 RID: 1121
		// (get) Token: 0x06001235 RID: 4661 RVA: 0x000200DF File Offset: 0x0001E2DF
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000462 RID: 1122
		// (get) Token: 0x06001236 RID: 4662 RVA: 0x000200E7 File Offset: 0x0001E2E7
		public ResolvedQueryExpression Expression
		{
			get
			{
				return this._expression;
			}
		}

		// Token: 0x06001237 RID: 4663 RVA: 0x000200EF File Offset: 0x0001E2EF
		public bool Equals(ResolvedQueryTransformParameter other)
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.Equals(this, other);
		}

		// Token: 0x06001238 RID: 4664 RVA: 0x000200FD File Offset: 0x0001E2FD
		public override int GetHashCode()
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.GetHashCode(this);
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x0002010A File Offset: 0x0001E30A
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResolvedQueryTransformParameter);
		}

		// Token: 0x040007B8 RID: 1976
		private readonly string _name;

		// Token: 0x040007B9 RID: 1977
		private readonly ResolvedQueryExpression _expression;
	}
}
