using System;
using System.Collections;
using System.Data;
using System.Data.Common;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;

namespace Microsoft.Mashup.Engine1.Library.SapBusinessWarehouse
{
	// Token: 0x02000481 RID: 1153
	internal class ImpersonatingDbProviderFactory : DelegatingDbProviderFactory
	{
		// Token: 0x06002636 RID: 9782 RVA: 0x0006EA58 File Offset: 0x0006CC58
		public ImpersonatingDbProviderFactory(DbProviderFactory dbProviderFactory, Func<IDisposable> impersonate, IEngineHost host)
			: base(dbProviderFactory)
		{
			this.dbProviderFactory = dbProviderFactory;
			this.impersonate = impersonate;
			this.host = host;
		}

		// Token: 0x06002637 RID: 9783 RVA: 0x0006EA78 File Offset: 0x0006CC78
		public override DbConnection CreateConnection()
		{
			DbConnection dbConnection;
			using (this.impersonate())
			{
				dbConnection = new ImpersonatingDbProviderFactory.ConnectionWrapper(this, this.dbProviderFactory.CreateConnection());
			}
			return dbConnection;
		}

		// Token: 0x06002638 RID: 9784 RVA: 0x0006EAC0 File Offset: 0x0006CCC0
		public override DbConnectionStringBuilder CreateConnectionStringBuilder()
		{
			DbConnectionStringBuilder dbConnectionStringBuilder;
			using (this.impersonate())
			{
				dbConnectionStringBuilder = this.dbProviderFactory.CreateConnectionStringBuilder();
			}
			return dbConnectionStringBuilder;
		}

		// Token: 0x04001000 RID: 4096
		private readonly DbProviderFactory dbProviderFactory;

		// Token: 0x04001001 RID: 4097
		private readonly Func<IDisposable> impersonate;

		// Token: 0x04001002 RID: 4098
		private readonly IEngineHost host;

		// Token: 0x02000482 RID: 1154
		private class ConnectionWrapper : DbConnection
		{
			// Token: 0x06002639 RID: 9785 RVA: 0x0006EB04 File Offset: 0x0006CD04
			public ConnectionWrapper(ImpersonatingDbProviderFactory factory, DbConnection connection)
			{
				this.factory = factory;
				this.connection = connection;
			}

			// Token: 0x17000F42 RID: 3906
			// (get) Token: 0x0600263A RID: 9786 RVA: 0x0006EB1C File Offset: 0x0006CD1C
			public DbConnection InnerConnection
			{
				get
				{
					DbConnection dbConnection;
					using (this.factory.impersonate())
					{
						dbConnection = this.connection;
					}
					return dbConnection;
				}
			}

			// Token: 0x17000F43 RID: 3907
			// (get) Token: 0x0600263B RID: 9787 RVA: 0x0006EB60 File Offset: 0x0006CD60
			public override string DataSource
			{
				get
				{
					string dataSource;
					using (this.factory.impersonate())
					{
						dataSource = this.connection.DataSource;
					}
					return dataSource;
				}
			}

			// Token: 0x17000F44 RID: 3908
			// (get) Token: 0x0600263C RID: 9788 RVA: 0x0006EBA8 File Offset: 0x0006CDA8
			public override string Database
			{
				get
				{
					string database;
					using (this.factory.impersonate())
					{
						database = this.connection.Database;
					}
					return database;
				}
			}

			// Token: 0x17000F45 RID: 3909
			// (get) Token: 0x0600263D RID: 9789 RVA: 0x0006EBF0 File Offset: 0x0006CDF0
			public override string ServerVersion
			{
				get
				{
					string serverVersion;
					using (this.factory.impersonate())
					{
						serverVersion = this.connection.ServerVersion;
					}
					return serverVersion;
				}
			}

			// Token: 0x17000F46 RID: 3910
			// (get) Token: 0x0600263E RID: 9790 RVA: 0x0006EC38 File Offset: 0x0006CE38
			public override ConnectionState State
			{
				get
				{
					ConnectionState state;
					using (this.factory.impersonate())
					{
						state = this.connection.State;
					}
					return state;
				}
			}

