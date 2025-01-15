using System;
using dotless.Core.Exceptions;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x0200004D RID: 77
	public class Variable : Node
	{
		// Token: 0x170000AA RID: 170
		// (get) Token: 0x06000320 RID: 800 RVA: 0x0000E359 File Offset: 0x0000C559
		// (set) Token: 0x06000321 RID: 801 RVA: 0x0000E361 File Offset: 0x0000C561
		public string Name { get; set; }

		// Token: 0x06000322 RID: 802 RVA: 0x0000E36A File Offset: 0x0000C56A
		public Variable(string name)
		{
			this.Name = name;
		}

		// Token: 0x06000323 RID: 803 RVA: 0x0000E379 File Offset: 0x0000C579
		protected override Node CloneCore()
		{
			return new Variable(this.Name);
		}

		// Token: 0x06000324 RID: 804 RVA: 0x0000E388 File Offset: 0x0000C588
		public override Node Evaluate(Env env)
		{
			string text = this.Name;
			if (text.StartsWith("@@"))
			{
				Node node = new Variable(text.Substring(1)).Evaluate(env);
				text = "@" + ((node is TextNode) ? (node as TextNode).Value : node.ToCSS(env));
			}
			if (env.IsEvaluatingVariable(text))
			{
				throw new ParsingException("Recursive variable definition for " + text, base.Location);
			}
			Rule rule = env.FindVariable(text);
			if (rule)
			{
				return rule.Value.Evaluate(env.CreateVariableEvaluationEnv(text));
			}
			throw new ParsingException("variable " + text + " is undefined", base.Location);
		}
	}
}
