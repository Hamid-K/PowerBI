using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using Microsoft.Mashup.Engine1.Language.Query;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B59 RID: 2905
	public class FuzzyOutputEnumerator : IEnumerator<IValueReference>, IDisposable, IEnumerator
	{
		// Token: 0x0600506D RID: 20589 RVA: 0x0010D3D9 File Offset: 0x0010B5D9
		public FuzzyOutputEnumerator(IDataReader outputDataReader, FuzzyJoinParameters parameters, IDataReader inputDataReader, List<RecordValue> referenceRecords)
		{
			this.outputDataReader = outputDataReader;
			this.parameters = parameters;
			this.inputDataReader = inputDataReader;
			this.referenceRecords = referenceRecords;
		}

		// Token: 0x17001914 RID: 6420
		// (get) Token: 0x0600506E RID: 20590 RVA: 0x0010D3FE File Offset: 0x0010B5FE
		object IEnumerator.Current
		{
			get
			{
				return this.Current;
			}
		}

		// Token: 0x17001915 RID: 6421
		// (get) Token: 0x0600506F RID: 20591 RVA: 0x0010D406 File Offset: 0x0010B606
		public IValueReference Current
		{
			get
			{
				if (this.currentRecord == null)
				{
					throw new InvalidOperationException();
				}
				return this.currentRecord;
			}
		}

		// Token: 0x06005070 RID: 20592 RVA: 0x0010D41C File Offset: 0x0010B61C
		public void Dispose()
		{
			this.currentRecord = null;
			this.outputDataReader.Dispose();
		}

		// Token: 0x06005071 RID: 20593 RVA: 0x000033E7 File Offset: 0x000015E7
		public void Reset()
		{
			throw new NotSupportedException();
		}

		// Token: 0x06005072 RID: 20594 RVA: 0x0010D430 File Offset: 0x0010B630
		public bool MoveNext()
		{
			if (this.outputDataReader.Read())
			{
				this.currentRecord = this.CreateRecord();
				return true;
			}
			this.currentRecord = null;
			return false;
		}

		// Token: 0x06005073 RID: 20595 RVA: 0x0010D458 File Offset: 0x0010B658
		private RecordValue CreateRecord()
		{
			RecordValue recordValue4;
			try
			{
				int num = this.parameters.LeftKeyColumns.Length;
				RecordValue recordValue = ((FuzzyInputDataReader)this.inputDataReader).CurrentRecord;
				RecordValue recordValue2 = null;
				if (!this.outputDataReader.IsDBNull(num + 1))
				{
					int @int = this.outputDataReader.GetInt32(num + 1);
					recordValue2 = this.referenceRecords[@int];
				}
				RecordValue recordValue3 = JoinAlgorithm.GetRow(recordValue, recordValue2, this.parameters.JoinKeys, this.parameters.JoinColumns);
				if (this.parameters.JoinOptions.SimilarityColumnName != null)
				{
					Value value;
					try
					{
						value = NumberValue.New(FuzzyUtils.TruncateSimilarity(this.outputDataReader.GetDouble((num + 1) * 2)));
					}
					catch (InvalidCastException)
					{
						value = Value.Null;
					}
					recordValue3 = Library.Record.AddField.Invoke(recordValue3, TextValue.New(this.parameters.JoinOptions.SimilarityColumnName), value).AsRecord;
				}
				recordValue4 = recordValue3;
			}
			catch
			{
				if (this.outputDataReader != null)
				{
					this.outputDataReader.Dispose();
				}
				throw;
			}
			return recordValue4;
		}

		// Token: 0x04002B1D RID: 11037
		private readonly IDataReader outputDataReader;

		// Token: 0x04002B1E RID: 11038
		private readonly FuzzyJoinParameters parameters;

		// Token: 0x04002B1F RID: 11039
		private readonly IDataReader inputDataReader;

		// Token: 0x04002B20 RID: 11040
		private readonly List<RecordValue> referenceRecords;

		// Token: 0x04002B21 RID: 11041
		private RecordValue currentRecord;
	}
}
