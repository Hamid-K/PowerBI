using System;
using System.Globalization;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x02000142 RID: 322
	public class EdmError
	{
		// Token: 0x0600082A RID: 2090 RVA: 0x00014B83 File Offset: 0x00012D83
		public EdmError(EdmLocation errorLocation, EdmErrorCode errorCode, string errorMessage)
		{
			this.ErrorLocation = errorLocation;
			this.ErrorCode = errorCode;
			this.ErrorMessage = errorMessage;
		}

		// Token: 0x1700028F RID: 655
		// (get) Token: 0x0600082B RID: 2091 RVA: 0x00014BA0 File Offset: 0x00012DA0
		// (set) Token: 0x0600082C RID: 2092 RVA: 0x00014BA8 File Offset: 0x00012DA8
		public EdmLocation ErrorLocation { get; private set; }

		// Token: 0x17000290 RID: 656
		// (get) Token: 0x0600082D RID: 2093 RVA: 0x00014BB1 File Offset: 0x00012DB1
		// (set) Token: 0x0600082E RID: 2094 RVA: 0x00014BB9 File Offset: 0x00012DB9
		public EdmErrorCode ErrorCode { get; private set; }

		// Token: 0x17000291 RID: 657
		// (get) Token: 0x0600082F RID: 2095 RVA: 0x00014BC2 File Offset: 0x00012DC2
		// (set) Token: 0x06000830 RID: 2096 RVA: 0x00014BCA File Offset: 0x00012DCA
		public string ErrorMessage { get; private set; }

		// Token: 0x06000831 RID: 2097 RVA: 0x00014BD4 File Offset: 0x00012DD4
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
