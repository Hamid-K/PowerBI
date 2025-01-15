using System;
using System.Linq;
using JetBrains.Annotations;

namespace NLog.Internal
{
	// Token: 0x02000146 RID: 326
	public static class StringHelpers
	{
		// Token: 0x06000FC7 RID: 4039 RVA: 0x0002895C File Offset: 0x00026B5C
		[ContractAnnotation("value:null => true")]
		internal static bool IsNullOrWhiteSpace(string value)
		{
			return string.IsNullOrWhiteSpace(value);
		}

		// Token: 0x06000FC8 RID: 4040 RVA: 0x00028964 File Offset: 0x00026B64
		internal static string[] SplitAndTrimTokens(this string value, char delimiter)
		{
			if (StringHelpers.IsNullOrWhiteSpace(value))
			{
				return ArrayHelper.Empty<string>();
			}
			if (value.IndexOf(delimiter) == -1)
			{
				return new string[] { value.Trim() };
			}
			string[] array = value.Split(new char[] { delimiter }, StringSplitOptions.RemoveEmptyEntries);
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = array[i].Trim();
				if (string.IsNullOrEmpty(array[i]))
				{
					return (from s in array
						where !StringHelpers.IsNullOrWhiteSpace(s)
						select s.Trim()).ToArray<string>();
				}
			}
			return array;
		}
	}
}
