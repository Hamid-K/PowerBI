using System;
using System.Text.RegularExpressions;

namespace Microsoft.Fabric.Common
{
	// Token: 0x0200040D RID: 1037
	internal class PatternFunc : UnaryFunc
	{
		// Token: 0x06002412 RID: 9234 RVA: 0x0006E98B File Offset: 0x0006CB8B
		public PatternFunc(string pattern)
		{
			this.m_pattern = new Regex(pattern, RegexOptions.Compiled);
		}

		// Token: 0x06002413 RID: 9235 RVA: 0x0006E9A0 File Offset: 0x0006CBA0
		protected override object UnaryInvoke(object arg)
		{
			string text = arg as string;
			if (text == null)
			{
				if (arg == null)
				{
					return false;
				}
				text = arg.ToString();
			}
			return this.m_pattern.IsMatch(text);
		}

		// Token: 0x06002414 RID: 9236 RVA: 0x0006E9D9 File Offset: 0x0006CBD9
		public override string ToString()
		{
			return "pattern:" + this.m_pattern;
		}

		// Token: 0x04001650 RID: 5712
		private Regex m_pattern;
	}
}
