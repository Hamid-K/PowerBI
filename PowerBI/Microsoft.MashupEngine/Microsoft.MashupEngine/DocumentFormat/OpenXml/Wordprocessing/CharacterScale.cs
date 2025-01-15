using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E90 RID: 11920
	[GeneratedCode("DomGen", "2.0")]
	internal class CharacterScale : OpenXmlLeafElement
	{
		// Token: 0x17008B2D RID: 35629
		// (get) Token: 0x0601954A RID: 103754 RVA: 0x002F2F1C File Offset: 0x002F111C
		public override string LocalName
		{
			get
			{
				return "w";
			}
		}

		// Token: 0x17008B2E RID: 35630
		// (get) Token: 0x0601954B RID: 103755 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B2F RID: 35631
		// (get) Token: 0x0601954C RID: 103756 RVA: 0x00348A54 File Offset: 0x00346C54
		internal override int ElementTypeId
		{
			get
			{
				return 11594;
			}
		}

		// Token: 0x0601954D RID: 103757 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008B30 RID: 35632
		// (get) Token: 0x0601954E RID: 103758 RVA: 0x00348A5B File Offset: 0x00346C5B
		internal override string[] AttributeTagNames
		{
			get
			{
				return CharacterScale.attributeTagNames;
			}
		}

		// Token: 0x17008B31 RID: 35633
		// (get) Token: 0x0601954F RID: 103759 RVA: 0x00348A62 File Offset: 0x00346C62
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CharacterScale.attributeNamespaceIds;
			}
		}

		// Token: 0x17008B32 RID: 35634
		// (get) Token: 0x06019550 RID: 103760 RVA: 0x002EC050 File Offset: 0x002EA250
		// (set) Token: 0x06019551 RID: 103761 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public IntegerValue Val
		{
			get
			{
				return (IntegerValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06019553 RID: 103763 RVA: 0x00348A69 File Offset: 0x00346C69
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new IntegerValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019554 RID: 103764 RVA: 0x00348A8B File Offset: 0x00346C8B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CharacterScale>(deep);
		}

		// Token: 0x0400A85C RID: 43100
		private const string tagName = "w";

		// Token: 0x0400A85D RID: 43101
		private const byte tagNsId = 23;

		// Token: 0x0400A85E RID: 43102
		internal const int ElementTypeIdConst = 11594;

		// Token: 0x0400A85F RID: 43103
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400A860 RID: 43104
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
