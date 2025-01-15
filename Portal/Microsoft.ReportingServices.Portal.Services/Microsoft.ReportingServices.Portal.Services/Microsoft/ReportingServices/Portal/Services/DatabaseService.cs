using System;
using System.Data;
using System.Diagnostics;
using Microsoft.BIServer.HostingEnvironment;
using Microsoft.BIServer.HostingEnvironment.HostingInfo;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.Library;
using Microsoft.ReportingServices.Portal.Interfaces.Exceptions;
using Microsoft.ReportingServices.Portal.Interfaces.Services;

namespace Microsoft.ReportingServices.Portal.Services
{
	// Token: 0x0200001E RID: 30
	internal sealed class DatabaseService : IDatabaseService
	{
		// Token: 0x06000186 RID: 390 RVA: 0x0000C4F8 File Offset: 0x0000A6F8
		public void Init()
		{
			ConnectionManager.Init();
		}

		// Token: 0x06000187 RID: 391 RVA: 0x0000C500 File Offset: 0x0000A700
		public void EnsureDatabaseAvailable(ILogger logger)
		{
			this.CheckStatusAndThrowIfMatching(logger, DatabaseValidationStatus.DowngradeDetected, SR.Error_DatabaseDowngradeDetected);
			this.CheckStatusAndThrowIfMatching(logger, DatabaseValidationStatus.PbiToRsAttach, SR.Error_PbiToRsAttachDetected);
			if (HostingState.Current.DatabaseValidationStatus == DatabaseValidationStatus.EncryptedContentNotAccessible)
			{
				logger.Trace(TraceLevel.Error, SR.Error_EncryptionFailure);
				throw new CannotValidateEncryptedDataException();
			}
			ConnectionManager connectionManager = null;
			try
			{
				connectionManager = new ConnectionManager(ConnectionTransactionType.Explicit, IsolationLevel.ReadUncommitted);
				connectionManager.WillDisconnectStorage();
				connectionManager.VerifyConnection(true);
			}
			catch (InvalidReportServerDatabaseException ex)
			{
				throw new DatabaseUnavailableException(ex.Message, ex);
			}
			catch (ReportServerDatabaseUnavailableException ex2)
			{
				throw new DatabaseUnavailableException(ex2.Message, ex2);
			}
			catch (ReportServerDisabledException ex3)
			{
				throw new KeyStateNotValidException(ex3);
			}
			finally
			{
				if (connectionManager != null)
				{
					connectionManager.DisconnectStorage();
				}
			}
		}

		// Token: 0x06000188 RID: 392 RVA: 0x0000C5C4 File Offset: 0x0000A7C4
		private void CheckStatusAndThrowIfMatching(ILogger logger, DatabaseValidationStatus statusToMatch, string errorString)
		{
			if (HostingState.Current.DatabaseValidationStatus == statusToMatch)
			{
				logger.Trace(TraceLevel.Error, errorString);
				throw new DatabaseUnavailableException(errorString);
			}
		}
	}
}
