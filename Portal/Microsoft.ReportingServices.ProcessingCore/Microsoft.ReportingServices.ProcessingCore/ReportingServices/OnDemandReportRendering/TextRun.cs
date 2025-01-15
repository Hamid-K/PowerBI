using System;
using System.Collections.Generic;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000319 RID: 793
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class TextRun : ReportElement, IROMActionOwner
	{
		// Token: 0x06001D5E RID: 7518 RVA: 0x00073E7F File Offset: 0x0007207F
		internal TextRun(Paragraph paragraph, int indexIntoParentCollectionDef, RenderingContext renderingContext)
			: base(paragraph.ReportScope, paragraph, paragraph.TextBox.ReportItemDef, renderingContext)
		{
			this.m_paragraph = paragraph;
			this.m_indexIntoParentCollectionDef = indexIntoParentCollectionDef;
		}

		// Token: 0x06001D5F RID: 7519 RVA: 0x00073EA8 File Offset: 0x000720A8
		internal TextRun(Paragraph paragraph, RenderingContext renderingContext)
			: base(paragraph, paragraph.TextBox.RenderReportItem, renderingContext)
		{
			this.m_paragraph = paragraph;
		}

		// Token: 0x17001078 RID: 4216
		// (get) Token: 0x06001D60 RID: 7520 RVA: 0x00073EC4 File Offset: 0x000720C4
		public override string DefinitionPath
		{
			get
			{
				if (this.m_definitionPath == null)
				{
					this.m_definitionPath = DefinitionPathConstants.GetCollectionDefinitionPath(this.m_parentDefinitionPath, this.m_indexIntoParentCollectionDef);
				}
				return this.m_definitionPath;
			}
		}

		// Token: 0x17001079 RID: 4217
		// (get) Token: 0x06001D61 RID: 7521 RVA: 0x00073EEB File Offset: 0x000720EB
		internal override string InstanceUniqueName
		{
			get
			{
				if (base.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				return this.Instance.UniqueName;
			}
		}

		// Token: 0x1700107A RID: 4218
		// (get) Token: 0x06001D62 RID: 7522 RVA: 0x00073F07 File Offset: 0x00072107
		public virtual string Label
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700107B RID: 4219
		// (get) Token: 0x06001D63 RID: 7523
		public abstract ReportStringProperty Value { get; }

		// Token: 0x1700107C RID: 4220
		// (get) Token: 0x06001D64 RID: 7524 RVA: 0x00073F0A File Offset: 0x0007210A
		string IROMActionOwner.UniqueName
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700107D RID: 4221
		// (get) Token: 0x06001D65 RID: 7525 RVA: 0x00073F0D File Offset: 0x0007210D
		public virtual ActionInfo ActionInfo
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700107E RID: 4222
		// (get) Token: 0x06001D66 RID: 7526 RVA: 0x00073F10 File Offset: 0x00072110
		public virtual ReportStringProperty ToolTip
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700107F RID: 4223
		// (get) Token: 0x06001D67 RID: 7527
		public abstract ReportEnumProperty<MarkupType> MarkupType { get; }

		// Token: 0x17001080 RID: 4224
		// (get) Token: 0x06001D68 RID: 7528
		public abstract TypeCode SharedTypeCode { get; }

		// Token: 0x17001081 RID: 4225
		// (get) Token: 0x06001D69 RID: 7529 RVA: 0x00073F13 File Offset: 0x00072113
		internal TextBox TextBox
		{
			get
			{
				return this.m_paragraph.TextBox;
			}
		}

		// Token: 0x17001082 RID: 4226
		// (get) Token: 0x06001D6A RID: 7530
		public abstract bool FormattedValueExpressionBased { get; }

		// Token: 0x17001083 RID: 4227
		// (get) Token: 0x06001D6B RID: 7531 RVA: 0x00073F20 File Offset: 0x00072120
		internal override ReportElementInstance ReportElementInstance
		{
			get
			{
				return this.Instance;
			}
		}

		// Token: 0x17001084 RID: 4228
		// (get) Token: 0x06001D6C RID: 7532
		public new abstract TextRunInstance Instance { get; }

		// Token: 0x17001085 RID: 4229
		// (get) Token: 0x06001D6D RID: 7533 RVA: 0x00073F28 File Offset: 0x00072128
		public virtual CompiledRichTextInstance CompiledInstance
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001086 RID: 4230
		// (get) Token: 0x06001D6E RID: 7534 RVA: 0x00073F2B File Offset: 0x0007212B
		List<string> IROMActionOwner.FieldsUsedInValueExpression
		{
			get
			{
				return this.FieldsUsedInValueExpression;
			}
		}

		// Token: 0x17001087 RID: 4231
		// (get) Token: 0x06001D6F RID: 7535 RVA: 0x00073F33 File Offset: 0x00072133
		internal virtual List<string> FieldsUsedInValueExpression
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06001D70 RID: 7536 RVA: 0x00073F36 File Offset: 0x00072136
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x06001D71 RID: 7537 RVA: 0x00073F51 File Offset: 0x00072151
		internal override void SetNewContextChildren()
		{
		}

		// Token: 0x06001D72 RID: 7538 RVA: 0x00073F53 File Offset: 0x00072153
		internal virtual void UpdateRenderReportItem(ReportItem renderReportItem)
		{
		}

		// Token: 0x04000F49 RID: 3913
		protected ReportStringProperty m_value;

		// Token: 0x04000F4A RID: 3914
		protected ReportEnumProperty<MarkupType> m_markupType;

		// Token: 0x04000F4B RID: 3915
		private string m_definitionPath;

		// Token: 0x04000F4C RID: 3916
		protected int m_indexIntoParentCollectionDef;

		// Token: 0x04000F4D RID: 3917
		protected TextRunInstance m_instance;

		// Token: 0x04000F4E RID: 3918
		protected Paragraph m_paragraph;

		// Token: 0x04000F4F RID: 3919
		protected bool? m_formattedValueExpressionBased;
	}
}
