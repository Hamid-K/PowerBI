using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Resources;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005B0 RID: 1456
	internal class ScalarRestriction : MemberRestriction
	{
		// Token: 0x060046D3 RID: 18131 RVA: 0x000FA2EE File Offset: 0x000F84EE
		internal ScalarRestriction(MemberPath member, Constant value)
			: base(new MemberProjectedSlot(member), value)
		{
		}

		// Token: 0x060046D4 RID: 18132 RVA: 0x000FA2FD File Offset: 0x000F84FD
		internal ScalarRestriction(MemberPath member, IEnumerable<Constant> values, IEnumerable<Constant> possibleValues)
			: base(new MemberProjectedSlot(member), values, possibleValues)
		{
		}

		// Token: 0x060046D5 RID: 18133 RVA: 0x000FA30D File Offset: 0x000F850D
		internal ScalarRestriction(MemberProjectedSlot slot, Domain domain)
			: base(slot, domain)
		{
		}

		// Token: 0x060046D6 RID: 18134 RVA: 0x000FA318 File Offset: 0x000F8518
		internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> FixRange(Set<Constant> range, MemberDomainMap memberDomainMap)
		{
			IEnumerable<Constant> domain = memberDomainMap.GetDomain(base.RestrictedMemberSlot.MemberPath);
			return new ScalarRestriction(base.RestrictedMemberSlot, new Domain(range, domain)).GetDomainBoolExpression(memberDomainMap);
		}

		// Token: 0x060046D7 RID: 18135 RVA: 0x000FA34F File Offset: 0x000F854F
		internal override BoolLiteral RemapBool(Dictionary<MemberPath, MemberPath> remap)
		{
			return new ScalarRestriction(base.RestrictedMemberSlot.RemapSlot(remap), base.Domain);
		}

		// Token: 0x060046D8 RID: 18136 RVA: 0x000FA368 File Offset: 0x000F8568
		internal override MemberRestriction CreateCompleteMemberRestriction(IEnumerable<Constant> possibleValues)
		{
			return new ScalarRestriction(base.RestrictedMemberSlot, new Domain(base.Domain.Values, possibleValues));
		}

		// Token: 0x060046D9 RID: 18137 RVA: 0x000FA386 File Offset: 0x000F8586
		internal override StringBuilder AsEsql(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return this.ToStringHelper(builder, blockAlias, skipIsNotNull, false);
		}

		// Token: 0x060046DA RID: 18138 RVA: 0x000FA394 File Offset: 0x000F8594
		internal override DbExpression AsCqt(DbExpression row, bool skipIsNotNull)
		{
			DbExpression cqt = null;
			Func<Constant, DbExpression> <>9__4;
			this.AsCql(delegate(NegatedConstant negated, IEnumerable<Constant> domainValues)
			{
				cqt = negated.AsCqt(row, domainValues, this.RestrictedMemberSlot.MemberPath, skipIsNotNull);
			}, delegate(Set<Constant> domainValues)
			{
				cqt = this.RestrictedMemberSlot.MemberPath.AsCqt(row);
				if (domainValues.Count == 1)
				{
					cqt = cqt.Equal(domainValues.Single<Constant>().AsCqt(row, this.RestrictedMemberSlot.MemberPath));
					return;
				}
				Func<Constant, DbExpression> func;
				if ((func = <>9__4) == null)
				{
					func = (<>9__4 = (Constant c) => cqt.Equal(c.AsCqt(row, this.RestrictedMemberSlot.MemberPath)));
				}
				List<DbExpression> list = domainValues.Select(func).ToList<DbExpression>();
				cqt = Helpers.BuildBalancedTreeInPlace<DbExpression>(list, (DbExpression prev, DbExpression next) => prev.Or(next));
			}, delegate
			{
				DbExpression dbExpression = this.RestrictedMemberSlot.MemberPath.AsCqt(row).IsNull().Not();
				cqt = ((cqt != null) ? cqt.And(dbExpression) : dbExpression);
			}, delegate
			{
				DbExpression dbExpression2 = this.RestrictedMemberSlot.MemberPath.AsCqt(row).IsNull();
				cqt = ((cqt != null) ? dbExpression2.Or(cqt) : dbExpression2);
			}, skipIsNotNull);
			return cqt;
		}

		// Token: 0x060046DB RID: 18139 RVA: 0x000FA405 File Offset: 0x000F8605
		internal override StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			return this.ToStringHelper(builder, blockAlias, skipIsNotNull, true);
		}

		// Token: 0x060046DC RID: 18140 RVA: 0x000FA414 File Offset: 0x000F8614
		private StringBuilder ToStringHelper(StringBuilder inputBuilder, string blockAlias, bool skipIsNotNull, bool userString)
		{
			StringBuilder builder = new StringBuilder();
			this.AsCql(delegate(NegatedConstant negated, IEnumerable<Constant> domainValues)
			{
				if (userString)
				{
					negated.AsUserString(builder, blockAlias, domainValues, this.RestrictedMemberSlot.MemberPath, skipIsNotNull);
					return;
				}
				negated.AsEsql(builder, blockAlias, domainValues, this.RestrictedMemberSlot.MemberPath, skipIsNotNull);
			}, delegate(Set<Constant> domainValues)
			{
				this.RestrictedMemberSlot.MemberPath.AsEsql(builder, blockAlias);
				if (domainValues.Count != 1)
				{
					builder.Append(" IN {");
					bool flag = true;
					foreach (Constant constant in domainValues)
					{
						if (!flag)
						{
							builder.Append(", ");
						}
						if (userString)
						{
							constant.ToCompactString(builder);
						}
						else
						{
							constant.AsEsql(builder, this.RestrictedMemberSlot.MemberPath, blockAlias);
						}
						flag = false;
					}
					builder.Append('}');
					return;
				}
				builder.Append(" = ");
				if (userString)
				{
					domainValues.Single<Constant>().ToCompactString(builder);
					return;
				}
				domainValues.Single<Constant>().AsEsql(builder, this.RestrictedMemberSlot.MemberPath, blockAlias);
			}, delegate
			{
				bool flag2 = builder.Length == 0;
				builder.Insert(0, '(');
				if (!flag2)
				{
					builder.Append(" AND ");
				}
				if (userString)
				{
					this.RestrictedMemberSlot.MemberPath.ToCompactString(builder, Strings.ViewGen_EntityInstanceToken);
					builder.Append(" is not NULL)");
					return;
				}
				this.RestrictedMemberSlot.MemberPath.AsEsql(builder, blockAlias);
				builder.Append(" IS NOT NULL)");
			}, delegate
			{
				bool flag3 = builder.Length == 0;
				StringBuilder stringBuilder = new StringBuilder();
				if (!flag3)
				{
					stringBuilder.Append('(');
				}
				if (userString)
				{
					this.RestrictedMemberSlot.MemberPath.ToCompactString(stringBuilder, blockAlias);
					stringBuilder.Append(" is NULL");
				}
				else
				{
					this.RestrictedMemberSlot.MemberPath.AsEsql(stringBuilder, blockAlias);
					stringBuilder.Append(" IS NULL");
				}
				if (!flag3)
				{
					stringBuilder.Append(" OR ");
				}
				builder.Insert(0, stringBuilder.ToString());
				if (!flag3)
				{
					builder.Append(')');
				}
			}, skipIsNotNull);
			inputBuilder.Append(builder);
			return inputBuilder;
		}

		// Token: 0x060046DD RID: 18141 RVA: 0x000FA49C File Offset: 0x000F869C
		private void AsCql(Action<NegatedConstant, IEnumerable<Constant>> negatedConstantAsCql, Action<Set<Constant>> varInDomain, Action varIsNotNull, Action varIsNull, bool skipIsNotNull)
		{
			NegatedConstant negatedConstant = (NegatedConstant)base.Domain.Values.FirstOrDefault((Constant c) => c is NegatedConstant);
			if (negatedConstant != null)
			{
				negatedConstantAsCql(negatedConstant, base.Domain.Values);
				return;
			}
			Set<Constant> set = new Set<Constant>(base.Domain.Values, Constant.EqualityComparer);
			bool flag = false;
			if (set.Contains(Constant.Null))
			{
				flag = true;
				set.Remove(Constant.Null);
			}
			if (set.Contains(Constant.Undefined))
			{
				flag = true;
				set.Remove(Constant.Undefined);
			}
			bool flag2 = !skipIsNotNull && base.RestrictedMemberSlot.MemberPath.IsNullable;
			if (set.Count > 0)
			{
				varInDomain(set);
			}
			if (flag2)
			{
				varIsNotNull();
			}
			if (flag)
			{
				varIsNull();
			}
		}

		// Token: 0x060046DE RID: 18142 RVA: 0x000FA579 File Offset: 0x000F8779
		internal override void ToCompactString(StringBuilder builder)
		{
			base.RestrictedMemberSlot.ToCompactString(builder);
			builder.Append(" IN (");
			StringUtil.ToCommaSeparatedStringSorted(builder, base.Domain.Values);
			builder.Append(")");
		}
	}
}
