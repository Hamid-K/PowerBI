using System;
using System.Data;
using Microsoft.AnalysisServices.AdomdClient;
using Microsoft.Data.Serialization;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Engine1.Library.AnalysisServices
{
	// Token: 0x02000F02 RID: 3842
	internal class AnalysisServicesConnectionFactory : IAnalysisServicesConnectionFactory
	{
		// Token: 0x060065CF RID: 26063 RVA: 0x0015EAC4 File Offset: 0x0015CCC4
		public IAnalysisServicesConnection CreateConnection(string connectionString)
		{
			return new AnalysisServicesConnectionFactory.AnalysisServicesConnection(new AdomdConnection(connectionString));
		}

		// Token: 0x02000F03 RID: 3843
		private class AnalysisServicesConnection : IAnalysisServicesConnection, IDisposable
		{
			// Token: 0x060065D1 RID: 26065 RVA: 0x0015EAD1 File Offset: 0x0015CCD1
			public AnalysisServicesConnection(AdomdConnection connection)
			{
				this.connection = connection;
				this.state = ConnectionState.Closed;
				this.isDisposed = false;
			}

			// Token: 0x17001D97 RID: 7575
			// (get) Token: 0x060065D2 RID: 26066 RVA: 0x0015EAEE File Offset: 0x0015CCEE
			public string ProviderVersion
			{
				get
				{
					return this.connection.ProviderVersion;
				}
			}

			// Token: 0x17001D98 RID: 7576
			// (get) Token: 0x060065D3 RID: 26067 RVA: 0x0015EAFB File Offset: 0x0015CCFB
			public string ServerVersion
			{
				get
				{
					return this.connection.ServerVersion;
				}
			}

			// Token: 0x17001D99 RID: 7577
			// (get) Token: 0x060065D4 RID: 26068 RVA: 0x0015EB08 File Offset: 0x0015CD08
			public ConnectionState State
			{
				get
				{
					return this.state;
				}
			}

			// Token: 0x060065D5 RID: 26069 RVA: 0x0015EB10 File Offset: 0x0015CD10
			public void Open()
			{
				try
				{
					this.connection.Open();
					this.state = ConnectionState.Open;
				}
				catch (Exception ex)
				{
					if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
					{
						throw;
					}
					throw new AnalysisServicesException(ex.Message, ex);
				}
			}

			// Token: 0x060065D6 RID: 26070 RVA: 0x0015EB5C File Offset: 0x0015CD5C
			public DataSet GetSchemaDataSet(string schemaName, AdomdRestrictionCollection restrictions)
			{
				DataSet schemaDataSet;
				try
				{
					schemaDataSet = this.connection.GetSchemaDataSet(schemaName, restrictions);
				}
				catch (AdomdException ex)
				{
					throw new AnalysisServicesException(ex.Message, ex);
				}
				return schemaDataSet;
			}

			// Token: 0x060065D7 RID: 26071 RVA: 0x0015EB98 File Offset: 0x0015CD98
			public IAnalysisServicesCommand CreateCommand()
			{
				return new AnalysisServicesConnectionFactory.AnalysisServicesCommand(this.connection.CreateCommand());
			}

			// Token: 0x060065D8 RID: 26072 RVA: 0x0015EBAA File Offset: 0x0015CDAA
			public void Dispose()
			{
				if (!this.isDisposed)
				{
					this.connection.Dispose();
					this.state = ConnectionState.Closed;
				}
			}

			// Token: 0x040037F3 RID: 14323
			private readonly AdomdConnection connection;

			// Token: 0x040037F4 RID: 14324
			private ConnectionState state;

			// Token: 0x040037F5 RID: 14325
			private bool isDisposed;
		}

		// Token: 0x02000F04 RID: 3844
		private class AnalysisServicesCommand : IAnalysisServicesCommand, IDisposable
		{
			// Token: 0x060065D9 RID: 26073 RVA: 0x0015EBC6 File Offset: 0x0015CDC6
			public AnalysisServicesCommand(AdomdCommand command)
			{
				this.command = command;
			}

			// Token: 0x17001D9A RID: 7578
			// (set) Token: 0x060065DA RID: 26074 RVA: 0x0015EBD5 File Offset: 0x0015CDD5
			public string CommandText
			{
				set
				{
					this.command.CommandText = value;
				}
			}

			// Token: 0x17001D9B RID: 7579
			// (set) Token: 0x060065DB RID: 26075 RVA: 0x0015EBE3 File Offset: 0x0015CDE3
			public int CommandTimeout
			{
				set
				{
					this.command.CommandTimeout = value;
				}
			}

			// Token: 0x060065DC RID: 26076 RVA: 0x0015EBF4 File Offset: 0x0015CDF4
			public IDataReader ExecuteReader()
			{
				IDataReader dataReader;
				try
				{
					dataReader = new AnalysisServicesConnectionFactory.AnalysisServicesDataReader(this, this.command.ExecuteReader());
				}
				catch (AdomdException ex)
				{
					throw new AnalysisServicesException(ex.Message, ex);
				}
				return dataReader;
			}

			// Token: 0x060065DD RID: 26077 RVA: 0x0015EC34 File Offset: 0x0015CE34
			public void AddParameter(string name, object value)
			{
				this.command.Parameters.Add(name, value);
			}

			// Token: 0x060065DE RID: 26078 RVA: 0x0015EC49 File Offset: 0x0015CE49
			public void Cancel()
			{
				this.command.Cancel();
			}

			// Token: 0x060065DF RID: 26079 RVA: 0x0015EC56 File Offset: 0x0015CE56
			public void Dispose()
			{
				this.command.Dispose();
			}

			// Token: 0x040037F6 RID: 14326
			private readonly AdomdCommand command;
		}

		// Token: 0x02000F05 RID: 3845
		private class AnalysisServicesDataReader : DelegatingDataReaderWithTableSchema
		{
			// Token: 0x060065E0 RID: 26080 RVA: 0x0015EC63 File Offset: 0x0015CE63
			public AnalysisServicesDataReader(IAnalysisServicesCommand command, IDataReader reader)
				: base(reader.WithTableSchema())
			{
				this.command = command;
			}

			// Token: 0x060065E1 RID: 26081 RVA: 0x0015EC78 File Offset: 0x0015CE78
			public override bool Read()
			{
				bool flag;
				try
				{
					flag = base.Read();
				}
				catch (AdomdException ex)
				{
					throw new AnalysisServicesException(ex.Message, ex);
				}
				return flag;
			}

			// Token: 0x060065E2 RID: 26082 RVA: 0x0015ECB0 File Offset: 0x0015CEB0
			public override object GetValue(int i)
			{
				return base.GetValue(i) ?? DBNull.Value;
			}

			// Token: 0x060065E3 RID: 26083 RVA: 0x0015ECC2 File Offset: 0x0015CEC2
			public override void Dispose()
			{
				this.CancelCommand();
				base.Dispose();
			}

			// Token: 0x060065E4 RID: 26084 RVA: 0x0015ECD0 File Offset: 0x0015CED0
			public override void Close()
			{
				this.CancelCommand();
				base.Close();
			}

			// Token: 0x060065E5 RID: 26085 RVA: 0x0015ECE0 File Offset: 0x0015CEE0
			private void CancelCommand()
			{
				if (this.command != null)
				{
					try
					{
						this.command.Cancel();
					}
					catch (Exception ex)
					{
						if (!Microsoft.Mashup.Common.SafeExceptions.IsSafeException(ex))
						{
							throw;
						}
					}
					this.command = null;
				}
			}

			// Token: 0x040037F7 RID: 14327
			private IAnalysisServicesCommand command;
		}
	}
}
