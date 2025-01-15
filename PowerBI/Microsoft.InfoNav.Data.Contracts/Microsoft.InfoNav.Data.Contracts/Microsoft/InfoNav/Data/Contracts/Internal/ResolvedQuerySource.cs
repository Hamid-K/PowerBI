using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000253 RID: 595
	[ImmutableObject(true)]
	public abstract class ResolvedQuerySource : IEquatable<ResolvedQuerySource>
	{
		// Token: 0x060011EB RID: 4587 RVA: 0x0001FD6D File Offset: 0x0001DF6D
		protected ResolvedQuerySource(string name)
		{
			this.Name = name;
		}

		// Token: 0x1700044D RID: 1101
		// (get) Token: 0x060011EC RID: 4588 RVA: 0x0001FD7C File Offset: 0x0001DF7C
		public string Name { get; }

		// Token: 0x060011ED RID: 4589
		public abstract bool AcceptEquals(ResolvedQueryDefinitionEqualityComparer comparer, ResolvedQuerySource other);

		// Token: 0x060011EE RID: 4590
		public abstract int AcceptGetHashCode(ResolvedQueryDefinitionEqualityComparer comparer);

		// Token: 0x060011EF RID: 4591 RVA: 0x0001FD84 File Offset: 0x0001DF84
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResolvedQuerySource);
		}

		// Token: 0x060011F0 RID: 4592 RVA: 0x0001FD92 File Offset: 0x0001DF92
		public bool Equals(ResolvedQuerySource other)
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.Equals(this, other);
		}

		// Token: 0x060011F1 RID: 4593 RVA: 0x0001FDA0 File Offset: 0x0001DFA0
		public override int GetHashCode()
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.GetHashCode(this);
		}
	}
}
