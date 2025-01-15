using System;
using System.Data;
using System.Diagnostics;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Data
{
	// Token: 0x02000058 RID: 88
	[DebuggerDisplay("{ToString()}")]
	[Serializable]
	public class SimpleDataRecord : DataRecordImplBase
	{
		// Token: 0x060002FA RID: 762 RVA: 0x000154F9 File Offset: 0x000136F9
		public SimpleDataRecord()
		{
		}

		// Token: 0x060002FB RID: 763 RVA: 0x00015501 File Offset: 0x00013701
		public SimpleDataRecord(DataTable schemaTable, object[] recordValues)
		{
			base.Schema = schemaTable;
			this.Values = recordValues;
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00015517 File Offset: 0x00013717
		public override DataTable GetSchemaTable()
		{
			return base.Schema;
		}

		// Token: 0x17000074 RID: 116
		public override object this[int i]
		{
			get
			{
				return this.Values[i];
			}
			set
			{
				this.Values[i] = value;
			}
		}

		// Token: 0x17000075 RID: 117
		public new object this[string columnName]
		{
			get
			{
				return this[this.GetOrdinal(columnName)];
			}
			set
			{
				this[this.GetOrdinal(columnName)] = value;
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00015553 File Offset: 0x00013753
		public void SetValue(int i, object value)
		{
			this[i] = value;
		}

		// Token: 0x06000302 RID: 770 RVA: 0x0001555D File Offset: 0x0001375D
		public void SetValue(string columnName, object value)
		{
			this.SetValue(this.GetOrdinal(columnName), value);
		}

		// Token: 0x04000078 RID: 120
		public object[] Values;
	}
}
