using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024C6 RID: 9414
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class SphereCoordinates : OpenXmlLeafElement
	{
		// Token: 0x170052D4 RID: 21204
		// (get) Token: 0x06011751 RID: 71505 RVA: 0x002EEA02 File Offset: 0x002ECC02
		public override string LocalName
		{
			get
			{
				return "rot";
			}
		}

		// Token: 0x170052D5 RID: 21205
		// (get) Token: 0x06011752 RID: 71506 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170052D6 RID: 21206
		// (get) Token: 0x06011753 RID: 71507 RVA: 0x002EEA09 File Offset: 0x002ECC09
		internal override int ElementTypeId
		{
			get
			{
				return 12886;
			}
		}

		// Token: 0x06011754 RID: 71508 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170052D7 RID: 21207
		// (get) Token: 0x06011755 RID: 71509 RVA: 0x002EEA10 File Offset: 0x002ECC10
		internal override string[] AttributeTagNames
		{
			get
			{
				return SphereCoordinates.attributeTagNames;
			}
		}

		// Token: 0x170052D8 RID: 21208
		// (get) Token: 0x06011756 RID: 71510 RVA: 0x002EEA17 File Offset: 0x002ECC17
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SphereCoordinates.attributeNamespaceIds;
			}
		}

		// Token: 0x170052D9 RID: 21209
		// (get) Token: 0x06011757 RID: 71511 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06011758 RID: 71512 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "lat")]
		public Int32Value Lattitude
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

		// Token: 0x170052DA RID: 21210
		// (get) Token: 0x06011759 RID: 71513 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x0601175A RID: 71514 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(52, "lon")]
		public Int32Value Longitude
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

		// Token: 0x170052DB RID: 21211
		// (get) Token: 0x0601175B RID: 71515 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x0601175C RID: 71516 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(52, "rev")]
		public Int32Value Revolution
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601175E RID: 71518 RVA: 0x002EEA20 File Offset: 0x002ECC20
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "lat" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "lon" == name)
			{
				return new Int32Value();
			}
			if (52 == namespaceId && "rev" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601175F RID: 71519 RVA: 0x002EEA7D File Offset: 0x002ECC7D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SphereCoordinates>(deep);
		}

		// Token: 0x040079EA RID: 31210
		private const string tagName = "rot";

		// Token: 0x040079EB RID: 31211
		private const byte tagNsId = 52;

		// Token: 0x040079EC RID: 31212
		internal const int ElementTypeIdConst = 12886;

		// Token: 0x040079ED RID: 31213
		private static string[] attributeTagNames = new string[] { "lat", "lon", "rev" };

		// Token: 0x040079EE RID: 31214
		private static byte[] attributeNamespaceIds = new byte[] { 52, 52, 52 };
	}
}
