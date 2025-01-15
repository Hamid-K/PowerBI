using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Microsoft.ProgramSynthesis.Learning
{
	// Token: 0x020006C3 RID: 1731
	internal static class Generators
	{
		// Token: 0x040011D3 RID: 4563
		public static readonly Dictionary<Type, LiteralGenerator> Standard = new Dictionary<Type, LiteralGenerator>
		{
			{
				typeof(int),
				() => Generators.IntGenerator
			},
			{
				typeof(Regex),
				() => Generators.RegexGenerator
			}
		};

		// Token: 0x040011D4 RID: 4564
		private static readonly object[] RegexGenerator = new object[]
		{
			new Regex("\\d+", RegexOptions.Compiled),
			new Regex("[a-zA-Z]+", RegexOptions.Compiled),
			new Regex("", RegexOptions.Compiled)
		};

		// Token: 0x040011D5 RID: 4565
		private static readonly object[] IntGenerator = Enumerable.Range(-20, 40).Cast<object>().ToArray<object>();
	}
}
