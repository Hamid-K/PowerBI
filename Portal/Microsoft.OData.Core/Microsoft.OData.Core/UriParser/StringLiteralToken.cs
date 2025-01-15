using System;
using System.Diagnostics;

namespace Microsoft.OData.UriParser
{
	// Token: 0x020001CD RID: 461
	[DebuggerDisplay("StringLiteralToken ({text})")]
	internal sealed class StringLiteralToken : QueryToken
	{
		// Token: 0x06001520 RID: 5408 RVA: 0x0003C503 File Offset: 0x0003A703
		internal StringLiteralToken(string text)
		{
			this.text = text;
		}

		// Token: 0x170004F5 RID: 1269
		// (get) Token: 0x06001521 RID: 5409 RVA: 0x000392D2 File Offset: 0x000374D2
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.StringLiteral;
			}
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001522 RID: 5410 RVA: 0x0003C512 File Offset: 0x0003A712
		internal string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x06001523 RID: 5411 RVA: 0x000032BD File Offset: 0x000014BD
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0400092E RID: 2350
		private readonly string text;
	}
}
