using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Mapping.ViewGeneration.Structures;
using System.Globalization;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.QueryRewriting
{
	// Token: 0x02000584 RID: 1412
	internal class FragmentQuery : ITileQuery
	{
		// Token: 0x17000D7D RID: 3453
		// (get) Token: 0x0600443C RID: 17468 RVA: 0x000EFE8E File Offset: 0x000EE08E
		public HashSet<MemberPath> Attributes
		{
			get
			{
				return this.m_attributes;
			}
		}

		// Token: 0x17000D7E RID: 3454
		// (get) Token: 0x0600443D RID: 17469 RVA: 0x000EFE96 File Offset: 0x000EE096
		public BoolExpression Condition
		{
			get
			{
				return this.m_condition;
			}
		}

		// Token: 0x0600443E RID: 17470 RVA: 0x000EFEA0 File Offset: 0x000EE0A0
		public static FragmentQuery Create(BoolExpression fromVariable, CellQuery cellQuery)
		{
			BoolExpression boolExpression = cellQuery.WhereClause;
			boolExpression = boolExpression.MakeCopy();
			boolExpression.ExpensiveSimplify();
			return new FragmentQuery(null, fromVariable, new HashSet<MemberPath>(cellQuery.GetProjectedMembers()), boolExpression);
		}

		// Token: 0x0600443F RID: 17471 RVA: 0x000EFED4 File Offset: 0x000EE0D4
		public static FragmentQuery Create(string label, RoleBoolean roleBoolean, CellQuery cellQuery)
		{
			BoolExpression boolExpression = cellQuery.WhereClause.Create(roleBoolean);
			boolExpression = BoolExpression.CreateAnd(new BoolExpression[] { boolExpression, cellQuery.WhereClause });
			boolExpression = boolExpression.MakeCopy();
			boolExpression.ExpensiveSimplify();
			return new FragmentQuery(label, null, new HashSet<MemberPath>(), boolExpression);
		}

		// Token: 0x06004440 RID: 17472 RVA: 0x000EFF21 File Offset: 0x000EE121
		public static FragmentQuery Create(IEnumerable<MemberPath> attrs, BoolExpression whereClause)
		{
			return new FragmentQuery(null, null, attrs, whereClause);
		}

		// Token: 0x06004441 RID: 17473 RVA: 0x000EFF2C File Offset: 0x000EE12C
		public static FragmentQuery Create(BoolExpression whereClause)
		{
			return new FragmentQuery(null, null, new MemberPath[0], whereClause);
		}

		// Token: 0x06004442 RID: 17474 RVA: 0x000EFF3C File Offset: 0x000EE13C
		internal FragmentQuery(string label, BoolExpression fromVariable, IEnumerable<MemberPath> attrs, BoolExpression condition)
		{
			this.m_label = label;
			this.m_fromVariable = fromVariable;
			this.m_condition = condition;
			this.m_attributes = new HashSet<MemberPath>(attrs);
		}

		// Token: 0x17000D7F RID: 3455
		// (get) Token: 0x06004443 RID: 17475 RVA: 0x000EFF66 File Offset: 0x000EE166
		public BoolExpression FromVariable
		{
			get
			{
				return this.m_fromVariable;
			}
		}

		// Token: 0x17000D80 RID: 3456
		// (get) Token: 0x06004444 RID: 17476 RVA: 0x000EFF70 File Offset: 0x000EE170
		public string Description
		{
			get
			{
				string text = this.m_label;
				if (text == null && this.m_fromVariable != null)
				{
					text = this.m_fromVariable.ToString();
				}
				return text;
			}
		}

		// Token: 0x06004445 RID: 17477 RVA: 0x000EFF9C File Offset: 0x000EE19C
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MemberPath memberPath in this.Attributes)
			{
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(',');
				}
				stringBuilder.Append(memberPath);
			}
			if (this.Description != null && this.Description != stringBuilder.ToString())
			{
				return string.Format(CultureInfo.InvariantCulture, "{0}: [{1} where {2}]", new object[] { this.Description, stringBuilder, this.Condition });
			}
			return string.Format(CultureInfo.InvariantCulture, "[{0} where {1}]", new object[] { stringBuilder, this.Condition });
		}

		// Token: 0x06004446 RID: 17478 RVA: 0x000F0074 File Offset: 0x000EE274
		internal static BoolExpression CreateMemberCondition(MemberPath path, Constant domainValue, MemberDomainMap domainMap)
		{
			if (domainValue is TypeConstant)
			{
				return BoolExpression.CreateLiteral(new TypeRestriction(new MemberProjectedSlot(path), new Domain(domainValue, domainMap.GetDomain(path))), domainMap);
			}
			return BoolExpression.CreateLiteral(new ScalarRestriction(new MemberProjectedSlot(path), new Domain(domainValue, domainMap.GetDomain(path))), domainMap);
		}

		// Token: 0x06004447 RID: 17479 RVA: 0x000F00C6 File Offset: 0x000EE2C6
		internal static IEqualityComparer<FragmentQuery> GetEqualityComparer(FragmentQueryProcessor qp)
		{
			return new FragmentQuery.FragmentQueryEqualityComparer(qp);
		}

		// Token: 0x04001895 RID: 6293
		private readonly BoolExpression m_fromVariable;

		// Token: 0x04001896 RID: 6294
		private readonly string m_label;

		// Token: 0x04001897 RID: 6295
		private readonly HashSet<MemberPath> m_attributes;

		// Token: 0x04001898 RID: 6296
		private readonly BoolExpression m_condition;

		// Token: 0x02000B8B RID: 2955
		private class FragmentQueryEqualityComparer : IEqualityComparer<FragmentQuery>
		{
			// Token: 0x0600668D RID: 26253 RVA: 0x0015FBF3 File Offset: 0x0015DDF3
			internal FragmentQueryEqualityComparer(FragmentQueryProcessor qp)
			{
				this._qp = qp;
			}

			// Token: 0x0600668E RID: 26254 RVA: 0x0015FC02 File Offset: 0x0015DE02
			public bool Equals(FragmentQuery x, FragmentQuery y)
			{
				return x.Attributes.SetEquals(y.Attributes) && this._qp.IsEquivalentTo(x, y);
			}

			// Token: 0x0600668F RID: 26255 RVA: 0x0015FC28 File Offset: 0x0015DE28
			public int GetHashCode(FragmentQuery q)
			{
				int num = 0;
				foreach (MemberPath memberPath in q.Attributes)
				{
					num ^= MemberPath.EqualityComparer.GetHashCode(memberPath);
				}
				int num2 = 0;
				int num3 = 0;
				foreach (MemberRestriction memberRestriction in q.Condition.MemberRestrictions)
				{
					num2 ^= MemberPath.EqualityComparer.GetHashCode(memberRestriction.RestrictedMemberSlot.MemberPath);
					foreach (Constant constant in memberRestriction.Domain.Values)
					{
						num3 ^= Constant.EqualityComparer.GetHashCode(constant);
					}
				}
				return num * 13 + num2 * 7 + num3;
			}

			// Token: 0x04002E0F RID: 11791
			private readonly FragmentQueryProcessor _qp;
		}
	}
}
