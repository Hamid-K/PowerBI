using System;
using System.ComponentModel;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.Packaging.Project.Artifacts
{
	// Token: 0x0200008E RID: 142
	[DataContract]
	public sealed class ReportRemoteArtifact
	{
		// Token: 0x17000137 RID: 311
		// (get) Token: 0x0600041F RID: 1055 RVA: 0x0000A961 File Offset: 0x00008B61
		// (set) Token: 0x06000420 RID: 1056 RVA: 0x0000A969 File Offset: 0x00008B69
		[DisplayName("ReportId")]
		[Description("The ID of a published report created from this definition.")]
		[DataMember(Name = "reportId", EmitDefaultValue = true, IsRequired = true)]
		public string ReportId { get; set; }
	}
}
