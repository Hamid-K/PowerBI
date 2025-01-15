using System;
using System.ComponentModel;
using System.Data;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.HostIntegration.Drda.Common;
using Microsoft.HostIntegration.Drda.Requester;

namespace Microsoft.HostIntegration.DrdaClient
{
	// Token: 0x02000A0A RID: 2570
	internal sealed class DrdaSchemaCommand : DbCommand
	{
		// Token: 0x1700139A RID: 5018
		// (get) Token: 0x060050EB RID: 20715 RVA: 0x00143E9D File Offset: 0x0014209D
		internal ISqlStatement Statement
		{
			get
			{
				return this._statement;
			}
		}

		// Token: 0x1700139B RID: 5019
		// (get) Token: 0x060050EC RID: 20716 RVA: 0x00143EA5 File Offset: 0x001420A5
		internal IRequester Requester
		{
			get
			{
				return this.Connection.Requester;
			}
		}

		// Token: 0x1700139C RID: 5020
		// (get) Token: 0x060050ED RID: 20717 RVA: 0x00143EB2 File Offset: 0x001420B2
		// (set) Token: 0x060050EE RID: 20718 RVA: 0x00143EBA File Offset: 0x001420BA
		internal string Options { get; set; }

		// Token: 0x060050EF RID: 20719 RVA: 0x00143EC4 File Offset: 0x001420C4
		public DrdaSchemaCommand()
		{
			this._connection = null;
			this._transaction = null;
			this._dataReader = null;
			this.Options = null;
			GC.SuppressFinalize(this);
		}

		// Token: 0x060050F0 RID: 20720 RVA: 0x00143F12 File Offset: 0x00142112
		public DrdaSchemaCommand(string commandText, DrdaConnection connection, string options)
			: this()
		{
			this.CommandText = commandText;
			this.Options = options;
			this.Connection = connection;
			this.Transaction = connection.Transaction;
		}

		// Token: 0x1700139D RID: 5021
		// (get) Token: 0x060050F1 RID: 20721 RVA: 0x00143F3B File Offset: 0x0014213B
		// (set) Token: 0x060050F2 RID: 20722 RVA: 0x00143F43 File Offset: 0x00142143
		public override bool DesignTimeVisible
		{
			get
			{
				return this._designTimeVisible;
			}
			set
			{
				this._designTimeVisible = value;
				TypeDescriptor.Refresh(this);
			}
		}

		// Token: 0x1700139E RID: 5022
		// (get) Token: 0x060050F3 RID: 20723 RVA: 0x00143F52 File Offset: 0x00142152
		// (set) Token: 0x060050F4 RID: 20724 RVA: 0x00143F5A File Offset: 0x0014215A
		public override UpdateRowSource UpdatedRowSource
		{
			get
			{
				return this._updatedRowSource;
			}
			set
			{
				if (value <= UpdateRowSource.Both)
				{
					this._updatedRowSource = value;
					return;
				}
				throw DrdaException.InvalidUpdateRowSource((int)value);
			}
		}

		// Token: 0x060050F5 RID: 20725 RVA: 0x00143F6E File Offset: 0x0014216E
		public override void Cancel()
		{
			throw DrdaException.NotSupported("Cancel()");
		}

		// Token: 0x1700139F RID: 5023
		// (get) Token: 0x060050F6 RID: 20726 RVA: 0x00143F7A File Offset: 0x0014217A
		// (set) Token: 0x060050F7 RID: 20727 RVA: 0x00143F82 File Offset: 0x00142182
		public override string CommandText
		{
			get
			{
				return this._commandText;
			}
			set
			{
				this._commandText = value;
			}
		}

		// Token: 0x170013A0 RID: 5024
		// (get) Token: 0x060050F8 RID: 20728 RVA: 0x00143F8B File Offset: 0x0014218B
		// (set) Token: 0x060050F9 RID: 20729 RVA: 0x000036A9 File Offset: 0x000018A9
		public override int CommandTimeout
		{
			get
			{
				return 30;
			}
			set
			{
			}
		}

		// Token: 0x170013A1 RID: 5025
		// (get) Token: 0x060050FA RID: 20730 RVA: 0x00002B16 File Offset: 0x00000D16
		// (set) Token: 0x060050FB RID: 20731 RVA: 0x000036A9 File Offset: 0x000018A9
		public override CommandType CommandType
		{
			get
			{
				return CommandType.Text;
			}
			set
			{
			}
		}

