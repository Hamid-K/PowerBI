using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200023C RID: 572
	[ImmutableObject(true)]
	public sealed class ResolvedQueryLetBinding : IEquatable<ResolvedQueryLetBinding>
	{
		// Token: 0x0600115C RID: 4444 RVA: 0x0001F6EC File Offset: 0x0001D8EC
		public ResolvedQueryLetBinding(string name, ResolvedQueryExpression expression)
		{
			this.Name = name;
			this.Expression = expression;
		}

		// Token: 0x1700042B RID: 1067
		// (get) Token: 0x0600115D RID: 4445 RVA: 0x0001F702 File Offset: 0x0001D902
		public string Name { get; }

		// Token: 0x1700042C RID: 1068
		// (get) Token: 0x0600115E RID: 4446 RVA: 0x0001F70A File Offset: 0x0001D90A
		public ResolvedQueryExpression Expression { get; }

		// Token: 0x0600115F RID: 4447 RVA: 0x0001F712 File Offset: 0x0001D912
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResolvedQueryLetBinding);
		}

		// Token: 0x06001160 RID: 4448 RVA: 0x0001F720 File Offset: 0x0001D920
		public bool Equals(ResolvedQueryLetBinding other)
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.Equals(this, other);
		}

		// Token: 0x06001161 RID: 4449 RVA: 0x0001F72E File Offset: 0x0001D92E
		public override int GetHashCode()
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.GetHashCode(this);
		}
	}
}
