using System;
using dotless.Core.Parser.Infrastructure;
using dotless.Core.Parser.Infrastructure.Nodes;

namespace dotless.Core.Parser.Tree
{
	// Token: 0x02000030 RID: 48
	public class Comment : Node
	{
		// Token: 0x1700005F RID: 95
		// (get) Token: 0x060001C5 RID: 453 RVA: 0x00009BF7 File Offset: 0x00007DF7
		// (set) Token: 0x060001C6 RID: 454 RVA: 0x00009BFF File Offset: 0x00007DFF
		public string Value { get; set; }

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x060001C7 RID: 455 RVA: 0x00009C08 File Offset: 0x00007E08
		// (set) Token: 0x060001C8 RID: 456 RVA: 0x00009C10 File Offset: 0x00007E10
		public bool IsValidCss { get; set; }

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x060001C9 RID: 457 RVA: 0x00009C19 File Offset: 0x00007E19
		// (set) Token: 0x060001CA RID: 458 RVA: 0x00009C21 File Offset: 0x00007E21
		public bool IsSpecialCss { get; set; }

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001CB RID: 459 RVA: 0x00009C2A File Offset: 0x00007E2A
		// (set) Token: 0x060001CC RID: 460 RVA: 0x00009C32 File Offset: 0x00007E32
		public bool IsPreSelectorComment { get; set; }

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001CD RID: 461 RVA: 0x00009C3B File Offset: 0x00007E3B
		// (set) Token: 0x060001CE RID: 462 RVA: 0x00009C43 File Offset: 0x00007E43
		private bool IsCSSHack { get; set; }

		// Token: 0x060001CF RID: 463 RVA: 0x00009C4C File Offset: 0x00007E4C
		public Comment(string value)
		{
			this.Value = value;
			this.IsValidCss = !value.StartsWith("//");
			this.IsSpecialCss = value.StartsWith("/**") || value.StartsWith("/*!");
			this.IsCSSHack = value == "/**/" || value == "/*\\*/";
		}

		// Token: 0x060001D0 RID: 464 RVA: 0x00009CBC File Offset: 0x00007EBC
		protected override Node CloneCore()
		{
			return new Comment(this.Value);
		}

		// Token: 0x060001D1 RID: 465 RVA: 0x00009CCC File Offset: 0x00007ECC
		public override void AppendCSS(Env env)
		{
			if (base.IsReference || env.IsCommentSilent(this.IsValidCss, this.IsCSSHack, this.IsSpecialCss))
			{
				return;
			}
			env.Output.Append(this.Value);
			if (!this.IsCSSHack && this.IsPreSelectorComment)
			{
				env.Output.Append("\n");
			}
		}
	}
}
