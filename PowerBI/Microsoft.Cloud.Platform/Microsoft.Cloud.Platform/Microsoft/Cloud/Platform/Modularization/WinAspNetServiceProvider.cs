using System;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Modularization
{
	// Token: 0x020000D8 RID: 216
	public class WinAspNetServiceProvider : Block, IWinAspNetServiceProvider
	{
		// Token: 0x06000614 RID: 1556 RVA: 0x000157BC File Offset: 0x000139BC
		public WinAspNetServiceProvider()
			: this("WinAspNetApplicationContainer")
		{
		}

		// Token: 0x06000615 RID: 1557 RVA: 0x000157C9 File Offset: 0x000139C9
		public WinAspNetServiceProvider(string name)
			: base(name)
		{
			WinAspNetServiceProvider.s_instance = this;
			this.m_services = new ServicesContainer();
		}

		// Token: 0x06000616 RID: 1558 RVA: 0x000157E3 File Offset: 0x000139E3
		protected override void OnShutdown()
		{
			this.m_services.Deactivate();
			WinAspNetServiceProvider.s_instance = null;
			base.OnShutdown();
		}

		// Token: 0x06000617 RID: 1559 RVA: 0x000157FC File Offset: 0x000139FC
		[CanBeNull]
		public object GetService(Type serviceType)
		{
			if (base.BlockHost == null)
			{
				return null;
			}
			if (!this.m_services.ContainsService(serviceType))
			{
				BlockServiceTicket blockServiceTicket = base.BlockHost.TryGetService(new RequestedBlockService(this, serviceType));
				if (blockServiceTicket == null)
				{
					return null;
				}
				object service = blockServiceTicket.GetService();
				if (!this.m_services.AddService(serviceType, blockServiceTicket, service))
				{
					blockServiceTicket.Dispose();
				}
			}
			return this.m_services[serviceType];
		}

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000618 RID: 1560 RVA: 0x00015862 File Offset: 0x00013A62
		public static IWinAspNetServiceProvider Instance
		{
			get
			{
				return WinAspNetServiceProvider.s_instance;
			}
		}

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000619 RID: 1561 RVA: 0x00015869 File Offset: 0x00013A69
		internal int Count
		{
			get
			{
				return this.m_services.Count;
			}
		}

		// Token: 0x0400021F RID: 543
		private ServicesContainer m_services;

		// Token: 0x04000220 RID: 544
		private static WinAspNetServiceProvider s_instance;
	}
}