			// Token: 0x0600263F RID: 9791 RVA: 0x0006EC80 File Offset: 0x0006CE80
			protected override DbTransaction BeginDbTransaction(IsolationLevel isolationLevel)
			{
				DbTransaction dbTransaction;
				using (this.factory.impersonate())
				{
					dbTransaction = this.connection.BeginTransaction(isolationLevel);
				}
				return dbTransaction;
			}

			// Token: 0x06002640 RID: 9792 RVA: 0x0006ECC8 File Offset: 0x0006CEC8
			public override void ChangeDatabase(string databaseName)
			{
				using (this.factory.impersonate())
				{
					this.connection.ChangeDatabase(databaseName);
				}
			}

			// Token: 0x06002641 RID: 9793 RVA: 0x0006ED10 File Offset: 0x0006CF10
			public override void Close()
			{
				using (this.factory.impersonate())
				{
					this.connection.Close();
				}
			}

			// Token: 0x17000F47 RID: 3911
			// (get) Token: 0x06002642 RID: 9794 RVA: 0x0006ED58 File Offset: 0x0006CF58
			// (set) Token: 0x06002643 RID: 9795 RVA: 0x0006EDA0 File Offset: 0x0006CFA0
			public override string ConnectionString
			{
				get
				{
					string connectionString;
					using (this.factory.impersonate())
					{
						connectionString = this.connection.ConnectionString;
					}
					return connectionString;
				}
				set
				{
					using (this.factory.impersonate())
					{
						this.connection.ConnectionString = value;
					}
				}
			}

			// Token: 0x06002644 RID: 9796 RVA: 0x0006EDE8 File Offset: 0x0006CFE8
			protected override DbCommand CreateDbCommand()
			{
				DbCommand dbCommand;
				using (this.factory.impersonate())
				{
					dbCommand = new ImpersonatingDbProviderFactory.CommandWrapper(this.factory, this.connection.CreateCommand());
				}
				return dbCommand;
			}

			// Token: 0x06002645 RID: 9797 RVA: 0x0006EE3C File Offset: 0x0006D03C
			public override void Open()
			{
				using (this.factory.impersonate())
				{
					this.connection.Open();
				}
			}

			// Token: 0x06002646 RID: 9798 RVA: 0x0006EE84 File Offset: 0x0006D084
			public override DataTable GetSchema()
			{
				DataTable schema;
				using (this.factory.impersonate())
				{
					schema = this.connection.GetSchema();
				}
				return schema;
			}

			// Token: 0x06002647 RID: 9799 RVA: 0x0006EECC File Offset: 0x0006D0CC
			public override DataTable GetSchema(string collectionName)
			{
				DataTable schema;
				using (this.factory.impersonate())
				{
					schema = this.connection.GetSchema(collectionName);
				}
				return schema;
			}

			// Token: 0x06002648 RID: 9800 RVA: 0x0006EF14 File Offset: 0x0006D114
			public override DataTable GetSchema(string collectionName, string[] restrictionValues)
			{
				DataTable schema;
				using (this.factory.impersonate())
				{
					schema = this.connection.GetSchema(collectionName, restrictionValues);
				}
				return schema;
			}

			// Token: 0x06002649 RID: 9801 RVA: 0x0006EF60 File Offset: 0x0006D160
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					Microsoft.Mashup.Common.SafeExceptions.IgnoreSafeExceptions(this.factory.host, "ImpersonatingDbProviderFactoryWrapper/ConnectionWrapper/Dispose", delegate
					{
						using (this.factory.impersonate())
						{
							if (this.connection != null)
							{
								this.connection.Dispose();
							}
						}
					});
				}
			}

			// Token: 0x04001003 RID: 4099
			private readonly ImpersonatingDbProviderFactory factory;

