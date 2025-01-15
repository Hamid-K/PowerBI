using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Vocabularies;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x02000162 RID: 354
	internal class CsdlSemanticsDirectValueAnnotationsManager : EdmDirectValueAnnotationsManager
	{
		// Token: 0x0600093E RID: 2366 RVA: 0x00019D20 File Offset: 0x00017F20
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
