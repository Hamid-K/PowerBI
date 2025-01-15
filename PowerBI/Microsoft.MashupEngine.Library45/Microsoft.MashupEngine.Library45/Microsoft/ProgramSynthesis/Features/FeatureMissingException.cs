using System;
using System.Runtime.CompilerServices;

namespace Microsoft.ProgramSynthesis.Features
{
	// Token: 0x020007D6 RID: 2006
	public class FeatureMissingException : Exception
	{
		// Token: 0x06002ABB RID: 10939 RVA: 0x00077F8F File Offset: 0x0007618F
		public FeatureMissingException(Grammar grammar, string name)
			: base(FormattableString.Invariant(FormattableStringFactory.Create("No feature '{0}' exists in the grammar {1}.", new object[] { name, grammar.Name })))
		{
			this.Grammar = grammar;
			this.Name = name;
		}

		// Token: 0x1700077C RID: 1916
		// (get) Token: 0x06002ABC RID: 10940 RVA: 0x00077FC7 File Offset: 0x000761C7
		public Grammar Grammar { get; }

		// Token: 0x1700077D RID: 1917
		// (get) Token: 0x06002ABD RID: 10941 RVA: 0x00077FCF File Offset: 0x000761CF
		public string Name { get; }
	}
}
