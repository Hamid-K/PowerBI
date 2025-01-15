using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Wordprocessing
{
	// Token: 0x020028A0 RID: 10400
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(EffectExtent))]
	internal class WrapTopBottom : OpenXmlCompositeElement
	{
		// Token: 0x1700682D RID: 26669
		// (get) Token: 0x06014753 RID: 83795 RVA: 0x00313702 File Offset: 0x00311902
		public override string LocalName
		{
			get
			{
				return "wrapTopAndBottom";
			}
		}

		// Token: 0x1700682E RID: 26670
		// (get) Token: 0x06014754 RID: 83796 RVA: 0x00227072 File Offset: 0x00225272
		internal override byte NamespaceId
		{
			get
			{
				return 16;
			}
		}

		// Token: 0x1700682F RID: 26671
		// (get) Token: 0x06014755 RID: 83797 RVA: 0x00313709 File Offset: 0x00311909
		internal override int ElementTypeId
		{
			get
			{
				return 10698;
			}
		}

		// Token: 0x06014756 RID: 83798 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006830 RID: 26672
		// (get) Token: 0x06014757 RID: 83799 RVA: 0x00313710 File Offset: 0x00311910
		internal override string[] AttributeTagNames
		{
			get
			{
				return WrapTopBottom.attributeTagNames;
			}
		}

		// Token: 0x17006831 RID: 26673
		// (get) Token: 0x06014758 RID: 83800 RVA: 0x00313717 File Offset: 0x00311917
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WrapTopBottom.attributeNamespaceIds;
			}
		}

		// Token: 0x17006832 RID: 26674
		// (get) Token: 0x06014759 RID: 83801 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601475A RID: 83802 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "distT")]
		public UInt32Value DistanceFromTop
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17006833 RID: 26675
		// (get) Token: 0x0601475B RID: 83803 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x0601475C RID: 83804 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "distB")]
		public UInt32Value DistanceFromBottom
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

		// Token: 0x0601475D RID: 83805 RVA: 0x00293ECF File Offset: 0x002920CF
		public WrapTopBottom()
		{
		}

		// Token: 0x0601475E RID: 83806 RVA: 0x00293ED7 File Offset: 0x002920D7
		public WrapTopBottom(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601475F RID: 83807 RVA: 0x00293EE0 File Offset: 0x002920E0
		public WrapTopBottom(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014760 RID: 83808 RVA: 0x00293EE9 File Offset: 0x002920E9
		public WrapTopBottom(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014761 RID: 83809 RVA: 0x003133B4 File Offset: 0x003115B4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (16 == namespaceId && "effectExtent" == name)
			{
				return new EffectExtent();
			}
			return null;
		}

		// Token: 0x17006834 RID: 26676
		// (get) Token: 0x06014762 RID: 83810 RVA: 0x0031371E File Offset: 0x0031191E
		internal override string[] ElementTagNames
		{
			get
			{
				return WrapTopBottom.eleTagNames;
			}
		}

		// Token: 0x17006835 RID: 26677
		// (get) Token: 0x06014763 RID: 83811 RVA: 0x00313725 File Offset: 0x00311925
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return WrapTopBottom.eleNamespaceIds;
			}
		}

		// Token: 0x17006836 RID: 26678
		// (get) Token: 0x06014764 RID: 83812 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006837 RID: 26679
		// (get) Token: 0x06014765 RID: 83813 RVA: 0x003133DD File Offset: 0x003115DD
		// (set) Token: 0x06014766 RID: 83814 RVA: 0x003133E6 File Offset: 0x003115E6
		public EffectExtent EffectExtent
		{
			get
			{
				return base.GetElement<EffectExtent>(0);
			}
			set
			{
				base.SetElement<EffectExtent>(0, value);
			}
		}

		// Token: 0x06014767 RID: 83815 RVA: 0x0031372C File Offset: 0x0031192C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "distT" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "distB" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014768 RID: 83816 RVA: 0x00313762 File Offset: 0x00311962
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WrapTopBottom>(deep);
		}

		// Token: 0x06014769 RID: 83817 RVA: 0x0031376C File Offset: 0x0031196C
		// Note: this type is marked as 'beforefieldinit'.
		static WrapTopBottom()
		{
			byte[] array = new byte[2];
			WrapTopBottom.attributeNamespaceIds = array;
			WrapTopBottom.eleTagNames = new string[] { "effectExtent" };
			WrapTopBottom.eleNamespaceIds = new byte[] { 16 };
		}

		// Token: 0x04008E35 RID: 36405
		private const string tagName = "wrapTopAndBottom";

		// Token: 0x04008E36 RID: 36406
		private const byte tagNsId = 16;

		// Token: 0x04008E37 RID: 36407
		internal const int ElementTypeIdConst = 10698;

		// Token: 0x04008E38 RID: 36408
		private static string[] attributeTagNames = new string[] { "distT", "distB" };

		// Token: 0x04008E39 RID: 36409
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008E3A RID: 36410
		private static readonly string[] eleTagNames;

		// Token: 0x04008E3B RID: 36411
		private static readonly byte[] eleNamespaceIds;
	}
}
