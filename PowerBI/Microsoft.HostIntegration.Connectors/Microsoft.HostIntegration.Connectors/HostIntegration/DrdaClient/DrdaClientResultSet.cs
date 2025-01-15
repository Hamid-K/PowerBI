using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A05 RID: 2565
	internal class DrdaClientResultSet : DrdaResultSet
	{
		// Token: 0x060050D0 RID: 20688 RVA: 0x001437F2 File Offset: 0x001419F2
		public DrdaClientResultSet(IResultSet resultSet, DrdaConnection connection, ISqlStatement statement, bool schemaOnly)
			: base(resultSet, connection, statement)
		{
			this._schemaOnly = schemaOnly;
		}

		// Token: 0x060050D1 RID: 20689 RVA: 0x0014380C File Offset: 0x00141A0C
		public void Initialize()
		{
			IColumnInfo[] columnInfos = base.ResultSet.ColumnInfos;
			this._numColumns = columnInfos.Length;
			Trace.MessageVerboseTrace(Trace.GetTracePoint(base.Connection), "DrdaDrdaResultSet(): column count = {0}", this._numColumns);
			if (this._numColumns > 0)
			{
				this._columns = new DrdaColumnBinding[this._numColumns];
				Trace.MessageVerboseTrace(Trace.GetTracePoint(base.Connection), "DrdaDrdaResultSet(): begin initialize columns.");
				for (int i = 0; i < this._numColumns; i++)
				{
					this._columns[i] = new DrdaColumnBinding();
					this._columns[i].Initialize(columnInfos[i]);
				}
				Trace.MessageVerboseTrace(Trace.GetTracePoint(base.Connection), "DrdaDrdaResultSet(): end initialize columns.");
			}
		}

		// Token: 0x060050D2 RID: 20690 RVA: 0x001438C1 File Offset: 0x00141AC1
		public override void Close()
		{
			this._rowPrefetchedFromServer = false;
			this._readAtLeastOnce = false;
			this._hasData = false;
			this._hasSchema = false;
		}

		// Token: 0x060050D3 RID: 20691 RVA: 0x001438DF File Offset: 0x00141ADF
		public override bool HasData()
		{
			return this._hasData;
		}

		// Token: 0x060050D4 RID: 20692 RVA: 0x001438E7 File Offset: 0x00141AE7
		public override bool HasSchema()
		{
			return this._hasSchema;
		}

		// Token: 0x17001396 RID: 5014
		// (get) Token: 0x060050D5 RID: 20693 RVA: 0x001438EF File Offset: 0x00141AEF
		public override int FieldCount
		{
			get
			{
				return this._numColumns;
			}
		}

		// Token: 0x060050D6 RID: 20694 RVA: 0x001438F7 File Offset: 0x00141AF7
		public override DrdaColumnBinding GetColumn(int i)
		{
			return this._columns[i];
		}

		// Token: 0x17001397 RID: 5015
		// (get) Token: 0x060050D7 RID: 20695 RVA: 0x00143904 File Offset: 0x00141B04
		public override bool HasRows
		{
			get
			{
				bool flag = this._hasData;
				if (!this._rowPrefetchedFromServer && !this._readAtLeastOnce)
				{
					flag = this.ReadInternalAsync(QueryScrollOrientation.Next, 0L, false, CancellationToken.None).GetAwaiter().GetResult();
					this._rowPrefetchedFromServer = true;
				}
				return flag;
			}
		}

		// Token: 0x060050D8 RID: 20696 RVA: 0x00143950 File Offset: 0x00141B50
		public override async Task<bool> ReadAsync(QueryScrollOrientation orientation, long number, bool isAsync, CancellationToken cancellationToken)
		{
			bool flag = this._hasData;
			if (!this._rowPrefetchedFromServer || orientation != QueryScrollOrientation.Next)
			{
				flag = await this.ReadInternalAsync(orientation, number, isAsync, cancellationToken);
				this._readAtLeastOnce = true;
			}
			this._rowPrefetchedFromServer = false;
			return flag;
		}

		// Token: 0x060050D9 RID: 20697 RVA: 0x001439B8 File Offset: 0x00141BB8
		private async Task<bool> ReadInternalAsync(QueryScrollOrientation orientation, long number, bool isAsync, CancellationToken cancellationToken)
		{
			bool flag;
			if (this._schemaOnly)
			{
				flag = false;
			}
			else
			{
				IResultSet resultSet = base.ResultSet;
				bool flag2 = await resultSet.ReadRowAsync(orientation, number, isAsync, cancellationToken);
				this._hasData = flag2;
				this._readAtLeastOnce = true;
				if (!this._hasData)
				{
					flag = false;
				}
				else
				{
					Trace.MessageVerboseTrace(Trace.GetTracePoint(base.Connection), "DrdaDrdaResultSet(): begin getting column data");
					ushort i = 0;
					while ((int)i < this._numColumns)
					{
						await this._columns[(int)i].GetData(resultSet, i, isAsync, cancellationToken);
						i += 1;
					}
					Trace.MessageVerboseTrace(Trace.GetTracePoint(base.Connection), "DrdaDrdaResultSet(): end getting column data");
					if (resultSet.RowsCount == 0)
					{
						this._hasData = false;
					}
					flag = true;
				}
			}
			return flag;
		}

		// Token: 0x04003F68 RID: 16232
		private int _numColumns;

		// Token: 0x04003F69 RID: 16233
		private DrdaColumnBinding[] _columns;

		// Token: 0x04003F6A RID: 16234
		private bool _hasData;

		// Token: 0x04003F6B RID: 16235
		private bool _hasSchema = true;

		// Token: 0x04003F6C RID: 16236
		private bool _rowPrefetchedFromServer;

		// Token: 0x04003F6D RID: 16237
		private bool _readAtLeastOnce;

		// Token: 0x04003F6E RID: 16238
		private bool _schemaOnly;
	}
}
