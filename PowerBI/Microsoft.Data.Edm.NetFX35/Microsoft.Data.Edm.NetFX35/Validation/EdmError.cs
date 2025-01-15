using System;
using System.Globalization;

namespace Microsoft.Data.Edm.Validation
{
	// Token: 0x020001ED RID: 493
	public class EdmError
	{
		// Token: 0x06000BDA RID: 3034 RVA: 0x0002293B File Offset: 0x00020B3B
		public EdmError(EdmLocation errorLocation, EdmErrorCode errorCode, string errorMessage)
		{
			this.ErrorLocation = errorLocation;
			this.ErrorCode = errorCode;
			this.ErrorMessage = errorMessage;
		}

		// Token: 0x1700046C RID: 1132
		// (get) Token: 0x06000BDB RID: 3035 RVA: 0x00022958 File Offset: 0x00020B58
		// (set) Token: 0x06000BDC RID: 3036 RVA: 0x00022960 File Offset: 0x00020B60
		public EdmLocation ErrorLocation { get; private set; }

		// Token: 0x1700046D RID: 1133
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x00022969 File Offset: 0x00020B69
		// (set) Token: 0x06000BDE RID: 3038 RVA: 0x00022971 File Offset: 0x00020B71
		public EdmErrorCode ErrorCode { get; private set; }

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0002297A File Offset: 0x00020B7A
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x00022982 File Offset: 0x00020B82
		public string ErrorMessage { get; private set; }

		// Token: 0x06000BE1 RID: 3041 RVA: 0x0002298C File Offset: 0x00020B8C
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
