using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FB3 RID: 12211
	[GeneratedCode("DomGen", "2.0")]
	internal class FontFamily : OpenXmlLeafElement
	{
		// Token: 0x1700938F RID: 37775
		// (get) Token: 0x0601A782 RID: 108418 RVA: 0x0033375B File Offset: 0x0033195B
		public override string LocalName
		{
			get
			{
				return "family";
			}
		}

		// Token: 0x17009390 RID: 37776
		// (get) Token: 0x0601A783 RID: 108419 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009391 RID: 37777
		// (get) Token: 0x0601A784 RID: 108420 RVA: 0x00362BFC File Offset: 0x00360DFC
		internal override int ElementTypeId
		{
			get
			{
				return 11918;
			}
		}

		// Token: 0x0601A785 RID: 108421 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009392 RID: 37778
		// (get) Token: 0x0601A786 RID: 108422 RVA: 0x00362C03 File Offset: 0x00360E03
		internal override string[] AttributeTagNames
		{
			get
			{
				return FontFamily.attributeTagNames;
			}
		}

		// Token: 0x17009393 RID: 37779
		// (get) Token: 0x0601A787 RID: 108423 RVA: 0x00362C0A File Offset: 0x00360E0A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FontFamily.attributeNamespaceIds;
			}
		}

		// Token: 0x17009394 RID: 37780
		// (get) Token: 0x0601A788 RID: 108424 RVA: 0x00362C11 File Offset: 0x00360E11
		// (set) Token: 0x0601A789 RID: 108425 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public EnumValue<FontFamilyValues> Val
		{
			get
			{
				return (EnumValue<FontFamilyValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601A78B RID: 108427 RVA: 0x00362C20 File Offset: 0x00360E20
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new EnumValue<FontFamilyValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A78C RID: 108428 RVA: 0x00362C42 File Offset: 0x00360E42
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FontFamily>(deep);
		}

		// Token: 0x0400AD14 RID: 44308
		private const string tagName = "family";

		// Token: 0x0400AD15 RID: 44309
		private const byte tagNsId = 23;

		// Token: 0x0400AD16 RID: 44310
		internal const int ElementTypeIdConst = 11918;

		// Token: 0x0400AD17 RID: 44311
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AD18 RID: 44312
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
