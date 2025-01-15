using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000171 RID: 369
	internal class CsdlSemanticsDirectValueAnnotationsManager : EdmDirectValueAnnotationsManager
	{
		// Token: 0x060009F9 RID: 2553 RVA: 0x0001BE20 File Offset: 0x0001A020
		protected override IEnumerable<IEdmDirectValueAnnotation> GetAttachedAnnotations(IEdmElement element)
		{
			CsdlSemanticsElement csdlSemanticsElement = element as CsdlSemanticsElement;
			if (csdlSemanticsElement != null)
			{
				return csdlSemanticsElement.DirectValueAnnotations;
			}
			return Enumerable.Empty<IEdmDirectValueAnnotation>();
		}
	}
}
