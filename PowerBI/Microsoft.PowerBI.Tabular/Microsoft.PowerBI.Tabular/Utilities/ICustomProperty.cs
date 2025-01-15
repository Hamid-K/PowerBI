using System;
using Microsoft.AnalysisServices.Tabular.Json.Linq;

namespace Microsoft.AnalysisServices.Tabular.Utilities
{
	// Token: 0x02000196 RID: 406
	internal interface ICustomProperty<TOwner, TMetadataValue> where TOwner : MetadataObject
	{
		// Token: 0x17000612 RID: 1554
		// (get) Token: 0x0600188E RID: 6286
		// (set) Token: 0x0600188F RID: 6287
		TOwner Owner { get; set; }

		// Token: 0x06001890 RID: 6288
		void Parse(TMetadataValue value);

		// Token: 0x06001891 RID: 6289
		TMetadataValue Convert();

		// Token: 0x06001892 RID: 6290
		bool TryParseJson(JToken json, out TMetadataValue value);
	}
}
