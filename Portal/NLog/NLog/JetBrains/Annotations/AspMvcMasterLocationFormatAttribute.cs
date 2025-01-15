using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001DA RID: 474
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
	internal sealed class AspMvcMasterLocationFormatAttribute : Attribute
	{
		// Token: 0x0600144A RID: 5194 RVA: 0x0003693B File Offset: 0x00034B3B
		public AspMvcMasterLocationFormatAttribute([NotNull] string format)
		{
			this.Format = format;
		}

		// Token: 0x170003C5 RID: 965
		// (get) Token: 0x0600144B RID: 5195 RVA: 0x0003694A File Offset: 0x00034B4A
		// (set) Token: 0x0600144C RID: 5196 RVA: 0x00036952 File Offset: 0x00034B52
		[NotNull]
		public string Format { get; private set; }
	}
}
