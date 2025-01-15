using System;
using System.Collections.Generic;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020002E8 RID: 744
	public sealed class UriAndFragmentComparer : IEqualityComparer<Uri>
	{
		// Token: 0x060013D6 RID: 5078 RVA: 0x00044D0E File Offset: 0x00042F0E
		public bool Equals(Uri x, Uri y)
		{
			return x == y || (x != null && y != null && x.Equals(y) && string.Equals(x.Fragment, y.Fragment, StringComparison.OrdinalIgnoreCase));
		}

		// Token: 0x060013D7 RID: 5079 RVA: 0x00044D47 File Offset: 0x00042F47
		public int GetHashCode(Uri obj)
		{
			return obj.GetHashCode();
		}
	}
}
