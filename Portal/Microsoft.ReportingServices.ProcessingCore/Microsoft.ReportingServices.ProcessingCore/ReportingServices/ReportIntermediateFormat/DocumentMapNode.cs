using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004C4 RID: 1220
	[Serializable]
	internal sealed class DocumentMapNode : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17001A46 RID: 6726
		// (get) Token: 0x06003DA2 RID: 15778 RVA: 0x00107B52 File Offset: 0x00105D52
		// (set) Token: 0x06003DA3 RID: 15779 RVA: 0x00107B5A File Offset: 0x00105D5A
		internal string Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		// Token: 0x17001A47 RID: 6727
		// (get) Token: 0x06003DA4 RID: 15780 RVA: 0x00107B63 File Offset: 0x00105D63
		// (set) Token: 0x06003DA5 RID: 15781 RVA: 0x00107B6B File Offset: 0x00105D6B
		internal string Id
		{
			get
			{
				return this.m_id;
			}
			set
			{
				Global.Tracer.Assert(value != null, "The id of a document map node cannot be null.");
				this.m_id = value;
			}
		}

		// Token: 0x06003DA6 RID: 15782 RVA: 0x00107B87 File Offset: 0x00105D87
		internal DocumentMapNode()
		{
		}

		// Token: 0x06003DA7 RID: 15783 RVA: 0x00107B90 File Offset: 0x00105D90
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DocumentMapNode, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ID, Token.String),
				new MemberInfo(MemberName.Label, Token.String)
			});
		}

		// Token: 0x06003DA8 RID: 15784 RVA: 0x00107BD8 File Offset: 0x00105DD8
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(DocumentMapNode.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.ID)
				{
					if (memberName != MemberName.Label)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_label);
					}
				}
				else
				{
					writer.Write(this.m_id);
				}
			}
		}

		// Token: 0x06003DA9 RID: 15785 RVA: 0x00107C40 File Offset: 0x00105E40
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(DocumentMapNode.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.ID)
				{
					if (memberName != MemberName.Label)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_label = reader.ReadString();
					}
				}
				else
				{
					this.m_id = reader.ReadString();
				}
			}
		}

		// Token: 0x06003DAA RID: 15786 RVA: 0x00107CA7 File Offset: 0x00105EA7
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06003DAB RID: 15787 RVA: 0x00107CB4 File Offset: 0x00105EB4
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.DocumentMapNode;
		}

		// Token: 0x04001CC5 RID: 7365
		internal const char IdLevelSeparator = '_';

		// Token: 0x04001CC6 RID: 7366
		private string m_id;

		// Token: 0x04001CC7 RID: 7367
		private string m_label;

		// Token: 0x04001CC8 RID: 7368
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = DocumentMapNode.GetDeclaration();
	}
}
