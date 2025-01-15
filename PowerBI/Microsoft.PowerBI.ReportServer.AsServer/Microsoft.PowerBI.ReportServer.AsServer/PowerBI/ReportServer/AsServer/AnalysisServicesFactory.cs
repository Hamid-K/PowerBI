using System;
using Microsoft.AnalysisServices;
using Microsoft.AnalysisServices.Tabular;

namespace Microsoft.PowerBI.ReportServer.AsServer
{
	// Token: 0x02000008 RID: 8
	internal class AnalysisServicesFactory : IAnalysisServicesFactory
	{
		// Token: 0x06000042 RID: 66 RVA: 0x000039B0 File Offset: 0x00001BB0
		public TOMWrapper CreateASWrapper(AnalysisServicesSettings settings)
		{
			return this.CreateASWrapper(settings.ServerAddress, settings.Port, TimeSpan.Zero);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x000039C9 File Offset: 0x00001BC9
		public IDataSourceReader CreateASDatabaseWrapper(AnalysisServicesSettings settings, string databaseName)
		{
			return this.CreateASDatabaseWrapper(settings.ServerAddress, settings.Port, TimeSpan.Zero, databaseName);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x000039E3 File Offset: 0x00001BE3
		public TOMWrapper CreateASWrapperUsingTimeout(AnalysisServicesSettings settings)
		{
			return this.CreateASWrapper(settings.ServerAddress, settings.Port, settings.ScheduleRefreshTimeout);
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003A00 File Offset: 0x00001C00
		private TOMWrapper CreateASWrapper(string serverAddress, int port, TimeSpan timeout)
		{
			TOMWrapper tomwrapper = new TOMWrapper(new Server());
			try
			{
				tomwrapper.Connect(string.Format("Data Source={0}:{1};Timeout={2}", serverAddress, port, timeout.TotalSeconds));
			}
			catch (AmoException ex)
			{
				throw new AsConnectionException("Failed to connect to AS", AsConnectionExceptionErrorCode.LostConnection, ex);
			}
			catch (Exception ex2)
			{
				throw new AsConnectionException("Failed to connect to AS", AsConnectionExceptionErrorCode.Unknown, ex2);
			}
			return tomwrapper;
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00003A78 File Offset: 0x00001C78
		private TOMDataSourceReader CreateASDatabaseWrapper(string serverAddress, int port, TimeSpan timeout, string databaseName)
		{
			TOMDataSourceReader tomdataSourceReader = new TOMDataSourceReader(new Server());
			try
			{
				tomdataSourceReader.Connect(string.Format("Data Source={0}:{1};Timeout={2};Initial Catalog={3}", new object[] { serverAddress, port, timeout.TotalSeconds, databaseName }));
			}
			catch (AmoException ex)
			{
				throw new AsConnectionException("Failed to connect to AS", AsConnectionExceptionErrorCode.LostConnection, ex);
			}
			catch (Exception ex2)
			{
				throw new AsConnectionException("Failed to connect to AS", AsConnectionExceptionErrorCode.Unknown, ex2);
			}
			return tomdataSourceReader;
		}
	}
}
