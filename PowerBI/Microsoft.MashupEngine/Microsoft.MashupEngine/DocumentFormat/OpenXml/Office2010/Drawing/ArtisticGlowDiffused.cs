using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002363 RID: 9059
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ArtisticGlowDiffused : OpenXmlLeafElement
	{
		// Token: 0x17004A67 RID: 19047
		// (get) Token: 0x0601047F RID: 66687 RVA: 0x002E1BE3 File Offset: 0x002DFDE3
		public override string LocalName
		{
			get
			{
				return "artisticGlowDiffused";
			}
		}

		// Token: 0x17004A68 RID: 19048
		// (get) Token: 0x06010480 RID: 66688 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A69 RID: 19049
		// (get) Token: 0x06010481 RID: 66689 RVA: 0x002E1BEA File Offset: 0x002DFDEA
		internal override int ElementTypeId
		{
			get
			{
				return 12742;
			}
		}

		// Token: 0x06010482 RID: 66690 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A6A RID: 19050
		// (get) Token: 0x06010483 RID: 66691 RVA: 0x002E1BF1 File Offset: 0x002DFDF1
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticGlowDiffused.attributeTagNames;
			}
		}

		// Token: 0x17004A6B RID: 19051
		// (get) Token: 0x06010484 RID: 66692 RVA: 0x002E1BF8 File Offset: 0x002DFDF8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticGlowDiffused.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A6C RID: 19052
		// (get) Token: 0x06010485 RID: 66693 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010486 RID: 66694 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004A6D RID: 19053
		// (get) Token: 0x06010487 RID: 66695 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06010488 RID: 66696 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "intensity")]
		public Int32Value Intensity
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

		// Token: 0x0601048A RID: 66698 RVA: 0x002E1BFF File Offset: 0x002DFDFF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "trans" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "intensity" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601048B RID: 66699 RVA: 0x002E1C35 File Offset: 0x002DFE35
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticGlowDiffused>(deep);
		}

		// Token: 0x0601048C RID: 66700 RVA: 0x002E1C40 File Offset: 0x002DFE40
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticGlowDiffused()
		{
			byte[] array = new byte[2];
			ArtisticGlowDiffused.attributeNamespaceIds = array;
		}

		// Token: 0x040073E2 RID: 29666
		private const string tagName = "artisticGlowDiffused";

		// Token: 0x040073E3 RID: 29667
		private const byte tagNsId = 48;

		// Token: 0x040073E4 RID: 29668
		internal const int ElementTypeIdConst = 12742;

		// Token: 0x040073E5 RID: 29669
		private static string[] attributeTagNames = new string[] { "trans", "intensity" };

		// Token: 0x040073E6 RID: 29670
		private static byte[] attributeNamespaceIds;
	}
}
