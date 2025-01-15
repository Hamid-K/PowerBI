using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel
{
	// Token: 0x02000060 RID: 96
	public abstract class TextBoxExprHost : ReportItemExprHost
	{
		// Token: 0x1700015B RID: 347
		// (get) Token: 0x060001FA RID: 506 RVA: 0x0000305B File Offset: 0x0000125B
		internal IList<ParagraphExprHost> ParagraphHostsRemotable
		{
			get
			{
				return this.m_paragraphHostsRemotable;
			}
		}

		// Token: 0x060001FB RID: 507 RVA: 0x00003063 File Offset: 0x00001263
		internal void SetTextBox(ReportItem textBox)
		{
			this.m_textBox = textBox;
		}

		// Token: 0x1700015C RID: 348
		// (get) Token: 0x060001FC RID: 508 RVA: 0x0000306C File Offset: 0x0000126C
		public object Value
		{
			get
			{
				return this.m_textBox.Value;
			}
		}

		// Token: 0x1700015D RID: 349
		// (get) Token: 0x060001FD RID: 509 RVA: 0x00003079 File Offset: 0x00001279
		internal ReportItem ReportObjectModelTextBox
		{
			get
			{
				return this.m_textBox;
			}
		}

		// Token: 0x1700015E RID: 350
		// (get) Token: 0x060001FE RID: 510 RVA: 0x00003081 File Offset: 0x00001281
		public virtual object ValueExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700015F RID: 351
		// (get) Token: 0x060001FF RID: 511 RVA: 0x00003084 File Offset: 0x00001284
		public virtual object ToggleImageInitialStateExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x040000A1 RID: 161
		[CLSCompliant(false)]
		protected IList<ParagraphExprHost> m_paragraphHostsRemotable;

		// Token: 0x040000A2 RID: 162
		public const string ValueName = "Value";

		// Token: 0x040000A3 RID: 163
		private ReportItem m_textBox;
	}
}
