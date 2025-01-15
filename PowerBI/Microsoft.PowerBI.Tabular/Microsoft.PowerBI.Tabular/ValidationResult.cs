using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace Microsoft.AnalysisServices.Tabular
{
	// Token: 0x0200011D RID: 285
	public class ValidationResult
	{
		// Token: 0x06001245 RID: 4677 RVA: 0x00080879 File Offset: 0x0007EA79
		internal ValidationResult()
		{
			this.errors = new List<ValidationError>();
			this.errorsReadOnly = this.errors.AsReadOnly();
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001246 RID: 4678 RVA: 0x0008089D File Offset: 0x0007EA9D
		public bool ContainsErrors
		{
			get
			{
				return this.errors.Count > 0;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001247 RID: 4679 RVA: 0x000808AD File Offset: 0x0007EAAD
		public ReadOnlyCollection<ValidationError> Errors
		{
			get
			{
				return this.errorsReadOnly;
			}
		}

		// Token: 0x06001248 RID: 4680 RVA: 0x000808B5 File Offset: 0x0007EAB5
		internal void AddError(ValidationError error)
		{
			this.errors.Add(error);
		}

		// Token: 0x040002E6 RID: 742
		private List<ValidationError> errors;

		// Token: 0x040002E7 RID: 743
		private ReadOnlyCollection<ValidationError> errorsReadOnly;
	}
}
