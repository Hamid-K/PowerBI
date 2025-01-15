using System;

namespace Microsoft.ReportingServices.ReportIntermediateFormat.Persistence
{
	// Token: 0x02000546 RID: 1350
	internal sealed class VersionStamp
	{
		// Token: 0x060049A1 RID: 18849 RVA: 0x00137352 File Offset: 0x00135552
		internal static byte[] GetBytes()
		{
			return VersionStamp.Stamp;
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x0013735C File Offset: 0x0013555C
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

		// Token: 0x040020AA RID: 8362
		private static readonly byte[] Stamp = new byte[]
		{
			146, 87, 240, 123, 205, 241, 175, 78, 136, 213,
			28, 14, 76, 128, 111, 25
		};
	}
}
