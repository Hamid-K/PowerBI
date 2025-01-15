using System;
using System.Globalization;
using Microsoft.HostIntegration.MqClient.StrictResources.Globals;
using Microsoft.HostIntegration.XaClient;

namespace Microsoft.HostIntegration.MqClient
{
	// Token: 0x02000BCE RID: 3022
	public class ExceptionHelpers
	{
		// Token: 0x06005DC9 RID: 24009 RVA: 0x0017F628 File Offset: 0x0017D828
		public static string FormatReturnCode(ReturnCode returnCode)
		{
			if (returnCode < ReturnCode.InternalReturnCode)
			{
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.MqReturnCode((int)returnCode).ToString(CultureInfo.InvariantCulture);
			}
			switch (returnCode)
			{
			case ReturnCode.TcpConnectFailed:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.TcpConnectFailed;
			case ReturnCode.TcpSslConnectFailed:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.TcpSslConnectFailed;
			case ReturnCode.TcpPoolingDifferentSsl:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.TcpPoolingDifferentSsl;
			case ReturnCode.QmConnectTcpConnectFailed:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QmConnectTcpConnectFailed;
			case ReturnCode.QmConnectTcpDisconnected:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QmConnectTcpDisconnected;
			case ReturnCode.QmConnectInitialDataRejectedCcsid:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QmConnectInitialDataRejectedCcsid;
			case ReturnCode.QmConnectInitialDataRejectedConversations0:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QmConnectInitialDataRejectedConversations0;
			case ReturnCode.QmConnectInitialDataRejectedEncoding:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QmConnectInitialDataRejectedEncoding;
			case ReturnCode.QmConnectInitialDataRejectedErrorFlag2:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QmConnectInitialDataRejectedErrorFlag2;
			case ReturnCode.QmConnectInitialDataRejectedFap:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QmConnectInitialDataRejectedFap;
			case ReturnCode.QmConnectInitialDataRejectedTransmissionSize:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QmConnectInitialDataRejectedTransmissionSize;
			case ReturnCode.QmConnectUnexpected:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QmConnectUnexpected;
			case ReturnCode.QmConnectMqConnFailed:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QmConnectMqConnFailed;
			case ReturnCode.QmConnectNoChannel:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QmConnectNoChannel;
			case ReturnCode.QmAsyncStatusFailedQuiescing:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QmAsyncStatusFailedQuiescing;
			case ReturnCode.QmAsyncStatusFailedHandle:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QmAsyncStatusFailedHandle;
			case ReturnCode.QSendMaximumMessageSize:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QSendMaximumMessageSize;
			case ReturnCode.QOpenFailedQmFailed:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QOpenFailedQmFailed;
			case ReturnCode.QOpenFailedQmQuiesced:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QOpenFailedQmQuiesced;
			case ReturnCode.QOpenFailedTcpFailed:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QOpenFailedTcpFailed;
			case ReturnCode.QOpenFailedFinalizing:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QOpenFailedFinalizing;
			case ReturnCode.QSendFailedUnknown:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QSendFailedUnknown;
			case ReturnCode.QSendFailedQmFailed:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QSendFailedQmFailed;
			case ReturnCode.QSendFailedQmQuiesced:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QSendFailedQmQuiesced;
			case ReturnCode.QReceiveFailedQmQuiesced:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QReceiveFailedQmQuiesced;
			case ReturnCode.QReceiveFailedQmFailed:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QReceiveFailedQmFailed;
			case ReturnCode.QReceiveFailedUnknown:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QReceiveFailedUnknown;
			case ReturnCode.XaEndClosing:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaEndClosing;
			case ReturnCode.XaEndDisconnecting:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaEndDisconnecting;
			case ReturnCode.XaFinalizeClosing:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaFinalizeClosing;
			case ReturnCode.XaFinalizeDisconnecting:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaFinalizeDisconnecting;
			case ReturnCode.XaPrepareClosing:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaPrepareClosing;
			case ReturnCode.XaPrepareDisconnecting:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaPrepareDisconnecting;
			case ReturnCode.XaStartClosing:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaStartClosing;
			case ReturnCode.XaStartDisconnecting:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaStartDisconnecting;
			case ReturnCode.XaRollback:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaRollback;
			case ReturnCode.XaRollbackCommunicationFailure:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaRollbackCommunicationFailure;
			case ReturnCode.XaRollbackDeadlock:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaRollbackDeadlock;
			case ReturnCode.XaRollbackIntegrity:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaRollbackIntegrity;
			case ReturnCode.XaRollbackOther:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaRollbackOther;
			case ReturnCode.XaRollbackProtocol:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaRollbackProtocol;
			case ReturnCode.XaRollbackTimeout:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaRollbackTimeout;
			case ReturnCode.XaRollbackTransient:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaRollbackTransient;
			case ReturnCode.XaNoMigrate:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaNoMigrate;
			case ReturnCode.XaHeuristicHazard:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaHeuristicHazard;
			case ReturnCode.XaHeuristicCommit:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaHeuristicCommit;
			case ReturnCode.XaHeuristicRollback:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaHeuristicRollback;
			case ReturnCode.XaHeuristicMix:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaHeuristicMix;
			case ReturnCode.XaRetry:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaRetry;
			case ReturnCode.XaReadOnly:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaReadOnly;
			case ReturnCode.XaOutstandingAsynchronousOperation:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaOutstandingAsynchronousOperation;
			case ReturnCode.XaResourceManagerError:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaResourceManagerError;
			case ReturnCode.XaInvalidXid:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaInvalidXid;
			case ReturnCode.XaInvalidArguments:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaInvalidArguments;
			case ReturnCode.XaProtocol:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaProtocol;
			case ReturnCode.XaResourceManagerUnavailable:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaResourceManagerUnavailable;
			case ReturnCode.XaDuplicateXid:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaDuplicateXid;
			case ReturnCode.XaWorkOutsideTransaction:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaWorkOutsideTransaction;
			case ReturnCode.XaEnlistmentFailed:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.XaEnlistmentFailed;
			case ReturnCode.StaleTransaction:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.StaleTransaction;
			case ReturnCode.QmConnectCipherSpec:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QmConnectCipherSpec;
			case ReturnCode.QmConnectSslExpected:
				return Microsoft.HostIntegration.MqClient.StrictResources.Globals.SR.QmConnectSslExpected;
			}
			throw new InvalidOperationException("Unknown Return Code");
		}

