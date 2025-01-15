using System;
using System.Globalization;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x020000D3 RID: 211
	public class EdmError
	{
		// Token: 0x0600062C RID: 1580 RVA: 0x0000F7E7 File Offset: 0x0000D9E7
		public EdmError(EdmLocation errorLocation, EdmErrorCode errorCode, string errorMessage)
		{
			this.ErrorLocation = errorLocation;
			this.ErrorCode = errorCode;
			this.ErrorMessage = errorMessage;
		}

		// Token: 0x170001B9 RID: 441
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0000F804 File Offset: 0x0000DA04
		// (set) Token: 0x0600062E RID: 1582 RVA: 0x0000F80C File Offset: 0x0000DA0C
		public EdmLocation ErrorLocation { get; private set; }

		// Token: 0x170001BA RID: 442
		// (get) Token: 0x0600062F RID: 1583 RVA: 0x0000F815 File Offset: 0x0000DA15
		// (set) Token: 0x06000630 RID: 1584 RVA: 0x0000F81D File Offset: 0x0000DA1D
		public EdmErrorCode ErrorCode { get; private set; }

		// Token: 0x170001BB RID: 443
		// (get) Token: 0x06000631 RID: 1585 RVA: 0x0000F826 File Offset: 0x0000DA26
		// (set) Token: 0x06000632 RID: 1586 RVA: 0x0000F82E File Offset: 0x0000DA2E
		public string ErrorMessage { get; private set; }

		// Token: 0x06000633 RID: 1587 RVA: 0x0000F838 File Offset: 0x0000DA38
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
