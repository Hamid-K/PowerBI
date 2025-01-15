using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000212 RID: 530
	[ImmutableObject(true)]
	public sealed class ResolvedQueryAxisGroup : IEquatable<ResolvedQueryAxisGroup>
	{
		// Token: 0x06000F5C RID: 3932 RVA: 0x0001D841 File Offset: 0x0001BA41
		public ResolvedQueryAxisGroup(IReadOnlyList<ResolvedQueryExpression> keys, bool subtotal)
		{
			this.Keys = keys;
			this.Subtotal = subtotal;
		}

		// Token: 0x170003F3 RID: 1011
		// (get) Token: 0x06000F5D RID: 3933 RVA: 0x0001D857 File Offset: 0x0001BA57
		public IReadOnlyList<ResolvedQueryExpression> Keys { get; }

		// Token: 0x170003F4 RID: 1012
		// (get) Token: 0x06000F5E RID: 3934 RVA: 0x0001D85F File Offset: 0x0001BA5F
		public bool Subtotal { get; }

		// Token: 0x06000F5F RID: 3935 RVA: 0x0001D867 File Offset: 0x0001BA67
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResolvedQueryAxisGroup);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0001D875 File Offset: 0x0001BA75
		public bool Equals(ResolvedQueryAxisGroup other)
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.Equals(this, other);
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0001D883 File Offset: 0x0001BA83
		public override int GetHashCode()
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.GetHashCode(this);
		}
	}
}
