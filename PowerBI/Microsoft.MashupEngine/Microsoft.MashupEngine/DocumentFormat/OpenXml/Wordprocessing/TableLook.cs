using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F0F RID: 12047
	[GeneratedCode("DomGen", "2.0")]
	internal class TableLook : OpenXmlLeafElement
	{
		// Token: 0x17008E2E RID: 36398
		// (get) Token: 0x06019BB4 RID: 105396 RVA: 0x00354683 File Offset: 0x00352883
		public override string LocalName
		{
			get
			{
				return "tblLook";
			}
		}

		// Token: 0x17008E2F RID: 36399
		// (get) Token: 0x06019BB5 RID: 105397 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008E30 RID: 36400
		// (get) Token: 0x06019BB6 RID: 105398 RVA: 0x0035468A File Offset: 0x0035288A
		internal override int ElementTypeId
		{
			get
			{
				return 11685;
			}
		}

		// Token: 0x06019BB7 RID: 105399 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008E31 RID: 36401
		// (get) Token: 0x06019BB8 RID: 105400 RVA: 0x00354691 File Offset: 0x00352891
		internal override string[] AttributeTagNames
		{
			get
			{
				return TableLook.attributeTagNames;
			}
		}

		// Token: 0x17008E32 RID: 36402
		// (get) Token: 0x06019BB9 RID: 105401 RVA: 0x00354698 File Offset: 0x00352898
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TableLook.attributeNamespaceIds;
			}
		}

		// Token: 0x17008E33 RID: 36403
		// (get) Token: 0x06019BBA RID: 105402 RVA: 0x002EA130 File Offset: 0x002E8330
		// (set) Token: 0x06019BBB RID: 105403 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public HexBinaryValue Val
		{
			get
			{
				return (HexBinaryValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008E34 RID: 36404
		// (get) Token: 0x06019BBC RID: 105404 RVA: 0x003480EF File Offset: 0x003462EF
		// (set) Token: 0x06019BBD RID: 105405 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "firstRow")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue FirstRow
		{
			get
			{
				return (OnOffValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008E35 RID: 36405
		// (get) Token: 0x06019BBE RID: 105406 RVA: 0x003461ED File Offset: 0x003443ED
		// (set) Token: 0x06019BBF RID: 105407 RVA: 0x002BD494 File Offset: 0x002BB694
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "lastRow")]
		public OnOffValue LastRow
		{
			get
			{
				return (OnOffValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17008E36 RID: 36406
		// (get) Token: 0x06019BC0 RID: 105408 RVA: 0x003474AC File Offset: 0x003456AC
		// (set) Token: 0x06019BC1 RID: 105409 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "firstColumn")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue FirstColumn
		{
			get
			{
				return (OnOffValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17008E37 RID: 36407
		// (get) Token: 0x06019BC2 RID: 105410 RVA: 0x002EB443 File Offset: 0x002E9643
		// (set) Token: 0x06019BC3 RID: 105411 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "lastColumn")]
		public OnOffValue LastColumn
		{
			get
			{
				return (OnOffValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17008E38 RID: 36408
		// (get) Token: 0x06019BC4 RID: 105412 RVA: 0x003461FC File Offset: 0x003443FC
		// (set) Token: 0x06019BC5 RID: 105413 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "noHBand")]
		[OfficeAvailability(FileFormatVersions.Office2010)]
		public OnOffValue NoHorizontalBand
		{
			get
			{
				return (OnOffValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17008E39 RID: 36409
		// (get) Token: 0x06019BC6 RID: 105414 RVA: 0x00353104 File Offset: 0x00351304
		// (set) Token: 0x06019BC7 RID: 105415 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[OfficeAvailability(FileFormatVersions.Office2010)]
		[SchemaAttr(23, "noVBand")]
		public OnOffValue NoVerticalBand
		{
			get
			{
				return (OnOffValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x06019BC9 RID: 105417 RVA: 0x003546A0 File Offset: 0x003528A0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new HexBinaryValue();
			}
			if (23 == namespaceId && "firstRow" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "lastRow" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "firstColumn" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "lastColumn" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "noHBand" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "noVBand" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019BCA RID: 105418 RVA: 0x0035475D File Offset: 0x0035295D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableLook>(deep);
		}

		// Token: 0x0400AA57 RID: 43607
		private const string tagName = "tblLook";

		// Token: 0x0400AA58 RID: 43608
		private const byte tagNsId = 23;

		// Token: 0x0400AA59 RID: 43609
		internal const int ElementTypeIdConst = 11685;

		// Token: 0x0400AA5A RID: 43610
		private static string[] attributeTagNames = new string[] { "val", "firstRow", "lastRow", "firstColumn", "lastColumn", "noHBand", "noVBand" };

		// Token: 0x0400AA5B RID: 43611
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23, 23 };
	}
}
