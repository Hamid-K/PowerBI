using System;
using Microsoft.InfoNav.Utils;

namespace Microsoft.InfoNav.Data.Contracts.Internal
{
	// Token: 0x0200026C RID: 620
	internal sealed class QueryResolutionMessage
	{
		// Token: 0x060012D3 RID: 4819 RVA: 0x00021817 File Offset: 0x0001FA17
		internal QueryResolutionMessage(string text, IContainsTelemetryMarkup unresolvedModelReference = null)
		{
			this._text = text;
			this._unresolvedModelReference = unresolvedModelReference;
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x060012D4 RID: 4820 RVA: 0x0002182D File Offset: 0x0001FA2D
		public string Text
		{
			get
			{
				return this._text;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x060012D5 RID: 4821 RVA: 0x00021835 File Offset: 0x0001FA35
		public IContainsTelemetryMarkup UnresolvedModelReference
		{
			get
			{
				return this._unresolvedModelReference;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x060012D6 RID: 4822 RVA: 0x0002183D File Offset: 0x0001FA3D
		public bool HasUnresolvedModelReference
		{
			get
			{
				return this._unresolvedModelReference != null;
			}
		}

		// Token: 0x040007D3 RID: 2003
		private readonly string _text;

		// Token: 0x040007D4 RID: 2004
		private readonly IContainsTelemetryMarkup _unresolvedModelReference;
	}
}
