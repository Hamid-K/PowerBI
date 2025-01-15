using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x02000145 RID: 325
	public static class ValidationExtensionMethods
	{
		// Token: 0x0600083B RID: 2107 RVA: 0x00014D17 File Offset: 0x00012F17
		public static bool IsBad(this IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return element.Errors().FirstOrDefault<EdmError>() != null;
		}

		// Token: 0x0600083C RID: 2108 RVA: 0x00014D33 File Offset: 0x00012F33
		public static IEnumerable<EdmError> Errors(this IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return InterfaceValidator.GetStructuralErrors(element);
		}

		// Token: 0x0600083D RID: 2109 RVA: 0x00014D47 File Offset: 0x00012F47
		public static IEnumerable<EdmError> TypeErrors(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return InterfaceValidator.GetStructuralErrors(type).Concat(InterfaceValidator.GetStructuralErrors(type.Definition));
		}
	}
}
