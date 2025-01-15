using System;
using System.Collections.Generic;
using Microsoft.Cloud.Platform.Modularization;

namespace Microsoft.Cloud.Platform.Application
{
	// Token: 0x020003F1 RID: 1009
	public class WinAspNetApplicationRoot : ElementApplicationRoot
	{
		// Token: 0x06001F02 RID: 7938 RVA: 0x0007419E File Offset: 0x0007239E
		public WinAspNetApplicationRoot()
			: base("WinAspNetApplicationRoot")
		{
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x000741AC File Offset: 0x000723AC
		protected override void OnInitialize()
		{
			WinAspNetServiceProvider winAspNetServiceProvider = new WinAspNetServiceProvider("WinAspNetServiceProvider");
			base.AddBlock(winAspNetServiceProvider);
			base.OnInitialize();
			IList<IBlock> list = Loader.LoadConfiguredBlocks();
			base.AddBlocks(list);
		}
	}
}
