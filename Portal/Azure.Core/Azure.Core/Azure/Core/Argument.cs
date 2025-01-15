using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Azure.Core
{
	// Token: 0x0200006A RID: 106
	[NullableContext(1)]
	[Nullable(0)]
	internal static class Argument
	{
		// Token: 0x0600038E RID: 910 RVA: 0x0000A828 File Offset: 0x00008A28
		public static void AssertNotNull<[Nullable(2)] T>([AllowNull] [NotNull] T value, string name)
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		// Token: 0x0600038F RID: 911 RVA: 0x0000A839 File Offset: 0x00008A39
		[NullableContext(0)]
		public static void AssertNotNull<T>(T? value, [Nullable(1)] string name) where T : struct
		{
			if (value == null)
			{
				throw new ArgumentNullException(name);
			}
		}

		// Token: 0x06000390 RID: 912 RVA: 0x0000A84C File Offset: 0x00008A4C
		public static void AssertNotNullOrEmpty<[Nullable(2)] T>([AllowNull] [NotNull] IEnumerable<T> value, string name)
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

		// Token: 0x06000391 RID: 913 RVA: 0x0000A8D8 File Offset: 0x00008AD8
		public static void AssertNotNullOrEmpty([AllowNull] [NotNull] string value, string name)
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

		// Token: 0x06000392 RID: 914 RVA: 0x0000A8F8 File Offset: 0x00008AF8
		public static void AssertNotNullOrWhiteSpace([AllowNull] [NotNull] string value, string name)
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

		// Token: 0x06000393 RID: 915 RVA: 0x0000A918 File Offset: 0x00008B18
		[NullableContext(0)]
		public static void AssertNotDefault<T>(ref T value, [Nullable(1)] string name) where T : struct, IEquatable<T>
		{
			if (value.Equals(default(T)))
			{
				throw new ArgumentException("Value cannot be empty.", name);
			}
		}

		// Token: 0x06000394 RID: 916 RVA: 0x0000A948 File Offset: 0x00008B48
		public static void AssertInRange<T>(T value, T minimum, T maximum, string name) where T : IComparable<T>
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

		// Token: 0x06000395 RID: 917 RVA: 0x0000A984 File Offset: 0x00008B84
		public static void AssertEnumDefined(Type enumType, object value, string name)
		{
			if (!Enum.IsDefined(enumType, value))
			{
				throw new ArgumentException("Value not defined for " + enumType.FullName + ".", name);
			}
		}

		// Token: 0x06000396 RID: 918 RVA: 0x0000A9AB File Offset: 0x00008BAB
		public static T CheckNotNull<T>([AllowNull] [NotNull] T value, string name) where T : class
		{
			Argument.AssertNotNull<T>(value, name);
			return value;
		}

		// Token: 0x06000397 RID: 919 RVA: 0x0000A9B5 File Offset: 0x00008BB5
		public static string CheckNotNullOrEmpty([AllowNull] [NotNull] string value, string name)
		{
			Argument.AssertNotNullOrEmpty(value, name);
			return value;
		}

		// Token: 0x06000398 RID: 920 RVA: 0x0000A9BF File Offset: 0x00008BBF
		public static void AssertNull<[Nullable(2)] T>([AllowNull] T value, string name, [AllowNull] string message = null)
		{
			if (value != null)
			{
				throw new ArgumentException(message ?? "Value must be null.", name);
			}
		}
	}
}
