using System;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x0200001D RID: 29
	internal class ServiceScope : IServiceScope, IDisposable
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00003A6D File Offset: 0x00001C6D
		public ServiceScope(ServiceProvider scopedProvider)
		{
			this._scopedProvider = scopedProvider;
		}

		// Token: 0x17000033 RID: 51
		// (get) Token: 0x060000BE RID: 190 RVA: 0x00003A7C File Offset: 0x00001C7C
		public IServiceProvider ServiceProvider
		{
			get
			{
				return this._scopedProvider;
			}
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00003A84 File Offset: 0x00001C84
		public void Dispose()
		{
			this._scopedProvider.Dispose();
		}

		// Token: 0x04000032 RID: 50
		private readonly ServiceProvider _scopedProvider;
	}
}
