using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace dotless.Core.Utils
{
	// Token: 0x02000010 RID: 16
	public static class StringExtensions
	{
		// Token: 0x06000080 RID: 128 RVA: 0x00003304 File Offset: 0x00001504
		public static string JoinStrings(this IEnumerable<string> source, string separator)
		{
			return string.Join(separator, source.ToArray<string>());
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00003312 File Offset: 0x00001512
		public static string AggregatePaths(this IEnumerable<string> source, string currentDirectory)
		{
			if (!source.Any<string>())
			{
				return "";
			}
			return StringExtensions.CanonicalizePath(source.Aggregate("", new Func<string, string, string>(Path.Combine)), currentDirectory);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00003340 File Offset: 0x00001540
		private static string CanonicalizePath(string path, string currentDirectory)
		{
			Stack<string> stack = new Stack<string>();
			foreach (string text in path.Split(new char[] { '\\', '/' }))
			{
				if (text.Equals("..") && stack.Count > 0 && stack.Peek() != "..")
				{
					stack.Pop();
				}
				else
				{
					stack.Push(text);
				}
			}
			IEnumerable<string> enumerable = stack.Reverse<string>().ToList<string>();
			if (enumerable.First<string>().Equals(".."))
			{
				int num = enumerable.TakeWhile((string segment) => segment.Equals("..")).Count<string>();
				IEnumerable<string> enumerable2 = currentDirectory.Split(new char[] { '\\', '/' }).Reverse<string>().Take(num)
					.Reverse<string>();
				int num2 = 0;
				int num3 = num;
				foreach (string text2 in enumerable2)
				{
					if (num3 >= enumerable.Count<string>() || !string.Equals(text2, enumerable.ElementAt(num3++), StringComparison.InvariantCultureIgnoreCase))
					{
						break;
					}
					num2++;
				}
				enumerable = enumerable.Take(num - num2).Concat(enumerable.Skip(num - num2 + num2 * 2));
			}
			return string.Join("/", enumerable.ToArray<string>());
		}
	}
}
