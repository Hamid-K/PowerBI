using System;
using System.Collections.ObjectModel;

namespace Microsoft.InfoNav.Explore.ExploreConverter.Internal
{
	// Token: 0x02000009 RID: 9
	internal sealed class ColorRange
	{
		// Token: 0x0600000F RID: 15 RVA: 0x00002401 File Offset: 0x00000601
		internal ColorRange(ReadOnlyCollection<string> colors)
		{
			this.Colors = colors;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002410 File Offset: 0x00000610
		// (set) Token: 0x06000011 RID: 17 RVA: 0x00002418 File Offset: 0x00000618
		public ReadOnlyCollection<string> Colors { get; private set; }
	}
}
