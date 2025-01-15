using System;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000090 RID: 144
	internal sealed class DataShapeIntersection : IDataRegionCell, IDefinitionPath, IReportScope
	{
		// Token: 0x06000907 RID: 2311 RVA: 0x00026687 File Offset: 0x00024887
		internal DataShapeIntersection(DataShape owner, DataShapeIntersection rifIntersection, int rowIndex, int colIndex, DataShapeLimit limit)
		{
			this.m_owner = owner;
			this.m_rifIntersection = rifIntersection;
			this.m_rowIndex = rowIndex;
			this.m_columnIndex = colIndex;
			this.m_limit = limit;
		}

		// Token: 0x1700059F RID: 1439
		// (get) Token: 0x06000908 RID: 2312 RVA: 0x000266B4 File Offset: 0x000248B4
		IReportScopeInstance IReportScope.ReportScopeInstance
		{
			get
			{
				return this.Instance;
			}
		}

		// Token: 0x170005A0 RID: 1440
		// (get) Token: 0x06000909 RID: 2313 RVA: 0x000266BC File Offset: 0x000248BC
		IRIFReportScope IReportScope.RIFReportScope
		{
			get
			{
				return this.m_rifIntersection;
			}
		}

		// Token: 0x170005A1 RID: 1441
		// (get) Token: 0x0600090A RID: 2314 RVA: 0x000266C4 File Offset: 0x000248C4
		public string DefinitionPath
		{
			get
			{
				if (this.m_definitionPath == null)
				{
					this.m_definitionPath = DefinitionPathConstants.GetTablixCellDefinitionPath(this.m_owner, this.m_rowIndex, this.m_columnIndex, true);
				}
				return this.m_definitionPath;
			}
		}

		// Token: 0x170005A2 RID: 1442
		// (get) Token: 0x0600090B RID: 2315 RVA: 0x000266F2 File Offset: 0x000248F2
		public IDefinitionPath ParentDefinitionPath
		{
			get
			{
				return this.m_owner;
			}
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x000266FA File Offset: 0x000248FA
		void IDataRegionCell.SetNewContext()
		{
			this.SetNewContext();
		}

		// Token: 0x170005A3 RID: 1443
		// (get) Token: 0x0600090D RID: 2317 RVA: 0x00026702 File Offset: 0x00024902
		internal string ID
		{
			get
			{
				return this.m_rifIntersection.RenderingModelID;
			}
		}

		// Token: 0x170005A4 RID: 1444
		// (get) Token: 0x0600090E RID: 2318 RVA: 0x0002670F File Offset: 0x0002490F
		internal string ClientID
		{
			get
			{
				return this.m_rifIntersection.Name;
			}
		}

		// Token: 0x170005A5 RID: 1445
		// (get) Token: 0x0600090F RID: 2319 RVA: 0x0002671C File Offset: 0x0002491C
		public DataShapeCalculationCollection Calculations
		{
			get
			{
				if (this.m_calculations == null)
				{
					this.m_calculations = new DataShapeCalculationCollection(this, this.RifDataShapeIntersection.Calculations, this.m_owner.RenderingContext);
				}
				return this.m_calculations;
			}
		}

		// Token: 0x170005A6 RID: 1446
		// (get) Token: 0x06000910 RID: 2320 RVA: 0x0002674E File Offset: 0x0002494E
		public DataShapeCollection DataShapes
		{
			get
			{
				if (this.m_dataShapes == null)
				{
					this.m_dataShapes = new DataShapeCollection(this.RifDataShapeIntersection.DataShapes, this, this.m_owner.RenderingContext);
				}
				return this.m_dataShapes;
			}
		}

		// Token: 0x170005A7 RID: 1447
		// (get) Token: 0x06000911 RID: 2321 RVA: 0x00026780 File Offset: 0x00024980
		internal DataShapeIntersection RifDataShapeIntersection
		{
			get
			{
				return this.m_rifIntersection;
			}
		}

		// Token: 0x170005A8 RID: 1448
		// (get) Token: 0x06000912 RID: 2322 RVA: 0x00026788 File Offset: 0x00024988
		internal DataShapeIntersectionInstance Instance
		{
			get
			{
				if (this.m_instance == null)
				{
					this.m_instance = new DataShapeIntersectionInstance(this);
				}
				return this.m_instance;
			}
		}

		// Token: 0x170005A9 RID: 1449
		// (get) Token: 0x06000913 RID: 2323 RVA: 0x000267A4 File Offset: 0x000249A4
		internal DataShapeLimit Limit
		{
			get
			{
				return this.m_limit;
			}
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x000267AC File Offset: 0x000249AC
		internal void SetNewContext()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_calculations != null)
			{
				this.m_calculations.SetNewContext();
			}
			if (this.m_dataShapes != null)
			{
				this.m_dataShapes.SetNewContext();
			}
			if (this.m_rifIntersection != null)
			{
				this.m_rifIntersection.ClearStreamingScopeInstanceBinding();
			}
		}

		// Token: 0x0400024A RID: 586
		private readonly DataShape m_owner;

		// Token: 0x0400024B RID: 587
		private readonly DataShapeIntersection m_rifIntersection;

		// Token: 0x0400024C RID: 588
		private readonly int m_rowIndex;

		// Token: 0x0400024D RID: 589
		private readonly int m_columnIndex;

		// Token: 0x0400024E RID: 590
		private readonly DataShapeLimit m_limit;

		// Token: 0x0400024F RID: 591
		private DataShapeCalculationCollection m_calculations;

		// Token: 0x04000250 RID: 592
		private DataShapeCollection m_dataShapes;

		// Token: 0x04000251 RID: 593
		private DataShapeIntersectionInstance m_instance;

		// Token: 0x04000252 RID: 594
		private string m_definitionPath;
	}
}
