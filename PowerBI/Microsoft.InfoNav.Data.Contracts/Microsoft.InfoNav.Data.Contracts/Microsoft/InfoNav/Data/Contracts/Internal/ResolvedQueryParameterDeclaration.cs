using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000249 RID: 585
	[ImmutableObject(true)]
	public sealed class ResolvedQueryParameterDeclaration : IEquatable<ResolvedQueryParameterDeclaration>
	{
		// Token: 0x060011AC RID: 4524 RVA: 0x0001FA6C File Offset: 0x0001DC6C
		public ResolvedQueryParameterDeclaration(string name, ResolvedQueryExpression typeExpression)
		{
			this.Name = name;
			this.TypeExpression = typeExpression;
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x060011AD RID: 4525 RVA: 0x0001FA82 File Offset: 0x0001DC82
		public string Name { get; }

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x060011AE RID: 4526 RVA: 0x0001FA8A File Offset: 0x0001DC8A
		public ResolvedQueryExpression TypeExpression { get; }

		// Token: 0x060011AF RID: 4527 RVA: 0x0001FA92 File Offset: 0x0001DC92
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResolvedQueryParameterDeclaration);
		}

		// Token: 0x060011B0 RID: 4528 RVA: 0x0001FAA0 File Offset: 0x0001DCA0
		public bool Equals(ResolvedQueryParameterDeclaration other)
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.Equals(this, other);
		}

		// Token: 0x060011B1 RID: 4529 RVA: 0x0001FAAE File Offset: 0x0001DCAE
		public override int GetHashCode()
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.GetHashCode(this);
		}
	}
}
