using System;
using System.ComponentModel;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003F8 RID: 1016
	public sealed class Parameter
	{
		// Token: 0x0600203B RID: 8251 RVA: 0x000025F4 File Offset: 0x000007F4
		public Parameter()
		{
		}

		// Token: 0x0600203C RID: 8252 RVA: 0x0007F325 File Offset: 0x0007D525
		public Parameter(string name, string value)
		{
			this.Name = name;
			this.Value = new Expression(value);
		}

		// Token: 0x04000E0C RID: 3596
		public string Name;

		// Token: 0x04000E0D RID: 3597
		public Expression Value;

		// Token: 0x04000E0E RID: 3598
		[DefaultValue(typeof(TrueFalseString), "")]
		public TrueFalseString Omit;
	}
}
