using System;
using Microsoft.ReportingServices.ReportProcessing.ReportObjectModel;

namespace Microsoft.ReportingServices.ReportProcessing.ExprHostObjectModel
{
	// Token: 0x02000031 RID: 49
	public abstract class TextBoxExprHost : ReportItemExprHost
	{
		// Token: 0x06000108 RID: 264 RVA: 0x00002885 File Offset: 0x00000A85
		internal void SetTextBox(ReportItem textBox)
		{
			this.m_textBox = textBox;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x06000109 RID: 265 RVA: 0x0000288E File Offset: 0x00000A8E
		public object Value
		{
			get
			{
				return this.m_textBox.Value;
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x0600010A RID: 266 RVA: 0x0000289B File Offset: 0x00000A9B
		internal ReportItem ReportObjectModelTextBox
		{
			get
			{
				return this.m_textBox;
			}
		}

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x0600010B RID: 267 RVA: 0x000028A3 File Offset: 0x00000AA3
		public virtual object ValueExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170000BB RID: 187
		// (get) Token: 0x0600010C RID: 268 RVA: 0x000028A6 File Offset: 0x00000AA6
		public virtual object ToggleImageInitialStateExpr
		{
			get
			{
				return null;
			}
		}

		// Token: 0x04000038 RID: 56
		public const string ValueName = "Value";

		// Token: 0x04000039 RID: 57
		private ReportItem m_textBox;
	}
}
