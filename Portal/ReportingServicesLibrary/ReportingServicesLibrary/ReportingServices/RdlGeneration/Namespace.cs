using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text.RegularExpressions;
using Microsoft.ReportingServices.Common;

namespace Microsoft.ReportingServices.RdlGeneration
{
	// Token: 0x02000383 RID: 899
	internal class Namespace
	{
		// Token: 0x06001DE2 RID: 7650 RVA: 0x0007A526 File Offset: 0x00078726
		public Namespace(string defaultName, bool clsCompliant)
		{
			this.m_defaultName = defaultName;
			this.m_clsCompliant = clsCompliant;
		}

		// Token: 0x06001DE3 RID: 7651 RVA: 0x0007A54C File Offset: 0x0007874C
		public Namespace(Namespace source)
		{
			this.m_names.AddRange(source.Names);
			this.m_defaultName = source.DefaultName;
			this.m_clsCompliant = source.ClsCompliant;
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06001DE4 RID: 7652 RVA: 0x0007A598 File Offset: 0x00078798
		public string DefaultName
		{
			get
			{
				return this.m_defaultName;
			}
		}

		// Token: 0x1700086F RID: 2159
		// (get) Token: 0x06001DE5 RID: 7653 RVA: 0x0007A5A0 File Offset: 0x000787A0
		public bool ClsCompliant
		{
			get
			{
				return this.m_clsCompliant;
			}
		}

		// Token: 0x17000870 RID: 2160
		// (get) Token: 0x06001DE6 RID: 7654 RVA: 0x0007A5A8 File Offset: 0x000787A8
		public ICollection<string> Names
		{
			get
			{
				return this.m_names;
			}
		}

		// Token: 0x06001DE7 RID: 7655 RVA: 0x0007A5B0 File Offset: 0x000787B0
		public string Add(string candidateName)
		{
			string text = candidateName;
			if (string.IsNullOrEmpty(text))
			{
				text = this.m_defaultName;
			}
			if (this.m_clsCompliant)
			{
				text = Microsoft.ReportingServices.Common.StringUtil.GetClsCompliantIdentifier(text, "x");
			}
			string text2 = text;
			int num = 1;
			Match match = Namespace.NumberSuffixRegex.Match(text);
			if (match.Success)
			{
				text = match.Groups["base"].Value;
				num = int.Parse(match.Groups["num"].Value, CultureInfo.InvariantCulture);
			}
			while (this.m_names.Contains(text2))
			{
				text2 = text + ++num;
			}
			this.m_names.Add(text2);
			return text2;
		}

		// Token: 0x06001DE8 RID: 7656 RVA: 0x0007A65E File Offset: 0x0007885E
		public void Remove(string name)
		{
			if (!this.m_names.Contains(name))
			{
				return;
			}
			this.m_names.Remove(name);
		}

		// Token: 0x04000C93 RID: 3219
		private const string DefaultClsPrefix = "x";

		// Token: 0x04000C94 RID: 3220
		private static readonly Regex NumberSuffixRegex = new Regex("^(?'base'.*)(?'num'[0-9]{1,9})$");

		// Token: 0x04000C95 RID: 3221
		private readonly Bag<string> m_names = new Bag<string>(StringComparer.OrdinalIgnoreCase);

		// Token: 0x04000C96 RID: 3222
		private readonly string m_defaultName;

		// Token: 0x04000C97 RID: 3223
		private readonly bool m_clsCompliant;
	}
}
