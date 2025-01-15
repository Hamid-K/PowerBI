using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005AA RID: 1450
	internal abstract class MemberRestriction : BoolLiteral
	{
		// Token: 0x06004679 RID: 18041 RVA: 0x000F8DB4 File Offset: 0x000F6FB4
		protected MemberRestriction(MemberProjectedSlot slot, Constant value)
			: this(slot, new Constant[] { value })
		{
		}

		// Token: 0x0600467A RID: 18042 RVA: 0x000F8DC7 File Offset: 0x000F6FC7
		protected MemberRestriction(MemberProjectedSlot slot, IEnumerable<Constant> values)
		{
			this.m_restrictedMemberSlot = slot;
			this.m_domain = new Domain(values, values);
		}

		// Token: 0x0600467B RID: 18043 RVA: 0x000F8DE3 File Offset: 0x000F6FE3
		protected MemberRestriction(MemberProjectedSlot slot, Domain domain)
		{
			this.m_restrictedMemberSlot = slot;
			this.m_domain = domain;
			this.m_isComplete = true;
		}

		// Token: 0x0600467C RID: 18044 RVA: 0x000F8E00 File Offset: 0x000F7000
		protected MemberRestriction(MemberProjectedSlot slot, IEnumerable<Constant> values, IEnumerable<Constant> possibleValues)
			: this(slot, new Domain(values, possibleValues))
		{
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x0600467D RID: 18045 RVA: 0x000F8E10 File Offset: 0x000F7010
		internal bool IsComplete
		{
			get
			{
				return this.m_isComplete;
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x0600467E RID: 18046 RVA: 0x000F8E18 File Offset: 0x000F7018
		internal MemberProjectedSlot RestrictedMemberSlot
		{
			get
			{
				return this.m_restrictedMemberSlot;
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x0600467F RID: 18047 RVA: 0x000F8E20 File Offset: 0x000F7020
		internal Domain Domain
		{
			get
			{
				return this.m_domain;
			}
		}

		// Token: 0x06004680 RID: 18048 RVA: 0x000F8E28 File Offset: 0x000F7028
		internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> GetDomainBoolExpression(MemberDomainMap domainMap)
		{
			TermExpr<DomainConstraint<BoolLiteral, Constant>> termExpr;
			if (domainMap != null)
			{
				IEnumerable<Constant> domain = domainMap.GetDomain(this.m_restrictedMemberSlot.MemberPath);
				termExpr = BoolLiteral.MakeTermExpression(this, domain, this.m_domain.Values);
			}
			else
			{
				termExpr = BoolLiteral.MakeTermExpression(this, this.m_domain.AllPossibleValues, this.m_domain.Values);
			}
			return termExpr;
		}

		// Token: 0x06004681 RID: 18049
		internal abstract MemberRestriction CreateCompleteMemberRestriction(IEnumerable<Constant> possibleValues);

		// Token: 0x06004682 RID: 18050 RVA: 0x000F8E80 File Offset: 0x000F7080
		internal override void GetRequiredSlots(MemberProjectionIndex projectedSlotMap, bool[] requiredSlots)
		{
			MemberPath memberPath = this.RestrictedMemberSlot.MemberPath;
			int num = projectedSlotMap.IndexOf(memberPath);
			requiredSlots[num] = true;
		}

		// Token: 0x06004683 RID: 18051 RVA: 0x000F8EA8 File Offset: 0x000F70A8
		protected override bool IsEqualTo(BoolLiteral right)
		{
			MemberRestriction memberRestriction = right as MemberRestriction;
			return memberRestriction != null && (this == memberRestriction || (ProjectedSlot.EqualityComparer.Equals(this.m_restrictedMemberSlot, memberRestriction.m_restrictedMemberSlot) && this.m_domain.IsEqualTo(memberRestriction.m_domain)));
		}

		// Token: 0x06004684 RID: 18052 RVA: 0x000F8EF2 File Offset: 0x000F70F2
		public override int GetHashCode()
		{
			return ProjectedSlot.EqualityComparer.GetHashCode(this.m_restrictedMemberSlot) ^ this.m_domain.GetHash();
		}

		// Token: 0x06004685 RID: 18053 RVA: 0x000F8F10 File Offset: 0x000F7110
		protected override bool IsIdentifierEqualTo(BoolLiteral right)
		{
			MemberRestriction memberRestriction = right as MemberRestriction;
			return memberRestriction != null && (this == memberRestriction || ProjectedSlot.EqualityComparer.Equals(this.m_restrictedMemberSlot, memberRestriction.m_restrictedMemberSlot));
		}

		// Token: 0x06004686 RID: 18054 RVA: 0x000F8F45 File Offset: 0x000F7145
		protected override int GetIdentifierHash()
		{
			return ProjectedSlot.EqualityComparer.GetHashCode(this.m_restrictedMemberSlot);
		}

		// Token: 0x06004687 RID: 18055 RVA: 0x000F8F57 File Offset: 0x000F7157
		internal override StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return this.AsEsql(builder, blockAlias, skipIsNotNull);
		}

		// Token: 0x06004688 RID: 18056 RVA: 0x000F8F62 File Offset: 0x000F7162
		internal override StringBuilder AsNegatedUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			builder.Append("NOT(");
			builder = this.AsUserString(builder, blockAlias, skipIsNotNull);
			builder.Append(")");
			return builder;
		}

		// Token: 0x0400191D RID: 6429
		private readonly MemberProjectedSlot m_restrictedMemberSlot;

		// Token: 0x0400191E RID: 6430
		private readonly Domain m_domain;

		// Token: 0x0400191F RID: 6431
		private readonly bool m_isComplete;
	}
}
