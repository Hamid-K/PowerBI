using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.Action;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001135 RID: 4405
	internal abstract class StatementBuilder
	{
		// Token: 0x06007354 RID: 29524 RVA: 0x0018D169 File Offset: 0x0018B369
		public static FunctionValue BindToResult(Func<Value, ActionValue> binding)
		{
			return new StatementBuilder.BindToResultFunctionValue(binding);
		}

		// Token: 0x06007355 RID: 29525 RVA: 0x0018D171 File Offset: 0x0018B371
		protected StatementBuilder(TableValue table)
		{
			this.table = table;
		}

		// Token: 0x17002034 RID: 8244
		// (get) Token: 0x06007356 RID: 29526
		protected abstract IEngineHost Host { get; }

		// Token: 0x17002035 RID: 8245
		// (get) Token: 0x06007357 RID: 29527
		protected abstract int InsertBatchSize { get; }

		// Token: 0x06007358 RID: 29528
		protected abstract void VerifyActionPermitted();

		// Token: 0x06007359 RID: 29529
		protected abstract Keys GetKeys();

		// Token: 0x0600735A RID: 29530
		protected abstract IEnumerator<IValueReference> GetValueEnumerator();

		// Token: 0x0600735B RID: 29531
		protected abstract bool TryGetInsertAction(Query rowsToInsert, bool countOnlyTable, out ActionValue action);

		// Token: 0x0600735C RID: 29532
		protected abstract bool TryGetBatchInsertAction(TableValue batch, bool countOnlyTable, out ActionValue action);

		// Token: 0x0600735D RID: 29533
		protected abstract bool TryGetUpdateAction(FunctionValue filter, ColumnUpdates columnUpdates, bool countOnlyTable, out ActionValue action);

		// Token: 0x0600735E RID: 29534
		protected abstract bool TryGetDeleteAction(FunctionValue filter, bool countOnlyTable, out ActionValue action);

		// Token: 0x17002036 RID: 8246
		// (get) Token: 0x0600735F RID: 29535 RVA: 0x0018D180 File Offset: 0x0018B380
		private Compiler Compiler
		{
			get
			{
				if (this.compiler == null)
				{
					this.compiler = new Compiler(CompileOptions.None);
				}
				return this.compiler;
			}
		}

		// Token: 0x06007360 RID: 29536 RVA: 0x0018D19C File Offset: 0x0018B39C
		public ActionValue InsertRows(Query rowsToInsert)
		{
			this.VerifyActionPermitted();
			rowsToInsert = rowsToInsert.QueryDomain.Optimize(rowsToInsert);
			return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.InsertRows(rowsToInsert, countOnlyTable));
		}

		// Token: 0x06007361 RID: 29537 RVA: 0x0018D1F0 File Offset: 0x0018B3F0
		private ActionValue InsertRows(Query rowsToInsert, bool countOnlyTable)
		{
			ActionValue actionValue;
			if (this.TryGetInsertAction(rowsToInsert, countOnlyTable, out actionValue))
			{
				return actionValue.ClearDataCache(this.Host);
			}
			this.CheckInsertRowsType(rowsToInsert, countOnlyTable);
			return this.InsertRowsNonFolding(rowsToInsert, countOnlyTable).ClearDataCache(this.Host);
		}

		// Token: 0x06007362 RID: 29538 RVA: 0x0018D234 File Offset: 0x0018B434
		private void CheckInsertRowsType(Query rowsToInsert, bool countOnlyTable)
		{
			TableValue tableValue = new QueryTableValue(rowsToInsert);
			this.GetBatchInsertAction(tableValue.Type.AsTableType, new List<IValueReference>(), countOnlyTable);
		}

		// Token: 0x06007363 RID: 29539 RVA: 0x0018D260 File Offset: 0x0018B460
		protected virtual ActionValue InsertRowsNonFolding(Query rowsToInsert, bool countOnlyTable)
		{
			return ActionValue.New(ListValue.New(this.GetInsertActions(rowsToInsert, countOnlyTable)));
		}

		// Token: 0x06007364 RID: 29540 RVA: 0x0018D274 File Offset: 0x0018B474
		private IEnumerable<IValueReference> GetInsertActions(Query rowsToInsert, bool countOnlyTable)
		{
			yield return ActionModule.Action.Return.Invoke(ListValue.Empty.ToTable(this.table.Type.AsTableType));
			List<IValueReference> batch = new List<IValueReference>();
			TableValue tableToInsert = new QueryTableValue(rowsToInsert);
			foreach (IValueReference valueReference in tableToInsert)
			{
				batch.Add(valueReference);
				if (batch.Count >= this.InsertBatchSize)
				{
					yield return this.GetBatchInsertAction(tableToInsert.Type.AsTableType, batch, countOnlyTable);
					batch.Clear();
				}
			}
			IEnumerator<IValueReference> enumerator = null;
			if (batch.Count > 0)
			{
				yield return this.GetBatchInsertAction(tableToInsert.Type.AsTableType, batch, countOnlyTable);
			}
			yield break;
			yield break;
		}

		// Token: 0x06007365 RID: 29541 RVA: 0x0018D294 File Offset: 0x0018B494
		private FunctionValue GetBatchInsertAction(TableTypeValue tableType, List<IValueReference> batch, bool countOnlyTable)
		{
			TableValue tableValue = new ListTableValue(ListValue.New(batch), tableType);
			ActionValue actionValue;
			if (this.TryGetBatchInsertAction(tableValue, countOnlyTable, out actionValue))
			{
				return ActionHelpers.NewCombineActionResultsFunction(actionValue);
			}
			throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, this.table, null);
		}

		// Token: 0x06007366 RID: 29542 RVA: 0x0007D355 File Offset: 0x0007B555
		protected virtual bool TryGetBulkCopyAction(Query rowsToInsert, out ActionValue action)
		{
			action = null;
			return false;
		}

		// Token: 0x06007367 RID: 29543 RVA: 0x0018D2D2 File Offset: 0x0018B4D2
		public ActionValue UpdateRows(ColumnUpdates columnUpdates, FunctionValue selector)
		{
			this.VerifyActionPermitted();
			return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.UpdateRows(columnUpdates, selector, countOnlyTable));
		}

		// Token: 0x06007368 RID: 29544 RVA: 0x0018D304 File Offset: 0x0018B504
		private ActionValue UpdateRows(ColumnUpdates columnUpdates, FunctionValue selector, bool countOnlyTable)
		{
			ActionValue actionValue;
			if (this.TryGetUpdateAction(selector, columnUpdates, countOnlyTable, out actionValue))
			{
				return actionValue.ClearDataCache(this.Host);
			}
			TableKey key = this.GetKey();
			IFunctionTypeExpression functionTypeExpression = this.CreateFilterFunctionType();
			IExpression[] array = this.CreateKeyColumnReferences(functionTypeExpression.Parameters[0].Identifier, key);
			List<IValueReference> list = new List<IValueReference>();
			list.Add(ActionModule.Action.Return.Invoke(TableValue.Empty));
			foreach (IValueReference valueReference in this.GetValues(selector))
			{
				RecordValue asRecord = valueReference.Value.AsRecord;
				IExpression expression = this.CreateRowCondition(key, array, asRecord);
				IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(functionTypeExpression, expression);
				IDictionary<int, FunctionValue> dictionary = new Dictionary<int, FunctionValue>();
				foreach (KeyValuePair<int, FunctionValue> keyValuePair in columnUpdates.Updates)
				{
					Value value = keyValuePair.Value.Invoke(asRecord);
					IFunctionExpression functionExpression2 = new FunctionExpressionSyntaxNode(functionTypeExpression, new ConstantExpressionSyntaxNode(value));
					dictionary[keyValuePair.Key] = this.Compiler.ToFunction(functionExpression2);
				}
				if (!this.TryGetUpdateAction(this.Compiler.ToFunction(functionExpression), new ColumnUpdates(dictionary), countOnlyTable, out actionValue))
				{
					throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, this.table, null);
				}
				list.Add(ActionHelpers.NewCombineActionResultsFunction(actionValue));
			}
			return ActionValue.New(ListValue.New(list)).ClearDataCache(this.Host);
		}

		// Token: 0x06007369 RID: 29545 RVA: 0x0018D4A8 File Offset: 0x0018B6A8
		public ActionValue DeleteRows(FunctionValue selector)
		{
			this.VerifyActionPermitted();
			return CountOnlyTableBindingActionValue.New((bool countOnlyTable) => this.DeleteRows(selector, countOnlyTable));
		}

		// Token: 0x0600736A RID: 29546 RVA: 0x0018D4D4 File Offset: 0x0018B6D4
		private ActionValue DeleteRows(FunctionValue selector, bool countOnlyTable)
		{
			ActionValue actionValue;
			if (this.TryGetDeleteAction(selector, countOnlyTable, out actionValue))
			{
				return actionValue.ClearDataCache(this.Host);
			}
			TableKey key = this.GetKey();
			IFunctionTypeExpression functionTypeExpression = this.CreateFilterFunctionType();
			IExpression[] array = this.CreateKeyColumnReferences(functionTypeExpression.Parameters[0].Identifier, key);
			IExpression expression = null;
			foreach (IValueReference valueReference in this.GetValues(selector))
			{
				RecordValue asRecord = valueReference.Value.AsRecord;
				IExpression expression2 = this.CreateRowCondition(key, array, asRecord);
				if (expression == null)
				{
					expression = expression2;
				}
				else
				{
					expression = BinaryExpressionSyntaxNode.New(BinaryOperator2.Or, expression, expression2, TokenRange.Null);
				}
			}
			if (expression == null)
			{
				return ActionModule.Action.Return.Invoke(new CountAndTypeTableValue(0L, this.table.Type.AsTableType)).AsAction.ClearDataCache(this.Host);
			}
			IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(functionTypeExpression, expression);
			if (this.TryGetDeleteAction(this.Compiler.ToFunction(functionExpression), countOnlyTable, out actionValue))
			{
				return actionValue.ClearDataCache(this.Host);
			}
			throw ValueException.NewDataSourceError<Message0>(Strings.Value_UpdateNotSupported, this.table, null);
		}

		// Token: 0x0600736B RID: 29547 RVA: 0x0018D60C File Offset: 0x0018B80C
		private IEnumerable<IValueReference> GetValues(FunctionValue selector)
		{
			using (IEnumerator<IValueReference> enumerator = this.GetValueEnumerator())
			{
				while (enumerator.MoveNext())
				{
					IValueReference valueReference = enumerator.Current;
					Value value = selector.Invoke(valueReference.Value.AsRecord);
					if (!value.IsNull && value.AsBoolean)
					{
						yield return enumerator.Current;
					}
				}
			}
			IEnumerator<IValueReference> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x0600736C RID: 29548 RVA: 0x0018D624 File Offset: 0x0018B824
		private TableKey GetKey()
		{
			TableTypeValue asTableType = this.table.Type.AsTableType;
			TableKey tableKey = asTableType.GetPrimaryKey() ?? asTableType.TableKeys.FirstOrDefault<TableKey>();
			if (tableKey == null || tableKey.Columns.Length == 0)
			{
				throw ValueException.NewDataSourceError<Message0>(Strings.Table_UpdateNotSupportedWithoutKey, this.table, null);
			}
			return tableKey;
		}

		// Token: 0x0600736D RID: 29549 RVA: 0x0018D677 File Offset: 0x0018B877
		private IFunctionTypeExpression CreateFilterFunctionType()
		{
			return new FunctionTypeSyntaxNode(null, new IParameter[]
			{
				new ParameterSyntaxNode(Identifier.New(), null)
			}, 1);
		}

		// Token: 0x0600736E RID: 29550 RVA: 0x0018D694 File Offset: 0x0018B894
		private IExpression[] CreateKeyColumnReferences(Identifier rowIdentifier, TableKey key)
		{
			IExpression[] array = new IExpression[key.Columns.Length];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = new RequiredFieldAccessExpressionSyntaxNode(new InclusiveIdentifierExpressionSyntaxNode(rowIdentifier), Identifier.New(this.GetKeys()[key.Columns[i]]));
			}
			return array;
		}

		// Token: 0x0600736F RID: 29551 RVA: 0x0018D6E4 File Offset: 0x0018B8E4
		private IExpression CreateRowCondition(TableKey key, IExpression[] columnRefs, RecordValue row)
		{
			IExpression expression = null;
			for (int i = 0; i < key.Columns.Length; i++)
			{
				IExpression expression2 = BinaryExpressionSyntaxNode.New(BinaryOperator2.Equals, columnRefs[i], new ConstantExpressionSyntaxNode(row[this.table.Columns[key.Columns[i]]]), TokenRange.Null);
				if (expression == null)
				{
					expression = expression2;
				}
				else
				{
					expression = BinaryExpressionSyntaxNode.New(BinaryOperator2.And, expression, expression2, TokenRange.Null);
				}
			}
			return expression;
		}

		// Token: 0x06007370 RID: 29552 RVA: 0x0018D750 File Offset: 0x0018B950
		protected Query GetSelectionQuery(TableValue rowsToSelect, TableKey key)
		{
			IFunctionTypeExpression functionTypeExpression = this.CreateFilterFunctionType();
			IExpression[] array = this.CreateKeyColumnReferences(functionTypeExpression.Parameters[0].Identifier, key);
			IExpression expression = null;
			foreach (IValueReference valueReference in rowsToSelect)
			{
				RecordValue asRecord = valueReference.Value.AsRecord;
				IExpression expression2 = this.CreateRowCondition(key, array, asRecord);
				if (expression == null)
				{
					expression = expression2;
				}
				else
				{
					expression = BinaryExpressionSyntaxNode.New(BinaryOperator2.Or, expression, expression2, TokenRange.Null);
				}
			}
			IFunctionExpression functionExpression = new FunctionExpressionSyntaxNode(functionTypeExpression, expression);
			FunctionValue functionValue = this.Compiler.ToFunction(functionExpression);
			return this.table.Query.SelectRows(functionValue);
		}

		// Token: 0x04003F8B RID: 16267
		public static readonly FunctionValue BatchedRows = new StatementBuilder.BatchedRowsFunctionValue();

		// Token: 0x04003F8C RID: 16268
		public static readonly FunctionValue ReturnScalar = new StatementBuilder.ReturnScalarFunctionValue();

		// Token: 0x04003F8D RID: 16269
		protected readonly TableValue table;

		// Token: 0x04003F8E RID: 16270
		private Compiler compiler;

		// Token: 0x02001136 RID: 4406
		private sealed class BatchedRowsFunctionValue : NativeFunctionValue1
		{
			// Token: 0x06007372 RID: 29554 RVA: 0x0018D826 File Offset: 0x0018BA26
			public BatchedRowsFunctionValue()
				: base("table")
			{
			}

			// Token: 0x06007373 RID: 29555 RVA: 0x0000A6A5 File Offset: 0x000088A5
			public override Value Invoke(Value table)
			{
				return table;
			}
		}

		// Token: 0x02001137 RID: 4407
		private sealed class ReturnScalarFunctionValue : NativeFunctionValue1
		{
			// Token: 0x06007374 RID: 29556 RVA: 0x0018D833 File Offset: 0x0018BA33
			public ReturnScalarFunctionValue()
				: base("value")
			{
			}

			// Token: 0x06007375 RID: 29557 RVA: 0x0018D840 File Offset: 0x0018BA40
			public override Value Invoke(Value value)
			{
				return ActionModule.Action.Return.Invoke(value.AsTable.Item0.AsRecord.Item0);
			}
		}

		// Token: 0x02001138 RID: 4408
		private sealed class BindToResultFunctionValue : NativeFunctionValue1
		{
			// Token: 0x06007376 RID: 29558 RVA: 0x0018D861 File Offset: 0x0018BA61
			public BindToResultFunctionValue(Func<Value, ActionValue> binding)
				: base("value")
			{
				this.binding = binding;
			}

			// Token: 0x06007377 RID: 29559 RVA: 0x0018D875 File Offset: 0x0018BA75
			public override Value Invoke(Value value)
			{
				return this.binding(value);
			}

			// Token: 0x04003F8F RID: 16271
			private readonly Func<Value, ActionValue> binding;
		}
	}
}
