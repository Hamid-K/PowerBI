using System;
using System.Security.Permissions;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020000FF RID: 255
	[StrongNameIdentityPermission(SecurityAction.InheritanceDemand, PublicKey = "0024000004800000940000000602000000240000525341310004000001000100272736ad6e5f9586bac2d531eabc3acc666c2f8ec879fa94f8f7b0327d2ff2ed523448f83c3d5c5dd2dfc7bc99c5286b2c125117bf5cbe242b9d41750732b2bdffe649c6efb8e5526d526fdd130095ecdb7bf210809c6cdad8824faa9ac0310ac3cba2aa0523567b2dfa7fe250b30facbd62d4ec99b94ac47c7d3b28f1f6e4c8")]
	public abstract class Gauge : GaugePanelItem
	{
		// Token: 0x06000B62 RID: 2914 RVA: 0x000329BE File Offset: 0x00030BBE
		internal Gauge(Gauge defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x17000670 RID: 1648
		// (get) Token: 0x06000B63 RID: 2915 RVA: 0x000329D6 File Offset: 0x00030BD6
		public BackFrame BackFrame
		{
			get
			{
				if (this.m_backFrame == null && this.GaugeDef.BackFrame != null)
				{
					this.m_backFrame = new BackFrame(this.GaugeDef.BackFrame, this.m_gaugePanel);
				}
				return this.m_backFrame;
			}
		}

		// Token: 0x17000671 RID: 1649
		// (get) Token: 0x06000B64 RID: 2916 RVA: 0x00032A0F File Offset: 0x00030C0F
		public ReportBoolProperty ClipContent
		{
			get
			{
				if (this.m_clipContent == null && this.GaugeDef.ClipContent != null)
				{
					this.m_clipContent = new ReportBoolProperty(this.GaugeDef.ClipContent);
				}
				return this.m_clipContent;
			}
		}

		// Token: 0x17000672 RID: 1650
		// (get) Token: 0x06000B65 RID: 2917 RVA: 0x00032A42 File Offset: 0x00030C42
		public TopImage TopImage
		{
			get
			{
				if (this.m_topImage == null && this.GaugeDef.TopImage != null)
				{
					this.m_topImage = new TopImage(this.GaugeDef.TopImage, this.m_gaugePanel);
				}
				return this.m_topImage;
			}
		}

		// Token: 0x17000673 RID: 1651
		// (get) Token: 0x06000B66 RID: 2918 RVA: 0x00032A7B File Offset: 0x00030C7B
		public ReportDoubleProperty AspectRatio
		{
			get
			{
				if (this.m_aspectRatio == null && this.GaugeDef.AspectRatio != null)
				{
					this.m_aspectRatio = new ReportDoubleProperty(this.GaugeDef.AspectRatio);
				}
				return this.m_aspectRatio;
			}
		}

		// Token: 0x17000674 RID: 1652
		// (get) Token: 0x06000B67 RID: 2919 RVA: 0x00032AAE File Offset: 0x00030CAE
		internal Gauge GaugeDef
		{
			get
			{
				return (Gauge)this.m_defObject;
			}
		}

		// Token: 0x17000675 RID: 1653
		// (get) Token: 0x06000B68 RID: 2920 RVA: 0x00032ABB File Offset: 0x00030CBB
		public new GaugeInstance Instance
		{
			get
			{
				return (GaugeInstance)this.GetInstance();
			}
		}

		// Token: 0x06000B69 RID: 2921 RVA: 0x00032AC8 File Offset: 0x00030CC8
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_backFrame != null)
			{
				this.m_backFrame.SetNewContext();
			}
			if (this.m_topImage != null)
			{
				this.m_topImage.SetNewContext();
			}
		}

		// Token: 0x040004D8 RID: 1240
		private BackFrame m_backFrame;

		// Token: 0x040004D9 RID: 1241
		private ReportBoolProperty m_clipContent;

		// Token: 0x040004DA RID: 1242
		private TopImage m_topImage;

		// Token: 0x040004DB RID: 1243
		private ReportDoubleProperty m_aspectRatio;
	}
}
