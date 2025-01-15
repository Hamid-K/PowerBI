using System;
using System.Text.RegularExpressions;

namespace dotless.Core.Parser.Infrastructure.Nodes
{
	// Token: 0x02000061 RID: 97
	public class RegexMatchResult : TextNode
	{
		// Token: 0x170000D4 RID: 212
		// (get) Token: 0x0600043E RID: 1086 RVA: 0x000153AB File Offset: 0x000135AB
		// (set) Token: 0x0600043F RID: 1087 RVA: 0x000153B3 File Offset: 0x000135B3
		public Match Match { get; set; }

		// Token: 0x06000440 RID: 1088 RVA: 0x000153BC File Offset: 0x000135BC
		public RegexMatchResult(Match match)
			: base(match.Value)
		{
			this.Match = match;
		}

		// Token: 0x170000D5 RID: 213
		public string this[int index]
		{
			get
			{
				string value = this.Match.Groups[index].Value;
				if (!string.IsNullOrEmpty(value))
				{
					return value;
				}
				return null;
			}
		}
	}
}
