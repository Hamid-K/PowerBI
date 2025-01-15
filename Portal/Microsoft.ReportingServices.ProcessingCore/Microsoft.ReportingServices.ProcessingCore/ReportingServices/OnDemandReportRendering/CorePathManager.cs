using System;
using System.Drawing;
using System.Globalization;
using Microsoft.Reporting.Map.WebForms;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200015C RID: 348
	internal class CorePathManager : CoreSpatialElementManager
	{
		// Token: 0x06000E61 RID: 3681 RVA: 0x0003E733 File Offset: 0x0003C933
		internal CorePathManager(MapControl mapControl, MapVectorLayer mapVectorLayer)
			: base(mapControl, mapVectorLayer)
		{
		}

		// Token: 0x06000E62 RID: 3682 RVA: 0x0003E740 File Offset: 0x0003C940
		internal override void AddSpatialElement(ISpatialElement spatialElement)
		{
			((NamedElement)spatialElement).Name = this.m_coreMap.Paths.Count.ToString(CultureInfo.InvariantCulture);
			this.m_coreMap.Paths.Add((Path)spatialElement);
		}

		// Token: 0x06000E63 RID: 3683 RVA: 0x0003E78C File Offset: 0x0003C98C
		internal override void RemoveSpatialElement(ISpatialElement spatialElement)
		{
			this.m_coreMap.Paths.Remove((Path)spatialElement);
		}

		// Token: 0x06000E64 RID: 3684 RVA: 0x0003E7A4 File Offset: 0x0003C9A4
		internal override ISpatialElement CreateSpatialElement()
		{
			Path path = new Path();
			path.BorderColor = Color.Black;
			path.Text = "";
			return new Path();
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x06000E65 RID: 3685 RVA: 0x0003E7C5 File Offset: 0x0003C9C5
		protected override NamedCollection SpatialElements
		{
			get
			{
				return this.m_coreMap.Paths;
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x06000E66 RID: 3686 RVA: 0x0003E7D2 File Offset: 0x0003C9D2
		internal override FieldCollection FieldDefinitions
		{
			get
			{
				return this.m_coreMap.PathFields;
			}
		}
	}
}
