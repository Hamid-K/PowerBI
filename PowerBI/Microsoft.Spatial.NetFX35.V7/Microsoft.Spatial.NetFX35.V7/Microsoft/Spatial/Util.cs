using System;
using System.Security;
using System.Threading;

namespace Microsoft.Spatial
{
	// Token: 0x0200006B RID: 107
	internal class Util
	{
		// Token: 0x0600028B RID: 651 RVA: 0x0000680E File Offset: 0x00004A0E
		internal static void CheckArgumentNull([Util.ValidatedNotNullAttribute] object arg, string errorMessage)
		{
			if (arg == null)
			{
				throw new ArgumentNullException(errorMessage);
			}
		}

		// Token: 0x0600028C RID: 652 RVA: 0x0000681C File Offset: 0x00004A1C
		internal static bool IsCatchableExceptionType(Exception e)
		{
			Type type = e.GetType();
			return type != Util.OutOfMemoryType && type != Util.StackOverflowType && type != Util.ThreadAbortType && type != Util.AccessViolationType && type != Util.NullReferenceType && !Util.SecurityType.IsAssignableFrom(type);
		}

		// Token: 0x040000CB RID: 203
		private static readonly Type StackOverflowType = typeof(StackOverflowException);

		// Token: 0x040000CC RID: 204
		private static readonly Type ThreadAbortType = typeof(ThreadAbortException);

		// Token: 0x040000CD RID: 205
		private static readonly Type AccessViolationType = typeof(AccessViolationException);

		// Token: 0x040000CE RID: 206
		private static readonly Type OutOfMemoryType = typeof(OutOfMemoryException);

		// Token: 0x040000CF RID: 207
		private static readonly Type NullReferenceType = typeof(NullReferenceException);

		// Token: 0x040000D0 RID: 208
		private static readonly Type SecurityType = typeof(SecurityException);

		// Token: 0x0200008B RID: 139
		private sealed class ValidatedNotNullAttribute : Attribute
		{
		}
	}
}
