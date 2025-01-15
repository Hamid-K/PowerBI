using System;
using System.Diagnostics;
using Microsoft.DataShaping.InternalContracts;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x02000008 RID: 8
	[Serializable]
	internal sealed class SemanticQueryTranslationErrorContext : EngineErrorContextBase<SemanticQueryTranslationMessage>
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002399 File Offset: 0x00000599
		internal SemanticQueryTranslationErrorContext(ITracer tracer)
		{
			this.m_tracer = tracer;
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000023A8 File Offset: 0x000005A8
		public void Register(SemanticQueryTranslationMessage message)
		{
			TraceLevel traceLevel = ((message.Severity == EngineMessageSeverity.Error) ? TraceLevel.Error : TraceLevel.Warning);
			this.m_tracer.SanitizedTrace(traceLevel, "{0} - {1}", new string[] { message.ErrorCode, message.TraceMessage });
			base.Add(message);
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000023F2 File Offset: 0x000005F2
		internal IErrorContext CreateAdapter(string defaultErrorCode, ErrorSource errorSource)
		{
			return new SemanticQueryTranslationErrorContextAdapter(this, defaultErrorCode, errorSource);
		}

		// Token: 0x04000033 RID: 51
		[NonSerialized]
		private readonly ITracer m_tracer;
	}
}
