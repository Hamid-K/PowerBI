using System;
using System.Collections.Generic;
using System.Data;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Data
{
	// Token: 0x02000051 RID: 81
	[Serializable]
	public class SimpleDataReader : DataReaderDelegateImplBase
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002B1 RID: 689 RVA: 0x00014C63 File Offset: 0x00012E63
		// (set) Token: 0x060002B2 RID: 690 RVA: 0x00014C6B File Offset: 0x00012E6B
		public DataTable SchemaTable { get; set; }

		// Token: 0x060002B3 RID: 691 RVA: 0x00014C74 File Offset: 0x00012E74
		public SimpleDataReader(DataTable schemaTable = null)
		{
			this.SchemaTable = schemaTable;
		}

		// Token: 0x060002B4 RID: 692 RVA: 0x00014C83 File Offset: 0x00012E83
		public SimpleDataReader(DataTable schemaTable, IEnumerator<IDataRecord> records)
		{
			this.SchemaTable = schemaTable;
			this.m_recordEnumerator = records;
		}

		// Token: 0x060002B5 RID: 693 RVA: 0x00014C99 File Offset: 0x00012E99
		public void Reset(IEnumerator<IDataRecord> records)
		{
			this.m_recordEnumerator = records;
		}

		// Token: 0x060002B6 RID: 694 RVA: 0x00014CA2 File Offset: 0x00012EA2
		private IEnumerator<IDataRecord> SingletonEnumerator(IDataRecord r)
		{
			yield return r;
			yield break;
		}

		// Token: 0x060002B7 RID: 695 RVA: 0x00014CB1 File Offset: 0x00012EB1
		public void Reset(IDataRecord record)
		{
			this.m_recordEnumerator = this.SingletonEnumerator(record);
		}

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060002B8 RID: 696 RVA: 0x00014CC0 File Offset: 0x00012EC0
		protected override IDataRecord Current
		{
			get
			{
				return this.m_recordEnumerator.Current;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x00014CCD File Offset: 0x00012ECD
		public override bool IsClosed
		{
			get
			{
				return this.m_bClosed;
			}
		}

		// Token: 0x060002BA RID: 698 RVA: 0x00014CD5 File Offset: 0x00012ED5
		public override void Close()
		{
			this.m_bClosed = true;
		}

		// Token: 0x060002BB RID: 699 RVA: 0x00014CDE File Offset: 0x00012EDE
		public override DataTable GetSchemaTable()
		{
			return this.SchemaTable;
		}

		// Token: 0x060002BC RID: 700 RVA: 0x00014CE6 File Offset: 0x00012EE6
		public override bool NextResult()
		{
			return false;
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00014CE9 File Offset: 0x00012EE9
		public override bool Read()
		{
			return this.m_recordEnumerator.MoveNext();
		}

		// Token: 0x0400006E RID: 110
		private IEnumerator<IDataRecord> m_recordEnumerator;

		// Token: 0x0400006F RID: 111
		private bool m_bClosed;
	}
}
