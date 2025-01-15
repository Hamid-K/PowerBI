using System;
using AngleSharp.Css;
using AngleSharp.Extensions;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200030D RID: 781
	internal sealed class FirstTypeSelector : ChildSelector
	{
		// Token: 0x06001695 RID: 5781 RVA: 0x0004F147 File Offset: 0x0004D347
		public FirstTypeSelector()
			: base(PseudoClassNames.NthOfType)
		{
		}

		// Token: 0x06001696 RID: 5782 RVA: 0x0004F154 File Offset: 0x0004D354
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
