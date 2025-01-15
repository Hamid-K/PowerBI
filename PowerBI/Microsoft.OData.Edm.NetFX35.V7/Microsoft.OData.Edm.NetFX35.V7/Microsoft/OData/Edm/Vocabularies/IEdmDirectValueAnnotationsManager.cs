using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000E7 RID: 231
	public interface IEdmDirectValueAnnotationsManager
	{
		// Token: 0x060006A7 RID: 1703
		IEnumerable<IEdmDirectValueAnnotation> GetDirectValueAnnotations(IEdmElement element);

		// Token: 0x060006A8 RID: 1704
		void SetAnnotationValue(IEdmElement element, string namespaceName, string localName, object value);

		// Token: 0x060006A9 RID: 1705
		void SetAnnotationValues(IEnumerable<IEdmDirectValueAnnotationBinding> annotations);

		// Token: 0x060006AA RID: 1706
		object GetAnnotationValue(IEdmElement element, string namespaceName, string localName);

		// Token: 0x060006AB RID: 1707
		object[] GetAnnotationValues(IEnumerable<IEdmDirectValueAnnotationBinding> annotations);
	}
}
