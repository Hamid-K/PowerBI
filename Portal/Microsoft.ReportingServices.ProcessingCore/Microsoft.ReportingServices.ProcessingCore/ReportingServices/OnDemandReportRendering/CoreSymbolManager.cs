using System;
using System.Drawing;
using System.Globalization;
using Microsoft.Reporting.Map.WebForms;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200015B RID: 347
	internal class CoreSymbolManager : CoreSpatialElementManager
	{
		// Token: 0x06000E5B RID: 3675 RVA: 0x0003E68B File Offset: 0x0003C88B
		internal CoreSymbolManager(MapControl mapControl, MapVectorLayer mapVectorLayer)
			: base(mapControl, mapVectorLayer)
		{
		}

		// Token: 0x06000E5C RID: 3676 RVA: 0x0003E698 File Offset: 0x0003C898
		internal override void AddSpatialElement(ISpatialElement spatialElement)
		{
			((NamedElement)spatialElement).Name = this.m_coreMap.Symbols.Count.ToString(CultureInfo.InvariantCulture);
			this.m_coreMap.Symbols.Add((Symbol)spatialElement);
		}

		// Token: 0x06000E5D RID: 3677 RVA: 0x0003E6E4 File Offset: 0x0003C8E4
		internal override void RemoveSpatialElement(ISpatialElement spatialElement)
		{
			this.m_coreMap.Symbols.Remove((Symbol)spatialElement);
		}

		// Token: 0x06000E5E RID: 3678 RVA: 0x0003E6FC File Offset: 0x0003C8FC
		internal override ISpatialElement CreateSpatialElement()
		{
			return new Symbol
			{
				BorderColor = Color.Black,
				Text = ""
			};
		}

		// Token: 0x17000820 RID: 2080
		// (get) Token: 0x06000E5F RID: 3679 RVA: 0x0003E719 File Offset: 0x0003C919
		protected override NamedCollection SpatialElements
		{
			get
			{
				return this.m_coreMap.Symbols;
			}
		}

		// Token: 0x17000821 RID: 2081
		// (get) Token: 0x06000E60 RID: 3680 RVA: 0x0003E726 File Offset: 0x0003C926
		internal override FieldCollection FieldDefinitions
		{
			get
			{
				return this.m_coreMap.SymbolFields;
			}
		}
	}
}
