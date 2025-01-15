using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x0200003D RID: 61
	public class Keyword : Node, IComparable
	{
		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000241 RID: 577 RVA: 0x0000AE63 File Offset: 0x00009063
		// (set) Token: 0x06000242 RID: 578 RVA: 0x0000AE6B File Offset: 0x0000906B
		public string Value { get; set; }

		// Token: 0x06000243 RID: 579 RVA: 0x0000AE74 File Offset: 0x00009074
		public Keyword(string value)
		{
			this.Value = value;
		}

		// Token: 0x06000244 RID: 580 RVA: 0x0000AE83 File Offset: 0x00009083
		public override Node Evaluate(Env env)
		{
			return (Color.GetColorFromKeyword(this.Value) ?? this).ReducedFrom<Node>(new Node[] { this });
		}

		// Token: 0x06000245 RID: 581 RVA: 0x0000AEA4 File Offset: 0x000090A4
		protected override Node CloneCore()
		{
			return new Keyword(this.Value);
		}

		// Token: 0x06000246 RID: 582 RVA: 0x0000AEB1 File Offset: 0x000090B1
		public override void AppendCSS(Env env)
		{
			env.Output.Append(this.Value);
		}

		// Token: 0x06000247 RID: 583 RVA: 0x0000AEC5 File Offset: 0x000090C5
		public override string ToString()
		{
			return this.Value;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x0000AECD File Offset: 0x000090CD
		public int CompareTo(object obj)
		{
			if (obj == null)
			{
				return -1;
			}
			return obj.ToString().CompareTo(this.ToString());
		}
	}
}
