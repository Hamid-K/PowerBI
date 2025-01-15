using System;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x020000B5 RID: 181
	public abstract class TextRunExprHost : StyleExprHost
	{
		// Token: 0x06000411 RID: 1041 RVA: 0x00003949 File Offset: 0x00001B49
		internal void SetTextRun(TextRun textRun)
		{
			this.m_textRun = textRun;
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000412 RID: 1042 RVA: 0x00003952 File Offset: 0x00001B52
		public object Value
		{
			get
			{
				return this.m_textRun.Value;
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000413 RID: 1043 RVA: 0x0000395F File Offset: 0x00001B5F
		public virtual object LabelExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000414 RID: 1044 RVA: 0x00003962 File Offset: 0x00001B62
		public virtual object ValueExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000415 RID: 1045 RVA: 0x00003965 File Offset: 0x00001B65
		public virtual object ToolTipExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000416 RID: 1046 RVA: 0x00003968 File Offset: 0x00001B68
		public virtual object MarkupTypeExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x0400012E RID: 302
		public ActionInfoExprHost ActionInfoHost;

		// Token: 0x0400012F RID: 303
		private TextRun m_textRun;
	}
}
