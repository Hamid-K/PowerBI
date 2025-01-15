using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x0200289E RID: 10398
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(WrapPolygon))]
	internal class WrapTight : OpenXmlCompositeElement
	{
		// Token: 0x17006815 RID: 26645
		// (get) Token: 0x06014721 RID: 83745 RVA: 0x003134F2 File Offset: 0x003116F2
		public override string LocalName
		{
			get
			{
				return "wrapTight";
			}
		}

		// Token: 0x17006816 RID: 26646
		// (get) Token: 0x06014722 RID: 83746 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17006817 RID: 26647
		// (get) Token: 0x06014723 RID: 83747 RVA: 0x003134F9 File Offset: 0x003116F9
		internal override int ElementTypeId
		{
			get
			{
				return 10696;
			}
		}

		// Token: 0x06014724 RID: 83748 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006818 RID: 26648
		// (get) Token: 0x06014725 RID: 83749 RVA: 0x00313500 File Offset: 0x00311700
		internal override string[] AttributeTagNames
		{
			get
			{
				return WrapTight.attributeTagNames;
			}
		}

		// Token: 0x17006819 RID: 26649
		// (get) Token: 0x06014726 RID: 83750 RVA: 0x00313507 File Offset: 0x00311707
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WrapTight.attributeNamespaceIds;
			}
		}

		// Token: 0x1700681A RID: 26650
		// (get) Token: 0x06014727 RID: 83751 RVA: 0x003133A5 File Offset: 0x003115A5
		// (set) Token: 0x06014728 RID: 83752 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "wrapText")]
		public EnumValue<WrapTextValues> WrapText
		{
			get
			{
				return (EnumValue<WrapTextValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700681B RID: 26651
		// (get) Token: 0x06014729 RID: 83753 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601472A RID: 83754 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "distL")]
		public UInt32Value DistanceFromLeft
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700681C RID: 26652
		// (get) Token: 0x0601472B RID: 83755 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x0601472C RID: 83756 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "distR")]
		public UInt32Value DistanceFromRight
		{
			get
			{
				return (UInt32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x0601472D RID: 83757 RVA: 0x00293ECF File Offset: 0x002920CF
		public WrapTight()
		{
		}

		// Token: 0x0601472E RID: 83758 RVA: 0x00293ED7 File Offset: 0x002920D7
		public WrapTight(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601472F RID: 83759 RVA: 0x00293EE0 File Offset: 0x002920E0
		public WrapTight(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014730 RID: 83760 RVA: 0x00293EE9 File Offset: 0x002920E9
		public WrapTight(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014731 RID: 83761 RVA: 0x0031350E File Offset: 0x0031170E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (16 == namespaceId && "wrapPolygon" == name)
			{
				return new WrapPolygon();
			}
			return null;
		}

		// Token: 0x1700681D RID: 26653
		// (get) Token: 0x06014732 RID: 83762 RVA: 0x00313529 File Offset: 0x00311729
		internal override string[] ElementTagNames
		{
			get
			{
				return WrapTight.eleTagNames;
			}
		}

		// Token: 0x1700681E RID: 26654
		// (get) Token: 0x06014733 RID: 83763 RVA: 0x00313530 File Offset: 0x00311730
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return WrapTight.eleNamespaceIds;
			}
		}

		// Token: 0x1700681F RID: 26655
		// (get) Token: 0x06014734 RID: 83764 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006820 RID: 26656
		// (get) Token: 0x06014735 RID: 83765 RVA: 0x00313537 File Offset: 0x00311737
		// (set) Token: 0x06014736 RID: 83766 RVA: 0x00313540 File Offset: 0x00311740
		public WrapPolygon WrapPolygon
		{
			get
			{
				return base.GetElement<WrapPolygon>(0);
			}
			set
			{
				base.SetElement<WrapPolygon>(0, value);
			}
		}

		// Token: 0x06014737 RID: 83767 RVA: 0x0031354C File Offset: 0x0031174C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "wrapText" == name)
			{
				return new EnumValue<WrapTextValues>();
			}
			if (namespaceId == 0 && "distL" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "distR" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014738 RID: 83768 RVA: 0x003135A3 File Offset: 0x003117A3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WrapTight>(deep);
		}

		// Token: 0x06014739 RID: 83769 RVA: 0x003135AC File Offset: 0x003117AC
		// Note: this type is marked as 'beforefieldinit'.
		static WrapTight()
		{
			byte[] array = new byte[3];
			WrapTight.attributeNamespaceIds = array;
			WrapTight.eleTagNames = new string[] { "wrapPolygon" };
			WrapTight.eleNamespaceIds = new byte[] { 16 };
		}

		// Token: 0x04008E27 RID: 36391
		private const string tagName = "wrapTight";

		// Token: 0x04008E28 RID: 36392
		private const byte tagNsId = 16;

		// Token: 0x04008E29 RID: 36393
		internal const int ElementTypeIdConst = 10696;

		// Token: 0x04008E2A RID: 36394
		private static string[] attributeTagNames = new string[] { "wrapText", "distL", "distR" };

		// Token: 0x04008E2B RID: 36395
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008E2C RID: 36396
		private static readonly string[] eleTagNames;

		// Token: 0x04008E2D RID: 36397
		private static readonly byte[] eleNamespaceIds;
	}
}
