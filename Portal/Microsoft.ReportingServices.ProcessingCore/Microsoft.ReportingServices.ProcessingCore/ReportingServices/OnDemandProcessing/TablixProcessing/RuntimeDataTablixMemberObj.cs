using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008F0 RID: 2288
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeDataTablixMemberObj : RuntimeMemberObj
	{
		// Token: 0x06007DDB RID: 32219 RVA: 0x00207C09 File Offset: 0x00205E09
		internal RuntimeDataTablixMemberObj()
		{
		}

		// Token: 0x06007DDC RID: 32220 RVA: 0x00207C14 File Offset: 0x00205E14
		private RuntimeDataTablixMemberObj(IReference<IScope> owner, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode dynamicMember, ref DataActions dataAction, OnDemandProcessingContext odpContext, IReference<RuntimeMemberObj>[] innerGroupings, HierarchyNodeList staticMembers, bool outerMostStatics, int headingLevel, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(owner, dynamicMember)
		{
			int num = -1;
			if (dynamicMember != null)
			{
				num = dynamicMember.MemberCellIndex;
				RuntimeDataTablixGroupRootObj runtimeDataTablixGroupRootObj = new RuntimeDataTablixGroupRootObj(owner, dynamicMember, ref dataAction, odpContext, innerGroupings, outerMostStatics, headingLevel, objectType);
				this.m_groupRoot = (RuntimeDataTablixGroupRootObjReference)runtimeDataTablixGroupRootObj.SelfReference;
				this.m_groupRoot.UnPinValue();
			}
			if (staticMembers != null && staticMembers.Count != 0)
			{
				int count = staticMembers.Count;
				this.m_hasStaticMembers = true;
				this.m_staticLeafCellIndexes = staticMembers.GetLeafCellIndexes(num);
			}
		}

		// Token: 0x06007DDD RID: 32221 RVA: 0x00207C90 File Offset: 0x00205E90
		internal static IReference<RuntimeMemberObj> CreateRuntimeMemberObject(IReference<IScope> owner, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode dynamicMemberDef, ref DataActions dataAction, OnDemandProcessingContext odpContext, IReference<RuntimeMemberObj>[] innerGroupings, HierarchyNodeList staticMembers, bool outerMostStatics, int headingLevel, Microsoft.ReportingServices.ReportProcessing.ObjectType dataRegionType)
		{
			RuntimeDataTablixMemberObj runtimeDataTablixMemberObj = new RuntimeDataTablixMemberObj(owner, dynamicMemberDef, ref dataAction, odpContext, innerGroupings, staticMembers, outerMostStatics, headingLevel, dataRegionType);
			return odpContext.TablixProcessingScalabilityCache.Allocate<RuntimeMemberObj>(runtimeDataTablixMemberObj, headingLevel);
		}

		// Token: 0x06007DDE RID: 32222 RVA: 0x00207CC0 File Offset: 0x00205EC0
		internal override void CreateInstances(IReference<RuntimeDataRegionObj> containingScopeRef, OnDemandProcessingContext odpContext, DataRegionInstance dataRegionInstance, bool isOuterGrouping, IReference<RuntimeDataTablixGroupRootObj> currOuterGroupRoot, ScopeInstance parentInstance, IReference<RuntimeMemberObj>[] innerMembers, IReference<RuntimeDataTablixGroupLeafObj> innerGroupLeaf)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef = dataRegionInstance.DataRegionDef;
			if (isOuterGrouping && this.m_hasStaticMembers)
			{
				dataRegionDef.NewOuterCells();
			}
			if (null != this.m_groupRoot)
			{
				dataRegionDef.CurrentOuterGroupRoot = currOuterGroupRoot;
				using (this.m_groupRoot.PinValue())
				{
					this.m_groupRoot.Value().CreateInstances(parentInstance, innerMembers, innerGroupLeaf);
				}
				if (this.m_staticLeafCellIndexes != null)
				{
					if (isOuterGrouping && this.m_hasStaticMembers)
					{
						dataRegionDef.NewOuterCells();
					}
					IReference<RuntimeDataTablixGroupRootObj> reference = null;
					if (this.m_owner is IReference<RuntimeDataTablixObj>)
					{
						using (this.m_owner.PinValue())
						{
							((RuntimeDataTablixObj)this.m_owner.Value()).SetupEnvironment();
						}
						if (isOuterGrouping && this.m_hasStaticMembers)
						{
							reference = dataRegionDef.CurrentOuterGroupRoot;
							dataRegionDef.CurrentOuterGroupRoot = null;
							currOuterGroupRoot = null;
						}
					}
					else
					{
						using (containingScopeRef.PinValue())
						{
							containingScopeRef.Value().SetupEnvironment();
						}
					}
					this.CreateCells(containingScopeRef, odpContext, dataRegionInstance, isOuterGrouping, currOuterGroupRoot, parentInstance, innerMembers, innerGroupLeaf);
					if (reference != null)
					{
						dataRegionDef.CurrentOuterGroupRoot = reference;
						return;
					}
				}
			}
			else
			{
				this.CreateCells(containingScopeRef, odpContext, dataRegionInstance, isOuterGrouping, currOuterGroupRoot, parentInstance, innerMembers, innerGroupLeaf);
			}
		}

		// Token: 0x06007DDF RID: 32223 RVA: 0x00207E1C File Offset: 0x0020601C
		private void CreateCells(IReference<RuntimeDataRegionObj> containingScopeRef, OnDemandProcessingContext odpContext, DataRegionInstance dataRegionInstance, bool isOuterGroup, IReference<RuntimeDataTablixGroupRootObj> currOuterGroupRoot, ScopeInstance parentInstance, IReference<RuntimeMemberObj>[] innerMembers, IReference<RuntimeDataTablixGroupLeafObj> innerGroupLeafRef)
		{
			using (containingScopeRef.PinValue())
			{
				RuntimeDataRegionObj runtimeDataRegionObj = containingScopeRef.Value();
				if (runtimeDataRegionObj is RuntimeDataTablixGroupLeafObj)
				{
					((RuntimeDataTablixGroupLeafObj)runtimeDataRegionObj).CreateStaticCells(dataRegionInstance, parentInstance, currOuterGroupRoot, isOuterGroup, this.m_staticLeafCellIndexes, innerMembers, innerGroupLeafRef);
				}
				else
				{
					((RuntimeDataTablixObj)runtimeDataRegionObj).CreateOutermostStaticCells(dataRegionInstance, isOuterGroup, innerMembers, innerGroupLeafRef);
				}
			}
		}

		// Token: 0x06007DE0 RID: 32224 RVA: 0x00207E8C File Offset: 0x0020608C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeDataTablixMemberObj.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.HasStaticMembers)
				{
					if (memberName != MemberName.StaticLeafCellIndexes)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.WriteListOfPrimitives<int>(this.m_staticLeafCellIndexes);
					}
				}
				else
				{
					writer.Write(this.m_hasStaticMembers);
				}
			}
		}

		// Token: 0x06007DE1 RID: 32225 RVA: 0x00207F00 File Offset: 0x00206100
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeDataTablixMemberObj.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.HasStaticMembers)
				{
					if (memberName != MemberName.StaticLeafCellIndexes)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_staticLeafCellIndexes = reader.ReadListOfPrimitives<int>();
					}
				}
				else
				{
					this.m_hasStaticMembers = reader.ReadBoolean();
				}
			}
		}

		// Token: 0x06007DE2 RID: 32226 RVA: 0x00207F73 File Offset: 0x00206173
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007DE3 RID: 32227 RVA: 0x00207F75 File Offset: 0x00206175
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixMemberObj;
		}

		// Token: 0x06007DE4 RID: 32228 RVA: 0x00207F7C File Offset: 0x0020617C
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeDataTablixMemberObj.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixMemberObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMemberObj, new List<MemberInfo>
				{
					new MemberInfo(MemberName.HasStaticMembers, Token.Boolean),
					new MemberInfo(MemberName.StaticLeafCellIndexes, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.Int32)
				});
			}
			return RuntimeDataTablixMemberObj.m_declaration;
		}

		// Token: 0x170028FF RID: 10495
		// (get) Token: 0x06007DE5 RID: 32229 RVA: 0x00207FD4 File Offset: 0x002061D4
		public override int Size
		{
			get
			{
				return base.Size + 1 + ItemSizes.SizeOf(this.m_staticLeafCellIndexes);
			}
		}

		// Token: 0x04003E0D RID: 15885
		private bool m_hasStaticMembers;

		// Token: 0x04003E0E RID: 15886
		private List<int> m_staticLeafCellIndexes;

		// Token: 0x04003E0F RID: 15887
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeDataTablixMemberObj.GetDeclaration();
	}
}
