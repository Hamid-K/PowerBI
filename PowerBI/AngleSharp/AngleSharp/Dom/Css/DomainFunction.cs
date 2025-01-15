using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000234 RID: 564
	internal sealed class DomainFunction : DocumentFunction
	{
		// Token: 0x0600139E RID: 5022 RVA: 0x0004AB8C File Offset: 0x00048D8C
		public DomainFunction(string url)
			: base(FunctionNames.Domain, url)
		{
			this._subdomain = "." + url;
		}

		// Token: 0x0600139F RID: 5023 RVA: 0x0004ABAC File Offset: 0x00048DAC
		public override bool Matches(Url url)
		{
			string hostName = url.HostName;
			return hostName.Isi(base.Data) || hostName.EndsWith(this._subdomain, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x04000BD1 RID: 3025
		private readonly string _subdomain;
	}
}
