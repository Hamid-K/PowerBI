using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020003C3 RID: 963
	[SkipStaticValidation]
	[NonPersistent]
	internal sealed class DataShape : DataRegion
	{
		// Token: 0x060026E1 RID: 9953 RVA: 0x000B9EA1 File Offset: 0x000B80A1
		internal DataShape(ReportItem parent)
			: base(parent)
		{
		}

		// Token: 0x060026E2 RID: 9954 RVA: 0x000B9EAA File Offset: 0x000B80AA
		internal DataShape(int id, ReportItem parent)
			: base(id, parent)
		{
		}

		// Token: 0x170013EB RID: 5099
		// (get) Token: 0x060026E3 RID: 9955 RVA: 0x000B9EB4 File Offset: 0x000B80B4
		internal override Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.DataShape;
			}
		}

		// Token: 0x170013EC RID: 5100
		// (get) Token: 0x060026E4 RID: 9956 RVA: 0x000B9EB8 File Offset: 0x000B80B8
		internal override HierarchyNodeList ColumnMembers
		{
			get
			{
				return this.m_dataColumnMembers;
			}
		}

		// Token: 0x170013ED RID: 5101
		// (get) Token: 0x060026E5 RID: 9957 RVA: 0x000B9EC0 File Offset: 0x000B80C0
		internal override HierarchyNodeList RowMembers
		{
			get
			{
				return this.m_dataRowMembers;
			}
		}

		// Token: 0x170013EE RID: 5102
		// (get) Token: 0x060026E6 RID: 9958 RVA: 0x000B9EC8 File Offset: 0x000B80C8
		internal override RowList Rows
		{
			get
			{
				return this.m_dataRows;
			}
		}

		// Token: 0x170013EF RID: 5103
		// (get) Token: 0x060026E7 RID: 9959 RVA: 0x000B9ED0 File Offset: 0x000B80D0
		// (set) Token: 0x060026E8 RID: 9960 RVA: 0x000B9ED8 File Offset: 0x000B80D8
		internal DataShapeMemberList SecondaryHierarchy
		{
			get
			{
				return this.m_dataColumnMembers;
			}
			set
			{
				this.m_dataColumnMembers = value;
			}
		}

		// Token: 0x170013F0 RID: 5104
		// (get) Token: 0x060026E9 RID: 9961 RVA: 0x000B9EE1 File Offset: 0x000B80E1
		// (set) Token: 0x060026EA RID: 9962 RVA: 0x000B9EE9 File Offset: 0x000B80E9
		internal DataShapeMemberList PrimaryHierarchy
		{
			get
			{
				return this.m_dataRowMembers;
			}
			set
			{
				this.m_dataRowMembers = value;
			}
		}

		// Token: 0x170013F1 RID: 5105
		// (get) Token: 0x060026EB RID: 9963 RVA: 0x000B9EF2 File Offset: 0x000B80F2
		// (set) Token: 0x060026EC RID: 9964 RVA: 0x000B9EFA File Offset: 0x000B80FA
		internal DataShapeRowList DataRows
		{
			get
			{
				return this.m_dataRows;
			}
			set
			{
				this.m_dataRows = value;
			}
		}

		// Token: 0x170013F2 RID: 5106
		// (get) Token: 0x060026ED RID: 9965 RVA: 0x000B9F03 File Offset: 0x000B8103
		// (set) Token: 0x060026EE RID: 9966 RVA: 0x000B9F0B File Offset: 0x000B810B
		internal List<Calculation> Calculations
		{
			get
			{
				return this.m_calculations;
			}
			set
			{
				this.m_calculations = value;
			}
		}

		// Token: 0x170013F3 RID: 5107
		// (get) Token: 0x060026EF RID: 9967 RVA: 0x000B9F14 File Offset: 0x000B8114
		internal DataShapeErrorContext ErrorContext
		{
			get
			{
				if (this.m_errorContext == null)
				{
					this.m_errorContext = new DataShapeErrorContext();
				}
				return this.m_errorContext;
			}
		}

		// Token: 0x170013F4 RID: 5108
		// (get) Token: 0x060026F0 RID: 9968 RVA: 0x000B9F2F File Offset: 0x000B812F
		// (set) Token: 0x060026F1 RID: 9969 RVA: 0x000B9F37 File Offset: 0x000B8137
		internal List<ReportItem> DataShapes
		{
			get
			{
				return this.m_dataShapes;
			}
			set
			{
				this.m_dataShapes = value;
			}
		}

		// Token: 0x170013F5 RID: 5109
		// (get) Token: 0x060026F2 RID: 9970 RVA: 0x000B9F40 File Offset: 0x000B8140
		// (set) Token: 0x060026F3 RID: 9971 RVA: 0x000B9F48 File Offset: 0x000B8148
		internal int? RequestedPrimaryLeafCount
		{
			get
			{
				return this.m_requestedPrimaryLeafCount;
			}
			set
			{
				this.m_requestedPrimaryLeafCount = value;
			}
		}

		// Token: 0x170013F6 RID: 5110
		// (get) Token: 0x060026F4 RID: 9972 RVA: 0x000B9F51 File Offset: 0x000B8151
		// (set) Token: 0x060026F5 RID: 9973 RVA: 0x000B9F59 File Offset: 0x000B8159
		internal List<DataShapeLimit> Limits
		{
			get
			{
				return this.m_limits;
			}
			set
			{
				this.m_limits = value;
			}
		}

		// Token: 0x170013F7 RID: 5111
		// (get) Token: 0x060026F6 RID: 9974 RVA: 0x000B9F62 File Offset: 0x000B8162
		// (set) Token: 0x060026F7 RID: 9975 RVA: 0x000B9F6A File Offset: 0x000B816A
		internal List<RestartDefinition> RestartDefinitions
		{
			get
			{
				return this.m_restartDefinitions;
			}
			set
			{
				this.m_restartDefinitions = value;
			}
		}

		// Token: 0x170013F8 RID: 5112
		// (get) Token: 0x060026F8 RID: 9976 RVA: 0x000B9F74 File Offset: 0x000B8174
		internal HashSet<DataShapeMember> RestartMembers
		{
			get
			{
				if (this.m_restartMembers == null && this.m_restartDefinitions != null)
				{
					this.m_restartMembers = new HashSet<DataShapeMember>(this.m_restartDefinitions.Select((RestartDefinition def) => def.DataMember));
				}
				return this.m_restartMembers;
			}
		}

		// Token: 0x060026F9 RID: 9977 RVA: 0x000B9FCC File Offset: 0x000B81CC
		internal override object EvaluateNoRowsMessageExpression()
		{
			Global.Tracer.Assert(false, "EvaluateNoRowsMessageExpression should never be called for data shape processing.");
			throw new InvalidOperationException();
		}

		// Token: 0x060026FA RID: 9978 RVA: 0x000B9FE3 File Offset: 0x000B81E3
		internal override void CalculateSizes(double width, double height, InitializationContext context, bool overwrite)
		{
			Global.Tracer.Assert(false, "CalculateSizes should never be called for data shape processing.");
		}

		// Token: 0x060026FB RID: 9979 RVA: 0x000B9FF5 File Offset: 0x000B81F5
		protected override void AddInScopeTextBox(TextBox textbox)
		{
			Global.Tracer.Assert(false, "AddInScopeTextBox should never be called for data shape processing.");
		}

		// Token: 0x060026FC RID: 9980 RVA: 0x000BA007 File Offset: 0x000B8207
		protected override Cell CreateCell(int id, int rowIndex, int colIndex)
		{
			return new DataShapeIntersection(id, this);
		}

		// Token: 0x060026FD RID: 9981 RVA: 0x000BA010 File Offset: 0x000B8210
		public override void CreateDomainScopeMember(ReportHierarchyNode parentNode, Grouping grouping, AutomaticSubtotalContext context)
		{
			Global.Tracer.Assert(false, "CreateDomainScopeMember should never be called for data shape processing.");
		}

		// Token: 0x060026FE RID: 9982 RVA: 0x000BA022 File Offset: 0x000B8222
		protected override void CreateDomainScopeRowsAndCells(AutomaticSubtotalContext context, ReportHierarchyNode member)
		{
			Global.Tracer.Assert(false, "CreateDomainScopeRowsAndCells should never be called for data shape processing.");
		}

		// Token: 0x060026FF RID: 9983 RVA: 0x000BA034 File Offset: 0x000B8234
		protected override ReportHierarchyNode CreateHierarchyNode(int id)
		{
			return new DataShapeMember(id, this);
		}

		// Token: 0x06002700 RID: 9984 RVA: 0x000BA03D File Offset: 0x000B823D
		protected override Row CreateRow(int id, int columnCount)
		{
			return new DataShapeRow(id);
		}

		// Token: 0x06002701 RID: 9985 RVA: 0x000BA045 File Offset: 0x000B8245
		protected override void DataRendererInitialize(InitializationContext context)
		{
		}

		// Token: 0x06002702 RID: 9986 RVA: 0x000BA047 File Offset: 0x000B8247
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			Global.Tracer.Assert(false, "Deserialize should never be called for data shape processing.");
		}

		// Token: 0x06002703 RID: 9987 RVA: 0x000BA05C File Offset: 0x000B825C
		internal override void DetermineGroupingExprValueCount(InitializationContext context, int groupingExprCount)
		{
			base.DetermineGroupingExprValueCount(context, groupingExprCount);
			if (this.m_dataShapes != null)
			{
				for (int i = 0; i < this.m_dataShapes.Count; i++)
				{
					this.m_dataShapes[i].DetermineGroupingExprValueCount(context, groupingExprCount);
				}
			}
		}

		// Token: 0x06002704 RID: 9988 RVA: 0x000BA0A4 File Offset: 0x000B82A4
		protected override void DetermineGroupingExprValueCount(int outerIndex, int innerIndex, InitializationContext context, int groupingExprCount)
		{
			int num;
			int num2;
			if (this.m_processingInnerGrouping == DataRegion.ProcessingInnerGroupings.Row)
			{
				num = outerIndex;
				num2 = innerIndex;
			}
			else
			{
				num = innerIndex;
				num2 = outerIndex;
			}
			if (this.m_dataRows != null)
			{
				DataShapeIntersection dataShapeIntersection = this.m_dataRows[num2].DataShapeIntersections[num];
				if (dataShapeIntersection != null)
				{
					dataShapeIntersection.DetermineGroupingExprValueCount(context, groupingExprCount);
				}
			}
		}

		// Token: 0x06002705 RID: 9989 RVA: 0x000BA0F0 File Offset: 0x000B82F0
		internal override bool Initialize(InitializationContext context)
		{
			context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion;
			context.ObjectType = this.ObjectType;
			context.ObjectName = this.m_name;
			if (!context.RegisterDataRegion(this))
			{
				return false;
			}
			context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataSet | Microsoft.ReportingServices.ReportPublishing.LocationFlags.InDataRegion;
			context.ExprHostBuilder.DataRegionStart(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.DataShape, this.m_name);
			base.Initialize(context);
			if (this.m_dataShapes != null)
			{
				for (int i = 0; i < this.m_dataShapes.Count; i++)
				{
					this.m_dataShapes[i].Initialize(context);
				}
			}
			if (this.m_calculations != null)
			{
				for (int j = 0; j < this.m_calculations.Count; j++)
				{
					this.m_calculations[j].Initialize(context);
				}
			}
			context.ExprHostBuilder.DataRegionEnd(Microsoft.ReportingServices.RdlExpressions.ExprHostBuilder.DataRegionMode.DataShape);
			context.UnRegisterDataRegion(this);
			return false;
		}

		// Token: 0x06002706 RID: 9990 RVA: 0x000BA1D4 File Offset: 0x000B83D4
		protected override bool InitializeColumnMembers(InitializationContext context)
		{
			return this.ColumnMembers == null || this.ColumnMembers.Count == 0 || base.InitializeColumnMembers(context);
		}

		// Token: 0x06002707 RID: 9991 RVA: 0x000BA1F4 File Offset: 0x000B83F4
		protected override bool InitializeRowMembers(InitializationContext context)
		{
			return this.RowMembers == null || this.RowMembers.Count == 0 || base.InitializeRowMembers(context);
		}

		// Token: 0x06002708 RID: 9992 RVA: 0x000BA214 File Offset: 0x000B8414
		protected override void InitializeData(InitializationContext context)
		{
			if (this.Rows != null && this.Rows.Count > 0)
			{
				base.InitializeData(context);
			}
		}

		// Token: 0x06002709 RID: 9993 RVA: 0x000BA234 File Offset: 0x000B8434
		protected override bool InitializeRows(InitializationContext context)
		{
			if ((this.RowMembers == null || this.ColumnMembers != null) && (this.ColumnMembers == null || this.RowMembers != null))
			{
				return base.InitializeRows(context);
			}
			if (this.Rows != null && this.Rows.Count > 0)
			{
				context.ErrorContext.Register(ProcessingErrorCode.rsSingleHierarchyWithDataRows, Severity.Error, context.ObjectType, context.ObjectName, null, Array.Empty<string>());
				return false;
			}
			return true;
		}

		// Token: 0x0600270A RID: 9994 RVA: 0x000BA2A9 File Offset: 0x000B84A9
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Global.Tracer.Assert(false, "PublishClone should never be called for data shape processing.");
			throw new InvalidOperationException();
		}

		// Token: 0x0600270B RID: 9995 RVA: 0x000BA2C0 File Offset: 0x000B84C0
		internal override void ResetTextBoxImpls(OnDemandProcessingContext context)
		{
			Global.Tracer.Assert(false, "ResetTextBoxImpls should never be called for data shape processing.");
		}

		// Token: 0x0600270C RID: 9996 RVA: 0x000BA2D2 File Offset: 0x000B84D2
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false, "ResolveReferences should never be called for data shape processing.");
		}

		// Token: 0x0600270D RID: 9997 RVA: 0x000BA2E4 File Offset: 0x000B84E4
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			Global.Tracer.Assert(false, "Serialize should never be called for data shape processing.");
		}

		// Token: 0x0600270E RID: 9998 RVA: 0x000BA2F6 File Offset: 0x000B84F6
		internal override void SetupCriRenderItemDef(ReportItem reportItem)
		{
			Global.Tracer.Assert(false, "SetupCriRenderItemDef should never be called for data shape processing.");
		}

		// Token: 0x0600270F RID: 9999 RVA: 0x000BA308 File Offset: 0x000B8508
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			Global.Tracer.Assert(false, "GetObjectType should never be called for data shape processing.");
			throw new InvalidOperationException();
		}

		// Token: 0x06002710 RID: 10000 RVA: 0x000BA320 File Offset: 0x000B8520
		protected override void TraverseDataRegionLevelScopes(IRIFScopeVisitor visitor)
		{
			if (this.m_dataShapes != null)
			{
				for (int i = 0; i < this.m_dataShapes.Count; i++)
				{
					this.m_dataShapes[i].TraverseScopes(visitor);
				}
			}
		}

		// Token: 0x06002711 RID: 10001 RVA: 0x000BA35D File Offset: 0x000B855D
		internal override void DataRegionContentsSetExprHost(ObjectModelImpl reportObjectModel, bool traverseDataRegions)
		{
			Global.Tracer.Assert(false, "DataRegionContentsSetExprHost should never be called for data shape processing.");
		}

		// Token: 0x170013F9 RID: 5113
		// (get) Token: 0x06002712 RID: 10002 RVA: 0x000BA36F File Offset: 0x000B856F
		protected override IndexedExprHost UserSortExpressionsHost
		{
			get
			{
				Global.Tracer.Assert(false, "UserSortExpressionsHost should never be called for data shape processing.");
				throw new InvalidOperationException();
			}
		}

		// Token: 0x06002713 RID: 10003 RVA: 0x000BA386 File Offset: 0x000B8586
		internal override void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			Global.Tracer.Assert(false, "SetExprHost should never be called for data shape processing.");
		}

		// Token: 0x06002714 RID: 10004 RVA: 0x000BA398 File Offset: 0x000B8598
		protected override bool ValidateInnerStructure(InitializationContext context)
		{
			return true;
		}

		// Token: 0x06002715 RID: 10005 RVA: 0x000BA39C File Offset: 0x000B859C
		protected override List<ReportItem> ComputeDataRegionScopedItemsForDataProcessing()
		{
			List<ReportItem> list = base.ComputeDataRegionScopedItemsForDataProcessing();
			if (this.m_dataShapes != null && this.m_dataShapes.Count > 0)
			{
				if (list == null)
				{
					list = new List<ReportItem>(this.m_dataShapes);
				}
				else
				{
					list.AddRange(this.m_dataShapes);
				}
			}
			return list;
		}

		// Token: 0x04001667 RID: 5735
		private DataShapeMemberList m_dataColumnMembers;

		// Token: 0x04001668 RID: 5736
		private DataShapeMemberList m_dataRowMembers;

		// Token: 0x04001669 RID: 5737
		private DataShapeRowList m_dataRows;

		// Token: 0x0400166A RID: 5738
		private List<Calculation> m_calculations;

		// Token: 0x0400166B RID: 5739
		private List<ReportItem> m_dataShapes;

		// Token: 0x0400166C RID: 5740
		private List<DataShapeLimit> m_limits;

		// Token: 0x0400166D RID: 5741
		private int? m_requestedPrimaryLeafCount;

		// Token: 0x0400166E RID: 5742
		private DataShapeErrorContext m_errorContext;

		// Token: 0x0400166F RID: 5743
		private List<RestartDefinition> m_restartDefinitions;

		// Token: 0x04001670 RID: 5744
		private HashSet<DataShapeMember> m_restartMembers;
	}
}
