using System;
using System.Collections.Generic;

namespace Microsoft.OData.Edm.Vocabularies
{
	// Token: 0x020000DF RID: 223
	public interface IEdmDirectValueAnnotationsManager
	{
		// Token: 0x060006C7 RID: 1735
		IEnumerable<IEdmDirectValueAnnotation> GetDirectValueAnnotations(IEdmElement element);

		// Token: 0x060006C8 RID: 1736
		void SetAnnotationValue(IEdmElement element, string namespaceName, string localName, object value);

		// Token: 0x060006C9 RID: 1737
		void SetAnnotationValues(IEnumerable<IEdmDirectValueAnnotationBinding> annotations);

		// Token: 0x060006CA RID: 1738
		object GetAnnotationValue(IEdmElement element, string namespaceName, string localName);

		// Token: 0x060006CB RID: 1739
		object[] GetAnnotationValues(IEnumerable<IEdmDirectValueAnnotationBinding> annotations);
	}
}
