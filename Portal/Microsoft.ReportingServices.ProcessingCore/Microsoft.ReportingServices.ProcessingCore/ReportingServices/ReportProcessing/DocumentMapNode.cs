using System;
using System.Collections;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x0200067D RID: 1661
	[Serializable]
	internal sealed class DocumentMapNode : InstanceInfo
	{
		// Token: 0x17002049 RID: 8265
		// (get) Token: 0x06005B15 RID: 23317 RVA: 0x00176839 File Offset: 0x00174A39
		// (set) Token: 0x06005B16 RID: 23318 RVA: 0x00176841 File Offset: 0x00174A41
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

		// Token: 0x1700204A RID: 8266
		// (get) Token: 0x06005B17 RID: 23319 RVA: 0x0017684A File Offset: 0x00174A4A
		// (set) Token: 0x06005B18 RID: 23320 RVA: 0x00176852 File Offset: 0x00174A52
		internal string Id
		{
			get
			{
				return this.m_id;
			}
			set
			{
				this.m_id = value;
			}
		}

		// Token: 0x1700204B RID: 8267
		// (get) Token: 0x06005B19 RID: 23321 RVA: 0x0017685B File Offset: 0x00174A5B
		// (set) Token: 0x06005B1A RID: 23322 RVA: 0x00176863 File Offset: 0x00174A63
		internal int Page
		{
			get
			{
				return this.m_page;
			}
			set
			{
				this.m_page = value;
			}
		}

		// Token: 0x1700204C RID: 8268
		// (get) Token: 0x06005B1B RID: 23323 RVA: 0x0017686C File Offset: 0x00174A6C
		// (set) Token: 0x06005B1C RID: 23324 RVA: 0x00176874 File Offset: 0x00174A74
		internal DocumentMapNode[] Children
		{
			get
			{
				return this.m_children;
			}
			set
			{
				this.m_children = value;
			}
		}

		// Token: 0x06005B1D RID: 23325 RVA: 0x0017687D File Offset: 0x00174A7D
		internal DocumentMapNode()
		{
		}

		// Token: 0x06005B1E RID: 23326 RVA: 0x0017688C File Offset: 0x00174A8C
		internal DocumentMapNode(string id, string label, int page, ArrayList children)
		{
			Global.Tracer.Assert(id != null, "The id of a document map node cannot be null.");
			this.m_id = id;
			this.m_label = label;
			this.m_page = page;
			if (children != null && children.Count > 0)
			{
				this.m_children = (DocumentMapNode[])children.ToArray(typeof(DocumentMapNode));
			}
		}

		// Token: 0x06005B1F RID: 23327 RVA: 0x001768F8 File Offset: 0x00174AF8
		internal new static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.InstanceInfo, new MemberInfoList
			{
				new MemberInfo(MemberName.Id, Token.String),
				new MemberInfo(MemberName.Label, Token.String),
				new MemberInfo(MemberName.DocMapPage, Token.Int32),
				new MemberInfo(MemberName.Children, Token.Array, ObjectType.DocumentMapNode)
			});
		}

		// Token: 0x04002F67 RID: 12135
		private string m_id;

		// Token: 0x04002F68 RID: 12136
		private string m_label;

		// Token: 0x04002F69 RID: 12137
		private int m_page = -1;

		// Token: 0x04002F6A RID: 12138
		private DocumentMapNode[] m_children;
	}
}
