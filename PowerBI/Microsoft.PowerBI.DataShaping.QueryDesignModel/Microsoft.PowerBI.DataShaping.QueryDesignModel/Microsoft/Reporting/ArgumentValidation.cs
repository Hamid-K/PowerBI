using System;
using System.Collections.Generic;
using System.Linq;

namespace Microsoft.Reporting
{
	// Token: 0x020000C6 RID: 198
	internal static class ArgumentValidation
	{
		// Token: 0x06000CA8 RID: 3240 RVA: 0x000213ED File Offset: 0x0001F5ED
		internal static T CheckNotNull<T>(T argument, string name) where T : class
		{
			if (argument == null)
			{
				throw new ArgumentNullException(name);
			}
			return argument;
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x000213FF File Offset: 0x0001F5FF
		internal static string CheckNotNullOrEmpty(string argument, string name)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(name);
			}
			if (string.IsNullOrEmpty(argument))
			{
				throw new ArgumentException("The argument value must be a non-empty string.", name);
			}
			return argument;
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x00021420 File Offset: 0x0001F620
		internal static string CheckNotNullOrWhiteSpace(string argument, string name)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(name);
			}
			if (argument.IsNullOrWhiteSpace())
			{
				throw new ArgumentException("The argument value must be a non-empty string that does not consist solely of whitespace.", name);
			}
			return argument;
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x00021441 File Offset: 0x0001F641
		internal static IEnumerable<T> CheckNotNullOrEmpty<T>(IEnumerable<T> argument, string name)
		{
			if (argument == null)
			{
				throw new ArgumentNullException(name);
			}
			if (!argument.Any<T>())
			{
				throw new ArgumentException("The argument value must be a non-empty collection.", name);
			}
			return argument;
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x00021462 File Offset: 0x0001F662
		internal static T CheckAs<T>(object argument, string name) where T : class
		{
			T t = argument as T;
			if (t == null)
			{
				throw new ArgumentOutOfRangeException(name);
			}
			return t;
		}

		// Token: 0x06000CAD RID: 3245 RVA: 0x00021480 File Offset: 0x0001F680
		internal static T CheckAsOrNull<T>(object argument, string name) where T : class
		{
			if (argument == null)
			{
				return default(T);
			}
			return ArgumentValidation.CheckAs<T>(argument, name);
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x000214A1 File Offset: 0x0001F6A1
		internal static void CheckCondition(bool validArgument)
		{
			if (!validArgument)
			{
				throw new ArgumentException();
			}
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x000214AC File Offset: 0x0001F6AC
		internal static void CheckCondition(bool validArgument, string argumentName)
		{
			ArgumentValidation.CheckCondition(validArgument, argumentName, "The argument value is invalid.");
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x000214BA File Offset: 0x0001F6BA
		internal static void CheckCondition(bool validArgument, string argumentName, string message)
		{
			if (!validArgument)
			{
				throw new ArgumentException(message, argumentName);
			}
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x000214C7 File Offset: 0x0001F6C7
		internal static T CheckCondition<T>(T argument, bool validArgument, string argumentName)
		{
			return ArgumentValidation.CheckCondition<T>(argument, validArgument, argumentName, "The argument value is invalid.");
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x000214D6 File Offset: 0x0001F6D6
		internal static T CheckCondition<T>(T argument, bool validArgument, string argumentName, string message)
		{
			if (!validArgument)
			{
				throw new ArgumentException(message, argumentName);
			}
			return argument;
		}
	}
}
