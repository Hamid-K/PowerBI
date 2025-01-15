using System;
using System.Security.Permissions;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200031E RID: 798
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ParagraphInstance : ReportElementInstance
	{
		// Token: 0x06001DA0 RID: 7584 RVA: 0x00074BE8 File Offset: 0x00072DE8
		internal ParagraphInstance(Paragraph paragraphDef)
			: base(paragraphDef)
		{
		}

		// Token: 0x06001DA1 RID: 7585 RVA: 0x00074BF1 File Offset: 0x00072DF1
		protected ParagraphInstance(ReportElement reportElementDef)
			: base(reportElementDef)
		{
		}

		// Token: 0x170010A6 RID: 4262
		// (get) Token: 0x06001DA2 RID: 7586
		public abstract string UniqueName { get; }

		// Token: 0x170010A7 RID: 4263
		// (get) Token: 0x06001DA3 RID: 7587 RVA: 0x00074BFA File Offset: 0x00072DFA
		public virtual ReportSize LeftIndent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010A8 RID: 4264
		// (get) Token: 0x06001DA4 RID: 7588 RVA: 0x00074BFD File Offset: 0x00072DFD
		public virtual ReportSize RightIndent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010A9 RID: 4265
		// (get) Token: 0x06001DA5 RID: 7589 RVA: 0x00074C00 File Offset: 0x00072E00
		public virtual ReportSize HangingIndent
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010AA RID: 4266
		// (get) Token: 0x06001DA6 RID: 7590 RVA: 0x00074C03 File Offset: 0x00072E03
		public virtual ListStyle ListStyle
		{
			get
			{
				return ListStyle.None;
			}
		}

		// Token: 0x170010AB RID: 4267
		// (get) Token: 0x06001DA7 RID: 7591 RVA: 0x00074C06 File Offset: 0x00072E06
		public virtual int ListLevel
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x170010AC RID: 4268
		// (get) Token: 0x06001DA8 RID: 7592 RVA: 0x00074C09 File Offset: 0x00072E09
		public virtual ReportSize SpaceBefore
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010AD RID: 4269
		// (get) Token: 0x06001DA9 RID: 7593 RVA: 0x00074C0C File Offset: 0x00072E0C
		public virtual ReportSize SpaceAfter
		{
			get
			{
				return null;
			}
		}

		// Token: 0x170010AE RID: 4270
		// (get) Token: 0x06001DAA RID: 7594 RVA: 0x00074C0F File Offset: 0x00072E0F
		public Paragraph Definition
		{
			get
			{
				return (Paragraph)this.m_reportElementDef;
			}
		}

		// Token: 0x170010AF RID: 4271
		// (get) Token: 0x06001DAB RID: 7595 RVA: 0x00074C1C File Offset: 0x00072E1C
		public TextRunInstanceCollection TextRunInstances
		{
			get
			{
				if (this.m_textRunInstances == null)
				{
					this.m_textRunInstances = new TextRunInstanceCollection(this);
				}
				return this.m_textRunInstances;
			}
		}

		// Token: 0x170010B0 RID: 4272
		// (get) Token: 0x06001DAC RID: 7596
		public abstract bool IsCompiled { get; }

		// Token: 0x06001DAD RID: 7597 RVA: 0x00074C38 File Offset: 0x00072E38
		protected override void ResetInstanceCache()
		{
			base.ResetInstanceCache();
			this.m_uniqueName = null;
		}

		// Token: 0x04000F5F RID: 3935
		protected TextRunInstanceCollection m_textRunInstances;

		// Token: 0x04000F60 RID: 3936
		protected string m_uniqueName;
	}
}
