using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004C1 RID: 1217
	[Serializable]
	internal sealed class DocumentMapBeginContainer : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003D8D RID: 15757 RVA: 0x001077B6 File Offset: 0x001059B6
		private DocumentMapBeginContainer()
		{
		}

		// Token: 0x06003D8E RID: 15758 RVA: 0x001077C0 File Offset: 0x001059C0
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			List<MemberInfo> list = new List<MemberInfo>();
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DocumentMapBeginContainer, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, list);
		}

		// Token: 0x06003D8F RID: 15759 RVA: 0x001077DF File Offset: 0x001059DF
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(DocumentMapBeginContainer.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003D90 RID: 15760 RVA: 0x00107810 File Offset: 0x00105A10
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(DocumentMapBeginContainer.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003D91 RID: 15761 RVA: 0x00107841 File Offset: 0x00105A41
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06003D92 RID: 15762 RVA: 0x0010784E File Offset: 0x00105A4E
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DocumentMapBeginContainer;
		}

		// Token: 0x17001A44 RID: 6724
		// (get) Token: 0x06003D93 RID: 15763 RVA: 0x00107855 File Offset: 0x00105A55
		internal static DocumentMapBeginContainer Instance
		{
			get
			{
				return DocumentMapBeginContainer.m_instance;
			}
		}

		// Token: 0x04001CB8 RID: 7352
		[NonSerialized]
		private static readonly DocumentMapBeginContainer m_instance = new DocumentMapBeginContainer();

		// Token: 0x04001CB9 RID: 7353
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DocumentMapBeginContainer.GetDeclaration();
	}
}
