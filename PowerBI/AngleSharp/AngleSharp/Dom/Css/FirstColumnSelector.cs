using System;
using AngleSharp.Css;
using AngleSharp.Dom.Html;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200030C RID: 780
	internal sealed class FirstColumnSelector : ChildSelector
	{
		// Token: 0x06001693 RID: 5779 RVA: 0x0004F090 File Offset: 0x0004D290
		public FirstColumnSelector()
			: base(PseudoClassNames.NthColumn)
		{
		}

		// Token: 0x06001694 RID: 5780 RVA: 0x0004F0A0 File Offset: 0x0004D2A0
		public override bool Match(IElement element)
		{
			IElement parentElement = element.ParentElement;
			if (parentElement != null)
			{
				int num = Math.Sign(this._step);
				int num2 = 0;
				for (int i = 0; i < parentElement.ChildNodes.Length; i++)
				{
					IHtmlTableCellElement htmlTableCellElement = parentElement.ChildNodes[i] as IHtmlTableCellElement;
					if (htmlTableCellElement != null)
					{
						int columnSpan = htmlTableCellElement.ColumnSpan;
						num2 += columnSpan;
						if (htmlTableCellElement == element)
						{
							int num3 = num2 - this._offset;
							int j = 0;
							while (j < columnSpan)
							{
								if (num3 == 0 || (Math.Sign(num3) == num && num3 % this._step == 0))
								{
									return true;
								}
								j++;
								num3--;
							}
							return false;
						}
					}
				}
			}
			return false;
		}
	}
}
