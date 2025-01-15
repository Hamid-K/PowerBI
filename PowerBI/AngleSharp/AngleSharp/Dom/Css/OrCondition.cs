using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AngleSharp.Dom.Css
{
	// Token: 0x0200020B RID: 523
	internal sealed class OrCondition : CssNode, IConditionFunction, ICssNode, IStyleFormattable
	{
		// Token: 0x0600139B RID: 5019 RVA: 0x0004AACC File Offset: 0x00048CCC
		public bool Check()
		{
			using (IEnumerator<IConditionFunction> enumerator = base.Children.OfType<IConditionFunction>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (enumerator.Current.Check())
					{
						return true;
					}
				}
			}
			return false;
		}

		// Token: 0x0600139C RID: 5020 RVA: 0x0004AB24 File Offset: 0x00048D24
		public override void ToCss(TextWriter writer, IStyleFormatter formatter)
		{
			IEnumerable<IConditionFunction> enumerable = base.Children.OfType<IConditionFunction>();
			bool flag = true;
			foreach (IStyleFormattable styleFormattable in enumerable)
			{
				if (flag)
				{
					flag = false;
				}
				else
				{
					writer.Write(" or ");
				}
				styleFormattable.ToCss(writer, formatter);
			}
		}
	}
}
