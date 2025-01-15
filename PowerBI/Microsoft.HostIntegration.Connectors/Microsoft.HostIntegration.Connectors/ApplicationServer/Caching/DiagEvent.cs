using System;
using System.Globalization;
using System.Runtime.Serialization;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020001AD RID: 429
	[DataContract(Name = "Event", Namespace = "http://schemas.microsoft.com/velocity/namespace")]
	internal class DiagEvent
	{
		// Token: 0x1700032F RID: 815
		// (get) Token: 0x06000DF4 RID: 3572 RVA: 0x0002F67C File Offset: 0x0002D87C
		// (set) Token: 0x06000DF5 RID: 3573 RVA: 0x0002F684 File Offset: 0x0002D884
		public long Ticks { get; set; }

		// Token: 0x17000330 RID: 816
		// (get) Token: 0x06000DF6 RID: 3574 RVA: 0x0002F68D File Offset: 0x0002D88D
		// (set) Token: 0x06000DF7 RID: 3575 RVA: 0x0002F695 File Offset: 0x0002D895
		public DiagEventName CurrentEventName { get; set; }

		// Token: 0x17000331 RID: 817
		// (get) Token: 0x06000DF8 RID: 3576 RVA: 0x0002F69E File Offset: 0x0002D89E
		// (set) Token: 0x06000DF9 RID: 3577 RVA: 0x0002F6A6 File Offset: 0x0002D8A6
		public string RawData { get; set; }

		// Token: 0x17000332 RID: 818
		// (get) Token: 0x06000DFA RID: 3578 RVA: 0x0002F6AF File Offset: 0x0002D8AF
		// (set) Token: 0x06000DFB RID: 3579 RVA: 0x0002F6B7 File Offset: 0x0002D8B7
		public bool Result { get; set; }

		// Token: 0x06000DFC RID: 3580 RVA: 0x00002061 File Offset: 0x00000261
		public DiagEvent()
		{
		}

		// Token: 0x06000DFD RID: 3581 RVA: 0x0002F6C0 File Offset: 0x0002D8C0
		public DiagEvent(DiagEventName curState, bool res)
		{
			this.Ticks = DateTime.UtcNow.Ticks;
			this.CurrentEventName = curState;
			this.Result = res;
		}

		// Token: 0x06000DFE RID: 3582 RVA: 0x0002F6F4 File Offset: 0x0002D8F4
		public DiagEvent(DiagEventName curState)
			: this(curState, true)
		{
		}

		// Token: 0x06000DFF RID: 3583 RVA: 0x0002F6FE File Offset: 0x0002D8FE
		public DiagEvent(DiagEventName curState, string data, bool res)
			: this(curState, res)
		{
			this.RawData = data;
		}

		// Token: 0x06000E00 RID: 3584 RVA: 0x0002F710 File Offset: 0x0002D910
		public override string ToString()
		{
			return string.Format(CultureInfo.InvariantCulture, "{0}:{1}:{2}:{3}", new object[]
			{
				DiagConfigManager.EventNames[(int)this.CurrentEventName],
				this.Ticks,
				this.Result,
				this.RawData
			});
		}
	}
}