		// Token: 0x170013A2 RID: 5026
		// (get) Token: 0x060050FC RID: 20732 RVA: 0x00143F8F File Offset: 0x0014218F
		// (set) Token: 0x060050FD RID: 20733 RVA: 0x00143F98 File Offset: 0x00142198
		public new DrdaConnection Connection
		{
			get
			{
				return this._connection;
			}
			set
			{
				if (this._connection != value)
				{
					if (this._connection != null)
					{
						this.ResetResourcesAsync(false, CancellationToken.None).GetAwaiter().GetResult();
					}
					this._connection = value;
				}
			}
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x060050FE RID: 20734 RVA: 0x00143FD8 File Offset: 0x001421D8
		private bool ConnectionIsClosed
		{
			get
			{
				DrdaConnection connection = this.Connection;
				return connection == null || connection.State == global::System.Data.ConnectionState.Closed;
			}
		}

		// Token: 0x170013A4 RID: 5028
		// (get) Token: 0x060050FF RID: 20735 RVA: 0x00143FFA File Offset: 0x001421FA
		// (set) Token: 0x06005100 RID: 20736 RVA: 0x00144002 File Offset: 0x00142202
		protected override DbConnection DbConnection
		{
			get
			{
				return this.Connection;
			}
			set
			{
				this.Connection = (DrdaConnection)value;
			}
		}

		// Token: 0x170013A5 RID: 5029
		// (get) Token: 0x06005101 RID: 20737 RVA: 0x00144010 File Offset: 0x00142210
		protected override DbParameterCollection DbParameterCollection
		{
			get
			{
				return this.Parameters;
			}
		}

		// Token: 0x170013A6 RID: 5030
		// (get) Token: 0x06005102 RID: 20738 RVA: 0x00144018 File Offset: 0x00142218
		// (set) Token: 0x06005103 RID: 20739 RVA: 0x00144020 File Offset: 0x00142220
		protected override DbTransaction DbTransaction
		{
			get
			{
				return this.Transaction;
			}
			set
			{
				this.Transaction = (DrdaTransaction)value;
			}
		}

		// Token: 0x170013A7 RID: 5031
		// (get) Token: 0x06005104 RID: 20740 RVA: 0x0014402E File Offset: 0x0014222E
		public new DrdaParameterCollection Parameters
		{
			get
			{
				if (this._parameterCollection == null)
				{
					this._parameterCollection = new DrdaParameterCollection();
				}
				return this._parameterCollection;
			}
		}

		// Token: 0x170013A8 RID: 5032
		// (get) Token: 0x06005105 RID: 20741 RVA: 0x00144049 File Offset: 0x00142249
		// (set) Token: 0x06005106 RID: 20742 RVA: 0x0014406D File Offset: 0x0014226D
		public new DrdaTransaction Transaction
		{
			get
			{
				if (this._transaction != null && this._transaction.IsComplete)
				{
					this._transaction = null;
				}
				return this._transaction;
			}
			set
			{
				this._transaction = value;
			}
		}

		// Token: 0x06005107 RID: 20743 RVA: 0x00134C4D File Offset: 0x00132E4D
		public new DrdaParameter CreateParameter()
		{
			return new DrdaParameter();
		}

		// Token: 0x06005108 RID: 20744 RVA: 0x00144076 File Offset: 0x00142276
		protected override DbParameter CreateDbParameter()
		{
			return this.CreateParameter();
		}

		// Token: 0x06005109 RID: 20745 RVA: 0x00006F04 File Offset: 0x00005104
		public override int ExecuteNonQuery()
		{
			return 0;
		}

		// Token: 0x0600510A RID: 20746 RVA: 0x00144080 File Offset: 0x00142280
		private async Task<DbDataReader> InternalExecuteDbDataReaderAsync(CommandBehavior behavior, bool isAsync, CancellationToken cancellationToken)
		{
			DrdaSchemaInformation schemaInformation = null;
			this.CheckExecutionConditions();
			this.CheckStatementHandle();
			int num = 0;
			while (num < DrdaSchemaCommand.SchemaList.Length && schemaInformation == null)
			{
				if (this.CommandText.Equals(DrdaSchemaCommand.SchemaList[num].SchemaName))
				{
					schemaInformation = DrdaSchemaCommand.SchemaList[num];
					this.ValidateRestrictions(schemaInformation);
					if ((this.CommandText.Equals("Columns") || this.CommandText.Equals("DataTypes")) && this.Requester.HostType != HostType.AS400 && this.Requester.Flavor != DrdaFlavor.Informix)
					{
						this.Options = "SUPPORTEDNEWTYPES=XML";
					}
					await schemaInformation.ExecuteAsync(this.Connection, this.Statement, this.Parameters, this.Options, isAsync, cancellationToken);
					break;
				}
				num++;
			}
			if (schemaInformation == null)
			{
				throw DrdaException.NoSchemaCollection(this.CommandText);
			}
			int count = this._statement.ResultSets.Count;
			int affectedRowCount = this._statement.AffectedRowCount;
			DrdaResultSet[] array;
			if (count > 0)
			{
				array = new DrdaResultSet[count];
				ushort num2 = 0;
				while ((int)num2 < count)
				{
					array[(int)num2] = new DrdaSchemaResultSet(this._statement.ResultSets[(int)num2], this.Connection, this.Statement, (behavior & CommandBehavior.SchemaOnly) == CommandBehavior.SchemaOnly, schemaInformation);
					((DrdaSchemaResultSet)array[(int)num2]).Initialize();
					num2 += 1;
				}
			}
			else
			{
				array = new DrdaResultSet[]
				{
					new DrdaEmptyResultSet()
				};
			}
			DrdaDataReader drdaDataReader = new DrdaDataReader(this.Connection, this, this.Statement, this.CommandText, behavior, affectedRowCount, array);
			this.SetDataReader(drdaDataReader);
			return drdaDataReader;
		}

		// Token: 0x0600510B RID: 20747 RVA: 0x001440DD File Offset: 0x001422DD
		protected override Task<DbDataReader> ExecuteDbDataReaderAsync(CommandBehavior behavior, CancellationToken cancellationToken)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalExecuteDbDataReaderAsync(behavior, true, cancellationToken);
		}

