using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200017C RID: 380
	public sealed class MapBindingFieldPair : MapObjectCollectionItem
	{
		// Token: 0x06000FDB RID: 4059 RVA: 0x000444C6 File Offset: 0x000426C6
		internal MapBindingFieldPair(MapBindingFieldPair defObject, MapVectorLayer mapVectorLayer, Map map)
		{
			this.m_defObject = defObject;
			this.m_mapVectorLayer = mapVectorLayer;
			this.m_map = map;
		}

		// Token: 0x17000875 RID: 2165
		// (get) Token: 0x06000FDC RID: 4060 RVA: 0x000444E3 File Offset: 0x000426E3
		public ReportStringProperty FieldName
		{
			get
			{
				if (this.m_fieldName == null && this.m_defObject.FieldName != null)
				{
					this.m_fieldName = new ReportStringProperty(this.m_defObject.FieldName);
				}
				return this.m_fieldName;
			}
		}

		// Token: 0x17000876 RID: 2166
		// (get) Token: 0x06000FDD RID: 4061 RVA: 0x00044516 File Offset: 0x00042716
		public ReportVariantProperty BindingExpression
		{
			get
			{
				if (this.m_bindingExpression == null && this.m_defObject.BindingExpression != null)
				{
					this.m_bindingExpression = new ReportVariantProperty(this.m_defObject.BindingExpression);
				}
				return this.m_bindingExpression;
			}
		}

		// Token: 0x17000877 RID: 2167
		// (get) Token: 0x06000FDE RID: 4062 RVA: 0x00044549 File Offset: 0x00042749
		internal Map MapDef
		{
			get
			{
				return this.m_map;
			}
		}

		// Token: 0x17000878 RID: 2168
		// (get) Token: 0x06000FDF RID: 4063 RVA: 0x00044551 File Offset: 0x00042751
		internal MapBindingFieldPair MapBindingFieldPairDef
		{
			get
			{
				return this.m_defObject;
			}
		}

		// Token: 0x17000879 RID: 2169
		// (get) Token: 0x06000FE0 RID: 4064 RVA: 0x00044559 File Offset: 0x00042759
		public MapBindingFieldPairInstance Instance
		{
			get
			{
				if (this.m_map.RenderingContext.InstanceAccessDisallowed)
				{
					return null;
				}
				if (this.m_instance == null)
				{
					this.m_instance = new MapBindingFieldPairInstance(this);
				}
				return (MapBindingFieldPairInstance)this.m_instance;
			}
		}

		// Token: 0x1700087A RID: 2170
		// (get) Token: 0x06000FE1 RID: 4065 RVA: 0x0004458E File Offset: 0x0004278E
		internal IReportScope ReportScope
		{
			get
			{
				if (this.m_mapVectorLayer != null)
				{
					return this.m_mapVectorLayer.ReportScope;
				}
				return this.MapDef.ReportScope;
			}
		}

		// Token: 0x06000FE2 RID: 4066 RVA: 0x000445AF File Offset: 0x000427AF
		internal override void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
		}

		// Token: 0x0400075B RID: 1883
		private Map m_map;

		// Token: 0x0400075C RID: 1884
		private MapVectorLayer m_mapVectorLayer;

		// Token: 0x0400075D RID: 1885
		private MapBindingFieldPair m_defObject;

		// Token: 0x0400075E RID: 1886
		private ReportStringProperty m_fieldName;

		// Token: 0x0400075F RID: 1887
		private ReportVariantProperty m_bindingExpression;
	}
}
