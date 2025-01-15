using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Runtime.Serialization;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Collections;
using Microsoft.DataIntegration.FuzzyMatchingCommon.Data;

namespace Microsoft.DataIntegration.FuzzyMatching
{
	// Token: 0x02000056 RID: 86
	[Serializable]
	public sealed class MatchResultsReader : IDataReader, IDisposable, IDataRecord, IMatchResult, IEnumerable<IMatchResult>, IEnumerable, IDeserializationCallback
	{
		// Token: 0x170000AE RID: 174
		// (get) Token: 0x0600032D RID: 813 RVA: 0x0001088A File Offset: 0x0000EA8A
		// (set) Token: 0x0600032E RID: 814 RVA: 0x00010892 File Offset: 0x0000EA92
		internal DataTable OutputSchema { get; private set; }

		// Token: 0x0600032F RID: 815 RVA: 0x0001089B File Offset: 0x0000EA9B
		internal MatchResultsReader(DataTable inputSchema, DataTable referenceSchema, DataTable outputSchema)
		{
			this.m_inputSchema = inputSchema;
			this.m_referenceSchema = referenceSchema;
			this.OutputSchema = outputSchema;
		}

		// Token: 0x06000330 RID: 816 RVA: 0x000108CF File Offset: 0x0000EACF
		void IDeserializationCallback.OnDeserialization(object sender)
		{
			this.m_results = new ObjectVector<MatchResult>(1);
		}

		// Token: 0x06000331 RID: 817 RVA: 0x000108DD File Offset: 0x0000EADD
		internal void Reset(IDataRecord inputRecord)
		{
			this.m_results.Clear();
			this.m_defaultResult.Reset();
			this.m_iteratorIdx = -1;
			this.m_bIteratorClosed = false;
			this.m_inputRecord = inputRecord;
		}

		// Token: 0x170000AF RID: 175
		// (get) Token: 0x06000332 RID: 818 RVA: 0x0001090A File Offset: 0x0000EB0A
		public int MatchCount
		{
			get
			{
				return this.m_results.Count;
			}
		}

