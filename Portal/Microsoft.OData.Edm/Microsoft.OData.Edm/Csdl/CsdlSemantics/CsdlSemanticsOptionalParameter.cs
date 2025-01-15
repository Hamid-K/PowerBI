using System;
using Microsoft.OData.Edm.Csdl.Parsing.Ast;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A0 RID: 416
	internal class CsdlSemanticsOptionalParameter : CsdlSemanticsOperationParameter, IEdmOptionalParameter, IEdmOperationParameter, IEdmNamedElement, IEdmElement, IEdmVocabularyAnnotatable
	{
		// Token: 0x06000B90 RID: 2960 RVA: 0x0001F67D File Offset: 0x0001D87D
		public CsdlSemanticsOptionalParameter(CsdlSemanticsOperation declaringOperation, CsdlOperationParameter parameter, string defaultValue)
			: base(declaringOperation, parameter)
		{
			this.DefaultValueString = defaultValue;
		}

		// Token: 0x170003C7 RID: 967
		// (get) Token: 0x06000B91 RID: 2961 RVA: 0x0001F68E File Offset: 0x0001D88E
		// (set) Token: 0x06000B92 RID: 2962 RVA: 0x0001F696 File Offset: 0x0001D896
		public string DefaultValueString { get; private set; }
	}
}
