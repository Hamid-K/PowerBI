using System;
using Microsoft.DataShaping.ServiceContracts;
using Microsoft.InfoNav.Data.Contracts.Internal;
using Microsoft.PowerBI.Query.Contracts;

namespace Microsoft.InfoNav.DataShapeQueryGeneration
{
	// Token: 0x02000049 RID: 73
	internal sealed class DataShapeGenerationErrorContextAdapter : IErrorContext
	{
		// Token: 0x0600026D RID: 621 RVA: 0x0000A88C File Offset: 0x00008A8C
		internal DataShapeGenerationErrorContextAdapter(DataShapeGenerationErrorContext context, DataShapeGenerationErrorCode errorCode, ErrorSource errorSource)
		{
			this._context = context;
			this._errorCode = errorCode;
			this._errorSource = errorSource;
			this._hasError = false;
		}

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x0600026E RID: 622 RVA: 0x0000A8B0 File Offset: 0x00008AB0
		public bool HasError
		{
			get
			{
				return this._hasError;
			}
		}

		// Token: 0x0600026F RID: 623 RVA: 0x0000A8B8 File Offset: 0x00008AB8
		public void RegisterError(string messageTemplate, params object[] args)
		{
			DataShapeGenerationMessage dataShapeGenerationMessage = DataShapeGenerationMessage.Create(this._errorCode, messageTemplate, EngineMessageSeverity.Error, this._errorSource, args);
			this._context.Register(dataShapeGenerationMessage);
			this._hasError = true;
		}

		// Token: 0x06000270 RID: 624 RVA: 0x0000A8F0 File Offset: 0x00008AF0
		public void RegisterWarning(string messageTemplate, params object[] args)
		{
			DataShapeGenerationMessage dataShapeGenerationMessage = DataShapeGenerationMessage.Create(this._errorCode, messageTemplate, EngineMessageSeverity.Warning, this._errorSource, args);
			this._context.Register(dataShapeGenerationMessage);
		}

		// Token: 0x040001F6 RID: 502
		private readonly DataShapeGenerationErrorContext _context;

		// Token: 0x040001F7 RID: 503
		private readonly DataShapeGenerationErrorCode _errorCode;

		// Token: 0x040001F8 RID: 504
		private readonly ErrorSource _errorSource;

		// Token: 0x040001F9 RID: 505
		private bool _hasError;
	}
}