		// Token: 0x0600510C RID: 20748 RVA: 0x001440F8 File Offset: 0x001422F8
		protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
		{
			Trace.ApiEnterTrace(Trace.GetTracePoint(this._connection));
			return this.InternalExecuteDbDataReaderAsync(behavior, false, CancellationToken.None).GetAwaiter().GetResult();
		}

		// Token: 0x0600510D RID: 20749 RVA: 0x00003CAB File Offset: 0x00001EAB
		public override object ExecuteScalar()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600510E RID: 20750 RVA: 0x00003CAB File Offset: 0x00001EAB
		public override void Prepare()
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600510F RID: 20751 RVA: 0x00144130 File Offset: 0x00142330
		private void CheckExecutionConditions()
		{
			if (this.Connection == null || this.Connection.State != global::System.Data.ConnectionState.Open)
			{
				throw DrdaException.ClosedConnectionError();
			}
			if (this.Connection == null || this.Connection.State != global::System.Data.ConnectionState.Open)
			{
				throw DrdaException.ClosedConnectionError();
			}
			DrdaTransaction transaction = this.Transaction;
			DrdaTransaction transaction2 = this.Connection.Transaction;
			if (transaction != transaction2)
			{
				if (transaction2 != null)
				{
					throw DrdaException.NotPartOfTransaction();
				}
				if (!transaction.IsComplete)
				{
					throw DrdaException.NoActiveTransaction();
				}
				this.Transaction = null;
			}
			this.Connection.CheckExecuteTransactionIsolationLevel();
			if (this._dataReader != null)
			{
				throw DrdaException.OpenReaderExists();
			}
		}

		// Token: 0x06005110 RID: 20752 RVA: 0x001441C5 File Offset: 0x001423C5
		private void CheckStatementHandle()
		{
			if (this._statement == null)
			{
				this._statement = this.Connection.CheckOutStatement();
			}
		}

		// Token: 0x06005111 RID: 20753 RVA: 0x001441E0 File Offset: 0x001423E0
		internal async Task ReleaseStatementHandleAsync(bool isAsync, CancellationToken cancellationToken)
		{
			if (this._statement != null)
			{
				await this.Connection.CheckInStatementAsync(this._statement, isAsync, cancellationToken);
				this._statement = null;
			}
		}

		// Token: 0x06005112 RID: 20754 RVA: 0x00144235 File Offset: 0x00142435
		private void SetDataReader(DrdaDataReader reader)
		{
			this._dataReader = reader;
		}

		// Token: 0x06005113 RID: 20755 RVA: 0x00144240 File Offset: 0x00142440
		internal async Task RemoveDataReaderAsync(DrdaDataReader reader, bool isAsync, CancellationToken cancellationToken)
		{
			this._dataReader = null;
			await this.ReleaseStatementHandleAsync(isAsync, cancellationToken);
		}

		// Token: 0x06005114 RID: 20756 RVA: 0x00144298 File Offset: 0x00142498
		private void DisconnectDataReaders()
		{
			if (this._dataReader != null && !this.Statement.Equals(IntPtr.Zero))
			{
				if (this.Connection.IsClosing)
				{
					this._dataReader.Close();
				}
				else
				{
					this._statement = null;
				}
				this._dataReader = null;
			}
		}

