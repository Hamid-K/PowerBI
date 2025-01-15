using System;
using System.Collections.Generic;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Explanations.Default
{
	// Token: 0x020019C6 RID: 6598
	public class EmptyExplanationMeta : IExplanationMeta
	{
		// Token: 0x170023CE RID: 9166
		// (get) Token: 0x0600D76A RID: 55146 RVA: 0x0008E3C3 File Offset: 0x0008C5C3
		public string Key
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x170023CF RID: 9167
		// (get) Token: 0x0600D76B RID: 55147 RVA: 0x002DC561 File Offset: 0x002DA761
		public IDictionary<string, string> Replacements
		{
			get
			{
				return new Dictionary<string, string>();
			}
		}

		// Token: 0x170023D0 RID: 9168
		// (get) Token: 0x0600D76C RID: 55148 RVA: 0x0008653C File Offset: 0x0008473C
		public IReadOnlyList<string> OrderedReplacements
		{
			get
			{
				return new List<string>();
			}
		}

		// Token: 0x170023D1 RID: 9169
		// (get) Token: 0x0600D76D RID: 55149 RVA: 0x0008E3C3 File Offset: 0x0008C5C3
		public string Text
		{
			get
			{
				return string.Empty;
			}
		}

		// Token: 0x0600D76E RID: 55150 RVA: 0x0008E3C3 File Offset: 0x0008C5C3
		public override string ToString()
		{
			return string.Empty;
		}
	}
}
