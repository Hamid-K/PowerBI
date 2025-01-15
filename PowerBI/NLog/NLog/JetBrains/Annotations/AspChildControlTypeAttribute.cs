using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001FA RID: 506
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	internal sealed class AspChildControlTypeAttribute : Attribute
	{
		// Token: 0x06001480 RID: 5248 RVA: 0x00036B3B File Offset: 0x00034D3B
		public AspChildControlTypeAttribute([NotNull] string tagName, [NotNull] Type controlType)
		{
			this.TagName = tagName;
			this.ControlType = controlType;
		}

		// Token: 0x170003CF RID: 975
		// (get) Token: 0x06001481 RID: 5249 RVA: 0x00036B51 File Offset: 0x00034D51
		// (set) Token: 0x06001482 RID: 5250 RVA: 0x00036B59 File Offset: 0x00034D59
		[NotNull]
		public string TagName { get; private set; }

		// Token: 0x170003D0 RID: 976
		// (get) Token: 0x06001483 RID: 5251 RVA: 0x00036B62 File Offset: 0x00034D62
		// (set) Token: 0x06001484 RID: 5252 RVA: 0x00036B6A File Offset: 0x00034D6A
		[NotNull]
		public Type ControlType { get; private set; }
	}
}
