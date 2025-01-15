using System;
using System.Diagnostics.CodeAnalysis;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x0200026C RID: 620
	internal abstract class QueryToken : ODataAnnotatable
	{
		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x060015BD RID: 5565
		public abstract QueryTokenKind Kind { get; }

		// Token: 0x060015BE RID: 5566
		public abstract T Accept<T>(ISyntacticTreeVisitor<T> visitor);

		// Token: 0x04000908 RID: 2312
		[SuppressMessage("Microsoft.Security", "CA2105:ArrayFieldsShouldNotBeReadOnly", Justification = "Modeled after Type.EmptyTypes")]
		public static readonly QueryToken[] EmptyTokens = new QueryToken[0];
	}
}
