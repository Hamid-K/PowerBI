using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net.Http;
using System.Text.RegularExpressions;

namespace System.Web.Http.Routing.Constraints
{
	// Token: 0x0200017D RID: 381
	public class RegexRouteConstraint : IHttpRouteConstraint
	{
		// Token: 0x060009E6 RID: 2534 RVA: 0x000197EF File Offset: 0x000179EF
		public RegexRouteConstraint(string pattern)
		{
			this.Pattern = pattern;
			this._regex = new Regex(pattern, RegexOptions.IgnoreCase | RegexOptions.Compiled | RegexOptions.CultureInvariant);
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x060009E7 RID: 2535 RVA: 0x0001980F File Offset: 0x00017A0F
		// (set) Token: 0x060009E8 RID: 2536 RVA: 0x00019817 File Offset: 0x00017A17
		public string Pattern { get; private set; }

		// Token: 0x060009E9 RID: 2537 RVA: 0x00019820 File Offset: 0x00017A20
		public bool Match(HttpRequestMessage request, IHttpRoute route, string parameterName, IDictionary<string, object> values, HttpRouteDirection routeDirection)
		{
			if (parameterName == null)
			{
				throw Error.ArgumentNull("parameterName");
			}
			if (values == null)
			{
				throw Error.ArgumentNull("values");
			}
			object obj;
			if (values.TryGetValue(parameterName, out obj) && obj != null)
			{
				string text = Convert.ToString(obj, CultureInfo.InvariantCulture);
				return this._regex.IsMatch(text);
			}
			return false;
		}

		// Token: 0x040002A7 RID: 679
		private readonly Regex _regex;
	}
}
