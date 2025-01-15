using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008EF RID: 2287
	[PersistedWithinRequestOnly]
	public abstract class RuntimeMemberObj : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007DCB RID: 32203 RVA: 0x002078E1 File Offset: 0x00205AE1
		internal RuntimeMemberObj()
		{
		}

		// Token: 0x06007DCC RID: 32204 RVA: 0x002078E9 File Offset: 0x00205AE9
		internal RuntimeMemberObj(IReference<IScope> owner, Microsoft.ReportingServices.ReportIntermediateFormat.ReportHierarchyNode dynamicMember)
		{
			this.m_owner = owner;
		}

		// Token: 0x170028FD RID: 10493
		// (get) Token: 0x06007DCD RID: 32205 RVA: 0x002078F8 File Offset: 0x00205AF8
		internal RuntimeDataTablixGroupRootObjReference GroupRoot
		{
			get
			{
				return this.m_groupRoot;
			}
		}

		// Token: 0x06007DCE RID: 32206 RVA: 0x00207900 File Offset: 0x00205B00
		internal virtual void NextRow(bool isOuterGrouping, OnDemandProcessingContext odpContext)
		{
			if (null != this.m_groupRoot)
			{
				using (this.m_groupRoot.PinValue())
				{
					this.m_groupRoot.Value().NextRow();
				}
			}
		}

		// Token: 0x06007DCF RID: 32207 RVA: 0x00207954 File Offset: 0x00205B54
		internal virtual bool SortAndFilter(AggregateUpdateContext aggContext)
		{
			if (null != this.m_groupRoot)
			{
				using (this.m_groupRoot.PinValue())
				{
					return this.m_groupRoot.Value().SortAndFilter(aggContext);
				}
				return true;
			}
			return true;
		}

		// Token: 0x06007DD0 RID: 32208 RVA: 0x002079AC File Offset: 0x00205BAC
		internal virtual void UpdateAggregates(AggregateUpdateContext aggContext)
		{
			if (null != this.m_groupRoot)
			{
				using (this.m_groupRoot.PinValue())
				{
					this.m_groupRoot.Value().UpdateAggregates(aggContext);
				}
			}
		}

		// Token: 0x06007DD1 RID: 32209 RVA: 0x00207A00 File Offset: 0x00205C00
		internal virtual void CalculateRunningValues(Dictionary<string, IReference<RuntimeGroupRootObj>> groupCol, IReference<RuntimeGroupRootObj> lastGroup, AggregateUpdateContext aggContext)
		{
			if (null != this.m_groupRoot)
			{
				using (this.m_groupRoot.PinValue())
				{
					this.m_groupRoot.Value().CalculateRunningValues(groupCol, lastGroup, aggContext);
				}
			}
		}

		// Token: 0x06007DD2 RID: 32210 RVA: 0x00207A58 File Offset: 0x00205C58
		internal virtual void PrepareCalculateRunningValues()
		{
			if (null != this.m_groupRoot)
			{
				using (this.m_groupRoot.PinValue())
				{
					this.m_groupRoot.Value().PrepareCalculateRunningValues();
				}
			}
		}

		// Token: 0x06007DD3 RID: 32211
		internal abstract void CreateInstances(IReference<RuntimeDataRegionObj> outerGroupRef, OnDemandProcessingContext odpContext, DataRegionInstance dataRegionInstance, bool outerGroupings, IReference<RuntimeDataTablixGroupRootObj> currOuterGroupRoot, ScopeInstance parentInstance, IReference<RuntimeMemberObj>[] innerMembers, IReference<RuntimeDataTablixGroupLeafObj> innerGroupLeafRef);

		// Token: 0x06007DD4 RID: 32212 RVA: 0x00207AAC File Offset: 0x00205CAC
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RuntimeMemberObj.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.Owner)
				{
					if (memberName != MemberName.GroupRoot)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_groupRoot);
					}
				}
				else
				{
					writer.Write(this.m_owner);
				}
			}
		}

		// Token: 0x06007DD5 RID: 32213 RVA: 0x00207B18 File Offset: 0x00205D18
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RuntimeMemberObj.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.Owner)
				{
					if (memberName != MemberName.GroupRoot)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_groupRoot = (RuntimeDataTablixGroupRootObjReference)reader.ReadRIFObject();
					}
				}
				else
				{
					this.m_owner = (IReference<IScope>)reader.ReadRIFObject();
				}
			}
		}

		// Token: 0x06007DD6 RID: 32214 RVA: 0x00207B8B File Offset: 0x00205D8B
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007DD7 RID: 32215 RVA: 0x00207B8D File Offset: 0x00205D8D
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMemberObj;
		}

		// Token: 0x06007DD8 RID: 32216 RVA: 0x00207B94 File Offset: 0x00205D94
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeMemberObj.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeMemberObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.Owner, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IScopeReference),
					new MemberInfo(MemberName.GroupRoot, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeDataTablixGroupRootObjReference)
				});
			}
			return RuntimeMemberObj.m_declaration;
		}

		// Token: 0x170028FE RID: 10494
		// (get) Token: 0x06007DD9 RID: 32217 RVA: 0x00207BE4 File Offset: 0x00205DE4
		public virtual int Size
		{
			get
			{
				return ItemSizes.SizeOf(this.m_owner) + ItemSizes.SizeOf(this.m_groupRoot);
			}
		}

		// Token: 0x04003E0A RID: 15882
		protected IReference<IScope> m_owner;

		// Token: 0x04003E0B RID: 15883
		protected RuntimeDataTablixGroupRootObjReference m_groupRoot;

		// Token: 0x04003E0C RID: 15884
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeMemberObj.GetDeclaration();
	}
}
