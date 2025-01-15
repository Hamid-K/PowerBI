using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x0200289F RID: 10399
	[ChildElementInfo(typeof(WrapPolygon))]
	[GeneratedCode("DomGen", "2.0")]
	internal class WrapThrough : OpenXmlCompositeElement
	{
		// Token: 0x17006821 RID: 26657
		// (get) Token: 0x0601473A RID: 83770 RVA: 0x00313612 File Offset: 0x00311812
		public override string LocalName
		{
			get
			{
				return "wrapThrough";
			}
		}

		// Token: 0x17006822 RID: 26658
		// (get) Token: 0x0601473B RID: 83771 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x17006823 RID: 26659
		// (get) Token: 0x0601473C RID: 83772 RVA: 0x00313619 File Offset: 0x00311819
		internal override int ElementTypeId
		{
			get
			{
				return 10697;
			}
		}

		// Token: 0x0601473D RID: 83773 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006824 RID: 26660
		// (get) Token: 0x0601473E RID: 83774 RVA: 0x00313620 File Offset: 0x00311820
		internal override string[] AttributeTagNames
		{
			get
			{
				return WrapThrough.attributeTagNames;
			}
		}

		// Token: 0x17006825 RID: 26661
		// (get) Token: 0x0601473F RID: 83775 RVA: 0x00313627 File Offset: 0x00311827
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WrapThrough.attributeNamespaceIds;
			}
		}

		// Token: 0x17006826 RID: 26662
		// (get) Token: 0x06014740 RID: 83776 RVA: 0x003133A5 File Offset: 0x003115A5
		// (set) Token: 0x06014741 RID: 83777 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17006827 RID: 26663
		// (get) Token: 0x06014742 RID: 83778 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06014743 RID: 83779 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17006828 RID: 26664
		// (get) Token: 0x06014744 RID: 83780 RVA: 0x002E5814 File Offset: 0x002E3A14
		// (set) Token: 0x06014745 RID: 83781 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x06014746 RID: 83782 RVA: 0x00293ECF File Offset: 0x002920CF
		public WrapThrough()
		{
		}

		// Token: 0x06014747 RID: 83783 RVA: 0x00293ED7 File Offset: 0x002920D7
		public WrapThrough(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014748 RID: 83784 RVA: 0x00293EE0 File Offset: 0x002920E0
		public WrapThrough(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014749 RID: 83785 RVA: 0x00293EE9 File Offset: 0x002920E9
		public WrapThrough(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601474A RID: 83786 RVA: 0x0031350E File Offset: 0x0031170E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (16 == namespaceId && "wrapPolygon" == name)
			{
				return new WrapPolygon();
			}
			return null;
		}

		// Token: 0x17006829 RID: 26665
		// (get) Token: 0x0601474B RID: 83787 RVA: 0x0031362E File Offset: 0x0031182E
		internal override string[] ElementTagNames
		{
			get
			{
				return WrapThrough.eleTagNames;
			}
		}

		// Token: 0x1700682A RID: 26666
		// (get) Token: 0x0601474C RID: 83788 RVA: 0x00313635 File Offset: 0x00311835
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return WrapThrough.eleNamespaceIds;
			}
		}

		// Token: 0x1700682B RID: 26667
		// (get) Token: 0x0601474D RID: 83789 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700682C RID: 26668
		// (get) Token: 0x0601474E RID: 83790 RVA: 0x00313537 File Offset: 0x00311737
		// (set) Token: 0x0601474F RID: 83791 RVA: 0x00313540 File Offset: 0x00311740
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

		// Token: 0x06014750 RID: 83792 RVA: 0x0031363C File Offset: 0x0031183C
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

		// Token: 0x06014751 RID: 83793 RVA: 0x00313693 File Offset: 0x00311893
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WrapThrough>(deep);
		}

		// Token: 0x06014752 RID: 83794 RVA: 0x0031369C File Offset: 0x0031189C
		// Note: this type is marked as 'beforefieldinit'.
		static WrapThrough()
		{
			byte[] array = new byte[3];
			WrapThrough.attributeNamespaceIds = array;
			WrapThrough.eleTagNames = new string[] { "wrapPolygon" };
			WrapThrough.eleNamespaceIds = new byte[] { 16 };
		}

		// Token: 0x04008E2E RID: 36398
		private const string tagName = "wrapThrough";

		// Token: 0x04008E2F RID: 36399
		private const byte tagNsId = 16;

		// Token: 0x04008E30 RID: 36400
		internal const int ElementTypeIdConst = 10697;

		// Token: 0x04008E31 RID: 36401
		private static string[] attributeTagNames = new string[] { "wrapText", "distL", "distR" };

		// Token: 0x04008E32 RID: 36402
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008E33 RID: 36403
		private static readonly string[] eleTagNames;

		// Token: 0x04008E34 RID: 36404
		private static readonly byte[] eleNamespaceIds;
	}
}
