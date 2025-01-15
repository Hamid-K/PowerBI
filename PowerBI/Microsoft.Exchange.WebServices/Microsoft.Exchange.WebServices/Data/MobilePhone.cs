using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002A0 RID: 672
	public sealed class MobilePhone : ISelfValidate
	{
		// Token: 0x060017BB RID: 6075 RVA: 0x00040CA8 File Offset: 0x0003FCA8
		public MobilePhone()
		{
		}

		// Token: 0x060017BC RID: 6076 RVA: 0x00040CB0 File Offset: 0x0003FCB0
		public MobilePhone(string name, string phoneNumber)
		{
			this.name = name;
			this.phoneNumber = phoneNumber;
		}

		// Token: 0x170005C0 RID: 1472
		// (get) Token: 0x060017BD RID: 6077 RVA: 0x00040CC6 File Offset: 0x0003FCC6
		// (set) Token: 0x060017BE RID: 6078 RVA: 0x00040CCE File Offset: 0x0003FCCE
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170005C1 RID: 1473
		// (get) Token: 0x060017BF RID: 6079 RVA: 0x00040CD7 File Offset: 0x0003FCD7
		// (set) Token: 0x060017C0 RID: 6080 RVA: 0x00040CDF File Offset: 0x0003FCDF
		public string PhoneNumber
		{
			get
			{
				return this.phoneNumber;
			}
			set
			{
				this.phoneNumber = value;
			}
		}

		// Token: 0x060017C1 RID: 6081 RVA: 0x00040CE8 File Offset: 0x0003FCE8
		void ISelfValidate.Validate()
		{
			if (string.IsNullOrEmpty(this.PhoneNumber))
			{
				throw new ServiceValidationException("PhoneNumber cannot be empty.");
			}
		}

		// Token: 0x0400137A RID: 4986
		private string name;

		// Token: 0x0400137B RID: 4987
		private string phoneNumber;
	}
}
