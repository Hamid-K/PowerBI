using System;
using dotless.Core.Exceptions;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000031 RID: 49
	public class Condition : Node
	{
		// Token: 0x17000064 RID: 100
		// (get) Token: 0x060001D2 RID: 466 RVA: 0x00009D2F File Offset: 0x00007F2F
		// (set) Token: 0x060001D3 RID: 467 RVA: 0x00009D37 File Offset: 0x00007F37
		public Node Left { get; set; }

		// Token: 0x17000065 RID: 101
		// (get) Token: 0x060001D4 RID: 468 RVA: 0x00009D40 File Offset: 0x00007F40
		// (set) Token: 0x060001D5 RID: 469 RVA: 0x00009D48 File Offset: 0x00007F48
		public Node Right { get; set; }

		// Token: 0x17000066 RID: 102
		// (get) Token: 0x060001D6 RID: 470 RVA: 0x00009D51 File Offset: 0x00007F51
		// (set) Token: 0x060001D7 RID: 471 RVA: 0x00009D59 File Offset: 0x00007F59
		public string Operation { get; set; }

		// Token: 0x17000067 RID: 103
		// (get) Token: 0x060001D8 RID: 472 RVA: 0x00009D62 File Offset: 0x00007F62
		// (set) Token: 0x060001D9 RID: 473 RVA: 0x00009D6A File Offset: 0x00007F6A
		public bool Negate { get; set; }

		// Token: 0x17000068 RID: 104
		// (get) Token: 0x060001DA RID: 474 RVA: 0x00009D73 File Offset: 0x00007F73
		// (set) Token: 0x060001DB RID: 475 RVA: 0x00009D7B File Offset: 0x00007F7B
		public bool IsDefault { get; private set; }

		// Token: 0x060001DC RID: 476 RVA: 0x00009D84 File Offset: 0x00007F84
		public Condition(Node left, string operation, Node right, bool negate)
		{
			this.Left = left;
			this.Right = right;
			this.Operation = operation.Trim();
			this.Negate = negate;
		}

		// Token: 0x060001DD RID: 477 RVA: 0x00009DAE File Offset: 0x00007FAE
		protected override Node CloneCore()
		{
			return new Condition(this.Left.Clone(), this.Operation, this.Right.Clone(), this.Negate);
		}

		// Token: 0x060001DE RID: 478 RVA: 0x00009DD7 File Offset: 0x00007FD7
		public override void AppendCSS(Env env)
		{
		}

		// Token: 0x060001DF RID: 479 RVA: 0x00009DDC File Offset: 0x00007FDC
		public override Node Evaluate(Env env)
		{
			Call call = this.Left as Call;
			if (call != null && call.Name == "default")
			{
				this.IsDefault = true;
			}
			Node node = this.Left.Evaluate(env);
			Node node2 = this.Right.Evaluate(env);
			bool flag = this.Evaluate(node, this.Operation, node2);
			if (this.Negate)
			{
				flag = !flag;
			}
			return new BooleanNode(flag);
		}

		// Token: 0x060001E0 RID: 480 RVA: 0x00009E50 File Offset: 0x00008050
		private bool Evaluate(Node lValue, string operation, Node rValue)
		{
			if (operation == "or")
			{
				return Condition.ToBool(lValue) || Condition.ToBool(rValue);
			}
			if (operation == "and")
			{
				return Condition.ToBool(lValue) && Condition.ToBool(rValue);
			}
			IComparable comparable = lValue as IComparable;
			int num;
			if (comparable != null)
			{
				num = comparable.CompareTo(rValue);
			}
			else
			{
				IComparable comparable2 = rValue as IComparable;
				if (comparable2 == null)
				{
					throw new ParsingException("Cannot compare objects in mixin guard condition", base.Location);
				}
				num = comparable2.CompareTo(lValue);
				if (num < 0)
				{
					num = 1;
				}
				else if (num > 0)
				{
					num = -1;
				}
			}
			if (num == 0)
			{
				return operation == "=" || operation == ">=" || operation == "=<";
			}
			if (num < 0)
			{
				return operation == "<" || operation == "=<";
			}
			if (num > 0)
			{
				return operation == ">" || operation == ">=";
			}
			throw new ParsingException("C# compiler can't work out it is impossible to get here", base.Location);
		}

		// Token: 0x060001E1 RID: 481 RVA: 0x00009F5B File Offset: 0x0000815B
		public bool Passes(Env env)
		{
			return Condition.ToBool(this.Evaluate(env));
		}

		// Token: 0x060001E2 RID: 482 RVA: 0x00009F6C File Offset: 0x0000816C
		private static bool ToBool(Node node)
		{
			BooleanNode booleanNode = node as BooleanNode;
			return booleanNode != null && booleanNode.Value;
		}

		// Token: 0x060001E3 RID: 483 RVA: 0x00009F8B File Offset: 0x0000818B
		public override void Accept(IVisitor visitor)
		{
			this.Left = base.VisitAndReplace<Node>(this.Left, visitor);
			this.Right = base.VisitAndReplace<Node>(this.Right, visitor);
		}
	}
}
