using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200273D RID: 10045
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(SystemColor))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(SchemeColor))]
	[ChildElementInfo(typeof(PresetColor))]
	internal abstract class ColorType : OpenXmlCompositeElement
	{
		// Token: 0x06013533 RID: 79155 RVA: 0x00306190 File Offset: 0x00304390
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

		// Token: 0x1700604F RID: 24655
		// (get) Token: 0x06013534 RID: 79156 RVA: 0x0030622E File Offset: 0x0030442E
		internal override string[] ElementTagNames
		{
			get
			{
				return ColorType.eleTagNames;
			}
		}

		// Token: 0x17006050 RID: 24656
		// (get) Token: 0x06013535 RID: 79157 RVA: 0x00306235 File Offset: 0x00304435
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ColorType.eleNamespaceIds;
			}
		}

		// Token: 0x17006051 RID: 24657
		// (get) Token: 0x06013536 RID: 79158 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17006052 RID: 24658
		// (get) Token: 0x06013537 RID: 79159 RVA: 0x002E4994 File Offset: 0x002E2B94
		// (set) Token: 0x06013538 RID: 79160 RVA: 0x002E499D File Offset: 0x002E2B9D
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

		// Token: 0x17006053 RID: 24659
		// (get) Token: 0x06013539 RID: 79161 RVA: 0x002E49A7 File Offset: 0x002E2BA7
		// (set) Token: 0x0601353A RID: 79162 RVA: 0x002E49B0 File Offset: 0x002E2BB0
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

		// Token: 0x17006054 RID: 24660
		// (get) Token: 0x0601353B RID: 79163 RVA: 0x002E49BA File Offset: 0x002E2BBA
		// (set) Token: 0x0601353C RID: 79164 RVA: 0x002E49C3 File Offset: 0x002E2BC3
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

		// Token: 0x17006055 RID: 24661
		// (get) Token: 0x0601353D RID: 79165 RVA: 0x002E49CD File Offset: 0x002E2BCD
		// (set) Token: 0x0601353E RID: 79166 RVA: 0x002E49D6 File Offset: 0x002E2BD6
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

		// Token: 0x17006056 RID: 24662
		// (get) Token: 0x0601353F RID: 79167 RVA: 0x002E49E0 File Offset: 0x002E2BE0
		// (set) Token: 0x06013540 RID: 79168 RVA: 0x002E49E9 File Offset: 0x002E2BE9
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

		// Token: 0x17006057 RID: 24663
		// (get) Token: 0x06013541 RID: 79169 RVA: 0x002E49F3 File Offset: 0x002E2BF3
		// (set) Token: 0x06013542 RID: 79170 RVA: 0x002E49FC File Offset: 0x002E2BFC
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

		// Token: 0x06013543 RID: 79171 RVA: 0x00293ECF File Offset: 0x002920CF
		protected ColorType()
		{
		}

		// Token: 0x06013544 RID: 79172 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected ColorType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013545 RID: 79173 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected ColorType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013546 RID: 79174 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected ColorType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x040085A6 RID: 34214
		private static readonly string[] eleTagNames = new string[] { "scrgbClr", "srgbClr", "hslClr", "sysClr", "schemeClr", "prstClr" };

		// Token: 0x040085A7 RID: 34215
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
	}
}
