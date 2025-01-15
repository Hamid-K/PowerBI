using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Library.Table;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x020010B9 RID: 4281
	internal class EnvironmentStatementBuilder : StatementBuilder
	{
		// Token: 0x0600703A RID: 28730 RVA: 0x00181B8C File Offset: 0x0017FD8C
		public EnvironmentStatementBuilder(TableValue table, EnvironmentBase environment, ValueBuilderBase valueBuilder, IExpression syntaxTree)
			: base(table)
		{
			this.environment = environment;
			this.valueBuilder = valueBuilder;
			this.syntaxTree = syntaxTree;
		}

		// Token: 0x17001F95 RID: 8085
		// (get) Token: 0x0600703B RID: 28731 RVA: 0x00181BAB File Offset: 0x0017FDAB
		public IExpression SyntaxTree
		{
			get
			{
				return this.syntaxTree;
			}
		}

		// Token: 0x17001F96 RID: 8086
		// (get) Token: 0x0600703C RID: 28732 RVA: 0x00181BB3 File Offset: 0x0017FDB3
		public EnvironmentBase Environment
		{
			get
			{
				return this.environment;
			}
		}

		// Token: 0x17001F97 RID: 8087
		// (get) Token: 0x0600703D RID: 28733 RVA: 0x00181BBB File Offset: 0x0017FDBB
		protected override int InsertBatchSize
		{
			get
			{
				return this.environment.InsertBatchSize;
			}
		}

		// Token: 0x17001F98 RID: 8088
		// (get) Token: 0x0600703E RID: 28734 RVA: 0x00181BC8 File Offset: 0x0017FDC8
		protected override IEngineHost Host
		{
			get
			{
				return this.environment.Host;
			}
		}

		// Token: 0x0600703F RID: 28735 RVA: 0x00181BD5 File Offset: 0x0017FDD5
		protected override void VerifyActionPermitted()
		{
			this.environment.VerifyActionPermitted();
		}

		// Token: 0x06007040 RID: 28736 RVA: 0x00181BE2 File Offset: 0x0017FDE2
		protected override Keys GetKeys()
		{
			return this.valueBuilder.Type.AsTableType.ItemType.Fields.Keys;
		}

		// Token: 0x06007041 RID: 28737 RVA: 0x00181C04 File Offset: 0x0017FE04
		protected override bool TryGetInsertAction(Query rowsToInsert, bool countOnlyTable, out ActionValue action)
		{
			IExpression expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(ActionModule.TableAction.InsertRows), this.syntaxTree, this.GetTableQuery(rowsToInsert));
			if (this.TryCreateAction(expression, countOnlyTable, "Insert", out action))
			{
				DbEnvironment dbEnv = this.environment as DbEnvironment;
				if (dbEnv != null)
				{
					action = action.Bind(StatementBuilder.BindToResult((Value result) => ActionValue.New(delegate
					{
						long largeCount = ((TableValue)result).LargeCount;
						dbEnv.HostProgressService.RecordRowsWritten(largeCount);
						return result;
					})));
				}
				return true;
			}
			action = null;
			return false;
		}

		// Token: 0x06007042 RID: 28738 RVA: 0x00181C80 File Offset: 0x0017FE80
		protected override bool TryGetBatchInsertAction(TableValue batch, bool countOnlyTable, out ActionValue action)
		{
			long rowsRead = 0L;
			DbEnvironment dbEnv = this.environment as DbEnvironment;
			if (dbEnv != null)
			{
				batch = new EnvironmentStatementBuilder.RowCountingTableValue(batch, delegate(long rr)
				{
					rowsRead = rr;
				});
			}
			IExpression expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(ActionModule.TableAction.InsertRows), this.syntaxTree, new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(StatementBuilder.BatchedRows), new ConstantExpressionSyntaxNode(batch)));
			if (this.TryCreateAction(expression, countOnlyTable, "BatchInsert", out action))
			{
				if (dbEnv != null)
				{
					action = action.Bind(StatementBuilder.BindToResult((Value result) => ActionValue.New(delegate
					{
						dbEnv.HostProgressService.RecordRowsWritten(rowsRead);
						return result;
					})));
				}
				return true;
			}
			action = null;
			return false;
		}

		// Token: 0x06007043 RID: 28739 RVA: 0x00181D2C File Offset: 0x0017FF2C
		protected override bool TryGetUpdateAction(FunctionValue filter, ColumnUpdates columnUpdates, bool countOnlyTable, out ActionValue action)
		{
			IListExpression listExpression;
			if (this.TryGetUpdateExpressions(columnUpdates, out listExpression))
			{
				IExpression expression = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(ActionModule.TableAction.UpdateRows), new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.SelectRows), this.syntaxTree, filter.Expression), listExpression);
				return this.TryCreateAction(expression, countOnlyTable, "Update", out action);
			}
			action = null;
			return false;
		}

		// Token: 0x06007044 RID: 28740 RVA: 0x00181D88 File Offset: 0x0017FF88
		private bool TryGetUpdateExpressions(ColumnUpdates columnUpdates, out IListExpression updateList)
		{
			Keys keys = this.GetKeys();
			List<IExpression> list = new List<IExpression>(columnUpdates.Updates.Count);
			List<FunctionValue> list2 = new List<FunctionValue>(columnUpdates.Updates.Count);
			foreach (KeyValuePair<int, FunctionValue> keyValuePair in columnUpdates.Updates)
			{
				list.Add(new ConstantExpressionSyntaxNode(TextValue.New(keys[keyValuePair.Key])));
				list2.Add(keyValuePair.Value);
			}
			List<IExpression> list3 = new List<IExpression>();
			for (int i = 0; i < list.Count; i++)
			{
				IFunctionExpression functionExpression = NormalizationVisitor.Normalize(list2[i].Expression, true) as IFunctionExpression;
				if (functionExpression == null)
				{
					updateList = null;
					return false;
				}
				list3.Add(new ListExpressionSyntaxNode(new IExpression[]
				{
					list[i],
					functionExpression
				}));
			}
			updateList = new ListExpressionSyntaxNode(list3);
			return true;
		}

		// Token: 0x06007045 RID: 28741 RVA: 0x00181E90 File Offset: 0x00180090
		protected override bool TryGetDeleteAction(FunctionValue filter, bool countOnlyTable, out ActionValue action)
		{
			IExpression expression = new InvocationExpressionSyntaxNode1(new ConstantExpressionSyntaxNode(ActionModule.TableAction.DeleteRows), new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(TableModule.Table.SelectRows), this.syntaxTree, filter.Expression));
			return this.TryCreateAction(expression, countOnlyTable, "Delete", out action);
		}

		// Token: 0x06007046 RID: 28742 RVA: 0x00181ED6 File Offset: 0x001800D6
		private bool TryCompileStatement(Query query, IExpression expression, string statementType, out ActionValue action)
		{
			if (this.environment.IsStatementSupported(expression))
			{
				action = this.environment.CompileStatement(query, expression, statementType);
				return true;
			}
			action = null;
			return false;
		}

		// Token: 0x06007047 RID: 28743 RVA: 0x00181EFE File Offset: 0x001800FE
		protected override IEnumerator<IValueReference> GetValueEnumerator()
		{
			return this.valueBuilder.GetEnumerator();
		}

		// Token: 0x06007048 RID: 28744 RVA: 0x00181F0C File Offset: 0x0018010C
		private IExpression GetTableQuery(Query query)
		{
			TableValue tableValue = new QueryTableValue(query);
			Value value;
			if (query.TryInvokeAsArgument(QueryResultTableValue.ExtractionFunction, new Value[] { tableValue }, 0, out value))
			{
				QueryResultTableValue queryResultTableValue = value as QueryResultTableValue;
				if (queryResultTableValue != null && this.environment.OtherCanFoldToThis(queryResultTableValue.Environment))
				{
					return queryResultTableValue.SyntaxTree;
				}
			}
			return new ConstantExpressionSyntaxNode(tableValue);
		}

		// Token: 0x06007049 RID: 28745 RVA: 0x00181F64 File Offset: 0x00180164
		private bool TryCreateAction(IExpression expression, bool countOnlyTable, string statementType, out ActionValue action)
		{
			IExpression expression2 = new InvocationExpressionSyntaxNode2(new ConstantExpressionSyntaxNode(ActionModule.Action.Bind), expression, new ConstantExpressionSyntaxNode(ReturnRowCountFunctionValue.Instance));
			if (countOnlyTable && this.TryCompileStatement(this.table.Query, expression2, statementType, out action))
			{
				action = action.Bind(StatementBuilder.ReturnScalar);
				action = action.Bind(new ReturnTypedTableFromCountFunctionValue(this.table.Type.AsTableType));
				return true;
			}
			return this.TryCompileStatement(this.table.Query, expression, statementType, out action);
		}

		// Token: 0x04003DFE RID: 15870
		private readonly EnvironmentBase environment;

		// Token: 0x04003DFF RID: 15871
		private readonly ValueBuilderBase valueBuilder;

		// Token: 0x04003E00 RID: 15872
		private readonly IExpression syntaxTree;

		// Token: 0x020010BA RID: 4282
		private class RowCountingTableValue : DelegatingTableValue
		{
			// Token: 0x0600704A RID: 28746 RVA: 0x00181FED File Offset: 0x001801ED
			public RowCountingTableValue(TableValue table, Action<long> onRowsRead)
				: base(table)
			{
				this.onRowsRead = onRowsRead;
			}

			// Token: 0x0600704B RID: 28747 RVA: 0x00181FFD File Offset: 0x001801FD
			public override IEnumerator<IValueReference> GetEnumerator()
			{
				long rowsRead = 0L;
				try
				{
					foreach (IValueReference valueReference in base.Table)
					{
						long num = rowsRead;
						rowsRead = num + 1L;
						yield return valueReference;
					}
					IEnumerator<IValueReference> enumerator = null;
				}
				finally
				{
					this.onRowsRead(rowsRead);
				}
				yield break;
				yield break;
			}

			// Token: 0x04003E01 RID: 15873
			private readonly Action<long> onRowsRead;
		}
	}
}