			// Token: 0x04001004 RID: 4100
			private readonly DbConnection connection;
		}

		// Token: 0x02000483 RID: 1155
		private class CommandWrapper : DbCommand
		{
			// Token: 0x0600264B RID: 9803 RVA: 0x0006EFD8 File Offset: 0x0006D1D8
			public CommandWrapper(ImpersonatingDbProviderFactory factory, DbCommand command)
			{
				this.factory = factory;
				this.command = command;
			}

			// Token: 0x17000F48 RID: 3912
			// (get) Token: 0x0600264C RID: 9804 RVA: 0x0006EFF0 File Offset: 0x0006D1F0
			// (set) Token: 0x0600264D RID: 9805 RVA: 0x0006F038 File Offset: 0x0006D238
			public override string CommandText
			{
				get
				{
					string commandText;
					using (this.factory.impersonate())
					{
						commandText = this.command.CommandText;
					}
					return commandText;
				}
				set
				{
					using (this.factory.impersonate())
					{
						this.command.CommandText = value;
					}
				}
			}

			// Token: 0x17000F49 RID: 3913
			// (get) Token: 0x0600264E RID: 9806 RVA: 0x0006F080 File Offset: 0x0006D280
			// (set) Token: 0x0600264F RID: 9807 RVA: 0x0006F0C8 File Offset: 0x0006D2C8
			public override int CommandTimeout
			{
				get
				{
					int commandTimeout;
					using (this.factory.impersonate())
					{
						commandTimeout = this.command.CommandTimeout;
					}
					return commandTimeout;
				}
				set
				{
					using (this.factory.impersonate())
					{
						this.command.CommandTimeout = value;
					}
				}
			}

			// Token: 0x17000F4A RID: 3914
			// (get) Token: 0x06002650 RID: 9808 RVA: 0x0006F110 File Offset: 0x0006D310
			// (set) Token: 0x06002651 RID: 9809 RVA: 0x0006F158 File Offset: 0x0006D358
			public override CommandType CommandType
			{
				get
				{
					CommandType commandType;
					using (this.factory.impersonate())
					{
						commandType = this.command.CommandType;
					}
					return commandType;
				}
				set
				{
					using (this.factory.impersonate())
					{
						this.command.CommandType = value;
					}
				}
			}

			// Token: 0x06002652 RID: 9810 RVA: 0x0006F1A0 File Offset: 0x0006D3A0
			protected override DbParameter CreateDbParameter()
			{
				return this.command.CreateParameter();
			}

			// Token: 0x17000F4B RID: 3915
			// (get) Token: 0x06002653 RID: 9811 RVA: 0x0006F1B0 File Offset: 0x0006D3B0
			// (set) Token: 0x06002654 RID: 9812 RVA: 0x0006F1F8 File Offset: 0x0006D3F8
			protected override DbConnection DbConnection
			{
				get
				{
					DbConnection connection;
					using (this.factory.impersonate())
					{
						connection = this.command.Connection;
					}
					return connection;
				}
				set
				{
					using (this.factory.impersonate())
					{
						this.command.Connection = value;
					}
				}
			}

			// Token: 0x17000F4C RID: 3916
			// (get) Token: 0x06002655 RID: 9813 RVA: 0x0006F240 File Offset: 0x0006D440
			protected override DbParameterCollection DbParameterCollection
			{
				get
				{
					DbParameterCollection parameters;
					using (this.factory.impersonate())
					{
						parameters = this.command.Parameters;
					}
					return parameters;
				}
			}

			// Token: 0x17000F4D RID: 3917
			// (get) Token: 0x06002656 RID: 9814 RVA: 0x0006F288 File Offset: 0x0006D488
			// (set) Token: 0x06002657 RID: 9815 RVA: 0x0006F2D0 File Offset: 0x0006D4D0
			protected override DbTransaction DbTransaction
			{
				get
				{
					DbTransaction transaction;
					using (this.factory.impersonate())
					{
						transaction = this.command.Transaction;
					}
					return transaction;
				}
				set
				{
					using (this.factory.impersonate())
					{
						this.command.Transaction = value;
					}
				}
			}

			// Token: 0x17000F4E RID: 3918
			// (get) Token: 0x06002658 RID: 9816 RVA: 0x000091AE File Offset: 0x000073AE
			// (set) Token: 0x06002659 RID: 9817 RVA: 0x000091AE File Offset: 0x000073AE
			public override bool DesignTimeVisible
			{
				get
				{
					throw new NotImplementedException();
				}
				set
				{
					throw new NotImplementedException();
				}
			}

			// Token: 0x0600265A RID: 9818 RVA: 0x0006F318 File Offset: 0x0006D518
			protected override DbDataReader ExecuteDbDataReader(CommandBehavior behavior)
			{
				DbDataReader dbDataReader;
				using (this.factory.impersonate())
				{
					dbDataReader = new ImpersonatingDbProviderFactory.DataReaderWrapper(this.factory, this, this.command.ExecuteReader(behavior).WithTableSchema());
				}
				return dbDataReader;
			}

			// Token: 0x0600265B RID: 9819 RVA: 0x0006F374 File Offset: 0x0006D574
			public override int ExecuteNonQuery()
			{
				int num;
				using (this.factory.impersonate())
				{
					num = this.command.ExecuteNonQuery();
				}
				return num;
			}

			// Token: 0x0600265C RID: 9820 RVA: 0x0006F3BC File Offset: 0x0006D5BC
			public override object ExecuteScalar()
			{
				object obj;
				using (this.factory.impersonate())
				{
					obj = this.command.ExecuteScalar();
				}
				return obj;
			}

			// Token: 0x0600265D RID: 9821 RVA: 0x0006F404 File Offset: 0x0006D604
			public override void Prepare()
			{
				using (this.factory.impersonate())
				{
					this.command.Prepare();
				}
			}

			// Token: 0x17000F4F RID: 3919
			// (get) Token: 0x0600265E RID: 9822 RVA: 0x0006F44C File Offset: 0x0006D64C
			// (set) Token: 0x0600265F RID: 9823 RVA: 0x0006F494 File Offset: 0x0006D694
			public override UpdateRowSource UpdatedRowSource
			{
				get
				{
					UpdateRowSource updatedRowSource;
					using (this.factory.impersonate())
					{
						updatedRowSource = this.command.UpdatedRowSource;
					}
					return updatedRowSource;
				}
				set
				{
					using (this.factory.impersonate())
					{
						this.command.UpdatedRowSource = value;
					}
				}
			}

			// Token: 0x06002660 RID: 9824 RVA: 0x0006F4DC File Offset: 0x0006D6DC
			public override void Cancel()
			{
				Microsoft.Mashup.Common.SafeExceptions.IgnoreSafeExceptions(this.factory.host, "ImpersonatingDbProviderFactoryWrapper/CommandWrapper/Cancel", delegate
				{
					using (this.factory.impersonate())
					{
						this.command.Cancel();
					}
				});
			}

			// Token: 0x06002661 RID: 9825 RVA: 0x0006F4FF File Offset: 0x0006D6FF
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					Microsoft.Mashup.Common.SafeExceptions.IgnoreSafeExceptions(this.factory.host, "ImpersonatingDbProviderFactoryWrapper/CommandWrapper/Dispose", delegate
					{
						using (this.factory.impersonate())
						{
							this.command.Dispose();
						}
					});
				}
			}

			// Token: 0x04001005 RID: 4101
			private readonly ImpersonatingDbProviderFactory factory;

			// Token: 0x04001006 RID: 4102
			private readonly DbCommand command;
		}

		// Token: 0x02000484 RID: 1156
		private class DataReaderWrapper : DbDataReaderWithTableSchema
		{
			// Token: 0x06002664 RID: 9828 RVA: 0x0006F5B8 File Offset: 0x0006D7B8
			public DataReaderWrapper(ImpersonatingDbProviderFactory factory, DbCommand command, DbDataReaderWithTableSchema reader)
			{
				this.command = command;
				this.factory = factory;
				this.reader = reader;
			}

			// Token: 0x06002665 RID: 9829 RVA: 0x0006F5D8 File Offset: 0x0006D7D8
			public override bool Read()
			{
				bool flag;
				using (this.factory.impersonate())
				{
					flag = this.reader.Read();
				}
				return flag;
			}

			// Token: 0x17000F50 RID: 3920
			// (get) Token: 0x06002666 RID: 9830 RVA: 0x0006F620 File Offset: 0x0006D820
			public override int Depth
			{
				get
				{
					int depth;
					using (this.factory.impersonate())
					{
						depth = this.reader.Depth;
					}
					return depth;
				}
			}

			// Token: 0x17000F51 RID: 3921
			// (get) Token: 0x06002667 RID: 9831 RVA: 0x0006F668 File Offset: 0x0006D868
			public override int FieldCount
			{
				get
				{
					int fieldCount;
					using (this.factory.impersonate())
					{
						fieldCount = this.reader.FieldCount;
					}
					return fieldCount;
				}
			}

			// Token: 0x17000F52 RID: 3922
			// (get) Token: 0x06002668 RID: 9832 RVA: 0x0006F6B0 File Offset: 0x0006D8B0
			public override TableSchema Schema
			{
				get
				{
					TableSchema schema;
					using (this.factory.impersonate())
					{
						schema = this.reader.Schema;
					}
					return schema;
				}
			}

			// Token: 0x06002669 RID: 9833 RVA: 0x0006F6F8 File Offset: 0x0006D8F8
			public override bool GetBoolean(int ordinal)
			{
				bool boolean;
				using (this.factory.impersonate())
				{
					boolean = this.reader.GetBoolean(ordinal);
				}
				return boolean;
			}

			// Token: 0x0600266A RID: 9834 RVA: 0x0006F740 File Offset: 0x0006D940
			public override byte GetByte(int ordinal)
			{
				byte @byte;
				using (this.factory.impersonate())
				{
					@byte = this.reader.GetByte(ordinal);
				}
				return @byte;
			}

			// Token: 0x0600266B RID: 9835 RVA: 0x0006F788 File Offset: 0x0006D988
			public override long GetBytes(int ordinal, long dataOffset, byte[] buffer, int bufferOffset, int length)
			{
				long bytes;
				using (this.factory.impersonate())
				{
					bytes = this.reader.GetBytes(ordinal, dataOffset, buffer, bufferOffset, length);
				}
				return bytes;
			}

			// Token: 0x0600266C RID: 9836 RVA: 0x0006F7D8 File Offset: 0x0006D9D8
			public override char GetChar(int ordinal)
			{
				char @char;
				using (this.factory.impersonate())
				{
					@char = this.reader.GetChar(ordinal);
				}
				return @char;
			}

			// Token: 0x0600266D RID: 9837 RVA: 0x0006F820 File Offset: 0x0006DA20
			public override long GetChars(int ordinal, long dataOffset, char[] buffer, int bufferOffset, int length)
			{
				long chars;
				using (this.factory.impersonate())
				{
					chars = this.reader.GetChars(ordinal, dataOffset, buffer, bufferOffset, length);
				}
				return chars;
			}

			// Token: 0x0600266E RID: 9838 RVA: 0x0006F870 File Offset: 0x0006DA70
			public override string GetDataTypeName(int ordinal)
			{
				string dataTypeName;
				using (this.factory.impersonate())
				{
					dataTypeName = this.reader.GetDataTypeName(ordinal);
				}
				return dataTypeName;
			}

			// Token: 0x0600266F RID: 9839 RVA: 0x0006F8B8 File Offset: 0x0006DAB8
			public override DateTime GetDateTime(int ordinal)
			{
				DateTime dateTime;
				using (this.factory.impersonate())
				{
					dateTime = this.reader.GetDateTime(ordinal);
				}
				return dateTime;
			}

			// Token: 0x06002670 RID: 9840 RVA: 0x0006F900 File Offset: 0x0006DB00
			public override decimal GetDecimal(int ordinal)
			{
				decimal @decimal;
				using (this.factory.impersonate())
				{
					@decimal = this.reader.GetDecimal(ordinal);
				}
				return @decimal;
			}

			// Token: 0x06002671 RID: 9841 RVA: 0x0006F948 File Offset: 0x0006DB48
			public override double GetDouble(int ordinal)
			{
				double @double;
				using (this.factory.impersonate())
				{
					@double = this.reader.GetDouble(ordinal);
				}
				return @double;
			}

			// Token: 0x06002672 RID: 9842 RVA: 0x0006F990 File Offset: 0x0006DB90
			public override IEnumerator GetEnumerator()
			{
				IEnumerator enumerator;
				using (this.factory.impersonate())
				{
					enumerator = this.reader.GetEnumerator();
				}
				return enumerator;
			}

			// Token: 0x06002673 RID: 9843 RVA: 0x0006F9D8 File Offset: 0x0006DBD8
			public override Type GetFieldType(int ordinal)
			{
				Type fieldType;
				using (this.factory.impersonate())
				{
					fieldType = this.reader.GetFieldType(ordinal);
				}
				return fieldType;
			}

			// Token: 0x06002674 RID: 9844 RVA: 0x0006FA20 File Offset: 0x0006DC20
			public override float GetFloat(int ordinal)
			{
				float @float;
				using (this.factory.impersonate())
				{
					@float = this.reader.GetFloat(ordinal);
				}
				return @float;
			}

			// Token: 0x06002675 RID: 9845 RVA: 0x0006FA68 File Offset: 0x0006DC68
			public override Guid GetGuid(int ordinal)
			{
				Guid guid;
				using (this.factory.impersonate())
				{
					guid = this.reader.GetGuid(ordinal);
				}
				return guid;
			}

			// Token: 0x06002676 RID: 9846 RVA: 0x0006FAB0 File Offset: 0x0006DCB0
			public override short GetInt16(int ordinal)
			{
				short @int;
				using (this.factory.impersonate())
				{
					@int = this.reader.GetInt16(ordinal);
				}
				return @int;
			}

			// Token: 0x06002677 RID: 9847 RVA: 0x0006FAF8 File Offset: 0x0006DCF8
			public override int GetInt32(int ordinal)
			{
				int @int;
				using (this.factory.impersonate())
				{
					@int = this.reader.GetInt32(ordinal);
				}
				return @int;
			}

			// Token: 0x06002678 RID: 9848 RVA: 0x0006FB40 File Offset: 0x0006DD40
			public override long GetInt64(int ordinal)
			{
				long @int;
				using (this.factory.impersonate())
				{
					@int = this.reader.GetInt64(ordinal);
				}
				return @int;
			}

			// Token: 0x06002679 RID: 9849 RVA: 0x0006FB88 File Offset: 0x0006DD88
			public override string GetName(int ordinal)
			{
				string name;
				using (this.factory.impersonate())
				{
					name = this.reader.GetName(ordinal);
				}
				return name;
			}

			// Token: 0x0600267A RID: 9850 RVA: 0x0006FBD0 File Offset: 0x0006DDD0
			public override int GetOrdinal(string name)
			{
				int ordinal;
				using (this.factory.impersonate())
				{
					ordinal = this.reader.GetOrdinal(name);
				}
				return ordinal;
			}

			// Token: 0x0600267B RID: 9851 RVA: 0x0006FC18 File Offset: 0x0006DE18
			public override string GetString(int ordinal)
			{
				string @string;
				using (this.factory.impersonate())
				{
					@string = this.reader.GetString(ordinal);
				}
				return @string;
			}

			// Token: 0x0600267C RID: 9852 RVA: 0x0006FC60 File Offset: 0x0006DE60
			public override Type GetProviderSpecificFieldType(int ordinal)
			{
				Type providerSpecificFieldType;
				using (this.factory.impersonate())
				{
					providerSpecificFieldType = this.reader.GetProviderSpecificFieldType(ordinal);
				}
				return providerSpecificFieldType;
			}

			// Token: 0x0600267D RID: 9853 RVA: 0x0006FCA8 File Offset: 0x0006DEA8
			public override object GetProviderSpecificValue(int ordinal)
			{
				object providerSpecificValue;
				using (this.factory.impersonate())
				{
					providerSpecificValue = this.reader.GetProviderSpecificValue(ordinal);
				}
				return providerSpecificValue;
			}

			// Token: 0x0600267E RID: 9854 RVA: 0x0006FCF0 File Offset: 0x0006DEF0
			public override object GetValue(int ordinal)
			{
				object value;
				using (this.factory.impersonate())
				{
					value = this.reader.GetValue(ordinal);
				}
				return value;
			}

			// Token: 0x0600267F RID: 9855 RVA: 0x0006FD38 File Offset: 0x0006DF38
			public override int GetValues(object[] values)
			{
				int values2;
				using (this.factory.impersonate())
				{
					values2 = this.reader.GetValues(values);
				}
				return values2;
			}

			// Token: 0x17000F53 RID: 3923
			// (get) Token: 0x06002680 RID: 9856 RVA: 0x0006FD80 File Offset: 0x0006DF80
			public override bool HasRows
			{
				get
				{
					bool hasRows;
					using (this.factory.impersonate())
					{
						hasRows = this.reader.HasRows;
					}
					return hasRows;
				}
			}

			// Token: 0x17000F54 RID: 3924
			// (get) Token: 0x06002681 RID: 9857 RVA: 0x0006FDC8 File Offset: 0x0006DFC8
			public override bool IsClosed
			{
				get
				{
					bool isClosed;
					using (this.factory.impersonate())
					{
						isClosed = this.reader.IsClosed;
					}
					return isClosed;
				}
			}

			// Token: 0x06002682 RID: 9858 RVA: 0x0006FE10 File Offset: 0x0006E010
			public override bool IsDBNull(int ordinal)
			{
				bool flag;
				using (this.factory.impersonate())
				{
					flag = this.reader.IsDBNull(ordinal);
				}
				return flag;
			}

			// Token: 0x06002683 RID: 9859 RVA: 0x0006FE58 File Offset: 0x0006E058
			public override bool NextResult()
			{
				bool flag;
				using (this.factory.impersonate())
				{
					flag = this.reader.NextResult();
				}
				return flag;
			}

			// Token: 0x17000F55 RID: 3925
			// (get) Token: 0x06002684 RID: 9860 RVA: 0x0006FEA0 File Offset: 0x0006E0A0
			public override int RecordsAffected
			{
				get
				{
					int recordsAffected;
					using (this.factory.impersonate())
					{
						recordsAffected = this.reader.RecordsAffected;
					}
					return recordsAffected;
				}
			}

			// Token: 0x17000F56 RID: 3926
			public override object this[string name]
			{
				get
				{
					object obj;
					using (this.factory.impersonate())
					{
						obj = this.reader[name];
					}
					return obj;
				}
			}

			// Token: 0x17000F57 RID: 3927
			public override object this[int ordinal]
			{
				get
				{
					object obj;
					using (this.factory.impersonate())
					{
						obj = this.reader[ordinal];
					}
					return obj;
				}
			}

			// Token: 0x06002687 RID: 9863 RVA: 0x0006FF78 File Offset: 0x0006E178
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					Microsoft.Mashup.Common.SafeExceptions.IgnoreSafeExceptions(this.factory.host, "ImpersonatingDbProviderFactoryWrapper/DataReaderWrapper/Dispose", delegate
					{
						this.Close();
						this.reader.Dispose();
					});
					this.command = null;
				}
			}

			// Token: 0x06002688 RID: 9864 RVA: 0x0006FFA5 File Offset: 0x0006E1A5
			public override void Close()
			{
				Microsoft.Mashup.Common.SafeExceptions.IgnoreSafeExceptions(this.factory.host, "ImpersonatingDbProviderFactoryWrapper/DataReaderWrapper/Close", delegate
				{
					this.reader.Close();
				});
				this.command = null;
			}

			// Token: 0x04001007 RID: 4103
			private DbCommand command;

			// Token: 0x04001008 RID: 4104
			private readonly ImpersonatingDbProviderFactory factory;

			// Token: 0x04001009 RID: 4105
			private readonly DbDataReaderWithTableSchema reader;
		}
	}
}
