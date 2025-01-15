using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B4F RID: 2895
	public class IndexFuzzyNestedJoinAlgorithm : IndexNestedJoinAlgorithm
	{
		// Token: 0x0600502F RID: 20527 RVA: 0x0010C9F2 File Offset: 0x0010ABF2
		public IndexFuzzyNestedJoinAlgorithm(IEngineHost host)
		{
			this.host = host;
		}

		// Token: 0x06005030 RID: 20528 RVA: 0x0010CA01 File Offset: 0x0010AC01
		public override IEnumerable<IValueReference> NestedJoin(NestedJoinParameters parameters)
		{
			if (parameters.JoinKind == TableTypeAlgebra.JoinKind.LeftOuter)
			{
				return new IndexFuzzyNestedJoinAlgorithm.IndexFuzzyNestedJoinEnumerable(this.host, (FuzzyNestedJoinParameters)parameters);
			}
			throw new InvalidOperationException();
		}

		// Token: 0x06005031 RID: 20529 RVA: 0x0010CA24 File Offset: 0x0010AC24
		public static IEnumerator<IValueReference> Join(IEngineHost host, FuzzyNestedJoinParameters parameters)
		{
			IEnumerator<IValueReference> enumerator = parameters.LeftQuery.GetRows().GetEnumerator();
			return new IndexFuzzyNestedJoinAlgorithm.IndexFuzzyNestedJoinEnumerator(host, parameters.RightTable, parameters.JoinColumns, parameters.LeftKeyColumns, parameters.RightKey, enumerator, parameters);
		}

		// Token: 0x04002B01 RID: 11009
		private readonly IEngineHost host;

		// Token: 0x02000B50 RID: 2896
		private class IndexFuzzyNestedJoinEnumerable : IEnumerable<IValueReference>, IEnumerable
		{
			// Token: 0x06005032 RID: 20530 RVA: 0x0010CA62 File Offset: 0x0010AC62
			public IndexFuzzyNestedJoinEnumerable(IEngineHost host, FuzzyNestedJoinParameters parameters)
			{
				this.host = host;
				this.parameters = parameters;
			}

			// Token: 0x06005033 RID: 20531 RVA: 0x0010CA78 File Offset: 0x0010AC78
			IEnumerator IEnumerable.GetEnumerator()
			{
				return this.GetEnumerator();
			}

			// Token: 0x06005034 RID: 20532 RVA: 0x0010CA80 File Offset: 0x0010AC80
			public IEnumerator<IValueReference> GetEnumerator()
			{
				return IndexFuzzyNestedJoinAlgorithm.Join(this.host, this.parameters);
			}

			// Token: 0x04002B02 RID: 11010
			private readonly IEngineHost host;

			// Token: 0x04002B03 RID: 11011
			private readonly FuzzyNestedJoinParameters parameters;
		}

		// Token: 0x02000B51 RID: 2897
		public class IndexFuzzyNestedJoinEnumerator : IndexNestedJoinAlgorithm.IndexNestedJoinEnumerator
		{
			// Token: 0x06005035 RID: 20533 RVA: 0x0010CA94 File Offset: 0x0010AC94
			public IndexFuzzyNestedJoinEnumerator(IEngineHost host, Value rightTable, Keys joinColumns, int[] leftKeyColumns, Keys rightKey, IEnumerator<IValueReference> leftEnumerator, FuzzyNestedJoinParameters parameters)
				: base(rightTable, joinColumns, leftKeyColumns, rightKey, leftEnumerator)
			{
				this.host = host;
				this.parameters = parameters;
				this.sessionId = Guid.NewGuid();
				this.isFirst = true;
				this.joinStarted = false;
				this.fuzzyMatcher = FuzzyMatcher.New(this.parameters.JoinKind, true);
				int[] columns = TableValue.GetColumns(this.parameters.RightTable.AsTable.Columns, this.parameters.RightKey);
				this.referenceTable = new FuzzyMatchingReferenceTable(base.RightTable.AsTable, columns);
				this.transformationTable = new Lazy<DataTable>(() => FuzzyDataTableCreator.CreateTransformationDataTableFromTableValue(parameters.JoinOptions.TransformationTable));
			}

			// Token: 0x06005036 RID: 20534 RVA: 0x0010CB54 File Offset: 0x0010AD54
			public override void Dispose()
			{
				if (this.joinStarted)
				{
					this.fuzzyMatcher.FinishNestedJoin(this.sessionId);
					this.joinStarted = false;
				}
				base.Dispose();
			}

			// Token: 0x06005037 RID: 20535 RVA: 0x0010CB7C File Offset: 0x0010AD7C
			protected override IValueReference GetNestedTableValue(RecordValue leftRow, RecordValue rightKeyValue)
			{
				IValueReference valueReference;
				try
				{
					bool flag;
					IValueReference nestedJoinTableValue = this.fuzzyMatcher.GetNestedJoinTableValue(this.host, leftRow, base.LeftKeyColumns, this.referenceTable, this.transformationTable, this.parameters, this.sessionId, this.isFirst, out flag);
					if (this.isFirst)
					{
						this.isFirst = false;
					}
					if (flag)
					{
						this.joinStarted = true;
					}
					valueReference = nestedJoinTableValue;
				}
				catch (ValueException ex)
				{
					valueReference = new ExceptionValueReference(ex);
				}
				return valueReference;
			}

			// Token: 0x04002B04 RID: 11012
			private readonly IEngineHost host;

			// Token: 0x04002B05 RID: 11013
			private readonly FuzzyNestedJoinParameters parameters;

			// Token: 0x04002B06 RID: 11014
			private readonly Guid sessionId;

			// Token: 0x04002B07 RID: 11015
			private readonly FuzzyMatcher fuzzyMatcher;

			// Token: 0x04002B08 RID: 11016
			private readonly FuzzyMatchingReferenceTable referenceTable;

			// Token: 0x04002B09 RID: 11017
			private readonly Lazy<DataTable> transformationTable;

			// Token: 0x04002B0A RID: 11018
			private bool isFirst;

			// Token: 0x04002B0B RID: 11019
			private bool joinStarted;
		}
	}
}
