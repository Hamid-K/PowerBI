using System;
using System.Security;

namespace Microsoft.Spatial
{
	// Token: 0x02000070 RID: 112
	internal class Util
	{
		// Token: 0x06000301 RID: 769 RVA: 0x000074D6 File Offset: 0x000056D6
		internal static void CheckArgumentNull([Util.ValidatedNotNullAttribute] object arg, string errorMessage)
		{
			if (arg == null)
			{
				throw new ArgumentNullException(errorMessage);
			}
		}

		// Token: 0x06000302 RID: 770 RVA: 0x000074E4 File Offset: 0x000056E4
		internal static bool IsCatchableExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != Util.OutOfMemoryType && type != Util.NullReferenceType && !Util.SecurityType.IsAssignableFrom(type);
		}

		// Token: 0x040000D8 RID: 216
		private static readonly Type OutOfMemoryType = typeof(OutOfMemoryException);

		// Token: 0x040000D9 RID: 217
		private static readonly Type NullReferenceType = typeof(NullReferenceException);

		// Token: 0x040000DA RID: 218
		private static readonly Type SecurityType = typeof(SecurityException);

		// Token: 0x02000097 RID: 151
		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}
