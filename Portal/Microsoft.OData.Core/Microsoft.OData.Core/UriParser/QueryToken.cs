using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001C7 RID: 455
	public abstract class QueryToken
	{
		// Token: 0x170004E2 RID: 1250
		// (get) Token: 0x060014F2 RID: 5362
		public abstract QueryTokenKind Kind { get; }

		// Token: 0x060014F3 RID: 5363
		public abstract T Accept<T>(ISyntacticTreeVisitor<T> visitor);

		// Token: 0x04000921 RID: 2337
		[SuppressMessage("Microsoft.Security", "CA2105:ArrayFieldsShouldNotBeReadOnly", Justification = "Modeled after Type.EmptyTypes")]
		public static readonly QueryToken[] EmptyTokens = new QueryToken[0];
	}
}
