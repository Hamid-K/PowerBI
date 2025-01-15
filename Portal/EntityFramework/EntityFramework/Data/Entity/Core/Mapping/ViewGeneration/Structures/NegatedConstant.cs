using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Resources;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x020005AB RID: 1451
	internal sealed class NegatedConstant : Constant
	{
		// Token: 0x06004689 RID: 18057 RVA: 0x000F8F88 File Offset: 0x000F7188
		internal NegatedConstant(IEnumerable<Constant> values)
		{
			this.m_negatedDomain = new Set<Constant>(values, Constant.EqualityComparer);
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x0600468A RID: 18058 RVA: 0x000F8FA1 File Offset: 0x000F71A1
		internal IEnumerable<Constant> Elements
		{
			get
			{
				return this.m_negatedDomain;
			}
		}

		// Token: 0x0600468B RID: 18059 RVA: 0x000F8FA9 File Offset: 0x000F71A9
		internal bool Contains(Constant constant)
		{
			return this.m_negatedDomain.Contains(constant);
		}

		// Token: 0x0600468C RID: 18060 RVA: 0x000F8FB7 File Offset: 0x000F71B7
		internal override bool IsNull()
		{
			return false;
		}

		// Token: 0x0600468D RID: 18061 RVA: 0x000F8FBA File Offset: 0x000F71BA
		internal override bool IsNotNull()
		{
			return this == Constant.NotNull || (this.m_negatedDomain.Count == 1 && this.m_negatedDomain.Contains(Constant.Null));
		}

		// Token: 0x0600468E RID: 18062 RVA: 0x000F8FE6 File Offset: 0x000F71E6
		internal override bool IsUndefined()
		{
			return false;
		}

		// Token: 0x0600468F RID: 18063 RVA: 0x000F8FE9 File Offset: 0x000F71E9
		internal override bool HasNotNull()
		{
			return this.m_negatedDomain.Contains(Constant.Null);
		}

		// Token: 0x06004690 RID: 18064 RVA: 0x000F8FFC File Offset: 0x000F71FC
		public override int GetHashCode()
		{
			int num = 0;
			foreach (Constant constant in this.m_negatedDomain)
			{
				num ^= Constant.EqualityComparer.GetHashCode(constant);
			}
			return num;
		}

		// Token: 0x06004691 RID: 18065 RVA: 0x000F905C File Offset: 0x000F725C
		protected override bool IsEqualTo(Constant right)
		{
			NegatedConstant negatedConstant = right as NegatedConstant;
			return negatedConstant != null && this.m_negatedDomain.SetEquals(negatedConstant.m_negatedDomain);
		}

		// Token: 0x06004692 RID: 18066 RVA: 0x000F9086 File Offset: 0x000F7286
		internal override StringBuilder AsEsql(StringBuilder builder, MemberPath outputMember, string blockAlias)
		{
			return null;
		}

		// Token: 0x06004693 RID: 18067 RVA: 0x000F9089 File Offset: 0x000F7289
		internal override DbExpression AsCqt(DbExpression row, MemberPath outputMember)
		{
			return null;
		}

		// Token: 0x06004694 RID: 18068 RVA: 0x000F908C File Offset: 0x000F728C
		internal StringBuilder AsEsql(StringBuilder builder, string blockAlias, IEnumerable<Constant> constants, MemberPath outputMember, bool skipIsNotNull)
		{
			return this.ToStringHelper(builder, blockAlias, constants, outputMember, skipIsNotNull, false);
		}

		// Token: 0x06004695 RID: 18069 RVA: 0x000F909C File Offset: 0x000F729C
		internal DbExpression AsCqt(DbExpression row, IEnumerable<Constant> constants, MemberPath outputMember, bool skipIsNotNull)
		{
			DbExpression cqt = null;
			this.AsCql(delegate
			{
				cqt = DbExpressionBuilder.True;
			}, delegate
			{
				cqt = outputMember.AsCqt(row).IsNull().Not();
			}, delegate(Constant constant)
			{
				DbExpression dbExpression = outputMember.AsCqt(row).NotEqual(constant.AsCqt(row, outputMember));
				if (cqt != null)
				{
					cqt = cqt.And(dbExpression);
					return;
				}
				cqt = dbExpression;
			}, constants, outputMember, skipIsNotNull);
			return cqt;
		}

		// Token: 0x06004696 RID: 18070 RVA: 0x000F90FD File Offset: 0x000F72FD
		internal StringBuilder AsUserString(StringBuilder builder, string blockAlias, IEnumerable<Constant> constants, MemberPath outputMember, bool skipIsNotNull)
		{
			return this.ToStringHelper(builder, blockAlias, constants, outputMember, skipIsNotNull, true);
		}

		// Token: 0x06004697 RID: 18071 RVA: 0x000F9110 File Offset: 0x000F7310
		private void AsCql(Action trueLiteral, Action varIsNotNull, Action<Constant> varNotEqualsTo, IEnumerable<Constant> constants, MemberPath outputMember, bool skipIsNotNull)
		{
			bool isNullable = outputMember.IsNullable;
			Set<Constant> set = new Set<Constant>(this.Elements, Constant.EqualityComparer);
			foreach (Constant constant in constants)
			{
				if (!constant.Equals(this))
				{
					set.Remove(constant);
				}
			}
			if (set.Count == 0)
			{
				trueLiteral();
				return;
			}
			bool flag = set.Contains(Constant.Null);
			set.Remove(Constant.Null);
			if (flag || (isNullable && !skipIsNotNull))
			{
				varIsNotNull();
			}
			foreach (Constant constant2 in set)
			{
				varNotEqualsTo(constant2);
			}
		}

		// Token: 0x06004698 RID: 18072 RVA: 0x000F91F4 File Offset: 0x000F73F4
		private StringBuilder ToStringHelper(StringBuilder builder, string blockAlias, IEnumerable<Constant> constants, MemberPath outputMember, bool skipIsNotNull, bool userString)
		{
			bool anyAdded = false;
			this.AsCql(delegate
			{
				builder.Append("true");
			}, delegate
			{
				if (userString)
				{
					outputMember.ToCompactString(builder, blockAlias);
					builder.Append(" is not NULL");
				}
				else
				{
					outputMember.AsEsql(builder, blockAlias);
					builder.Append(" IS NOT NULL");
				}
				anyAdded = true;
			}, delegate(Constant constant)
			{
				if (anyAdded)
				{
					builder.Append(" AND ");
				}
				anyAdded = true;
				if (userString)
				{
					outputMember.ToCompactString(builder, blockAlias);
					builder.Append(" <>");
					constant.ToCompactString(builder);
					return;
				}
				outputMember.AsEsql(builder, blockAlias);
				builder.Append(" <>");
				constant.AsEsql(builder, outputMember, blockAlias);
			}, constants, outputMember, skipIsNotNull);
			return builder;
		}

		// Token: 0x06004699 RID: 18073 RVA: 0x000F9268 File Offset: 0x000F7468
		internal override string ToUserString()
		{
			if (this.IsNotNull())
			{
				return Strings.ViewGen_NotNull;
			}
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = true;
			foreach (Constant constant in this.m_negatedDomain)
			{
				if (this.m_negatedDomain.Count <= 1 || !constant.IsNull())
				{
					if (!flag)
					{
						stringBuilder.Append(Strings.ViewGen_CommaBlank);
					}
					flag = false;
					stringBuilder.Append(constant.ToUserString());
				}
			}
			StringBuilder stringBuilder2 = new StringBuilder();
			stringBuilder2.Append(Strings.ViewGen_NegatedCellConstant(stringBuilder.ToString()));
			return stringBuilder2.ToString();
		}

		// Token: 0x0600469A RID: 18074 RVA: 0x000F931C File Offset: 0x000F751C
		internal override void ToCompactString(StringBuilder builder)
		{
			if (this.IsNotNull())
			{
				builder.Append("NOT_NULL");
				return;
			}
			builder.Append("NOT(");
			StringUtil.ToCommaSeparatedStringSorted(builder, this.m_negatedDomain);
			builder.Append(")");
		}

		// Token: 0x04001920 RID: 6432
		private readonly Set<Constant> m_negatedDomain;
	}
}
