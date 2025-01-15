using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing
{
	// Token: 0x02000827 RID: 2087
	internal class GroupTreePartition : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007533 RID: 30003 RVA: 0x001E58E1 File Offset: 0x001E3AE1
		internal GroupTreePartition()
		{
		}

		// Token: 0x17002791 RID: 10129
		// (get) Token: 0x06007534 RID: 30004 RVA: 0x001E58E9 File Offset: 0x001E3AE9
		internal bool IsEmpty
		{
			get
			{
				return this.m_topLevelScopeInstances == null;
			}
		}

		// Token: 0x17002792 RID: 10130
		// (get) Token: 0x06007535 RID: 30005 RVA: 0x001E58F4 File Offset: 0x001E3AF4
		internal List<IReference<ScopeInstance>> TopLevelScopeInstances
		{
			get
			{
				return this.m_topLevelScopeInstances;
			}
		}

		// Token: 0x06007536 RID: 30006 RVA: 0x001E58FC File Offset: 0x001E3AFC
		internal void AddTopLevelScopeInstance(IReference<ScopeInstance> instance)
		{
			if (this.m_topLevelScopeInstances == null)
			{
				this.m_topLevelScopeInstances = new List<IReference<ScopeInstance>>();
			}
			this.m_topLevelScopeInstances.Add(instance);
		}

		// Token: 0x06007537 RID: 30007 RVA: 0x001E5920 File Offset: 0x001E3B20
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GroupTreePartition, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.TopLevelScopeInstances, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopeInstanceReference)
			});
		}

		// Token: 0x06007538 RID: 30008 RVA: 0x001E5954 File Offset: 0x001E3B54
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(GroupTreePartition.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.TopLevelScopeInstances)
				{
					writer.Write<IReference<ScopeInstance>>(this.m_topLevelScopeInstances);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06007539 RID: 30009 RVA: 0x001E59A8 File Offset: 0x001E3BA8
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(GroupTreePartition.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.TopLevelScopeInstances)
				{
					this.m_topLevelScopeInstances = reader.ReadGenericListOfRIFObjects<IReference<ScopeInstance>>();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
			reader.ResolveReferences();
		}

		// Token: 0x0600753A RID: 30010 RVA: 0x001E5A00 File Offset: 0x001E3C00
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x0600753B RID: 30011 RVA: 0x001E5A0D File Offset: 0x001E3C0D
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.GroupTreePartition;
		}

		// Token: 0x04003B81 RID: 15233
		private List<IReference<ScopeInstance>> m_topLevelScopeInstances;

		// Token: 0x04003B82 RID: 15234
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = GroupTreePartition.GetDeclaration();
	}
}
