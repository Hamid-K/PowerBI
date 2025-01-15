using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.OnDemandProcessing.TablixProcessing
{
	// Token: 0x020008CA RID: 2250
	[PersistedWithinRequestOnly]
	internal sealed class RuntimeGroupingObjDetail : RuntimeGroupingObjLinkedList
	{
		// Token: 0x06007B40 RID: 31552 RVA: 0x001FB3A9 File Offset: 0x001F95A9
		internal RuntimeGroupingObjDetail()
		{
		}

		// Token: 0x06007B41 RID: 31553 RVA: 0x001FB3B1 File Offset: 0x001F95B1
		internal RuntimeGroupingObjDetail(RuntimeHierarchyObj owner, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType)
			: base(owner, objectType)
		{
		}

		// Token: 0x06007B42 RID: 31554 RVA: 0x001FB3BB File Offset: 0x001F95BB
		internal override void NextRow(object keyValue, bool hasParent, object parentKey)
		{
			Global.Tracer.Assert(!hasParent, "(!hasParent)");
			base.CreateHierarchyObjAndAddToParent();
		}

		// Token: 0x06007B43 RID: 31555 RVA: 0x001FB3D7 File Offset: 0x001F95D7
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(RuntimeGroupingObjDetail.m_declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007B44 RID: 31556 RVA: 0x001FB40F File Offset: 0x001F960F
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(RuntimeGroupingObjDetail.m_declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06007B45 RID: 31557 RVA: 0x001FB447 File Offset: 0x001F9647
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06007B46 RID: 31558 RVA: 0x001FB449 File Offset: 0x001F9649
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjDetail;
		}

		// Token: 0x06007B47 RID: 31559 RVA: 0x001FB450 File Offset: 0x001F9650
		public new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (RuntimeGroupingObjDetail.m_declaration == null)
			{
				List<MemberInfo> list = new List<MemberInfo>();
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjDetail, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RuntimeGroupingObjLinkedList, list);
			}
			return RuntimeGroupingObjDetail.m_declaration;
		}

		// Token: 0x17002879 RID: 10361
		// (get) Token: 0x06007B48 RID: 31560 RVA: 0x001FB480 File Offset: 0x001F9680
		public override int Size
		{
			get
			{
				return base.Size;
			}
		}

		// Token: 0x04003D6A RID: 15722
		[NonSerialized]
		private static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_declaration = RuntimeGroupingObjDetail.GetDeclaration();
	}
}
