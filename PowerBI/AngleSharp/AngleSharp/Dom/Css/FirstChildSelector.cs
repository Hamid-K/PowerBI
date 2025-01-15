using System;
using AngleSharp.Css;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200030B RID: 779
	internal sealed class FirstChildSelector : ChildSelector
	{
		// Token: 0x06001691 RID: 5777 RVA: 0x0004EFEE File Offset: 0x0004D1EE
		public FirstChildSelector()
			: base(PseudoClassNames.NthChild)
		{
		}

		// Token: 0x06001692 RID: 5778 RVA: 0x0004EFFC File Offset: 0x0004D1FC
		public override bool Match(IElement element)
		{
			IElement parentElement = element.ParentElement;
			if (parentElement != null)
			{
				int num = Math.Sign(this._step);
				int num2 = 0;
				for (int i = 0; i < parentElement.ChildNodes.Length; i++)
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
