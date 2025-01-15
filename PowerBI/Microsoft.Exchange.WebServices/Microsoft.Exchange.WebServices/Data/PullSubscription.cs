using System;

namespace Microsoft.Exchange.WebServices.Data
{
	// Token: 0x020002BB RID: 699
	public sealed class PullSubscription : SubscriptionBase
	{
		// Token: 0x060018EA RID: 6378 RVA: 0x00043F46 File Offset: 0x00042F46
		internal PullSubscription(ExchangeService service)
			: base(service)
		{
		}

		// Token: 0x060018EB RID: 6379 RVA: 0x00043F50 File Offset: 0x00042F50
		public GetEventsResults GetEvents()
		{
			GetEventsResults events = base.Service.GetEvents(base.Id, base.Watermark);
			base.Watermark = events.NewWatermark;
			this.moreEventsAvailable = new bool?(events.MoreEventsAvailable);
			return events;
		}

		// Token: 0x060018EC RID: 6380 RVA: 0x00043F93 File Offset: 0x00042F93
		public IAsyncResult BeginGetEvents(AsyncCallback callback, object state)
		{
			return base.Service.BeginGetEvents(callback, state, base.Id, base.Watermark);
		}

		// Token: 0x060018ED RID: 6381 RVA: 0x00043FB0 File Offset: 0x00042FB0
		public GetEventsResults EndGetEvents(IAsyncResult asyncResult)
		{
			GetEventsResults getEventsResults = base.Service.EndGetEvents(asyncResult);
			base.Watermark = getEventsResults.NewWatermark;
			this.moreEventsAvailable = new bool?(getEventsResults.MoreEventsAvailable);
			return getEventsResults;
		}

		// Token: 0x060018EE RID: 6382 RVA: 0x00043FE8 File Offset: 0x00042FE8
		public void Unsubscribe()
		{
			base.Service.Unsubscribe(base.Id);
		}

		// Token: 0x060018EF RID: 6383 RVA: 0x00043FFB File Offset: 0x00042FFB
		public IAsyncResult BeginUnsubscribe(AsyncCallback callback, object state)
		{
			return base.Service.BeginUnsubscribe(callback, state, base.Id);
		}

		// Token: 0x060018F0 RID: 6384 RVA: 0x00044010 File Offset: 0x00043010
		public void EndUnsubscribe(IAsyncResult asyncResult)
		{
			base.Service.EndUnsubscribe(asyncResult);
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x060018F1 RID: 6385 RVA: 0x0004401E File Offset: 0x0004301E
		public bool? MoreEventsAvailable
		{
			get
			{
				return this.moreEventsAvailable;
			}
		}

		// Token: 0x040013E1 RID: 5089
		private bool? moreEventsAvailable;
	}
}
