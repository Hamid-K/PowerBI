using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A32 RID: 2610
	internal class DrdaSchemaResultSet : DrdaResultSet
	{
		// Token: 0x060051A5 RID: 20901 RVA: 0x0014D627 File Offset: 0x0014B827
		public DrdaSchemaResultSet(IResultSet resultSet, DrdaConnection connection, ISqlStatement statement, bool schemaOnly, DrdaSchemaInformation schemaInformation)
			: base(resultSet, connection, statement)
		{
			this.ColumnMapping = null;
			this._queryResult = new DrdaClientResultSet(resultSet, connection, statement, schemaOnly);
			this._schemaInformation = schemaInformation;
		}

		// Token: 0x060051A6 RID: 20902 RVA: 0x0014D654 File Offset: 0x0014B854
		public void Initialize()
		{
			this._queryResult.Initialize();
			this._columns = this.CreateColumns(null);
			HostType hostType = DrdaSchemaQuery.GetHostType(base.Connection);
			this._isUDB = hostType != HostType.DB2 && hostType != HostType.MVS && hostType != HostType.AS400;
			if (this._isUDB)
			{
				this._rowQue = new Queue<DrdaSchemaColumnBinding[]>();
			}
		}

		// Token: 0x060051A7 RID: 20903 RVA: 0x0014D6B0 File Offset: 0x0014B8B0
		public override bool HasData()
		{
			return this._queryResult.HasData();
		}

		// Token: 0x170013B2 RID: 5042
		// (get) Token: 0x060051A8 RID: 20904 RVA: 0x0014D6BD File Offset: 0x0014B8BD
		public override bool HasRows
		{
			get
			{
				return (this._isUDB && this._rowQue.Count > 0) || this._queryResult.HasRows;
			}
		}

		// Token: 0x170013B3 RID: 5043
		// (get) Token: 0x060051A9 RID: 20905 RVA: 0x0014D6E2 File Offset: 0x0014B8E2
		// (set) Token: 0x060051AA RID: 20906 RVA: 0x0014D6EA File Offset: 0x0014B8EA
		public int[] ColumnMapping { get; private set; }

		// Token: 0x060051AB RID: 20907 RVA: 0x0014D6F3 File Offset: 0x0014B8F3
		public override bool HasSchema()
		{
			return this._queryResult.HasSchema();
		}

		// Token: 0x060051AC RID: 20908 RVA: 0x0014D700 File Offset: 0x0014B900
		public override DrdaColumnBinding GetColumn(int i)
		{
			return this._columns[i];
		}

		// Token: 0x060051AD RID: 20909 RVA: 0x0014D70C File Offset: 0x0014B90C
		public override async Task<bool> ReadAsync(QueryScrollOrientation orientation, long number, bool isAsync, CancellationToken cancellationToken)
		{
			if (this._hasColumns == null)
			{
				this._hasColumns = new bool[this.FieldCount];
				for (int i = 0; i < this.FieldCount; i++)
				{
					this._hasColumns[i] = true;
				}
			}
			bool flag;
			if (this.ReadQueue())
			{
				flag = true;
			}
			else
			{
				bool flag2 = await this._queryResult.ReadAsync(orientation, number, isAsync, cancellationToken);
				if (flag2)
				{
					this._schemaInformation.GetResultValues(base.Connection, this, this._hasColumns);
					if (this.ReadQueue())
					{
						return true;
					}
				}
				flag = flag2;
			}
			return flag;
		}

		// Token: 0x170013B4 RID: 5044
		// (get) Token: 0x060051AE RID: 20910 RVA: 0x0014D772 File Offset: 0x0014B972
		public override int FieldCount
		{
			get
			{
				return this._schemaInformation.ResultColumns.Length;
			}
		}

		// Token: 0x170013B5 RID: 5045
		// (get) Token: 0x060051AF RID: 20911 RVA: 0x0014D781 File Offset: 0x0014B981
		internal DrdaResultSet QueryResultSet
		{
			get
			{
				return this._queryResult;
			}
		}

		// Token: 0x170013B6 RID: 5046
		// (get) Token: 0x060051B0 RID: 20912 RVA: 0x0014D789 File Offset: 0x0014B989
		internal DrdaSchemaColumnBinding[] Columns
		{
			get
			{
				return this._columns;
			}
		}

		// Token: 0x060051B1 RID: 20913 RVA: 0x0014D791 File Offset: 0x0014B991
		private bool ReadQueue()
		{
			if (this._isUDB && this._rowQue.Count > 0)
			{
				this._columns = this._rowQue.Dequeue();
				return true;
			}
			return false;
		}

		// Token: 0x060051B2 RID: 20914 RVA: 0x0014D7C0 File Offset: 0x0014B9C0
		internal DrdaSchemaColumnBinding[] CreateColumns(DrdaSchemaColumnBinding[] sourceColumns)
		{
			this.ColumnMapping = new int[this._schemaInformation.ResultColumns.Length];
			DrdaSchemaColumnBinding[] array = new DrdaSchemaColumnBinding[this._schemaInformation.ResultColumns.Length];
			for (int i = 0; i < this._schemaInformation.ResultColumns.Length; i++)
			{
				array[i] = new DrdaSchemaColumnBinding(this._schemaInformation.ResultColumns[i]);
				array[i].Initialize();
				if (sourceColumns != null && sourceColumns.Length > i)
				{
					array[i].Value = sourceColumns[i].Value;
				}
				this.ColumnMapping[i] = -1;
			}
			return array;
		}

		// Token: 0x060051B3 RID: 20915 RVA: 0x0014D851 File Offset: 0x0014BA51
		internal void AddRow(DrdaSchemaColumnBinding[] row)
		{
			if (this._isUDB)
			{
				this._rowQue.Enqueue(row);
			}
		}

		// Token: 0x060051B4 RID: 20916 RVA: 0x0014D868 File Offset: 0x0014BA68
		internal int GetColumnMappingIndex(int index, string columnName, int defaultOrdinal)
		{
			if (this.ColumnMapping[index] == -1)
			{
				int num = 0;
				if (!this._queryResult.TryGetOrdinal(columnName, out num))
				{
					num = defaultOrdinal;
				}
				this.ColumnMapping[index] = num;
			}
			return this.ColumnMapping[index];
		}

		// Token: 0x04004045 RID: 16453
		private DrdaSchemaInformation _schemaInformation;

		// Token: 0x04004046 RID: 16454
		private DrdaClientResultSet _queryResult;

		// Token: 0x04004047 RID: 16455
		private DrdaSchemaColumnBinding[] _columns;

		// Token: 0x04004048 RID: 16456
		private bool[] _hasColumns;

		// Token: 0x04004049 RID: 16457
		private bool _isUDB;

		// Token: 0x0400404A RID: 16458
		private Queue<DrdaSchemaColumnBinding[]> _rowQue;
	}
}
