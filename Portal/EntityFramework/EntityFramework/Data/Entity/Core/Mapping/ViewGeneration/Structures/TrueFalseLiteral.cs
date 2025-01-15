using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Linq;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005B1 RID: 1457
	internal abstract class TrueFalseLiteral : BoolLiteral
	{
		// Token: 0x060046DF RID: 18143 RVA: 0x000FA5B0 File Offset: 0x000F87B0
		internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> GetDomainBoolExpression(MemberDomainMap domainMap)
		{
			IEnumerable<Constant> enumerable = new Constant[]
			{
				new ScalarConstant(true)
			};
			Set<Constant> set = new Set<Constant>(new Constant[]
			{
				new ScalarConstant(true),
				new ScalarConstant(false)
			}, Constant.EqualityComparer).MakeReadOnly();
			Set<Constant> set2 = new Set<Constant>(enumerable, Constant.EqualityComparer).MakeReadOnly();
			return BoolLiteral.MakeTermExpression(this, set, set2);
		}

		// Token: 0x060046E0 RID: 18144 RVA: 0x000FA620 File Offset: 0x000F8820
		internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> FixRange(Set<Constant> range, MemberDomainMap memberDomainMap)
		{
			ScalarConstant scalarConstant = (ScalarConstant)range.First<Constant>();
			BoolExpr<DomainConstraint<BoolLiteral, Constant>> boolExpr = this.GetDomainBoolExpression(memberDomainMap);
			if (!(bool)scalarConstant.Value)
			{
				boolExpr = new NotExpr<DomainConstraint<BoolLiteral, Constant>>(boolExpr);
			}
			return boolExpr;
		}
	}
}
