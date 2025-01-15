using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008C3 RID: 2243
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeSortHierarchyObj : IHierarchyObj, IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007AD6 RID: 31446 RVA: 0x001F9EA2 File Offset: 0x001F80A2
		internal RuntimeSortHierarchyObj()
		{
		}

		// Token: 0x06007AD7 RID: 31447 RVA: 0x001F9EAC File Offset: 0x001F80AC
		internal RuntimeSortHierarchyObj(IHierarchyObj outerHierarchy, int depth)
		{
			this.m_hierarchyRoot = outerHierarchy.HierarchyRoot;
			this.m_odpContext = this.m_hierarchyRoot.Value().OdpContext;
			List<int> sortFilterInfoIndices = this.m_hierarchyRoot.Value().SortFilterInfoIndices;
			int num = outerHierarchy.ExpressionIndex + 1;
			if (sortFilterInfoIndices == null || num >= sortFilterInfoIndices.Count)
			{
				RuntimeDataTablixGroupRootObjReference runtimeDataTablixGroupRootObjReference = this.m_hierarchyRoot as RuntimeDataTablixGroupRootObjReference;
				if (null != runtimeDataTablixGroupRootObjReference)
				{
					using (runtimeDataTablixGroupRootObjReference.PinValue())
					{
						RuntimeDataTablixGroupRootObj runtimeDataTablixGroupRootObj = runtimeDataTablixGroupRootObjReference.Value();
						this.m_dataHolder = (IReference<ISortDataHolder>)runtimeDataTablixGroupRootObj.CreateGroupLeaf();
						if (!runtimeDataTablixGroupRootObj.HasParent)
						{
							runtimeDataTablixGroupRootObj.AddChildWithNoParent((RuntimeGroupLeafObjReference)this.m_dataHolder);
						}
						return;
					}
				}
				this.m_dataRowHolder = new RuntimeSortDataHolder();
				return;
			}
			this.m_sortHierarchyStruct = new RuntimeSortHierarchyObj.SortHierarchyStructure(this, num, this.m_odpContext.RuntimeSortFilterInfo, sortFilterInfoIndices);
		}

		// Token: 0x17002867 RID: 10343
		// (get) Token: 0x06007AD8 RID: 31448 RVA: 0x001F9F9C File Offset: 0x001F819C
		public int Depth
		{
			get
			{
				return this.m_hierarchyRoot.Value().Depth + 1;
			}
		}

		// Token: 0x17002868 RID: 10344
		// (get) Token: 0x06007AD9 RID: 31449 RVA: 0x001F9FB0 File Offset: 0x001F81B0
		IReference<IHierarchyObj> IHierarchyObj.HierarchyRoot
		{
			get
			{
				return this.m_hierarchyRoot;
			}
		}

		// Token: 0x17002869 RID: 10345
		// (get) Token: 0x06007ADA RID: 31450 RVA: 0x001F9FB8 File Offset: 0x001F81B8
		OnDemandProcessingContext IHierarchyObj.OdpContext
		{
			get
			{
				return this.m_hierarchyRoot.Value().OdpContext;
			}
		}

		// Token: 0x1700286A RID: 10346
		// (get) Token: 0x06007ADB RID: 31451 RVA: 0x001F9FCA File Offset: 0x001F81CA
		BTree IHierarchyObj.SortTree
		{
			get
			{
				if (this.m_sortHierarchyStruct != null)
				{
					return this.m_sortHierarchyStruct.SortTree;
				}
				return null;
			}
		}

		// Token: 0x1700286B RID: 10347
		// (get) Token: 0x06007ADC RID: 31452 RVA: 0x001F9FE1 File Offset: 0x001F81E1
		int IHierarchyObj.ExpressionIndex
		{
			get
			{
				if (this.m_sortHierarchyStruct != null)
				{
					return this.m_sortHierarchyStruct.SortIndex;
				}
				return -1;
			}
		}

		// Token: 0x1700286C RID: 10348
		// (get) Token: 0x06007ADD RID: 31453 RVA: 0x001F9FF8 File Offset: 0x001F81F8
		List<int> IHierarchyObj.SortFilterInfoIndices
		{
			get
			{
				return this.m_hierarchyRoot.Value().SortFilterInfoIndices;
			}
		}

		// Token: 0x1700286D RID: 10349
		// (get) Token: 0x06007ADE RID: 31454 RVA: 0x001FA00A File Offset: 0x001F820A
		bool IHierarchyObj.IsDetail
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700286E RID: 10350
		// (get) Token: 0x06007ADF RID: 31455 RVA: 0x001FA00D File Offset: 0x001F820D
		bool IHierarchyObj.InDataRowSortPhase
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06007AE0 RID: 31456 RVA: 0x001FA010 File Offset: 0x001F8210
		IHierarchyObj IHierarchyObj.CreateHierarchyObjForSortTree()
		{
			return new RuntimeSortHierarchyObj(this, this.Depth + 1);
		}

		// Token: 0x06007AE1 RID: 31457 RVA: 0x001FA020 File Offset: 0x001F8220
		ProcessingMessageList IHierarchyObj.RegisterComparisonError(string propertyName)
		{
			return this.m_odpContext.RegisterComparisonErrorForSortFilterEvent(propertyName);
		}

		// Token: 0x06007AE2 RID: 31458 RVA: 0x001FA030 File Offset: 0x001F8230
		void IHierarchyObj.NextRow(IHierarchyObj owner)
		{
			if (this.m_dataHolder != null)
			{
				using (this.m_dataHolder.PinValue())
				{
					this.m_dataHolder.Value().NextRow();
					return;
				}
			}
			if (this.m_sortHierarchyStruct != null)
			{
				IReference<RuntimeSortFilterEventInfo> sortInfo = this.m_sortHierarchyStruct.SortInfo;
				object sortOrder;
				using (sortInfo.PinValue())
				{
					sortOrder = sortInfo.Value().GetSortOrder(this.m_odpContext.ReportRuntime);
				}
				this.m_sortHierarchyStruct.SortTree.NextRow(sortOrder, this);
				return;
			}
			if (this.m_dataRowHolder != null)
			{
				this.m_dataRowHolder.NextRow(this.m_odpContext, this.Depth + 1);
			}
		}

		// Token: 0x06007AE3 RID: 31459 RVA: 0x001FA0FC File Offset: 0x001F82FC
		void IHierarchyObj.Traverse(ProcessingStages operation, ITraversalContext traversalContext)
		{
			if (this.m_sortHierarchyStruct != null)
			{
				bool flag = true;
				RuntimeSortFilterEventInfo runtimeSortFilterEventInfo = this.m_sortHierarchyStruct.SortInfo.Value();
				if (runtimeSortFilterEventInfo.EventSource.UserSort.SortExpressionScope == null)
				{
					flag = runtimeSortFilterEventInfo.SortDirection;
				}
				this.m_sortHierarchyStruct.SortTree.Traverse(operation, flag, traversalContext);
			}
			if (this.m_dataHolder != null)
			{
				using (this.m_dataHolder.PinValue())
				{
					this.m_dataHolder.Value().Traverse(operation, traversalContext);
				}
			}
			if (this.m_dataRowHolder != null)
			{
				using (this.m_hierarchyRoot.PinValue())
				{
					this.m_dataRowHolder.Traverse(operation, traversalContext, this.m_hierarchyRoot.Value());
				}
			}
		}

		// Token: 0x06007AE4 RID: 31460 RVA: 0x001FA1D8 File Offset: 0x001F83D8
		void IHierarchyObj.ReadRow()
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007AE5 RID: 31461 RVA: 0x001FA1E5 File Offset: 0x001F83E5
		void IHierarchyObj.ProcessUserSort()
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007AE6 RID: 31462 RVA: 0x001FA1F2 File Offset: 0x001F83F2
		void IHierarchyObj.MarkSortInfoProcessed(List<IReference<RuntimeSortFilterEventInfo>> runtimeSortFilterInfo)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007AE7 RID: 31463 RVA: 0x001FA1FF File Offset: 0x001F83FF
		void IHierarchyObj.AddSortInfoIndex(int sortInfoIndex, IReference<RuntimeSortFilterEventInfo> sortInfo)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06007AE8 RID: 31464 RVA: 0x001FA20C File Offset: 0x001F840C
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RuntimeSortHierarchyObj.m_declaration);
			IScalabilityCache scalabilityCache = writer.PersistenceHelper as IScalabilityCache;
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.OdpContext)
				{
					switch (memberName)
					{
					case MemberName.HierarchyRoot:
						writer.Write(this.m_hierarchyRoot);
						break;
					case MemberName.SortHierarchyStruct:
						writer.Write(this.m_sortHierarchyStruct);
						break;
					case MemberName.DataHolder:
						writer.Write(this.m_dataHolder);
						break;
					default:
						if (memberName != MemberName.DataRowHolder)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_dataRowHolder);
						}
						break;
					}
				}
				else
				{
					int num = scalabilityCache.StoreStaticReference(this.m_odpContext);
					writer.Write(num);
				}
			}
		}

		// Token: 0x06007AE9 RID: 31465 RVA: 0x001FA2D4 File Offset: 0x001F84D4
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RuntimeSortHierarchyObj.m_declaration);
			IScalabilityCache scalabilityCache = reader.PersistenceHelper as IScalabilityCache;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.OdpContext)
				{
					switch (memberName)
					{
					case MemberName.HierarchyRoot:
						this.m_hierarchyRoot = (IReference<IHierarchyObj>)reader.ReadRIFObject();
						break;
					case MemberName.SortHierarchyStruct:
						this.m_sortHierarchyStruct = (RuntimeSortHierarchyObj.SortHierarchyStructure)reader.ReadRIFObject();
						break;
					case MemberName.DataHolder:
						this.m_dataHolder = (IReference<ISortDataHolder>)reader.ReadRIFObject();
						break;
					default:
						if (memberName != MemberName.DataRowHolder)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							this.m_dataRowHolder = (RuntimeSortDataHolder)reader.ReadRIFObject();
						}
						break;
					}
				}
				else
				{
					int num = reader.ReadInt32();
					this.m_odpContext = (OnDemandProcessingContext)scalabilityCache.FetchStaticReference(num);
				}
			}
		}

		// Token: 0x06007AEA RID: 31466 RVA: 0x001FA3B3 File Offset: 0x001F85B3
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007AEB RID: 31467 RVA: 0x001FA3B5 File Offset: 0x001F85B5
		Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortHierarchyObj;
		}

		// Token: 0x06007AEC RID: 31468 RVA: 0x001FA3BC File Offset: 0x001F85BC
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeSortHierarchyObj.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortHierarchyObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.HierarchyRoot, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IHierarchyObjReference),
					new MemberInfo(MemberName.OdpContext, Token.Int32),
					new MemberInfo(MemberName.SortHierarchyStruct, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortHierarchyStruct),
					new MemberInfo(MemberName.DataHolder, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ISortDataHolderReference),
					new MemberInfo(MemberName.DataRowHolder, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortDataHolder)
				});
			}
			return RuntimeSortHierarchyObj.m_declaration;
		}

		// Token: 0x1700286F RID: 10351
		// (get) Token: 0x06007AED RID: 31469 RVA: 0x001FA43C File Offset: 0x001F863C
		public int Size
		{
			get
			{
				return ItemSizes.SizeOf(this.m_hierarchyRoot) + ItemSizes.ReferenceSize + ItemSizes.SizeOf(this.m_sortHierarchyStruct) + ItemSizes.SizeOf(this.m_dataHolder) + ItemSizes.SizeOf(this.m_dataRowHolder);
			}
		}

		// Token: 0x04003D58 RID: 15704
		private IReference<IHierarchyObj> m_hierarchyRoot;

		// Token: 0x04003D59 RID: 15705
		private OnDemandProcessingContext m_odpContext;

		// Token: 0x04003D5A RID: 15706
		private RuntimeSortHierarchyObj.SortHierarchyStructure m_sortHierarchyStruct;

		// Token: 0x04003D5B RID: 15707
		private IReference<ISortDataHolder> m_dataHolder;

		// Token: 0x04003D5C RID: 15708
		private RuntimeSortDataHolder m_dataRowHolder;

		// Token: 0x04003D5D RID: 15709
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeSortHierarchyObj.GetDeclaration();

		// Token: 0x02000D1B RID: 3355
		[PersistedWithinRequestOnly]
		internal class SortHierarchyStructure : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
		{
			// Token: 0x06008EF7 RID: 36599 RVA: 0x00246311 File Offset: 0x00244511
			internal SortHierarchyStructure()
			{
			}

			// Token: 0x06008EF8 RID: 36600 RVA: 0x00246319 File Offset: 0x00244519
			internal SortHierarchyStructure(IHierarchyObj owner, int sortIndex, List<IReference<RuntimeSortFilterEventInfo>> sortInfoList, List<int> sortInfoIndices)
			{
				this.SortIndex = sortIndex;
				this.SortInfo = sortInfoList[sortInfoIndices[sortIndex]];
				this.SortTree = new BTree(owner, owner.OdpContext, owner.Depth);
			}

			// Token: 0x06008EF9 RID: 36601 RVA: 0x00246354 File Offset: 0x00244554
			public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
			{
				writer.RegisterDeclaration(RuntimeSortHierarchyObj.SortHierarchyStructure.m_declaration);
				while (writer.NextMember())
				{
					MemberName memberName = writer.CurrentMember.MemberName;
					if (memberName != MemberName.SortTree)
					{
						if (memberName != MemberName.SortInfo)
						{
							if (memberName != MemberName.SortIndex)
							{
								Global.Tracer.Assert(false);
							}
							else
							{
								writer.Write(this.SortIndex);
							}
						}
						else
						{
							writer.Write(this.SortInfo);
						}
					}
					else
					{
						writer.Write(this.SortTree);
					}
				}
			}

			// Token: 0x06008EFA RID: 36602 RVA: 0x002463D4 File Offset: 0x002445D4
			public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
			{
				reader.RegisterDeclaration(RuntimeSortHierarchyObj.SortHierarchyStructure.m_declaration);
				while (reader.NextMember())
				{
					MemberName memberName = reader.CurrentMember.MemberName;
					if (memberName != MemberName.SortTree)
					{
						if (memberName != MemberName.SortInfo)
						{
							if (memberName != MemberName.SortIndex)
							{
								Global.Tracer.Assert(false);
							}
							else
							{
								this.SortIndex = reader.ReadInt32();
							}
						}
						else
						{
							this.SortInfo = (IReference<RuntimeSortFilterEventInfo>)reader.ReadRIFObject();
						}
					}
					else
					{
						this.SortTree = (BTree)reader.ReadRIFObject();
					}
				}
			}

			// Token: 0x06008EFB RID: 36603 RVA: 0x0024645E File Offset: 0x0024465E
			public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
			{
			}

			// Token: 0x06008EFC RID: 36604 RVA: 0x00246460 File Offset: 0x00244660
			public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
			{
				return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortHierarchyStruct;
			}

			// Token: 0x06008EFD RID: 36605 RVA: 0x00246464 File Offset: 0x00244664
			public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
			{
				if (RuntimeSortHierarchyObj.SortHierarchyStructure.m_declaration == null)
				{
					return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortHierarchyStruct, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
					{
						new MemberInfo(MemberName.SortInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeSortFilterEventInfoReference),
						new MemberInfo(MemberName.SortIndex, Token.Int32),
						new MemberInfo(MemberName.SortTree, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.BTreeNode)
					});
				}
				return RuntimeSortHierarchyObj.SortHierarchyStructure.m_declaration;
			}

			// Token: 0x17002BDA RID: 11226
			// (get) Token: 0x06008EFE RID: 36606 RVA: 0x002464C3 File Offset: 0x002446C3
			public int Size
			{
				get
				{
					return ItemSizes.SizeOf(this.SortInfo) + 4 + ItemSizes.SizeOf(this.SortTree);
				}
			}

			// Token: 0x04005057 RID: 20567
			internal IReference<RuntimeSortFilterEventInfo> SortInfo;

			// Token: 0x04005058 RID: 20568
			internal int SortIndex;

			// Token: 0x04005059 RID: 20569
			internal BTree SortTree;

			// Token: 0x0400505A RID: 20570
			[NonSerialized]
			private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeSortHierarchyObj.SortHierarchyStructure.GetDeclaration();
		}
	}
}
