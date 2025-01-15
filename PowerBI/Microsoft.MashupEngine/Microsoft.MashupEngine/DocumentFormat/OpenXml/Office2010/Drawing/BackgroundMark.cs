using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x0200235B RID: 9051
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class BackgroundMark : OpenXmlLeafElement
	{
		// Token: 0x17004A2E RID: 18990
		// (get) Token: 0x0601040D RID: 66573 RVA: 0x002E1753 File Offset: 0x002DF953
		public override string LocalName
		{
			get
			{
				return "backgroundMark";
			}
		}

		// Token: 0x17004A2F RID: 18991
		// (get) Token: 0x0601040E RID: 66574 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004A30 RID: 18992
		// (get) Token: 0x0601040F RID: 66575 RVA: 0x002E175A File Offset: 0x002DF95A
		internal override int ElementTypeId
		{
			get
			{
				return 12734;
			}
		}

		// Token: 0x06010410 RID: 66576 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004A31 RID: 18993
		// (get) Token: 0x06010411 RID: 66577 RVA: 0x002E1761 File Offset: 0x002DF961
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackgroundMark.attributeTagNames;
			}
		}

		// Token: 0x17004A32 RID: 18994
		// (get) Token: 0x06010412 RID: 66578 RVA: 0x002E1768 File Offset: 0x002DF968
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackgroundMark.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A33 RID: 18995
		// (get) Token: 0x06010413 RID: 66579 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010414 RID: 66580 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004A34 RID: 18996
		// (get) Token: 0x06010415 RID: 66581 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x06010416 RID: 66582 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004A35 RID: 18997
		// (get) Token: 0x06010417 RID: 66583 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x06010418 RID: 66584 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004A36 RID: 18998
		// (get) Token: 0x06010419 RID: 66585 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x0601041A RID: 66586 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x0601041C RID: 66588 RVA: 0x002E1770 File Offset: 0x002DF970
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

		// Token: 0x0601041D RID: 66589 RVA: 0x002E17DD File Offset: 0x002DF9DD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackgroundMark>(deep);
		}

		// Token: 0x0601041E RID: 66590 RVA: 0x002E17E8 File Offset: 0x002DF9E8
		// Note: this type is marked as 'beforefieldinit'.
		static BackgroundMark()
		{
			byte[] array = new byte[4];
			BackgroundMark.attributeNamespaceIds = array;
		}

		// Token: 0x040073BA RID: 29626
		private const string tagName = "backgroundMark";

		// Token: 0x040073BB RID: 29627
		private const byte tagNsId = 48;

		// Token: 0x040073BC RID: 29628
		internal const int ElementTypeIdConst = 12734;

		// Token: 0x040073BD RID: 29629
		private static string[] attributeTagNames = new string[] { "x1", "y1", "x2", "y2" };

		// Token: 0x040073BE RID: 29630
		private static byte[] attributeNamespaceIds;
	}
}
