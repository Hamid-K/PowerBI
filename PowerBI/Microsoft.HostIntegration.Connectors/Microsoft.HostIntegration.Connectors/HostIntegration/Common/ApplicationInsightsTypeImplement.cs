using System;

namespace Microsoft.HostIntegration.Common
{
	// Token: 0x020006F3 RID: 1779
	public class ApplicationInsightsTypeImplement<T>
	{
		// Token: 0x17000C96 RID: 3222
		// (get) Token: 0x060038A5 RID: 14501 RVA: 0x000BE227 File Offset: 0x000BC427
		// (set) Token: 0x060038A6 RID: 14502 RVA: 0x000BE22F File Offset: 0x000BC42F
		public object Value { get; protected set; }

		// Token: 0x17000C97 RID: 3223
		// (get) Token: 0x060038A7 RID: 14503 RVA: 0x000BE238 File Offset: 0x000BC438
		// (set) Token: 0x060038A8 RID: 14504 RVA: 0x000BE23F File Offset: 0x000BC43F
		internal static Type Type { get; set; }
	}
}
