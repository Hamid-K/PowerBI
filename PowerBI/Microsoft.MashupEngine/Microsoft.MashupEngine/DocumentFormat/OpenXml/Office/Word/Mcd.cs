using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002469 RID: 9321
	[GeneratedCode("DomGen", "2.0")]
	internal class Mcd : OpenXmlLeafElement
	{
		// Token: 0x170050E7 RID: 20711
		// (get) Token: 0x060112F6 RID: 70390 RVA: 0x002EB768 File Offset: 0x002E9968
		public override string LocalName
		{
			get
			{
				return "mcd";
			}
		}

		// Token: 0x170050E8 RID: 20712
		// (get) Token: 0x060112F7 RID: 70391 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x170050E9 RID: 20713
		// (get) Token: 0x060112F8 RID: 70392 RVA: 0x002EB76F File Offset: 0x002E996F
		internal override int ElementTypeId
		{
			get
			{
				return 12548;
			}
		}

		// Token: 0x060112F9 RID: 70393 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170050EA RID: 20714
		// (get) Token: 0x060112FA RID: 70394 RVA: 0x002EB776 File Offset: 0x002E9976
		internal override string[] AttributeTagNames
		{
			get
			{
				return Mcd.attributeTagNames;
			}
		}

		// Token: 0x170050EB RID: 20715
		// (get) Token: 0x060112FB RID: 70395 RVA: 0x002EB77D File Offset: 0x002E997D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Mcd.attributeNamespaceIds;
			}
		}

		// Token: 0x170050EC RID: 20716
		// (get) Token: 0x060112FC RID: 70396 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060112FD RID: 70397 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(33, "macroName")]
		public StringValue MacroName
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

		// Token: 0x170050ED RID: 20717
		// (get) Token: 0x060112FE RID: 70398 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060112FF RID: 70399 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(33, "name")]
		public StringValue Name
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

		// Token: 0x170050EE RID: 20718
		// (get) Token: 0x06011300 RID: 70400 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06011301 RID: 70401 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(33, "menuHelp")]
		public StringValue MenuHelp
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170050EF RID: 20719
		// (get) Token: 0x06011302 RID: 70402 RVA: 0x002EB434 File Offset: 0x002E9634
		// (set) Token: 0x06011303 RID: 70403 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(33, "bEncrypt")]
		public HexBinaryValue BEncrypt
		{
			get
			{
				return (HexBinaryValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170050F0 RID: 20720
		// (get) Token: 0x06011304 RID: 70404 RVA: 0x002EB784 File Offset: 0x002E9984
		// (set) Token: 0x06011305 RID: 70405 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(33, "cmg")]
		public HexBinaryValue Cmg
		{
			get
			{
				return (HexBinaryValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06011307 RID: 70407 RVA: 0x002EB794 File Offset: 0x002E9994
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "macroName" == name)
			{
				return new StringValue();
			}
			if (33 == namespaceId && "name" == name)
			{
				return new StringValue();
			}
			if (33 == namespaceId && "menuHelp" == name)
			{
				return new StringValue();
			}
			if (33 == namespaceId && "bEncrypt" == name)
			{
				return new HexBinaryValue();
			}
			if (33 == namespaceId && "cmg" == name)
			{
				return new HexBinaryValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011308 RID: 70408 RVA: 0x002EB821 File Offset: 0x002E9A21
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Mcd>(deep);
		}

		// Token: 0x0400788D RID: 30861
		private const string tagName = "mcd";

		// Token: 0x0400788E RID: 30862
		private const byte tagNsId = 33;

		// Token: 0x0400788F RID: 30863
		internal const int ElementTypeIdConst = 12548;

		// Token: 0x04007890 RID: 30864
		private static string[] attributeTagNames = new string[] { "macroName", "name", "menuHelp", "bEncrypt", "cmg" };

		// Token: 0x04007891 RID: 30865
		private static byte[] attributeNamespaceIds = new byte[] { 33, 33, 33, 33, 33 };
	}
}
