using System;
using System.Threading.Tasks;

namespace AngleSharp.Dom.Events
{
	// Token: 0x020001E9 RID: 489
	public class InteractivityEvent<T> : Event
	{
		// Token: 0x0600101E RID: 4126 RVA: 0x00047495 File Offset: 0x00045695
		public InteractivityEvent(string eventName, T data)
			: base(eventName, false, false)
		{
			this.Data = data;
		}

		// Token: 0x1700038D RID: 909
		// (get) Token: 0x0600101F RID: 4127 RVA: 0x000474A7 File Offset: 0x000456A7
		public Task Result
		{
			get
			{
				return this._result;
			}
		}

		// Token: 0x06001020 RID: 4128 RVA: 0x000474AF File Offset: 0x000456AF
		public void SetResult(Task value)
		{
			if (this._result != null)
			{
				this._result = TaskEx.WhenAll(new Task[] { this._result, value });
				return;
			}
			this._result = value;
		}

		// Token: 0x1700038E RID: 910
		// (get) Token: 0x06001021 RID: 4129 RVA: 0x000474DF File Offset: 0x000456DF
		// (set) Token: 0x06001022 RID: 4130 RVA: 0x000474E7 File Offset: 0x000456E7
		public T Data { get; private set; }

		// Token: 0x04000A4F RID: 2639
		private Task _result;
	}
}
