using System;

namespace Microsoft.Identity.Client
{
	// Token: 0x02000177 RID: 375
	public struct Prompt
	{
		// Token: 0x170003BC RID: 956
		// (get) Token: 0x0600125A RID: 4698 RVA: 0x0003EA78 File Offset: 0x0003CC78
		internal readonly string PromptValue { get; }

		// Token: 0x0600125B RID: 4699 RVA: 0x0003EA80 File Offset: 0x0003CC80
		private Prompt(string promptValue)
		{
			this.PromptValue = promptValue;
		}

		// Token: 0x0600125C RID: 4700 RVA: 0x0003EA8C File Offset: 0x0003CC8C
		public override bool Equals(object obj)
		{
			if (obj is Prompt)
			{
				Prompt prompt = (Prompt)obj;
				return this == prompt;
			}
			return false;
		}

		// Token: 0x0600125D RID: 4701 RVA: 0x0003EAB6 File Offset: 0x0003CCB6
		public override int GetHashCode()
		{
			return this.PromptValue.GetHashCode();
		}

		// Token: 0x0600125E RID: 4702 RVA: 0x0003EAC3 File Offset: 0x0003CCC3
		public static bool operator ==(Prompt x, Prompt y)
		{
			return x.PromptValue == y.PromptValue;
		}

		// Token: 0x0600125F RID: 4703 RVA: 0x0003EAD8 File Offset: 0x0003CCD8
		public static bool operator !=(Prompt x, Prompt y)
		{
			return !(x == y);
		}

		// Token: 0x040006C8 RID: 1736
		public static readonly Prompt SelectAccount = new Prompt("select_account");

		// Token: 0x040006C9 RID: 1737
		public static readonly Prompt ForceLogin = new Prompt("login");

		// Token: 0x040006CA RID: 1738
		public static readonly Prompt Consent = new Prompt("consent");

		// Token: 0x040006CB RID: 1739
		public static readonly Prompt NoPrompt = new Prompt("no_prompt");

		// Token: 0x040006CC RID: 1740
		public static readonly Prompt Create = new Prompt("create");

		// Token: 0x040006CD RID: 1741
		public static readonly Prompt Never = new Prompt("attempt_none");

		// Token: 0x040006CE RID: 1742
		internal static readonly Prompt NotSpecified = new Prompt("not_specified");
	}
}
