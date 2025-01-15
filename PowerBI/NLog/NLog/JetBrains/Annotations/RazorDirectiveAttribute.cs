using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000202 RID: 514
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	internal sealed class RazorDirectiveAttribute : Attribute
	{
		// Token: 0x06001496 RID: 5270 RVA: 0x00036C23 File Offset: 0x00034E23
		public RazorDirectiveAttribute([NotNull] string directive)
		{
			this.Directive = directive;
		}

		// Token: 0x170003D6 RID: 982
		// (get) Token: 0x06001497 RID: 5271 RVA: 0x00036C32 File Offset: 0x00034E32
		// (set) Token: 0x06001498 RID: 5272 RVA: 0x00036C3A File Offset: 0x00034E3A
		[NotNull]
		public string Directive { get; private set; }
	}
}
