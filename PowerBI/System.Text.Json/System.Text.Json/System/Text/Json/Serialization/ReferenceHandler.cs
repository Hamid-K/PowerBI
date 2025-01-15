using System;
using System.Runtime.CompilerServices;

namespace System.Text.Json.Serialization
{
	// Token: 0x02000097 RID: 151
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class ReferenceHandler
	{
		// Token: 0x170001CD RID: 461
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x00027369 File Offset: 0x00025569
		public static ReferenceHandler Preserve { get; } = new PreserveReferenceHandler();

		// Token: 0x170001CE RID: 462
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x00027370 File Offset: 0x00025570
		public static ReferenceHandler IgnoreCycles { get; } = new IgnoreReferenceHandler();

		// Token: 0x0600090A RID: 2314
		public abstract ReferenceResolver CreateResolver();

		// Token: 0x0600090B RID: 2315 RVA: 0x00027377 File Offset: 0x00025577
		internal virtual ReferenceResolver CreateResolver(bool writing)
		{
			return this.CreateResolver();
		}

		// Token: 0x0400030B RID: 779
		internal ReferenceHandlingStrategy HandlingStrategy = ReferenceHandlingStrategy.Preserve;
	}
}
