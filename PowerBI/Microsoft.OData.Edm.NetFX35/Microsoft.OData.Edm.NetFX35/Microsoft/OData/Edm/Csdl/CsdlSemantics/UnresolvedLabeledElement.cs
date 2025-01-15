using System;
using Microsoft.OData.Edm.Library;
using Microsoft.OData.Edm.Validation;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x020001BC RID: 444
	internal class UnresolvedLabeledElement : BadLabeledExpression, IUnresolvedElement
	{
		// Token: 0x0600095B RID: 2395 RVA: 0x00019410 File Offset: 0x00017610
		public UnresolvedLabeledElement(string label, EdmLocation location)
			: base(label, new EdmError[]
			{
				new EdmError(location, EdmErrorCode.BadUnresolvedLabeledElement, Strings.Bad_UnresolvedLabeledElement(label))
			})
		{
		}
	}
}