		// Token: 0x06000333 RID: 819 RVA: 0x00010917 File Offset: 0x0000EB17
		IEnumerator IEnumerable.GetEnumerator()
		{
			while (this.Read())
			{
				yield return this;
			}
			yield break;
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00010926 File Offset: 0x0000EB26
		public IEnumerator<IMatchResult> GetEnumerator()
		{
			while (this.Read())
			{
				yield return this;
			}
			yield break;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00010935 File Offset: 0x0000EB35
		private MatchResult GetMatchResult(int iteratorIndex)
		{
			if (iteratorIndex >= 0 && iteratorIndex < this.m_results.Count)
			{
				return this.m_results[iteratorIndex];
			}
			return this.m_defaultResult;
		}

		// Token: 0x170000B0 RID: 176
		// (get) Token: 0x06000336 RID: 822 RVA: 0x0001095C File Offset: 0x0000EB5C
		public int RightRecordId
		{
			get
			{
				return this.GetMatchResult(this.m_iteratorIdx).RightRecordId;
			}
		}

		// Token: 0x170000B1 RID: 177
		// (get) Token: 0x06000337 RID: 823 RVA: 0x0001096F File Offset: 0x0000EB6F
		public IDataRecord InputRecord
		{
			get
			{
				return this.m_inputRecord;
			}
		}

		// Token: 0x170000B2 RID: 178
		// (get) Token: 0x06000338 RID: 824 RVA: 0x00010977 File Offset: 0x0000EB77
		public IDataRecord RightRecord
		{
			get
			{
				return this.GetMatchResult(this.m_iteratorIdx).RightRecord;
			}
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000339 RID: 825 RVA: 0x0001098A File Offset: 0x0000EB8A
		public ComparisonResult ComparisonResult
		{
			get
			{
				return this.GetMatchResult(this.m_iteratorIdx).ComparisonResult;
			}
		}

		// Token: 0x170000B4 RID: 180
		public object this[int i]
		{
			get
			{
				if (this.m_iteratorIdx < 0)
				{
					throw new InvalidOperationException("Iterator is not positioned at a valid row.  Must call Read() and/or Reset().");
				}
				if (i < this.m_inputSchema.Rows.Count)
				{
					return this.InputRecord[i];
				}
				if (i < this.m_inputSchema.Rows.Count + this.m_referenceSchema.Rows.Count)
				{
					return this.RightRecord[i - this.m_inputSchema.Rows.Count];
				}
				if (i == this.m_inputSchema.Rows.Count + this.m_referenceSchema.Rows.Count)
				{
					return this.GetMatchResult(this.m_iteratorIdx).ComparisonResult.Similarity;
				}
				if (i < this.OutputSchema.Rows.Count)
				{
					DataRow dataRow = this.OutputSchema.Rows[i];
					return (this.GetMatchResult(this.m_iteratorIdx).ComparisonResult as IPropertyBag)[dataRow[SchemaTableOptionalColumn.ProviderSpecificDataType] as string];
				}
				throw new ArgumentOutOfRangeException();
			}
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x0600033B RID: 827 RVA: 0x00010AB9 File Offset: 0x0000ECB9
		public int FieldCount
		{
			get
			{
				return this.GetSchemaTable().Rows.Count;
			}
		}

		// Token: 0x0600033C RID: 828 RVA: 0x00010ACB File Offset: 0x0000ECCB
		public object GetValue(int i)
		{
			return this[i];
		}

		// Token: 0x0600033D RID: 829 RVA: 0x00010AD4 File Offset: 0x0000ECD4
		public bool GetBoolean(int i)
		{
			return (bool)this[i];
		}

		// Token: 0x0600033E RID: 830 RVA: 0x00010AE2 File Offset: 0x0000ECE2
		public byte GetByte(int i)
		{
			return (byte)this[i];
		}

		// Token: 0x0600033F RID: 831 RVA: 0x00010AF0 File Offset: 0x0000ECF0
		public char GetChar(int i)
		{
			return (char)this[i];
		}

		// Token: 0x06000340 RID: 832 RVA: 0x00010AFE File Offset: 0x0000ECFE
		public decimal GetDecimal(int i)
		{
			return (decimal)this[i];
		}

		// Token: 0x06000341 RID: 833 RVA: 0x00010B0C File Offset: 0x0000ED0C
		public double GetDouble(int i)
		{
			return (double)this[i];
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00010B1A File Offset: 0x0000ED1A
		public float GetFloat(int i)
		{
			return (float)this[i];
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00010B28 File Offset: 0x0000ED28
		public short GetInt16(int i)
		{
			return (short)this[i];
		}

		// Token: 0x06000344 RID: 836 RVA: 0x00010B36 File Offset: 0x0000ED36
		public int GetInt32(int i)
		{
			return (int)this[i];
		}

		// Token: 0x06000345 RID: 837 RVA: 0x00010B44 File Offset: 0x0000ED44
		public long GetInt64(int i)
		{
			return (long)this[i];
		}

		// Token: 0x06000346 RID: 838 RVA: 0x00010B52 File Offset: 0x0000ED52
		public string GetString(int i)
		{
			return this[i].ToString();
		}

		// Token: 0x06000347 RID: 839 RVA: 0x00010B60 File Offset: 0x0000ED60
		public DateTime GetDateTime(int i)
		{
			return (DateTime)this[i];
		}

		// Token: 0x06000348 RID: 840 RVA: 0x00010B6E File Offset: 0x0000ED6E
		public Guid GetGuid(int i)
		{
			return (Guid)this[i];
		}

		// Token: 0x06000349 RID: 841 RVA: 0x00010B7C File Offset: 0x0000ED7C
		public IDataReader GetData(int i)
		{
			return (IDataReader)this[i];
		}

		// Token: 0x0600034A RID: 842 RVA: 0x00010B8A File Offset: 0x0000ED8A
		public bool IsDBNull(int i)
		{
			return this[i] == null || DBNull.Value == this[i];
		}

		// Token: 0x0600034B RID: 843 RVA: 0x00010BA5 File Offset: 0x0000EDA5
		public int GetOrdinal(string name)
		{
			return SchemaUtils.GetOrdinal(this.GetSchemaTable(), name, true);
		}

		// Token: 0x0600034C RID: 844 RVA: 0x00010BB4 File Offset: 0x0000EDB4
		public string GetName(int i)
		{
			return SchemaUtils.GetName(this.GetSchemaTable(), i);
		}

		// Token: 0x170000B6 RID: 182
		public object this[string name]
		{
			get
			{
				return this[this.GetOrdinal(name)];
			}
		}

		// Token: 0x0600034E RID: 846 RVA: 0x00010BD1 File Offset: 0x0000EDD1
		public Type GetFieldType(int i)
		{
			return (Type)this.GetSchemaTable().Rows[i][SchemaTableColumn.DataType];
		}

		// Token: 0x0600034F RID: 847 RVA: 0x00010BF3 File Offset: 0x0000EDF3
		public string GetDataTypeName(int i)
		{
			return SchemaUtils.GetDataTypeName(this.GetSchemaTable(), i);
		}

		// Token: 0x06000350 RID: 848 RVA: 0x00010C04 File Offset: 0x0000EE04
		public int GetValues(object[] values)
		{
			int num = Math.Min(values.Length, this.FieldCount);
			for (int i = 0; i < num; i++)
			{
				values[i] = this[i];
			}
			return num;
		}

		// Token: 0x06000351 RID: 849 RVA: 0x00010C37 File Offset: 0x0000EE37
		public long GetBytes(int i, long fieldOffset, byte[] buffer, int bufferOffset, int length)
		{
			byte[] array = (byte[])this[i];
			Array.Copy(array, fieldOffset, buffer, (long)bufferOffset, (long)length);
			return Math.Min((long)array.Length - fieldOffset, (long)Math.Min(buffer.Length - bufferOffset, length));
		}

		// Token: 0x06000352 RID: 850 RVA: 0x00010C6B File Offset: 0x0000EE6B
		public long GetChars(int i, long fieldOffset, char[] buffer, int bufferOffset, int length)
		{
			char[] array = (char[])this[i];
			Array.Copy(array, fieldOffset, buffer, (long)bufferOffset, (long)length);
			return Math.Min((long)array.Length - fieldOffset, (long)Math.Min(buffer.Length - bufferOffset, length));
		}

		// Token: 0x06000353 RID: 851 RVA: 0x00010C9F File Offset: 0x0000EE9F
		public DataTable GetSchemaTable()
		{
			return this.OutputSchema;
		}

		// Token: 0x06000354 RID: 852 RVA: 0x00010CA8 File Offset: 0x0000EEA8
		public bool Read()
		{
			int num = this.m_iteratorIdx + 1;
			this.m_iteratorIdx = num;
			return num < this.m_results.Count;
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x06000355 RID: 853 RVA: 0x00010CD3 File Offset: 0x0000EED3
		public int Depth
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000356 RID: 854 RVA: 0x00010CDA File Offset: 0x0000EEDA
		public bool IsClosed
		{
			get
			{
				return this.m_bIteratorClosed;
			}
		}

		// Token: 0x06000357 RID: 855 RVA: 0x00010CE2 File Offset: 0x0000EEE2
		public void Close()
		{
			this.m_iteratorIdx = this.m_results.Count;
			this.m_bIteratorClosed = true;
			this.m_results.Clear();
			this.m_inputRecord = null;
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x06000358 RID: 856 RVA: 0x00010D0E File Offset: 0x0000EF0E
		public int RecordsAffected
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x06000359 RID: 857 RVA: 0x00010D11 File Offset: 0x0000EF11
		public bool NextResult()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600035A RID: 858 RVA: 0x00010D18 File Offset: 0x0000EF18
		public void Dispose()
		{
			for (int i = 0; i < this.m_results.Count; i++)
			{
				this.m_results[i].Reset();
			}
			this.Close();
		}

		// Token: 0x0400011D RID: 285
		private int m_iteratorIdx;

		// Token: 0x0400011E RID: 286
		private bool m_bIteratorClosed;

		// Token: 0x0400011F RID: 287
		internal DataTable m_inputSchema;

		// Token: 0x04000120 RID: 288
		internal DataTable m_referenceSchema;

		// Token: 0x04000122 RID: 290
		[NonSerialized]
		private IDataRecord m_inputRecord;

		// Token: 0x04000123 RID: 291
		[NonSerialized]
		internal ObjectVector<MatchResult> m_results = new ObjectVector<MatchResult>(1);

		// Token: 0x04000124 RID: 292
		internal MatchResult m_defaultResult = new MatchResult();
	}
}
