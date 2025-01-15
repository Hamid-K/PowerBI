using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.OData.Edm.Validation.Internal;

namespace Microsoft.OData.Edm.Validation
{
	// Token: 0x02000271 RID: 625
	public static class ValidationExtensionMethods
	{
		// Token: 0x06000DB2 RID: 3506 RVA: 0x000272FA File Offset: 0x000254FA
		public static bool IsBad(this IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return Enumerable.FirstOrDefault<EdmError>(element.Errors()) != null;
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00027319 File Offset: 0x00025519
		public static IEnumerable<EdmError> Errors(this IEdmElement element)
		{
			EdmUtil.CheckArgumentNull<IEdmElement>(element, "element");
			return InterfaceValidator.GetStructuralErrors(element);
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x0002732D File Offset: 0x0002552D
		public static IEnumerable<EdmError> TypeErrors(this IEdmTypeReference type)
		{
			EdmUtil.CheckArgumentNull<IEdmTypeReference>(type, "type");
			return Enumerable.Concat<EdmError>(InterfaceValidator.GetStructuralErrors(type), InterfaceValidator.GetStructuralErrors(type.Definition));
		}
	}
}
