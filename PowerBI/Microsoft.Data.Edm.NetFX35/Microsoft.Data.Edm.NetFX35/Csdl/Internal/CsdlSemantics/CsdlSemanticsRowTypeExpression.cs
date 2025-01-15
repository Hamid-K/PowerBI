using System;
using Microsoft.Data.Edm.Csdl.Internal.Parsing.Ast;

namespace Microsoft.Data.Edm.Csdl.Internal.CsdlSemantics
{
	// Token: 0x020000BE RID: 190
	internal class CsdlSemanticsRowTypeExpression : CsdlSemanticsTypeExpression, IEdmRowTypeReference, IEdmStructuredTypeReference, IEdmTypeReference, IEdmElement
	{
		// Token: 0x0600035D RID: 861 RVA: 0x00008BBA File Offset: 0x00006DBA
		public CsdlSemanticsRowTypeExpression(CsdlExpressionTypeReference expressionUsage, CsdlSemanticsTypeDefinition type)
			: base(expressionUsage, type)
		{
		}
	}
}
