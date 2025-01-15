using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002679 RID: 9849
	[GeneratedCode("DomGen", "2.0")]
	internal class Description : OpenXmlLeafElement
	{
		// Token: 0x17005C7D RID: 23677
		// (get) Token: 0x06012CF4 RID: 77044 RVA: 0x002FDB8B File Offset: 0x002FBD8B
		public override string LocalName
		{
			get
			{
				return "desc";
			}
		}

		// Token: 0x17005C7E RID: 23678
		// (get) Token: 0x06012CF5 RID: 77045 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005C7F RID: 23679
		// (get) Token: 0x06012CF6 RID: 77046 RVA: 0x002FFB43 File Offset: 0x002FDD43
		internal override int ElementTypeId
		{
			get
			{
				return 10664;
			}
		}

		// Token: 0x06012CF7 RID: 77047 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005C80 RID: 23680
		// (get) Token: 0x06012CF8 RID: 77048 RVA: 0x002FFB4A File Offset: 0x002FDD4A
		internal override string[] AttributeTagNames
		{
			get
			{
				return Description.attributeTagNames;
			}
		}

		// Token: 0x17005C81 RID: 23681
		// (get) Token: 0x06012CF9 RID: 77049 RVA: 0x002FFB51 File Offset: 0x002FDD51
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Description.attributeNamespaceIds;
			}
		}

		// Token: 0x17005C82 RID: 23682
		// (get) Token: 0x06012CFA RID: 77050 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012CFB RID: 77051 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "lang")]
		public StringValue Language
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005C83 RID: 23683
		// (get) Token: 0x06012CFC RID: 77052 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06012CFD RID: 77053 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "val")]
		public StringValue Val
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06012CFF RID: 77055 RVA: 0x002FDB15 File Offset: 0x002FBD15
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "lang" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "val" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012D00 RID: 77056 RVA: 0x002FFB58 File Offset: 0x002FDD58
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Description>(deep);
		}

		// Token: 0x06012D01 RID: 77057 RVA: 0x002FFB64 File Offset: 0x002FDD64
		// Note: this type is marked as 'beforefieldinit'.
		static Description()
		{
			byte[] array = new byte[2];
			Description.attributeNamespaceIds = array;
		}

		// Token: 0x040081AC RID: 33196
		private const string tagName = "desc";

		// Token: 0x040081AD RID: 33197
		private const byte tagNsId = 14;

		// Token: 0x040081AE RID: 33198
		internal const int ElementTypeIdConst = 10664;

		// Token: 0x040081AF RID: 33199
		private static string[] attributeTagNames = new string[] { "lang", "val" };

		// Token: 0x040081B0 RID: 33200
		private static byte[] attributeNamespaceIds;
	}
}
