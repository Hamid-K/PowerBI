using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002362 RID: 9058
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ArtisticGlass : OpenXmlLeafElement
	{
		// Token: 0x17004A60 RID: 19040
		// (get) Token: 0x06010471 RID: 66673 RVA: 0x002E1B4F File Offset: 0x002DFD4F
		public override string LocalName
		{
			get
			{
				return "artisticGlass";
			}
		}

		// Token: 0x17004A61 RID: 19041
		// (get) Token: 0x06010472 RID: 66674 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A62 RID: 19042
		// (get) Token: 0x06010473 RID: 66675 RVA: 0x002E1B56 File Offset: 0x002DFD56
		internal override int ElementTypeId
		{
			get
			{
				return 12741;
			}
		}

		// Token: 0x06010474 RID: 66676 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A63 RID: 19043
		// (get) Token: 0x06010475 RID: 66677 RVA: 0x002E1B5D File Offset: 0x002DFD5D
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticGlass.attributeTagNames;
			}
		}

		// Token: 0x17004A64 RID: 19044
		// (get) Token: 0x06010476 RID: 66678 RVA: 0x002E1B64 File Offset: 0x002DFD64
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticGlass.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A65 RID: 19045
		// (get) Token: 0x06010477 RID: 66679 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010478 RID: 66680 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004A66 RID: 19046
		// (get) Token: 0x06010479 RID: 66681 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x0601047A RID: 66682 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "scaling")]
		public Int32Value Scaling
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

		// Token: 0x0601047C RID: 66684 RVA: 0x002E1B6B File Offset: 0x002DFD6B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "trans" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "scaling" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601047D RID: 66685 RVA: 0x002E1BA1 File Offset: 0x002DFDA1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticGlass>(deep);
		}

		// Token: 0x0601047E RID: 66686 RVA: 0x002E1BAC File Offset: 0x002DFDAC
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticGlass()
		{
			byte[] array = new byte[2];
			ArtisticGlass.attributeNamespaceIds = array;
		}

		// Token: 0x040073DD RID: 29661
		private const string tagName = "artisticGlass";

		// Token: 0x040073DE RID: 29662
		private const byte tagNsId = 48;

		// Token: 0x040073DF RID: 29663
		internal const int ElementTypeIdConst = 12741;

		// Token: 0x040073E0 RID: 29664
		private static string[] attributeTagNames = new string[] { "trans", "scaling" };

		// Token: 0x040073E1 RID: 29665
		private static byte[] attributeNamespaceIds;
	}
}
