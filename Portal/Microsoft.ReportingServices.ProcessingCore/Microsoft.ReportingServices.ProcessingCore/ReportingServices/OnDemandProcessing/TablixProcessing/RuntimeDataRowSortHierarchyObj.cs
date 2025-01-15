using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008C2 RID: 2242
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeDataRowSortHierarchyObj : IHierarchyObj, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007ABD RID: 31421 RVA: 0x001F9A9B File Offset: 0x001F7C9B
		internal RuntimeDataRowSortHierarchyObj()
		{
		}

		// Token: 0x06007ABE RID: 31422 RVA: 0x001F9AA4 File Offset: 0x001F7CA4
		internal RuntimeDataRowSortHierarchyObj(IHierarchyObj outerHierarchy, int depth)
		{
			this.m_hierarchyRoot = outerHierarchy.HierarchyRoot;
			int num = outerHierarchy.ExpressionIndex + 1;
			Microsoft.ReportingServices.ReportIntermediateFormat.Sorting sortingDef = ((IDataRowSortOwner)this.m_hierarchyRoot.Value()).SortingDef;
			if (sortingDef.SortExpressions == null || num >= sortingDef.SortExpressions.Count)
			{
				this.m_dataRowHolder = new RuntimeSortDataHolder();
				return;
			}
			this.m_sortExpression = new RuntimeExpressionInfo(sortingDef.SortExpressions, sortingDef.ExprHost, sortingDef.SortDirections, num);
			this.m_sortTree = new BTree(this, this.OdpContext, depth);
		}

		// Token: 0x1700285E RID: 10334
		// (get) Token: 0x06007ABF RID: 31423 RVA: 0x001F9B35 File Offset: 0x001F7D35
		public int Depth
		{
			get
			{
				return this.m_hierarchyRoot.Value().Depth + 1;
			}
		}

		// Token: 0x1700285F RID: 10335
		// (get) Token: 0x06007AC0 RID: 31424 RVA: 0x001F9B49 File Offset: 0x001F7D49
		IReference<IHierarchyObj> IHierarchyObj.HierarchyRoot
		{
			get
			{
				return this.m_hierarchyRoot;
			}
		}

		// Token: 0x17002860 RID: 10336
		// (get) Token: 0x06007AC1 RID: 31425 RVA: 0x001F9B51 File Offset: 0x001F7D51
		public OnDemandProcessingContext OdpContext
		{
			get
			{
				return this.m_hierarchyRoot.Value().OdpContext;
			}
		}

		// Token: 0x17002861 RID: 10337
		// (get) Token: 0x06007AC2 RID: 31426 RVA: 0x001F9B63 File Offset: 0x001F7D63
		BTree IHierarchyObj.SortTree
		{
			get
			{
				return this.m_sortTree;
			}
		}

		// Token: 0x17002862 RID: 10338
		// (get) Token: 0x06007AC3 RID: 31427 RVA: 0x001F9B6B File Offset: 0x001F7D6B
		int IHierarchyObj.ExpressionIndex
		{
			get
			{
				Global.Tracer.Assert(this.m_sortExpression != null, "m_sortExpression != null");
				return this.m_sortExpression.ExpressionIndex;
			}
		}

		// Token: 0x17002863 RID: 10339
		// (get) Token: 0x06007AC4 RID: 31428 RVA: 0x001F9B90 File Offset: 0x001F7D90
		List<int> IHierarchyObj.SortFilterInfoIndices
		{
			get
			{
				Global.Tracer.Assert(false, "SortFilterInfoIndices should not be called on this type");
				return null;
			}
		}

		// Token: 0x17002864 RID: 10340
		// (get) Token: 0x06007AC5 RID: 31429 RVA: 0x001F9BA3 File Offset: 0x001F7DA3
		bool IHierarchyObj.IsDetail
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17002865 RID: 10341
		// (get) Token: 0x06007AC6 RID: 31430 RVA: 0x001F9BA6 File Offset: 0x001F7DA6
		bool IHierarchyObj.InDataRowSortPhase
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06007AC7 RID: 31431 RVA: 0x001F9BA9 File Offset: 0x001F7DA9
		IHierarchyObj IHierarchyObj.CreateHierarchyObjForSortTree()
		{
			return new RuntimeDataRowSortHierarchyObj(this, this.Depth + 1);
		}

		// Token: 0x06007AC8 RID: 31432 RVA: 0x001F9BB9 File Offset: 0x001F7DB9
		ProcessingMessageList IHierarchyObj.RegisterComparisonError(string propertyName)
		{
			return this.m_hierarchyRoot.Value().RegisterComparisonError(propertyName);
		}

		// Token: 0x06007AC9 RID: 31433 RVA: 0x001F9BCC File Offset: 0x001F7DCC
		void IHierarchyObj.NextRow(IHierarchyObj owner)
		{
			if (this.m_dataRowHolder != null)
			{
				this.m_dataRowHolder.NextRow(owner.OdpContext, owner.Depth);
				return;
			}
			object obj = ((IDataRowSortOwner)this.m_hierarchyRoot.Value()).EvaluateDataRowSortExpression(this.m_sortExpression);
			this.m_sortTree.NextRow(obj, this);
		}

		// Token: 0x06007ACA RID: 31434 RVA: 0x001F9C22 File Offset: 0x001F7E22
		void IHierarchyObj.Traverse(ProcessingStages operation, ITraversalContext traversalContext)
		{
			if (this.m_dataRowHolder != null)
			{
				this.m_dataRowHolder.Traverse(operation, traversalContext, this);
				return;
			}
			this.m_sortTree.Traverse(operation, this.m_sortExpression.Direction, traversalContext);
		}

		// Token: 0x06007ACB RID: 31435 RVA: 0x001F9C53 File Offset: 0x001F7E53
		void IHierarchyObj.ReadRow()
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007ACC RID: 31436 RVA: 0x001F9C60 File Offset: 0x001F7E60
		void IHierarchyObj.ProcessUserSort()
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007ACD RID: 31437 RVA: 0x001F9C6D File Offset: 0x001F7E6D
		void IHierarchyObj.MarkSortInfoProcessed(List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007ACE RID: 31438 RVA: 0x001F9C7A File Offset: 0x001F7E7A
		void IHierarchyObj.AddSortInfoIndex(int sortInfoIndex, IReference<RuntimeSortFilterEventInfo> sortInfo)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007ACF RID: 31439 RVA: 0x001F9C88 File Offset: 0x001F7E88
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RuntimeDataRowSortHierarchyObj.m_declaration);
			PersistenceHelper persistenceHelper = writer.PersistenceHelper;
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.SortTree)
				{
					if (memberName == MemberName.HierarchyRoot)
					{
						writer.Write(this.m_hierarchyRoot);
						continue;
					}
					if (memberName == MemberName.SortTree)
					{
						writer.Write(this.m_sortTree);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Expression)
					{
						writer.Write(this.m_sortExpression);
						continue;
					}
					if (memberName == MemberName.DataRowHolder)
					{
						writer.Write(this.m_dataRowHolder);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007AD0 RID: 31440 RVA: 0x001F9D2C File Offset: 0x001F7F2C
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RuntimeDataRowSortHierarchyObj.m_declaration);
			PersistenceHelper persistenceHelper = reader.PersistenceHelper;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.SortTree)
				{
					if (memberName == MemberName.HierarchyRoot)
					{
						this.m_hierarchyRoot = (IReference<IHierarchyObj>)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.SortTree)
					{
						this.m_sortTree = (BTree)reader.ReadRIFObject();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Expression)
					{
						this.m_sortExpression = (RuntimeExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.DataRowHolder)
					{
						this.m_dataRowHolder = (RuntimeSortDataHolder)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007AD1 RID: 31441 RVA: 0x001F9DE9 File Offset: 0x001F7FE9
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007AD2 RID: 31442 RVA: 0x001F9DEB File Offset: 0x001F7FEB
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataRowSortHierarchyObj;
		}

		// Token: 0x06007AD3 RID: 31443 RVA: 0x001F9DF4 File Offset: 0x001F7FF4
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeDataRowSortHierarchyObj.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataRowSortHierarchyObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.HierarchyRoot, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IHierarchyObjReference),
					new MemberInfo(MemberName.DataRowHolder, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortDataHolder),
					new MemberInfo(MemberName.Expression, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeExpressionInfo),
					new MemberInfo(MemberName.SortTree, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTree)
				});
			}
			return RuntimeDataRowSortHierarchyObj.m_declaration;
		}

		// Token: 0x17002866 RID: 10342
		// (get) Token: 0x06007AD4 RID: 31444 RVA: 0x001F9E65 File Offset: 0x001F8065
		public int Size
		{
			get
			{
				return ItemSizes.SizeOf(this.m_hierarchyRoot) + ItemSizes.SizeOf(this.m_dataRowHolder) + ItemSizes.SizeOf(this.m_sortExpression) + ItemSizes.SizeOf(this.m_sortTree);
			}
		}

		// Token: 0x04003D53 RID: 15699
		private IReference<IHierarchyObj> m_hierarchyRoot;

		// Token: 0x04003D54 RID: 15700
		private RuntimeSortDataHolder m_dataRowHolder;

		// Token: 0x04003D55 RID: 15701
		private RuntimeExpressionInfo m_sortExpression;

		// Token: 0x04003D56 RID: 15702
		private BTree m_sortTree;

		// Token: 0x04003D57 RID: 15703
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeDataRowSortHierarchyObj.GetDeclaration();
	}
}
