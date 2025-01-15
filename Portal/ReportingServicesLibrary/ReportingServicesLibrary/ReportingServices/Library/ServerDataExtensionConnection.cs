using System;
using Microsoft.ReportingServices.DataExtensions;
using Microsoft.ReportingServices.Diagnostics;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.Library
{
	// Token: 0x020002DD RID: 733
	internal class ServerDataExtensionConnection : DataExtensionConnectionBase
	{
		// Token: 0x06001A27 RID: 6695 RVA: 0x00069334 File Offset: 0x00067534
		public ServerDataExtensionConnection(ReportProcessing.CreateDataExtensionInstance deInstance, UserContext threadUser, ReportProcessing.ExecutionType execType, IAdditionalToken additionalToken)
			: base(deInstance, threadUser, execType, DataProtection.Instance, additionalToken)
		{
		}

		// Token: 0x06001A28 RID: 6696 RVA: 0x00069346 File Offset: 0x00067546
		public ServerDataExtensionConnection(ReportProcessing.CreateDataExtensionInstance deInstance, UserContext threadUser, ReportProcessing.ExecutionType execType, IAdditionalToken additionalToken, IDbConnectionPool connectionPool)
			: base(deInstance, threadUser, execType, DataProtection.Instance, additionalToken, connectionPool)
		{
		}

		// Token: 0x06001A29 RID: 6697 RVA: 0x0006935A File Offset: 0x0006755A
		public ServerDataExtensionConnection(ReportProcessing.CreateDataExtensionInstance deInstance, UserContext threadUser, ReportProcessing.ExecutionType execType, IAdditionalToken additionalToken, IDbConnectionPool connectionPool, IOpenConnectionExtension openConnectionExtension)
			: base(deInstance, threadUser, execType, DataProtection.Instance, additionalToken, connectionPool, openConnectionExtension)
		{
		}

		// Token: 0x06001A2A RID: 6698 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected override void OnPooledConnectionOpen()
		{
		}

		// Token: 0x06001A2B RID: 6699 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected override void OnPooledConnectionClose()
		{
		}

		// Token: 0x06001A2C RID: 6700 RVA: 0x00069370 File Offset: 0x00067570
		protected override void OnConnectionOpen()
		{
			ServerDataExtensionConnection.OnConnectionOpenAction();
		}

		// Token: 0x06001A2D RID: 6701 RVA: 0x00005BF2 File Offset: 0x00003DF2
		protected override void OnConnectionOpenFailure()
		{
		}

		// Token: 0x06001A2E RID: 6702 RVA: 0x00069377 File Offset: 0x00067577
		protected override void OnConnectionClose()
		{
			ServerDataExtensionConnection.OnConnectionCloseAction();
		}

		// Token: 0x06001A2F RID: 6703 RVA: 0x0006937E File Offset: 0x0006757E
		protected override bool GoodForExecutionUnderServiceAccount(DataSourceInfo dataSourceInfo)
		{
			return DataSourceUtility.GoodForExecutionUnderServiceAccount(dataSourceInfo);
		}

		// Token: 0x06001A30 RID: 6704 RVA: 0x00069386 File Offset: 0x00067586
		protected override bool GoodForUnattendedSurrogateExecution(DataSourceInfo dataSourceInfo)
		{
			return DataSourceUtility.GoodForUnattendedSurrogateExecution(dataSourceInfo);
		}

		// Token: 0x06001A31 RID: 6705 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal static void OnConnectionOpenAction()
		{
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x00005BF2 File Offset: 0x00003DF2
		internal static void OnConnectionCloseAction()
		{
		}
	}
}
