using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200235C RID: 9052
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ArtisticBlur : OpenXmlLeafElement
	{
		// Token: 0x17004A37 RID: 18999
		// (get) Token: 0x0601041F RID: 66591 RVA: 0x002E182F File Offset: 0x002DFA2F
		public override string LocalName
		{
			get
			{
				return "artisticBlur";
			}
		}

		// Token: 0x17004A38 RID: 19000
		// (get) Token: 0x06010420 RID: 66592 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A39 RID: 19001
		// (get) Token: 0x06010421 RID: 66593 RVA: 0x002E1836 File Offset: 0x002DFA36
		internal override int ElementTypeId
		{
			get
			{
				return 12735;
			}
		}

		// Token: 0x06010422 RID: 66594 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A3A RID: 19002
		// (get) Token: 0x06010423 RID: 66595 RVA: 0x002E183D File Offset: 0x002DFA3D
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticBlur.attributeTagNames;
			}
		}

		// Token: 0x17004A3B RID: 19003
		// (get) Token: 0x06010424 RID: 66596 RVA: 0x002E1844 File Offset: 0x002DFA44
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticBlur.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A3C RID: 19004
		// (get) Token: 0x06010425 RID: 66597 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010426 RID: 66598 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "radius")]
		public Int32Value Radius
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

		// Token: 0x06010428 RID: 66600 RVA: 0x002E184B File Offset: 0x002DFA4B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "radius" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010429 RID: 66601 RVA: 0x002E186B File Offset: 0x002DFA6B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticBlur>(deep);
		}

		// Token: 0x0601042A RID: 66602 RVA: 0x002E1874 File Offset: 0x002DFA74
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticBlur()
		{
			byte[] array = new byte[1];
			ArtisticBlur.attributeNamespaceIds = array;
		}

		// Token: 0x040073BF RID: 29631
		private const string tagName = "artisticBlur";

		// Token: 0x040073C0 RID: 29632
		private const byte tagNsId = 48;

		// Token: 0x040073C1 RID: 29633
		internal const int ElementTypeIdConst = 12735;

		// Token: 0x040073C2 RID: 29634
		private static string[] attributeTagNames = new string[] { "radius" };

		// Token: 0x040073C3 RID: 29635
		private static byte[] attributeNamespaceIds;
	}
}
