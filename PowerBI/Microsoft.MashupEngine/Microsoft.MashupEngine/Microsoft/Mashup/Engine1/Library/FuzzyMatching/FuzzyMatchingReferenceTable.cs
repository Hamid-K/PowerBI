using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.FuzzyMatching
{
	// Token: 0x02000B4C RID: 2892
	public class FuzzyMatchingReferenceTable
	{
		// Token: 0x170018F3 RID: 6387
		// (get) Token: 0x06005022 RID: 20514 RVA: 0x0010C68B File Offset: 0x0010A88B
		public int[] Keys { get; }

		// Token: 0x170018F4 RID: 6388
		// (get) Token: 0x06005023 RID: 20515 RVA: 0x0010C693 File Offset: 0x0010A893
		public TableValue Table { get; }

		// Token: 0x06005024 RID: 20516 RVA: 0x0010C69B File Offset: 0x0010A89B
		public FuzzyMatchingReferenceTable(TableValue table, int[] keys)
		{
			this.Table = table;
			this.Keys = keys;
			this.dataTable = null;
			this.records = null;
		}

		// Token: 0x170018F5 RID: 6389
		// (get) Token: 0x06005025 RID: 20517 RVA: 0x0010C6BF File Offset: 0x0010A8BF
		public DataTable DataTable
		{
			get
			{
				if (this.dataTable == null)
				{
					this.CreateDataTable();
				}
				return this.dataTable;
			}
		}

		// Token: 0x170018F6 RID: 6390
		// (get) Token: 0x06005026 RID: 20518 RVA: 0x0010C6D5 File Offset: 0x0010A8D5
		public List<RecordValue> Records
		{
			get
			{
				if (this.records == null)
				{
					this.CreateDataTable();
				}
				return this.records;
			}
		}

		// Token: 0x06005027 RID: 20519 RVA: 0x0010C6EB File Offset: 0x0010A8EB
		private void CreateDataTable()
		{
			this.dataTable = FuzzyDataTableCreator.CreateFromRecords("Reference", this.Table.Query.GetRows(), this.Keys, out this.records);
		}

		// Token: 0x04002AFD RID: 11005
		private DataTable dataTable;

		// Token: 0x04002AFE RID: 11006
		private List<RecordValue> records;
	}
}
