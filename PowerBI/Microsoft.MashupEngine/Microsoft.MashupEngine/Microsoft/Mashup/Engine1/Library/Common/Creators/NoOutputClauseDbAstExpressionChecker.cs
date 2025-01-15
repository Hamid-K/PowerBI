using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Typeflow;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common.Creators
{
	// Token: 0x0200119B RID: 4507
	internal abstract class NoOutputClauseDbAstExpressionChecker : DbAstExpressionChecker
	{
		// Token: 0x0600770E RID: 30478 RVA: 0x00050416 File Offset: 0x0004E616
		protected NoOutputClauseDbAstExpressionChecker(IExpression expression, LogicalAstToCachedTypeflowResultCursor cursor, EnvironmentBase externalEnvironment)
			: base(expression, cursor, externalEnvironment)
		{
		}

		// Token: 0x0600770F RID: 30479 RVA: 0x0019D674 File Offset: 0x0019B874
		protected override Dictionary<FunctionValue, Action<IInvocationExpression>> GetStatementFunctions()
		{
			return new Dictionary<FunctionValue, Action<IInvocationExpression>>
			{
				{
					ActionModule.Action.Bind,
					new Action<IInvocationExpression>(base.CheckBind)
				},
				{
					ActionModule.TableAction.InsertRows,
					new Action<IInvocationExpression>(base.CheckInsertRows)
				},
				{
					ActionModule.TableAction.UpdateRows,
					new Action<IInvocationExpression>(base.CheckUpdateRows)
				},
				{
					ActionModule.TableAction.DeleteRows,
					new Action<IInvocationExpression>(base.CheckDeleteRows)
				}
			};
		}

		// Token: 0x06007710 RID: 30480 RVA: 0x0019D6E4 File Offset: 0x0019B8E4
		protected override bool CanReturnUpdatedRows(TypeValue targetType, TypeValue sourceType = null, bool updatesPrimaryKey = false)
		{
			if (updatesPrimaryKey)
			{
				return false;
			}
			TableTypeValue tableTypeValue = targetType as TableTypeValue;
			TableTypeValue tableTypeValue2 = sourceType as TableTypeValue;
			TableKey primaryKey = tableTypeValue.GetPrimaryKey();
			if (primaryKey == null || primaryKey.Columns == null)
			{
				return false;
			}
			if (tableTypeValue2 != null)
			{
				IEnumerable<string> enumerable = tableTypeValue.ItemType.Fields.Keys.ToList<string>().Where((string t, int i) => primaryKey.Columns.Contains(i)).ToList<string>();
				List<string> list = tableTypeValue2.ItemType.Fields.Keys.ToList<string>();
				return !enumerable.Except(list).Any<string>();
			}
			return true;
		}
	}
}
