using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x02000011 RID: 17
	internal static class Argument
	{
		// Token: 0x06000031 RID: 49 RVA: 0x0000244D File Offset: 0x0000064D
		public static void AssertNotNull<T>(T value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000245E File Offset: 0x0000065E
		public static void AssertNotNull<T>(T? value, string name) where T : struct
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		// Token: 0x06000033 RID: 51 RVA: 0x00002470 File Offset: 0x00000670
		public static void AssertNotNullOrEmpty<T>(IEnumerable<T> value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
			ICollection<T> collection = value as ICollection<T>;
			if (collection != null && collection.Count == 0)
			{
				throw new ArgumentException("Value cannot be an empty collection.", name);
			}
			ICollection collection2 = value as ICollection;
			if (collection2 != null && collection2.Count == 0)
			{
				throw new ArgumentException("Value cannot be an empty collection.", name);
			}
			using (IEnumerator<T> enumerator = value.GetEnumerator())
			{
				if (!enumerator.MoveNext())
				{
					throw new ArgumentException("Value cannot be an empty collection.", name);
				}
			}
		}

		// Token: 0x06000034 RID: 52 RVA: 0x000024FC File Offset: 0x000006FC
		public static void AssertNotNullOrEmpty(string value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
			if (value.Length == 0)
			{
				throw new ArgumentException("Value cannot be an empty string.", name);
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x0000251C File Offset: 0x0000071C
		public static void AssertNotNullOrWhiteSpace(string value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException("Value cannot be empty or contain only white-space characters.", name);
			}
		}

		// Token: 0x06000036 RID: 54 RVA: 0x0000253C File Offset: 0x0000073C
		public static void AssertNotDefault<T>(ref T value, string name) where T : struct, IEquatable<T>
		{
			if (value.Equals(default(T)))
			{
				throw new ArgumentException("Value cannot be empty.", name);
			}
		}

		// Token: 0x06000037 RID: 55 RVA: 0x0000256C File Offset: 0x0000076C
		public static void AssertInRange<[Nullable(1)] T>(T value, T minimum, T maximum, string name) where T : IComparable<T>
		{
			if (minimum.CompareTo(value) > 0)
			{
				throw new ArgumentOutOfRangeException(name, "Value is less than the minimum allowed.");
			}
			if (maximum.CompareTo(value) < 0)
			{
				throw new ArgumentOutOfRangeException(name, "Value is greater than the maximum allowed.");
			}
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000025A8 File Offset: 0x000007A8
		public static void AssertEnumDefined(Type enumType, object value, string name)
		{
			if (!Enum.IsDefined(enumType, value))
			{
				throw new ArgumentException("Value not defined for " + enumType.FullName + ".", name);
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000025CF File Offset: 0x000007CF
		public static T CheckNotNull<T>(T value, string name) where T : class
		{
			Argument.AssertNotNull<T>(value, name);
			return value;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000025D9 File Offset: 0x000007D9
		public static string CheckNotNullOrEmpty(string value, string name)
		{
			Argument.AssertNotNullOrEmpty(value, name);
			return value;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x000025E3 File Offset: 0x000007E3
		[NullableContext(1)]
		public static void AssertNull<[Nullable(2)] T>(T value, string name, [Nullable(2)] string message = null)
		{
			if (value != null)
			{
				throw new ArgumentException(message ?? "Value must be null.", name);
			}
		}
	}
}
