using System;
using System.ComponentModel;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x02000251 RID: 593
	[ImmutableObject(true)]
	public sealed class ResolvedQuerySelect : IEquatable<ResolvedQuerySelect>
	{
		// Token: 0x060011DE RID: 4574 RVA: 0x0001FCC0 File Offset: 0x0001DEC0
		public ResolvedQuerySelect(ResolvedQueryExpression expression, string name, string nativeReferenceName)
		{
			this.Expression = expression;
			this.Name = name;
			this.NativeReferenceName = nativeReferenceName;
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x060011DF RID: 4575 RVA: 0x0001FCDD File Offset: 0x0001DEDD
		public ResolvedQueryExpression Expression { get; }

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x060011E0 RID: 4576 RVA: 0x0001FCE5 File Offset: 0x0001DEE5
		public string NativeReferenceName { get; }

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x060011E1 RID: 4577 RVA: 0x0001FCED File Offset: 0x0001DEED
		public string Name { get; }

		// Token: 0x060011E2 RID: 4578 RVA: 0x0001FCF5 File Offset: 0x0001DEF5
		public override bool Equals(object obj)
		{
			return this.Equals(obj as ResolvedQuerySelect);
		}

		// Token: 0x060011E3 RID: 4579 RVA: 0x0001FD03 File Offset: 0x0001DF03
		public bool Equals(ResolvedQuerySelect other)
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.Equals(this, other);
		}

		// Token: 0x060011E4 RID: 4580 RVA: 0x0001FD11 File Offset: 0x0001DF11
		public override int GetHashCode()
		{
			return DefaultResolvedQueryDefinitionEqualityComparer.Instance.GetHashCode(this);
		}
	}
}
