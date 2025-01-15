using System;
using System.Linq;
using Microsoft.InfoNav;
using Microsoft.InfoNav.Common;

namespace Microsoft.Lucia.Core
{
	// Token: 0x020000A4 RID: 164
	public static class InterpretRequestVersionExtensions
	{
		// Token: 0x0600033C RID: 828 RVA: 0x00006988 File Offset: 0x00004B88
		public static InterpretRequestVersion FromValue(int value)
		{
			return (InterpretRequestVersion)value;
		}

		// Token: 0x0600033D RID: 829 RVA: 0x0000698B File Offset: 0x00004B8B
		public static int ToValue(this InterpretRequestVersion version)
		{
			return (int)version;
		}

		// Token: 0x0600033E RID: 830 RVA: 0x0000698E File Offset: 0x00004B8E
		internal static bool IsSupported(this InterpretRequestVersion version)
		{
			return InterpretRequestVersionExtensions._supportedVersions.Contains(version);
		}

		// Token: 0x0600033F RID: 831 RVA: 0x0000699B File Offset: 0x00004B9B
		internal static bool IsVersionOrLower(this InterpretRequestVersion version, InterpretRequestVersion maximum)
		{
			return version <= maximum;
		}

		// Token: 0x06000340 RID: 832 RVA: 0x000069A4 File Offset: 0x00004BA4
		internal static bool IsVersionOrHigher(this InterpretRequestVersion version, InterpretRequestVersion minimum)
		{
			return version >= minimum;
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000069AD File Offset: 0x00004BAD
		private static ReadOnlySet<InterpretRequestVersion> CreateSupportedVersions()
		{
			return Enum.GetValues(typeof(InterpretRequestVersion)).Cast<InterpretRequestVersion>().AsReadOnlySet(null);
		}

		// Token: 0x04000398 RID: 920
		private static readonly ReadOnlySet<InterpretRequestVersion> _supportedVersions = InterpretRequestVersionExtensions.CreateSupportedVersions();
	}
}
