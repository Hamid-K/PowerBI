using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200008D RID: 141
	internal sealed class DataShapeCollection : ReportElementCollectionBase<DataShape>
	{
		// Token: 0x060008F2 RID: 2290 RVA: 0x000263FA File Offset: 0x000245FA
		internal DataShapeCollection(List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> rifDataShapes, IDefinitionPath parentDefinitionPath, RenderingContext renderingContext)
		{
			this.m_rifDataShapes = rifDataShapes;
			this.m_parentDefinitionPath = parentDefinitionPath;
			this.m_renderingContext = renderingContext;
			if (rifDataShapes != null)
			{
				this.m_dataShapes = new DataShape[rifDataShapes.Count];
			}
		}

		// Token: 0x17000599 RID: 1433
		public override DataShape this[int index]
		{
			get
			{
				if (index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				DataShape dataShape = this.m_dataShapes[index];
				if (dataShape == null)
				{
					dataShape = (this.m_dataShapes[index] = new DataShape(this.m_parentDefinitionPath, index, this.m_rifDataShapes[index], this.m_renderingContext));
				}
				return dataShape;
			}
		}

		// Token: 0x1700059A RID: 1434
		// (get) Token: 0x060008F4 RID: 2292 RVA: 0x000264AC File Offset: 0x000246AC
		public override int Count
		{
			get
			{
				if (this.m_dataShapes != null)
				{
					return this.m_dataShapes.Length;
				}
				return 0;
			}
		}

		// Token: 0x060008F5 RID: 2293 RVA: 0x000264C0 File Offset: 0x000246C0
		internal void SetNewContext()
		{
			for (int i = 0; i < this.m_dataShapes.Length; i++)
			{
				this.m_dataShapes[i].SetNewContext();
			}
		}

		// Token: 0x04000244 RID: 580
		private readonly List<Microsoft.ReportingServices.ReportIntermediateFormat.ReportItem> m_rifDataShapes;

		// Token: 0x04000245 RID: 581
		private readonly IDefinitionPath m_parentDefinitionPath;

		// Token: 0x04000246 RID: 582
		private readonly RenderingContext m_renderingContext;

		// Token: 0x04000247 RID: 583
		private readonly DataShape[] m_dataShapes;
	}
}
