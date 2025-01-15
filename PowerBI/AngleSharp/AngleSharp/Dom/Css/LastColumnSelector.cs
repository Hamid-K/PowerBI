using System;
using AngleSharp.Css;
using AngleSharp.Dom.Html;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000310 RID: 784
	internal sealed class LastColumnSelector : ChildSelector
	{
		// Token: 0x0600169D RID: 5789 RVA: 0x0004F32E File Offset: 0x0004D52E
		public LastColumnSelector()
			: base(PseudoClassNames.NthLastColumn)
		{
		}

		// Token: 0x0600169E RID: 5790 RVA: 0x0004F33C File Offset: 0x0004D53C
		public override bool Match(IElement element)
		{
			IElement parentElement = element.ParentElement;
			if (parentElement != null)
			{
				int num = Math.Sign(this._step);
				int num2 = 0;
				for (int i = parentElement.ChildNodes.Length - 1; i >= 0; i--)
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
