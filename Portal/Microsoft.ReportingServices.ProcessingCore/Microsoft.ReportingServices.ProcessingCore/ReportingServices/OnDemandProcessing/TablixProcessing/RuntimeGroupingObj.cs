using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008C5 RID: 2245
	[PersistedWithinRequestOnly]
	public abstract class RuntimeGroupingObj : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06007AF5 RID: 31477 RVA: 0x001FA5CB File Offset: 0x001F87CB
		internal RuntimeGroupingObj()
		{
		}

		// Token: 0x06007AF6 RID: 31478 RVA: 0x001FA5D3 File Offset: 0x001F87D3
		internal RuntimeGroupingObj(RuntimeHierarchyObj owner, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
		{
			this.m_owner = owner;
			this.m_objectType = objectType;
		}

		// Token: 0x06007AF7 RID: 31479 RVA: 0x001FA5EC File Offset: 0x001F87EC
		internal static RuntimeGroupingObj CreateGroupingObj(RuntimeGroupingObj.GroupingTypes type, RuntimeHierarchyObj owner, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
		{
			switch (type)
			{
			case RuntimeGroupingObj.GroupingTypes.None:
				return new RuntimeGroupingObjLinkedList(owner, objectType);
			case RuntimeGroupingObj.GroupingTypes.Hash:
				return new RuntimeGroupingObjHash(owner, objectType);
			case RuntimeGroupingObj.GroupingTypes.Sort:
				return new RuntimeGroupingObjTree(owner, objectType);
			case RuntimeGroupingObj.GroupingTypes.Detail:
				return new RuntimeGroupingObjDetail(owner, objectType);
			case RuntimeGroupingObj.GroupingTypes.DetailUserSort:
				return new RuntimeGroupingObjDetailUserSort(owner, objectType);
			case RuntimeGroupingObj.GroupingTypes.NaturalGroup:
				return new RuntimeGroupingObjNaturalGroup(owner, objectType);
			default:
				Global.Tracer.Assert(false, "Unexpected GroupingTypes");
				throw new InvalidOperationException();
			}
		}

		// Token: 0x17002872 RID: 10354
		// (get) Token: 0x06007AF8 RID: 31480 RVA: 0x001FA65E File Offset: 0x001F885E
		internal virtual BTree Tree
		{
			get
			{
				Global.Tracer.Assert(false, "Tree is only available for sort based groupings.");
				return null;
			}
		}

		// Token: 0x06007AF9 RID: 31481
		internal abstract void Cleanup();

		// Token: 0x06007AFA RID: 31482 RVA: 0x001FA671 File Offset: 0x001F8871
		internal void NextRow(object keyValue)
		{
			this.NextRow(keyValue, false, null);
		}

		// Token: 0x06007AFB RID: 31483
		internal abstract void NextRow(object keyValue, bool hasParent, object parentKey);

		// Token: 0x06007AFC RID: 31484
		internal abstract void Traverse(ProcessingStages operation, bool ascending, ITraversalContext traversalContext);

		// Token: 0x06007AFD RID: 31485
		internal abstract void CopyDomainScopeGroupInstances(RuntimeGroupRootObj destination);

		// Token: 0x06007AFE RID: 31486 RVA: 0x001FA67C File Offset: 0x001F887C
		internal void SetOwner(RuntimeHierarchyObj owner)
		{
			this.m_owner = owner;
		}

		// Token: 0x06007AFF RID: 31487 RVA: 0x001FA688 File Offset: 0x001F8888
		public virtual void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(RuntimeGroupingObj.m_declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.ObjectType)
				{
					writer.WriteEnum((int)this.m_objectType);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06007B00 RID: 31488 RVA: 0x001FA6DC File Offset: 0x001F88DC
		public virtual void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(RuntimeGroupingObj.m_declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.ObjectType)
				{
					this.m_objectType = (Microsoft.ReportingServices.ReportProcessing.ObjectType)reader.ReadEnum();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x06007B01 RID: 31489 RVA: 0x001FA72D File Offset: 0x001F892D
		public virtual void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007B02 RID: 31490 RVA: 0x001FA72F File Offset: 0x001F892F
		public virtual Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObj;
		}

		// Token: 0x06007B03 RID: 31491 RVA: 0x001FA734 File Offset: 0x001F8934
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeGroupingObj.m_declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObj, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.ObjectType, Token.Enum)
				});
			}
			return RuntimeGroupingObj.m_declaration;
		}

		// Token: 0x17002873 RID: 10355
		// (get) Token: 0x06007B04 RID: 31492 RVA: 0x001FA76E File Offset: 0x001F896E
		public virtual int Size
		{
			get
			{
				return ItemSizes.ReferenceSize + 4;
			}
		}

		// Token: 0x04003D60 RID: 15712
		[NonSerialized]
		protected RuntimeHierarchyObj m_owner;

		// Token: 0x04003D61 RID: 15713
		protected Microsoft.ReportingServices.ReportProcessing.ObjectType m_objectType;

		// Token: 0x04003D62 RID: 15714
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeGroupingObj.GetDeclaration();

		// Token: 0x02000D1D RID: 3357
		public enum GroupingTypes
		{
			// Token: 0x04005060 RID: 20576
			None,
			// Token: 0x04005061 RID: 20577
			Hash,
			// Token: 0x04005062 RID: 20578
			Sort,
			// Token: 0x04005063 RID: 20579
			Detail,
			// Token: 0x04005064 RID: 20580
			DetailUserSort,
			// Token: 0x04005065 RID: 20581
			NaturalGroup
		}
	}
}
