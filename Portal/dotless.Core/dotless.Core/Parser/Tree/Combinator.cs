using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x0200002F RID: 47
	public class Combinator : Node
	{
		// Token: 0x1700005E RID: 94
		// (get) Token: 0x060001BF RID: 447 RVA: 0x00009AF7 File Offset: 0x00007CF7
		// (set) Token: 0x060001C0 RID: 448 RVA: 0x00009AFF File Offset: 0x00007CFF
		public string Value { get; set; }

		// Token: 0x060001C1 RID: 449 RVA: 0x00009B08 File Offset: 0x00007D08
		public Combinator(string value)
		{
			if (string.IsNullOrEmpty(value))
			{
				this.Value = "";
				return;
			}
			if (value == " ")
			{
				this.Value = " ";
				return;
			}
			this.Value = value.Trim();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00009B54 File Offset: 0x00007D54
		protected override Node CloneCore()
		{
			return new Combinator(this.Value);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00009B61 File Offset: 0x00007D61
		public override void AppendCSS(Env env)
		{
			env.Output.Append(this.GetValue(env));
		}

		// Token: 0x060001C4 RID: 452 RVA: 0x00009B78 File Offset: 0x00007D78
		private string GetValue(Env env)
		{
			string value = this.Value;
			if (!(value == "+"))
			{
				if (!(value == "~"))
				{
					if (!(value == ">"))
					{
						return this.Value;
					}
					if (!env.Compress)
					{
						return " > ";
					}
					return ">";
				}
				else
				{
					if (!env.Compress)
					{
						return " ~ ";
					}
					return "~";
				}
			}
			else
			{
				if (!env.Compress)
				{
					return " + ";
				}
				return "+";
			}
		}
	}
}
