using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000028 RID: 40
	internal sealed class VersionStamp
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00008133 File Offset: 0x00006333
		internal static byte[] GetBytes()
		{
			return VersionStamp.Stamp;
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x0000813C File Offset: 0x0000633C
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

		// Token: 0x0400012A RID: 298
		private static readonly byte[] Stamp = new byte[]
		{
			146, 87, 240, 123, 205, 241, 175, 78, 136, 213,
			28, 14, 76, 128, 111, 25
		};
	}
}
