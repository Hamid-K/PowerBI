using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004C0 RID: 1216
	[Serializable]
	internal sealed class DocumentMapEndContainer : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003D85 RID: 15749 RVA: 0x001076FC File Offset: 0x001058FC
		private DocumentMapEndContainer()
		{
		}

		// Token: 0x06003D86 RID: 15750 RVA: 0x00107704 File Offset: 0x00105904
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DocumentMapEndContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, list);
		}

		// Token: 0x06003D87 RID: 15751 RVA: 0x00107723 File Offset: 0x00105923
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(DocumentMapEndContainer.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003D88 RID: 15752 RVA: 0x00107754 File Offset: 0x00105954
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(DocumentMapEndContainer.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003D89 RID: 15753 RVA: 0x00107785 File Offset: 0x00105985
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06003D8A RID: 15754 RVA: 0x00107792 File Offset: 0x00105992
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DocumentMapEndContainer;
		}

		// Token: 0x17001A43 RID: 6723
		// (get) Token: 0x06003D8B RID: 15755 RVA: 0x00107799 File Offset: 0x00105999
		internal static DocumentMapEndContainer Instance
		{
			get
			{
				return DocumentMapEndContainer.m_instance;
			}
		}

		// Token: 0x04001CB6 RID: 7350
		[NonSerialized]
		private static readonly DocumentMapEndContainer m_instance = new DocumentMapEndContainer();

		// Token: 0x04001CB7 RID: 7351
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DocumentMapEndContainer.GetDeclaration();
	}
}
