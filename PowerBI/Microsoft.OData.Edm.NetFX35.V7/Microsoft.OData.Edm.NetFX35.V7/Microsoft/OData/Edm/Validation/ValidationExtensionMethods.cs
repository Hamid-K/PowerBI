using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x020000D6 RID: 214
	public static class ValidationExtensionMethods
	{
		// Token: 0x0600063D RID: 1597 RVA: 0x0000F97B File Offset: 0x0000DB7B
		public static bool IsBad(this IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return Enumerable.FirstOrDefault<EdmError>(element.Errors()) != null;
		}

		// Token: 0x0600063E RID: 1598 RVA: 0x0000F997 File Offset: 0x0000DB97
		public static IEnumerable<EdmError> Errors(this IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return InterfaceValidator.GetStructuralErrors(element);
		}

		// Token: 0x0600063F RID: 1599 RVA: 0x0000F9AB File Offset: 0x0000DBAB
		public static IEnumerable<EdmError> TypeErrors(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return Enumerable.Concat<EdmError>(InterfaceValidator.GetStructuralErrors(type), InterfaceValidator.GetStructuralErrors(type.Definition));
		}
	}
}
