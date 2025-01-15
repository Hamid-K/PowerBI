using System;
using Newtonsoft.Json.Linq;

namespace Microsoft.Lucia.Core.TermIndex
{
	// Token: 0x02000162 RID: 354
	internal interface IDataIndexMetadataUpgradeTransform
	{
		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000701 RID: 1793
		Version SourceVersion { get; }

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000702 RID: 1794
		Version TargetVersion { get; }

		// Token: 0x06000703 RID: 1795
		void Upgrade(JObject metadata);
	}
}
