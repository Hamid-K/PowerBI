using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000236 RID: 566
	internal sealed class UrlFunction : DocumentFunction
	{
		// Token: 0x060013A2 RID: 5026 RVA: 0x0004AC0F File Offset: 0x00048E0F
		public UrlFunction(string url)
			: base(FunctionNames.Url, url)
		{
			this._expected = Url.Create(base.Data);
		}

		// Token: 0x060013A3 RID: 5027 RVA: 0x0004AC2E File Offset: 0x00048E2E
		public override bool Matches(Url actual)
		{
			return !this._expected.IsInvalid && this._expected.Equals(actual);
		}

		// Token: 0x04000BD3 RID: 3027
		private readonly Url _expected;
	}
}
