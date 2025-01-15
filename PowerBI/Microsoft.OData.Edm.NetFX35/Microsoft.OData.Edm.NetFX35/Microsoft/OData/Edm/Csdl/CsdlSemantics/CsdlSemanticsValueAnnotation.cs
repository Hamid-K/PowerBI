using System;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Expressions;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020000CA RID: 202
	internal class CsdlSemanticsValueAnnotation : CsdlSemanticsVocabularyAnnotation, IEdmValueAnnotation, IEdmVocabularyAnnotation, IEdmElement
	{
		// Token: 0x0600036D RID: 877 RVA: 0x00007F79 File Offset: 0x00006179
		public CsdlSemanticsValueAnnotation(CsdlSemanticsSchema schema, IEdmVocabularyAnnotatable targetContext, CsdlSemanticsAnnotations annotationsContext, CsdlAnnotation annotation, string externalQualifier)
			: base(schema, targetContext, annotationsContext, annotation, externalQualifier)
		{
		}

		// Token: 0x170001C8 RID: 456
		// (get) Token: 0x0600036E RID: 878 RVA: 0x00007F93 File Offset: 0x00006193
		public IEdmExpression Value
		{
			get
			{
				return this.valueCache.GetValue(this, CsdlSemanticsValueAnnotation.ComputeValueFunc, null);
			}
		}

		// Token: 0x0600036F RID: 879 RVA: 0x00007FA7 File Offset: 0x000061A7
		protected override IEdmTerm ComputeTerm()
		{
			return base.Schema.FindValueTerm(this.Annotation.Term) ?? new UnresolvedValueTerm(base.Schema.UnresolvedName(this.Annotation.Term));
		}

		// Token: 0x06000370 RID: 880 RVA: 0x00007FDE File Offset: 0x000061DE
		private IEdmExpression ComputeValue()
		{
			return CsdlSemanticsModel.WrapExpression(this.Annotation.Expression, base.TargetBindingContext, base.Schema);
		}

		// Token: 0x04000173 RID: 371
		private readonly Cache<CsdlSemanticsValueAnnotation, IEdmExpression> valueCache = new Cache<CsdlSemanticsValueAnnotation, IEdmExpression>();

		// Token: 0x04000174 RID: 372
		private static readonly Func<CsdlSemanticsValueAnnotation, IEdmExpression> ComputeValueFunc = (CsdlSemanticsValueAnnotation me) => me.ComputeValue();
	}
}
