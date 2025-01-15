using System;
using System.Diagnostics;

namespace Microsoft.ProgramSynthesis.Utils.JetBrains.Annotations
{
	// Token: 0x0200055D RID: 1373
	[AttributeUsage(AttributeTargets.Parameter)]
	[Conditional("JETBRAINS_ANNOTATIONS")]
	public sealed class PathReferenceAttribute : Attribute
	{
		// Token: 0x06001ED1 RID: 7889 RVA: 0x0000957E File Offset: 0x0000777E
		public PathReferenceAttribute()
		{
		}

		// Token: 0x06001ED2 RID: 7890 RVA: 0x00059994 File Offset: 0x00057B94
		public PathReferenceAttribute(string basePath)
		{
			this.BasePath = basePath;
		}

		// Token: 0x17000560 RID: 1376
		// (get) Token: 0x06001ED3 RID: 7891 RVA: 0x000599A3 File Offset: 0x00057BA3
		public string BasePath { get; }
	}
}
