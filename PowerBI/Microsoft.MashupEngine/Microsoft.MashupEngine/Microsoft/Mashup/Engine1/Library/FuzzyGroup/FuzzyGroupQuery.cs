using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.FuzzyMatching;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.FuzzyGroup
{
	// Token: 0x02000B69 RID: 2921
	internal class FuzzyGroupQuery : DataSourceQuery
	{
		// Token: 0x060050BA RID: 20666 RVA: 0x0010E59C File Offset: 0x0010C79C
		public FuzzyGroupQuery(IEngineHost host, Query query, FuzzyGrouping fuzzyGrouping)
		{
			this.host = host;
			this.query = query;
			this.fuzzyGrouping = fuzzyGrouping;
		}

		// Token: 0x1700192F RID: 6447
		// (get) Token: 0x060050BB RID: 20667 RVA: 0x0010E5B9 File Offset: 0x0010C7B9
		public Query Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x17001930 RID: 6448
		// (get) Token: 0x060050BC RID: 20668 RVA: 0x0010E5C1 File Offset: 0x0010C7C1
		public FuzzyGrouping FuzzyGrouping
		{
			get
			{
				return this.fuzzyGrouping;
			}
		}

		// Token: 0x17001931 RID: 6449
		// (get) Token: 0x060050BD RID: 20669 RVA: 0x0010E5C9 File Offset: 0x0010C7C9
		public override IEngineHost EngineHost
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x17001932 RID: 6450
		// (get) Token: 0x060050BE RID: 20670 RVA: 0x0010E5D1 File Offset: 0x0010C7D1
		public override Keys Columns
		{
			get
			{
				return this.fuzzyGrouping.ResultKeys;
			}
		}

		// Token: 0x060050BF RID: 20671 RVA: 0x0010E5E0 File Offset: 0x0010C7E0
		public override TypeValue GetColumnType(int column)
		{
			if (column < this.fuzzyGrouping.KeyColumns.Length)
			{
				return this.query.GetColumnType(this.fuzzyGrouping.KeyColumns[column]);
			}
			return this.fuzzyGrouping.Constructors[column - this.fuzzyGrouping.KeyColumns.Length].Type.Value.AsType;
		}

		// Token: 0x060050C0 RID: 20672 RVA: 0x0010E640 File Offset: 0x0010C840
		public override IEnumerable<IValueReference> GetRows()
		{
			FuzzyUtils.ValidateTextColumns(this.query, this.fuzzyGrouping.KeyColumns);
			return new FuzzyGroupQuery.FuzzyGroupEnumerable(this.query.GetRows(), this.fuzzyGrouping);
		}

		// Token: 0x060050C1 RID: 20673 RVA: 0x0010E670 File Offset: 0x0010C870
		public override bool TryGetExpression(out IExpression expression)
		{
			Value[] array = new Value[this.FuzzyGrouping.KeyKeys.Length];
			for (int i = 0; i < this.FuzzyGrouping.KeyKeys.Length; i++)
			{
				string text = this.FuzzyGrouping.KeyKeys[i];
				array[i] = TextValue.New(text);
			}
			List<Value> list = new List<Value>(this.FuzzyGrouping.Constructors.Length);
			foreach (ColumnConstructor columnConstructor in this.FuzzyGrouping.Constructors)
			{
				list.Add(ListValue.New(new Value[]
				{
					TextValue.New(columnConstructor.Name),
					columnConstructor.Function,
					columnConstructor.Type.Value
				}));
			}
			expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(new FuzzyGroupingModule.FuzzyGroupFunctionValue(this.host)), new IExpression[]
			{
				QueryToExpressionVisitor.ToExpression(this.Query),
				new ConstantExpressionSyntaxNode(ListValue.New(array.ToArray<Value>())),
				new ConstantExpressionSyntaxNode(ListValue.New(list.ToArray())),
				new ConstantExpressionSyntaxNode(this.FuzzyGrouping.FuzzyGroupOptions.AsRecord())
			});
			return true;
		}

		// Token: 0x04002B5B RID: 11099
		private readonly IEngineHost host;

		// Token: 0x04002B5C RID: 11100
		private readonly Query query;

		// Token: 0x04002B5D RID: 11101
		private readonly FuzzyGrouping fuzzyGrouping;

		// Token: 0x02000B6A RID: 2922
		private class FuzzyGroupEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x060050C2 RID: 20674 RVA: 0x0010E7A6 File Offset: 0x0010C9A6
			public FuzzyGroupEnumerable(IEnumerable<IValueReference> rows, FuzzyGrouping fuzzyGrouping)
			{
				this.rows = rows;
				this.fuzzyGrouping = fuzzyGrouping;
			}

			// Token: 0x060050C3 RID: 20675 RVA: 0x0010E7BC File Offset: 0x0010C9BC
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x060050C4 RID: 20676 RVA: 0x0010E7C4 File Offset: 0x0010C9C4
			public IEnumerator<IValueReference> GetEnumerator()
			{
				List<RecordValue> list;
				return (from fuzzyCluster in ExternalFuzzyGroup.GetFuzzyGroups(FuzzyDataTableCreator.CreateFromRecords("Input", this.rows, this.fuzzyGrouping.KeyColumns, out list), this.fuzzyGrouping.FuzzyGroupOptions, list)
					select FuzzyGroupQuery.FuzzyGroupEnumerable.CreateFuzzyGroupResult(this.fuzzyGrouping, fuzzyCluster)).GetEnumerator();
			}

			// Token: 0x060050C5 RID: 20677 RVA: 0x0010E818 File Offset: 0x0010CA18
			private static IValueReference CreateFuzzyGroupResult(FuzzyGrouping fuzzyGrouping, DuplicateGroupWithValues fuzzyCluster)
			{
				int[] keyColumns = fuzzyGrouping.KeyColumns;
				ColumnConstructor[] constructors = fuzzyGrouping.Constructors;
				IValueReference[] array = new IValueReference[fuzzyGrouping.ResultKeys.Length];
				for (int i = 0; i < keyColumns.Length; i++)
				{
					array[i] = fuzzyCluster.RepresentativeRecord[keyColumns[i]];
				}
				for (int j = 0; j < constructors.Length; j++)
				{
					ListValue listValue;
					if (fuzzyCluster.DuplicateRecords.Count<RecordValue>() != 0)
					{
						Value[] array2 = fuzzyCluster.DuplicateRecords.ToArray<RecordValue>();
						listValue = ListValue.New(array2);
					}
					else
					{
						listValue = ListValue.Empty;
					}
					IValueReference valueReference = listValue.ToTable();
					array[keyColumns.Length + j] = new TransformValueReference(valueReference, constructors[j].Function);
				}
				return RecordValue.New(fuzzyGrouping.ResultKeys, array);
			}

			// Token: 0x04002B5E RID: 11102
			private const string InputTableName = "Input";

			// Token: 0x04002B5F RID: 11103
			private readonly IEnumerable<IValueReference> rows;

			// Token: 0x04002B60 RID: 11104
			private readonly FuzzyGrouping fuzzyGrouping;
		}
	}
}
