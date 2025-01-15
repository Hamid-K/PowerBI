using System;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x02000272 RID: 626
	internal abstract class PathToken : QueryToken
	{
		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x060015D7 RID: 5591
		// (set) Token: 0x060015D8 RID: 5592
		public abstract QueryToken NextToken { get; set; }

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x060015D9 RID: 5593
		public abstract string Identifier { get; }
	}
}
