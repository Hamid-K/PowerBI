using System;

namespace Microsoft.Cloud.Platform.Common
{
	// Token: 0x02000533 RID: 1331
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Interface, AllowMultiple = false, Inherited = false)]
	public sealed class TraceAttribute : Attribute
	{
		// Token: 0x17000697 RID: 1687
		// (get) Token: 0x060028C4 RID: 10436 RVA: 0x000925DB File Offset: 0x000907DB
		// (set) Token: 0x060028C5 RID: 10437 RVA: 0x000925E3 File Offset: 0x000907E3
		public bool Enable { get; set; }

		// Token: 0x17000698 RID: 1688
		// (get) Token: 0x060028C6 RID: 10438 RVA: 0x000925EC File Offset: 0x000907EC
		// (set) Token: 0x060028C7 RID: 10439 RVA: 0x000925F4 File Offset: 0x000907F4
		public Type TraceProvider { get; set; }

		// Token: 0x060028C8 RID: 10440 RVA: 0x000925FD File Offset: 0x000907FD
		public TraceAttribute(Type traceProvider)
		{
			this.Enable = traceProvider != null;
			this.TraceProvider = traceProvider;
		}

		// Token: 0x060028C9 RID: 10441 RVA: 0x00092619 File Offset: 0x00090819
		public TraceAttribute()
			: this(null)
		{
		}
	}
}
