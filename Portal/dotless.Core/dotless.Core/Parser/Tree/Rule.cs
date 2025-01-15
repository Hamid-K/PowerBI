using System;
using System.Text.RegularExpressions;
using dotless.Core.Exceptions;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000046 RID: 70
	public class Rule : Node
	{
		// Token: 0x17000091 RID: 145
		// (get) Token: 0x060002B5 RID: 693 RVA: 0x0000CB54 File Offset: 0x0000AD54
		// (set) Token: 0x060002B6 RID: 694 RVA: 0x0000CB5C File Offset: 0x0000AD5C
		public string Name { get; set; }

		// Token: 0x17000092 RID: 146
		// (get) Token: 0x060002B7 RID: 695 RVA: 0x0000CB65 File Offset: 0x0000AD65
		// (set) Token: 0x060002B8 RID: 696 RVA: 0x0000CB6D File Offset: 0x0000AD6D
		public Node Value { get; set; }

		// Token: 0x17000093 RID: 147
		// (get) Token: 0x060002B9 RID: 697 RVA: 0x0000CB76 File Offset: 0x0000AD76
		// (set) Token: 0x060002BA RID: 698 RVA: 0x0000CB7E File Offset: 0x0000AD7E
		public bool Variable { get; set; }

		// Token: 0x17000094 RID: 148
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0000CB87 File Offset: 0x0000AD87
		// (set) Token: 0x060002BC RID: 700 RVA: 0x0000CB8F File Offset: 0x0000AD8F
		public NodeList PostNameComments { get; set; }

		// Token: 0x17000095 RID: 149
		// (get) Token: 0x060002BD RID: 701 RVA: 0x0000CB98 File Offset: 0x0000AD98
		// (set) Token: 0x060002BE RID: 702 RVA: 0x0000CBA0 File Offset: 0x0000ADA0
		public bool IsSemiColonRequired { get; set; }

		// Token: 0x17000096 RID: 150
		// (get) Token: 0x060002BF RID: 703 RVA: 0x0000CBA9 File Offset: 0x0000ADA9
		// (set) Token: 0x060002C0 RID: 704 RVA: 0x0000CBB1 File Offset: 0x0000ADB1
		public bool Variadic { get; set; }

		// Token: 0x17000097 RID: 151
		// (get) Token: 0x060002C1 RID: 705 RVA: 0x0000CBBA File Offset: 0x0000ADBA
		// (set) Token: 0x060002C2 RID: 706 RVA: 0x0000CBC2 File Offset: 0x0000ADC2
		public bool InterpolatedName { get; set; }

		// Token: 0x060002C3 RID: 707 RVA: 0x0000CBCB File Offset: 0x0000ADCB
		public Rule(string name, Node value)
			: this(name, value, false)
		{
		}

		// Token: 0x060002C4 RID: 708 RVA: 0x0000CBD6 File Offset: 0x0000ADD6
		public Rule(string name, Node value, bool variadic)
		{
			this.Name = name;
			this.Value = value;
			this.Variable = !string.IsNullOrEmpty(name) && name[0] == '@';
			this.IsSemiColonRequired = true;
			this.Variadic = variadic;
		}

		// Token: 0x060002C5 RID: 709 RVA: 0x0000CC18 File Offset: 0x0000AE18
		public override Node Evaluate(Env env)
		{
			env.Rule = this;
			if (this.Value == null)
			{
				throw new ParsingException("No value found for rule " + this.Name, base.Location);
			}
			Rule rule = new Rule(this.EvaluateName(env), this.Value.Evaluate(env)).ReducedFrom<Rule>(new Node[] { this });
			rule.IsSemiColonRequired = this.IsSemiColonRequired;
			rule.PostNameComments = this.PostNameComments;
			env.Rule = null;
			return rule;
		}

		// Token: 0x060002C6 RID: 710 RVA: 0x0000CC98 File Offset: 0x0000AE98
		private string EvaluateName(Env env)
		{
			if (!this.InterpolatedName)
			{
				return this.Name;
			}
			Rule rule = env.FindVariable(this.Name).Evaluate(env) as Rule;
			if (rule == null)
			{
				throw new ParsingException("Invalid variable value for property name", base.Location);
			}
			Keyword keyword = rule.Value as Keyword;
			if (keyword == null)
			{
				throw new ParsingException("Invalid variable value for property name", base.Location);
			}
			return keyword.ToCSS(env);
		}

		// Token: 0x060002C7 RID: 711 RVA: 0x0000CD03 File Offset: 0x0000AF03
		protected override Node CloneCore()
		{
			return new Rule(this.Name, this.Value.Clone(), this.Variadic)
			{
				IsSemiColonRequired = this.IsSemiColonRequired,
				Variable = this.Variable
			};
		}

		// Token: 0x060002C8 RID: 712 RVA: 0x0000CD3C File Offset: 0x0000AF3C
		public override void AppendCSS(Env env)
		{
			if (this.Variable)
			{
				return;
			}
			Node value = this.Value;
			env.Output.Append(this.Name).Append(this.PostNameComments).Append(env.Compress ? ":" : ": ");
			env.Output.Push().Append(value);
			if (env.Compress)
			{
				env.Output.Reset(Regex.Replace(env.Output.ToString(), "(\\s)+", " ").Replace(", ", ","));
			}
			env.Output.PopAndAppend();
			if (this.IsSemiColonRequired)
			{
				env.Output.Append(";");
			}
		}

		// Token: 0x060002C9 RID: 713 RVA: 0x0000CE02 File Offset: 0x0000B002
		public override void Accept(IVisitor visitor)
		{
			this.Value = base.VisitAndReplace<Node>(this.Value, visitor);
		}
	}
}
