using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.UriParser
{
	// Token: 0x0200017B RID: 379
	public abstract class QueryToken
	{
		// Token: 0x170003FF RID: 1023
		// (get) Token: 0x06000FC2 RID: 4034
		public abstract QueryTokenKind Kind { get; }

		// Token: 0x06000FC3 RID: 4035
		public abstract T Accept<T>(ISyntacticTreeVisitor<T> visitor);

		// Token: 0x040007DE RID: 2014
		[SuppressMessage("Microsoft.Security", "CA2105:ArrayFieldsShouldNotBeReadOnly", Justification = "Modeled after Type.EmptyTypes")]
		public static readonly QueryToken[] EmptyTokens = new QueryToken[0];
	}
}
