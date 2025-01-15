using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace AngleSharp.Dom.Css
{
	// Token: 0x02000206 RID: 518
	internal sealed class AndCondition : CssNode, IConditionFunction, ICssNode, IStyleFormattable
	{
		// Token: 0x06001388 RID: 5000 RVA: 0x0004A8AC File Offset: 0x00048AAC
		public bool Check()
		{
			using (IEnumerator<IConditionFunction> enumerator = base.Children.OfType<IConditionFunction>().GetEnumerator())
			{
				while (enumerator.MoveNext())
				{
					if (!enumerator.Current.Check())
					{
						return false;
					}
				}
			}
			return true;
		}

		// Token: 0x06001389 RID: 5001 RVA: 0x0004A904 File Offset: 0x00048B04
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
					writer.Write(" and ");
				}
				styleFormattable.ToCss(writer, formatter);
			}
		}
	}
}
