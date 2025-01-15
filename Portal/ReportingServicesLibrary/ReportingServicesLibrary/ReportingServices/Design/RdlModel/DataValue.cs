using System;

namespace Microsoft.ReportingServices.Design.RdlModel
{
	// Token: 0x020003AF RID: 943
	public class DataValue
	{
		// Token: 0x06001EB9 RID: 7865 RVA: 0x0007D856 File Offset: 0x0007BA56
		public DataValue()
		{
			this.Value = "";
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x0007D869 File Offset: 0x0007BA69
		public DataValue(string sExpression)
		{
			this.Value = sExpression;
		}

		// Token: 0x04000D29 RID: 3369
		public string Value;
	}
}
