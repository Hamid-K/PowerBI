using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ReportingServices.ReportIntermediateFormat;

namespace Microsoft.ReportingServices.OnDemandReportRendering
{
	// Token: 0x02000089 RID: 137
	internal sealed class DataShape : DataRegion
	{
		// Token: 0x060008C4 RID: 2244 RVA: 0x00025CB8 File Offset: 0x00023EB8
		internal DataShape(IDefinitionPath parentDefinitionPath, int indexIntoParentCollectionDef, ReportItem rifDataShape, RenderingContext renderingContext)
			: base(parentDefinitionPath, indexIntoParentCollectionDef, rifDataShape, renderingContext)
		{
			this.m_rifDataShape = (DataShape)rifDataShape;
			if (this.RifDataShapeDefinition.Limits != null)
			{
				this.m_limits = new List<DataShapeLimit>();
				foreach (DataShapeLimit dataShapeLimit in this.RifDataShapeDefinition.Limits)
				{
					this.m_limits.Add(this.CreateDataShapeLimit(dataShapeLimit));
				}
			}
		}

		// Token: 0x17000582 RID: 1410
		// (get) Token: 0x060008C5 RID: 2245 RVA: 0x00025D4C File Offset: 0x00023F4C
		internal override bool HasDataCells
		{
			get
			{
				return this.m_dataRows != null;
			}
		}

		// Token: 0x17000583 RID: 1411
		// (get) Token: 0x060008C6 RID: 2246 RVA: 0x00025D57 File Offset: 0x00023F57
		internal override IDataRegionRowCollection RowCollection
		{
			get
			{
				return this.m_dataRows;
			}
		}

		// Token: 0x060008C7 RID: 2247 RVA: 0x00025D5F File Offset: 0x00023F5F
		internal override ReportItemInstance GetOrCreateInstance()
		{
			if (this.m_instance == null)
			{
				this.m_instance = new DataShapeInstance(this);
			}
			return this.m_instance;
		}

		// Token: 0x060008C8 RID: 2248 RVA: 0x00025D7C File Offset: 0x00023F7C
		internal override void SetNewContextChildren()
		{
			if (this.m_instance != null)
			{
				this.m_instance.SetNewContext();
			}
			if (this.m_rowHierarchy != null)
			{
				this.m_rowHierarchy.SetNewContext();
			}
			if (this.m_columnHierarchy != null)
			{
				this.m_columnHierarchy.SetNewContext();
			}
			if (this.m_calculations != null)
			{
				this.m_calculations.SetNewContext();
			}
			if (this.m_dataShapes != null)
			{
				this.m_dataShapes.SetNewContext();
			}
		}

		// Token: 0x17000584 RID: 1412
		// (get) Token: 0x060008C9 RID: 2249 RVA: 0x00025DE8 File Offset: 0x00023FE8
		public string ClientID
		{
			get
			{
				return this.m_rifDataShape.Name;
			}
		}

		// Token: 0x17000585 RID: 1413
		// (get) Token: 0x060008CA RID: 2250 RVA: 0x00025DF5 File Offset: 0x00023FF5
		public DataShapeMemberHierarchy ColumnHierarchy
		{
			get
			{
				if (this.m_columnHierarchy == null)
				{
					this.m_columnHierarchy = new DataShapeMemberHierarchy(this, true);
				}
				return this.m_columnHierarchy;
			}
		}

		// Token: 0x17000586 RID: 1414
		// (get) Token: 0x060008CB RID: 2251 RVA: 0x00025E12 File Offset: 0x00024012
		public DataShapeMemberHierarchy RowHierarchy
		{
			get
			{
				if (this.m_rowHierarchy == null)
				{
					this.m_rowHierarchy = new DataShapeMemberHierarchy(this, false);
				}
				return this.m_rowHierarchy;
			}
		}

		// Token: 0x17000587 RID: 1415
		// (get) Token: 0x060008CC RID: 2252 RVA: 0x00025E2F File Offset: 0x0002402F
		public DataShapeRowCollection DataRows
		{
			get
			{
				if (this.m_dataRows == null)
				{
					this.m_dataRows = new DataShapeRowCollection(this, this.RifDataShapeDefinition.DataRows);
				}
				return this.m_dataRows;
			}
		}

		// Token: 0x17000588 RID: 1416
		// (get) Token: 0x060008CD RID: 2253 RVA: 0x00025E56 File Offset: 0x00024056
		public DataShapeCalculationCollection Calculations
		{
			get
			{
				if (this.m_calculations == null)
				{
					this.m_calculations = new DataShapeCalculationCollection(this, this.RifDataShapeDefinition.Calculations, this.m_renderingContext);
				}
				return this.m_calculations;
			}
		}

		// Token: 0x17000589 RID: 1417
		// (get) Token: 0x060008CE RID: 2254 RVA: 0x00025E83 File Offset: 0x00024083
		public DataShapeCollection DataShapes
		{
			get
			{
				if (this.m_dataShapes == null)
				{
					this.m_dataShapes = new DataShapeCollection(this.RifDataShapeDefinition.DataShapes, this, this.m_renderingContext);
				}
				return this.m_dataShapes;
			}
		}

