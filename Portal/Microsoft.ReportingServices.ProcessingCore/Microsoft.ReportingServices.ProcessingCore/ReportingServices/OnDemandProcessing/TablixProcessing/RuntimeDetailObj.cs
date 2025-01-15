using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008E3 RID: 2275
	[PersistedWithinRequestOnly]
	internal abstract class RuntimeDetailObj : RuntimeHierarchyObj
	{
		// Token: 0x06007C48 RID: 31816 RVA: 0x001FEB20 File Offset: 0x001FCD20
		protected RuntimeDetailObj(IReference<IScope> outerScope, Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion dataRegionDef, DataActions dataAction, OnDemandProcessingContext odpContext, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(odpContext, objectType, (outerScope == null) ? dataRegionDef.InnerGroupingDynamicMemberCount : (outerScope.Value().Depth + 1))
		{
			this.m_hierarchyRoot = (RuntimeDetailObjReference)base.SelfReference;
			this.m_outerScope = outerScope;
			this.m_dataRegionDef = dataRegionDef;
			this.m_outerDataAction = dataAction;
		}

		// Token: 0x06007C49 RID: 31817 RVA: 0x001FEB75 File Offset: 0x001FCD75
		internal RuntimeDetailObj(RuntimeDetailObj detailRoot, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(detailRoot.OdpContext, objectType, detailRoot.Depth)
		{
			this.m_hierarchyRoot = (RuntimeDetailObjReference)detailRoot.SelfReference;
			this.m_outerScope = detailRoot.m_outerScope;
			this.m_dataRegionDef = detailRoot.m_dataRegionDef;
		}

		// Token: 0x06007C4A RID: 31818 RVA: 0x001FEBB4 File Offset: 0x001FCDB4
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeDetailObj.m_declaration);
			IScalabilityCache scalabilityCache = writer.PersistenceHelper as IScalabilityCache;
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.OuterDataAction)
				{
					if (memberName == MemberName.RunningValueValues)
					{
						writer.Write<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult>(this.m_rvValueList);
						continue;
					}
					if (memberName == MemberName.OuterScope)
					{
						writer.Write(this.m_outerScope);
						continue;
					}
					if (memberName == MemberName.OuterDataAction)
					{
						writer.Write(this.m_outerDataAction);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DataRegionDef)
					{
						int num = scalabilityCache.StoreStaticReference(this.m_dataRegionDef);
						writer.Write(num);
						continue;
					}
					if (memberName == MemberName.DataRows)
					{
						writer.Write(this.m_dataRows);
						continue;
					}
					switch (memberName)
					{
					case MemberName.RunningValuesInGroup:
						writer.WriteListOfPrimitives<string>(this.m_runningValuesInGroup);
						continue;
					case MemberName.PreviousValuesInGroup:
						writer.WriteListOfPrimitives<string>(this.m_previousValuesInGroup);
						continue;
					case MemberName.GroupCollection:
						writer.Write(this.m_groupCollection);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007C4B RID: 31819 RVA: 0x001FECE0 File Offset: 0x001FCEE0
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeDetailObj.m_declaration);
			IScalabilityCache scalabilityCache = reader.PersistenceHelper as IScalabilityCache;
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.OuterDataAction)
				{
					if (memberName == MemberName.RunningValueValues)
					{
						this.m_rvValueList = reader.ReadListOfRIFObjectArrays<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult>();
						continue;
					}
					if (memberName == MemberName.OuterScope)
					{
						this.m_outerScope = (IReference<IScope>)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.OuterDataAction)
					{
						this.m_outerDataAction = (DataActions)reader.ReadEnum();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DataRegionDef)
					{
						int num = reader.ReadInt32();
						this.m_dataRegionDef = (Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion)scalabilityCache.FetchStaticReference(num);
						continue;
					}
					if (memberName == MemberName.DataRows)
					{
						this.m_dataRows = reader.ReadRIFObject<ScalableList<DataFieldRow>>();
						continue;
					}
					switch (memberName)
					{
					case MemberName.RunningValuesInGroup:
						this.m_runningValuesInGroup = reader.ReadListOfPrimitives<string>();
						continue;
					case MemberName.PreviousValuesInGroup:
						this.m_previousValuesInGroup = reader.ReadListOfPrimitives<string>();
						continue;
					case MemberName.GroupCollection:
						this.m_groupCollection = reader.ReadStringRIFObjectDictionary<IReference<RuntimeGroupRootObj>>();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007C4C RID: 31820 RVA: 0x001FEE13 File Offset: 0x001FD013
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			base.ResolveReferences(memberReferencesCollection, referenceableItems);
		}

		// Token: 0x06007C4D RID: 31821 RVA: 0x001FEE1D File Offset: 0x001FD01D
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDetailObj;
		}

		// Token: 0x06007C4E RID: 31822 RVA: 0x001FEE24 File Offset: 0x001FD024
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeDetailObj.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDetailObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeHierarchyObj, new List<MemberInfo>
				{
					new MemberInfo(MemberName.OuterScope, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IScopeReference),
					new MemberInfo(MemberName.DataRegionDef, Token.Int32),
					new MemberInfo(MemberName.DataRows, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScalableList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DataFieldRow),
					new MemberInfo(MemberName.RunningValueValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectArray),
					new MemberInfo(MemberName.RunningValuesInGroup, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.String),
					new MemberInfo(MemberName.PreviousValuesInGroup, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList, Token.String),
					new MemberInfo(MemberName.GroupCollection, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StringRIFObjectDictionary, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupRootObjReference),
					new MemberInfo(MemberName.OuterDataAction, Token.Enum)
				});
			}
			return RuntimeDetailObj.m_declaration;
		}

		// Token: 0x1700289B RID: 10395
		// (get) Token: 0x06007C4F RID: 31823 RVA: 0x001FEEF0 File Offset: 0x001FD0F0
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.SizeOf(this.m_outerScope) + ItemSizes.ReferenceSize + ItemSizes.SizeOf<DataFieldRow>(this.m_dataRows) + ItemSizes.SizeOf<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult>(this.m_rvValueList) + ItemSizes.SizeOf(this.m_runningValuesInGroup) + ItemSizes.SizeOf(this.m_previousValuesInGroup) + ItemSizes.SizeOf<string, IReference<RuntimeGroupRootObj>>(this.m_groupCollection) + 4;
			}
		}

		// Token: 0x04003DA0 RID: 15776
		protected IReference<IScope> m_outerScope;

		// Token: 0x04003DA1 RID: 15777
		[StaticReference]
		protected Microsoft.ReportingServices.ReportIntermediateFormat.DataRegion m_dataRegionDef;

		// Token: 0x04003DA2 RID: 15778
		protected ScalableList<DataFieldRow> m_dataRows;

		// Token: 0x04003DA3 RID: 15779
		protected List<Microsoft.ReportingServices.ReportIntermediateFormat.DataAggregateObjResult[]> m_rvValueList;

		// Token: 0x04003DA4 RID: 15780
		protected List<string> m_runningValuesInGroup;

		// Token: 0x04003DA5 RID: 15781
		protected List<string> m_previousValuesInGroup;

		// Token: 0x04003DA6 RID: 15782
		protected Dictionary<string, IReference<RuntimeGroupRootObj>> m_groupCollection;

		// Token: 0x04003DA7 RID: 15783
		protected DataActions m_outerDataAction;

		// Token: 0x04003DA8 RID: 15784
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeDetailObj.GetDeclaration();
	}
}
