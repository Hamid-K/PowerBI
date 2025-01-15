using System;
using Newtonsoft.Json.Linq;

namespace Microsoft.Lucia.Core.DomainModel.Serialization
{
	// Token: 0x02000192 RID: 402
	internal interface ILsdlDocumentUpgradeTransform
	{
		// Token: 0x1700026F RID: 623
		// (get) Token: 0x0600082C RID: 2092
		Version SourceVersion { get; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x0600082D RID: 2093
		Version TargetVersion { get; }

		// Token: 0x0600082E RID: 2094
		void Upgrade(JObject schema);
	}
}
