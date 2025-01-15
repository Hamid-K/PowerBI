using System;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Utilities
{
	// Token: 0x02000089 RID: 137
	internal class Check
	{
		// Token: 0x06000465 RID: 1125 RVA: 0x000105AC File Offset: 0x0000E7AC
		public static T NotNull<T>(T value, string parameterName) where T : class
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			return value;
		}

		// Token: 0x06000466 RID: 1126 RVA: 0x000105BE File Offset: 0x0000E7BE
		public static T? NotNull<T>(T? value, string parameterName) where T : struct
		{
			if (value == null)
			{
				throw new ArgumentNullException(parameterName);
			}
			return value;
		}

		// Token: 0x06000467 RID: 1127 RVA: 0x000105D1 File Offset: 0x0000E7D1
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
