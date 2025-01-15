using System;
using dotless.Core.Exceptions;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Parser.Tree;
using dotless.Core.Utils;

namespace dotless.Core.Parser.Functions
{
	// Token: 0x0200006D RID: 109
	public class ColorFunction : Function
	{
		// Token: 0x06000462 RID: 1122 RVA: 0x000156D0 File Offset: 0x000138D0
		protected override Node Evaluate(Env env)
		{
			Guard.ExpectNumArguments(1, base.Arguments.Count, this, base.Location);
			TextNode textNode = Guard.ExpectNode<TextNode>(base.Arguments[0], this, base.Location);
			Node node;
			try
			{
				node = Color.From(textNode.Value);
			}
			catch (FormatException ex)
			{
				throw new ParsingException(string.Format("Invalid RGB color string '{0}'", textNode.Value), ex, base.Location, null);
			}
			return node;
		}
	}
}
