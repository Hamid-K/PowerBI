using System;

namespace Microsoft.ReportingServices.Diagnostics
{
	// Token: 0x0200001F RID: 31
	public interface IDataProtection
	{
		// Token: 0x060000E1 RID: 225
		byte[] ProtectData(string unprotectedData, string tag);

		// Token: 0x060000E2 RID: 226
		string UnprotectDataToString(byte[] protectedData, string tag);
	}
}
