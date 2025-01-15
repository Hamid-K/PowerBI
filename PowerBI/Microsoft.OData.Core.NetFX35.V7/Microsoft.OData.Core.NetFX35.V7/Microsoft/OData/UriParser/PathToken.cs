using System;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200017A RID: 378
	public abstract class PathToken : QueryToken
	{
		// Token: 0x170003FD RID: 1021
		// (get) Token: 0x06000FBE RID: 4030
		// (set) Token: 0x06000FBF RID: 4031
		public abstract QueryToken NextToken { get; set; }

		// Token: 0x170003FE RID: 1022
		// (get) Token: 0x06000FC0 RID: 4032
		public abstract string Identifier { get; }
	}
}
