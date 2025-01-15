using System;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000637 RID: 1591
	[Serializable]
	public class PageProperties
	{
		// Token: 0x06005706 RID: 22278 RVA: 0x0016EEA4 File Offset: 0x0016D0A4
		public PageProperties(double pageHeight, double pageWidth, double topMargin, double bottomMargin, double leftMargin, double rightMargin)
		{
			this.m_pageHeight = pageHeight;
			this.m_pageWidth = pageWidth;
			this.m_topMargin = topMargin;
			this.m_bottomMargin = bottomMargin;
			this.m_leftMargin = leftMargin;
			this.m_rightMargin = rightMargin;
		}

		// Token: 0x06005707 RID: 22279 RVA: 0x0016EF40 File Offset: 0x0016D140
		protected PageProperties()
		{
		}

		// Token: 0x17001FB3 RID: 8115
		// (get) Token: 0x06005708 RID: 22280 RVA: 0x0016EFAD File Offset: 0x0016D1AD
		public double PageHeight
		{
			get
			{
				return this.m_pageHeight;
			}
		}

		// Token: 0x17001FB4 RID: 8116
		// (get) Token: 0x06005709 RID: 22281 RVA: 0x0016EFB5 File Offset: 0x0016D1B5
		public double PageWidth
		{
			get
			{
				return this.m_pageWidth;
			}
		}

		// Token: 0x17001FB5 RID: 8117
		// (get) Token: 0x0600570A RID: 22282 RVA: 0x0016EFBD File Offset: 0x0016D1BD
		public double TopMargin
		{
			get
			{
				return this.m_topMargin;
			}
		}

		// Token: 0x17001FB6 RID: 8118
		// (get) Token: 0x0600570B RID: 22283 RVA: 0x0016EFC5 File Offset: 0x0016D1C5
		public double BottomMargin
		{
			get
			{
				return this.m_bottomMargin;
			}
		}

		// Token: 0x17001FB7 RID: 8119
		// (get) Token: 0x0600570C RID: 22284 RVA: 0x0016EFCD File Offset: 0x0016D1CD
		public double LeftMargin
		{
			get
			{
				return this.m_leftMargin;
			}
		}

		// Token: 0x17001FB8 RID: 8120
		// (get) Token: 0x0600570D RID: 22285 RVA: 0x0016EFD5 File Offset: 0x0016D1D5
		public double RightMargin
		{
			get
			{
				return this.m_rightMargin;
			}
		}

		// Token: 0x04002DF7 RID: 11767
		protected double m_pageHeight = 279.4;

		// Token: 0x04002DF8 RID: 11768
		protected double m_pageWidth = 215.89999999999998;

		// Token: 0x04002DF9 RID: 11769
		protected double m_topMargin = 12.7;

		// Token: 0x04002DFA RID: 11770
		protected double m_bottomMargin = 12.7;

		// Token: 0x04002DFB RID: 11771
		protected double m_leftMargin = 12.7;

		// Token: 0x04002DFC RID: 11772
		protected double m_rightMargin = 12.7;
	}
}
