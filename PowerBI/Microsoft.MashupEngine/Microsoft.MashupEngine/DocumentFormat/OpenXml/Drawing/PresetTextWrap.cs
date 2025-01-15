using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002728 RID: 10024
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AdjustValueList))]
	internal class PresetTextWrap : OpenXmlCompositeElement
	{
		// Token: 0x17005FD9 RID: 24537
		// (get) Token: 0x06013426 RID: 78886 RVA: 0x0030588E File Offset: 0x00303A8E
		public override string LocalName
		{
			get
			{
				return "prstTxWarp";
			}
		}

		// Token: 0x17005FDA RID: 24538
		// (get) Token: 0x06013427 RID: 78887 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005FDB RID: 24539
		// (get) Token: 0x06013428 RID: 78888 RVA: 0x00305895 File Offset: 0x00303A95
		internal override int ElementTypeId
		{
			get
			{
				return 10087;
			}
		}

		// Token: 0x06013429 RID: 78889 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005FDC RID: 24540
		// (get) Token: 0x0601342A RID: 78890 RVA: 0x0030589C File Offset: 0x00303A9C
		internal override string[] AttributeTagNames
		{
			get
			{
				return PresetTextWrap.attributeTagNames;
			}
		}

		// Token: 0x17005FDD RID: 24541
		// (get) Token: 0x0601342B RID: 78891 RVA: 0x003058A3 File Offset: 0x00303AA3
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PresetTextWrap.attributeNamespaceIds;
			}
		}

		// Token: 0x17005FDE RID: 24542
		// (get) Token: 0x0601342C RID: 78892 RVA: 0x003058AA File Offset: 0x00303AAA
		// (set) Token: 0x0601342D RID: 78893 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "prst")]
		public EnumValue<TextShapeValues> Preset
		{
			get
			{
				return (EnumValue<TextShapeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601342E RID: 78894 RVA: 0x00293ECF File Offset: 0x002920CF
		public PresetTextWrap()
		{
		}

		// Token: 0x0601342F RID: 78895 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PresetTextWrap(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013430 RID: 78896 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PresetTextWrap(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013431 RID: 78897 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PresetTextWrap(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013432 RID: 78898 RVA: 0x003057E3 File Offset: 0x003039E3
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "avLst" == name)
			{
				return new AdjustValueList();
			}
			return null;
		}

		// Token: 0x17005FDF RID: 24543
		// (get) Token: 0x06013433 RID: 78899 RVA: 0x003058B9 File Offset: 0x00303AB9
		internal override string[] ElementTagNames
		{
			get
			{
				return PresetTextWrap.eleTagNames;
			}
		}

		// Token: 0x17005FE0 RID: 24544
		// (get) Token: 0x06013434 RID: 78900 RVA: 0x003058C0 File Offset: 0x00303AC0
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PresetTextWrap.eleNamespaceIds;
			}
		}

		// Token: 0x17005FE1 RID: 24545
		// (get) Token: 0x06013435 RID: 78901 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005FE2 RID: 24546
		// (get) Token: 0x06013436 RID: 78902 RVA: 0x003056DC File Offset: 0x003038DC
		// (set) Token: 0x06013437 RID: 78903 RVA: 0x003056E5 File Offset: 0x003038E5
		public AdjustValueList AdjustValueList
		{
			get
			{
				return base.GetElement<AdjustValueList>(0);
			}
			set
			{
				base.SetElement<AdjustValueList>(0, value);
			}
		}

		// Token: 0x06013438 RID: 78904 RVA: 0x003058C7 File Offset: 0x00303AC7
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "prst" == name)
			{
				return new EnumValue<TextShapeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013439 RID: 78905 RVA: 0x003058E7 File Offset: 0x00303AE7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PresetTextWrap>(deep);
		}

		// Token: 0x0601343A RID: 78906 RVA: 0x003058F0 File Offset: 0x00303AF0
		// Note: this type is marked as 'beforefieldinit'.
		static PresetTextWrap()
		{
			byte[] array = new byte[1];
			PresetTextWrap.attributeNamespaceIds = array;
			PresetTextWrap.eleTagNames = new string[] { "avLst" };
			PresetTextWrap.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x04008555 RID: 34133
		private const string tagName = "prstTxWarp";

		// Token: 0x04008556 RID: 34134
		private const byte tagNsId = 10;

		// Token: 0x04008557 RID: 34135
		internal const int ElementTypeIdConst = 10087;

		// Token: 0x04008558 RID: 34136
		private static string[] attributeTagNames = new string[] { "prst" };

		// Token: 0x04008559 RID: 34137
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400855A RID: 34138
		private static readonly string[] eleTagNames;

		// Token: 0x0400855B RID: 34139
		private static readonly byte[] eleNamespaceIds;
	}
}
