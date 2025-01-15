using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200235E RID: 9054
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ArtisticChalkSketch : OpenXmlLeafElement
	{
		// Token: 0x17004A44 RID: 19012
		// (get) Token: 0x06010439 RID: 66617 RVA: 0x002E1937 File Offset: 0x002DFB37
		public override string LocalName
		{
			get
			{
				return "artisticChalkSketch";
			}
		}

		// Token: 0x17004A45 RID: 19013
		// (get) Token: 0x0601043A RID: 66618 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A46 RID: 19014
		// (get) Token: 0x0601043B RID: 66619 RVA: 0x002E193E File Offset: 0x002DFB3E
		internal override int ElementTypeId
		{
			get
			{
				return 12737;
			}
		}

		// Token: 0x0601043C RID: 66620 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A47 RID: 19015
		// (get) Token: 0x0601043D RID: 66621 RVA: 0x002E1945 File Offset: 0x002DFB45
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticChalkSketch.attributeTagNames;
			}
		}

		// Token: 0x17004A48 RID: 19016
		// (get) Token: 0x0601043E RID: 66622 RVA: 0x002E194C File Offset: 0x002DFB4C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticChalkSketch.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A49 RID: 19017
		// (get) Token: 0x0601043F RID: 66623 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010440 RID: 66624 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004A4A RID: 19018
		// (get) Token: 0x06010441 RID: 66625 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06010442 RID: 66626 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "pressure")]
		public Int32Value Pressure
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

		// Token: 0x06010444 RID: 66628 RVA: 0x002E1953 File Offset: 0x002DFB53
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "trans" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "pressure" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010445 RID: 66629 RVA: 0x002E1989 File Offset: 0x002DFB89
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticChalkSketch>(deep);
		}

		// Token: 0x06010446 RID: 66630 RVA: 0x002E1994 File Offset: 0x002DFB94
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticChalkSketch()
		{
			byte[] array = new byte[2];
			ArtisticChalkSketch.attributeNamespaceIds = array;
		}

		// Token: 0x040073C9 RID: 29641
		private const string tagName = "artisticChalkSketch";

		// Token: 0x040073CA RID: 29642
		private const byte tagNsId = 48;

		// Token: 0x040073CB RID: 29643
		internal const int ElementTypeIdConst = 12737;

		// Token: 0x040073CC RID: 29644
		private static string[] attributeTagNames = new string[] { "trans", "pressure" };

		// Token: 0x040073CD RID: 29645
		private static byte[] attributeNamespaceIds;
	}
}
