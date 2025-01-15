using System;

namespace Microsoft.ReportingServices.ReportProcessing.Persistence
{
	// Token: 0x0200079F RID: 1951
	internal sealed class VersionStamp
	{
		// Token: 0x06006C63 RID: 27747 RVA: 0x001B788E File Offset: 0x001B5A8E
		internal static byte[] GetBytes()
		{
			return VersionStamp.Stamp;
		}

		// Token: 0x06006C64 RID: 27748 RVA: 0x001B7898 File Offset: 0x001B5A98
		internal static bool Validate(byte[] stamp)
		{
			if (stamp == null)
			{
				return false;
			}
			if (VersionStamp.Stamp.Length != stamp.Length)
			{
				return false;
			}
			for (int i = 0; i < VersionStamp.Stamp.Length; i++)
			{
				if (VersionStamp.Stamp[i] != stamp[i])
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x04003965 RID: 14693
		private static readonly byte[] Stamp = new byte[]
		{
			146, 87, 240, 123, 205, 241, 175, 78, 136, 213,
			28, 14, 76, 128, 111, 25
		};
	}
}
