using System;
using Microsoft.OData.Core.UriParser.TreeNodeKinds;
using Microsoft.OData.Core.UriParser.Visitors;
using Microsoft.OData.Edm;

namespace Microsoft.OData.Core.UriParser.Syntactic
{
	// Token: 0x02000278 RID: 632
	internal sealed class FunctionParameterAliasToken : QueryToken
	{
		// Token: 0x06001602 RID: 5634 RVA: 0x0004C378 File Offset: 0x0004A578
		public FunctionParameterAliasToken(string alias)
		{
			ExceptionUtils.CheckArgumentStringNotNullOrEmpty(alias, "alias");
			this.Alias = alias;
		}

		// Token: 0x170004F6 RID: 1270
		// (get) Token: 0x06001603 RID: 5635 RVA: 0x0004C392 File Offset: 0x0004A592
		// (set) Token: 0x06001604 RID: 5636 RVA: 0x0004C39A File Offset: 0x0004A59A
		public string Alias { get; private set; }

		// Token: 0x170004F7 RID: 1271
		// (get) Token: 0x06001605 RID: 5637 RVA: 0x0004C3A3 File Offset: 0x0004A5A3
		public override QueryTokenKind Kind
		{
			get
			{
				return QueryTokenKind.FunctionParameterAlias;
			}
		}

		// Token: 0x170004F8 RID: 1272
		// (get) Token: 0x06001606 RID: 5638 RVA: 0x0004C3A7 File Offset: 0x0004A5A7
		// (set) Token: 0x06001607 RID: 5639 RVA: 0x0004C3AF File Offset: 0x0004A5AF
		internal IEdmTypeReference ExpectedParameterType { get; set; }

		// Token: 0x06001608 RID: 5640 RVA: 0x0004C3B8 File Offset: 0x0004A5B8
		public override T Accept<T>(ISyntacticTreeVisitor<T> visitor)
		{
			throw new NotImplementedException();
		}
	}
}
