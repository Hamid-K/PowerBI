using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x0200008B RID: 139
	[DataContract]
	public sealed class ReportDatasetReferenceByPath
	{
		// Token: 0x1700012B RID: 299
		// (get) Token: 0x06000405 RID: 1029 RVA: 0x0000A87C File Offset: 0x00008A7C
		// (set) Token: 0x06000406 RID: 1030 RVA: 0x0000A884 File Offset: 0x00008A84
		[DisplayName("Path")]
		[Description("A relative path from this file to the target semantic model artifact folder. Uses ‘/’ as a directory separator.")]
		[DataMember(Name = "path", EmitDefaultValue = true, IsRequired = true)]
		public string Path { get; set; }
	}
}
