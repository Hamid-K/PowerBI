using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000190 RID: 400
	internal class CsdlSemanticsOptionalParameter : CsdlSemanticsOperationParameter, IEdmOptionalParameter, IEdmOperationParameter, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000AC0 RID: 2752 RVA: 0x0001CED1 File Offset: 0x0001B0D1
		public CsdlSemanticsOptionalParameter(CsdlSemanticsOperation declaringOperation, CsdlOperationParameter parameter, string defaultValue)
			: base(declaringOperation, parameter)
		{
			this.DefaultValueString = defaultValue;
		}

		// Token: 0x1700036E RID: 878
		// (get) Token: 0x06000AC1 RID: 2753 RVA: 0x0001CEE2 File Offset: 0x0001B0E2
		// (set) Token: 0x06000AC2 RID: 2754 RVA: 0x0001CEEA File Offset: 0x0001B0EA
		public string DefaultValueString { get; private set; }
	}
}
