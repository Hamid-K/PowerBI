using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x02000144 RID: 324
	public sealed class ValidationContext
	{
		// Token: 0x06000835 RID: 2101 RVA: 0x00014CBA File Offset: 0x00012EBA
		internal ValidationContext(IEdmModel model, Func<object, bool> isBad)
		{
			this.model = model;
			this.isBad = isBad;
		}

		// Token: 0x17000292 RID: 658
		// (get) Token: 0x06000836 RID: 2102 RVA: 0x00014CDB File Offset: 0x00012EDB
		public IEdmModel Model
		{
			get
			{
				return this.model;
			}
		}

		// Token: 0x17000293 RID: 659
		// (get) Token: 0x06000837 RID: 2103 RVA: 0x00014CE3 File Offset: 0x00012EE3
		internal IEnumerable<EdmError> Errors
		{
			get
			{
				return this.errors;
			}
		}

		// Token: 0x06000838 RID: 2104 RVA: 0x00014CEB File Offset: 0x00012EEB
		public bool IsBad(IEdmElement element)
		{
			return this.isBad(element);
		}

		// Token: 0x06000839 RID: 2105 RVA: 0x00014CF9 File Offset: 0x00012EF9
		public void AddError(EdmLocation location, EdmErrorCode errorCode, string errorMessage)
		{
			this.AddError(new EdmError(location, errorCode, errorMessage));
		}

		// Token: 0x0600083A RID: 2106 RVA: 0x00014D09 File Offset: 0x00012F09
		public void AddError(EdmError error)
		{
			this.errors.Add(error);
		}

		// Token: 0x0400038E RID: 910
		private readonly List<EdmError> errors = new List<EdmError>();

		// Token: 0x0400038F RID: 911
		private readonly IEdmModel model;

		// Token: 0x04000390 RID: 912
		private readonly Func<object, bool> isBad;
	}
}
