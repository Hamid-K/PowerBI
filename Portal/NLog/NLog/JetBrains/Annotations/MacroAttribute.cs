using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001D6 RID: 470
	[AttributeUsage(AttributeTargets.Method | AttributeTargets.Parameter, AllowMultiple = true)]
	internal sealed class MacroAttribute : Attribute
	{
		// Token: 0x170003BF RID: 959
		// (get) Token: 0x0600143A RID: 5178 RVA: 0x000368A0 File Offset: 0x00034AA0
		// (set) Token: 0x0600143B RID: 5179 RVA: 0x000368A8 File Offset: 0x00034AA8
		[CanBeNull]
		public string Expression { get; set; }

		// Token: 0x170003C0 RID: 960
		// (get) Token: 0x0600143C RID: 5180 RVA: 0x000368B1 File Offset: 0x00034AB1
		// (set) Token: 0x0600143D RID: 5181 RVA: 0x000368B9 File Offset: 0x00034AB9
		public int Editable { get; set; }

		// Token: 0x170003C1 RID: 961
		// (get) Token: 0x0600143E RID: 5182 RVA: 0x000368C2 File Offset: 0x00034AC2
		// (set) Token: 0x0600143F RID: 5183 RVA: 0x000368CA File Offset: 0x00034ACA
		[CanBeNull]
		public string Target { get; set; }
	}
}
