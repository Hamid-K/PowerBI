using System;
using dotless.Core.Exceptions;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;
using dotless.Core.Plugins;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000042 RID: 66
	public class Operation : Node
	{
		// Token: 0x1700008A RID: 138
		// (get) Token: 0x0600028D RID: 653 RVA: 0x0000C4C3 File Offset: 0x0000A6C3
		// (set) Token: 0x0600028E RID: 654 RVA: 0x0000C4CB File Offset: 0x0000A6CB
		public Node First { get; set; }

		// Token: 0x1700008B RID: 139
		// (get) Token: 0x0600028F RID: 655 RVA: 0x0000C4D4 File Offset: 0x0000A6D4
		// (set) Token: 0x06000290 RID: 656 RVA: 0x0000C4DC File Offset: 0x0000A6DC
		public Node Second { get; set; }

		// Token: 0x1700008C RID: 140
		// (get) Token: 0x06000291 RID: 657 RVA: 0x0000C4E5 File Offset: 0x0000A6E5
		// (set) Token: 0x06000292 RID: 658 RVA: 0x0000C4ED File Offset: 0x0000A6ED
		public string Operator { get; set; }

		// Token: 0x06000293 RID: 659 RVA: 0x0000C4F6 File Offset: 0x0000A6F6
		public Operation(string op, Node first, Node second)
		{
			this.First = first;
			this.Second = second;
			this.Operator = op.Trim();
		}

		// Token: 0x06000294 RID: 660 RVA: 0x0000C518 File Offset: 0x0000A718
		protected override Node CloneCore()
		{
			return new Operation(this.Operator, this.First.Clone(), this.Second.Clone());
		}

		// Token: 0x06000295 RID: 661 RVA: 0x0000C53C File Offset: 0x0000A73C
		public override Node Evaluate(Env env)
		{
			Node node = this.First.Evaluate(env);
			Node node2 = this.Second.Evaluate(env);
			if (node is Number && node2 is Color)
			{
				if (!(this.Operator == "*") && !(this.Operator == "+"))
				{
					throw new ParsingException("Can't substract or divide a color from a number", base.Location);
				}
				Node node3 = node2;
				node2 = node;
				node = node3;
			}
			Node node4;
			try
			{
				IOperable operable = node as IOperable;
				if (operable == null)
				{
					throw new ParsingException(string.Format("Cannot apply operator {0} to the left hand side: {1}", this.Operator, node.ToCSS(env)), base.Location);
				}
				node4 = operable.Operate(this, node2).ReducedFrom<Node>(new Node[] { this });
			}
			catch (DivideByZeroException ex)
			{
				throw new ParsingException(ex, base.Location);
			}
			catch (InvalidOperationException ex2)
			{
				throw new ParsingException(ex2, base.Location);
			}
			return node4;
		}

		// Token: 0x06000296 RID: 662 RVA: 0x0000C62C File Offset: 0x0000A82C
		public static double Operate(string op, double first, double second)
		{
			if (op == "/" && second == 0.0)
			{
				throw new DivideByZeroException();
			}
			if (op == "+")
			{
				return first + second;
			}
			if (op == "-")
			{
				return first - second;
			}
			if (op == "*")
			{
				return first * second;
			}
			if (!(op == "/"))
			{
				throw new InvalidOperationException("Unknown operator");
			}
			return first / second;
		}

		// Token: 0x06000297 RID: 663 RVA: 0x0000C6A8 File Offset: 0x0000A8A8
		public override void Accept(IVisitor visitor)
		{
			this.First = base.VisitAndReplace<Node>(this.First, visitor);
			this.Second = base.VisitAndReplace<Node>(this.Second, visitor);
		}
	}
}
