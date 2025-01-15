using System;
using System.Collections.Generic;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Engine1.Language.Ast
{
	// Token: 0x020018A7 RID: 6311
	internal sealed class EmbeddingReferenceDiscoveryVisitor : AstVisitor
	{
		// Token: 0x0600A092 RID: 41106 RVA: 0x00213DA8 File Offset: 0x00211FA8
		public IList<EmbeddingReference> DiscoverEmbeddingReferences(IDocument document)
		{
			IList<EmbeddingReference> list;
			try
			{
				this.document = document;
				this.embeddingReferences = new List<EmbeddingReference>();
				base.Visit(document);
				list = this.embeddingReferences;
			}
			finally
			{
				this.document = null;
				this.embeddingReferences = null;
			}
			return list;
		}

		// Token: 0x0600A093 RID: 41107 RVA: 0x00213DF8 File Offset: 0x00211FF8
		protected override IExpression VisitInvocation(IInvocationExpression invocation)
		{
			IIdentifierExpression identifierExpression = invocation.Function as IIdentifierExpression;
			if (identifierExpression != null && invocation.Arguments.Count == 1 && identifierExpression.Name.Name == "Embedded.Value")
			{
				IConstantExpression constantExpression = invocation.Arguments[0] as IConstantExpression;
				if (constantExpression != null && constantExpression.Value.IsText)
				{
					TextRange textRange = new TextRange(this.document.Tokens.GetRange(invocation.Range.Start).Start, this.document.Tokens.GetRange(invocation.Range.End).End);
					this.embeddingReferences.Add(new EmbeddingReference(constantExpression.Value.AsString, textRange));
				}
			}
			return base.VisitInvocation(invocation);
		}

		// Token: 0x040053E9 RID: 21481
		private List<EmbeddingReference> embeddingReferences;

		// Token: 0x040053EA RID: 21482
		private IDocument document;
	}
}
