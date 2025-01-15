using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001C4 RID: 452
	[AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
	internal sealed class ValueProviderAttribute : Attribute
	{
		// Token: 0x06001404 RID: 5124 RVA: 0x00036679 File Offset: 0x00034879
		public ValueProviderAttribute([NotNull] string name)
		{
			this.Name = name;
		}

		// Token: 0x170003B2 RID: 946
		// (get) Token: 0x06001405 RID: 5125 RVA: 0x00036688 File Offset: 0x00034888
		// (set) Token: 0x06001406 RID: 5126 RVA: 0x00036690 File Offset: 0x00034890
		[NotNull]
		public string Name { get; private set; }
	}
}
