using System;
using System.ComponentModel;
using System.Runtime.Serialization;
using Microsoft.PowerBI.Packaging.Storage;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x0200008F RID: 143
	[DisplayName("UnappliedChanges")]
	[Description("Holds information about changes that have not yet been applied to the data model. This includes M queries and associated metadata. This file is optional.")]
	[DataContract]
	public sealed class UnappliedChanges : QueriesStorage, IArtifactBase, IFromPBIProjectFile
	{
		// Token: 0x17000138 RID: 312
		// (get) Token: 0x06000422 RID: 1058 RVA: 0x0000A97A File Offset: 0x00008B7A
		// (set) Token: 0x06000423 RID: 1059 RVA: 0x0000A982 File Offset: 0x00008B82
		public string DollarVeryUniqueSchemaProperty { get; set; }

		// Token: 0x17000139 RID: 313
		// (get) Token: 0x06000424 RID: 1060 RVA: 0x0000A98B File Offset: 0x00008B8B
		// (set) Token: 0x06000425 RID: 1061 RVA: 0x0000A993 File Offset: 0x00008B93
		public string FileName { get; set; }
	}
}
