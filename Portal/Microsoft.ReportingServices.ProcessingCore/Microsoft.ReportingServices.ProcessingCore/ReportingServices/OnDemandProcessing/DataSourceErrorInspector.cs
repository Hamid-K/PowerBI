using System;
using Microsoft.ReportingServices.DataProcessing;
using Microsoft.ReportingServices.Diagnostics.Utilities;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x020007EF RID: 2031
	internal sealed class DataSourceErrorInspector
	{
		// Token: 0x06007191 RID: 29073 RVA: 0x001D81CD File Offset: 0x001D63CD
		internal DataSourceErrorInspector(IDbConnection connection)
		{
			this.m_connection = connection;
		}

		// Token: 0x06007192 RID: 29074 RVA: 0x001D81DC File Offset: 0x001D63DC
		internal bool TryInterpretProviderErrorCode(Exception e, out ErrorCode errorCode)
		{
			IDbErrorInspectorFactory dbErrorInspectorFactory = this.m_connection as IDbErrorInspectorFactory;
			if (dbErrorInspectorFactory != null)
			{
				IDbErrorInspector dbErrorInspector = dbErrorInspectorFactory.CreateErrorInspector();
				if (dbErrorInspector.IsQueryMemoryLimitExceeded(e))
				{
					errorCode = ErrorCode.rsQueryMemoryLimitExceeded;
					return true;
				}
				if (dbErrorInspector.IsQueryTimeout(e))
				{
					errorCode = ErrorCode.rsQueryTimeoutExceeded;
					return true;
				}
			}
			errorCode = ErrorCode.rsSuccess;
			return false;
		}

		// Token: 0x06007193 RID: 29075 RVA: 0x001D8228 File Offset: 0x001D6428
		internal bool IsOnPremiseServiceException(Exception e)
		{
			IDbErrorInspectorFactory dbErrorInspectorFactory = this.m_connection as IDbErrorInspectorFactory;
			return dbErrorInspectorFactory != null && dbErrorInspectorFactory.CreateErrorInspector().IsOnPremisesServiceException(e);
		}

		// Token: 0x04003A73 RID: 14963
		private readonly IDbConnection m_connection;
	}
}
