using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200025B RID: 603
	[ImmutableObject(true)]
	public sealed class ResolvedQueryTransformInput : IEquatable<ResolvedQueryTransformInput>
	{
		// Token: 0x06001223 RID: 4643 RVA: 0x0001FFF9 File Offset: 0x0001E1F9
		internal ResolvedQueryTransformInput(IReadOnlyList<ResolvedQueryTransformParameter> parameters, ResolvedQueryTransformTable table)
		{
			this._parameters = parameters;
			this._table = table;
		}

		// Token: 0x1700045D RID: 1117
		// (get) Token: 0x06001224 RID: 4644 RVA: 0x0002000F File Offset: 0x0001E20F
		public IReadOnlyList<ResolvedQueryTransformParameter> Parameters
		{
			get
			{
				return this._parameters;
			}
		}

		// Token: 0x1700045E RID: 1118
		// (get) Token: 0x06001225 RID: 4645 RVA: 0x00020017 File Offset: 0x0001E217
		public ResolvedQueryTransformTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x06001226 RID: 4646 RVA: 0x0002001F File Offset: 0x0001E21F
		public bool Equals(ResolvedQueryTransformInput other)
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.Equals(this, other);
		}

		// Token: 0x06001227 RID: 4647 RVA: 0x0002002D File Offset: 0x0001E22D
		public override int GetHashCode()
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.GetHashCode(this);
		}

		// Token: 0x06001228 RID: 4648 RVA: 0x0002003A File Offset: 0x0001E23A
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResolvedQueryTransformInput);
		}

		// Token: 0x040007B4 RID: 1972
		private readonly IReadOnlyList<ResolvedQueryTransformParameter> _parameters;

		// Token: 0x040007B5 RID: 1973
		private readonly ResolvedQueryTransformTable _table;
	}
}
