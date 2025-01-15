using System;

namespace Microsoft.HostIntegration.Drda.Common
{
	// Token: 0x0200080B RID: 2059
	public interface ICcsidManager
	{
		// Token: 0x06004100 RID: 16640
		byte[] GetBytes(string s);

		// Token: 0x06004101 RID: 16641
		int GetBytes(string s, byte[] abyte0, int i);

		// Token: 0x06004102 RID: 16642
		string GetString(byte[] abyte0);

		// Token: 0x06004103 RID: 16643
		string GetString(byte[] abyte0, int i, int j);
	}
}
