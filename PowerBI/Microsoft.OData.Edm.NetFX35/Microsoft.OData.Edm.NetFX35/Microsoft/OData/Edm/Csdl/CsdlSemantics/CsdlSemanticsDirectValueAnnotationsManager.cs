using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Annotations;
using Microsoft.OData.Edm.Library.Annotations;

namespace Microsoft.OData.Edm.Csdl.CsdlSemantics
{
	// Token: 0x0200007B RID: 123
	internal class CsdlSemanticsDirectValueAnnotationsManager : EdmDirectValueAnnotationsManager
	{
		// Token: 0x060001F3 RID: 499 RVA: 0x00005784 File Offset: 0x00003984
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