		// Token: 0x06005115 RID: 20757 RVA: 0x001442EC File Offset: 0x001424EC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this._connection != null)
				{
					this.ResetResourcesAsync(true, CancellationToken.None).GetAwaiter().GetResult();
				}
				this._transaction = null;
				this._parameterCollection = null;
				this._connection = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06005116 RID: 20758 RVA: 0x0014433C File Offset: 0x0014253C
		private async Task ResetResourcesAsync(bool isAsync, CancellationToken cancellationToken)
		{
			this.DisconnectDataReaders();
			this._dataReader = null;
			if (this._statement != null)
			{
				await this.ReleaseStatementHandleAsync(isAsync, cancellationToken);
				this._statement = null;
			}
		}

		// Token: 0x06005117 RID: 20759 RVA: 0x00144394 File Offset: 0x00142594
		private void ValidateRestrictions(DrdaSchemaInformation schemaInformation)
		{
			DrdaSchemaRestriction[] restrictions = schemaInformation.Restrictions;
			for (int i = 0; i < restrictions.Length; i++)
			{
				if (this.Parameters.Count > i)
				{
					object obj = this.Parameters[i].Value;
					if (obj != null)
					{
						if (obj.GetType() != restrictions[i].RestrictionType)
						{
							bool flag = false;
							try
							{
								if (restrictions[i].RestrictionType == typeof(short) && obj is string)
								{
									flag = true;
									obj = short.Parse((string)obj);
								}
								else if (restrictions[i].RestrictionType == typeof(int) && obj is string)
								{
									flag = true;
									obj = int.Parse((string)obj);
								}
							}
							catch (FormatException)
							{
								throw DrdaException.BadRestrictionType(i, this.CommandText, restrictions[i].RestrictionType);
							}
							if (!flag)
							{
								throw DrdaException.BadRestrictionType(i, this.CommandText, restrictions[i].RestrictionType);
							}
							this.Parameters[i].Value = obj;
							this.Parameters[i].DrdaType = DataTypeConverter.ToDrdaType(DrdaMetaType.GetMetaTypeForType(restrictions[i].RestrictionType).DrdaType);
						}
						if (obj is string && restrictions[i].UsesMaxLength && ((string)obj).Length > restrictions[i].MaxLength)
						{
							throw DrdaException.BadRestrictionLength(i, this.CommandText, restrictions[i].MaxLength);
						}
					}
					else
					{
						this.Parameters[i].Value = schemaInformation.GetRestrictionValue(this.Connection, i);
					}
				}
				else
				{
					DrdaParameter drdaParameter = new DrdaParameter();
					drdaParameter.Value = schemaInformation.GetRestrictionValue(this.Connection, i);
					drdaParameter.DrdaType = DataTypeConverter.ToDrdaType(DrdaMetaType.GetMetaTypeForType(restrictions[i].RestrictionType).DrdaType);
					if (restrictions[i].UsesMaxLength)
					{
						drdaParameter.Size = restrictions[i].MaxLength;
					}
					this.Parameters.Add(drdaParameter);
				}
			}
			if (restrictions.Length < this.Parameters.Count)
			{
				throw DrdaException.BadRestrictionCount(this.CommandText, restrictions.Length);
			}
		}

		// Token: 0x04003F83 RID: 16259
		private DrdaConnection _connection;

		// Token: 0x04003F84 RID: 16260
		private DrdaTransaction _transaction;

		// Token: 0x04003F85 RID: 16261
		private DrdaParameterCollection _parameterCollection;

		// Token: 0x04003F86 RID: 16262
		private UpdateRowSource _updatedRowSource = UpdateRowSource.Both;

		// Token: 0x04003F87 RID: 16263
		private bool _designTimeVisible = true;

		// Token: 0x04003F88 RID: 16264
		internal static DrdaSchemaInformation[] SchemaList = new DrdaSchemaInformation[]
		{
			new DrdaSchemaTablesInformation(),
			new DrdaSchemaColumnsInformation(),
			new DrdaSchemaProceduresInformation(),
			new DrdaSchemaProcedureParametersInformation(),
			new DrdaSchemaPrimaryKeysInformation(),
			new DrdaSchemaIndexesInformation(),
			new DrdaSchemaForeignKeysInformation(),
			new DrdaSchemaDataTypesInformation(),
			new DrdaSchemaSchemasInformation()
		};

		// Token: 0x04003F89 RID: 16265
		private DrdaDataReader _dataReader;

		// Token: 0x04003F8A RID: 16266
		private ISqlStatement _statement;

		// Token: 0x04003F8B RID: 16267
		private string _commandText = string.Empty;
	}
}
