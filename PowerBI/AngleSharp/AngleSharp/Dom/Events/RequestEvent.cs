using System;
using AngleSharp.Html;
using AngleSharp.Network;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001F0 RID: 496
	public class RequestEvent : Event
	{
		// Token: 0x0600106A RID: 4202 RVA: 0x00047891 File Offset: 0x00045A91
		public RequestEvent(IRequest request, IResponse response)
			: base((response != null) ? EventNames.RequestEnd : EventNames.RequestStart, false, false)
		{
			this.Response = response;
			this.Request = request;
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x0600106B RID: 4203 RVA: 0x000478B8 File Offset: 0x00045AB8
		// (set) Token: 0x0600106C RID: 4204 RVA: 0x000478C0 File Offset: 0x00045AC0
		public IRequest Request { get; private set; }

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x0600106D RID: 4205 RVA: 0x000478C9 File Offset: 0x00045AC9
		// (set) Token: 0x0600106E RID: 4206 RVA: 0x000478D1 File Offset: 0x00045AD1
		public IResponse Response { get; private set; }
	}
}
