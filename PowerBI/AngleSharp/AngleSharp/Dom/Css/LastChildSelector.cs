using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200030F RID: 783
	internal sealed class LastChildSelector : ChildSelector
	{
		// Token: 0x0600169B RID: 5787 RVA: 0x0004F288 File Offset: 0x0004D488
		public LastChildSelector()
			: base(PseudoClassNames.NthLastChild)
		{
		}

		// Token: 0x0600169C RID: 5788 RVA: 0x0004F298 File Offset: 0x0004D498
		public override bool Match(IElement element)
		{
			IElement parentElement = element.ParentElement;
			if (parentElement != null)
			{
				int num = Math.Sign(this._step);
				int num2 = 0;
				for (int i = parentElement.ChildNodes.Length - 1; i >= 0; i--)
				{
					IElement element2 = parentElement.ChildNodes[i] as IElement;
					if (element2 != null && this._kind.Match(element2))
					{
						num2++;
						if (element2 == element)
						{
							int num3 = num2 - this._offset;
							return num3 == 0 || (Math.Sign(num3) == num && num3 % this._step == 0);
						}
					}
				}
			}
			return false;
		}
	}
}
