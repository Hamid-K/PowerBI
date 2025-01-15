using System;

namespace Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast
{
	// Token: 0x02000066 RID: 102
	internal class CsdlValueAnnotation : CsdlVocabularyAnnotationBase
	{
		// Token: 0x060001AD RID: 429 RVA: 0x00005281 File Offset: 0x00003481
		public CsdlValueAnnotation(string term, string qualifier, CsdlExpressionBase expression, CsdlLocation location)
			: base(term, qualifier, location)
		{
			this.expression = expression;
		}

		// Token: 0x170000E0 RID: 224
		// (get) Token: 0x060001AE RID: 430 RVA: 0x00005294 File Offset: 0x00003494
		public CsdlExpressionBase Expression
		{
			get
			{
				return this.expression;
			}
		}

		// Token: 0x040000B8 RID: 184
		private readonly CsdlExpressionBase expression;
	}
}
