using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002BBF RID: 11199
	[GeneratedCode("DomGen", "2.0")]
	internal class Reviewed : OpenXmlLeafElement
	{
		// Token: 0x17007C66 RID: 31846
		// (get) Token: 0x060174E8 RID: 95464 RVA: 0x00335327 File Offset: 0x00333527
		public override string LocalName
		{
			get
			{
				return "reviewed";
			}
		}

		// Token: 0x17007C67 RID: 31847
		// (get) Token: 0x060174E9 RID: 95465 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007C68 RID: 31848
		// (get) Token: 0x060174EA RID: 95466 RVA: 0x0033532E File Offset: 0x0033352E
		internal override int ElementTypeId
		{
			get
			{
				return 11170;
			}
		}

		// Token: 0x060174EB RID: 95467 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007C69 RID: 31849
		// (get) Token: 0x060174EC RID: 95468 RVA: 0x00335335 File Offset: 0x00333535
		internal override string[] AttributeTagNames
		{
			get
			{
				return Reviewed.attributeTagNames;
			}
		}

		// Token: 0x17007C6A RID: 31850
		// (get) Token: 0x060174ED RID: 95469 RVA: 0x0033533C File Offset: 0x0033353C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Reviewed.attributeNamespaceIds;
			}
		}

		// Token: 0x17007C6B RID: 31851
		// (get) Token: 0x060174EE RID: 95470 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060174EF RID: 95471 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rId")]
		public UInt32Value RevisionId
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060174F1 RID: 95473 RVA: 0x00335343 File Offset: 0x00333543
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rId" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060174F2 RID: 95474 RVA: 0x00335363 File Offset: 0x00333563
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Reviewed>(deep);
		}

		// Token: 0x060174F3 RID: 95475 RVA: 0x0033536C File Offset: 0x0033356C
		// Note: this type is marked as 'beforefieldinit'.
		static Reviewed()
		{
			byte[] array = new byte[1];
			Reviewed.attributeNamespaceIds = array;
		}

		// Token: 0x04009BE4 RID: 39908
		private const string tagName = "reviewed";

		// Token: 0x04009BE5 RID: 39909
		private const byte tagNsId = 22;

		// Token: 0x04009BE6 RID: 39910
		internal const int ElementTypeIdConst = 11170;

		// Token: 0x04009BE7 RID: 39911
		private static string[] attributeTagNames = new string[] { "rId" };

		// Token: 0x04009BE8 RID: 39912
		private static byte[] attributeNamespaceIds;
	}
}