		// Token: 0x1700058A RID: 1418
		// (get) Token: 0x060008CF RID: 2255 RVA: 0x00025EB0 File Offset: 0x000240B0
		public int? RequestedPrimaryLeafCount
		{
			get
			{
				return this.m_rifDataShape.RequestedPrimaryLeafCount;
			}
		}

		// Token: 0x1700058B RID: 1419
		// (get) Token: 0x060008D0 RID: 2256 RVA: 0x00025EC0 File Offset: 0x000240C0
		internal bool HasExceededRequestedPrimaryLeafCount
		{
			get
			{
				DataShapeInstance dataShapeInstance = (DataShapeInstance)this.GetOrCreateInstance();
				return this.RequestedPrimaryLeafCount != null && dataShapeInstance.PrimaryLeafCount >= this.RequestedPrimaryLeafCount.Value;
			}
		}

		// Token: 0x060008D1 RID: 2257 RVA: 0x00025F04 File Offset: 0x00024104
		public void PrepareRowHierarchy()
		{
			this.m_startedRowHierarchyPass = true;
		}

		// Token: 0x1700058C RID: 1420
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x00025F0D File Offset: 0x0002410D
		internal bool StartedRowHierarchyPass
		{
			get
			{
				return this.m_startedRowHierarchyPass;
			}
		}

		// Token: 0x1700058D RID: 1421
		// (get) Token: 0x060008D3 RID: 2259 RVA: 0x00025F15 File Offset: 0x00024115
		internal bool IsComplete
		{
			get
			{
				return !this.HasExceededRequestedPrimaryLeafCount || !this.m_hasMoreRows;
			}
		}

		// Token: 0x1700058E RID: 1422
		// (get) Token: 0x060008D4 RID: 2260 RVA: 0x00025F2A File Offset: 0x0002412A
		internal DataShapeMessageCollection Messages
		{
			get
			{
				if (this.m_errorMessages == null)
				{
					this.m_errorMessages = new DataShapeMessageCollection(this.m_rifDataShape.ErrorContext.Messages);
				}
				return this.m_errorMessages;
			}
		}

		// Token: 0x060008D5 RID: 2261 RVA: 0x00025F55 File Offset: 0x00024155
		internal void AddMessage(DataShapeErrorMessage message)
		{
			this.m_errorMessages.Add(message);
		}

		// Token: 0x1700058F RID: 1423
		// (get) Token: 0x060008D6 RID: 2262 RVA: 0x00025F63 File Offset: 0x00024163
		internal List<DataShapeLimit> Limits
		{
			get
			{
				return this.m_limits;
			}
		}

