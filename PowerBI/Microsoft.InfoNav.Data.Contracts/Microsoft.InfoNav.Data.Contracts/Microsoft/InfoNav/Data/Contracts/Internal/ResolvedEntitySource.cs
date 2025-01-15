using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200020A RID: 522
	[ImmutableObject(true)]
	public sealed class ResolvedEntitySource : ResolvedQuerySource
	{
		// Token: 0x06000F32 RID: 3890 RVA: 0x0001D64B File Offset: 0x0001B84B
		internal ResolvedEntitySource(string name, IConceptualEntity entity, string schema)
			: base(name)
		{
			this.Entity = entity;
			this.Schema = schema;
		}

		// Token: 0x170003E9 RID: 1001
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x0001D662 File Offset: 0x0001B862
		public IConceptualEntity Entity { get; }

		// Token: 0x170003EA RID: 1002
		// (get) Token: 0x06000F34 RID: 3892 RVA: 0x0001D66A File Offset: 0x0001B86A
		public string Schema { get; }

		// Token: 0x06000F35 RID: 3893 RVA: 0x0001D672 File Offset: 0x0001B872
		public override bool AcceptEquals(ResolvedQueryDefinitionEqualityComparer comparer, ResolvedQuerySource other)
		{
			return comparer.Equals(this, other as ResolvedEntitySource);
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x0001D681 File Offset: 0x0001B881
		public override int AcceptGetHashCode(ResolvedQueryDefinitionEqualityComparer comparer)
		{
			return comparer.GetHashCode(this);
		}
	}
}
