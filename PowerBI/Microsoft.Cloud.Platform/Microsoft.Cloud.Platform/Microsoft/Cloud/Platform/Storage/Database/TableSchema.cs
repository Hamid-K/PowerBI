using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace Microsoft.Cloud.Platform.Storage.Database
{
	// Token: 0x02000052 RID: 82
	public abstract class TableSchema
	{
		// Token: 0x1700006F RID: 111
		// (get) Token: 0x060001EE RID: 494 RVA: 0x000067EC File Offset: 0x000049EC
		// (set) Token: 0x060001EF RID: 495 RVA: 0x000067F4 File Offset: 0x000049F4
		public IEnumerable<ColumnDefinition> Columns { get; private set; }

		// Token: 0x17000070 RID: 112
		// (get) Token: 0x060001F0 RID: 496 RVA: 0x000067FD File Offset: 0x000049FD
		// (set) Token: 0x060001F1 RID: 497 RVA: 0x00006805 File Offset: 0x00004A05
		public string TableName { get; private set; }

		// Token: 0x060001F2 RID: 498 RVA: 0x0000680E File Offset: 0x00004A0E
		protected TableSchema(string tableName)
		{
			this.TableName = tableName;
			this.Columns = this.GetColumnDefinitions().ToList<ColumnDefinition>();
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x0000682E File Offset: 0x00004A2E
		protected TableSchema(string name, IEnumerable<ColumnDefinition> columns)
		{
			this.TableName = name;
			this.Columns = columns;
		}

		// Token: 0x060001F4 RID: 500
		protected abstract IEnumerable<ColumnDefinition> GetColumnDefinitions();

		// Token: 0x060001F5 RID: 501 RVA: 0x00006844 File Offset: 0x00004A44
		public DataTable CreateTableSchema()
		{
			DataTable dataTable = new DataTable(this.TableName);
			dataTable.Columns.AddRange(this.Columns.Select((ColumnDefinition col) => col.GetDataColumn()).ToArray<DataColumn>());
			return dataTable;
		}
	}
}
