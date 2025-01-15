using System;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200026E RID: 622
	public sealed class ChartNoMoveDirectionsInstance : BaseInstance
	{
		// Token: 0x0600181D RID: 6173 RVA: 0x00062E3A File Offset: 0x0006103A
		internal ChartNoMoveDirectionsInstance(ChartNoMoveDirections chartNoMoveDirectionsDef)
			: base(chartNoMoveDirectionsDef.ReportScope)
		{
			this.m_chartNoMoveDirectionsDef = chartNoMoveDirectionsDef;
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x0600181E RID: 6174 RVA: 0x00062E50 File Offset: 0x00061050
		public bool Up
		{
			get
			{
				if (this.m_up == null && !this.m_chartNoMoveDirectionsDef.ChartDef.IsOldSnapshot)
				{
					this.m_up = new bool?(this.m_chartNoMoveDirectionsDef.ChartNoMoveDirectionsDef.EvaluateUp(this.ReportScopeInstance, this.m_chartNoMoveDirectionsDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_up.Value;
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x0600181F RID: 6175 RVA: 0x00062EC0 File Offset: 0x000610C0
		public bool Down
		{
			get
			{
				if (this.m_down == null && !this.m_chartNoMoveDirectionsDef.ChartDef.IsOldSnapshot)
				{
					this.m_down = new bool?(this.m_chartNoMoveDirectionsDef.ChartNoMoveDirectionsDef.EvaluateDown(this.ReportScopeInstance, this.m_chartNoMoveDirectionsDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_down.Value;
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06001820 RID: 6176 RVA: 0x00062F30 File Offset: 0x00061130
		public bool Left
		{
			get
			{
				if (this.m_left == null && !this.m_chartNoMoveDirectionsDef.ChartDef.IsOldSnapshot)
				{
					this.m_left = new bool?(this.m_chartNoMoveDirectionsDef.ChartNoMoveDirectionsDef.EvaluateLeft(this.ReportScopeInstance, this.m_chartNoMoveDirectionsDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_left.Value;
			}
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06001821 RID: 6177 RVA: 0x00062FA0 File Offset: 0x000611A0
		public bool Right
		{
			get
			{
				if (this.m_right == null && !this.m_chartNoMoveDirectionsDef.ChartDef.IsOldSnapshot)
				{
					this.m_right = new bool?(this.m_chartNoMoveDirectionsDef.ChartNoMoveDirectionsDef.EvaluateRight(this.ReportScopeInstance, this.m_chartNoMoveDirectionsDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_right.Value;
			}
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06001822 RID: 6178 RVA: 0x00063010 File Offset: 0x00061210
		public bool UpLeft
		{
			get
			{
				if (this.m_upLeft == null && !this.m_chartNoMoveDirectionsDef.ChartDef.IsOldSnapshot)
				{
					this.m_upLeft = new bool?(this.m_chartNoMoveDirectionsDef.ChartNoMoveDirectionsDef.EvaluateUpLeft(this.ReportScopeInstance, this.m_chartNoMoveDirectionsDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_upLeft.Value;
			}
		}

		// Token: 0x17000DB7 RID: 3511
		// (get) Token: 0x06001823 RID: 6179 RVA: 0x00063080 File Offset: 0x00061280
		public bool UpRight
		{
			get
			{
				if (this.m_upRight == null && !this.m_chartNoMoveDirectionsDef.ChartDef.IsOldSnapshot)
				{
					this.m_upRight = new bool?(this.m_chartNoMoveDirectionsDef.ChartNoMoveDirectionsDef.EvaluateUpRight(this.ReportScopeInstance, this.m_chartNoMoveDirectionsDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_upRight.Value;
			}
		}

		// Token: 0x17000DB8 RID: 3512
		// (get) Token: 0x06001824 RID: 6180 RVA: 0x000630F0 File Offset: 0x000612F0
		public bool DownLeft
		{
			get
			{
				if (this.m_downLeft == null && !this.m_chartNoMoveDirectionsDef.ChartDef.IsOldSnapshot)
				{
					this.m_downLeft = new bool?(this.m_chartNoMoveDirectionsDef.ChartNoMoveDirectionsDef.EvaluateDownLeft(this.ReportScopeInstance, this.m_chartNoMoveDirectionsDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_downLeft.Value;
			}
		}

		// Token: 0x17000DB9 RID: 3513
		// (get) Token: 0x06001825 RID: 6181 RVA: 0x00063160 File Offset: 0x00061360
		public bool DownRight
		{
			get
			{
				if (this.m_downRight == null && !this.m_chartNoMoveDirectionsDef.ChartDef.IsOldSnapshot)
				{
					this.m_downRight = new bool?(this.m_chartNoMoveDirectionsDef.ChartNoMoveDirectionsDef.EvaluateDownRight(this.ReportScopeInstance, this.m_chartNoMoveDirectionsDef.ChartDef.RenderingContext.OdpContext));
				}
				return this.m_downRight.Value;
			}
		}

		// Token: 0x06001826 RID: 6182 RVA: 0x000631D0 File Offset: 0x000613D0
		protected override void ResetInstanceCache()
		{
			this.m_up = null;
			this.m_down = null;
			this.m_left = null;
			this.m_right = null;
			this.m_upLeft = null;
			this.m_upRight = null;
			this.m_downLeft = null;
			this.m_downRight = null;
		}

		// Token: 0x04000C2F RID: 3119
		private ChartNoMoveDirections m_chartNoMoveDirectionsDef;

		// Token: 0x04000C30 RID: 3120
		private bool? m_up;

		// Token: 0x04000C31 RID: 3121
		private bool? m_down;

		// Token: 0x04000C32 RID: 3122
		private bool? m_left;

		// Token: 0x04000C33 RID: 3123
		private bool? m_right;

		// Token: 0x04000C34 RID: 3124
		private bool? m_upLeft;

		// Token: 0x04000C35 RID: 3125
		private bool? m_upRight;

		// Token: 0x04000C36 RID: 3126
		private bool? m_downLeft;

		// Token: 0x04000C37 RID: 3127
		private bool? m_downRight;
	}
}
