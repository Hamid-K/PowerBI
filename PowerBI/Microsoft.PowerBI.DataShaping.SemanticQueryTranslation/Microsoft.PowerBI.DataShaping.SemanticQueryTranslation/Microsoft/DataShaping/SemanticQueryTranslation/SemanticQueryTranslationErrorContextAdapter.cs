using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.DataShaping.SemanticQueryTranslation
{
	// Token: 0x02000009 RID: 9
	internal sealed class SemanticQueryTranslationErrorContextAdapter : IErrorContext
	{
		// Token: 0x06000012 RID: 18 RVA: 0x000023FC File Offset: 0x000005FC
		internal SemanticQueryTranslationErrorContextAdapter(SemanticQueryTranslationErrorContext errorContext, string defaultErrorCode, ErrorSource errorSource)
		{
			this._errorContext = errorContext;
			this._defaultErrorCode = defaultErrorCode;
			this._errorSource = errorSource;
			this._hasError = false;
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000013 RID: 19 RVA: 0x00002420 File Offset: 0x00000620
		public bool HasError
		{
			get
			{
				return this._hasError;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002428 File Offset: 0x00000628
		public void RegisterError(string messageTemplate, params object[] args)
		{
			SemanticQueryTranslationMessage semanticQueryTranslationMessage = SemanticQueryTranslationMessage.Create(this._defaultErrorCode, messageTemplate, EngineMessageSeverity.Error, this._errorSource, args);
			this._errorContext.Register(semanticQueryTranslationMessage);
			this._hasError = true;
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002460 File Offset: 0x00000660
		public void RegisterWarning(string messageTemplate, params object[] args)
		{
			SemanticQueryTranslationMessage semanticQueryTranslationMessage = SemanticQueryTranslationMessage.Create(this._defaultErrorCode, messageTemplate, EngineMessageSeverity.Warning, this._errorSource, args);
			this._errorContext.Register(semanticQueryTranslationMessage);
		}

		// Token: 0x04000034 RID: 52
		private readonly SemanticQueryTranslationErrorContext _errorContext;

		// Token: 0x04000035 RID: 53
		private readonly string _defaultErrorCode;

		// Token: 0x04000036 RID: 54
		private readonly ErrorSource _errorSource;

		// Token: 0x04000037 RID: 55
		private bool _hasError;
	}
}
