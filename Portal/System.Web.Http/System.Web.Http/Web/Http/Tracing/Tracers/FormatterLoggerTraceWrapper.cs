using System;
using System.Net.Http;
using System.Net.Http.Formatting;

namespace System.Web.Http.Tracing.Tracers
{
	// Token: 0x02000122 RID: 290
	internal class FormatterLoggerTraceWrapper : IFormatterLogger
	{
		// Token: 0x060007B8 RID: 1976 RVA: 0x00013717 File Offset: 0x00011917
		public FormatterLoggerTraceWrapper(IFormatterLogger formatterLogger, ITraceWriter traceWriter, HttpRequestMessage request, string operatorName, string operationName)
		{
			this._formatterLogger = formatterLogger;
			this._traceWriter = traceWriter;
			this._request = request;
			this._operatorName = operatorName;
			this._operationName = operationName;
		}

		// Token: 0x060007B9 RID: 1977 RVA: 0x00013744 File Offset: 0x00011944
		public void LogError(string errorPath, string errorMessage)
		{
			this._traceWriter.Trace(this._request, TraceCategories.FormattingCategory, TraceLevel.Error, delegate(TraceRecord traceRecord)
			{
				traceRecord.Kind = TraceKind.Trace;
				traceRecord.Operator = this._operatorName;
				traceRecord.Operation = this._operationName;
				traceRecord.Message = errorMessage;
			});
			this._formatterLogger.LogError(errorPath, errorMessage);
		}

		// Token: 0x060007BA RID: 1978 RVA: 0x0001379C File Offset: 0x0001199C
		public void LogError(string errorPath, Exception exception)
		{
			this._traceWriter.Trace(this._request, TraceCategories.FormattingCategory, TraceLevel.Error, delegate(TraceRecord traceRecord)
			{
				traceRecord.Kind = TraceKind.Trace;
				traceRecord.Operator = this._operatorName;
				traceRecord.Operation = this._operationName;
				traceRecord.Exception = exception;
			});
			this._formatterLogger.LogError(errorPath, exception);
		}

		// Token: 0x04000209 RID: 521
		private readonly IFormatterLogger _formatterLogger;

		// Token: 0x0400020A RID: 522
		private readonly ITraceWriter _traceWriter;

		// Token: 0x0400020B RID: 523
		private readonly HttpRequestMessage _request;

		// Token: 0x0400020C RID: 524
		private readonly string _operatorName;

		// Token: 0x0400020D RID: 525
		private readonly string _operationName;
	}
}
