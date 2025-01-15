using System;
using System.Collections.Generic;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200026A RID: 618
	public sealed class QueryResolutionErrorContext
	{
		// Token: 0x060012CC RID: 4812 RVA: 0x00021785 File Offset: 0x0001F985
		public QueryResolutionErrorContext(IErrorContext errorContext)
		{
			this._errorContext = errorContext;
			this._unresolvedModelReferences = new List<IContainsTelemetryMarkup>();
		}

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x060012CD RID: 4813 RVA: 0x0002179F File Offset: 0x0001F99F
		public bool HasError
		{
			get
			{
				return this._errorContext.HasError;
			}
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x060012CE RID: 4814 RVA: 0x000217AC File Offset: 0x0001F9AC
		public List<IContainsTelemetryMarkup> UnresolvedModelReferences
		{
			get
			{
				return this._unresolvedModelReferences;
			}
		}

		// Token: 0x060012CF RID: 4815 RVA: 0x000217B4 File Offset: 0x0001F9B4
		internal void RegisterError(QueryResolutionMessage error)
		{
			this._errorContext.RegisterError(error.Text, new object[0]);
			this.HandleUnresolvedModelReference(error);
		}

		// Token: 0x060012D0 RID: 4816 RVA: 0x000217D4 File Offset: 0x0001F9D4
		internal void RegisterWarning(QueryResolutionMessage warning)
		{
			this._errorContext.RegisterWarning(warning.Text, new object[0]);
			this.HandleUnresolvedModelReference(warning);
		}

		// Token: 0x060012D1 RID: 4817 RVA: 0x000217F4 File Offset: 0x0001F9F4
		private void HandleUnresolvedModelReference(QueryResolutionMessage message)
		{
			if (message.HasUnresolvedModelReference)
			{
				this._unresolvedModelReferences.Add(message.UnresolvedModelReference);
			}
		}

		// Token: 0x040007D1 RID: 2001
		private readonly IErrorContext _errorContext;

		// Token: 0x040007D2 RID: 2002
		private readonly List<IContainsTelemetryMarkup> _unresolvedModelReferences;
	}
}
