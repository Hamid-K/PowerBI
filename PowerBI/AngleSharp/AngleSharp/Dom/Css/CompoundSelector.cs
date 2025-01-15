using System;
using System.IO;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200030A RID: 778
	internal sealed class CompoundSelector : Selectors, ISelector, ICssNode, IStyleFormattable
	{
		// Token: 0x0600168E RID: 5774 RVA: 0x0004EF70 File Offset: 0x0004D170
		public bool Match(IElement element)
		{
			for (int i = 0; i < this._selectors.Count; i++)
			{
				if (!this._selectors[i].Match(element))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x0600168F RID: 5775 RVA: 0x0004EFAC File Offset: 0x0004D1AC
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			for (int i = 0; i < this._selectors.Count; i++)
			{
				writer.Write(this._selectors[i].Text);
			}
		}
	}
}
