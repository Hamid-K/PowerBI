using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Library.FuzzyMatching;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.FuzzyGroup
{
	// Token: 0x02000B5B RID: 2907
	internal class AddFuzzyClusterColumnQuery : DataSourceQuery
	{
		// Token: 0x06005079 RID: 20601 RVA: 0x0010D684 File Offset: 0x0010B884
		public AddFuzzyClusterColumnQuery(IEngineHost host, Query query, TextValue columnName, TextValue newColumnName, FuzzyGroupOptions fuzzyOptions)
		{
			KeysBuilder keysBuilder = new KeysBuilder(query.Columns.Length + 2);
			keysBuilder.Union(query.Columns);
			keysBuilder = this.AddKey(keysBuilder, newColumnName.String);
			if (fuzzyOptions.SimilarityColumnName != null)
			{
				keysBuilder = this.AddKey(keysBuilder, fuzzyOptions.SimilarityColumnName);
			}
			this.host = host;
			this.query = query;
			this.columns = keysBuilder.ToKeys();
			this.columnName = columnName;
			this.newColumnName = newColumnName;
			this.fuzzyOptions = fuzzyOptions;
		}

		// Token: 0x17001916 RID: 6422
		// (get) Token: 0x0600507A RID: 20602 RVA: 0x0010D711 File Offset: 0x0010B911
		public Query Query
		{
			get
			{
				return this.query;
			}
		}

		// Token: 0x17001917 RID: 6423
		// (get) Token: 0x0600507B RID: 20603 RVA: 0x0010D719 File Offset: 0x0010B919
		public TextValue ColumnName
		{
			get
			{
				return this.columnName;
			}
		}

		// Token: 0x17001918 RID: 6424
		// (get) Token: 0x0600507C RID: 20604 RVA: 0x0010D721 File Offset: 0x0010B921
		public TextValue NewColumnName
		{
			get
			{
				return this.newColumnName;
			}
		}

		// Token: 0x17001919 RID: 6425
		// (get) Token: 0x0600507D RID: 20605 RVA: 0x0010D729 File Offset: 0x0010B929
		public FuzzyGroupOptions FuzzyOptions
		{
			get
			{
				return this.fuzzyOptions;
			}
		}

		// Token: 0x1700191A RID: 6426
		// (get) Token: 0x0600507E RID: 20606 RVA: 0x0010D731 File Offset: 0x0010B931
		public override IEngineHost EngineHost
		{
			get
			{
				return this.host;
			}
		}

		// Token: 0x1700191B RID: 6427
		// (get) Token: 0x0600507F RID: 20607 RVA: 0x0010D739 File Offset: 0x0010B939
		public override Keys Columns
		{
			get
			{
				return this.columns;
			}
		}

		// Token: 0x06005080 RID: 20608 RVA: 0x0010D741 File Offset: 0x0010B941
		public override TypeValue GetColumnType(int column)
		{
			if (column < this.query.Columns.Length)
			{
				return this.query.GetColumnType(column);
			}
			if (column == this.query.Columns.Length)
			{
				return TypeValue.Text;
			}
			return TypeValue.Number;
		}

		// Token: 0x06005081 RID: 20609 RVA: 0x0010D784 File Offset: 0x0010B984
		public override IEnumerable<IValueReference> GetRows()
		{
			int[] array = TableValue.GetColumns(this.columns, this.columnName);
			FuzzyUtils.ValidateTextColumns(this.query, array);
			if (array.Length != 1)
			{
				return new ExceptionValueReference[]
				{
					new ExceptionValueReference(ValueException.DuplicateField(this.columnName.String))
				};
			}
			return new AddFuzzyClusterColumnQuery.AddFuzzyClusterColumnEnumerable(this.query.GetRows(), array[0], this.fuzzyOptions, this.columnName, this.newColumnName);
		}

		// Token: 0x06005082 RID: 20610 RVA: 0x0010D7FC File Offset: 0x0010B9FC
		public override bool TryGetExpression(out IExpression expression)
		{
			expression = new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(new FuzzyGroupingModule.AddFuzzyClusterColumnFunctionValue(this.host)), new IExpression[]
			{
				QueryToExpressionVisitor.ToExpression(this.Query),
				new ConstantExpressionSyntaxNode(this.columnName),
				new ConstantExpressionSyntaxNode(this.newColumnName),
				new ConstantExpressionSyntaxNode(this.fuzzyOptions.AsRecord())
			});
			return true;
		}

		// Token: 0x06005083 RID: 20611 RVA: 0x0010D864 File Offset: 0x0010BA64
		private KeysBuilder AddKey(KeysBuilder keysBuilder, string key)
		{
			if (!keysBuilder.Union(key))
			{
				throw ValueException.NewExpressionError<Message1>(Strings.Table_ColumnAlreadyExistsInTable(key), null, null);
			}
			return keysBuilder;
		}

		// Token: 0x04002B22 RID: 11042
		private readonly IEngineHost host;

		// Token: 0x04002B23 RID: 11043
		private readonly Query query;

		// Token: 0x04002B24 RID: 11044
		private readonly Keys columns;

		// Token: 0x04002B25 RID: 11045
		private readonly TextValue columnName;

		// Token: 0x04002B26 RID: 11046
		private readonly TextValue newColumnName;

		// Token: 0x04002B27 RID: 11047
		private readonly FuzzyGroupOptions fuzzyOptions;

		// Token: 0x02000B5C RID: 2908
		private class AddFuzzyClusterColumnEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x06005084 RID: 20612 RVA: 0x0010D87F File Offset: 0x0010BA7F
			public AddFuzzyClusterColumnEnumerable(IEnumerable<IValueReference> rows, int columnIndex, FuzzyGroupOptions fuzzyGroupOptions, TextValue columnName, TextValue newColumnName)
			{
				this.rows = rows;
				this.columnIndex = columnIndex;
				this.fuzzyGroupOptions = fuzzyGroupOptions;
				this.columnName = columnName;
				this.newColumnName = newColumnName;
			}

			// Token: 0x06005085 RID: 20613 RVA: 0x0010D8AC File Offset: 0x0010BAAC
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06005086 RID: 20614 RVA: 0x0010D8B4 File Offset: 0x0010BAB4
			public IEnumerator<IValueReference> GetEnumerator()
			{
				List<RecordValue> list;
				DataTable dataTable = FuzzyDataTableCreator.CreateFromRecords("Input", this.rows, new int[] { this.columnIndex }, out list);
				Dictionary<string, RepresentativeValueWithSimilarity> representativeValues = ExternalFuzzyGroup.GetRepresentativeValues(dataTable, this.fuzzyGroupOptions, list, this.columnIndex);
				return list.Select((RecordValue record) => AddFuzzyClusterColumnQuery.AddFuzzyClusterColumnEnumerable.CreateAddFuzzyClusterColumnResult(record, representativeValues, this.columnIndex, this.newColumnName, this.fuzzyGroupOptions.SimilarityColumnName)).GetEnumerator();
			}

			// Token: 0x06005087 RID: 20615 RVA: 0x0010D920 File Offset: 0x0010BB20
			private static IValueReference CreateAddFuzzyClusterColumnResult(RecordValue record, Dictionary<string, RepresentativeValueWithSimilarity> representativeValues, int columnIndex, TextValue newColumnName, string similarityColumnName)
			{
				RecordBuilder recordBuilder = new RecordBuilder(2);
				string text = (record[columnIndex].IsNull ? null : record[columnIndex].AsText.String);
				RepresentativeValueWithSimilarity representativeValueWithSimilarity = null;
				if (text != null && !representativeValues.TryGetValue(text, out representativeValueWithSimilarity))
				{
					representativeValueWithSimilarity = new RepresentativeValueWithSimilarity(text, text, 1.0);
				}
				if (representativeValueWithSimilarity != null)
				{
					recordBuilder.Add(newColumnName.String, TextValue.New(representativeValueWithSimilarity.RepresentativeValue), TypeValue.Text);
					if (similarityColumnName != null)
					{
						recordBuilder.Add(similarityColumnName, NumberValue.New(representativeValueWithSimilarity.Similarity), TypeValue.Number);
					}
				}
				else
				{
					recordBuilder.Add(newColumnName.String, Value.Null, TypeValue.Null);
					if (similarityColumnName != null)
					{
						recordBuilder.Add(similarityColumnName, NumberValue.New(1), TypeValue.Number);
					}
				}
				return record.Concatenate(recordBuilder.ToRecord());
			}

			// Token: 0x04002B28 RID: 11048
			private const string InputTableName = "Input";

			// Token: 0x04002B29 RID: 11049
			private readonly IEnumerable<IValueReference> rows;

			// Token: 0x04002B2A RID: 11050
			private readonly FuzzyGroupOptions fuzzyGroupOptions;

			// Token: 0x04002B2B RID: 11051
			private readonly int columnIndex;

			// Token: 0x04002B2C RID: 11052
			private readonly TextValue columnName;

			// Token: 0x04002B2D RID: 11053
			private readonly TextValue newColumnName;
		}
	}
}
