using System;
using System.Diagnostics;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x02000282 RID: 642
	[DebuggerDisplay("StringLiteralToken ({text})")]
	internal sealed class StringLiteralToken : QueryToken
	{
		// Token: 0x06001640 RID: 5696 RVA: 0x0004C627 File Offset: 0x0004A827
		internal StringLiteralToken(string text)
		{
			this.text = text;
		}

		// Token: 0x17000513 RID: 1299
		// (get) Token: 0x06001641 RID: 5697 RVA: 0x0004C636 File Offset: 0x0004A836
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.StringLiteral;
			}
		}

		// Token: 0x17000514 RID: 1300
		// (get) Token: 0x06001642 RID: 5698 RVA: 0x0004C63A File Offset: 0x0004A83A
		internal string Text
		{
			get
			{
				return this.text;
			}
		}

		// Token: 0x06001643 RID: 5699 RVA: 0x0004C642 File Offset: 0x0004A842
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}

		// Token: 0x04000938 RID: 2360
		private readonly string text;
	}
}
