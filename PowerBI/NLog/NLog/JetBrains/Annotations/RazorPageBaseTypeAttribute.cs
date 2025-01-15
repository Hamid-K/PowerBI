using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000203 RID: 515
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	internal sealed class RazorPageBaseTypeAttribute : Attribute
	{
		// Token: 0x06001499 RID: 5273 RVA: 0x00036C43 File Offset: 0x00034E43
		public RazorPageBaseTypeAttribute([NotNull] string baseType)
		{
			this.BaseType = baseType;
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00036C52 File Offset: 0x00034E52
		public RazorPageBaseTypeAttribute([NotNull] string baseType, string pageName)
		{
			this.BaseType = baseType;
			this.PageName = pageName;
		}

		// Token: 0x170003D7 RID: 983
		// (get) Token: 0x0600149B RID: 5275 RVA: 0x00036C68 File Offset: 0x00034E68
		// (set) Token: 0x0600149C RID: 5276 RVA: 0x00036C70 File Offset: 0x00034E70
		[NotNull]
		public string BaseType { get; private set; }

		// Token: 0x170003D8 RID: 984
		// (get) Token: 0x0600149D RID: 5277 RVA: 0x00036C79 File Offset: 0x00034E79
		// (set) Token: 0x0600149E RID: 5278 RVA: 0x00036C81 File Offset: 0x00034E81
		[CanBeNull]
		public string PageName { get; private set; }
	}
}
