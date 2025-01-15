using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001D4 RID: 468
	public class DomainNameTokenizer : RegexBasedTokenizer
	{
		// Token: 0x06000A40 RID: 2624 RVA: 0x0001EAD8 File Offset: 0x0001CCD8
		[JsonConstructor]
		public DomainNameTokenizer()
		{
			OverlapStrategy overlapStrategy = OverlapStrategy.Subsumption;
			RegexBasedTokenizer.TokenFactoryDelegate tokenFactoryDelegate;
			if ((tokenFactoryDelegate = DomainNameTokenizer.<>O.<0>__ProcessMatch) == null)
			{
				tokenFactoryDelegate = (DomainNameTokenizer.<>O.<0>__ProcessMatch = new RegexBasedTokenizer.TokenFactoryDelegate(DomainNameTokenizer.ProcessMatch));
			}
			base..ctor(overlapStrategy, tokenFactoryDelegate, new TokenPattern[]
			{
				new TokenPattern(DomainNameTokenizer.DomainNamePatternString, "^|[^\\-_\\p{L}\\p{N}]", "$|[^\\-_\\p{L}\\p{N}]", false, false, Array.Empty<RegexOptions>())
			});
		}

		// Token: 0x06000A41 RID: 2625 RVA: 0x0001EB2B File Offset: 0x0001CD2B
		private static IEnumerable<EntityToken> ProcessMatch(TokenPatternMatch m)
		{
			int num;
			if (m.Value.StartsWith("www."))
			{
				if (m.Value.Length <= 4)
				{
					yield break;
				}
				num = m.Start + 4;
			}
			else
			{
				num = m.Start;
			}
			string text = m.Value.Split(new char[] { '.' }).Last<string>();
			if (DomainNameTokenizer.TopLevelDomainNames.Contains(text))
			{
				yield return new DomainNameToken(m.Source, num, m.End);
			}
			yield break;
		}

		// Token: 0x04000517 RID: 1303
		private const string LeftContextPatternString = "^|[^\\-_\\p{L}\\p{N}]";

		// Token: 0x04000518 RID: 1304
		private const string RightContextPatternString = "$|[^\\-_\\p{L}\\p{N}]";

		// Token: 0x04000519 RID: 1305
		private const string ComponentPatternString = "[\\p{L}\\p{N}][\\p{L}\\p{N}\\-]*[\\p{L}\\p{N}]";

		// Token: 0x0400051A RID: 1306
		private static readonly HashSet<string> TopLevelDomainNames = new HashSet<string>
		{
			"ac", "ad", "ae", "af", "ag", "ai", "al", "am", "ao", "aq",
			"ar", "as", "at", "au", "aw", "ax", "az", "ba", "bb", "bd",
			"be", "bf", "bg", "bh", "bi", "bj", "bm", "bn", "bo", "br",
			"bs", "bt", "bv", "bw", "by", "bz", "ca", "cc", "cd", "cf",
			"cg", "ch", "ci", "ck", "cl", "cm", "cn", "co", "cr", "cu",
			"cv", "cw", "cx", "cy", "cz", "de", "dj", "dk", "dm", "do",
			"dz", "ec", "ee", "eg", "er", "es", "et", "eu", "fi", "fj",
			"fk", "fm", "fo", "fr", "ga", "gb", "gd", "ge", "gf", "gg",
			"gh", "gi", "gl", "gm", "gn", "gp", "gq", "gr", "gs", "gt",
			"gu", "gw", "gy", "hk", "hm", "hn", "hr", "ht", "hu", "id",
			"ie", "il", "im", "in", "io", "iq", "ir", "is", "it", "je",
			"jm", "jo", "jp", "ke", "kg", "kh", "ki", "km", "kn", "kp",
			"kr", "kw", "ky", "kz", "la", "lb", "lc", "li", "lk", "lr",
			"ls", "lt", "lu", "lv", "ly", "ma", "mc", "md", "me", "mg",
			"mh", "mk", "ml", "mm", "mn", "mo", "mp", "mq", "mr", "ms",
			"mt", "mu", "mv", "mw", "mx", "my", "mz", "na", "nc", "ne",
			"nf", "ng", "ni", "nl", "no", "np", "nr", "nu", "nz", "om",
			"pa", "pe", "pf", "pg", "ph", "pk", "pl", "pm", "pn", "pr",
			"ps", "pt", "pw", "py", "qa", "re", "ro", "rs", "ru", "rw",
			"sa", "sb", "sc", "sd", "se", "sg", "sh", "si", "sj", "sk",
			"sl", "sm", "sn", "so", "sr", "st", "su", "sv", "sx", "sy",
			"sz", "tc", "td", "tf", "tg", "th", "tj", "tk", "tl", "tm",
			"tn", "to", "tr", "tt", "tv", "tw", "tz", "ua", "ug", "uk",
			"us", "uy", "uz", "va", "vc", "ve", "vg", "vi", "vn", "vu",
			"wf", "ws", "ye", "yt", "za", "zm", "zw", "com", "org", "net",
			"int", "edu", "gov", "mil", "arpa", "info", "xyz", "biz"
		};

		// Token: 0x0400051B RID: 1307
		private static readonly string DomainNamePatternString = FormattableString.Invariant(FormattableStringFactory.Create("{0}(?:\\.{1})+", new object[] { "[\\p{L}\\p{N}][\\p{L}\\p{N}\\-]*[\\p{L}\\p{N}]", "[\\p{L}\\p{N}][\\p{L}\\p{N}\\-]*[\\p{L}\\p{N}]" }));

		// Token: 0x020001D5 RID: 469
		[CompilerGenerated]
		private static class <>O
		{
			// Token: 0x0400051C RID: 1308
			public static RegexBasedTokenizer.TokenFactoryDelegate <0>__ProcessMatch;
		}
	}
}
