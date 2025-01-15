using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200236B RID: 9067
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ArtisticPastelsSmooth : OpenXmlLeafElement
	{
		// Token: 0x17004A9F RID: 19103
		// (get) Token: 0x060104EF RID: 66799 RVA: 0x002E2013 File Offset: 0x002E0213
		public override string LocalName
		{
			get
			{
				return "artisticPastelsSmooth";
			}
		}

		// Token: 0x17004AA0 RID: 19104
		// (get) Token: 0x060104F0 RID: 66800 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004AA1 RID: 19105
		// (get) Token: 0x060104F1 RID: 66801 RVA: 0x002E201A File Offset: 0x002E021A
		internal override int ElementTypeId
		{
			get
			{
				return 12750;
			}
		}

		// Token: 0x060104F2 RID: 66802 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004AA2 RID: 19106
		// (get) Token: 0x060104F3 RID: 66803 RVA: 0x002E2021 File Offset: 0x002E0221
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticPastelsSmooth.attributeTagNames;
			}
		}

		// Token: 0x17004AA3 RID: 19107
		// (get) Token: 0x060104F4 RID: 66804 RVA: 0x002E2028 File Offset: 0x002E0228
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticPastelsSmooth.attributeNamespaceIds;
			}
		}

		// Token: 0x17004AA4 RID: 19108
		// (get) Token: 0x060104F5 RID: 66805 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060104F6 RID: 66806 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004AA5 RID: 19109
		// (get) Token: 0x060104F7 RID: 66807 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x060104F8 RID: 66808 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "scaling")]
		public Int32Value BrushSize
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

		// Token: 0x060104FA RID: 66810 RVA: 0x002E1B6B File Offset: 0x002DFD6B
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

		// Token: 0x060104FB RID: 66811 RVA: 0x002E202F File Offset: 0x002E022F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticPastelsSmooth>(deep);
		}

		// Token: 0x060104FC RID: 66812 RVA: 0x002E2038 File Offset: 0x002E0238
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticPastelsSmooth()
		{
			byte[] array = new byte[2];
			ArtisticPastelsSmooth.attributeNamespaceIds = array;
		}

		// Token: 0x0400740A RID: 29706
		private const string tagName = "artisticPastelsSmooth";

		// Token: 0x0400740B RID: 29707
		private const byte tagNsId = 48;

		// Token: 0x0400740C RID: 29708
		internal const int ElementTypeIdConst = 12750;

		// Token: 0x0400740D RID: 29709
		private static string[] attributeTagNames = new string[] { "trans", "scaling" };

		// Token: 0x0400740E RID: 29710
		private static byte[] attributeNamespaceIds;
	}
}
