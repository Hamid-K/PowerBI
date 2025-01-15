using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Extensions
{
	// Token: 0x020017DB RID: 6107
	public static class TypeExtension
	{
		// Token: 0x0600C964 RID: 51556 RVA: 0x002B2340 File Offset: 0x002B0540
		public static bool IsGenericInterface(this Type type, Type interfaceType)
		{
			return type == interfaceType || type.InheritsGeneric(interfaceType) != null;
		}

		// Token: 0x0600C965 RID: 51557 RVA: 0x002B2357 File Offset: 0x002B0557
		public static IEnumerable<T> Values<T>(this Type type)
		{
			return Enum.GetValues(type).OfType<T>();
		}

		// Token: 0x0600C966 RID: 51558 RVA: 0x002B2364 File Offset: 0x002B0564
		public static IEnumerable<string> Values(this Type type)
		{
			return Enum.GetNames(type);
		}
	}
}
