using System;

namespace Microsoft.DataIntegration.FuzzyMatchingCommon.Diagnostics
{
	// Token: 0x0200004D RID: 77
	[AttributeUsage(384, AllowMultiple = false)]
	[Serializable]
	public class StatisticAttribute : Attribute
	{
		// Token: 0x06000264 RID: 612 RVA: 0x00013C81 File Offset: 0x00011E81
		public StatisticAttribute()
		{
		}

		// Token: 0x06000265 RID: 613 RVA: 0x00013C89 File Offset: 0x00011E89
		public StatisticAttribute(bool ignore)
		{
			this.Ignore = ignore;
		}

		// Token: 0x06000266 RID: 614 RVA: 0x00013C98 File Offset: 0x00011E98
		public StatisticAttribute(int indentLevel)
		{
			this.IndentLevel = indentLevel;
		}

		// Token: 0x06000267 RID: 615 RVA: 0x00013CA7 File Offset: 0x00011EA7
		public StatisticAttribute(int indentLevel, bool insertNewLine)
		{
			this.IndentLevel = indentLevel;
			this.InsertNewLine = insertNewLine;
		}

		// Token: 0x1700005F RID: 95
		// (get) Token: 0x06000268 RID: 616 RVA: 0x00013CBD File Offset: 0x00011EBD
		// (set) Token: 0x06000269 RID: 617 RVA: 0x00013CC5 File Offset: 0x00011EC5
		public int IndentLevel
		{
			get
			{
				return this.m_indentLevel;
			}
			set
			{
				this.m_indentLevel = value;
			}
		}

		// Token: 0x17000060 RID: 96
		// (get) Token: 0x0600026A RID: 618 RVA: 0x00013CCE File Offset: 0x00011ECE
		// (set) Token: 0x0600026B RID: 619 RVA: 0x00013CD6 File Offset: 0x00011ED6
		public bool InsertNewLine
		{
			get
			{
				return this.m_insertNewLine;
			}
			set
			{
				this.m_insertNewLine = value;
			}
		}

		// Token: 0x17000061 RID: 97
		// (get) Token: 0x0600026C RID: 620 RVA: 0x00013CDF File Offset: 0x00011EDF
		// (set) Token: 0x0600026D RID: 621 RVA: 0x00013CE7 File Offset: 0x00011EE7
		public bool Ignore
		{
			get
			{
				return this.m_ignore;
			}
			set
			{
				this.m_ignore = value;
			}
		}

		// Token: 0x04000069 RID: 105
		public static readonly StatisticAttribute Empty = new StatisticAttribute
		{
			Ignore = true
		};

		// Token: 0x0400006A RID: 106
		private int m_indentLevel;

		// Token: 0x0400006B RID: 107
		private bool m_insertNewLine;

		// Token: 0x0400006C RID: 108
		private bool m_ignore;
	}
}
