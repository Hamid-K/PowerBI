using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000122 RID: 290
	public sealed class LinearScale : GaugeScale
	{
		// Token: 0x06000CC6 RID: 3270 RVA: 0x00036FCD File Offset: 0x000351CD
		internal LinearScale(LinearScale defObject, GaugePanel gaugePanel)
			: base(defObject, gaugePanel)
		{
			this.m_defObject = defObject;
			this.m_gaugePanel = gaugePanel;
		}

		// Token: 0x17000768 RID: 1896
		// (get) Token: 0x06000CC7 RID: 3271 RVA: 0x00036FE5 File Offset: 0x000351E5
		public LinearPointerCollection GaugePointers
		{
			get
			{
				if (this.m_gaugePointers == null && this.LinearScaleDef.GaugePointers != null)
				{
					this.m_gaugePointers = new LinearPointerCollection(this, this.m_gaugePanel);
				}
				return this.m_gaugePointers;
			}
		}

		// Token: 0x17000769 RID: 1897
		// (get) Token: 0x06000CC8 RID: 3272 RVA: 0x00037014 File Offset: 0x00035214
		public ReportDoubleProperty StartMargin
		{
			get
			{
				if (this.m_startMargin == null && this.LinearScaleDef.StartMargin != null)
				{
					this.m_startMargin = new ReportDoubleProperty(this.LinearScaleDef.StartMargin);
				}
				return this.m_startMargin;
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06000CC9 RID: 3273 RVA: 0x00037047 File Offset: 0x00035247
		public ReportDoubleProperty EndMargin
		{
			get
			{
				if (this.m_endMargin == null && this.LinearScaleDef.EndMargin != null)
				{
					this.m_endMargin = new ReportDoubleProperty(this.LinearScaleDef.EndMargin);
				}
				return this.m_endMargin;
			}
		}

		// Token: 0x1700076B RID: 1899
		// (get) Token: 0x06000CCA RID: 3274 RVA: 0x0003707A File Offset: 0x0003527A
		public ReportDoubleProperty Position
		{
			get
			{
				if (this.m_position == null && this.LinearScaleDef.Position != null)
				{
					this.m_position = new ReportDoubleProperty(this.LinearScaleDef.Position);
				}
				return this.m_position;
			}
		}

		// Token: 0x1700076C RID: 1900
		// (get) Token: 0x06000CCB RID: 3275 RVA: 0x000370AD File Offset: 0x000352AD
		internal LinearScale LinearScaleDef
		{
			get
			{
				return (LinearScale)this.m_defObject;
			}
		}

		// Token: 0x1700076D RID: 1901
		// (get) Token: 0x06000CCC RID: 3276 RVA: 0x000370BA File Offset: 0x000352BA
		public new LinearScaleInstance Instance
		{
			get
			{
				return (LinearScaleInstance)this.GetInstance();
			}
		}

		// Token: 0x06000CCD RID: 3277 RVA: 0x000370C7 File Offset: 0x000352C7
		internal override GaugeScaleInstance GetInstance()
		{
			if (this.m_gaugePanel.RenderingContext.InstanceAccessDisallowed)
			{
				return null;
			}
			if (this.m_instance == null)
			{
				this.m_instance = new LinearScaleInstance(this);
			}
			return (GaugeScaleInstance)this.m_instance;
		}

		// Token: 0x06000CCE RID: 3278 RVA: 0x000370FC File Offset: 0x000352FC
		internal override void SetNewContext()
		{
			base.SetNewContext();
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_gaugePointers != null)
			{
				this.m_gaugePointers.SetNewContext();
			}
		}

		// Token: 0x040005A9 RID: 1449
		private LinearPointerCollection m_gaugePointers;

		// Token: 0x040005AA RID: 1450
		private ReportDoubleProperty m_startMargin;

		// Token: 0x040005AB RID: 1451
		private ReportDoubleProperty m_endMargin;

		// Token: 0x040005AC RID: 1452
		private ReportDoubleProperty m_position;
	}
}
