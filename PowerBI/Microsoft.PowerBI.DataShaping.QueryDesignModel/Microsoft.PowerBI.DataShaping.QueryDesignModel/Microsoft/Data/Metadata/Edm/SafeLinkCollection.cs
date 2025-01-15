using System;
using System.Collections.Generic;

namespace Microsoft.Data.Metadata.Edm
{
	// Token: 0x020000B0 RID: 176
	internal class SafeLinkCollection<TParent, TChild> : ReadOnlyMetadataCollection<TChild> where TParent : class where TChild : MetadataItem
	{
		// Token: 0x06000B80 RID: 2944 RVA: 0x0001D70C File Offset: 0x0001B90C
		public SafeLinkCollection(TParent parent, Func<TChild, SafeLink<TParent>> getLink, MetadataCollection<TChild> children)
			: base((IList<TChild>)SafeLink<TParent>.BindChildren<TChild>(parent, getLink, children))
		{
		}
	}
}
