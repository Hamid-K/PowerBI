using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002372 RID: 9074
	[ChildElementInfo(typeof(ForegroundMark), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(BackgroundMark), FileFormatVersions.Office2010)]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class BackgroundRemoval : OpenXmlCompositeElement
	{
		// Token: 0x17004AD0 RID: 19152
		// (get) Token: 0x06010551 RID: 66897 RVA: 0x002E22CF File Offset: 0x002E04CF
		public override string LocalName
		{
			get
			{
				return "backgroundRemoval";
			}
		}

		// Token: 0x17004AD1 RID: 19153
		// (get) Token: 0x06010552 RID: 66898 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x17004AD2 RID: 19154
		// (get) Token: 0x06010553 RID: 66899 RVA: 0x002E22D6 File Offset: 0x002E04D6
		internal override int ElementTypeId
		{
			get
			{
				return 12757;
			}
		}

		// Token: 0x06010554 RID: 66900 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004AD3 RID: 19155
		// (get) Token: 0x06010555 RID: 66901 RVA: 0x002E22DD File Offset: 0x002E04DD
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackgroundRemoval.attributeTagNames;
			}
		}

		// Token: 0x17004AD4 RID: 19156
		// (get) Token: 0x06010556 RID: 66902 RVA: 0x002E22E4 File Offset: 0x002E04E4
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackgroundRemoval.attributeNamespaceIds;
			}
		}

		// Token: 0x17004AD5 RID: 19157
		// (get) Token: 0x06010557 RID: 66903 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06010558 RID: 66904 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "t")]
		public Int32Value MarqueeTop
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

		// Token: 0x17004AD6 RID: 19158
		// (get) Token: 0x06010559 RID: 66905 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x0601055A RID: 66906 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "b")]
		public Int32Value MarqueeBottom
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

		// Token: 0x17004AD7 RID: 19159
		// (get) Token: 0x0601055B RID: 66907 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x0601055C RID: 66908 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "l")]
		public Int32Value MarqueeLeft
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

		// Token: 0x17004AD8 RID: 19160
		// (get) Token: 0x0601055D RID: 66909 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x0601055E RID: 66910 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "r")]
		public Int32Value MarqueeRight
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

		// Token: 0x0601055F RID: 66911 RVA: 0x00293ECF File Offset: 0x002920CF
		public BackgroundRemoval()
		{
		}

		// Token: 0x06010560 RID: 66912 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BackgroundRemoval(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010561 RID: 66913 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BackgroundRemoval(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010562 RID: 66914 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BackgroundRemoval(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010563 RID: 66915 RVA: 0x002E22EB File Offset: 0x002E04EB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (48 == namespaceId && "foregroundMark" == name)
			{
				return new ForegroundMark();
			}
			if (48 == namespaceId && "backgroundMark" == name)
			{
				return new BackgroundMark();
			}
			return null;
		}

		// Token: 0x06010564 RID: 66916 RVA: 0x002E2320 File Offset: 0x002E0520
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "t" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "b" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "l" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "r" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010565 RID: 66917 RVA: 0x002E238D File Offset: 0x002E058D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackgroundRemoval>(deep);
		}

		// Token: 0x06010566 RID: 66918 RVA: 0x002E2398 File Offset: 0x002E0598
		// Note: this type is marked as 'beforefieldinit'.
		static BackgroundRemoval()
		{
			byte[] array = new byte[4];
			BackgroundRemoval.attributeNamespaceIds = array;
		}

		// Token: 0x0400742D RID: 29741
		private const string tagName = "backgroundRemoval";

		// Token: 0x0400742E RID: 29742
		private const byte tagNsId = 48;

		// Token: 0x0400742F RID: 29743
		internal const int ElementTypeIdConst = 12757;

		// Token: 0x04007430 RID: 29744
		private static string[] attributeTagNames = new string[] { "t", "b", "l", "r" };

		// Token: 0x04007431 RID: 29745
		private static byte[] attributeNamespaceIds;
	}
}
