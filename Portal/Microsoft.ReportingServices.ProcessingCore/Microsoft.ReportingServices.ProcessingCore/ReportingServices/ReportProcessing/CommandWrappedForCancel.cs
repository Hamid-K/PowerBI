using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.DataProcessing;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x020005DD RID: 1501
	internal sealed class CommandWrappedForCancel : IDbCommand, IDisposable
	{
		// Token: 0x060053F4 RID: 21492 RVA: 0x001617FB File Offset: 0x0015F9FB
		internal CommandWrappedForCancel(IDbCommand command, IProcessingDataExtensionConnection dataExtensionConnection, IProcessingDataSource dataSourceObj, DataSourceInfo dataSourceInfo, string datasetName, IDbConnection connection)
		{
			this.m_command = command;
			this.m_dataExtensionConnection = dataExtensionConnection;
			this.m_dataSourceObj = dataSourceObj;
			this.m_dataSourceInfo = dataSourceInfo;
			this.m_datasetName = datasetName;
			this.m_connection = connection;
		}

		// Token: 0x060053F5 RID: 21493 RVA: 0x00161830 File Offset: 0x0015FA30
		public IDataReader ExecuteReader(CommandBehavior behavior)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060053F6 RID: 21494 RVA: 0x00161837 File Offset: 0x0015FA37
		public IDataParameter CreateParameter()
		{
			throw new NotImplementedException();
		}

		// Token: 0x060053F7 RID: 21495 RVA: 0x00161840 File Offset: 0x0015FA40
		public void Cancel()
		{
			if (this.m_command is IDbImpersonationNeededForCommandCancel)
			{
				this.m_dataExtensionConnection.HandleImpersonation(this.m_dataSourceObj, this.m_dataSourceInfo, this.m_datasetName, this.m_connection, delegate
				{
					this.m_command.Cancel();
				});
				return;
			}
			this.m_command.Cancel();
		}

		// Token: 0x17001EF9 RID: 7929
		// (get) Token: 0x060053F8 RID: 21496 RVA: 0x00161895 File Offset: 0x0015FA95
		// (set) Token: 0x060053F9 RID: 21497 RVA: 0x0016189C File Offset: 0x0015FA9C
		public string CommandText
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

		// Token: 0x17001EFA RID: 7930
		// (get) Token: 0x060053FA RID: 21498 RVA: 0x001618A3 File Offset: 0x0015FAA3
		// (set) Token: 0x060053FB RID: 21499 RVA: 0x001618AA File Offset: 0x0015FAAA
		public int CommandTimeout
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

		// Token: 0x17001EFB RID: 7931
		// (get) Token: 0x060053FC RID: 21500 RVA: 0x001618B1 File Offset: 0x0015FAB1
		// (set) Token: 0x060053FD RID: 21501 RVA: 0x001618B8 File Offset: 0x0015FAB8
		public CommandType CommandType
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

		// Token: 0x17001EFC RID: 7932
		// (get) Token: 0x060053FE RID: 21502 RVA: 0x001618BF File Offset: 0x0015FABF
		public IDataParameterCollection Parameters
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17001EFD RID: 7933
		// (get) Token: 0x060053FF RID: 21503 RVA: 0x001618C6 File Offset: 0x0015FAC6
		// (set) Token: 0x06005400 RID: 21504 RVA: 0x001618CD File Offset: 0x0015FACD
		public IDbTransaction Transaction
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

		// Token: 0x06005401 RID: 21505 RVA: 0x001618D4 File Offset: 0x0015FAD4
		public void Dispose()
		{
			throw new NotImplementedException();
		}

		// Token: 0x04002A38 RID: 10808
		private readonly IDbCommand m_command;

		// Token: 0x04002A39 RID: 10809
		private readonly IProcessingDataExtensionConnection m_dataExtensionConnection;

		// Token: 0x04002A3A RID: 10810
		private readonly IProcessingDataSource m_dataSourceObj;

		// Token: 0x04002A3B RID: 10811
		private readonly DataSourceInfo m_dataSourceInfo;

		// Token: 0x04002A3C RID: 10812
		private readonly string m_datasetName;

		// Token: 0x04002A3D RID: 10813
		private readonly IDbConnection m_connection;
	}
}