		// Token: 0x17000590 RID: 1424
		// (get) Token: 0x060008D7 RID: 2263 RVA: 0x00025F6B File Offset: 0x0002416B
		internal DataShape RifDataShapeDefinition
		{
			get
			{
				return (DataShape)this.m_reportItemDef;
			}
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x00025F78 File Offset: 0x00024178
		internal int GetCurrentMemberCellDefinitionIndex()
		{
			return this.m_memberCellDefinitionIndex;
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x00025F80 File Offset: 0x00024180
		internal int GetAndIncrementMemberCellDefinitionIndex()
		{
			int memberCellDefinitionIndex = this.m_memberCellDefinitionIndex;
			this.m_memberCellDefinitionIndex = memberCellDefinitionIndex + 1;
			return memberCellDefinitionIndex;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x00025F9E File Offset: 0x0002419E
		internal void ResetMemberCellDefinitionIndex(int startIndex)
		{
			this.m_memberCellDefinitionIndex = startIndex;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00025FA7 File Offset: 0x000241A7
		internal DataShapeLimit CreateDataShapeLimit(DataShapeLimit rifLimit)
		{
			return new DataShapeLimit(rifLimit.ID, rifLimit.Operator.TranslateToRom(), rifLimit.Target, rifLimit.Within);
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00025FCC File Offset: 0x000241CC
		internal void ResetColumnHierarchyLimitsWithDataShapeAsWithinScope()
		{
			IEnumerable<DataShapeLimit> columnHierarchyLimitsWithDataShapeAsWithinScope = this.ColumnHierarchyLimitsWithDataShapeAsWithinScope;
			if (columnHierarchyLimitsWithDataShapeAsWithinScope != null)
			{
				foreach (DataShapeLimit dataShapeLimit in columnHierarchyLimitsWithDataShapeAsWithinScope)
				{
					dataShapeLimit.ResetCounter();
				}
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x0002601C File Offset: 0x0002421C
		internal void ResetLimitsWithDataShapeAsWithinScopeExceptColumnHierarchyLimits()
		{
			IEnumerable<DataShapeLimit> limitsWithDataShapeAsWithinScopeExceptColumnHierarchyLimits = this.LimitsWithDataShapeAsWithinScopeExceptColumnHierarchyLimits;
			if (limitsWithDataShapeAsWithinScopeExceptColumnHierarchyLimits != null)
			{
				foreach (DataShapeLimit dataShapeLimit in limitsWithDataShapeAsWithinScopeExceptColumnHierarchyLimits)
				{
					dataShapeLimit.ResetCounter();
					dataShapeLimit.ResetExceeded();
				}
			}
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00026074 File Offset: 0x00024274
		internal void IncrementRequestedPrimaryLeafCount()
		{
			DataShapeInstance dataShapeInstance = (DataShapeInstance)this.GetOrCreateInstance();
			int primaryLeafCount = dataShapeInstance.PrimaryLeafCount;
			dataShapeInstance.PrimaryLeafCount = primaryLeafCount + 1;
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x0002609B File Offset: 0x0002429B
		internal void SetHasMoreRows()
		{
			this.m_hasMoreRows = true;
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x000260A4 File Offset: 0x000242A4
		internal bool HasMoreRows()
		{
			return this.m_hasMoreRows;
		}

		// Token: 0x17000591 RID: 1425
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x000260AC File Offset: 0x000242AC
		private IEnumerable<DataShapeLimit> ColumnHierarchyLimitsWithDataShapeAsWithinScope
		{
			get
			{
				if (this.m_columnHierarchyWithinDataShapeLimits == null && this.m_limits != null)
				{
					HashSet<string> columnHierarchyMemberNames = new HashSet<string>();
					this.GetColumnHierarchyMemberNames(columnHierarchyMemberNames, this.ColumnHierarchy.MemberCollection);
					this.m_columnHierarchyWithinDataShapeLimits = this.m_limits.Where((DataShapeLimit l) => l.Within == this.ClientID && columnHierarchyMemberNames.Contains(l.Target));
				}
				return this.m_columnHierarchyWithinDataShapeLimits;
			}
		}

		// Token: 0x17000592 RID: 1426
		// (get) Token: 0x060008E2 RID: 2274 RVA: 0x0002611C File Offset: 0x0002431C
		private IEnumerable<DataShapeLimit> LimitsWithDataShapeAsWithinScopeExceptColumnHierarchyLimits
		{
			get
			{
				if (this.m_limitsWithDataShapeAsWithinScopeExceptColumnHierarchyLimits == null && this.m_limits != null)
				{
					IEnumerable<DataShapeLimit> enumerable = this.ColumnHierarchyLimitsWithDataShapeAsWithinScope ?? Enumerable.Empty<DataShapeLimit>();
					this.m_limitsWithDataShapeAsWithinScopeExceptColumnHierarchyLimits = this.m_limits.Where((DataShapeLimit l) => l.Within == this.ClientID).Except(enumerable);
				}
				return this.m_limitsWithDataShapeAsWithinScopeExceptColumnHierarchyLimits;
			}
		}

		// Token: 0x060008E3 RID: 2275 RVA: 0x00026174 File Offset: 0x00024374
		private void GetColumnHierarchyMemberNames(HashSet<string> memberNames, DataShapeMemberCollection memberCollection)
		{
			if (memberCollection == null)
			{
				return;
			}
			foreach (DataShapeMember dataShapeMember in memberCollection)
			{
				memberNames.Add(dataShapeMember.RifDataShapeMemberDefinition.Name);
				DataShapeMemberCollection children = dataShapeMember.Children;
				if (children != null)
				{
					this.GetColumnHierarchyMemberNames(memberNames, children);
				}
			}
		}

		// Token: 0x0400022B RID: 555
		private readonly DataShape m_rifDataShape;

		// Token: 0x0400022C RID: 556
		private DataShapeMemberHierarchy m_columnHierarchy;

		// Token: 0x0400022D RID: 557
		private DataShapeMemberHierarchy m_rowHierarchy;

		// Token: 0x0400022E RID: 558
		private DataShapeRowCollection m_dataRows;

		// Token: 0x0400022F RID: 559
		private DataShapeCalculationCollection m_calculations;

		// Token: 0x04000230 RID: 560
		private DataShapeCollection m_dataShapes;

		// Token: 0x04000231 RID: 561
		private DataShapeMessageCollection m_errorMessages;

		// Token: 0x04000232 RID: 562
		private int m_memberCellDefinitionIndex;

		// Token: 0x04000233 RID: 563
		private readonly List<DataShapeLimit> m_limits;

		// Token: 0x04000234 RID: 564
		private IEnumerable<DataShapeLimit> m_columnHierarchyWithinDataShapeLimits;

		// Token: 0x04000235 RID: 565
		private IEnumerable<DataShapeLimit> m_limitsWithDataShapeAsWithinScopeExceptColumnHierarchyLimits;

		// Token: 0x04000236 RID: 566
		private bool m_startedRowHierarchyPass;

		// Token: 0x04000237 RID: 567
		private bool m_hasMoreRows;
	}
}
