using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x02000270 RID: 624
	public sealed class ValidationContext
	{
		// Token: 0x06000DAC RID: 3500 RVA: 0x0002729D File Offset: 0x0002549D
		internal ValidationContext(IEdmModel model, Func<object, bool> isBad)
		{
			this.model = model;
			this.isBad = isBad;
		}

		// Token: 0x1700048B RID: 1163
		// (get) Token: 0x06000DAD RID: 3501 RVA: 0x000272BE File Offset: 0x000254BE
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x1700048C RID: 1164
		// (get) Token: 0x06000DAE RID: 3502 RVA: 0x000272C6 File Offset: 0x000254C6
		internal IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x000272CE File Offset: 0x000254CE
		public bool IsBad(IEdmElement element)
		{
			return this.isBad.Invoke(element);
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x000272DC File Offset: 0x000254DC
		public void AddError(EdmLocation location, EdmErrorCode errorCode, string errorMessage)
		{
			this.AddError(new EdmError(location, errorCode, errorMessage));
		}

		// Token: 0x06000DB1 RID: 3505 RVA: 0x000272EC File Offset: 0x000254EC
		public void AddError(EdmError error)
		{
			this.errors.Add(error);
		}

		// Token: 0x040005DC RID: 1500
		private readonly List<EdmError> errors = new List<EdmError>();

		// Token: 0x040005DD RID: 1501
		private readonly IEdmModel model;

		// Token: 0x040005DE RID: 1502
		private readonly Func<object, bool> isBad;
	}
}
