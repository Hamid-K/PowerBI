using System;
using System.Collections.Generic;
using System.Data.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x0200108A RID: 4234
	internal class DbExceptionInfo
	{
		// Token: 0x06006ED8 RID: 28376 RVA: 0x0017E811 File Offset: 0x0017CA11
		public DbExceptionInfo(IEngineHost engineHost, DbException exception)
		{
			this.engineHost = engineHost;
			this.exception = exception;
		}

		// Token: 0x17001F43 RID: 8003
		// (get) Token: 0x06006ED9 RID: 28377 RVA: 0x0017E827 File Offset: 0x0017CA27
		public DbException Exception
		{
			get
			{
				return this.exception;
			}
		}

		// Token: 0x06006EDA RID: 28378 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool IsRetryable(Tracer tracer, IResource resource, int retryAttempt)
		{
			return false;
		}

		// Token: 0x17001F44 RID: 8004
		// (get) Token: 0x06006EDB RID: 28379 RVA: 0x00002105 File Offset: 0x00000305
		public virtual ResourceExceptionKind ResourceExceptionKind
		{
			get
			{
				return ResourceExceptionKind.None;
			}
		}

		// Token: 0x17001F45 RID: 8005
		// (get) Token: 0x06006EDC RID: 28380 RVA: 0x0017E82F File Offset: 0x0017CA2F
		protected IEngineHost Host
		{
			get
			{
				return this.engineHost;
			}
		}

		// Token: 0x06006EDD RID: 28381 RVA: 0x0017E838 File Offset: 0x0017CA38
		public static List<RecordKeyDefinition> GetDetails(DbException exception)
		{
			return new List<RecordKeyDefinition>
			{
				new RecordKeyDefinition("Message", TextValue.New(exception.Message), TypeValue.Text),
				new RecordKeyDefinition("ErrorCode", NumberValue.New(exception.ErrorCode), TypeValue.Number)
			};
		}

		// Token: 0x06006EDE RID: 28382 RVA: 0x0017E88C File Offset: 0x0017CA8C
		public virtual Exception GetEngineException(string dataSourceName, IResource resource)
		{
			switch (this.ResourceExceptionKind)
			{
			case ResourceExceptionKind.None:
				return this.AsValueException(dataSourceName, resource);
			case ResourceExceptionKind.InvalidCredentials:
				return DataSourceException.NewAccessAuthorizationError(this.Host, resource, this.exception.Message, null, this.exception);
			case ResourceExceptionKind.SecureConnectionFailed:
				return DataSourceException.NewEncryptedConnectionError(this.Host, resource, this.exception.Message, null, this.exception);
			case ResourceExceptionKind.ServerNameMismatch:
				return DataSourceException.NewEncryptionPrincipalNameMismatch(this.Host, resource, this.exception.Message, null, null);
			default:
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			}
		}

		// Token: 0x06006EDF RID: 28383 RVA: 0x0000336E File Offset: 0x0000156E
		public virtual void TraceDetails(IHostTrace trace)
		{
		}

		// Token: 0x06006EE0 RID: 28384 RVA: 0x0017E928 File Offset: 0x0017CB28
		protected virtual ValueException AsValueException(string dataSourceName, IResource resource)
		{
			Message2 message = DataSourceException.DataSourceMessage(dataSourceName, this.Exception.Message);
			return DataSourceException.NewDataSourceError<Message2>(this.Host, message, resource, "ErrorCode", NumberValue.New(this.exception.ErrorCode), TypeValue.Number, this.exception);
		}

		// Token: 0x04003D7A RID: 15738
		private const string MessageKey = "Message";

		// Token: 0x04003D7B RID: 15739
		private const string ErrorCodeKey = "ErrorCode";

		// Token: 0x04003D7C RID: 15740
		private readonly IEngineHost engineHost;

		// Token: 0x04003D7D RID: 15741
		private readonly DbException exception;
	}
}
