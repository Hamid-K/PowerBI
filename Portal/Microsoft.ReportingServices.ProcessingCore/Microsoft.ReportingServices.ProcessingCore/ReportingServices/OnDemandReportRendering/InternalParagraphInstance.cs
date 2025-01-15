using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200031F RID: 799
	internal sealed class InternalParagraphInstance : ParagraphInstance
	{
		// Token: 0x06001DAE RID: 7598 RVA: 0x00074C47 File Offset: 0x00072E47
		internal InternalParagraphInstance(Paragraph paragraphDef)
			: base(paragraphDef)
		{
		}

		// Token: 0x06001DAF RID: 7599 RVA: 0x00074C50 File Offset: 0x00072E50
		internal InternalParagraphInstance(ReportElement reportElementDef)
			: base(reportElementDef)
		{
		}

		// Token: 0x170010B1 RID: 4273
		// (get) Token: 0x06001DB0 RID: 7600 RVA: 0x00074C59 File Offset: 0x00072E59
		public override string UniqueName
		{
			get
			{
				if (this.m_uniqueName == null)
				{
					this.m_uniqueName = InstancePathItem.GenerateUniqueNameString(this.ParagraphDef.IDString, this.ParagraphDef.InstancePath);
				}
				return this.m_uniqueName;
			}
		}

		// Token: 0x170010B2 RID: 4274
		// (get) Token: 0x06001DB1 RID: 7601 RVA: 0x00074C8A File Offset: 0x00072E8A
		public override ReportSize LeftIndent
		{
			get
			{
				if (this.m_leftIndent == null)
				{
					this.m_leftIndent = this.GetLeftIndent(true);
				}
				return this.m_leftIndent;
			}
		}

		// Token: 0x06001DB2 RID: 7602 RVA: 0x00074CA8 File Offset: 0x00072EA8
		internal ReportSize GetLeftIndent(bool constantUsable)
		{
			ExpressionInfo leftIndent = this.ParagraphDef.LeftIndent;
			if (leftIndent != null)
			{
				if (leftIndent.IsExpression)
				{
					return new ReportSize(this.ParagraphDef.EvaluateLeftIndent(this.ReportScopeInstance, this.m_reportElementDef.RenderingContext.OdpContext), false, false);
				}
				if (constantUsable)
				{
					return new ReportSize(leftIndent.StringValue, false, false);
				}
			}
			return null;
		}

		// Token: 0x170010B3 RID: 4275
		// (get) Token: 0x06001DB3 RID: 7603 RVA: 0x00074D07 File Offset: 0x00072F07
		public override ReportSize RightIndent
		{
			get
			{
				if (this.m_rightIndent == null)
				{
					this.m_rightIndent = this.GetRightIndent(true);
				}
				return this.m_rightIndent;
			}
		}

		// Token: 0x06001DB4 RID: 7604 RVA: 0x00074D24 File Offset: 0x00072F24
		internal ReportSize GetRightIndent(bool constantUsable)
		{
			ExpressionInfo rightIndent = this.ParagraphDef.RightIndent;
			if (rightIndent != null)
			{
				if (rightIndent.IsExpression)
				{
					return new ReportSize(this.ParagraphDef.EvaluateRightIndent(this.ReportScopeInstance, this.m_reportElementDef.RenderingContext.OdpContext), false, false);
				}
				if (constantUsable)
				{
					return new ReportSize(rightIndent.StringValue, false, false);
				}
			}
			return null;
		}

		// Token: 0x170010B4 RID: 4276
		// (get) Token: 0x06001DB5 RID: 7605 RVA: 0x00074D83 File Offset: 0x00072F83
		public override ReportSize HangingIndent
		{
			get
			{
				if (this.m_hangingIndent == null)
				{
					this.m_hangingIndent = this.GetHangingIndent(true);
				}
				return this.m_hangingIndent;
			}
		}

		// Token: 0x06001DB6 RID: 7606 RVA: 0x00074DA0 File Offset: 0x00072FA0
		internal ReportSize GetHangingIndent(bool constantUsable)
		{
			ExpressionInfo hangingIndent = this.ParagraphDef.HangingIndent;
			if (hangingIndent != null)
			{
				if (hangingIndent.IsExpression)
				{
					return new ReportSize(this.ParagraphDef.EvaluateHangingIndent(this.ReportScopeInstance, this.m_reportElementDef.RenderingContext.OdpContext), false, true);
				}
				if (constantUsable)
				{
					return new ReportSize(hangingIndent.StringValue, false, true);
				}
			}
			return null;
		}

		// Token: 0x170010B5 RID: 4277
		// (get) Token: 0x06001DB7 RID: 7607 RVA: 0x00074E00 File Offset: 0x00073000
		public override ListStyle ListStyle
		{
			get
			{
				if (this.m_listStyle == null)
				{
					ExpressionInfo listStyle = this.ParagraphDef.ListStyle;
					if (listStyle != null)
					{
						if (listStyle.IsExpression)
						{
							this.m_listStyle = new ListStyle?(RichTextHelpers.TranslateListStyle(this.ParagraphDef.EvaluateListStyle(this.ReportScopeInstance, this.m_reportElementDef.RenderingContext.OdpContext)));
						}
						else
						{
							this.m_listStyle = new ListStyle?(RichTextHelpers.TranslateListStyle(listStyle.StringValue));
						}
					}
					else
					{
						this.m_listStyle = new ListStyle?(ListStyle.None);
					}
				}
				return this.m_listStyle.Value;
			}
		}

		// Token: 0x170010B6 RID: 4278
		// (get) Token: 0x06001DB8 RID: 7608 RVA: 0x00074E94 File Offset: 0x00073094
		public override int ListLevel
		{
			get
			{
				if (this.m_listLevel == null)
				{
					ExpressionInfo listLevel = this.ParagraphDef.ListLevel;
					if (listLevel != null)
					{
						if (listLevel.IsExpression)
						{
							this.m_listLevel = this.ParagraphDef.EvaluateListLevel(this.ReportScopeInstance, this.m_reportElementDef.RenderingContext.OdpContext);
						}
						else
						{
							this.m_listLevel = new int?(listLevel.IntValue);
						}
					}
					if (this.m_listLevel == null)
					{
						this.m_listLevel = new int?((this.ListStyle > ListStyle.None) ? 1 : 0);
					}
				}
				return this.m_listLevel.Value;
			}
		}

		// Token: 0x170010B7 RID: 4279
		// (get) Token: 0x06001DB9 RID: 7609 RVA: 0x00074F2B File Offset: 0x0007312B
		public override ReportSize SpaceBefore
		{
			get
			{
				if (this.m_spaceBefore == null)
				{
					this.m_spaceBefore = this.GetSpaceBefore(true);
				}
				return this.m_spaceBefore;
			}
		}

		// Token: 0x06001DBA RID: 7610 RVA: 0x00074F48 File Offset: 0x00073148
		internal ReportSize GetSpaceBefore(bool constantUsable)
		{
			ExpressionInfo spaceBefore = this.ParagraphDef.SpaceBefore;
			if (spaceBefore != null)
			{
				if (spaceBefore.IsExpression)
				{
					return new ReportSize(this.ParagraphDef.EvaluateSpaceBefore(this.ReportScopeInstance, this.m_reportElementDef.RenderingContext.OdpContext), false, false);
				}
				if (constantUsable)
				{
					return new ReportSize(spaceBefore.StringValue, false, false);
				}
			}
			return null;
		}

		// Token: 0x170010B8 RID: 4280
		// (get) Token: 0x06001DBB RID: 7611 RVA: 0x00074FA7 File Offset: 0x000731A7
		public override ReportSize SpaceAfter
		{
			get
			{
				if (this.m_spaceAfter == null)
				{
					this.m_spaceAfter = this.GetSpaceAfter(true);
				}
				return this.m_spaceAfter;
			}
		}

		// Token: 0x06001DBC RID: 7612 RVA: 0x00074FC4 File Offset: 0x000731C4
		internal ReportSize GetSpaceAfter(bool constantUsable)
		{
			ExpressionInfo spaceAfter = this.ParagraphDef.SpaceAfter;
			if (spaceAfter != null)
			{
				if (spaceAfter.IsExpression)
				{
					return new ReportSize(this.ParagraphDef.EvaluateSpaceAfter(this.ReportScopeInstance, this.m_reportElementDef.RenderingContext.OdpContext), false, false);
				}
				if (constantUsable)
				{
					return new ReportSize(spaceAfter.StringValue, false, false);
				}
			}
			return null;
		}

		// Token: 0x170010B9 RID: 4281
		// (get) Token: 0x06001DBD RID: 7613 RVA: 0x00075023 File Offset: 0x00073223
		internal Paragraph ParagraphDef
		{
			get
			{
				return ((InternalParagraph)this.m_reportElementDef).ParagraphDef;
			}
		}

		// Token: 0x170010BA RID: 4282
		// (get) Token: 0x06001DBE RID: 7614 RVA: 0x00075035 File Offset: 0x00073235
		public override bool IsCompiled
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001DBF RID: 7615 RVA: 0x00075038 File Offset: 0x00073238
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_leftIndent = null;
			this.m_rightIndent = null;
			this.m_hangingIndent = null;
			this.m_spaceBefore = null;
			this.m_spaceAfter = null;
			this.m_listStyle = null;
			this.m_listLevel = null;
		}

		// Token: 0x04000F61 RID: 3937
		private ReportSize m_leftIndent;

		// Token: 0x04000F62 RID: 3938
		private ReportSize m_rightIndent;

		// Token: 0x04000F63 RID: 3939
		private ReportSize m_hangingIndent;

		// Token: 0x04000F64 RID: 3940
		private ReportSize m_spaceBefore;

		// Token: 0x04000F65 RID: 3941
		private ReportSize m_spaceAfter;

		// Token: 0x04000F66 RID: 3942
		private ListStyle? m_listStyle;

		// Token: 0x04000F67 RID: 3943
		private int? m_listLevel;
	}
}
