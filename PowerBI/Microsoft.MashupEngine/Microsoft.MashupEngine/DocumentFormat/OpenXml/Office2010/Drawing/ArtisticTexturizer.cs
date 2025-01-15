using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002370 RID: 9072
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class ArtisticTexturizer : OpenXmlLeafElement
	{
		// Token: 0x17004AC2 RID: 19138
		// (get) Token: 0x06010535 RID: 66869 RVA: 0x002E2217 File Offset: 0x002E0417
		public override string LocalName
		{
			get
			{
				return "artisticTexturizer";
			}
		}

		// Token: 0x17004AC3 RID: 19139
		// (get) Token: 0x06010536 RID: 66870 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004AC4 RID: 19140
		// (get) Token: 0x06010537 RID: 66871 RVA: 0x002E221E File Offset: 0x002E041E
		internal override int ElementTypeId
		{
			get
			{
				return 12755;
			}
		}

		// Token: 0x06010538 RID: 66872 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004AC5 RID: 19141
		// (get) Token: 0x06010539 RID: 66873 RVA: 0x002E2225 File Offset: 0x002E0425
		internal override string[] AttributeTagNames
		{
			get
			{
				return ArtisticTexturizer.attributeTagNames;
			}
		}

		// Token: 0x17004AC6 RID: 19142
		// (get) Token: 0x0601053A RID: 66874 RVA: 0x002E222C File Offset: 0x002E042C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ArtisticTexturizer.attributeNamespaceIds;
			}
		}

		// Token: 0x17004AC7 RID: 19143
		// (get) Token: 0x0601053B RID: 66875 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x0601053C RID: 66876 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004AC8 RID: 19144
		// (get) Token: 0x0601053D RID: 66877 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x0601053E RID: 66878 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x06010540 RID: 66880 RVA: 0x002E1B6B File Offset: 0x002DFD6B
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

		// Token: 0x06010541 RID: 66881 RVA: 0x002E2233 File Offset: 0x002E0433
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ArtisticTexturizer>(deep);
		}

		// Token: 0x06010542 RID: 66882 RVA: 0x002E223C File Offset: 0x002E043C
		// Note: this type is marked as 'beforefieldinit'.
		static ArtisticTexturizer()
		{
			byte[] array = new byte[2];
			ArtisticTexturizer.attributeNamespaceIds = array;
		}

		// Token: 0x04007423 RID: 29731
		private const string tagName = "artisticTexturizer";

		// Token: 0x04007424 RID: 29732
		private const byte tagNsId = 48;

		// Token: 0x04007425 RID: 29733
		internal const int ElementTypeIdConst = 12755;

		// Token: 0x04007426 RID: 29734
		private static string[] attributeTagNames = new string[] { "trans", "scaling" };

		// Token: 0x04007427 RID: 29735
		private static byte[] attributeNamespaceIds;
	}
}
