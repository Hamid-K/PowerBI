using System;
using System.Data.Entity.SqlServer.Resources;

namespace System.Data.Entity.SqlServer.Utilities
{
	// Token: 0x02000027 RID: 39
	internal class Check
	{
		// Token: 0x06000420 RID: 1056 RVA: 0x00010120 File Offset: 0x0000E320
		public static T NotNull<T>(T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			return value;
		}

		// Token: 0x06000421 RID: 1057 RVA: 0x00010132 File Offset: 0x0000E332
		public static T? NotNull<T>(T? value, string parameterName) where T : struct
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			return value;
		}

		// Token: 0x06000422 RID: 1058 RVA: 0x00010145 File Offset: 0x0000E345
		public static string NotEmpty(string value, string parameterName)
		{
			if (string.IsNullOrWhiteSpace(value))
			{
				throw new ArgumentException(Strings.ArgumentIsNullOrWhitespace(parameterName));
			}
			return value;
		}
	}
}
