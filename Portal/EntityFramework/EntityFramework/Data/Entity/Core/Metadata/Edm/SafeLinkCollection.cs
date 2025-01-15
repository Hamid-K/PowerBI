using System;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004F8 RID: 1272
	internal class SafeLinkCollection<TParent, TChild> : ReadOnlyMetadataCollection<TChild> where TParent : class where TChild : MetadataItem
	{
		// Token: 0x06003EF1 RID: 16113 RVA: 0x000D1480 File Offset: 0x000CF680
		public SafeLinkCollection(TParent parent, Func<TChild, SafeLink<TParent>> getLink, MetadataCollection<TChild> children)
			: base((MetadataCollection<TChild>)SafeLink<TParent>.BindChildren<TChild>(parent, getLink, children))
		{
		}
	}
}
