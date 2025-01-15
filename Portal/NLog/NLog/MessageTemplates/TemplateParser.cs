using System;
using System.Collections.Generic;

namespace NLog.MessageTemplates
{
	// Token: 0x02000089 RID: 137
	internal static class TemplateParser
	{
		// Token: 0x06000998 RID: 2456 RVA: 0x0001977C File Offset: 0x0001797C
		public static Template Parse(string template)
		{
			if (template == null)
			{
				throw new ArgumentNullException("template");
			}
			bool flag = true;
			List<Literal> list = new List<Literal>();
			List<Hole> list2 = new List<Hole>();
			TemplateEnumerator templateEnumerator = new TemplateEnumerator(template);
			while (templateEnumerator.MoveNext())
			{
				if (templateEnumerator.Current.Literal.Skip == 0)
				{
					list.Add(templateEnumerator.Current.Literal);
				}
				else
				{
					list.Add(templateEnumerator.Current.Literal);
					list2.Add(templateEnumerator.Current.Hole);
					if (templateEnumerator.Current.Hole.Index == -1)
					{
						flag = false;
					}
				}
			}
			return new Template(template, flag, list, list2);
		}
	}
}
