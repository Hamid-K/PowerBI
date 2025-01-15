using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.PowerBI.DataExtension.Contracts;
using Microsoft.PowerBI.DataExtension.Contracts.Hosting;
using Microsoft.PowerBI.DataExtension.Contracts.Internal;
using MsolapWrapper;

namespace Microsoft.PowerBI.DataExtension.Msolap
{
	// Token: 0x02000009 RID: 9
	internal sealed class Connection : IDbConnection, IDisposable, IUnderlyingConnectionProvider
	{
		// Token: 0x0600001C RID: 28 RVA: 0x00002428 File Offset: 0x00000628
		internal Connection(string connectionString, ITracer tracer, IPrivateInformationService piiService)
			: this(connectionString, tracer, piiService, false)
		{
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002434 File Offset: 0x00000634
		internal Connection(string connectionString, ITracer tracer, IPrivateInformationService piiService, bool enableMsolapTracing)
		{
			MsolapTracerBase msolapTracerBase = (enableMsolapTracing ? new MsolapWrapperProxyTracer(tracer) : null);
			this._connection = new Connection(connectionString, msolapTracerBase);
			this._tracer = tracer;
			this._piiService = piiService;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002470 File Offset: 0x00000670
		public Task OpenAsync()
		{
			return Utilities.RunSynchronously(new Action(this.OpenImpl));
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600001F RID: 31 RVA: 0x00002483 File Offset: 0x00000683
		public bool IsOpen
		{
			get
			{
				return this._connection.IsOpen();
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000020 RID: 32 RVA: 0x00002490 File Offset: 0x00000690
		public object UnderlyingConnection
		{
			get
			{
				return this.GetUnderlyingConnection();
			}
		}

		// Token: 0x06000021 RID: 33 RVA: 0x00002498 File Offset: 0x00000698
		private void OpenImpl()
		{
			try
			{
				this._connection.Open();
			}
			catch (MsolapWrapperException ex)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException(ex, DataExtensionErrorUtils.MapConnectionErrorCode(ex.ErrorCode), this._piiService, "Failed to open the MSOLAP connection.", new object[0]);
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x000024E4 File Offset: 0x000006E4
		public async Task<bool> IsAliveAsync()
		{
			bool flag;
			if (!this.IsOpen)
			{
				flag = false;
			}
			else
			{
				try
				{
					using (Command command = (Command)this.CreateCommand(string.Empty))
					{
						await command.ExecuteNonQueryAsync();
					}
					Command command = null;
					return true;
				}
				catch (DataExtensionException ex)
				{
					this._tracer.TraceDataExtensionError(TraceLevel.Error, ex, "An error occurred checking if the connection is active");
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00002527 File Offset: 0x00000727
		public Task CloseAsync()
		{
			return Utilities.RunSynchronously(new Action(this.CloseImpl));
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000253A File Offset: 0x0000073A
		private void CloseImpl()
		{
			this._connection.Close();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x00002548 File Offset: 0x00000748
		public IDbCommand CreateCommand(string query)
		{
			IDbCommand dbCommand;
			try
			{
				dbCommand = new Command(this._connection.CreateCommand(query), this._piiService);
			}
			catch (MsolapWrapperException ex)
			{
				throw DataExtensionErrorUtils.CreateDataExtensionException(ex, this._piiService, "Failed to create the MSOLAP command.", new object[0]);
			}
			return dbCommand;
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002598 File Offset: 0x00000798
		public IDbSchemaCommand CreateSchemaCommand()
		{
			return new SchemaCommand(this, this._connection.CreateSchemaCommand(), this._piiService);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x000025B1 File Offset: 0x000007B1
		public Connection GetUnderlyingConnection()
		{
			return this._connection;
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000025B9 File Offset: 0x000007B9
		public void Dispose()
		{
			this._connection.Dispose();
		}

		// Token: 0x0400003C RID: 60
		private readonly Connection _connection;

		// Token: 0x0400003D RID: 61
		private readonly ITracer _tracer;

		// Token: 0x0400003E RID: 62
		private readonly IPrivateInformationService _piiService;
	}
}
