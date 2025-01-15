using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Common.Utils.Boolean;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005B3 RID: 1459
	internal class TypeRestriction : MemberRestriction
	{
		// Token: 0x060046EF RID: 18159 RVA: 0x000FA874 File Offset: 0x000F8A74
		internal TypeRestriction(MemberPath member, IEnumerable<EdmType> values)
			: base(new MemberProjectedSlot(member), TypeRestriction.CreateTypeConstants(values))
		{
		}

		// Token: 0x060046F0 RID: 18160 RVA: 0x000FA888 File Offset: 0x000F8A88
		internal TypeRestriction(MemberPath member, Constant value)
			: base(new MemberProjectedSlot(member), value)
		{
		}

		// Token: 0x060046F1 RID: 18161 RVA: 0x000FA897 File Offset: 0x000F8A97
		internal TypeRestriction(MemberProjectedSlot slot, Domain domain)
			: base(slot, domain)
		{
		}

		// Token: 0x060046F2 RID: 18162 RVA: 0x000FA8A4 File Offset: 0x000F8AA4
		internal override BoolExpr<DomainConstraint<BoolLiteral, Constant>> FixRange(Set<Constant> range, MemberDomainMap memberDomainMap)
		{
			IEnumerable<Constant> domain = memberDomainMap.GetDomain(base.RestrictedMemberSlot.MemberPath);
			return new TypeRestriction(base.RestrictedMemberSlot, new Domain(range, domain)).GetDomainBoolExpression(memberDomainMap);
		}

		// Token: 0x060046F3 RID: 18163 RVA: 0x000FA8DB File Offset: 0x000F8ADB
		internal override BoolLiteral RemapBool(Dictionary<MemberPath, MemberPath> remap)
		{
			return new TypeRestriction(base.RestrictedMemberSlot.RemapSlot(remap), base.Domain);
		}

		// Token: 0x060046F4 RID: 18164 RVA: 0x000FA8F4 File Offset: 0x000F8AF4
		internal override MemberRestriction CreateCompleteMemberRestriction(IEnumerable<Constant> possibleValues)
		{
			return new TypeRestriction(base.RestrictedMemberSlot, new Domain(base.Domain.Values, possibleValues));
		}

		// Token: 0x060046F5 RID: 18165 RVA: 0x000FA914 File Offset: 0x000F8B14
		internal override StringBuilder AsEsql(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			if (base.Domain.Count > 1)
			{
				builder.Append('(');
			}
			bool flag = true;
			foreach (Constant constant in base.Domain.Values)
			{
				TypeConstant typeConstant = constant as TypeConstant;
				if (!flag)
				{
					builder.Append(" OR ");
				}
				flag = false;
				if (Helper.IsRefType(base.RestrictedMemberSlot.MemberPath.EdmType))
				{
					builder.Append("Deref(");
					base.RestrictedMemberSlot.MemberPath.AsEsql(builder, blockAlias);
					builder.Append(')');
				}
				else
				{
					base.RestrictedMemberSlot.MemberPath.AsEsql(builder, blockAlias);
				}
				if (constant.IsNull())
				{
					builder.Append(" IS NULL");
				}
				else
				{
					builder.Append(" IS OF (ONLY ");
					CqlWriter.AppendEscapedTypeName(builder, typeConstant.EdmType);
					builder.Append(')');
				}
			}
			if (base.Domain.Count > 1)
			{
				builder.Append(')');
			}
			return builder;
		}

		// Token: 0x060046F6 RID: 18166 RVA: 0x000FAA38 File Offset: 0x000F8C38
		internal override DbExpression AsCqt(DbExpression row, bool skipIsNotNull)
		{
			DbExpression cqt = base.RestrictedMemberSlot.MemberPath.AsCqt(row);
			if (Helper.IsRefType(base.RestrictedMemberSlot.MemberPath.EdmType))
			{
				cqt = cqt.Deref();
			}
			if (base.Domain.Count == 1)
			{
				cqt = cqt.IsOfOnly(TypeUsage.Create(((TypeConstant)base.Domain.Values.Single<Constant>()).EdmType));
			}
			else
			{
				List<DbExpression> list = base.Domain.Values.Select((Constant t) => cqt.IsOfOnly(TypeUsage.Create(((TypeConstant)t).EdmType))).ToList<DbExpression>();
				cqt = Helpers.BuildBalancedTreeInPlace<DbExpression>(list, (DbExpression prev, DbExpression next) => prev.Or(next));
			}
			return cqt;
		}

		// Token: 0x060046F7 RID: 18167 RVA: 0x000FAB20 File Offset: 0x000F8D20
		internal override StringBuilder AsUserString(StringBuilder builder, string blockAlias, bool skipIsNotNull)
		{
			if (Helper.IsRefType(base.RestrictedMemberSlot.MemberPath.EdmType))
			{
				builder.Append("Deref(");
				base.RestrictedMemberSlot.MemberPath.AsEsql(builder, blockAlias);
				builder.Append(')');
			}
			else
			{
				base.RestrictedMemberSlot.MemberPath.AsEsql(builder, blockAlias);
			}
			if (base.Domain.Count > 1)
			{
				builder.Append(" is a (");
			}
			else
			{
				builder.Append(" is type ");
			}
			bool flag = true;
			foreach (Constant constant in base.Domain.Values)
			{
				TypeConstant typeConstant = constant as TypeConstant;
				if (!flag)
				{
					builder.Append(" OR ");
				}
				if (constant.IsNull())
				{
					builder.Append(" NULL");
				}
				else
				{
					CqlWriter.AppendEscapedTypeName(builder, typeConstant.EdmType);
				}
				flag = false;
			}
			if (base.Domain.Count > 1)
			{
				builder.Append(')');
			}
			return builder;
		}

		// Token: 0x060046F8 RID: 18168 RVA: 0x000FAC3C File Offset: 0x000F8E3C
		private static IEnumerable<Constant> CreateTypeConstants(IEnumerable<EdmType> types)
		{
			foreach (EdmType edmType in types)
			{
				if (edmType == null)
				{
					yield return Constant.Null;
				}
				else
				{
					yield return new TypeConstant(edmType);
				}
			}
			IEnumerator<EdmType> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060046F9 RID: 18169 RVA: 0x000FAC4C File Offset: 0x000F8E4C
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.Append("type(");
			base.RestrictedMemberSlot.ToCompactString(builder);
			builder.Append(") IN (");
			StringUtil.ToCommaSeparatedStringSorted(builder, base.Domain.Values);
			builder.Append(")");
		}
	}
}
