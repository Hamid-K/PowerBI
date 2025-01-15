using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200235A RID: 9050
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ForegroundMark : OpenXmlLeafElement
	{
		// Token: 0x17004A25 RID: 18981
		// (get) Token: 0x060103FB RID: 66555 RVA: 0x002E1667 File Offset: 0x002DF867
		public override string LocalName
		{
			get
			{
				return "foregroundMark";
			}
		}

		// Token: 0x17004A26 RID: 18982
		// (get) Token: 0x060103FC RID: 66556 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A27 RID: 18983
		// (get) Token: 0x060103FD RID: 66557 RVA: 0x002E166E File Offset: 0x002DF86E
		internal override int ElementTypeId
		{
			get
			{
				return 12733;
			}
		}

		// Token: 0x060103FE RID: 66558 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A28 RID: 18984
		// (get) Token: 0x060103FF RID: 66559 RVA: 0x002E1675 File Offset: 0x002DF875
		internal override string[] AttributeTagNames
		{
			get
			{
				return ForegroundMark.attributeTagNames;
			}
		}

		// Token: 0x17004A29 RID: 18985
		// (get) Token: 0x06010400 RID: 66560 RVA: 0x002E167C File Offset: 0x002DF87C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ForegroundMark.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A2A RID: 18986
		// (get) Token: 0x06010401 RID: 66561 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010402 RID: 66562 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "x1")]
		public Int32Value FirstXCoordinate
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

		// Token: 0x17004A2B RID: 18987
		// (get) Token: 0x06010403 RID: 66563 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06010404 RID: 66564 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "y1")]
		public Int32Value FirstYCoordinate
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

		// Token: 0x17004A2C RID: 18988
		// (get) Token: 0x06010405 RID: 66565 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06010406 RID: 66566 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "x2")]
		public Int32Value SecondXCoordinate
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17004A2D RID: 18989
		// (get) Token: 0x06010407 RID: 66567 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x06010408 RID: 66568 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "y2")]
		public Int32Value SecondYCoordinate
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x0601040A RID: 66570 RVA: 0x002E1694 File Offset: 0x002DF894
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "x1" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "y1" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "x2" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "y2" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601040B RID: 66571 RVA: 0x002E1701 File Offset: 0x002DF901
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ForegroundMark>(deep);
		}

		// Token: 0x0601040C RID: 66572 RVA: 0x002E170C File Offset: 0x002DF90C
		// Note: this type is marked as 'beforefieldinit'.
		static ForegroundMark()
		{
			byte[] array = new byte[4];
			ForegroundMark.attributeNamespaceIds = array;
		}

		// Token: 0x040073B5 RID: 29621
		private const string tagName = "foregroundMark";

		// Token: 0x040073B6 RID: 29622
		private const byte tagNsId = 48;

		// Token: 0x040073B7 RID: 29623
		internal const int ElementTypeIdConst = 12733;

		// Token: 0x040073B8 RID: 29624
		private static string[] attributeTagNames = new string[] { "x1", "y1", "x2", "y2" };

		// Token: 0x040073B9 RID: 29625
		private static byte[] attributeNamespaceIds;
	}
}
