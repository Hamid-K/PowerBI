using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.Common.CommandTrees.ExpressionBuilder;
using System.Data.Entity.Core.Common.Utils;
using System.Data.Entity.Core.Mapping.ViewGeneration.CqlGeneration;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;
using System.Text;

namespace System.Data.Entity.Core.Mapping.ViewGeneration.Structures
{
	// Token: 0x02000597 RID: 1431
	internal sealed class CaseStatement : InternalBase
	{
		// Token: 0x06004535 RID: 17717 RVA: 0x000F437C File Offset: 0x000F257C
		internal CaseStatement(MemberPath memberPath)
		{
			this.m_memberPath = memberPath;
			this.m_clauses = new List<CaseStatement.WhenThen>();
		}

		// Token: 0x17000DA4 RID: 3492
		// (get) Token: 0x06004536 RID: 17718 RVA: 0x000F4396 File Offset: 0x000F2596
		internal MemberPath MemberPath
		{
			get
			{
				return this.m_memberPath;
			}
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06004537 RID: 17719 RVA: 0x000F439E File Offset: 0x000F259E
		internal List<CaseStatement.WhenThen> Clauses
		{
			get
			{
				return this.m_clauses;
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06004538 RID: 17720 RVA: 0x000F43A6 File Offset: 0x000F25A6
		internal ProjectedSlot ElseValue
		{
			get
			{
				return this.m_elseValue;
			}
		}

		// Token: 0x06004539 RID: 17721 RVA: 0x000F43B0 File Offset: 0x000F25B0
		internal CaseStatement DeepQualify(CqlBlock block)
		{
			CaseStatement caseStatement = new CaseStatement(this.m_memberPath);
			foreach (CaseStatement.WhenThen whenThen in this.m_clauses)
			{
				CaseStatement.WhenThen whenThen2 = whenThen.ReplaceWithQualifiedSlot(block);
				caseStatement.m_clauses.Add(whenThen2);
			}
			if (this.m_elseValue != null)
			{
				caseStatement.m_elseValue = this.m_elseValue.DeepQualify(block);
			}
			caseStatement.m_simplified = this.m_simplified;
			return caseStatement;
		}

		// Token: 0x0600453A RID: 17722 RVA: 0x000F4444 File Offset: 0x000F2644
		internal void AddWhenThen(BoolExpression condition, ProjectedSlot value)
		{
			condition.ExpensiveSimplify();
			this.m_clauses.Add(new CaseStatement.WhenThen(condition, value));
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x0600453B RID: 17723 RVA: 0x000F4460 File Offset: 0x000F2660
		internal bool DependsOnMemberValue
		{
			get
			{
				if (this.m_elseValue is MemberProjectedSlot)
				{
					return true;
				}
				using (List<CaseStatement.WhenThen>.Enumerator enumerator = this.m_clauses.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.Value is MemberProjectedSlot)
						{
							return true;
						}
					}
				}
				return false;
			}
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x0600453C RID: 17724 RVA: 0x000F44D0 File Offset: 0x000F26D0
		internal IEnumerable<EdmType> InstantiatedTypes
		{
			get
			{
				using (List<CaseStatement.WhenThen>.Enumerator enumerator = this.m_clauses.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						EdmType edmType;
						if (CaseStatement.TryGetInstantiatedType(enumerator.Current.Value, out edmType))
						{
							yield return edmType;
						}
					}
				}
				List<CaseStatement.WhenThen>.Enumerator enumerator = default(List<CaseStatement.WhenThen>.Enumerator);
				EdmType edmType2;
				if (CaseStatement.TryGetInstantiatedType(this.m_elseValue, out edmType2))
				{
					yield return edmType2;
				}
				yield break;
				yield break;
			}
		}

		// Token: 0x0600453D RID: 17725 RVA: 0x000F44E0 File Offset: 0x000F26E0
		private static bool TryGetInstantiatedType(ProjectedSlot slot, out EdmType type)
		{
			type = null;
			ConstantProjectedSlot constantProjectedSlot = slot as ConstantProjectedSlot;
			if (constantProjectedSlot != null)
			{
				TypeConstant typeConstant = constantProjectedSlot.CellConstant as TypeConstant;
				if (typeConstant != null)
				{
					type = typeConstant.EdmType;
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600453E RID: 17726 RVA: 0x000F4514 File Offset: 0x000F2714
		internal void Simplify()
		{
			if (this.m_simplified)
			{
				return;
			}
			List<CaseStatement.WhenThen> list = new List<CaseStatement.WhenThen>();
			bool flag = false;
			foreach (CaseStatement.WhenThen whenThen in this.m_clauses)
			{
				ConstantProjectedSlot constantProjectedSlot = whenThen.Value as ConstantProjectedSlot;
				if (constantProjectedSlot != null && (constantProjectedSlot.CellConstant.IsNull() || constantProjectedSlot.CellConstant.IsUndefined()))
				{
					flag = true;
				}
				else
				{
					list.Add(whenThen);
					if (whenThen.Condition.IsTrue)
					{
						break;
					}
				}
			}
			if (flag && list.Count == 0)
			{
				this.m_elseValue = new ConstantProjectedSlot(Constant.Null);
			}
			if (list.Count > 0 && !flag)
			{
				int num = list.Count - 1;
				this.m_elseValue = list[num].Value;
				list.RemoveAt(num);
			}
			this.m_clauses = list;
			this.m_simplified = true;
		}

		// Token: 0x0600453F RID: 17727 RVA: 0x000F4614 File Offset: 0x000F2814
		internal StringBuilder AsEsql(StringBuilder builder, IEnumerable<WithRelationship> withRelationships, string blockAlias, int indentLevel)
		{
			if (this.Clauses.Count == 0)
			{
				CaseStatement.CaseSlotValueAsEsql(builder, this.ElseValue, this.MemberPath, blockAlias, withRelationships, indentLevel);
				return builder;
			}
			builder.Append("CASE");
			foreach (CaseStatement.WhenThen whenThen in this.Clauses)
			{
				StringUtil.IndentNewLine(builder, indentLevel + 2);
				builder.Append("WHEN ");
				whenThen.Condition.AsEsql(builder, blockAlias);
				builder.Append(" THEN ");
				CaseStatement.CaseSlotValueAsEsql(builder, whenThen.Value, this.MemberPath, blockAlias, withRelationships, indentLevel + 2);
			}
			if (this.ElseValue != null)
			{
				StringUtil.IndentNewLine(builder, indentLevel + 2);
				builder.Append("ELSE ");
				CaseStatement.CaseSlotValueAsEsql(builder, this.ElseValue, this.MemberPath, blockAlias, withRelationships, indentLevel + 2);
			}
			StringUtil.IndentNewLine(builder, indentLevel + 1);
			builder.Append("END");
			return builder;
		}

		// Token: 0x06004540 RID: 17728 RVA: 0x000F472C File Offset: 0x000F292C
		internal DbExpression AsCqt(DbExpression row, IEnumerable<WithRelationship> withRelationships)
		{
			List<DbExpression> list = new List<DbExpression>();
			List<DbExpression> list2 = new List<DbExpression>();
			foreach (CaseStatement.WhenThen whenThen in this.Clauses)
			{
				list.Add(whenThen.Condition.AsCqt(row));
				list2.Add(CaseStatement.CaseSlotValueAsCqt(row, whenThen.Value, this.MemberPath, withRelationships));
			}
			DbExpression dbExpression = ((this.ElseValue != null) ? CaseStatement.CaseSlotValueAsCqt(row, this.ElseValue, this.MemberPath, withRelationships) : Constant.Null.AsCqt(row, this.MemberPath));
			if (this.Clauses.Count > 0)
			{
				return DbExpressionBuilder.Case(list, list2, dbExpression);
			}
			return dbExpression;
		}

		// Token: 0x06004541 RID: 17729 RVA: 0x000F47FC File Offset: 0x000F29FC
		private static StringBuilder CaseSlotValueAsEsql(StringBuilder builder, ProjectedSlot slot, MemberPath outputMember, string blockAlias, IEnumerable<WithRelationship> withRelationships, int indentLevel)
		{
			slot.AsEsql(builder, outputMember, blockAlias, 1);
			CaseStatement.WithRelationshipsClauseAsEsql(builder, withRelationships, blockAlias, indentLevel, slot);
			return builder;
		}

		// Token: 0x06004542 RID: 17730 RVA: 0x000F4816 File Offset: 0x000F2A16
		private static void WithRelationshipsClauseAsEsql(StringBuilder builder, IEnumerable<WithRelationship> withRelationships, string blockAlias, int indentLevel, ProjectedSlot slot)
		{
			bool first = true;
			CaseStatement.WithRelationshipsClauseAsCql(delegate(WithRelationship withRelationship)
			{
				if (first)
				{
					builder.Append(" WITH ");
					first = false;
				}
				withRelationship.AsEsql(builder, blockAlias, indentLevel);
			}, withRelationships, slot);
		}

		// Token: 0x06004543 RID: 17731 RVA: 0x000F484C File Offset: 0x000F2A4C
		private static DbExpression CaseSlotValueAsCqt(DbExpression row, ProjectedSlot slot, MemberPath outputMember, IEnumerable<WithRelationship> withRelationships)
		{
			DbExpression dbExpression = slot.AsCqt(row, outputMember);
			return CaseStatement.WithRelationshipsClauseAsCqt(row, dbExpression, withRelationships, slot);
		}

		// Token: 0x06004544 RID: 17732 RVA: 0x000F4870 File Offset: 0x000F2A70
		private static DbExpression WithRelationshipsClauseAsCqt(DbExpression row, DbExpression slotValueExpr, IEnumerable<WithRelationship> withRelationships, ProjectedSlot slot)
		{
			List<DbRelatedEntityRef> relatedEntityRefs = new List<DbRelatedEntityRef>();
			CaseStatement.WithRelationshipsClauseAsCql(delegate(WithRelationship withRelationship)
			{
				relatedEntityRefs.Add(withRelationship.AsCqt(row));
			}, withRelationships, slot);
			if (relatedEntityRefs.Count > 0)
			{
				DbNewInstanceExpression dbNewInstanceExpression = slotValueExpr as DbNewInstanceExpression;
				return DbExpressionBuilder.CreateNewEntityWithRelationshipsExpression((EntityType)dbNewInstanceExpression.ResultType.EdmType, dbNewInstanceExpression.Arguments, relatedEntityRefs);
			}
			return slotValueExpr;
		}

		// Token: 0x06004545 RID: 17733 RVA: 0x000F48E0 File Offset: 0x000F2AE0
		private static void WithRelationshipsClauseAsCql(Action<WithRelationship> emitWithRelationship, IEnumerable<WithRelationship> withRelationships, ProjectedSlot slot)
		{
			if (withRelationships != null && withRelationships.Count<WithRelationship>() > 0)
			{
				EdmType edmType = ((slot as ConstantProjectedSlot).CellConstant as TypeConstant).EdmType;
				foreach (WithRelationship withRelationship in withRelationships)
				{
					if (withRelationship.FromEndEntityType.IsAssignableFrom(edmType))
					{
						emitWithRelationship(withRelationship);
					}
				}
			}
		}

		// Token: 0x06004546 RID: 17734 RVA: 0x000F4958 File Offset: 0x000F2B58
		internal override void ToCompactString(StringBuilder builder)
		{
			builder.AppendLine("CASE");
			foreach (CaseStatement.WhenThen whenThen in this.m_clauses)
			{
				builder.Append(" WHEN ");
				whenThen.Condition.ToCompactString(builder);
				builder.Append(" THEN ");
				whenThen.Value.ToCompactString(builder);
				builder.AppendLine();
			}
			if (this.m_elseValue != null)
			{
				builder.Append(" ELSE ");
				this.m_elseValue.ToCompactString(builder);
				builder.AppendLine();
			}
			builder.Append(" END AS ");
			this.m_memberPath.ToCompactString(builder);
		}

		// Token: 0x040018DA RID: 6362
		private readonly MemberPath m_memberPath;

		// Token: 0x040018DB RID: 6363
		private List<CaseStatement.WhenThen> m_clauses;

		// Token: 0x040018DC RID: 6364
		private ProjectedSlot m_elseValue;

		// Token: 0x040018DD RID: 6365
		private bool m_simplified;

		// Token: 0x02000BB1 RID: 2993
		internal sealed class WhenThen : InternalBase
		{
			// Token: 0x06006758 RID: 26456 RVA: 0x001618E7 File Offset: 0x0015FAE7
			internal WhenThen(BoolExpression condition, ProjectedSlot value)
			{
				this.m_condition = condition;
				this.m_value = value;
			}

			// Token: 0x17001113 RID: 4371
			// (get) Token: 0x06006759 RID: 26457 RVA: 0x001618FD File Offset: 0x0015FAFD
			internal BoolExpression Condition
			{
				get
				{
					return this.m_condition;
				}
			}

			// Token: 0x17001114 RID: 4372
			// (get) Token: 0x0600675A RID: 26458 RVA: 0x00161905 File Offset: 0x0015FB05
			internal ProjectedSlot Value
			{
				get
				{
					return this.m_value;
				}
			}

			// Token: 0x0600675B RID: 26459 RVA: 0x00161910 File Offset: 0x0015FB10
			internal CaseStatement.WhenThen ReplaceWithQualifiedSlot(CqlBlock block)
			{
				ProjectedSlot projectedSlot = this.m_value.DeepQualify(block);
				return new CaseStatement.WhenThen(this.m_condition, projectedSlot);
			}

			// Token: 0x0600675C RID: 26460 RVA: 0x00161936 File Offset: 0x0015FB36
			internal override void ToCompactString(StringBuilder builder)
			{
				builder.Append("WHEN ");
				this.m_condition.ToCompactString(builder);
				builder.Append("THEN ");
				this.m_value.ToCompactString(builder);
			}

			// Token: 0x04002E74 RID: 11892
			private readonly BoolExpression m_condition;

			// Token: 0x04002E75 RID: 11893
			private readonly ProjectedSlot m_value;
		}
	}
}
