using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000211 RID: 529
	[ImmutableObject(true)]
	public sealed class ResolvedQueryAxis : IEquatable<ResolvedQueryAxis>
	{
		// Token: 0x06000F56 RID: 3926 RVA: 0x0001D7F2 File Offset: 0x0001B9F2
		public ResolvedQueryAxis(string name, IReadOnlyList<ResolvedQueryAxisGroup> groups)
		{
			this.Name = name;
			this.Groups = groups;
		}

		// Token: 0x170003F1 RID: 1009
		// (get) Token: 0x06000F57 RID: 3927 RVA: 0x0001D808 File Offset: 0x0001BA08
		public string Name { get; }

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000F58 RID: 3928 RVA: 0x0001D810 File Offset: 0x0001BA10
		public IReadOnlyList<ResolvedQueryAxisGroup> Groups { get; }

		// Token: 0x06000F59 RID: 3929 RVA: 0x0001D818 File Offset: 0x0001BA18
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResolvedQueryAxis);
		}

		// Token: 0x06000F5A RID: 3930 RVA: 0x0001D826 File Offset: 0x0001BA26
		public bool Equals(ResolvedQueryAxis other)
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.Equals(this, other);
		}

		// Token: 0x06000F5B RID: 3931 RVA: 0x0001D834 File Offset: 0x0001BA34
		public override int GetHashCode()
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.GetHashCode(this);
		}
	}
}
