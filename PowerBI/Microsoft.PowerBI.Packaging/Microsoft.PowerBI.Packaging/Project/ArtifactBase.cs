using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x02000055 RID: 85
	[DataContract]
	public abstract class ArtifactBase : IArtifactBase
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000292 RID: 658 RVA: 0x00007DE8 File Offset: 0x00005FE8
		// (set) Token: 0x06000293 RID: 659 RVA: 0x00007DF0 File Offset: 0x00005FF0
		public string DollarVeryUniqueSchemaProperty { get; set; }
	}
}
