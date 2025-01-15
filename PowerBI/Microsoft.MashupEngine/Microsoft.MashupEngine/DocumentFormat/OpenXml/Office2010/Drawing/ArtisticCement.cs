using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200235D RID: 9053
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ArtisticCement : OpenXmlLeafElement
	{
		// Token: 0x17004A3D RID: 19005
		// (get) Token: 0x0601042B RID: 66603 RVA: 0x002E18A3 File Offset: 0x002DFAA3
		public override string LocalName
		{
			get
			{
				return "artisticCement";
			}
		}

		// Token: 0x17004A3E RID: 19006
		// (get) Token: 0x0601042C RID: 66604 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A3F RID: 19007
		// (get) Token: 0x0601042D RID: 66605 RVA: 0x002E18AA File Offset: 0x002DFAAA
		internal override int ElementTypeId
		{
			get
			{
				return 12736;
			}
		}

		// Token: 0x0601042E RID: 66606 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A40 RID: 19008
		// (get) Token: 0x0601042F RID: 66607 RVA: 0x002E18B1 File Offset: 0x002DFAB1
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticCement.attributeTagNames;
			}
		}

		// Token: 0x17004A41 RID: 19009
		// (get) Token: 0x06010430 RID: 66608 RVA: 0x002E18B8 File Offset: 0x002DFAB8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticCement.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A42 RID: 19010
		// (get) Token: 0x06010431 RID: 66609 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010432 RID: 66610 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004A43 RID: 19011
		// (get) Token: 0x06010433 RID: 66611 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06010434 RID: 66612 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "crackSpacing")]
		public Int32Value CrackSpacing
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

		// Token: 0x06010436 RID: 66614 RVA: 0x002E18BF File Offset: 0x002DFABF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "trans" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "crackSpacing" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010437 RID: 66615 RVA: 0x002E18F5 File Offset: 0x002DFAF5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticCement>(deep);
		}

		// Token: 0x06010438 RID: 66616 RVA: 0x002E1900 File Offset: 0x002DFB00
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticCement()
		{
			byte[] array = new byte[2];
			ArtisticCement.attributeNamespaceIds = array;
		}

		// Token: 0x040073C4 RID: 29636
		private const string tagName = "artisticCement";

		// Token: 0x040073C5 RID: 29637
		private const byte tagNsId = 48;

		// Token: 0x040073C6 RID: 29638
		internal const int ElementTypeIdConst = 12736;

		// Token: 0x040073C7 RID: 29639
		private static string[] attributeTagNames = new string[] { "trans", "crackSpacing" };

		// Token: 0x040073C8 RID: 29640
		private static byte[] attributeNamespaceIds;
	}
}
