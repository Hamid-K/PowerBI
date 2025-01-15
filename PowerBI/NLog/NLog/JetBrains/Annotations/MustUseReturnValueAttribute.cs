using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001D2 RID: 466
	[AttributeUsage(AttributeTargets.Method)]
	internal sealed class MustUseReturnValueAttribute : Attribute
	{
		// Token: 0x06001430 RID: 5168 RVA: 0x00036840 File Offset: 0x00034A40
		public MustUseReturnValueAttribute()
		{
		}

		// Token: 0x06001431 RID: 5169 RVA: 0x00036848 File Offset: 0x00034A48
		public MustUseReturnValueAttribute([NotNull] string justification)
		{
			this.Justification = justification;
		}

		// Token: 0x170003BD RID: 957
		// (get) Token: 0x06001432 RID: 5170 RVA: 0x00036857 File Offset: 0x00034A57
		// (set) Token: 0x06001433 RID: 5171 RVA: 0x0003685F File Offset: 0x00034A5F
		[CanBeNull]
		public string Justification { get; private set; }
	}
}
