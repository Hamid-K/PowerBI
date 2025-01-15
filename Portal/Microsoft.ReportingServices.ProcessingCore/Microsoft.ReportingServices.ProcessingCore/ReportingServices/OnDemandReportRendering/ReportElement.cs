using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportRendering;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000306 RID: 774
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class ReportElement : IDefinitionPath, IROMStyleDefinitionContainer
	{
		// Token: 0x06001C52 RID: 7250 RVA: 0x00070B48 File Offset: 0x0006ED48
		internal ReportElement(IDefinitionPath parentDefinitionPath)
		{
			this.m_parentDefinitionPath = parentDefinitionPath;
		}

		// Token: 0x06001C53 RID: 7251 RVA: 0x00070B57 File Offset: 0x0006ED57
		internal ReportElement(IReportScope reportScope, IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem reportItemDef, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			this.m_reportScope = reportScope;
			this.m_parentDefinitionPath = parentDefinitionPath;
			this.m_reportItemDef = reportItemDef;
			this.m_renderingContext = renderingContext;
			this.m_isOldSnapshot = false;
		}

		// Token: 0x06001C54 RID: 7252 RVA: 0x00070B83 File Offset: 0x0006ED83
		internal ReportElement(IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.ReportRendering.ReportItem renderReportItem, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			this.m_parentDefinitionPath = parentDefinitionPath;
			this.m_renderReportItem = renderReportItem;
			this.m_renderingContext = renderingContext;
			this.m_isOldSnapshot = true;
		}

		// Token: 0x06001C55 RID: 7253 RVA: 0x00070BA7 File Offset: 0x0006EDA7
		internal ReportElement(IDefinitionPath parentDefinitionPath, Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext renderingContext)
		{
			this.m_parentDefinitionPath = parentDefinitionPath;
			this.m_renderingContext = renderingContext;
			this.m_isOldSnapshot = true;
		}

		// Token: 0x17000FDE RID: 4062
		// (get) Token: 0x06001C56 RID: 7254
		public abstract string DefinitionPath { get; }

		// Token: 0x17000FDF RID: 4063
		// (get) Token: 0x06001C57 RID: 7255 RVA: 0x00070BC4 File Offset: 0x0006EDC4
		public IDefinitionPath ParentDefinitionPath
		{
			get
			{
				return this.m_parentDefinitionPath;
			}
		}

		// Token: 0x17000FE0 RID: 4064
		// (get) Token: 0x06001C58 RID: 7256
		internal abstract string InstanceUniqueName { get; }

		// Token: 0x17000FE1 RID: 4065
		// (get) Token: 0x06001C59 RID: 7257 RVA: 0x00070BCC File Offset: 0x0006EDCC
		public ReportElementInstance Instance
		{
			get
			{
				return this.ReportElementInstance;
			}
		}

		// Token: 0x17000FE2 RID: 4066
		// (get) Token: 0x06001C5A RID: 7258
		internal abstract ReportElementInstance ReportElementInstance { get; }

		// Token: 0x17000FE3 RID: 4067
		// (get) Token: 0x06001C5B RID: 7259 RVA: 0x00070BD4 File Offset: 0x0006EDD4
		public virtual Microsoft.ReportingServices.OnDemandReportRendering.Style Style
		{
			get
			{
				if (this.m_style == null)
				{
					if (this.m_isOldSnapshot)
					{
						this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this.RenderReportItem, this.m_renderingContext, this.UseRenderStyle);
					}
					else
					{
						this.m_style = new Microsoft.ReportingServices.OnDemandReportRendering.Style(this, this.ReportScope, this.StyleContainer, this.m_renderingContext);
					}
				}
				return this.m_style;
			}
		}

		// Token: 0x17000FE4 RID: 4068
		// (get) Token: 0x06001C5C RID: 7260
		public abstract string ID { get; }

		// Token: 0x17000FE5 RID: 4069
		// (get) Token: 0x06001C5D RID: 7261 RVA: 0x00070C34 File Offset: 0x0006EE34
		internal virtual bool UseRenderStyle
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000FE6 RID: 4070
		// (get) Token: 0x06001C5E RID: 7262 RVA: 0x00070C37 File Offset: 0x0006EE37
		internal virtual IStyleContainer StyleContainer
		{
			get
			{
				return this.m_reportItemDef;
			}
		}

		// Token: 0x17000FE7 RID: 4071
		// (get) Token: 0x06001C5F RID: 7263 RVA: 0x00070C3F File Offset: 0x0006EE3F
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem ReportItemDef
		{
			get
			{
				return this.m_reportItemDef;
			}
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06001C60 RID: 7264 RVA: 0x00070C47 File Offset: 0x0006EE47
		internal Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext RenderingContext
		{
			get
			{
				return this.m_renderingContext;
			}
		}

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06001C61 RID: 7265 RVA: 0x00070C4F File Offset: 0x0006EE4F
		internal bool IsOldSnapshot
		{
			get
			{
				return this.m_isOldSnapshot;
			}
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06001C62 RID: 7266 RVA: 0x00070C57 File Offset: 0x0006EE57
		internal virtual Microsoft.ReportingServices.ReportRendering.ReportItem RenderReportItem
		{
			get
			{
				if (!this.m_isOldSnapshot)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				return this.m_renderReportItem;
			}
		}

		// Token: 0x17000FEB RID: 4075
		// (get) Token: 0x06001C63 RID: 7267 RVA: 0x00070C72 File Offset: 0x0006EE72
		internal virtual IReportScope ReportScope
		{
			get
			{
				return this.m_reportScope;
			}
		}

		// Token: 0x17000FEC RID: 4076
		// (get) Token: 0x06001C64 RID: 7268 RVA: 0x00070C7A File Offset: 0x0006EE7A
		// (set) Token: 0x06001C65 RID: 7269 RVA: 0x00070CA5 File Offset: 0x0006EEA5
		internal Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem CriOwner
		{
			get
			{
				Global.Tracer.Assert(!this.m_isOldSnapshot || this.__criOwner == null, "(!m_isOldSnapshot || __criOwner == null)");
				return this.__criOwner;
			}
			set
			{
				Global.Tracer.Assert(!this.m_isOldSnapshot, "(!m_isOldSnapshot)");
				this.__criOwner = value;
			}
		}

		// Token: 0x17000FED RID: 4077
		// (get) Token: 0x06001C66 RID: 7270 RVA: 0x00070CC6 File Offset: 0x0006EEC6
		// (set) Token: 0x06001C67 RID: 7271 RVA: 0x00070CF1 File Offset: 0x0006EEF1
		internal ReportElement.CriGenerationPhases CriGenerationPhase
		{
			get
			{
				Global.Tracer.Assert(!this.m_isOldSnapshot || this.__criGenerationPhase == ReportElement.CriGenerationPhases.None, "(!m_isOldSnapshot || __criGenerationPhase == CriGenerationPhases.None)");
				return this.__criGenerationPhase;
			}
			set
			{
				Global.Tracer.Assert(!this.m_isOldSnapshot, "(!m_isOldSnapshot)");
				this.__criGenerationPhase = value;
			}
		}

		// Token: 0x06001C68 RID: 7272 RVA: 0x00070D12 File Offset: 0x0006EF12
		internal virtual void SetNewContext()
		{
			if (this.m_style != null)
			{
				this.m_style.SetNewContext();
			}
			this.SetNewContextChildren();
		}

		// Token: 0x06001C69 RID: 7273
		internal abstract void SetNewContextChildren();

		// Token: 0x06001C6A RID: 7274 RVA: 0x00070D2D File Offset: 0x0006EF2D
		internal void ConstructReportElementDefinitionImpl()
		{
			Global.Tracer.Assert(this.CriGenerationPhase == ReportElement.CriGenerationPhases.Definition, "(CriGenerationPhase == CriGenerationPhases.Definition)");
			this.Style.ConstructStyleDefinition();
		}

		// Token: 0x04000EE7 RID: 3815
		protected bool m_isOldSnapshot;

		// Token: 0x04000EE8 RID: 3816
		internal Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem m_reportItemDef;

		// Token: 0x04000EE9 RID: 3817
		internal Microsoft.ReportingServices.OnDemandReportRendering.RenderingContext m_renderingContext;

		// Token: 0x04000EEA RID: 3818
		internal Microsoft.ReportingServices.ReportRendering.ReportItem m_renderReportItem;

		// Token: 0x04000EEB RID: 3819
		protected IDefinitionPath m_parentDefinitionPath;

		// Token: 0x04000EEC RID: 3820
		protected Microsoft.ReportingServices.OnDemandReportRendering.Style m_style;

		// Token: 0x04000EED RID: 3821
		private IReportScope m_reportScope;

		// Token: 0x04000EEE RID: 3822
		private Microsoft.ReportingServices.OnDemandReportRendering.CustomReportItem __criOwner;

		// Token: 0x04000EEF RID: 3823
		private ReportElement.CriGenerationPhases __criGenerationPhase;

		// Token: 0x02000948 RID: 2376
		internal enum CriGenerationPhases
		{
			// Token: 0x04004045 RID: 16453
			None,
			// Token: 0x04004046 RID: 16454
			Definition,
			// Token: 0x04004047 RID: 16455
			Instance
		}
	}
}
