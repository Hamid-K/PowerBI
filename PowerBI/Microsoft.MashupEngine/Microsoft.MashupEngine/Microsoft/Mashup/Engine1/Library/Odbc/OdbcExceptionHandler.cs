using System;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Library.Common;
using Microsoft.Mashup.Engine1.Library.Odbc.Interop;
using Microsoft.Mashup.Engine1.Runtime;
using Microsoft.Mashup.Engine1.Runtime.Typeflow;

namespace Microsoft.Mashup.Engine1.Library.Odbc
{
	// Token: 0x020005F0 RID: 1520
	internal class OdbcExceptionHandler
	{
		// Token: 0x06002FFB RID: 12283 RVA: 0x000912BF File Offset: 0x0008F4BF
		public OdbcExceptionHandler(IEngineHost engineHost)
		{
			this.engineHost = engineHost;
		}

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06002FFC RID: 12284 RVA: 0x000912CE File Offset: 0x0008F4CE
		protected IEngineHost Host
		{
			get
			{
				return this.engineHost;
			}
		}

		// Token: 0x06002FFD RID: 12285 RVA: 0x000912D6 File Offset: 0x0008F4D6
		public virtual bool TryHandle(IResource resource, OdbcException exception, out RuntimeException runtimeException)
		{
			runtimeException = null;
			return false;
		}

		// Token: 0x06002FFE RID: 12286 RVA: 0x0007D355 File Offset: 0x0007B555
		public virtual bool TryHandle(ValueException exception, out RuntimeException runtimeException)
		{
			runtimeException = null;
			return false;
		}

		// Token: 0x06002FFF RID: 12287 RVA: 0x00002105 File Offset: 0x00000305
		public virtual bool IsRetryable(Tracer tracer, OdbcException exception, IResource resource, int? retryAttemptCount)
		{
			return false;
		}

		// Token: 0x06003000 RID: 12288 RVA: 0x000912DC File Offset: 0x0008F4DC
		public static ValueException GetOdbcValueException(IEngineHost engineHost, OdbcException odbcException, IResource resource)
		{
			Message2 message = DataSourceException.DataSourceMessage("ODBC", odbcException.Message);
			Value[] array = new Value[odbcException.Errors.Count];
			for (int i = 0; i < array.Length; i++)
			{
				OdbcError odbcError = odbcException.Errors[i];
				array[i] = RecordValue.New(OdbcExceptionHandler.odbcErrorKeys, new Value[]
				{
					TextValue.New(odbcError.SQLState).NewMeta(ValueException.NonPii),
					NumberValue.New(odbcError.NativeError).NewMeta(ValueException.NonPii),
					TextValue.New(odbcError.Message)
				});
			}
			return DataSourceException.NewDataSourceError<Message2>(engineHost, message, resource, "OdbcErrors", ListValue.New(array).ToTable(OdbcExceptionHandler.errorsTableTypeValue), OdbcExceptionHandler.errorsTableTypeValue, odbcException);
		}

		// Token: 0x04001520 RID: 5408
		private const string OdbcErrorsKey = "OdbcErrors";

		// Token: 0x04001521 RID: 5409
		private static readonly Keys odbcErrorKeys = Keys.New("SQLState", "NativeError", "Message");

		// Token: 0x04001522 RID: 5410
		private static readonly RecordTypeValue errorRecordTypeValue = RecordTypeValue.New(RecordValue.New(OdbcExceptionHandler.odbcErrorKeys, new Value[]
		{
			RecordTypeAlgebra.NewField(TypeValue.Text, false),
			RecordTypeAlgebra.NewField(TypeValue.Number, false),
			RecordTypeAlgebra.NewField(TypeValue.Text, false)
		}));

		// Token: 0x04001523 RID: 5411
		private static readonly TableTypeValue errorsTableTypeValue = TableTypeValue.New(OdbcExceptionHandler.errorRecordTypeValue);

		// Token: 0x04001524 RID: 5412
		private IEngineHost engineHost;
	}
}
