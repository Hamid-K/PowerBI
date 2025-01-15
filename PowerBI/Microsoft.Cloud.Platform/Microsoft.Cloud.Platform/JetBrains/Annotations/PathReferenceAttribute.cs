using System;

namespace JetBrains.Annotations
{
	// Token: 0x02000568 RID: 1384
	[AttributeUsage(AttributeTargets.Parameter)]
	public class PathReferenceAttribute : Attribute
	{
		// Token: 0x06002A42 RID: 10818 RVA: 0x00012024 File Offset: 0x00010224
		public PathReferenceAttribute()
		{
		}

		// Token: 0x06002A43 RID: 10819 RVA: 0x000982E2 File Offset: 0x000964E2
		[UsedImplicitly]
		public PathReferenceAttribute([PathReference] string basePath)
		{
			this.BasePath = basePath;
		}

		// Token: 0x170006CD RID: 1741
		// (get) Token: 0x06002A44 RID: 10820 RVA: 0x000982F1 File Offset: 0x000964F1
		// (set) Token: 0x06002A45 RID: 10821 RVA: 0x000982F9 File Offset: 0x000964F9
		[UsedImplicitly]
		public string BasePath { get; private set; }
	}
}
