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
		// Token: 0x060005C8 RID: 1480 RVA: 0x00021805 File Offset: 0x0001FA05
		public AdomdDataAdapter()
		{
		}

		// Token: 0x060005C9 RID: 1481 RVA: 0x0002180D File Offset: 0x0001FA0D
		public AdomdDataAdapter(AdomdCommand selectCommand)
		{
			this.SelectCommand = selectCommand;
		}

		// Token: 0x060005CA RID: 1482 RVA: 0x0002181C File Offset: 0x0001FA1C
		public AdomdDataAdapter(string selectCommandText, AdomdConnection selectConnection)
			: this(new AdomdCommand(selectCommandText, selectConnection))
		{
		}

		// Token: 0x060005CB RID: 1483 RVA: 0x0002182B File Offset: 0x0001FA2B
		public AdomdDataAdapter(string selectCommandText, string selectConnectionString)
			: this(selectCommandText, new AdomdConnection(selectConnectionString))
		{
		}

		// Token: 0x17000170 RID: 368
		// (get) Token: 0x060005CC RID: 1484 RVA: 0x0002183A File Offset: 0x0001FA3A
		// (set) Token: 0x060005CD RID: 1485 RVA: 0x00021847 File Offset: 0x0001FA47
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

		// Token: 0x060005CE RID: 1486 RVA: 0x00021850 File Offset: 0x0001FA50
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

		// Token: 0x060005CF RID: 1487 RVA: 0x00021880 File Offset: 0x0001FA80
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

		// Token: 0x060005D0 RID: 1488 RVA: 0x000218B4 File Offset: 0x0001FAB4
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

		// Token: 0x060005D1 RID: 1489 RVA: 0x000218E9 File Offset: 0x0001FAE9
		public override int Update(DataSet dataSet)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005D2 RID: 1490 RVA: 0x000218F0 File Offset: 0x0001FAF0
		protected override int Update(DataRow[] dataRows, DataTableMapping tableMapping)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005D3 RID: 1491 RVA: 0x000218F7 File Offset: 0x0001FAF7
		protected override RowUpdatedEventArgs CreateRowUpdatedEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005D4 RID: 1492 RVA: 0x000218FE File Offset: 0x0001FAFE
		protected override RowUpdatingEventArgs CreateRowUpdatingEvent(DataRow dataRow, IDbCommand command, StatementType statementType, DataTableMapping tableMapping)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005D5 RID: 1493 RVA: 0x00021905 File Offset: 0x0001FB05
		protected override void OnRowUpdating(RowUpdatingEventArgs value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x0002190C File Offset: 0x0001FB0C
		protected override void OnRowUpdated(RowUpdatedEventArgs value)
		{
			throw new NotSupportedException();
		}

		// Token: 0x17000171 RID: 369
		// (get) Token: 0x060005D7 RID: 1495 RVA: 0x00021913 File Offset: 0x0001FB13
		// (set) Token: 0x060005D8 RID: 1496 RVA: 0x00021916 File Offset: 0x0001FB16
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

		// Token: 0x17000172 RID: 370
		// (get) Token: 0x060005D9 RID: 1497 RVA: 0x00021921 File Offset: 0x0001FB21
		// (set) Token: 0x060005DA RID: 1498 RVA: 0x00021924 File Offset: 0x0001FB24
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

		// Token: 0x17000173 RID: 371
		// (get) Token: 0x060005DB RID: 1499 RVA: 0x0002192F File Offset: 0x0001FB2F
		// (set) Token: 0x060005DC RID: 1500 RVA: 0x00021932 File Offset: 0x0001FB32
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

		// Token: 0x060005DD RID: 1501 RVA: 0x0002193D File Offset: 0x0001FB3D
		private void AdjustDataTableName(DataTable dataTable)
		{
			if (this.dataReader != null)
			{
				this.AdjustDataTableNames(new DataTable[] { dataTable });
			}
		}

		// Token: 0x060005DE RID: 1502 RVA: 0x00021958 File Offset: 0x0001FB58
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

		// Token: 0x060005DF RID: 1503 RVA: 0x000219CC File Offset: 0x0001FBCC
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

		// Token: 0x060005E0 RID: 1504 RVA: 0x00021A78 File Offset: 0x0001FC78
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

		// Token: 0x04000436 RID: 1078
		private AdomdDataReader dataReader;

		// Token: 0x04000437 RID: 1079
		private bool isAffectedObjects;

		// Token: 0x04000438 RID: 1080
		private string AffectedObjectsDatabase;

		// Token: 0x04000439 RID: 1081
		private int AffectedObjectsBaseVersion;

		// Token: 0x0400043A RID: 1082
		private int AffectedObjectsCurrentVersion;
	}
}
