using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Data.Edm.Validation.Internal;

namespace Microsoft.Data.Edm.Validation
{
	// Token: 0x02000239 RID: 569
	public static class ValidationExtensionMethods
	{
		// Token: 0x06000C9A RID: 3226 RVA: 0x000253F1 File Offset: 0x000235F1
		public static bool IsBad(this IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return Enumerable.FirstOrDefault<EdmError>(element.Errors()) != null;
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x00025410 File Offset: 0x00023610
		public static IEnumerable<EdmError> Errors(this IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return InterfaceValidator.GetStructuralErrors(element);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x00025424 File Offset: 0x00023624
		public static IEnumerable<EdmError> TypeErrors(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return Enumerable.Concat<EdmError>(InterfaceValidator.GetStructuralErrors(type), InterfaceValidator.GetStructuralErrors(type.Definition));
		}
	}
}
