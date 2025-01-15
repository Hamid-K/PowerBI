using System;
using System.Diagnostics.CodeAnalysis;

namespace Microsoft.OData.Client.ALinq.UriParser
{
	// Token: 0x02000131 RID: 305
	public abstract class QueryToken
	{
		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000C88 RID: 3208
		public abstract QueryTokenKind Kind { get; }

		// Token: 0x06000C89 RID: 3209
		public abstract T Accept<T>(ISyntacticTreeVisitor<T> visitor);

		// Token: 0x04000680 RID: 1664
		[SuppressMessage("Microsoft.Security", "CA2105:ArrayFieldsShouldNotBeReadOnly", Justification = "Modeled after Type.EmptyTypes")]
		public static readonly QueryToken[] EmptyTokens = new QueryToken[0];
	}
}
