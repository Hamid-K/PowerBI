using System;

namespace System.Text.Json.Serialization
{
	// Token: 0x0200008D RID: 141
	internal sealed class IgnoreReferenceHandler : ReferenceHandler
	{
		// Token: 0x06000879 RID: 2169 RVA: 0x00025D49 File Offset: 0x00023F49
		public IgnoreReferenceHandler()
		{
			this.HandlingStrategy = ReferenceHandlingStrategy.IgnoreCycles;
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x00025D58 File Offset: 0x00023F58
		public override ReferenceResolver CreateResolver()
		{
			return new IgnoreReferenceResolver();
		}
	}
}
