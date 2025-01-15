using System;
using System.Data;
using Microsoft.ReportingServices.DataProcessing;

namespace Microsoft.ReportingServices.DataExtensions
{
	// Token: 0x02000017 RID: 23
	public class DataReaderWrapper : BaseDataWrapper, Microsoft.ReportingServices.DataProcessing.IDataReader, IDisposable
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x00004DFB File Offset: 0x00002FFB
		public DataReaderWrapper(global::System.Data.IDataReader underlyingReader)
			: base(underlyingReader)
		{
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00004E04 File Offset: 0x00003004
		public virtual string GetName(int fieldIndex)
		{
			return this.UnderlyingReader.GetName(fieldIndex);
		}

		// Token: 0x060000C3 RID: 195 RVA: 0x00004E14 File Offset: 0x00003014
		public virtual int GetOrdinal(string fieldName)
		{
			int num = -1;
			if (fieldName != null)
			{
				try
				{
					num = this.UnderlyingReader.GetOrdinal(fieldName);
				}
				catch (IndexOutOfRangeException)
				{
				}
			}
			return num;
		}

		// Token: 0x1700005B RID: 91
		// (get) Token: 0x060000C4 RID: 196 RVA: 0x00004E4C File Offset: 0x0000304C
		public virtual int FieldCount
		{
			get
			{
				return this.UnderlyingReader.FieldCount;
			}
		}

		// Token: 0x060000C5 RID: 197 RVA: 0x00004E59 File Offset: 0x00003059
		public virtual bool Read()
		{
			if (this.UnderlyingReader.Read())
			{
				return true;
			}
			this.UnderlyingReader.NextResult();
			return false;
		}

		// Token: 0x060000C6 RID: 198 RVA: 0x00004E77 File Offset: 0x00003077
		public virtual Type GetFieldType(int fieldIndex)
		{
			return this.UnderlyingReader.GetFieldType(fieldIndex);
		}

		// Token: 0x060000C7 RID: 199 RVA: 0x00004E85 File Offset: 0x00003085
		public virtual object GetValue(int fieldIndex)
		{
			return this.UnderlyingReader.GetValue(fieldIndex);
		}

		// Token: 0x1700005C RID: 92
		// (get) Token: 0x060000C8 RID: 200 RVA: 0x00004E93 File Offset: 0x00003093
		public global::System.Data.IDataReader UnderlyingReader
		{
			get
			{
				return (global::System.Data.IDataReader)base.UnderlyingObject;
			}
		}
	}
}
