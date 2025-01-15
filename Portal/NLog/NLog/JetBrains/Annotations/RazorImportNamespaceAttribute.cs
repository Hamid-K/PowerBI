using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000200 RID: 512
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
	internal sealed class RazorImportNamespaceAttribute : Attribute
	{
		// Token: 0x0600148E RID: 5262 RVA: 0x00036BCB File Offset: 0x00034DCB
		public RazorImportNamespaceAttribute([NotNull] string name)
		{
			this.Name = name;
		}

		// Token: 0x170003D3 RID: 979
		// (get) Token: 0x0600148F RID: 5263 RVA: 0x00036BDA File Offset: 0x00034DDA
		// (set) Token: 0x06001490 RID: 5264 RVA: 0x00036BE2 File Offset: 0x00034DE2
		[NotNull]
		public string Name { get; private set; }
	}
}
