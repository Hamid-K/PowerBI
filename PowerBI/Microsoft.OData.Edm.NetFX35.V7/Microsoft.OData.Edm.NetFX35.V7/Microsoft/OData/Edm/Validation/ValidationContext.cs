using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x020000D5 RID: 213
	public sealed class ValidationContext
	{
		// Token: 0x06000637 RID: 1591 RVA: 0x0000F91E File Offset: 0x0000DB1E
		internal ValidationContext(IEdmModel model, Func<object, bool> isBad)
		{
			this.model = model;
			this.isBad = isBad;
		}

		// Token: 0x170001BC RID: 444
		// (get) Token: 0x06000638 RID: 1592 RVA: 0x0000F93F File Offset: 0x0000DB3F
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x170001BD RID: 445
		// (get) Token: 0x06000639 RID: 1593 RVA: 0x0000F947 File Offset: 0x0000DB47
		internal IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x0600063A RID: 1594 RVA: 0x0000F94F File Offset: 0x0000DB4F
		public bool IsBad(IEdmElement element)
		{
			return this.isBad.Invoke(element);
		}

		// Token: 0x0600063B RID: 1595 RVA: 0x0000F95D File Offset: 0x0000DB5D
		public void AddError(EdmLocation location, EdmErrorCode errorCode, string errorMessage)
		{
			this.AddError(new EdmError(location, errorCode, errorMessage));
		}

		// Token: 0x0600063C RID: 1596 RVA: 0x0000F96D File Offset: 0x0000DB6D
		public void AddError(EdmError error)
		{
			this.errors.Add(error);
		}

		// Token: 0x040002AC RID: 684
		private readonly List<EdmError> errors = new List<EdmError>();

		// Token: 0x040002AD RID: 685
		private readonly IEdmModel model;

		// Token: 0x040002AE RID: 686
		private readonly Func<object, bool> isBad;
	}
}
