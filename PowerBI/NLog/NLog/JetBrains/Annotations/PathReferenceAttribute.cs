using System;

namespace JetBrains.Annotations
{
	// Token: 0x020001D4 RID: 468
	[AttributeUsage(AttributeTargets.Parameter)]
	internal sealed class PathReferenceAttribute : Attribute
	{
		// Token: 0x06001435 RID: 5173 RVA: 0x00036870 File Offset: 0x00034A70
		public PathReferenceAttribute()
		{
		}

		// Token: 0x06001436 RID: 5174 RVA: 0x00036878 File Offset: 0x00034A78
		public PathReferenceAttribute([NotNull] [PathReference] string basePath)
		{
			this.BasePath = basePath;
		}

		// Token: 0x170003BE RID: 958
		// (get) Token: 0x06001437 RID: 5175 RVA: 0x00036887 File Offset: 0x00034A87
		// (set) Token: 0x06001438 RID: 5176 RVA: 0x0003688F File Offset: 0x00034A8F
		[CanBeNull]
		public string BasePath { get; private set; }
	}
}
