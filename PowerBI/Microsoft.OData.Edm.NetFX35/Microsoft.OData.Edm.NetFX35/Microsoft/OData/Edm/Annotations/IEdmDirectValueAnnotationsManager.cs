using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Annotations
{
	// Token: 0x02000079 RID: 121
	public interface IEdmDirectValueAnnotationsManager
	{
		// Token: 0x060001DE RID: 478
		IEnumerable<IEdmDirectValueAnnotation> GetDirectValueAnnotations(IEdmElement element);

		// Token: 0x060001DF RID: 479
		void SetAnnotationValue(IEdmElement element, string namespaceName, string localName, object value);

		// Token: 0x060001E0 RID: 480
		void SetAnnotationValues(IEnumerable<IEdmDirectValueAnnotationBinding> annotations);

		// Token: 0x060001E1 RID: 481
		object GetAnnotationValue(IEdmElement element, string namespaceName, string localName);

		// Token: 0x060001E2 RID: 482
		object[] GetAnnotationValues(IEnumerable<IEdmDirectValueAnnotationBinding> annotations);
	}
}
