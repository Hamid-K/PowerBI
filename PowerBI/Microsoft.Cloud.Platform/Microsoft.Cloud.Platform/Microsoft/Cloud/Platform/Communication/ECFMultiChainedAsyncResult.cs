using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using Microsoft.Cloud.Platform.Utils;

namespace Microsoft.Cloud.Platform.Communication
{
	// Token: 0x02000499 RID: 1177
	public class ECFMultiChainedAsyncResult<T> : MultiChainedAsyncResult<WorkTicket, Collection<Pair<IResourceHandle<T>, EndpointIdentifier>>>
	{
		// Token: 0x06002451 RID: 9297 RVA: 0x00082FC0 File Offset: 0x000811C0
		public ECFMultiChainedAsyncResult(AsyncCallback callback, object context, Collection<Pair<IResourceHandle<T>, EndpointIdentifier>> endpoints)
			: base(callback, context, null, endpoints)
		{
		}

		// Token: 0x170005EA RID: 1514
		// (get) Token: 0x06002452 RID: 9298 RVA: 0x00082FCC File Offset: 0x000811CC
		// (set) Token: 0x06002453 RID: 9299 RVA: 0x00082FD4 File Offset: 0x000811D4
		public IList<EndpointFault> Exceptions { get; set; }

		// Token: 0x170005EB RID: 1515
		// (get) Token: 0x06002454 RID: 9300 RVA: 0x00082FDD File Offset: 0x000811DD
		public bool BeginThrewException
		{
			get
			{
				return this.Exceptions != null && this.Exceptions.Count<EndpointFault>() > 0;
			}
		}
	}
}
