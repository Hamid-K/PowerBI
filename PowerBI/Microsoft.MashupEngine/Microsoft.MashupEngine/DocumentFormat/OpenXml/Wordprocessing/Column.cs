using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F44 RID: 12100
	[GeneratedCode("DomGen", "2.0")]
	internal class Column : OpenXmlLeafElement
	{
		// Token: 0x17008FC9 RID: 36809
		// (get) Token: 0x06019F6E RID: 106350 RVA: 0x002E35A2 File Offset: 0x002E17A2
		public override string LocalName
		{
			get
			{
				return "col";
			}
		}

		// Token: 0x17008FCA RID: 36810
		// (get) Token: 0x06019F6F RID: 106351 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008FCB RID: 36811
		// (get) Token: 0x06019F70 RID: 106352 RVA: 0x0035A4F8 File Offset: 0x003586F8
		internal override int ElementTypeId
		{
			get
			{
				return 11747;
			}
		}

		// Token: 0x06019F71 RID: 106353 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008FCC RID: 36812
		// (get) Token: 0x06019F72 RID: 106354 RVA: 0x0035A4FF File Offset: 0x003586FF
		internal override string[] AttributeTagNames
		{
			get
			{
				return Column.attributeTagNames;
			}
		}

		// Token: 0x17008FCD RID: 36813
		// (get) Token: 0x06019F73 RID: 106355 RVA: 0x0035A506 File Offset: 0x00358706
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Column.attributeNamespaceIds;
			}
		}

		// Token: 0x17008FCE RID: 36814
		// (get) Token: 0x06019F74 RID: 106356 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06019F75 RID: 106357 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "w")]
		public StringValue Width
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

		// Token: 0x17008FCF RID: 36815
		// (get) Token: 0x06019F76 RID: 106358 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06019F77 RID: 106359 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "space")]
		public StringValue Space
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

		// Token: 0x06019F79 RID: 106361 RVA: 0x0035A50D File Offset: 0x0035870D
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "w" == name)
			{
				return new StringValue();
			}
			if (23 == namespaceId && "space" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019F7A RID: 106362 RVA: 0x0035A547 File Offset: 0x00358747
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Column>(deep);
		}

		// Token: 0x0400AB26 RID: 43814
		private const string tagName = "col";

		// Token: 0x0400AB27 RID: 43815
		private const byte tagNsId = 23;

		// Token: 0x0400AB28 RID: 43816
		internal const int ElementTypeIdConst = 11747;

		// Token: 0x0400AB29 RID: 43817
		private static string[] attributeTagNames = new string[] { "w", "space" };

		// Token: 0x0400AB2A RID: 43818
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
