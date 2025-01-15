using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Odbc;
using System.Globalization;
using System.Linq;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine.Interface.Tracing;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005F1 RID: 1521
	internal class OdbcExceptionInfo : DbExceptionInfo
	{
		// Token: 0x06003002 RID: 12290 RVA: 0x00091415 File Offset: 0x0008F615
		public OdbcExceptionInfo(IEngineHost engineHost, DbException exception, OdbcExceptionHandler exceptionHandler)
			: base(engineHost, exception)
		{
			this.odbcException = this.GetEngineOdbcException();
			this.exceptionHandler = exceptionHandler;
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06003003 RID: 12291 RVA: 0x00091434 File Offset: 0x0008F634
		public override ResourceExceptionKind ResourceExceptionKind
		{
			get
			{
				if (this.odbcException == null)
				{
					return base.ResourceExceptionKind;
				}
				using (IEnumerator<Microsoft.Mashup.Engine1.Library.Odbc.Interop.OdbcError> enumerator = this.odbcException.Errors.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						if (enumerator.Current.SQLState.StartsWith("28", StringComparison.Ordinal))
						{
							return ResourceExceptionKind.InvalidCredentials;
						}
					}
				}
				return ResourceExceptionKind.None;
			}
		}

		// Token: 0x06003004 RID: 12292 RVA: 0x000914A8 File Offset: 0x0008F6A8
		public override bool IsRetryable(Tracer tracer, IResource resource, int retryAttemptCount)
		{
			return this.exceptionHandler.IsRetryable(tracer, this.odbcException, resource, new int?(retryAttemptCount));
		}

		// Token: 0x06003005 RID: 12293 RVA: 0x000914C4 File Offset: 0x0008F6C4
		public override Exception GetEngineException(string dataSourceName, IResource resource)
		{
			RuntimeException ex;
			bool flag = this.exceptionHandler.TryHandle(resource, this.odbcException, out ex);
			if (flag && ex is ResourceSecurityException)
			{
				return ex;
			}
			Exception engineException = base.GetEngineException(dataSourceName, resource);
			if (!flag || engineException is ResourceSecurityException)
			{
				return engineException;
			}
			return ex;
		}

		// Token: 0x06003006 RID: 12294 RVA: 0x0009150A File Offset: 0x0008F70A
		protected override ValueException AsValueException(string dataSourceName, IResource resource)
		{
			if (this.odbcException == null)
			{
				return base.AsValueException(dataSourceName, resource);
			}
			return OdbcExceptionHandler.GetOdbcValueException(base.Host, this.odbcException, resource);
		}

		// Token: 0x06003007 RID: 12295 RVA: 0x00091530 File Offset: 0x0008F730
		private Microsoft.Mashup.Engine1.Library.Odbc.Interop.OdbcException GetEngineOdbcException()
		{
			Microsoft.Mashup.Engine1.Library.Odbc.Interop.OdbcException ex = base.Exception as Microsoft.Mashup.Engine1.Library.Odbc.Interop.OdbcException;
			if (ex != null)
			{
				return ex;
			}
			global::System.Data.Odbc.OdbcException ex2 = base.Exception as global::System.Data.Odbc.OdbcException;
			if (ex2 != null)
			{
				Microsoft.Mashup.Engine1.Library.Odbc.Interop.OdbcError[] array = new Microsoft.Mashup.Engine1.Library.Odbc.Interop.OdbcError[ex2.Errors.Count];
				for (int i = 0; i < array.Length; i++)
				{
					global::System.Data.Odbc.OdbcError odbcError = ex2.Errors[i];
					array[i] = new Microsoft.Mashup.Engine1.Library.Odbc.Interop.OdbcError(odbcError.Message, odbcError.SQLState, odbcError.NativeError);
				}
				return new Microsoft.Mashup.Engine1.Library.Odbc.Interop.OdbcException(Odbc32.RetCode.ERROR, array);
			}
			return null;
		}

		// Token: 0x06003008 RID: 12296 RVA: 0x000915B0 File Offset: 0x0008F7B0
		public override void TraceDetails(IHostTrace trace)
		{
			trace.AddArray("OdbcSqlStates", this.odbcException.Errors.Select((Microsoft.Mashup.Engine1.Library.Odbc.Interop.OdbcError e) => e.SQLState ?? "<missing>"), false);
			trace.AddArray("OdbcNativeErrors", this.odbcException.Errors.Select((Microsoft.Mashup.Engine1.Library.Odbc.Interop.OdbcError e) => e.NativeError.ToString(CultureInfo.InvariantCulture)), false);
		}

		// Token: 0x04001525 RID: 5413
		private readonly Microsoft.Mashup.Engine1.Library.Odbc.Interop.OdbcException odbcException;

		// Token: 0x04001526 RID: 5414
		private readonly OdbcExceptionHandler exceptionHandler;
	}
}
