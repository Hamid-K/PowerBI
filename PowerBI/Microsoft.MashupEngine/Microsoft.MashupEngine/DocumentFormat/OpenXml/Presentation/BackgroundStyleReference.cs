using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x020029F9 RID: 10745
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(SystemColor))]
	[ChildElementInfo(typeof(SchemeColor))]
	[ChildElementInfo(typeof(PresetColor))]
	internal class BackgroundStyleReference : OpenXmlCompositeElement
	{
		// Token: 0x17006EAB RID: 28331
		// (get) Token: 0x06015651 RID: 87633 RVA: 0x0031E8CB File Offset: 0x0031CACB
		public override string LocalName
		{
			get
			{
				return "bgRef";
			}
		}

		// Token: 0x17006EAC RID: 28332
		// (get) Token: 0x06015652 RID: 87634 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17006EAD RID: 28333
		// (get) Token: 0x06015653 RID: 87635 RVA: 0x0031E8D2 File Offset: 0x0031CAD2
		internal override int ElementTypeId
		{
			get
			{
				return 12172;
			}
		}

		// Token: 0x06015654 RID: 87636 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006EAE RID: 28334
		// (get) Token: 0x06015655 RID: 87637 RVA: 0x0031E8D9 File Offset: 0x0031CAD9
		internal override string[] AttributeTagNames
		{
			get
			{
				return BackgroundStyleReference.attributeTagNames;
			}
		}

		// Token: 0x17006EAF RID: 28335
		// (get) Token: 0x06015656 RID: 87638 RVA: 0x0031E8E0 File Offset: 0x0031CAE0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BackgroundStyleReference.attributeNamespaceIds;
			}
		}

		// Token: 0x17006EB0 RID: 28336
		// (get) Token: 0x06015657 RID: 87639 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06015658 RID: 87640 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "idx")]
		public UInt32Value Index
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

		// Token: 0x06015659 RID: 87641 RVA: 0x00293ECF File Offset: 0x002920CF
		public BackgroundStyleReference()
		{
		}

		// Token: 0x0601565A RID: 87642 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BackgroundStyleReference(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601565B RID: 87643 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BackgroundStyleReference(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601565C RID: 87644 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BackgroundStyleReference(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601565D RID: 87645 RVA: 0x0031E8E8 File Offset: 0x0031CAE8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "scrgbClr" == name)
			{
				return new RgbColorModelPercentage();
			}
			if (10 == namespaceId && "srgbClr" == name)
			{
				return new RgbColorModelHex();
			}
			if (10 == namespaceId && "hslClr" == name)
			{
				return new HslColor();
			}
			if (10 == namespaceId && "sysClr" == name)
			{
				return new SystemColor();
			}
			if (10 == namespaceId && "schemeClr" == name)
			{
				return new SchemeColor();
			}
			if (10 == namespaceId && "prstClr" == name)
			{
				return new PresetColor();
			}
			return null;
		}

		// Token: 0x17006EB1 RID: 28337
		// (get) Token: 0x0601565E RID: 87646 RVA: 0x0031E986 File Offset: 0x0031CB86
		internal override string[] ElementTagNames
		{
			get
			{
				return BackgroundStyleReference.eleTagNames;
			}
		}

		// Token: 0x17006EB2 RID: 28338
		// (get) Token: 0x0601565F RID: 87647 RVA: 0x0031E98D File Offset: 0x0031CB8D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BackgroundStyleReference.eleNamespaceIds;
			}
		}

		// Token: 0x17006EB3 RID: 28339
		// (get) Token: 0x06015660 RID: 87648 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17006EB4 RID: 28340
		// (get) Token: 0x06015661 RID: 87649 RVA: 0x002E4994 File Offset: 0x002E2B94
		// (set) Token: 0x06015662 RID: 87650 RVA: 0x002E499D File Offset: 0x002E2B9D
		public RgbColorModelPercentage RgbColorModelPercentage
		{
			get
			{
				return base.GetElement<RgbColorModelPercentage>(0);
			}
			set
			{
				base.SetElement<RgbColorModelPercentage>(0, value);
			}
		}

		// Token: 0x17006EB5 RID: 28341
		// (get) Token: 0x06015663 RID: 87651 RVA: 0x002E49A7 File Offset: 0x002E2BA7
		// (set) Token: 0x06015664 RID: 87652 RVA: 0x002E49B0 File Offset: 0x002E2BB0
		public RgbColorModelHex RgbColorModelHex
		{
			get
			{
				return base.GetElement<RgbColorModelHex>(1);
			}
			set
			{
				base.SetElement<RgbColorModelHex>(1, value);
			}
		}

		// Token: 0x17006EB6 RID: 28342
		// (get) Token: 0x06015665 RID: 87653 RVA: 0x002E49BA File Offset: 0x002E2BBA
		// (set) Token: 0x06015666 RID: 87654 RVA: 0x002E49C3 File Offset: 0x002E2BC3
		public HslColor HslColor
		{
			get
			{
				return base.GetElement<HslColor>(2);
			}
			set
			{
				base.SetElement<HslColor>(2, value);
			}
		}

		// Token: 0x17006EB7 RID: 28343
		// (get) Token: 0x06015667 RID: 87655 RVA: 0x002E49CD File Offset: 0x002E2BCD
		// (set) Token: 0x06015668 RID: 87656 RVA: 0x002E49D6 File Offset: 0x002E2BD6
		public SystemColor SystemColor
		{
			get
			{
				return base.GetElement<SystemColor>(3);
			}
			set
			{
				base.SetElement<SystemColor>(3, value);
			}
		}

		// Token: 0x17006EB8 RID: 28344
		// (get) Token: 0x06015669 RID: 87657 RVA: 0x002E49E0 File Offset: 0x002E2BE0
		// (set) Token: 0x0601566A RID: 87658 RVA: 0x002E49E9 File Offset: 0x002E2BE9
		public SchemeColor SchemeColor
		{
			get
			{
				return base.GetElement<SchemeColor>(4);
			}
			set
			{
				base.SetElement<SchemeColor>(4, value);
			}
		}

		// Token: 0x17006EB9 RID: 28345
		// (get) Token: 0x0601566B RID: 87659 RVA: 0x002E49F3 File Offset: 0x002E2BF3
		// (set) Token: 0x0601566C RID: 87660 RVA: 0x002E49FC File Offset: 0x002E2BFC
		public PresetColor PresetColor
		{
			get
			{
				return base.GetElement<PresetColor>(5);
			}
			set
			{
				base.SetElement<PresetColor>(5, value);
			}
		}

		// Token: 0x0601566D RID: 87661 RVA: 0x002F3927 File Offset: 0x002F1B27
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "idx" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601566E RID: 87662 RVA: 0x0031E994 File Offset: 0x0031CB94
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackgroundStyleReference>(deep);
		}

		// Token: 0x0601566F RID: 87663 RVA: 0x0031E9A0 File Offset: 0x0031CBA0
		// Note: this type is marked as 'beforefieldinit'.
		static BackgroundStyleReference()
		{
			byte[] array = new byte[1];
			BackgroundStyleReference.attributeNamespaceIds = array;
			BackgroundStyleReference.eleTagNames = new string[] { "scrgbClr", "srgbClr", "hslClr", "sysClr", "schemeClr", "prstClr" };
			BackgroundStyleReference.eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
		}

		// Token: 0x04009354 RID: 37716
		private const string tagName = "bgRef";

		// Token: 0x04009355 RID: 37717
		private const byte tagNsId = 24;

		// Token: 0x04009356 RID: 37718
		internal const int ElementTypeIdConst = 12172;

		// Token: 0x04009357 RID: 37719
		private static string[] attributeTagNames = new string[] { "idx" };

		// Token: 0x04009358 RID: 37720
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009359 RID: 37721
		private static readonly string[] eleTagNames;

		// Token: 0x0400935A RID: 37722
		private static readonly byte[] eleNamespaceIds;
	}
}
