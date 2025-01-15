using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A2F RID: 10799
	[ChildElementInfo(typeof(SystemColor))]
	[ChildElementInfo(typeof(SchemeColor))]
	[ChildElementInfo(typeof(PresetColor))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(HslColor))]
	internal abstract class ColorType : OpenXmlCompositeElement
	{
		// Token: 0x06015AFD RID: 88829 RVA: 0x00322048 File Offset: 0x00320248
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

		// Token: 0x170070C3 RID: 28867
		// (get) Token: 0x06015AFE RID: 88830 RVA: 0x003220E6 File Offset: 0x003202E6
		internal override string[] ElementTagNames
		{
			get
			{
				return ColorType.eleTagNames;
			}
		}

		// Token: 0x170070C4 RID: 28868
		// (get) Token: 0x06015AFF RID: 88831 RVA: 0x003220ED File Offset: 0x003202ED
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ColorType.eleNamespaceIds;
			}
		}

		// Token: 0x170070C5 RID: 28869
		// (get) Token: 0x06015B00 RID: 88832 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x170070C6 RID: 28870
		// (get) Token: 0x06015B01 RID: 88833 RVA: 0x002E4994 File Offset: 0x002E2B94
		// (set) Token: 0x06015B02 RID: 88834 RVA: 0x002E499D File Offset: 0x002E2B9D
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

		// Token: 0x170070C7 RID: 28871
		// (get) Token: 0x06015B03 RID: 88835 RVA: 0x002E49A7 File Offset: 0x002E2BA7
		// (set) Token: 0x06015B04 RID: 88836 RVA: 0x002E49B0 File Offset: 0x002E2BB0
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

		// Token: 0x170070C8 RID: 28872
		// (get) Token: 0x06015B05 RID: 88837 RVA: 0x002E49BA File Offset: 0x002E2BBA
		// (set) Token: 0x06015B06 RID: 88838 RVA: 0x002E49C3 File Offset: 0x002E2BC3
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

		// Token: 0x170070C9 RID: 28873
		// (get) Token: 0x06015B07 RID: 88839 RVA: 0x002E49CD File Offset: 0x002E2BCD
		// (set) Token: 0x06015B08 RID: 88840 RVA: 0x002E49D6 File Offset: 0x002E2BD6
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

		// Token: 0x170070CA RID: 28874
		// (get) Token: 0x06015B09 RID: 88841 RVA: 0x002E49E0 File Offset: 0x002E2BE0
		// (set) Token: 0x06015B0A RID: 88842 RVA: 0x002E49E9 File Offset: 0x002E2BE9
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

		// Token: 0x170070CB RID: 28875
		// (get) Token: 0x06015B0B RID: 88843 RVA: 0x002E49F3 File Offset: 0x002E2BF3
		// (set) Token: 0x06015B0C RID: 88844 RVA: 0x002E49FC File Offset: 0x002E2BFC
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

		// Token: 0x06015B0D RID: 88845 RVA: 0x00293ECF File Offset: 0x002920CF
		protected ColorType()
		{
		}

		// Token: 0x06015B0E RID: 88846 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected ColorType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015B0F RID: 88847 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected ColorType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015B10 RID: 88848 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected ColorType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04009467 RID: 37991
		private static readonly string[] eleTagNames = new string[] { "scrgbClr", "srgbClr", "hslClr", "sysClr", "schemeClr", "prstClr" };

		// Token: 0x04009468 RID: 37992
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
	}
}
