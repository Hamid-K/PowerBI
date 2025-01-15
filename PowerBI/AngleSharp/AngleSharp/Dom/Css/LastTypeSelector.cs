using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000311 RID: 785
	internal sealed class LastTypeSelector : ChildSelector
	{
		// Token: 0x0600169F RID: 5791 RVA: 0x0004F3E5 File Offset: 0x0004D5E5
		public LastTypeSelector()
			: base(PseudoClassNames.NthLastOfType)
		{
		}

		// Token: 0x060016A0 RID: 5792 RVA: 0x0004F3F4 File Offset: 0x0004D5F4
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
					if (element2 != null && element2.NodeName.Is(element.NodeName))
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
