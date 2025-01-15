using System;
using System.Collections.Generic;

namespace Microsoft.Extensions.DependencyInjection.ServiceLookup
{
	// Token: 0x0200000D RID: 13
	internal class ClosedIEnumerableService : IService
	{
		// Token: 0x0600007B RID: 123 RVA: 0x000034D1 File Offset: 0x000016D1
		public ClosedIEnumerableService(Type itemType, ServiceEntry entry)
		{
			this._itemType = itemType;
			this._serviceEntry = entry;
		}

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600007C RID: 124 RVA: 0x000034E7 File Offset: 0x000016E7
		// (set) Token: 0x0600007D RID: 125 RVA: 0x000034EF File Offset: 0x000016EF
		public IService Next { get; set; }

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x0600007E RID: 126 RVA: 0x000034F8 File Offset: 0x000016F8
		public ServiceLifetime Lifetime
		{
			get
			{
				return ServiceLifetime.Transient;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600007F RID: 127 RVA: 0x000034FB File Offset: 0x000016FB
		public Type ServiceType
		{
			get
			{
				return this._itemType;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00003504 File Offset: 0x00001704
		public IServiceCallSite CreateCallSite(ServiceProvider provider, ISet<Type> callSiteChain)
		{
			List<IServiceCallSite> list = new List<IServiceCallSite>();
			for (IService service = this._serviceEntry.First; service != null; service = service.Next)
			{
				list.Add(provider.GetResolveCallSite(service, callSiteChain));
			}
			return new ClosedIEnumerableCallSite(this._itemType, list.ToArray());
		}

		// Token: 0x0400001B RID: 27
		private readonly Type _itemType;

		// Token: 0x0400001C RID: 28
		private readonly ServiceEntry _serviceEntry;
	}
}
