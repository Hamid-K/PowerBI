using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001F1 RID: 497
	public sealed class MapLimits
	{
		// Token: 0x060012BB RID: 4795 RVA: 0x0004C51A File Offset: 0x0004A71A
		internal MapLimits(MapLimits defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x17000A2F RID: 2607
		// (get) Token: 0x060012BC RID: 4796 RVA: 0x0004C530 File Offset: 0x0004A730
		public ReportDoubleProperty MinimumX
		{
			get
			{
				if (this.m_minimumX == null && this.m_defObject.MinimumX != null)
				{
					this.m_minimumX = new ReportDoubleProperty(this.m_defObject.MinimumX);
				}
				return this.m_minimumX;
			}
		}

		// Token: 0x17000A30 RID: 2608
		// (get) Token: 0x060012BD RID: 4797 RVA: 0x0004C563 File Offset: 0x0004A763
		public ReportDoubleProperty MinimumY
		{
			get
			{
				if (this.m_minimumY == null && this.m_defObject.MinimumY != null)
				{
					this.m_minimumY = new ReportDoubleProperty(this.m_defObject.MinimumY);
				}
				return this.m_minimumY;
			}
		}

		// Token: 0x17000A31 RID: 2609
		// (get) Token: 0x060012BE RID: 4798 RVA: 0x0004C596 File Offset: 0x0004A796
		public ReportDoubleProperty MaximumX
		{
			get
			{
				if (this.m_maximumX == null && this.m_defObject.MaximumX != null)
				{
					this.m_maximumX = new ReportDoubleProperty(this.m_defObject.MaximumX);
				}
				return this.m_maximumX;
			}
		}

		// Token: 0x17000A32 RID: 2610
		// (get) Token: 0x060012BF RID: 4799 RVA: 0x0004C5C9 File Offset: 0x0004A7C9
		public ReportDoubleProperty MaximumY
		{
			get
			{
				if (this.m_maximumY == null && this.m_defObject.MaximumY != null)
				{
					this.m_maximumY = new ReportDoubleProperty(this.m_defObject.MaximumY);
				}
				return this.m_maximumY;
			}
		}

		// Token: 0x17000A33 RID: 2611
		// (get) Token: 0x060012C0 RID: 4800 RVA: 0x0004C5FC File Offset: 0x0004A7FC
		public ReportBoolProperty LimitToData
		{
			get
			{
				if (this.m_limitToData == null && this.m_defObject.LimitToData != null)
				{
					this.m_limitToData = new ReportBoolProperty(this.m_defObject.LimitToData);
				}
				return this.m_limitToData;
			}
		}

		// Token: 0x17000A34 RID: 2612
		// (get) Token: 0x060012C1 RID: 4801 RVA: 0x0004C62F File Offset: 0x0004A82F
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x17000A35 RID: 2613
		// (get) Token: 0x060012C2 RID: 4802 RVA: 0x0004C637 File Offset: 0x0004A837
		internal MapLimits MapLimitsDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000A36 RID: 2614
		// (get) Token: 0x060012C3 RID: 4803 RVA: 0x0004C63F File Offset: 0x0004A83F
		public MapLimitsInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapLimitsInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x060012C4 RID: 4804 RVA: 0x0004C66F File Offset: 0x0004A86F
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x040008FB RID: 2299
		private Map m_map;

		// Token: 0x040008FC RID: 2300
		private MapLimits m_defObject;

		// Token: 0x040008FD RID: 2301
		private MapLimitsInstance m_instance;

		// Token: 0x040008FE RID: 2302
		private ReportDoubleProperty m_minimumX;

		// Token: 0x040008FF RID: 2303
		private ReportDoubleProperty m_minimumY;

		// Token: 0x04000900 RID: 2304
		private ReportDoubleProperty m_maximumX;

		// Token: 0x04000901 RID: 2305
		private ReportDoubleProperty m_maximumY;

		// Token: 0x04000902 RID: 2306
		private ReportBoolProperty m_limitToData;
	}
}
