using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200236C RID: 9068
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ArtisticPencilGrayscale : OpenXmlLeafElement
	{
		// Token: 0x17004AA6 RID: 19110
		// (get) Token: 0x060104FD RID: 66813 RVA: 0x002E206F File Offset: 0x002E026F
		public override string LocalName
		{
			get
			{
				return "artisticPencilGrayscale";
			}
		}

		// Token: 0x17004AA7 RID: 19111
		// (get) Token: 0x060104FE RID: 66814 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004AA8 RID: 19112
		// (get) Token: 0x060104FF RID: 66815 RVA: 0x002E2076 File Offset: 0x002E0276
		internal override int ElementTypeId
		{
			get
			{
				return 12751;
			}
		}

		// Token: 0x06010500 RID: 66816 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004AA9 RID: 19113
		// (get) Token: 0x06010501 RID: 66817 RVA: 0x002E207D File Offset: 0x002E027D
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticPencilGrayscale.attributeTagNames;
			}
		}

		// Token: 0x17004AAA RID: 19114
		// (get) Token: 0x06010502 RID: 66818 RVA: 0x002E2084 File Offset: 0x002E0284
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticPencilGrayscale.attributeNamespaceIds;
			}
		}

		// Token: 0x17004AAB RID: 19115
		// (get) Token: 0x06010503 RID: 66819 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010504 RID: 66820 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "trans")]
		public Int32Value Transparancy
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004AAC RID: 19116
		// (get) Token: 0x06010505 RID: 66821 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06010506 RID: 66822 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "pencilSize")]
		public Int32Value BrushSize
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06010508 RID: 66824 RVA: 0x002E1DBB File Offset: 0x002DFFBB
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "trans" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "pencilSize" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010509 RID: 66825 RVA: 0x002E208B File Offset: 0x002E028B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticPencilGrayscale>(deep);
		}

		// Token: 0x0601050A RID: 66826 RVA: 0x002E2094 File Offset: 0x002E0294
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticPencilGrayscale()
		{
			byte[] array = new byte[2];
			ArtisticPencilGrayscale.attributeNamespaceIds = array;
		}

		// Token: 0x0400740F RID: 29711
		private const string tagName = "artisticPencilGrayscale";

		// Token: 0x04007410 RID: 29712
		private const byte tagNsId = 48;

		// Token: 0x04007411 RID: 29713
		internal const int ElementTypeIdConst = 12751;

		// Token: 0x04007412 RID: 29714
		private static string[] attributeTagNames = new string[] { "trans", "pencilSize" };

		// Token: 0x04007413 RID: 29715
		private static byte[] attributeNamespaceIds;
	}
}
