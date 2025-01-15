using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Odbc;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x02000465 RID: 1125
	internal class SapHanaOdbcExceptionHandler : OdbcExceptionHandler
	{
		// Token: 0x0600259C RID: 9628 RVA: 0x0006C647 File Offset: 0x0006A847
		public SapHanaOdbcExceptionHandler(IEngineHost engineHost, bool isConnectionEncrypted)
			: base(engineHost)
		{
			this.isConnectionEncrypted = isConnectionEncrypted;
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x0006C658 File Offset: 0x0006A858
		public override bool TryHandle(IResource resource, OdbcException exception, out RuntimeException runtimeException)
		{
			foreach (OdbcError odbcError in exception.Errors)
			{
				if (SapHanaOdbcExceptionHandler.authorizationErrors.Contains(odbcError.NativeError))
				{
					runtimeException = DataSourceException.NewAccessAuthorizationError(base.Host, resource, odbcError.Message, odbcError.Message, exception);
					return true;
				}
				if (SapHanaOdbcExceptionHandler.accessDeniedErrors.Contains(odbcError.NativeError))
				{
					runtimeException = DataSourceException.NewAccessForbiddenError(base.Host, resource, odbcError.Message, odbcError.Message, exception);
					return true;
				}
				if (this.isConnectionEncrypted && SapHanaOdbcExceptionHandler.encryptionErrors.Contains(odbcError.NativeError))
				{
					runtimeException = DataSourceException.NewEncryptedConnectionError(base.Host, resource, odbcError.Message, odbcError.Message, exception);
					return true;
				}
			}
			return base.TryHandle(resource, exception, out runtimeException);
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x0006C750 File Offset: 0x0006A950
		public override bool IsRetryable(Tracer tracer, OdbcException exception, IResource resource, int? retryAttemptCount)
		{
			foreach (OdbcError odbcError in exception.Errors)
			{
				if (SapHanaOdbcExceptionHandler.retryableErrors.Contains(odbcError.NativeError) || (odbcError.NativeError >= 128 && odbcError.NativeError <= 153))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x04000F89 RID: 3977
		private const int transactionStart = 128;

		// Token: 0x04000F8A RID: 3978
		private const int transactionEnd = 153;

		// Token: 0x04000F8B RID: 3979
		private const int connectionError = -10709;

		// Token: 0x04000F8C RID: 3980
		private static readonly HashSet<int> authorizationErrors = new HashSet<int> { 10, 412, 414, 431, 438, 466 };

		// Token: 0x04000F8D RID: 3981
		private static readonly HashSet<int> accessDeniedErrors = new HashSet<int>
		{
			20, 258, 415, 416, 422, 436, 456, 460, 3841, 4273,
			4274, 4275
		};

		// Token: 0x04000F8E RID: 3982
		private static readonly HashSet<int> retryableErrors = new HashSet<int>
		{
			4, 12, 13, 14, 16, 18, 21, 377, 385, 429,
			515, 604, 607, 1025, 1027, 1028, 1029, 1030, 1031, 1032,
			1034, 1304, 3584
		};

		// Token: 0x04000F8F RID: 3983
		private static readonly HashSet<int> encryptionErrors = new HashSet<int> { 4289, 4290, 4291, -10709 };

		// Token: 0x04000F90 RID: 3984
		private readonly bool isConnectionEncrypted;
	}
}
