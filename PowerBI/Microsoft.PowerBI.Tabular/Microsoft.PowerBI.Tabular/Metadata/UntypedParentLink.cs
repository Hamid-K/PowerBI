using System;

namespace Microsoft.AnalysisServices.Tabular.Metadata
{
	// Token: 0x0200020E RID: 526
	internal class UntypedParentLink<TOwner> : ParentLink<TOwner, MetadataObject> where TOwner : MetadataObject
	{
		// Token: 0x06001DB5 RID: 7605 RVA: 0x000C96AC File Offset: 0x000C78AC
		public UntypedParentLink(TOwner owner, string propertyName)
			: base(owner, propertyName)
		{
		}
	}
}
