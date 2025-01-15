using System;
using Microsoft.Mashup.Engine.Interface.Tracing;

namespace Microsoft.Mashup.Engine1.Library.SapHana
{
	// Token: 0x0200041C RID: 1052
	internal sealed class SapHanaAdditionalTracesProvider
	{
		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x060023E3 RID: 9187 RVA: 0x0006530E File Offset: 0x0006350E
		// (set) Token: 0x060023E4 RID: 9188 RVA: 0x00065316 File Offset: 0x00063516
		public int? SessionId { get; set; }

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x060023E5 RID: 9189 RVA: 0x0006531F File Offset: 0x0006351F
		// (set) Token: 0x060023E6 RID: 9190 RVA: 0x00065327 File Offset: 0x00063527
		public int? Implementation { get; set; }

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x060023E7 RID: 9191 RVA: 0x00065330 File Offset: 0x00063530
		// (set) Token: 0x060023E8 RID: 9192 RVA: 0x00065338 File Offset: 0x00063538
		public bool? UseMultipleRowFetch { get; set; }

		// Token: 0x060023E9 RID: 9193 RVA: 0x00065344 File Offset: 0x00063544
		public void GetAdditionalTraces(IHostTrace trace)
		{
			if (this.SessionId != null)
			{
				trace.Add("SessionId", this.SessionId.Value, false);
			}
			if (this.Implementation != null)
			{
				trace.Add("Implementation", this.Implementation.Value, false);
			}
			if (this.UseMultipleRowFetch != null)
			{
				trace.Add("UseMultipleRowFetch", this.UseMultipleRowFetch.Value, false);
			}
		}
	}
}
