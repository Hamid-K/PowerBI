using System;
using System.Collections.Generic;
using Microsoft.Reporting.QueryDesign.ExpressionTrees.Internal;

namespace Microsoft.Reporting.QueryDesign.ExpressionTrees.Translators.Internal
{
	// Token: 0x02000145 RID: 325
	internal static class DaxStatements
	{
		// Token: 0x06001197 RID: 4503 RVA: 0x00030F97 File Offset: 0x0002F197
		internal static DaxExpression Evaluate(DaxExpression inputTable)
		{
			return DaxExpression.Table(new DaxStatements.DaxStatement("EVALUATE", false).Invoke(new DaxExpression[] { inputTable }), inputTable.ResultColumns, false);
		}

		// Token: 0x06001198 RID: 4504 RVA: 0x00030FBF File Offset: 0x0002F1BF
		internal static DaxExpression Return(DaxExpression input)
		{
			return DaxExpression.ScalarOrTable(new DaxStatements.DaxStatement("RETURN", true).Invoke(new DaxExpression[] { input }), input.ResultColumns);
		}

		// Token: 0x06001199 RID: 4505 RVA: 0x00030FE8 File Offset: 0x0002F1E8
		internal static DaxSortedExpression OrderBy(DaxExpression inputTable, IReadOnlyList<DaxSortItem> sortItems)
		{
			List<string> list = new List<string>();
			foreach (DaxSortItem daxSortItem in sortItems)
			{
				string text = daxSortItem.Column.ToString();
				if (daxSortItem.Direction == SortDirection.Descending)
				{
					text += " DESC";
				}
				list.Add(text);
			}
			DaxStatements.DaxStatement daxStatement = new DaxStatements.DaxStatement("ORDER BY", false);
			return DaxExpression.SortedTable(inputTable.Text + DaxFormat.NewLine + DaxFormat.NewLine + daxStatement.Invoke(list), inputTable.ResultColumns, sortItems);
		}

		// Token: 0x0600119A RID: 4506 RVA: 0x0003109C File Offset: 0x0002F29C
		internal static DaxExpression StartAt(DaxExpression inputTable, IEnumerable<string> startAtArguments)
		{
			DaxStatements.DaxStatement daxStatement = new DaxStatements.DaxStatement("START AT", false);
			string text = inputTable.Text + DaxFormat.NewLine + DaxFormat.NewLine + daxStatement.Invoke(startAtArguments);
			return inputTable.ReplaceText(text);
		}

		// Token: 0x0600119B RID: 4507 RVA: 0x000310D9 File Offset: 0x0002F2D9
		internal static DaxExpression DefineMeasure(DaxMeasureRef measureRef, DaxExpression expression)
		{
			return DaxExpression.Scalar(new DaxStatements.DaxDefine("MEASURE").Invoke(measureRef.ToString(), expression, false));
		}

		// Token: 0x0600119C RID: 4508 RVA: 0x000310FE File Offset: 0x0002F2FE
		internal static DaxExpression DefineColumn(DaxColumnRef columnRef, DaxExpression expression)
		{
			return DaxExpression.Scalar(new DaxStatements.DaxDefine("COLUMN").Invoke(columnRef.ToString(), expression, false));
		}

		// Token: 0x0600119D RID: 4509 RVA: 0x00031123 File Offset: 0x0002F323
		internal static DaxExpression DefineDataSourceVariable(DaxExpression expression)
		{
			return DaxExpression.Scalar(new DaxStatements.DaxDefine("DATASOURCEVARIABLES").Invoke(expression, false));
		}

		// Token: 0x0600119E RID: 4510 RVA: 0x0003113B File Offset: 0x0002F33B
		internal static DaxExpression DefineMParameters(string parameterName, DaxExpression expression)
		{
			DaxStatements.DaxDefine daxDefine = new DaxStatements.DaxDefine("MPARAMETER");
			parameterName = "'" + parameterName + "'";
			return DaxExpression.Scalar(daxDefine.Invoke(parameterName, expression, false));
		}

		// Token: 0x0600119F RID: 4511 RVA: 0x00031168 File Offset: 0x0002F368
		internal static DaxExpression DefineVariable(string name, DaxExpression expression)
		{
			bool flag = expression.ResultColumns.IsNullOrEmpty<DaxResultColumn>();
			string text = new DaxStatements.DaxDefine("VAR").Invoke(name, expression, flag);
			if (!flag)
			{
				return DaxExpression.Table(text, expression.ResultColumns, false);
			}
			return DaxExpression.Scalar(text);
		}

		// Token: 0x0200038C RID: 908
		private sealed class DaxStatement : DaxFunctionBase
		{
			// Token: 0x06001FBA RID: 8122 RVA: 0x00057412 File Offset: 0x00055612
			internal DaxStatement(string keyword, bool preferSingleLine = false)
				: base(keyword, false)
			{
				this._preferSingleLine = preferSingleLine;
			}

			// Token: 0x06001FBB RID: 8123 RVA: 0x00057424 File Offset: 0x00055624
			protected override string InvokeCore(string formattedArgs, bool multiline)
			{
				if (this._preferSingleLine && formattedArgs.Length + base.Name.Length < 80)
				{
					return base.Name + " " + formattedArgs;
				}
				return string.Concat(new object[]
				{
					base.Name,
					DaxFormat.NewLine,
					'\t',
					formattedArgs
				});
			}

			// Token: 0x040012D5 RID: 4821
			private readonly bool _preferSingleLine;
		}

		// Token: 0x0200038D RID: 909
		private sealed class DaxDefine
		{
			// Token: 0x06001FBC RID: 8124 RVA: 0x0005748B File Offset: 0x0005568B
			internal DaxDefine(string itemType)
			{
				this._itemType = itemType;
			}

			// Token: 0x06001FBD RID: 8125 RVA: 0x0005749A File Offset: 0x0005569A
			public string Invoke(DaxExpression content, bool preferSingleLine = false)
			{
				return this.Invoke(string.Empty, content, preferSingleLine);
			}

			// Token: 0x06001FBE RID: 8126 RVA: 0x000574AC File Offset: 0x000556AC
			public string Invoke(string variableName, DaxExpression content, bool preferSingleLine = false)
			{
				bool flag = true;
				string text = string.Empty;
				if (!string.IsNullOrEmpty(variableName))
				{
					text = " ";
				}
				if (preferSingleLine)
				{
					flag = this._itemType.Length + text.Length + variableName.Length + content.Text.Length + 3 > 80;
				}
				if (flag)
				{
					return string.Concat(new object[]
					{
						this._itemType,
						text,
						variableName,
						" = ",
						DaxFormat.NewLine,
						'\t',
						DaxFormat.IncreaseIndent(content.Text)
					});
				}
				return string.Concat(new string[] { this._itemType, text, variableName, " = ", content.Text });
			}

			// Token: 0x040012D6 RID: 4822
			private readonly string _itemType;
		}
	}
}
