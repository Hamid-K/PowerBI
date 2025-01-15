using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x020000A9 RID: 169
	public class CFormatString : Function
	{
		// Token: 0x060004FC RID: 1276 RVA: 0x00017188 File Offset: 0x00015388
		protected override Node Evaluate(Env env)
		{
			base.WarnNotSupportedByLessJS("%(string, args...)", "~\"\" and string interpolation");
			if (base.Arguments.Count == 0)
			{
				return new Quoted("", false);
			}
			Func<Node, string> stringValue = delegate(Node n)
			{
				if (!(n is Quoted))
				{
					return n.ToCSS(env);
				}
				return ((Quoted)n).Value;
			};
			string text = stringValue(base.Arguments[0]);
			List<Node> args = base.Arguments.Skip(1).ToList<Node>();
			int i = 0;
			MatchEvaluator matchEvaluator = delegate(Match m)
			{
				string text2;
				if (!(m.Value == "%s"))
				{
					List<Node> args3 = args;
					int num = i;
					i = num + 1;
					text2 = args3[num].ToCSS(env);
				}
				else
				{
					Func<Node, string> stringValue2 = stringValue;
					List<Node> args2 = args;
					int num = i;
					i = num + 1;
					text2 = stringValue2(args2[num]);
				}
				string text3 = text2;
				if (!char.IsUpper(m.Value[1]))
				{
					return text3;
				}
				return Uri.EscapeDataString(text3);
			};
			text = Regex.Replace(text, "%[sda]", matchEvaluator, RegexOptions.IgnoreCase);
			char? c = ((base.Arguments[0] is Quoted) ? (base.Arguments[0] as Quoted).Quote : null);
			return new Quoted(text, c);
		}
	}
}
