using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x02000486 RID: 1158
	public static class EnumUtils
	{
		// Token: 0x06001A21 RID: 6689 RVA: 0x0004F06C File Offset: 0x0004D26C
		public static T Parse<T>(this string value, bool ignoreCase = false) where T : struct
		{
			T t;
			if (!Enum.TryParse<T>(value, ignoreCase, out t))
			{
				throw new FormatException();
			}
			return t;
		}

		// Token: 0x06001A22 RID: 6690 RVA: 0x0004F08C File Offset: 0x0004D28C
		public static object ParseEnum(this string value, IReadOnlyList<Type> enumTypes, bool ignoreCase = false)
		{
			foreach (Type type in enumTypes)
			{
				try
				{
					return Enum.Parse(type, value, ignoreCase);
				}
				catch (ArgumentException)
				{
				}
			}
			throw new ArgumentException("String \"" + value + "\" is not of any of the specified enum types: " + string.Join<Type>(", ", enumTypes), "value");
		}

		// Token: 0x06001A23 RID: 6691 RVA: 0x0004F110 File Offset: 0x0004D310
		public static T[] GetValues<T>() where T : struct
		{
			return Enum.GetValues(typeof(T)).Cast<T>().ToArray<T>();
		}
	}
}
