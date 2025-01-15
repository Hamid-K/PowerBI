using System;
using System.Drawing;
using System.Globalization;
using Microsoft.Reporting.Map.WebForms;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200015A RID: 346
	internal class CoreShapeManager : CoreSpatialElementManager
	{
		// Token: 0x06000E55 RID: 3669 RVA: 0x0003E5E4 File Offset: 0x0003C7E4
		internal CoreShapeManager(MapControl mapControl, MapVectorLayer mapVectorLayer)
			: base(mapControl, mapVectorLayer)
		{
		}

		// Token: 0x06000E56 RID: 3670 RVA: 0x0003E5F0 File Offset: 0x0003C7F0
		internal override void AddSpatialElement(ISpatialElement spatialElement)
		{
			((NamedElement)spatialElement).Name = this.m_coreMap.Shapes.Count.ToString(CultureInfo.InvariantCulture);
			this.m_coreMap.Shapes.Add((Shape)spatialElement);
		}

		// Token: 0x06000E57 RID: 3671 RVA: 0x0003E63C File Offset: 0x0003C83C
		internal override void RemoveSpatialElement(ISpatialElement spatialElement)
		{
			this.m_coreMap.Shapes.Remove((Shape)spatialElement);
		}

		// Token: 0x06000E58 RID: 3672 RVA: 0x0003E654 File Offset: 0x0003C854
		internal override ISpatialElement CreateSpatialElement()
		{
			return new Shape
			{
				BorderColor = Color.Black,
				Text = ""
			};
		}

		// Token: 0x1700081E RID: 2078
		// (get) Token: 0x06000E59 RID: 3673 RVA: 0x0003E671 File Offset: 0x0003C871
		internal override FieldCollection FieldDefinitions
		{
			get
			{
				return this.m_coreMap.ShapeFields;
			}
		}

		// Token: 0x1700081F RID: 2079
		// (get) Token: 0x06000E5A RID: 3674 RVA: 0x0003E67E File Offset: 0x0003C87E
		protected override NamedCollection SpatialElements
		{
			get
			{
				return this.m_coreMap.Shapes;
			}
		}
	}
}
