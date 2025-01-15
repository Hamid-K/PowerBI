using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001C8 RID: 456
	[AttributeUsage(AttributeTargets.All)]
	internal sealed class LocalizationRequiredAttribute : Attribute
	{
		// Token: 0x06001412 RID: 5138 RVA: 0x0003670B File Offset: 0x0003490B
		public LocalizationRequiredAttribute()
			: this(true)
		{
		}

		// Token: 0x06001413 RID: 5139 RVA: 0x00036714 File Offset: 0x00034914
		public LocalizationRequiredAttribute(bool required)
		{
			this.Required = required;
		}

		// Token: 0x170003B6 RID: 950
		// (get) Token: 0x06001414 RID: 5140 RVA: 0x00036723 File Offset: 0x00034923
		// (set) Token: 0x06001415 RID: 5141 RVA: 0x0003672B File Offset: 0x0003492B
		public bool Required { get; private set; }
	}
}
