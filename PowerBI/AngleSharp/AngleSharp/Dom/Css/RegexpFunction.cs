using System;
using System.Text.RegularExpressions;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000235 RID: 565
	internal sealed class RegexpFunction : DocumentFunction
	{
		// Token: 0x060013A0 RID: 5024 RVA: 0x0004ABDD File Offset: 0x00048DDD
		public RegexpFunction(string url)
			: base(FunctionNames.Regexp, url)
		{
			this._regex = new Regex(url, RegexOptions.ECMAScript | RegexOptions.CultureInvariant);
		}

		// Token: 0x060013A1 RID: 5025 RVA: 0x0004ABFC File Offset: 0x00048DFC
		public override bool Matches(Url url)
		{
			return this._regex.IsMatch(url.Href);
		}

		// Token: 0x04000BD2 RID: 3026
		private readonly Regex _regex;
	}
}
