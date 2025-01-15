using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200025C RID: 604
	[ImmutableObject(true)]
	public sealed class ResolvedQueryTransformOutput : IEquatable<ResolvedQueryTransformOutput>
	{
		// Token: 0x06001229 RID: 4649 RVA: 0x00020048 File Offset: 0x0001E248
		internal ResolvedQueryTransformOutput(ResolvedQueryTransformTable table)
		{
			this._table = table;
		}

		// Token: 0x1700045F RID: 1119
		// (get) Token: 0x0600122A RID: 4650 RVA: 0x00020057 File Offset: 0x0001E257
		public ResolvedQueryTransformTable Table
		{
			get
			{
				return this._table;
			}
		}

		// Token: 0x0600122B RID: 4651 RVA: 0x0002005F File Offset: 0x0001E25F
		public bool Equals(ResolvedQueryTransformOutput other)
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.Equals(this, other);
		}

		// Token: 0x0600122C RID: 4652 RVA: 0x0002006D File Offset: 0x0001E26D
		public override int GetHashCode()
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.GetHashCode(this);
		}

		// Token: 0x0600122D RID: 4653 RVA: 0x0002007A File Offset: 0x0001E27A
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResolvedQueryTransformOutput);
		}

		// Token: 0x040007B6 RID: 1974
		private readonly ResolvedQueryTransformTable _table;
	}
}
