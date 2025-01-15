using System;
using System.Data;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Data
{
	// Token: 0x02000052 RID: 82
	public abstract class RecordTransformingDataReader : SimpleDataReader
	{
		// Token: 0x060002BE RID: 702 RVA: 0x00014CF6 File Offset: 0x00012EF6
		public RecordTransformingDataReader(IDataReader baseReader)
			: base(baseReader.GetSchemaTable())
		{
			this.m_record = new SimpleDataRecord(baseReader.GetSchemaTable(), new object[baseReader.FieldCount]);
		}

		// Token: 0x1700006C RID: 108
		// (get) Token: 0x060002BF RID: 703 RVA: 0x00014D20 File Offset: 0x00012F20
		protected override IDataRecord Current
		{
			get
			{
				return this.m_record;
			}
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00014D28 File Offset: 0x00012F28
		public override bool Read()
		{
			if (this.m_baseReader.Read())
			{
				this.m_baseReader.GetValues(this.m_record.Values);
				this.TransformRecord(this.m_record);
				return true;
			}
			return false;
		}

		// Token: 0x060002C1 RID: 705
		public abstract void TransformRecord(SimpleDataRecord record);

		// Token: 0x04000070 RID: 112
		private IDataReader m_baseReader;

		// Token: 0x04000071 RID: 113
		private SimpleDataRecord m_record;
	}
}
