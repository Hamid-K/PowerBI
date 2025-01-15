using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000313 RID: 787
	public sealed class TextBox : Microsoft.ReportingServices.OnDemandReportRendering.ReportItem, IROMActionOwner
	{
		// Token: 0x06001D1A RID: 7450 RVA: 0x00073408 File Offset: 0x00071608
		internal TextBox(IReportScope reportScope, IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, Microsoft.ReportingServices.ReportIntermediateFormat.TextBox reportItemDef, RenderingContext renderingContext)
			: base(reportScope, parentDefinitionPath, indexIntoParentCollectionDef, reportItemDef, renderingContext)
		{
			this.m_textBoxDef = reportItemDef;
		}

		// Token: 0x06001D1B RID: 7451 RVA: 0x0007341F File Offset: 0x0007161F
		internal TextBox(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, bool inSubtotal, Microsoft.ReportingServices.ReportRendering.TextBox renderTextBox, RenderingContext renderingContext)
			: base(parentDefinitionPath, indexIntoParentCollectionDef, inSubtotal, renderTextBox, renderingContext)
		{
			this.m_renderTextBox = renderTextBox;
		}

		// Token: 0x17001045 RID: 4165
		// (get) Token: 0x06001D1C RID: 7452 RVA: 0x00073436 File Offset: 0x00071636
		public override Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					if (this.m_style == null)
					{
						this.m_style = new TextBoxFilteredStyle(this.RenderReportItem, base.RenderingContext, this.UseRenderStyle);
					}
					return this.m_style;
				}
				return base.Style;
			}
		}

		// Token: 0x17001046 RID: 4166
		// (get) Token: 0x06001D1D RID: 7453 RVA: 0x00073472 File Offset: 0x00071672
		public bool CanScrollVertically
		{
			get
			{
				return !this.m_isOldSnapshot && this.m_textBoxDef.CanScrollVertically;
			}
		}

		// Token: 0x17001047 RID: 4167
		// (get) Token: 0x06001D1E RID: 7454 RVA: 0x00073489 File Offset: 0x00071689
		public bool CanGrow
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderTextBox.CanGrow;
				}
				return this.m_textBoxDef.CanGrow;
			}
		}

		// Token: 0x17001048 RID: 4168
		// (get) Token: 0x06001D1F RID: 7455 RVA: 0x000734AA File Offset: 0x000716AA
		public bool CanShrink
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderTextBox.CanShrink;
				}
				return this.m_textBoxDef.CanShrink;
			}
		}

		// Token: 0x17001049 RID: 4169
		// (get) Token: 0x06001D20 RID: 7456 RVA: 0x000734CB File Offset: 0x000716CB
		public bool HideDuplicates
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderTextBox.HideDuplicates;
				}
				return this.m_textBoxDef.IsSimple && this.m_textBoxDef.HideDuplicates != null;
			}
		}

		// Token: 0x1700104A RID: 4170
		// (get) Token: 0x06001D21 RID: 7457 RVA: 0x000734FE File Offset: 0x000716FE
		public StructureTypeOverwriteType StructureTypeOverwrite
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return StructureTypeOverwriteType.None;
				}
				return this.m_textBoxDef.StructureTypeOverwrite;
			}
		}

		// Token: 0x1700104B RID: 4171
		// (get) Token: 0x06001D22 RID: 7458 RVA: 0x00073515 File Offset: 0x00071715
		public string UniqueName
		{
			get
			{
				return this.m_reportItemDef.UniqueName;
			}
		}

		// Token: 0x1700104C RID: 4172
		// (get) Token: 0x06001D23 RID: 7459 RVA: 0x00073524 File Offset: 0x00071724
		public ActionInfo ActionInfo
		{
			get
			{
				if (this.m_actionInfo == null)
				{
					if (this.m_isOldSnapshot)
					{
						if (this.m_renderTextBox.ActionInfo != null)
						{
							this.m_actionInfo = new ActionInfo(base.RenderingContext, this.m_renderTextBox.ActionInfo);
						}
					}
					else if (this.m_textBoxDef.Action != null)
					{
						this.m_actionInfo = new ActionInfo(base.RenderingContext, this.ReportScope, this.m_textBoxDef.Action, this.m_reportItemDef, this, this.m_reportItemDef.ObjectType, this.m_reportItemDef.Name, this);
					}
				}
				return this.m_actionInfo;
			}
		}

		// Token: 0x1700104D RID: 4173
		// (get) Token: 0x06001D24 RID: 7460 RVA: 0x000735C2 File Offset: 0x000717C2
		public TypeCode SharedTypeCode
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderTextBox.SharedTypeCode;
				}
				if (this.IsSimple)
				{
					return this.Paragraphs[0].TextRuns[0].SharedTypeCode;
				}
				return TypeCode.String;
			}
		}

		// Token: 0x1700104E RID: 4174
		// (get) Token: 0x06001D25 RID: 7461 RVA: 0x000735FF File Offset: 0x000717FF
		public bool IsToggleParent
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderTextBox.IsSharedToggleParent;
				}
				return this.m_textBoxDef.IsToggle;
			}
		}

		// Token: 0x1700104F RID: 4175
		// (get) Token: 0x06001D26 RID: 7462 RVA: 0x00073620 File Offset: 0x00071820
		public bool CanSort
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return this.m_renderTextBox.CanSort;
				}
				return this.m_textBoxDef.UserSort != null;
			}
		}

		// Token: 0x17001050 RID: 4176
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x00073644 File Offset: 0x00071844
		public Microsoft.ReportingServices.OnDemandReportRendering.Report.DataElementStyles DataElementStyle
		{
			get
			{
				if (this.m_isOldSnapshot)
				{
					return (Microsoft.ReportingServices.OnDemandReportRendering.Report.DataElementStyles)this.m_renderTextBox.DataElementStyle;
				}
				if (!this.m_textBoxDef.DataElementStyleAttribute)
				{
					return Microsoft.ReportingServices.OnDemandReportRendering.Report.DataElementStyles.Element;
				}
				return Microsoft.ReportingServices.OnDemandReportRendering.Report.DataElementStyles.Attribute;
			}
		}

		// Token: 0x17001051 RID: 4177
		// (get) Token: 0x06001D28 RID: 7464 RVA: 0x0007366A File Offset: 0x0007186A
		public bool KeepTogether
		{
			get
			{
				return this.m_isOldSnapshot || this.m_textBoxDef.KeepTogether;
			}
		}

		// Token: 0x17001052 RID: 4178
		// (get) Token: 0x06001D29 RID: 7465 RVA: 0x00073681 File Offset: 0x00071881
		public ParagraphCollection Paragraphs
		{
			get
			{
				if (this.m_paragraphCollection == null)
				{
					this.m_paragraphCollection = new ParagraphCollection(this);
				}
				return this.m_paragraphCollection;
			}
		}

		// Token: 0x17001053 RID: 4179
		// (get) Token: 0x06001D2A RID: 7466 RVA: 0x0007369D File Offset: 0x0007189D
		public bool IsSimple
		{
			get
			{
				return this.m_isOldSnapshot || this.m_textBoxDef.IsSimple;
			}
		}

		// Token: 0x17001054 RID: 4180
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x000736B4 File Offset: 0x000718B4
		public bool FormattedValueExpressionBased
		{
			get
			{
				return this.IsSimple && this.Paragraphs[0].TextRuns[0].FormattedValueExpressionBased;
			}
		}

		// Token: 0x17001055 RID: 4181
		// (get) Token: 0x06001D2C RID: 7468 RVA: 0x000736DC File Offset: 0x000718DC
		internal Microsoft.ReportingServices.ReportIntermediateFormat.TextBox TexBoxDef
		{
			get
			{
				return this.m_textBoxDef;
			}
		}

		// Token: 0x06001D2D RID: 7469 RVA: 0x000736E4 File Offset: 0x000718E4
		internal override ReportItemInstance GetOrCreateInstance()
		{
			if (this.m_instance == null)
			{
				this.m_instance = new TextBoxInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x17001056 RID: 4182
		// (get) Token: 0x06001D2E RID: 7470 RVA: 0x00073700 File Offset: 0x00071900
		List<string> IROMActionOwner.FieldsUsedInValueExpression
		{
			get
			{
				if (base.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				return ((TextBoxInstance)this.GetOrCreateInstance()).GetFieldsUsedInValueExpression();
			}
		}

		// Token: 0x06001D2F RID: 7471 RVA: 0x00073724 File Offset: 0x00071924
		internal override void UpdateRenderReportItem(Microsoft.ReportingServices.ReportRendering.ReportItem renderReportItem)
		{
			base.UpdateRenderReportItem(renderReportItem);
			this.m_renderTextBox = (Microsoft.ReportingServices.ReportRendering.TextBox)this.m_renderReportItem;
			if (this.m_actionInfo != null)
			{
				this.m_actionInfo.Update(this.m_renderTextBox.ActionInfo);
			}
			if (this.m_paragraphCollection != null && this.m_paragraphCollection[0] != null)
			{
				this.m_paragraphCollection[0].UpdateRenderReportItem(renderReportItem);
			}
		}

		// Token: 0x06001D30 RID: 7472 RVA: 0x0007378F File Offset: 0x0007198F
		internal override void SetNewContextChildren()
		{
			base.SetNewContextChildren();
			if (this.m_actionInfo != null)
			{
				this.m_actionInfo.SetNewContext();
			}
			if (this.m_paragraphCollection != null)
			{
				this.m_paragraphCollection.SetNewContext();
			}
		}

		// Token: 0x04000F34 RID: 3892
		private ActionInfo m_actionInfo;

		// Token: 0x04000F35 RID: 3893
		private Microsoft.ReportingServices.ReportRendering.TextBox m_renderTextBox;

		// Token: 0x04000F36 RID: 3894
		private ParagraphCollection m_paragraphCollection;

		// Token: 0x04000F37 RID: 3895
		private Microsoft.ReportingServices.ReportIntermediateFormat.TextBox m_textBoxDef;
	}
}
