using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002367 RID: 9063
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ArtisticMarker : OpenXmlLeafElement
	{
		// Token: 0x17004A83 RID: 19075
		// (get) Token: 0x060104B7 RID: 66743 RVA: 0x002E1E33 File Offset: 0x002E0033
		public override string LocalName
		{
			get
			{
				return "artisticMarker";
			}
		}

		// Token: 0x17004A84 RID: 19076
		// (get) Token: 0x060104B8 RID: 66744 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A85 RID: 19077
		// (get) Token: 0x060104B9 RID: 66745 RVA: 0x002E1E3A File Offset: 0x002E003A
		internal override int ElementTypeId
		{
			get
			{
				return 12746;
			}
		}

		// Token: 0x060104BA RID: 66746 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A86 RID: 19078
		// (get) Token: 0x060104BB RID: 66747 RVA: 0x002E1E41 File Offset: 0x002E0041
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticMarker.attributeTagNames;
			}
		}

		// Token: 0x17004A87 RID: 19079
		// (get) Token: 0x060104BC RID: 66748 RVA: 0x002E1E48 File Offset: 0x002E0048
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticMarker.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A88 RID: 19080
		// (get) Token: 0x060104BD RID: 66749 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060104BE RID: 66750 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004A89 RID: 19081
		// (get) Token: 0x060104BF RID: 66751 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060104C0 RID: 66752 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "size")]
		public Int32Value Size
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

		// Token: 0x060104C2 RID: 66754 RVA: 0x002E1E4F File Offset: 0x002E004F
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "trans" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "size" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060104C3 RID: 66755 RVA: 0x002E1E85 File Offset: 0x002E0085
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticMarker>(deep);
		}

		// Token: 0x060104C4 RID: 66756 RVA: 0x002E1E90 File Offset: 0x002E0090
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticMarker()
		{
			byte[] array = new byte[2];
			ArtisticMarker.attributeNamespaceIds = array;
		}

		// Token: 0x040073F6 RID: 29686
		private const string tagName = "artisticMarker";

		// Token: 0x040073F7 RID: 29687
		private const byte tagNsId = 48;

		// Token: 0x040073F8 RID: 29688
		internal const int ElementTypeIdConst = 12746;

		// Token: 0x040073F9 RID: 29689
		private static string[] attributeTagNames = new string[] { "trans", "size" };

		// Token: 0x040073FA RID: 29690
		private static byte[] attributeNamespaceIds;
	}
}
