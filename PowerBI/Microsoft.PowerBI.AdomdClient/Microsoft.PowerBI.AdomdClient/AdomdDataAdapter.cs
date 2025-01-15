using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;

namespace Microsoft.AnalysisServices.AdomdClient
{
	// Token: 0x02000059 RID: 89
	[Designer(typeof(AdomdDataAdapterDesigner))]
	public sealed class AdomdDataAdapter : DbDataAdapter, IDbDataAdapter, IDataAdapter, IDataReaderConsumer
	{
		// Token: 0x060005BB RID: 1467 RVA: 0x000214D5 File Offset: 0x0001F6D5
		public AdomdDataAdapter()
		{
		}

		// Token: 0x060005BC RID: 1468 RVA: 0x000214DD File Offset: 0x0001F6DD
		public AdomdDataAdapter(AdomdCommand selectCommand)
		{
			this.SelectCommand = selectCommand;
		}

		// Token: 0x060005BD RID: 1469 RVA: 0x000214EC File Offset: 0x0001F6EC
		public AdomdDataAdapter(string selectCommandText, AdomdConnection selectConnection)
			: this(new AdomdCommand(selectCommandText, selectConnection))
		{
		}

		// Token: 0x060005BE RID: 1470 RVA: 0x000214FB File Offset: 0x0001F6FB
		public AdomdDataAdapter(string selectCommandText, string selectConnectionString)
			: this(selectCommandText, new AdomdConnection(selectConnectionString))
		{
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x060005BF RID: 1471 RVA: 0x0002150A File Offset: 0x0001F70A
		// (set) Token: 0x060005C0 RID: 1472 RVA: 0x00021517 File Offset: 0x0001F717
		public new AdomdCommand SelectCommand
		{
			get
			{
				return (AdomdCommand)((IDbDataAdapter)this).SelectCommand;
			}
			set
			{
				((IDbDataAdapter)this).SelectCommand = value;
			}
		}

		// Token: 0x060005C1 RID: 1473 RVA: 0x00021520 File Offset: 0x0001F720
		protected override int Fill(DataTable dataTable, IDbCommand command, CommandBehavior behavior)
		{
			AdomdCommand adomdCommand = command as AdomdCommand;
			if (adomdCommand != null)
			{
				adomdCommand.DataReaderConsumer = this;
			}
			int num = base.Fill(dataTable, command, behavior);
			this.AdjustDataTableName(dataTable);
			return num;
		}

		// Token: 0x060005C2 RID: 1474 RVA: 0x00021550 File Offset: 0x0001F750
		protected override int Fill(DataTable[] dataTables, int startRecord, int maxRecords, IDbCommand command, CommandBehavior behavior)
		{
			AdomdCommand adomdCommand = command as AdomdCommand;
			if (adomdCommand != null)
			{
				adomdCommand.DataReaderConsumer = this;
			}
			int num = base.Fill(dataTables, startRecord, maxRecords, command, behavior);
			this.AdjustDataTableNames(dataTables);
			return num;
		}

		// Token: 0x060005C3 RID: 1475 RVA: 0x00021584 File Offset: 0x0001F784
		protected override int Fill(DataSet dataSet, int startRecord, int maxRecords, string srcTable, IDbCommand command, CommandBehavior behavior)
		{
			AdomdCommand adomdCommand = command as AdomdCommand;
			if (adomdCommand != null)
			{
				adomdCommand.DataReaderConsumer = this;
			}
			int num = base.Fill(dataSet, startRecord, maxRecords, srcTable, command, behavior);
			this.AdjustDataSet(dataSet);
			return num;
		}

		// Token: 0x060005C4 RID: 1476 RVA: 0x000215B9 File Offset: 0x0001F7B9
		public override int Update(DataSet dataSet)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005C5 RID: 1477 RVA: 0x000215C0 File Offset: 0x0001F7C0
		protected override int Update(DataRow[] dataRows, DataTableMapping tableMapping)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005C6 RID: 1478 RVA: 0x000215C7 File Offset: 0x0001F7C7
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005C7 RID: 1479 RVA: 0x000215CE File Offset: 0x0001F7CE
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005C8 RID: 1480 RVA: 0x000215D5 File Offset: 0x0001F7D5
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x000215DC File Offset: 0x0001F7DC
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x060005CA RID: 1482 RVA: 0x000215E3 File Offset: 0x0001F7E3
		// (set) Token: 0x060005CB RID: 1483 RVA: 0x000215E6 File Offset: 0x0001F7E6
		IDbCommand IDbDataAdapter.DeleteCommand
		{
			get
			{
				return null;
			}
			set
			{
				if (value != null)
				{
					throw new NotSupportedException();
				}
			}
		}

		// Token: 0x1700016C RID: 364
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x000215F1 File Offset: 0x0001F7F1
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x000215F4 File Offset: 0x0001F7F4
		IDbCommand IDbDataAdapter.UpdateCommand
		{
			get
			{
				return null;
			}
			set
			{
				if (value != null)
				{
					throw new NotSupportedException();
				}
			}
		}

		// Token: 0x1700016D RID: 365
		// (get) Token: 0x060005CE RID: 1486 RVA: 0x000215FF File Offset: 0x0001F7FF
		// (set) Token: 0x060005CF RID: 1487 RVA: 0x00021602 File Offset: 0x0001F802
		IDbCommand IDbDataAdapter.InsertCommand
		{
			get
			{
				return null;
			}
			set
			{
				if (value != null)
				{
					throw new NotSupportedException();
				}
			}
		}

		// Token: 0x060005D0 RID: 1488 RVA: 0x0002160D File Offset: 0x0001F80D
		private void AdjustDataTableName(DataTable dataTable)
		{
			if (this.dataReader != null)
			{
				this.AdjustDataTableNames(new DataTable[] { dataTable });
			}
		}

		// Token: 0x060005D1 RID: 1489 RVA: 0x00021628 File Offset: 0x0001F828
		private void AdjustDataTableNames(DataTable[] dataTables)
		{
			XmlaDataReader xmlaDataReader = ((this.dataReader != null) ? this.dataReader.XmlaDataReader : null);
			if (xmlaDataReader != null && xmlaDataReader.RowsetNames != null && xmlaDataReader.RowsetNames.Count == dataTables.Length)
			{
				for (int i = 0; i < dataTables.Length; i++)
				{
					if (!string.IsNullOrEmpty(xmlaDataReader.RowsetNames[i]))
					{
						dataTables[i].TableName = xmlaDataReader.RowsetNames[i];
					}
				}
			}
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x0002169C File Offset: 0x0001F89C
		private void AdjustDataSet(DataSet dataSet)
		{
			if (((this.dataReader != null) ? this.dataReader.XmlaDataReader : null) != null)
			{
				DataTable[] array = new DataTable[dataSet.Tables.Count];
				dataSet.Tables.CopyTo(array, 0);
				this.AdjustDataTableNames(array);
			}
			if (this.isAffectedObjects)
			{
				dataSet.DataSetName = "AffectedObjects";
				dataSet.ExtendedProperties["BaseVersion"] = this.AffectedObjectsBaseVersion;
				dataSet.ExtendedProperties["CurrentVersion"] = this.AffectedObjectsCurrentVersion;
				dataSet.ExtendedProperties["name"] = this.AffectedObjectsDatabase;
			}
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x00021748 File Offset: 0x0001F948
		void IDataReaderConsumer.SetDataReader(AdomdDataReader reader)
		{
			this.dataReader = reader;
			AdomdAffectedObjectsReader adomdAffectedObjectsReader = reader as AdomdAffectedObjectsReader;
			if (adomdAffectedObjectsReader != null)
			{
				this.isAffectedObjects = true;
				this.AffectedObjectsDatabase = adomdAffectedObjectsReader.Database;
				this.AffectedObjectsBaseVersion = adomdAffectedObjectsReader.BaseVersion;
				this.AffectedObjectsCurrentVersion = adomdAffectedObjectsReader.CurrentVersion;
			}
		}

		// Token: 0x04000429 RID: 1065
		private AdomdDataReader dataReader;

		// Token: 0x0400042A RID: 1066
		private bool isAffectedObjects;

		// Token: 0x0400042B RID: 1067
		private string AffectedObjectsDatabase;

		// Token: 0x0400042C RID: 1068
		private int AffectedObjectsBaseVersion;

		// Token: 0x0400042D RID: 1069
		private int AffectedObjectsCurrentVersion;
	}
}
