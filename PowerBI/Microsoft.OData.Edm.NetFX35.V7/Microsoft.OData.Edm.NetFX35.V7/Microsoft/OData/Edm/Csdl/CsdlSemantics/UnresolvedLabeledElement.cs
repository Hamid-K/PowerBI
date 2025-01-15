using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000194 RID: 404
	internal class UnresolvedLabeledElement : BadLabeledExpression, IUnresolvedElement
	{
		// Token: 0x06000AF4 RID: 2804 RVA: 0x0001E679 File Offset: 0x0001C879
		public UnresolvedLabeledElement(string label, EdmLocation location)
			: base(label, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedLabeledElement, Strings.Bad_UnresolvedLabeledElement(label))
			})
		{
		}
	}
}
