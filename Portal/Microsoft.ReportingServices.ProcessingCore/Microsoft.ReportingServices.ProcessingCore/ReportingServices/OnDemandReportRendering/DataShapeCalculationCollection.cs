using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x0200008B RID: 139
	internal sealed class DataShapeCalculationCollection : ReportElementCollectionBase<DataShapeCalculation>
	{
		// Token: 0x060008EA RID: 2282 RVA: 0x00026262 File Offset: 0x00024462
		internal DataShapeCalculationCollection(IReportScope ownerReportScope, List<Calculation> rifCalculations, RenderingContext renderingContext)
		{
			this.m_ownerReportScope = ownerReportScope;
			this.m_rifCalculations = rifCalculations;
			this.m_renderingContext = renderingContext;
			if (rifCalculations != null)
			{
				this.m_calculations = new DataShapeCalculation[rifCalculations.Count];
			}
		}

		// Token: 0x17000596 RID: 1430
		public override DataShapeCalculation this[int index]
		{
			get
			{
				if (this.m_calculations == null || index < 0 || index >= this.Count)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidParameterRange, new object[] { index, 0, this.Count });
				}
				DataShapeCalculation dataShapeCalculation = this.m_calculations[index];
				if (dataShapeCalculation == null)
				{
					dataShapeCalculation = (this.m_calculations[index] = new DataShapeCalculation(this.m_ownerReportScope, this.m_rifCalculations[index], this.m_renderingContext));
				}
				return dataShapeCalculation;
			}
		}

		// Token: 0x17000597 RID: 1431
		// (get) Token: 0x060008EC RID: 2284 RVA: 0x0002631B File Offset: 0x0002451B
		public override int Count
		{
			get
			{
				if (this.m_calculations != null)
				{
					return this.m_calculations.Length;
				}
				return 0;
			}
		}

		// Token: 0x060008ED RID: 2285 RVA: 0x00026330 File Offset: 0x00024530
		internal void SetNewContext()
		{
			if (this.m_calculations != null)
			{
				for (int i = 0; i < this.m_calculations.Length; i++)
				{
					DataShapeCalculation dataShapeCalculation = this.m_calculations[i];
					if (dataShapeCalculation != null)
					{
						dataShapeCalculation.SetNewContext();
					}
				}
			}
		}

		// Token: 0x0400023C RID: 572
		private readonly IReportScope m_ownerReportScope;

		// Token: 0x0400023D RID: 573
		private readonly List<Calculation> m_rifCalculations;

		// Token: 0x0400023E RID: 574
		private readonly RenderingContext m_renderingContext;

		// Token: 0x0400023F RID: 575
		private readonly DataShapeCalculation[] m_calculations;
	}
}
