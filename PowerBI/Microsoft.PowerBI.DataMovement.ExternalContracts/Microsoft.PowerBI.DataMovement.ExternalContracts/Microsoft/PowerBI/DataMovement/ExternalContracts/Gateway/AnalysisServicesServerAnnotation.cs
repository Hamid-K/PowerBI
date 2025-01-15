using System;
using System.Runtime.Serialization;

namespace Microsoft.PowerBI.DataMovement.ExternalContracts.Gateway
{
	// Token: 0x02000014 RID: 20
	[DataContract]
	public sealed class AnalysisServicesServerAnnotation : ServerAnnotation<AnalysisServicesServerAnnotation>
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000054 RID: 84 RVA: 0x000027FF File Offset: 0x000009FF
		// (set) Token: 0x06000055 RID: 85 RVA: 0x00002807 File Offset: 0x00000A07
		[DataMember(Name = "hasCubes", IsRequired = true, EmitDefaultValue = true)]
		public bool HasCubes { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000056 RID: 86 RVA: 0x00002810 File Offset: 0x00000A10
		// (set) Token: 0x06000057 RID: 87 RVA: 0x00002818 File Offset: 0x00000A18
		[DataMember(Name = "isMultidimensional", IsRequired = true, EmitDefaultValue = true)]
		public bool IsMultidimensional { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000058 RID: 88 RVA: 0x00002821 File Offset: 0x00000A21
		// (set) Token: 0x06000059 RID: 89 RVA: 0x00002829 File Offset: 0x00000A29
		[DataMember(Name = "serverVersion", IsRequired = true, EmitDefaultValue = true)]
		public string ServerVersion { get; set; }
	}
}
