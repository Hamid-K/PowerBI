using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008C8 RID: 2248
	[PersistedWithinRequestOnly]
	internal class RuntimeGroupingObjLinkedList : RuntimeGroupingObj
	{
		// Token: 0x06007B25 RID: 31525 RVA: 0x001FB0E7 File Offset: 0x001F92E7
		internal RuntimeGroupingObjLinkedList()
		{
		}

		// Token: 0x06007B26 RID: 31526 RVA: 0x001FB0EF File Offset: 0x001F92EF
		internal RuntimeGroupingObjLinkedList(RuntimeHierarchyObj owner, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(owner, objectType)
		{
		}

		// Token: 0x06007B27 RID: 31527 RVA: 0x001FB0F9 File Offset: 0x001F92F9
		internal override void Cleanup()
		{
		}

		// Token: 0x06007B28 RID: 31528 RVA: 0x001FB0FB File Offset: 0x001F92FB
		internal override void NextRow(object keyValue, bool hasParent, object parentKey)
		{
			Global.Tracer.Assert(false, "This implementation of RuntimeGroupingObj does not support NextRow");
		}

		// Token: 0x06007B29 RID: 31529 RVA: 0x001FB110 File Offset: 0x001F9310
		internal override void Traverse(ProcessingStages operation, bool ascending, ITraversalContext traversalContext)
		{
			RuntimeGroupRootObj runtimeGroupRootObj = this.m_owner as RuntimeGroupRootObj;
			Global.Tracer.Assert(runtimeGroupRootObj != null, "(null != groupRootOwner)");
			runtimeGroupRootObj.TraverseLinkedGroupLeaves(operation, ascending, traversalContext);
		}

		// Token: 0x06007B2A RID: 31530 RVA: 0x001FB145 File Offset: 0x001F9345
		internal override void CopyDomainScopeGroupInstances(RuntimeGroupRootObj destination)
		{
			Global.Tracer.Assert(false, "Domain Scope should only be applied to Hash groups");
		}

		// Token: 0x06007B2B RID: 31531 RVA: 0x001FB158 File Offset: 0x001F9358
		protected RuntimeHierarchyObjReference CreateHierarchyObjAndAddToParent()
		{
			RuntimeHierarchyObjReference runtimeHierarchyObjReference = null;
			try
			{
				RuntimeHierarchyObj runtimeHierarchyObj = new RuntimeHierarchyObj(this.m_owner, this.m_objectType, ((IScope)this.m_owner).Depth + 1);
				runtimeHierarchyObjReference = (RuntimeHierarchyObjReference)runtimeHierarchyObj.SelfReference;
				runtimeHierarchyObj.NextRow();
			}
			finally
			{
				if (null != runtimeHierarchyObjReference)
				{
					runtimeHierarchyObjReference.UnPinValue();
				}
			}
			return runtimeHierarchyObjReference;
		}

		// Token: 0x06007B2C RID: 31532 RVA: 0x001FB1BC File Offset: 0x001F93BC
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeGroupingObjLinkedList.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007B2D RID: 31533 RVA: 0x001FB1F4 File Offset: 0x001F93F4
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeGroupingObjLinkedList.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007B2E RID: 31534 RVA: 0x001FB22C File Offset: 0x001F942C
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007B2F RID: 31535 RVA: 0x001FB22E File Offset: 0x001F942E
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjLinkedList;
		}

		// Token: 0x06007B30 RID: 31536 RVA: 0x001FB238 File Offset: 0x001F9438
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeGroupingObjLinkedList.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjLinkedList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObj, list);
			}
			return RuntimeGroupingObjLinkedList.m_declaration;
		}

		// Token: 0x17002877 RID: 10359
		// (get) Token: 0x06007B31 RID: 31537 RVA: 0x001FB265 File Offset: 0x001F9465
		public override int Size
		{
			get
			{
				return base.Size;
			}
		}

		// Token: 0x04003D68 RID: 15720
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeGroupingObjLinkedList.GetDeclaration();
	}
}
