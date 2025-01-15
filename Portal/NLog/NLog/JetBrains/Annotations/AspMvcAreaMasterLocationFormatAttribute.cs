using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001D7 RID: 471
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class AspMvcAreaMasterLocationFormatAttribute : Attribute
	{
		// Token: 0x06001441 RID: 5185 RVA: 0x000368DB File Offset: 0x00034ADB
		public AspMvcAreaMasterLocationFormatAttribute([NotNull] string format)
		{
			this.Format = format;
		}

		// Token: 0x170003C2 RID: 962
		// (get) Token: 0x06001442 RID: 5186 RVA: 0x000368EA File Offset: 0x00034AEA
		// (set) Token: 0x06001443 RID: 5187 RVA: 0x000368F2 File Offset: 0x00034AF2
		[NotNull]
		public string Format { get; private set; }
	}
}
