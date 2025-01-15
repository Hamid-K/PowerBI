using System;
using System.Collections.Generic;
using System.IO;
using AngleSharp.Css.Values;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200030E RID: 782
	internal sealed class KeyframeSelector : CssNode, IKeyframeSelector, ICssNode, IStyleFormattable
	{
		// Token: 0x06001697 RID: 5783 RVA: 0x0004F1F0 File Offset: 0x0004D3F0
		public KeyframeSelector(IEnumerable<Percent> stops)
		{
			this._stops = new List<Percent>(stops);
		}

		// Token: 0x170005D9 RID: 1497
		// (get) Token: 0x06001698 RID: 5784 RVA: 0x0004F204 File Offset: 0x0004D404
		public IEnumerable<Percent> Stops
		{
			get
			{
				return this._stops;
			}
		}

		// Token: 0x170005DA RID: 1498
		// (get) Token: 0x06001699 RID: 5785 RVA: 0x0004810B File Offset: 0x0004630B
		public string Text
		{
			get
			{
				return this.ToCss();
			}
		}

		// Token: 0x0600169A RID: 5786 RVA: 0x0004F20C File Offset: 0x0004D40C
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			if (this._stops.Count > 0)
			{
				writer.Write(this._stops[0].ToString());
				for (int i = 1; i < this._stops.Count; i++)
				{
					writer.Write(", ");
					writer.Write(this._stops[i].ToString());
				}
			}
		}

		// Token: 0x04000C9E RID: 3230
		private readonly List<Percent> _stops;
	}
}