		// Token: 0x06005DCA RID: 24010 RVA: 0x0017F8E8 File Offset: 0x0017DAE8
		public static ReturnCode XaReturnCodeToReturnCode(XaReturnCode xaReturnCode)
		{
			switch (xaReturnCode)
			{
			case XaReturnCode.WorkOutsideTransaction:
				return ReturnCode.XaWorkOutsideTransaction;
			case XaReturnCode.DuplicateXid:
				return ReturnCode.XaDuplicateXid;
			case XaReturnCode.ResourceManagerUnavailable:
				return ReturnCode.XaResourceManagerUnavailable;
			case XaReturnCode.Protocol:
				return ReturnCode.XaProtocol;
			case XaReturnCode.InvalidArguments:
				return ReturnCode.XaInvalidArguments;
			case XaReturnCode.InvalidXid:
				return ReturnCode.XaInvalidXid;
			case XaReturnCode.ResourceManagerError:
				return ReturnCode.XaResourceManagerError;
			case XaReturnCode.OutstandingAsynchronousOperation:
				return ReturnCode.XaOutstandingAsynchronousOperation;
			case (XaReturnCode)(-1):
			case (XaReturnCode)1:
			case (XaReturnCode)2:
				break;
			case XaReturnCode.Ok:
				return ReturnCode.Ok;
			case XaReturnCode.ReadOnly:
				return ReturnCode.XaReadOnly;
			case XaReturnCode.Retry:
				return ReturnCode.XaRetry;
			case XaReturnCode.HeuristicMix:
				return ReturnCode.XaHeuristicMix;
			case XaReturnCode.HeuristicRollback:
				return ReturnCode.XaHeuristicRollback;
			case XaReturnCode.HeuristicCommit:
				return ReturnCode.XaHeuristicCommit;
			case XaReturnCode.HeuristicHazard:
				return ReturnCode.XaHeuristicHazard;
			case XaReturnCode.NoMigrate:
				return ReturnCode.XaNoMigrate;
			default:
				switch (xaReturnCode)
				{
				case XaReturnCode.Rollback:
					return ReturnCode.XaRollback;
				case XaReturnCode.RollbackCommunicationFailure:
					return ReturnCode.XaRollbackCommunicationFailure;
				case XaReturnCode.RollbackDeadlock:
					return ReturnCode.XaRollbackDeadlock;
				case XaReturnCode.RollbackIntegrity:
					return ReturnCode.XaRollbackIntegrity;
				case XaReturnCode.RollbackOther:
					return ReturnCode.XaRollbackOther;
				case XaReturnCode.RollbackProtocol:
					return ReturnCode.XaRollbackProtocol;
				case XaReturnCode.RollbackTimeout:
					return ReturnCode.XaRollbackTimeout;
				case XaReturnCode.RollbackTransient:
					return ReturnCode.XaRollbackTransient;
				}
				break;
			}
			throw new InvalidOperationException("Unknown XA Return Code");
		}
	}
}
