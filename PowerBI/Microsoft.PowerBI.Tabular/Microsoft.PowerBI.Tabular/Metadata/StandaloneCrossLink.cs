using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x0200020A RID: 522
	internal class StandaloneCrossLink<TOwner, TObject> : CrossLink<TOwner, TObject> where TOwner : MetadataObject where TObject : MetadataObject
	{
		// Token: 0x06001D93 RID: 7571 RVA: 0x000C8CD4 File Offset: 0x000C6ED4
		public StandaloneCrossLink(string propertyName)
			: base(default(TOwner), propertyName)
		{
		}
	}
}
