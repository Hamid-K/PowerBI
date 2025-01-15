using System;
using System.Runtime.InteropServices;

namespace Microsoft.PowerBI.DataMovement.Pipeline.OleDbBase
{
	// Token: 0x020000AD RID: 173
	[StructLayout(LayoutKind.Sequential, Pack = 8)]
	public struct COAUTHIDENTITY
	{
		// Token: 0x04000331 RID: 817
		public unsafe char* User;

		// Token: 0x04000332 RID: 818
		public uint UserLength;

		// Token: 0x04000333 RID: 819
		public unsafe char* Domain;

		// Token: 0x04000334 RID: 820
		public uint DomainLength;

		// Token: 0x04000335 RID: 821
		public unsafe char* Password;

		// Token: 0x04000336 RID: 822
		public uint PasswordLength;

		// Token: 0x04000337 RID: 823
		public uint Flags;
	}
}
