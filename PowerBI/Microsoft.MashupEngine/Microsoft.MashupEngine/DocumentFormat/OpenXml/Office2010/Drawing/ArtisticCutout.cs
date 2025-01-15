using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002360 RID: 9056
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ArtisticCutout : OpenXmlLeafElement
	{
		// Token: 0x17004A52 RID: 19026
		// (get) Token: 0x06010455 RID: 66645 RVA: 0x002E1A27 File Offset: 0x002DFC27
		public override string LocalName
		{
			get
			{
				return "artisticCutout";
			}
		}

		// Token: 0x17004A53 RID: 19027
		// (get) Token: 0x06010456 RID: 66646 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A54 RID: 19028
		// (get) Token: 0x06010457 RID: 66647 RVA: 0x002E1A2E File Offset: 0x002DFC2E
		internal override int ElementTypeId
		{
			get
			{
				return 12739;
			}
		}

		// Token: 0x06010458 RID: 66648 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A55 RID: 19029
		// (get) Token: 0x06010459 RID: 66649 RVA: 0x002E1A35 File Offset: 0x002DFC35
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticCutout.attributeTagNames;
			}
		}

		// Token: 0x17004A56 RID: 19030
		// (get) Token: 0x0601045A RID: 66650 RVA: 0x002E1A3C File Offset: 0x002DFC3C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticCutout.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A57 RID: 19031
		// (get) Token: 0x0601045B RID: 66651 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601045C RID: 66652 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004A58 RID: 19032
		// (get) Token: 0x0601045D RID: 66653 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x0601045E RID: 66654 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "numberOfShades")]
		public Int32Value NumberOfShades
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

		// Token: 0x06010460 RID: 66656 RVA: 0x002E1A43 File Offset: 0x002DFC43
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "trans" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "numberOfShades" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010461 RID: 66657 RVA: 0x002E1A79 File Offset: 0x002DFC79
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticCutout>(deep);
		}

		// Token: 0x06010462 RID: 66658 RVA: 0x002E1A84 File Offset: 0x002DFC84
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticCutout()
		{
			byte[] array = new byte[2];
			ArtisticCutout.attributeNamespaceIds = array;
		}

		// Token: 0x040073D3 RID: 29651
		private const string tagName = "artisticCutout";

		// Token: 0x040073D4 RID: 29652
		private const byte tagNsId = 48;

		// Token: 0x040073D5 RID: 29653
		internal const int ElementTypeIdConst = 12739;

		// Token: 0x040073D6 RID: 29654
		private static string[] attributeTagNames = new string[] { "trans", "numberOfShades" };

		// Token: 0x040073D7 RID: 29655
		private static byte[] attributeNamespaceIds;
	}
}
