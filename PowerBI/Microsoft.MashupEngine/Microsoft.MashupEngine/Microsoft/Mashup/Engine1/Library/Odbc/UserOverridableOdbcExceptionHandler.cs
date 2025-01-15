using System;
using System.Diagnostics;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x0200067F RID: 1663
	internal sealed class UserOverridableOdbcExceptionHandler : OdbcExceptionHandler
	{
		// Token: 0x06003437 RID: 13367 RVA: 0x000A7E4F File Offset: 0x000A604F
		public UserOverridableOdbcExceptionHandler(IEngineHost engineHost, FunctionValue exceptionHandler)
			: base(engineHost)
		{
			this.exceptionHandler = exceptionHandler;
		}

		// Token: 0x06003438 RID: 13368 RVA: 0x000A7E60 File Offset: 0x000A6060
		public override bool TryHandle(IResource resource, OdbcException exception, out RuntimeException runtimeException)
		{
			ValueException odbcValueException = OdbcExceptionHandler.GetOdbcValueException(base.Host, exception, resource);
			return this.TryHandle(odbcValueException, out runtimeException);
		}

		// Token: 0x06003439 RID: 13369 RVA: 0x000A7E84 File Offset: 0x000A6084
		public override bool IsRetryable(Tracer tracer, OdbcException exception, IResource resource, int? retryAttemptCount)
		{
			ValueException odbcValueException = OdbcExceptionHandler.GetOdbcValueException(base.Host, exception, resource);
			bool flag;
			try
			{
				flag = UserOverridableOdbcExceptionHandler.IsRetry(this.EvaluateOnError(odbcValueException, retryAttemptCount));
			}
			catch (Exception ex) when (SafeExceptions.IsSafeException(ex))
			{
				using (IHostTrace hostTrace = tracer.CreateTrace("IsRetryable", TraceEventType.Warning))
				{
					hostTrace.Add(ex, TraceEventType.Warning, true);
				}
				flag = false;
			}
			return flag;
		}

		// Token: 0x0600343A RID: 13370 RVA: 0x000A7F0C File Offset: 0x000A610C
		public override bool TryHandle(ValueException exception, out RuntimeException runtimeException)
		{
			try
			{
				this.EvaluateOnError(exception, null);
				runtimeException = null;
				return false;
			}
			catch (RuntimeException ex)
			{
				runtimeException = ex;
			}
			return true;
		}

		// Token: 0x0600343B RID: 13371 RVA: 0x000A7F4C File Offset: 0x000A614C
		private Value EvaluateOnError(ValueException valueException, int? retryAttemptCount)
		{
			Value value;
			if (this.exceptionHandler.AsFunction.Type.AsFunctionType.ParameterCount == 1)
			{
				value = this.exceptionHandler.Invoke(valueException.Value);
			}
			else
			{
				Value value2 = ((retryAttemptCount == null) ? Value.Null : NumberValue.New(retryAttemptCount.Value));
				RecordValue recordValue = RecordValue.New(Keys.New("RetryCount"), new Value[] { value2 });
				value = this.exceptionHandler.Invoke(valueException.Value, recordValue);
			}
			return value;
		}

		// Token: 0x0600343C RID: 13372 RVA: 0x000A7FD7 File Offset: 0x000A61D7
		private static bool IsRetry(Value value)
		{
			return !value.IsNull && value.IsLogical && value.AsBoolean;
		}

		// Token: 0x04001777 RID: 6007
		private readonly FunctionValue exceptionHandler;
	}
}
