using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002361 RID: 9057
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ArtisticFilmGrain : OpenXmlLeafElement
	{
		// Token: 0x17004A59 RID: 19033
		// (get) Token: 0x06010463 RID: 66659 RVA: 0x002E1ABB File Offset: 0x002DFCBB
		public override string LocalName
		{
			get
			{
				return "artisticFilmGrain";
			}
		}

		// Token: 0x17004A5A RID: 19034
		// (get) Token: 0x06010464 RID: 66660 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A5B RID: 19035
		// (get) Token: 0x06010465 RID: 66661 RVA: 0x002E1AC2 File Offset: 0x002DFCC2
		internal override int ElementTypeId
		{
			get
			{
				return 12740;
			}
		}

		// Token: 0x06010466 RID: 66662 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A5C RID: 19036
		// (get) Token: 0x06010467 RID: 66663 RVA: 0x002E1AC9 File Offset: 0x002DFCC9
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticFilmGrain.attributeTagNames;
			}
		}

		// Token: 0x17004A5D RID: 19037
		// (get) Token: 0x06010468 RID: 66664 RVA: 0x002E1AD0 File Offset: 0x002DFCD0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticFilmGrain.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A5E RID: 19038
		// (get) Token: 0x06010469 RID: 66665 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601046A RID: 66666 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004A5F RID: 19039
		// (get) Token: 0x0601046B RID: 66667 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x0601046C RID: 66668 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "grainSize")]
		public Int32Value GrainSize
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

		// Token: 0x0601046E RID: 66670 RVA: 0x002E1AD7 File Offset: 0x002DFCD7
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "trans" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "grainSize" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601046F RID: 66671 RVA: 0x002E1B0D File Offset: 0x002DFD0D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticFilmGrain>(deep);
		}

		// Token: 0x06010470 RID: 66672 RVA: 0x002E1B18 File Offset: 0x002DFD18
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticFilmGrain()
		{
			byte[] array = new byte[2];
			ArtisticFilmGrain.attributeNamespaceIds = array;
		}

		// Token: 0x040073D8 RID: 29656
		private const string tagName = "artisticFilmGrain";

		// Token: 0x040073D9 RID: 29657
		private const byte tagNsId = 48;

		// Token: 0x040073DA RID: 29658
		internal const int ElementTypeIdConst = 12740;

		// Token: 0x040073DB RID: 29659
		private static string[] attributeTagNames = new string[] { "trans", "grainSize" };

		// Token: 0x040073DC RID: 29660
		private static byte[] attributeNamespaceIds;
	}
}
