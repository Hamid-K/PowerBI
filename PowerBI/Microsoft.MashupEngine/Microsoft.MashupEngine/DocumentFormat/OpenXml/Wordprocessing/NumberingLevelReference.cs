using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F27 RID: 12071
	[GeneratedCode("DomGen", "2.0")]
	internal class NumberingLevelReference : OpenXmlLeafElement
	{
		// Token: 0x17008F34 RID: 36660
		// (get) Token: 0x06019E2D RID: 106029 RVA: 0x00358F56 File Offset: 0x00357156
		public override string LocalName
		{
			get
			{
				return "ilvl";
			}
		}

		// Token: 0x17008F35 RID: 36661
		// (get) Token: 0x06019E2E RID: 106030 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F36 RID: 36662
		// (get) Token: 0x06019E2F RID: 106031 RVA: 0x00358F5D File Offset: 0x0035715D
		internal override int ElementTypeId
		{
			get
			{
				return 11712;
			}
		}

		// Token: 0x06019E30 RID: 106032 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008F37 RID: 36663
		// (get) Token: 0x06019E31 RID: 106033 RVA: 0x00358F64 File Offset: 0x00357164
		internal override string[] AttributeTagNames
		{
			get
			{
				return NumberingLevelReference.attributeTagNames;
			}
		}

		// Token: 0x17008F38 RID: 36664
		// (get) Token: 0x06019E32 RID: 106034 RVA: 0x00358F6B File Offset: 0x0035716B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NumberingLevelReference.attributeNamespaceIds;
			}
		}

		// Token: 0x17008F39 RID: 36665
		// (get) Token: 0x06019E33 RID: 106035 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06019E34 RID: 106036 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public Int32Value Val
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

		// Token: 0x06019E36 RID: 106038 RVA: 0x00346792 File Offset: 0x00344992
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019E37 RID: 106039 RVA: 0x00358F72 File Offset: 0x00357172
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberingLevelReference>(deep);
		}

		// Token: 0x0400AAB6 RID: 43702
		private const string tagName = "ilvl";

		// Token: 0x0400AAB7 RID: 43703
		private const byte tagNsId = 23;

		// Token: 0x0400AAB8 RID: 43704
		internal const int ElementTypeIdConst = 11712;

		// Token: 0x0400AAB9 RID: 43705
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400AABA RID: 43706
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
