using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008C9 RID: 2249
	[PersistedWithinRequestOnly]
	internal class RuntimeGroupingObjDetailUserSort : RuntimeGroupingObj
	{
		// Token: 0x06007B33 RID: 31539 RVA: 0x001FB279 File Offset: 0x001F9479
		internal RuntimeGroupingObjDetailUserSort()
		{
		}

		// Token: 0x06007B34 RID: 31540 RVA: 0x001FB281 File Offset: 0x001F9481
		internal RuntimeGroupingObjDetailUserSort(RuntimeHierarchyObj owner, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(owner, objectType)
		{
		}

		// Token: 0x06007B35 RID: 31541 RVA: 0x001FB28B File Offset: 0x001F948B
		internal override void Cleanup()
		{
		}

		// Token: 0x06007B36 RID: 31542 RVA: 0x001FB28D File Offset: 0x001F948D
		internal override void NextRow(object keyValue, bool hasParent, object parentKey)
		{
			Global.Tracer.Assert(false, "This implementation of RuntimeGroupingObj does not support NextRow");
		}

		// Token: 0x06007B37 RID: 31543 RVA: 0x001FB2A0 File Offset: 0x001F94A0
		internal override void Traverse(ProcessingStages operation, bool ascending, ITraversalContext traversalContext)
		{
			RuntimeGroupRootObj runtimeGroupRootObj = this.m_owner as RuntimeGroupRootObj;
			Global.Tracer.Assert(runtimeGroupRootObj != null, "(null != groupRootOwner)");
			runtimeGroupRootObj.GroupOrDetailSortTree.Traverse(operation, ascending, traversalContext);
		}

		// Token: 0x06007B38 RID: 31544 RVA: 0x001FB2DA File Offset: 0x001F94DA
		internal override void CopyDomainScopeGroupInstances(RuntimeGroupRootObj destination)
		{
			Global.Tracer.Assert(false, "Domain Scope should only be applied to Hash groups");
		}

		// Token: 0x06007B39 RID: 31545 RVA: 0x001FB2EC File Offset: 0x001F94EC
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeGroupingObjDetailUserSort.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007B3A RID: 31546 RVA: 0x001FB324 File Offset: 0x001F9524
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeGroupingObjDetailUserSort.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007B3B RID: 31547 RVA: 0x001FB35C File Offset: 0x001F955C
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007B3C RID: 31548 RVA: 0x001FB35E File Offset: 0x001F955E
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjDetailUserSort;
		}

		// Token: 0x06007B3D RID: 31549 RVA: 0x001FB368 File Offset: 0x001F9568
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeGroupingObjDetailUserSort.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjDetailUserSort, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObj, list);
			}
			return RuntimeGroupingObjDetailUserSort.m_declaration;
		}

		// Token: 0x17002878 RID: 10360
		// (get) Token: 0x06007B3E RID: 31550 RVA: 0x001FB395 File Offset: 0x001F9595
		public override int Size
		{
			get
			{
				return base.Size;
			}
		}

		// Token: 0x04003D69 RID: 15721
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeGroupingObjDetailUserSort.GetDeclaration();
	}
}
