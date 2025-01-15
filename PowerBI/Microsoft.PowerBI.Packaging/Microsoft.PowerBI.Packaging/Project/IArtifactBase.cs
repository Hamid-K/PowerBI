using System;
using Newtonsoft.Json;

namespace Microsoft.PowerBI.Packaging.Project
{
	// Token: 0x0200005B RID: 91
	public interface IArtifactBase
	{
		// Token: 0x170000CA RID: 202
		// (get) Token: 0x060002AD RID: 685
		// (set) Token: 0x060002AE RID: 686
		[JsonProperty("$schema", Required = Required.Default, DefaultValueHandling = DefaultValueHandling.Ignore, Order = -2)]
		string DollarVeryUniqueSchemaProperty { get; set; }
	}
}
