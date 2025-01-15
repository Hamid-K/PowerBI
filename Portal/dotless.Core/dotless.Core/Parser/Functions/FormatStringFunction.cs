using System;
using System.Linq;
using dotless.Core.Exceptions;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x02000078 RID: 120
	public class FormatStringFunction : Function
	{
		// Token: 0x06000480 RID: 1152 RVA: 0x00015DB4 File Offset: 0x00013FB4
		protected override Node Evaluate(Env env)
		{
			base.WarnNotSupportedByLessJS("formatstring(string, args...)", null, " You may want to consider using string interpolation (\"@{variable}\") which does the same thing and is supported.");
			if (base.Arguments.Count == 0)
			{
				return new Quoted("", false);
			}
			Func<Node, string> func = delegate(Node n)
			{
				if (!(n is Quoted))
				{
					return n.ToCSS(env);
				}
				return ((Quoted)n).UnescapeContents();
			};
			string text = func(base.Arguments[0]);
			string[] array = base.Arguments.Skip(1).Select(func).ToArray<string>();
			string text3;
			try
			{
				string text2 = text;
				object[] array2 = array;
				text3 = string.Format(text2, array2);
			}
			catch (FormatException ex)
			{
				throw new ParserException(string.Format("Error in formatString :{0}", ex.Message), ex);
			}
			return new Quoted(text3, false);
		}
	}
}
