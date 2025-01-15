using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x0200020D RID: 525
	internal class UntypedCrossLink<TOwner> : CrossLink<TOwner, MetadataObject> where TOwner : MetadataObject
	{
		// Token: 0x06001DB4 RID: 7604 RVA: 0x000C96A2 File Offset: 0x000C78A2
		public UntypedCrossLink(TOwner owner, string propertyName)
			: base(owner, propertyName)
		{
		}
	}
}
