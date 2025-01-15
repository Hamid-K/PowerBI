using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Common.SyntaxTree.Sql;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common.Creators
{
	// Token: 0x02001199 RID: 4505
	internal abstract class NoOutputClauseDbAstCreator : DbAstCreator
	{
		// Token: 0x06007704 RID: 30468 RVA: 0x0004FA88 File Offset: 0x0004DC88
		protected NoOutputClauseDbAstCreator(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, DbEnvironment environment)
			: base(expression, cursor, environment)
		{
		}

		// Token: 0x06007705 RID: 30469 RVA: 0x0019D328 File Offset: 0x0019B528
		protected override Dictionary<FunctionValue, Func<IInvocationExpression, SqlStatement>> GetStatementFunctions()
		{
			return new Dictionary<FunctionValue, Func<IInvocationExpression, SqlStatement>>
			{
				{
					ActionModule.Action.Bind,
					new Func<IInvocationExpression, SqlStatement>(base.CreateBind)
				},
				{
					ActionModule.TableAction.InsertRows,
					new Func<IInvocationExpression, SqlStatement>(this.CreateInsertRows)
				},
				{
					ActionModule.TableAction.UpdateRows,
					new Func<IInvocationExpression, SqlStatement>(this.CreateUpdateRows)
				},
				{
					ActionModule.TableAction.DeleteRows,
					new Func<IInvocationExpression, SqlStatement>(this.CreateDeleteRows)
				}
			};
		}

		// Token: 0x06007706 RID: 30470 RVA: 0x0019D39C File Offset: 0x0019B59C
		private OutputClause CreateOutputClause(Alias alias, TableTypeValue tableType, TableReference table, Condition whereClause, StatementType statementType)
		{
			if (!this.countOnly)
			{
				return new SelectOutputClause(tableType.ItemType.Fields.Keys.Select((string c) => new SelectItem(new ColumnReference(null, Alias.NewNativeAlias(c)))).ToList<SelectItem>(), table, whereClause, statementType);
			}
			return OutputClause.Null;
		}

		// Token: 0x06007707 RID: 30471 RVA: 0x0019D3FC File Offset: 0x0019B5FC
		protected override SqlStatement CreateInsertStatement(TableReference table, Alias alias, TableTypeValue tableType, List<ColumnReference> columnList, List<IList<ScalarExpression>> values)
		{
			if (this.countOnly)
			{
				return new SqlInsertStatement(table, values, OutputClause.Null, columnList);
			}
			TableKey primaryKey = tableType.GetPrimaryKey();
			if (primaryKey == null || primaryKey.Columns.Length == 0)
			{
				throw new NotSupportedException();
			}
			Keys keys = tableType.ItemType.Fields.Keys;
			int[] array = new int[primaryKey.Columns.Length];
			for (int i = 0; i < primaryKey.Columns.Length; i++)
			{
				string text = keys[primaryKey.Columns[i]];
				bool flag = false;
				for (int j = 0; j < columnList.Count; j++)
				{
					if (columnList[j].Name.Name == text)
					{
						array[i] = j;
						flag = true;
						break;
					}
				}
				if (!flag)
				{
					throw new NotSupportedException();
				}
			}
			Condition condition = null;
			foreach (IList<ScalarExpression> list in values)
			{
				Condition condition2 = null;
				foreach (int num in array)
				{
					Condition condition3 = new BinaryLogicalOperation(base.ConvertToScalar(columnList[num]), BinaryLogicalOperator.Equals, base.ConvertToScalar(list[num]));
					condition2 = ((condition2 != null) ? new ConditionOperation(condition2, ConditionOperator.And, condition3) : condition3);
				}
				condition = ((condition != null) ? new ConditionOperation(condition, ConditionOperator.Or, condition2) : condition2);
			}
			return new SqlInsertStatement(table, values, this.CreateOutputClause(alias, tableType, table, condition, StatementType.Insert), columnList);
		}

		// Token: 0x06007708 RID: 30472 RVA: 0x0019D590 File Offset: 0x0019B790
		protected override SqlStatement CreateInsertStatement(TableReference table, Alias alias, TableTypeValue tableType, List<ColumnReference> columnList, SqlQueryExpression sourceQuery)
		{
			return new SqlInsertStatement(table, sourceQuery, this.CreateOutputClause(alias, tableType, table, null, StatementType.Insert), columnList);
		}

		// Token: 0x06007709 RID: 30473 RVA: 0x0019D5A8 File Offset: 0x0019B7A8
		protected override SqlStatement CreateUpdateStatement(TableReference table, Alias alias, TableTypeValue tableType, List<SqlColumnUpdate> updates, Condition whereClause)
		{
			if (!this.countOnly && whereClause == null && updates != null)
			{
				Condition condition = null;
				foreach (SqlColumnUpdate sqlColumnUpdate in updates)
				{
					Condition condition2 = new BinaryLogicalOperation(sqlColumnUpdate.Column, BinaryLogicalOperator.Equals, sqlColumnUpdate.Expression);
					condition = ((condition != null) ? new ConditionOperation(condition, ConditionOperator.And, condition2) : condition2);
				}
				return new SqlUpdateStatement(table, updates, this.CreateOutputClause(alias, tableType, table, condition, StatementType.Update), whereClause);
			}
			return new SqlUpdateStatement(table, updates, this.CreateOutputClause(alias, tableType, table, whereClause, StatementType.Update), whereClause);
		}

		// Token: 0x0600770A RID: 30474 RVA: 0x0019D650 File Offset: 0x0019B850
		protected override SqlStatement CreateDeleteStatement(TableReference table, Alias alias, TableTypeValue tableType, Condition whereClause)
		{
			return new SqlDeleteStatement(table, this.CreateOutputClause(alias, tableType, table, whereClause, StatementType.Delete), whereClause);
		}
	}
}
