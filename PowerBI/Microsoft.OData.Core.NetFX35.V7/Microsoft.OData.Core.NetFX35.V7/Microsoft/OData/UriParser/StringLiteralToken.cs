using System;
using System.Diagnostics;

namespace Microsoft.OData.UriParser
{
	// Token: 0x02000180 RID: 384
	[DebuggerDisplay("StringLiteralToken ({text})")]
	internal sealed class StringLiteralToken : QueryToken
	{
		// Token: 0x06000FD4 RID: 4052 RVA: 0x0002C28E File Offset: 0x0002A48E
		internal StringLiteralToken(string text)
		{
			this.text = text;
		}

		// Token: 0x17000407 RID: 1031
		// (get) Token: 0x06000FD5 RID: 4053 RVA: 0x00029609 File Offset: 0x00027809
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.StringLiteral;
			}
		}

		// Token: 0x17000408 RID: 1032
		// (get) Token: 0x06000FD6 RID: 4054 RVA: 0x0002C29D File Offset: 0x0002A49D
		internal string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x0000FA90 File Offset: 0x0000DC90
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x040007FC RID: 2044
		private readonly string text;
	}
}
