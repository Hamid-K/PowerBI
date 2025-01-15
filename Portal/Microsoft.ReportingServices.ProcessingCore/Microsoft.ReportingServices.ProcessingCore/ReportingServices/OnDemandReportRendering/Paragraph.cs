using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000315 RID: 789
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class Paragraph : ReportElement
	{
		// Token: 0x06001D35 RID: 7477 RVA: 0x00073923 File Offset: 0x00071B23
		internal Paragraph(TextBox textBox, int indexIntoParentCollectionDef, RenderingContext renderingContext)
			: base(textBox.ReportScope, textBox, textBox.ReportItemDef, renderingContext)
		{
			this.m_textBox = textBox;
			this.m_indexIntoParentCollectionDef = indexIntoParentCollectionDef;
		}

		// Token: 0x06001D36 RID: 7478 RVA: 0x00073947 File Offset: 0x00071B47
		internal Paragraph(TextBox textBox, RenderingContext renderingContext)
			: base(textBox, textBox.RenderReportItem, renderingContext)
		{
			this.m_textBox = textBox;
		}

		// Token: 0x17001059 RID: 4185
		// (get) Token: 0x06001D37 RID: 7479 RVA: 0x0007395E File Offset: 0x00071B5E
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

		// Token: 0x1700105A RID: 4186
		// (get) Token: 0x06001D38 RID: 7480 RVA: 0x00073985 File Offset: 0x00071B85
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

		// Token: 0x1700105B RID: 4187
		// (get) Token: 0x06001D39 RID: 7481 RVA: 0x000739A1 File Offset: 0x00071BA1
		public TextRunCollection TextRuns
		{
			get
			{
				if (this.m_textRunCollection == null)
				{
					this.m_textRunCollection = new TextRunCollection(this);
				}
				return this.m_textRunCollection;
			}
		}

		// Token: 0x1700105C RID: 4188
		// (get) Token: 0x06001D3A RID: 7482 RVA: 0x000739BD File Offset: 0x00071BBD
		public virtual ReportSizeProperty LeftIndent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700105D RID: 4189
		// (get) Token: 0x06001D3B RID: 7483 RVA: 0x000739C0 File Offset: 0x00071BC0
		public virtual ReportSizeProperty RightIndent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700105E RID: 4190
		// (get) Token: 0x06001D3C RID: 7484 RVA: 0x000739C3 File Offset: 0x00071BC3
		public virtual ReportSizeProperty HangingIndent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700105F RID: 4191
		// (get) Token: 0x06001D3D RID: 7485
		public abstract ReportEnumProperty<ListStyle> ListStyle { get; }

		// Token: 0x17001060 RID: 4192
		// (get) Token: 0x06001D3E RID: 7486
		public abstract ReportIntProperty ListLevel { get; }

		// Token: 0x17001061 RID: 4193
		// (get) Token: 0x06001D3F RID: 7487 RVA: 0x000739C6 File Offset: 0x00071BC6
		public virtual ReportSizeProperty SpaceBefore
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001062 RID: 4194
		// (get) Token: 0x06001D40 RID: 7488 RVA: 0x000739C9 File Offset: 0x00071BC9
		public virtual ReportSizeProperty SpaceAfter
		{
			get
			{
				return null;
			}
		}

		// Token: 0x17001063 RID: 4195
		// (get) Token: 0x06001D41 RID: 7489 RVA: 0x000739CC File Offset: 0x00071BCC
		internal TextBox TextBox
		{
			get
			{
				return this.m_textBox;
			}
		}

		// Token: 0x17001064 RID: 4196
		// (get) Token: 0x06001D42 RID: 7490 RVA: 0x000739D4 File Offset: 0x00071BD4
		internal override ReportElementInstance ReportElementInstance
		{
			get
			{
				return this.Instance;
			}
		}

		// Token: 0x17001065 RID: 4197
		// (get) Token: 0x06001D43 RID: 7491
		public new abstract ParagraphInstance Instance { get; }

		// Token: 0x06001D44 RID: 7492 RVA: 0x000739DC File Offset: 0x00071BDC
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x06001D45 RID: 7493 RVA: 0x000739F7 File Offset: 0x00071BF7
		internal override void SetNewContextChildren()
		{
			if (this.m_textRunCollection != null)
			{
				this.m_textRunCollection.SetNewContext();
			}
		}

		// Token: 0x06001D46 RID: 7494 RVA: 0x00073A0C File Offset: 0x00071C0C
		internal virtual void UpdateRenderReportItem(ReportItem renderReportItem)
		{
			if (this.m_textRunCollection != null && this.m_textRunCollection[0] != null)
			{
				this.m_textRunCollection[0].UpdateRenderReportItem(renderReportItem);
			}
		}

		// Token: 0x04000F3A RID: 3898
		protected ReportIntProperty m_listLevel;

		// Token: 0x04000F3B RID: 3899
		protected ReportEnumProperty<ListStyle> m_listStyle;

		// Token: 0x04000F3C RID: 3900
		private string m_definitionPath;

		// Token: 0x04000F3D RID: 3901
		protected int m_indexIntoParentCollectionDef;

		// Token: 0x04000F3E RID: 3902
		protected ParagraphInstance m_instance;

		// Token: 0x04000F3F RID: 3903
		protected TextRunCollection m_textRunCollection;

		// Token: 0x04000F40 RID: 3904
		protected TextBox m_textBox;
	}
}
