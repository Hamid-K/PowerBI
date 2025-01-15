using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002BD RID: 701
	public sealed class StreamingSubscription : SubscriptionBase
	{
		// Token: 0x060018F3 RID: 6387 RVA: 0x0004402F File Offset: 0x0004302F
		internal StreamingSubscription(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x060018F4 RID: 6388 RVA: 0x00044038 File Offset: 0x00043038
		public void Unsubscribe()
		{
			this.Service.Unsubscribe(base.Id);
		}

		// Token: 0x060018F5 RID: 6389 RVA: 0x0004404B File Offset: 0x0004304B
		public IAsyncResult BeginUnsubscribe(AsyncCallback callback, object state)
		{
			return this.Service.BeginUnsubscribe(callback, state, base.Id);
		}

		// Token: 0x060018F6 RID: 6390 RVA: 0x00044060 File Offset: 0x00043060
		public void EndUnsubscribe(IAsyncResult asyncResult)
		{
			this.Service.EndUnsubscribe(asyncResult);
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x060018F7 RID: 6391 RVA: 0x0004406E File Offset: 0x0004306E
		public new ExchangeService Service
		{
			get
			{
				return base.Service;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x060018F8 RID: 6392 RVA: 0x00044076 File Offset: 0x00043076
		protected override bool UsesWatermark
		{
			get
			{
				return false;
			}
		}
	}
}
