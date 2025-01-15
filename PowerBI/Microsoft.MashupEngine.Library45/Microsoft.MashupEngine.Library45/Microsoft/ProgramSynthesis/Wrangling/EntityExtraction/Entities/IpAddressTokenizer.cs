using System;
using System.Collections.Generic;
using System.Net;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Utils;
using Newtonsoft.Json;

namespace Microsoft.ProgramSynthesis.Wrangling.EntityExtraction.Entities
{
	// Token: 0x020001E7 RID: 487
	public class IpAddressTokenizer : RegexBasedTokenizer
	{
		// Token: 0x06000A96 RID: 2710 RVA: 0x00020114 File Offset: 0x0001E314
		[JsonConstructor]
		public IpAddressTokenizer()
			: base(OverlapStrategy.Subsumption)
		{
			base.Initialize(new RegexBasedTokenizer.TokenFactoryDelegate(this.ProcessMatch), new TokenPattern[] { this._ipV4AddressTokenPattern, this._ipV6AddressTokenPattern });
		}

		// Token: 0x06000A97 RID: 2711 RVA: 0x00020194 File Offset: 0x0001E394
		private IEnumerable<EntityToken> ProcessMatch(TokenPatternMatch m)
		{
			Group group = m.FullMatch.Groups["Address"];
			if (!group.Success)
			{
				yield break;
			}
			IPAddress ipaddress;
			if (!IPAddress.TryParse(group.Value, out ipaddress))
			{
				yield break;
			}
			Group group2 = m.FullMatch.Groups["SubnetBits"];
			int? num = null;
			if (group2.Success)
			{
				num = new int?(Convert.ToInt32(group2.Value));
			}
			yield return IpAddressToken.Create(m.Source, m.Start, m.End, ipaddress, num);
			yield break;
		}

		// Token: 0x0400053D RID: 1341
		private const string DottedQuadComponentPattern = "\\d{1,3}";

		// Token: 0x0400053E RID: 1342
		private const string HexPattern = "[a-fA-F0-9]";

		// Token: 0x0400053F RID: 1343
		private const string IpV4LeftContext = "(?:^|[^\\.\\d])";

		// Token: 0x04000540 RID: 1344
		private const string IpV4RightContext = "(?:$|[^\\.\\d])";

		// Token: 0x04000541 RID: 1345
		private const string IpV6LeftContext = "(?:^|[^\\:a-fA-F0-9])";

		// Token: 0x04000542 RID: 1346
		private const string IpV6RightContext = "(?:$|[^\\:\\da-fA-F0-9])";

		// Token: 0x04000543 RID: 1347
		[JsonIgnore]
		public const string SubnetBitsGroupName = "SubnetBits";

		// Token: 0x04000544 RID: 1348
		[JsonIgnore]
		public const string AddressGroupName = "Address";

		// Token: 0x04000545 RID: 1349
		private static readonly string ColonSeparatedComponentPattern = FormattableString.Invariant(FormattableStringFactory.Create("{0}{{0,4}}", new object[] { "[a-fA-F0-9]" }));

		// Token: 0x04000546 RID: 1350
		private static readonly string IpV4AddressPattern = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>{1}(?:\\.{2}){{3}})(?:/(?<{3}>\\d{{1,2}}))?", new object[] { "Address", "\\d{1,3}", "\\d{1,3}", "SubnetBits" }));

		// Token: 0x04000547 RID: 1351
		private static readonly string IpV6AddressPattern = FormattableString.Invariant(FormattableStringFactory.Create("(?<{0}>{1}(?:\\:{2})+)(?:/(?<{3}>\\d{{1,3}}))?", new object[]
		{
			"Address",
			IpAddressTokenizer.ColonSeparatedComponentPattern,
			IpAddressTokenizer.ColonSeparatedComponentPattern,
			"SubnetBits"
		}));

		// Token: 0x04000548 RID: 1352
		[JsonIgnore]
		private readonly TokenPattern _ipV4AddressTokenPattern = new TokenPattern(IpAddressTokenizer.IpV4AddressPattern, "(?:^|[^\\.\\d])", "(?:$|[^\\.\\d])", false, false, Array.Empty<RegexOptions>());

		// Token: 0x04000549 RID: 1353
		[JsonIgnore]
		private readonly TokenPattern _ipV6AddressTokenPattern = new TokenPattern(IpAddressTokenizer.IpV6AddressPattern, "(?:^|[^\\:a-fA-F0-9])", "(?:$|[^\\:\\da-fA-F0-9])", false, false, Array.Empty<RegexOptions>());
	}
}
