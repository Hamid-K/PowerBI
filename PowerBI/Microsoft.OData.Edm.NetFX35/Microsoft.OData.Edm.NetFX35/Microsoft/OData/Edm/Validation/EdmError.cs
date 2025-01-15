using System;
using System.Globalization;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x0200021A RID: 538
	public class EdmError
	{
		// Token: 0x06000CDB RID: 3291 RVA: 0x0002457F File Offset: 0x0002277F
		public EdmError(EdmLocation errorLocation, EdmErrorCode errorCode, string errorMessage)
		{
			this.ErrorLocation = errorLocation;
			this.ErrorCode = errorCode;
			this.ErrorMessage = errorMessage;
		}

		// Token: 0x17000488 RID: 1160
		// (get) Token: 0x06000CDC RID: 3292 RVA: 0x0002459C File Offset: 0x0002279C
		// (set) Token: 0x06000CDD RID: 3293 RVA: 0x000245A4 File Offset: 0x000227A4
		public EdmLocation ErrorLocation { get; private set; }

		// Token: 0x17000489 RID: 1161
		// (get) Token: 0x06000CDE RID: 3294 RVA: 0x000245AD File Offset: 0x000227AD
		// (set) Token: 0x06000CDF RID: 3295 RVA: 0x000245B5 File Offset: 0x000227B5
		public EdmErrorCode ErrorCode { get; private set; }

		// Token: 0x1700048A RID: 1162
		// (get) Token: 0x06000CE0 RID: 3296 RVA: 0x000245BE File Offset: 0x000227BE
		// (set) Token: 0x06000CE1 RID: 3297 RVA: 0x000245C6 File Offset: 0x000227C6
		public string ErrorMessage { get; private set; }

		// Token: 0x06000CE2 RID: 3298 RVA: 0x000245D0 File Offset: 0x000227D0
		public override string ToString()
		{
			if (this.ErrorLocation != null && !(this.ErrorLocation is ObjectLocation))
			{
				return string.Concat(new string[]
				{
					Convert.ToString(this.ErrorCode, CultureInfo.InvariantCulture),
					" : ",
					this.ErrorMessage,
					" : ",
					this.ErrorLocation.ToString()
				});
			}
			return Convert.ToString(this.ErrorCode, CultureInfo.InvariantCulture) + " : " + this.ErrorMessage;
		}
	}
}
