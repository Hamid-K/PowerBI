using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000596 RID: 1430
	internal abstract class BoolLiteral : InternalBase
	{
		// Token: 0x06004526 RID: 17702 RVA: 0x000F42E4 File Offset: 0x000F24E4
		internal static TermExpr<DomainConstraint<BoolLiteral, Constant>> MakeTermExpression(BoolLiteral literal, IEnumerable<Constant> domain, IEnumerable<Constant> range)
		{
			Set<Constant> set = new Set<Constant>(domain, Constant.EqualityComparer);
			Set<Constant> set2 = new Set<Constant>(range, Constant.EqualityComparer);
			return BoolLiteral.MakeTermExpression(literal, set, set2);
		}

		// Token: 0x06004527 RID: 17703 RVA: 0x000F4314 File Offset: 0x000F2514
		internal static TermExpr<DomainConstraint<BoolLiteral, Constant>> MakeTermExpression(BoolLiteral literal, Set<Constant> domain, Set<Constant> range)
		{
			domain.MakeReadOnly();
			range.MakeReadOnly();
			DomainConstraint<BoolLiteral, Constant> domainConstraint = new DomainConstraint<BoolLiteral, Constant>(new DomainVariable<BoolLiteral, Constant>(literal, domain, BoolLiteral.EqualityIdentifierComparer), range);
			return new TermExpr<DomainConstraint<BoolLiteral, Constant>>(EqualityComparer<DomainConstraint<BoolLiteral, Constant>>.Default, domainConstraint);
		}

		// Token: 0x06004528 RID: 17704
		internal abstract BoolExpr<DomainConstraint<BoolLiteral, Constant>> FixRange(Set<Constant> range, MemberDomainMap memberDomainMap);

		// Token: 0x06004529 RID: 17705
		internal abstract BoolExpr<DomainConstraint<BoolLiteral, Constant>> GetDomainBoolExpression(MemberDomainMap domainMap);

		// Token: 0x0600452A RID: 17706
		internal abstract BoolLiteral RemapBool(Dictionary<MemberPath, MemberPath> remap);

		// Token: 0x0600452B RID: 17707
		internal abstract void GetRequiredSlots(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots);

		// Token: 0x0600452C RID: 17708
		internal abstract StringBuilder AsEsql(StringBuilder builder, string blockAlias, bool skipIsNotNull);

		// Token: 0x0600452D RID: 17709
		internal abstract DbExpression AsCqt(DbExpression row, bool skipIsNotNull);

		// Token: 0x0600452E RID: 17710
		internal abstract StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull);

		// Token: 0x0600452F RID: 17711
		internal abstract StringBuilder AsNegatedUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull);

		// Token: 0x06004530 RID: 17712 RVA: 0x000F434D File Offset: 0x000F254D
		protected virtual bool IsIdentifierEqualTo(BoolLiteral right)
		{
			return this.IsEqualTo(right);
		}

		// Token: 0x06004531 RID: 17713
		protected abstract bool IsEqualTo(BoolLiteral right);

		// Token: 0x06004532 RID: 17714 RVA: 0x000F4356 File Offset: 0x000F2556
		protected virtual int GetIdentifierHash()
		{
			return this.GetHashCode();
		}

		// Token: 0x040018D8 RID: 6360
		internal static readonly IEqualityComparer<BoolLiteral> EqualityComparer = new BoolLiteral.BoolLiteralComparer();

		// Token: 0x040018D9 RID: 6361
		internal static readonly IEqualityComparer<BoolLiteral> EqualityIdentifierComparer = new BoolLiteral.IdentifierComparer();

		// Token: 0x02000BAF RID: 2991
		private sealed class BoolLiteralComparer : IEqualityComparer<BoolLiteral>
		{
			// Token: 0x06006752 RID: 26450 RVA: 0x00161899 File Offset: 0x0015FA99
			public bool Equals(BoolLiteral left, BoolLiteral right)
			{
				return left == right || (left != null && right != null && left.IsEqualTo(right));
			}

			// Token: 0x06006753 RID: 26451 RVA: 0x001618B0 File Offset: 0x0015FAB0
			public int GetHashCode(BoolLiteral literal)
			{
				return literal.GetHashCode();
			}
		}

		// Token: 0x02000BB0 RID: 2992
		private sealed class IdentifierComparer : IEqualityComparer<BoolLiteral>
		{
			// Token: 0x06006755 RID: 26453 RVA: 0x001618C0 File Offset: 0x0015FAC0
			public bool Equals(BoolLiteral left, BoolLiteral right)
			{
				return left == right || (left != null && right != null && left.IsIdentifierEqualTo(right));
			}

			// Token: 0x06006756 RID: 26454 RVA: 0x001618D7 File Offset: 0x0015FAD7
			public int GetHashCode(BoolLiteral literal)
			{
				return literal.GetIdentifierHash();
			}
		}
	}
}
