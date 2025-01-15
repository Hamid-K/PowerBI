using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ProgramSynthesis.DslLibrary;

namespace Microsoft.ProgramSynthesis.Compound.Split
{
	// Token: 0x0200090A RID: 2314
	public class InputLineTelemetry
	{
		// Token: 0x060031CF RID: 12751 RVA: 0x00092B04 File Offset: 0x00090D04
		public InputLineTelemetry(StringRegion line, HashSet<string> symbols)
		{
			this.PrefixRegexes = (from re in RegularExpression.LearnRightMatches(line, 0U, 1, 0)
				where re.Count > 0
				select re into r
				select r.ToString()).ToList<string>();
			this.SymbolCounts = (from c in line.Value.Select((char c) => c.ToString()).Where(new Func<string, bool>(symbols.Contains))
				group c by c).ToDictionary((IGrouping<string, string> g) => g.Key, (IGrouping<string, string> g) => g.Count<string>());
			this.Length = line.Length;
		}

		// Token: 0x170008C4 RID: 2244
		// (get) Token: 0x060031D0 RID: 12752 RVA: 0x00092C28 File Offset: 0x00090E28
		// (set) Token: 0x060031D1 RID: 12753 RVA: 0x00092C30 File Offset: 0x00090E30
		public IReadOnlyList<string> PrefixRegexes { get; private set; }

		// Token: 0x170008C5 RID: 2245
		// (get) Token: 0x060031D2 RID: 12754 RVA: 0x00092C39 File Offset: 0x00090E39
		// (set) Token: 0x060031D3 RID: 12755 RVA: 0x00092C41 File Offset: 0x00090E41
		public IReadOnlyDictionary<string, int> SymbolCounts { get; private set; }

		// Token: 0x170008C6 RID: 2246
		// (get) Token: 0x060031D4 RID: 12756 RVA: 0x00092C4A File Offset: 0x00090E4A
		// (set) Token: 0x060031D5 RID: 12757 RVA: 0x00092C52 File Offset: 0x00090E52
		public uint Length { get; private set; }
	}
}
