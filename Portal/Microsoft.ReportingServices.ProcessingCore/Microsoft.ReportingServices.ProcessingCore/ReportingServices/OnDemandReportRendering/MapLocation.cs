using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200017A RID: 378
	public sealed class MapLocation
	{
		// Token: 0x06000FCB RID: 4043 RVA: 0x0004424E File Offset: 0x0004244E
		internal MapLocation(MapLocation defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x17000869 RID: 2153
		// (get) Token: 0x06000FCC RID: 4044 RVA: 0x00044264 File Offset: 0x00042464
		public ReportDoubleProperty Left
		{
			get
			{
				if (this.m_left == null && this.m_defObject.Left != null)
				{
					this.m_left = new ReportDoubleProperty(this.m_defObject.Left);
				}
				return this.m_left;
			}
		}

		// Token: 0x1700086A RID: 2154
		// (get) Token: 0x06000FCD RID: 4045 RVA: 0x00044297 File Offset: 0x00042497
		public ReportDoubleProperty Top
		{
			get
			{
				if (this.m_top == null && this.m_defObject.Top != null)
				{
					this.m_top = new ReportDoubleProperty(this.m_defObject.Top);
				}
				return this.m_top;
			}
		}

		// Token: 0x1700086B RID: 2155
		// (get) Token: 0x06000FCE RID: 4046 RVA: 0x000442CC File Offset: 0x000424CC
		public ReportEnumProperty<Unit> Unit
		{
			get
			{
				if (this.m_unit == null && this.m_defObject.Unit != null)
				{
					this.m_unit = new ReportEnumProperty<Unit>(this.m_defObject.Unit.IsExpression, this.m_defObject.Unit.OriginalText, EnumTranslator.TranslateUnit(this.m_defObject.Unit.StringValue, null));
				}
				return this.m_unit;
			}
		}

		// Token: 0x1700086C RID: 2156
		// (get) Token: 0x06000FCF RID: 4047 RVA: 0x00044335 File Offset: 0x00042535
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x1700086D RID: 2157
		// (get) Token: 0x06000FD0 RID: 4048 RVA: 0x0004433D File Offset: 0x0004253D
		internal MapLocation MapLocationDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x1700086E RID: 2158
		// (get) Token: 0x06000FD1 RID: 4049 RVA: 0x00044345 File Offset: 0x00042545
		public MapLocationInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapLocationInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x06000FD2 RID: 4050 RVA: 0x00044375 File Offset: 0x00042575
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x0400074F RID: 1871
		private Map m_map;

		// Token: 0x04000750 RID: 1872
		private MapLocation m_defObject;

		// Token: 0x04000751 RID: 1873
		private MapLocationInstance m_instance;

		// Token: 0x04000752 RID: 1874
		private ReportDoubleProperty m_left;

		// Token: 0x04000753 RID: 1875
		private ReportDoubleProperty m_top;

		// Token: 0x04000754 RID: 1876
		private ReportEnumProperty<Unit> m_unit;
	}
}
