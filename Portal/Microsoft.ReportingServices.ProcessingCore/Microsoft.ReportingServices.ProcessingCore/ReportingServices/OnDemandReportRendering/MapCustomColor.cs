using System;
using System.Drawing;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x020001CB RID: 459
	public sealed class MapCustomColor : MapObjectCollectionItem
	{
		// Token: 0x060011E1 RID: 4577 RVA: 0x00049E61 File Offset: 0x00048061
		internal MapCustomColor(MapCustomColor defObject, Map map)
		{
			this.m_defObject = defObject;
			this.m_map = map;
		}

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x060011E2 RID: 4578 RVA: 0x00049E78 File Offset: 0x00048078
		public ReportColorProperty Color
		{
			get
			{
				if (this.m_color == null && this.m_defObject.Color != null)
				{
					ExpressionInfo color = this.m_defObject.Color;
					if (color != null)
					{
						this.m_color = new ReportColorProperty(color.IsExpression, this.m_defObject.Color.OriginalText, color.IsExpression ? null : new ReportColor(color.StringValue.Trim(), true), color.IsExpression ? new ReportColor("", global::System.Drawing.Color.Empty, true) : null);
					}
				}
				return this.m_color;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x060011E3 RID: 4579 RVA: 0x00049F07 File Offset: 0x00048107
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x060011E4 RID: 4580 RVA: 0x00049F0F File Offset: 0x0004810F
		internal MapCustomColor MapCustomColorDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x170009AE RID: 2478
		// (get) Token: 0x060011E5 RID: 4581 RVA: 0x00049F17 File Offset: 0x00048117
		public MapCustomColorInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapCustomColorInstance(this);
				}
				return (MapCustomColorInstance)this.m_instance;
			}
		}

		// Token: 0x060011E6 RID: 4582 RVA: 0x00049F4C File Offset: 0x0004814C
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x04000879 RID: 2169
		private Map m_map;

		// Token: 0x0400087A RID: 2170
		private MapCustomColor m_defObject;

		// Token: 0x0400087B RID: 2171
		private ReportColorProperty m_color;
	}
}
