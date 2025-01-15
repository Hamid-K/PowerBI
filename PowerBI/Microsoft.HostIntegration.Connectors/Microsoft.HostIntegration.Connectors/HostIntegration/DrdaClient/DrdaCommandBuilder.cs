using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x020009D3 RID: 2515
	public sealed class DrdaCommandBuilder : DbCommandBuilder
	{
		// Token: 0x06004D9D RID: 19869 RVA: 0x0013835E File Offset: 0x0013655E
		public DrdaCommandBuilder()
			: this(null, false)
		{
		}

		// Token: 0x06004D9E RID: 19870 RVA: 0x00138368 File Offset: 0x00136568
		public DrdaCommandBuilder(bool disableOptimisticConcurrency)
			: this(null, disableOptimisticConcurrency)
		{
		}

		// Token: 0x06004D9F RID: 19871 RVA: 0x00138372 File Offset: 0x00136572
		public DrdaCommandBuilder(DrdaDataAdapter adapter)
			: this(adapter, false)
		{
			base.DataAdapter = adapter;
		}

		// Token: 0x06004DA0 RID: 19872 RVA: 0x00138383 File Offset: 0x00136583
		public DrdaCommandBuilder(DrdaDataAdapter adapter, bool disableOptimisticConcurrency)
		{
			base.DataAdapter = adapter;
			this.DisableOptimisticConcurrency = disableOptimisticConcurrency;
		}

		// Token: 0x170012B3 RID: 4787
		// (get) Token: 0x06004DA1 RID: 19873 RVA: 0x001383AF File Offset: 0x001365AF
		// (set) Token: 0x06004DA2 RID: 19874 RVA: 0x001383B7 File Offset: 0x001365B7
		public override string QuotePrefix
		{
			get
			{
				return this._quotePrefix;
			}
			set
			{
				this._quotePrefix = value;
			}
		}

		// Token: 0x170012B4 RID: 4788
		// (get) Token: 0x06004DA3 RID: 19875 RVA: 0x001383C0 File Offset: 0x001365C0
		// (set) Token: 0x06004DA4 RID: 19876 RVA: 0x001383C8 File Offset: 0x001365C8
		public override string QuoteSuffix
		{
			get
			{
				return this._quoteSuffix;
			}
			set
			{
				this._quoteSuffix = value;
			}
		}

		// Token: 0x170012B5 RID: 4789
		// (get) Token: 0x06004DA5 RID: 19877 RVA: 0x001383D1 File Offset: 0x001365D1
		// (set) Token: 0x06004DA6 RID: 19878 RVA: 0x001383D9 File Offset: 0x001365D9
		public bool DisableOptimisticConcurrency
		{
			get
			{
				return this._disableOptimisticConcurrency;
			}
			set
			{
				this._disableOptimisticConcurrency = value;
			}
		}

		// Token: 0x06004DA7 RID: 19879 RVA: 0x001383E4 File Offset: 0x001365E4
		protected override DataTable GetSchemaTable(DbCommand sourceCommand)
		{
			DataTable schemaTable = base.GetSchemaTable(sourceCommand);
			if (this.DisableOptimisticConcurrency)
			{
				bool flag = false;
				int num = 0;
				while (num < schemaTable.Rows.Count && !flag)
				{
					if ((bool)schemaTable.Rows[num][SchemaTableColumn.IsKey])
					{
						flag = true;
					}
					num++;
				}
				if (!flag)
				{
					schemaTable.Columns[SchemaTableColumn.IsKey].ReadOnly = false;
					for (int i = 0; i < schemaTable.Rows.Count; i++)
					{
						schemaTable.Rows[i][SchemaTableColumn.IsKey] = true;
					}
					schemaTable.Columns[SchemaTableColumn.IsKey].ReadOnly = true;
				}
			}
			return schemaTable;
		}

		// Token: 0x06004DA8 RID: 19880 RVA: 0x001384A0 File Offset: 0x001366A0
		protected override void ApplyParameterInfo(DbParameter parameter, DataRow row, StatementType statementType, bool whereClause)
		{
			DrdaParameter drdaParameter = (DrdaParameter)parameter;
			object obj = row[SchemaTableColumn.ProviderType];
			drdaParameter.DrdaType = DataTypeConverter.ToDrdaType((DrdaClientType)obj);
			object obj2 = row[SchemaTableColumn.NumericPrecision];
			if (DBNull.Value != obj2)
			{
				byte b = (byte)((short)obj2);
				drdaParameter.Precision = ((byte.MaxValue != b) ? b : 0);
			}
			obj2 = row[SchemaTableColumn.NumericScale];
			if (DBNull.Value != obj2)
			{
				byte b2 = (byte)((short)obj2);
				drdaParameter.Scale = ((byte.MaxValue != b2) ? b2 : 0);
			}
			obj2 = row[SchemaTableColumn.AllowDBNull];
			if (DBNull.Value != obj2)
			{
				drdaParameter.IsNullable = (bool)obj2;
			}
		}

		// Token: 0x06004DA9 RID: 19881 RVA: 0x00138550 File Offset: 0x00136750
		protected override string GetParameterName(int parameterOrdinal)
		{
			return DrdaCommandBuilder.CreateParameterName(parameterOrdinal);
		}

		// Token: 0x06004DAA RID: 19882 RVA: 0x00138558 File Offset: 0x00136758
		private static string CreateParameterName(int parameterOrdinal)
		{
			return "p" + parameterOrdinal.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06004DAB RID: 19883 RVA: 0x00028FA6 File Offset: 0x000271A6
		protected override string GetParameterName(string parameterName)
		{
			return parameterName;
		}

		// Token: 0x06004DAC RID: 19884 RVA: 0x00138570 File Offset: 0x00136770
		protected override string GetParameterPlaceholder(int parameterOrdinal)
		{
			return "?";
		}

		// Token: 0x06004DAD RID: 19885 RVA: 0x00138577 File Offset: 0x00136777
		protected override void SetRowUpdatingHandler(DbDataAdapter adapter)
		{
			if (adapter == base.DataAdapter)
			{
				((DrdaDataAdapter)adapter).RowUpdating -= this.DrdaRowUpdatingHandler;
				return;
			}
			((DrdaDataAdapter)adapter).RowUpdating += this.DrdaRowUpdatingHandler;
		}

		// Token: 0x06004DAE RID: 19886 RVA: 0x001385B1 File Offset: 0x001367B1
		private void DrdaRowUpdatingHandler(object sender, DrdaRowUpdatingEventArgs ruevent)
		{
			base.RowUpdatingHandler(ruevent);
		}

		// Token: 0x06004DAF RID: 19887 RVA: 0x001385BC File Offset: 0x001367BC
		public static void DeriveParameters(DrdaCommand cmd)
		{
			DrdaCommandBuilder.InternalDeriveParametersAsync(cmd, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x06004DB0 RID: 19888 RVA: 0x001385E2 File Offset: 0x001367E2
		public static Task DeriveParametersAsync(DrdaCommand cmd, CancellationToken cancellationToken)
		{
			return DrdaCommandBuilder.InternalDeriveParametersAsync(cmd, true, cancellationToken);
		}

		// Token: 0x06004DB1 RID: 19889 RVA: 0x001385EC File Offset: 0x001367EC
		private static async Task InternalDeriveParametersAsync(DrdaCommand cmd, bool isAsync, CancellationToken cancellationToken)
		{
			await cmd.InternalPrepareAsync(isAsync, cancellationToken);
			IList<ISqlParameter> list = await cmd.Statement.GetParametersAsync(cmd.CommandText, isAsync, cancellationToken);
			ushort num = 1;
			while ((int)num <= list.Count)
			{
				DrdaParameter drdaParameter = null;
				if (cmd.Parameters.Count > (int)num)
				{
					drdaParameter = cmd.Parameters[(int)(num - 1)];
				}
				if (drdaParameter == null)
				{
					drdaParameter = new DrdaParameter();
					cmd.Parameters.Add(drdaParameter);
				}
				drdaParameter.ParameterName = DrdaCommandBuilder.CreateParameterName((int)num);
				drdaParameter.Derive(list[(int)(num - 1)]);
				num += 1;
			}
		}

		// Token: 0x04003DA5 RID: 15781
		private bool _disableOptimisticConcurrency;

		// Token: 0x04003DA6 RID: 15782
		private string _quotePrefix = "";

		// Token: 0x04003DA7 RID: 15783
		private string _quoteSuffix = "";
	}
}
