using System;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001A4 RID: 420
	internal class UnresolvedLabeledElement : BadLabeledExpression, IUnresolvedElement
	{
		// Token: 0x06000BC5 RID: 3013 RVA: 0x00020D97 File Offset: 0x0001EF97
		public UnresolvedLabeledElement(string label, EdmLocation location)
			: base(label, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedLabeledElement, Strings.Bad_UnresolvedLabeledElement(label))
			})
		{
		}
	}
}
