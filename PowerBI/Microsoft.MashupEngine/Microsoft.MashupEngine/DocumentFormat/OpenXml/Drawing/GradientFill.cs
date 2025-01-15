using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002701 RID: 9985
	[ChildElementInfo(typeof(TileRectangle))]
	[ChildElementInfo(typeof(LinearGradientFill))]
	[ChildElementInfo(typeof(PathGradientFill))]
	[ChildElementInfo(typeof(GradientStopList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class GradientFill : OpenXmlCompositeElement
	{
		// Token: 0x17005E77 RID: 24183
		// (get) Token: 0x0601313C RID: 78140 RVA: 0x002ED0BD File Offset: 0x002EB2BD
		public override string LocalName
		{
			get
			{
				return "gradFill";
			}
		}

		// Token: 0x17005E78 RID: 24184
		// (get) Token: 0x0601313D RID: 78141 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E79 RID: 24185
		// (get) Token: 0x0601313E RID: 78142 RVA: 0x00303450 File Offset: 0x00301650
		internal override int ElementTypeId
		{
			get
			{
				return 10049;
			}
		}

		// Token: 0x0601313F RID: 78143 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005E7A RID: 24186
		// (get) Token: 0x06013140 RID: 78144 RVA: 0x00303457 File Offset: 0x00301657
		internal override string[] AttributeTagNames
		{
			get
			{
				return GradientFill.attributeTagNames;
			}
		}

		// Token: 0x17005E7B RID: 24187
		// (get) Token: 0x06013141 RID: 78145 RVA: 0x0030345E File Offset: 0x0030165E
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return GradientFill.attributeNamespaceIds;
			}
		}

		// Token: 0x17005E7C RID: 24188
		// (get) Token: 0x06013142 RID: 78146 RVA: 0x00303465 File Offset: 0x00301665
		// (set) Token: 0x06013143 RID: 78147 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "flip")]
		public EnumValue<TileFlipValues> Flip
		{
			get
			{
				return (EnumValue<TileFlipValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005E7D RID: 24189
		// (get) Token: 0x06013144 RID: 78148 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06013145 RID: 78149 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "rotWithShape")]
		public BooleanValue RotateWithShape
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06013146 RID: 78150 RVA: 0x00293ECF File Offset: 0x002920CF
		public GradientFill()
		{
		}

		// Token: 0x06013147 RID: 78151 RVA: 0x00293ED7 File Offset: 0x002920D7
		public GradientFill(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013148 RID: 78152 RVA: 0x00293EE0 File Offset: 0x002920E0
		public GradientFill(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013149 RID: 78153 RVA: 0x00293EE9 File Offset: 0x002920E9
		public GradientFill(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601314A RID: 78154 RVA: 0x00303474 File Offset: 0x00301674
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "gsLst" == name)
			{
				return new GradientStopList();
			}
			if (10 == namespaceId && "lin" == name)
			{
				return new LinearGradientFill();
			}
			if (10 == namespaceId && "path" == name)
			{
				return new PathGradientFill();
			}
			if (10 == namespaceId && "tileRect" == name)
			{
				return new TileRectangle();
			}
			return null;
		}

		// Token: 0x17005E7E RID: 24190
		// (get) Token: 0x0601314B RID: 78155 RVA: 0x003034E2 File Offset: 0x003016E2
		internal override string[] ElementTagNames
		{
			get
			{
				return GradientFill.eleTagNames;
			}
		}

		// Token: 0x17005E7F RID: 24191
		// (get) Token: 0x0601314C RID: 78156 RVA: 0x003034E9 File Offset: 0x003016E9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return GradientFill.eleNamespaceIds;
			}
		}

		// Token: 0x17005E80 RID: 24192
		// (get) Token: 0x0601314D RID: 78157 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005E81 RID: 24193
		// (get) Token: 0x0601314E RID: 78158 RVA: 0x003034F0 File Offset: 0x003016F0
		// (set) Token: 0x0601314F RID: 78159 RVA: 0x003034F9 File Offset: 0x003016F9
		public GradientStopList GradientStopList
		{
			get
			{
				return base.GetElement<GradientStopList>(0);
			}
			set
			{
				base.SetElement<GradientStopList>(0, value);
			}
		}

		// Token: 0x06013150 RID: 78160 RVA: 0x00303503 File Offset: 0x00301703
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "flip" == name)
			{
				return new EnumValue<TileFlipValues>();
			}
			if (namespaceId == 0 && "rotWithShape" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013151 RID: 78161 RVA: 0x00303539 File Offset: 0x00301739
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<GradientFill>(deep);
		}

		// Token: 0x06013152 RID: 78162 RVA: 0x00303544 File Offset: 0x00301744
		// Note: this type is marked as 'beforefieldinit'.
		static GradientFill()
		{
			byte[] array = new byte[2];
			GradientFill.attributeNamespaceIds = array;
			GradientFill.eleTagNames = new string[] { "gsLst", "lin", "path", "tileRect" };
			GradientFill.eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
		}

		// Token: 0x0400848D RID: 33933
		private const string tagName = "gradFill";

		// Token: 0x0400848E RID: 33934
		private const byte tagNsId = 10;

		// Token: 0x0400848F RID: 33935
		internal const int ElementTypeIdConst = 10049;

		// Token: 0x04008490 RID: 33936
		private static string[] attributeTagNames = new string[] { "flip", "rotWithShape" };

		// Token: 0x04008491 RID: 33937
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008492 RID: 33938
		private static readonly string[] eleTagNames;

		// Token: 0x04008493 RID: 33939
		private static readonly byte[] eleNamespaceIds;
	}
}
