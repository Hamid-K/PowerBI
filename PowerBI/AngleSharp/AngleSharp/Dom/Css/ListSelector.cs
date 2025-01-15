using System;
using System.IO;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000312 RID: 786
	internal sealed class ListSelector : Selectors, ISelector, ICssNode, IStyleFormattable
	{
		// Token: 0x170005DB RID: 1499
		// (get) Token: 0x060016A1 RID: 5793 RVA: 0x0004F492 File Offset: 0x0004D692
		// (set) Token: 0x060016A2 RID: 5794 RVA: 0x0004F49A File Offset: 0x0004D69A
		public bool IsInvalid { get; internal set; }

		// Token: 0x060016A3 RID: 5795 RVA: 0x0004F4A4 File Offset: 0x0004D6A4
		public bool Match(IElement element)
		{
			for (int i = 0; i < this._selectors.Count; i++)
			{
				if (this._selectors[i].Match(element))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060016A4 RID: 5796 RVA: 0x0004F4E0 File Offset: 0x0004D6E0
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			if (this._selectors.Count > 0)
			{
				writer.Write(this._selectors[0].Text);
				for (int i = 1; i < this._selectors.Count; i++)
				{
					writer.Write(',');
					writer.Write(this._selectors[i].Text);
				}
			}
		}
	}
}
