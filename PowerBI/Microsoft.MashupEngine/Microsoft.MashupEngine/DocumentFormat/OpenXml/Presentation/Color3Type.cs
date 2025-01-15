using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A46 RID: 10822
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(HslColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(PresetColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(RgbColorModelPercentage), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SystemColor), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SchemeColor))]
	internal abstract class Color3Type : OpenXmlCompositeElement
	{
		// Token: 0x06015CA7 RID: 89255 RVA: 0x003232E4 File Offset: 0x003214E4
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

		// Token: 0x17007182 RID: 29058
		// (get) Token: 0x06015CA8 RID: 89256 RVA: 0x00323382 File Offset: 0x00321582
		internal override string[] ElementTagNames
		{
			get
			{
				return Color3Type.eleTagNames;
			}
		}

		// Token: 0x17007183 RID: 29059
		// (get) Token: 0x06015CA9 RID: 89257 RVA: 0x00323389 File Offset: 0x00321589
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Color3Type.eleNamespaceIds;
			}
		}

		// Token: 0x17007184 RID: 29060
		// (get) Token: 0x06015CAA RID: 89258 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17007185 RID: 29061
		// (get) Token: 0x06015CAB RID: 89259 RVA: 0x002E4994 File Offset: 0x002E2B94
		// (set) Token: 0x06015CAC RID: 89260 RVA: 0x002E499D File Offset: 0x002E2B9D
		[OfficeAvailability(FileFormatVersions.Office2010)]
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

		// Token: 0x17007186 RID: 29062
		// (get) Token: 0x06015CAD RID: 89261 RVA: 0x002E49A7 File Offset: 0x002E2BA7
		// (set) Token: 0x06015CAE RID: 89262 RVA: 0x002E49B0 File Offset: 0x002E2BB0
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

		// Token: 0x17007187 RID: 29063
		// (get) Token: 0x06015CAF RID: 89263 RVA: 0x002E49BA File Offset: 0x002E2BBA
		// (set) Token: 0x06015CB0 RID: 89264 RVA: 0x002E49C3 File Offset: 0x002E2BC3
		[OfficeAvailability(FileFormatVersions.Office2010)]
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

		// Token: 0x17007188 RID: 29064
		// (get) Token: 0x06015CB1 RID: 89265 RVA: 0x002E49CD File Offset: 0x002E2BCD
		// (set) Token: 0x06015CB2 RID: 89266 RVA: 0x002E49D6 File Offset: 0x002E2BD6
		[OfficeAvailability(FileFormatVersions.Office2010)]
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

		// Token: 0x17007189 RID: 29065
		// (get) Token: 0x06015CB3 RID: 89267 RVA: 0x002E49E0 File Offset: 0x002E2BE0
		// (set) Token: 0x06015CB4 RID: 89268 RVA: 0x002E49E9 File Offset: 0x002E2BE9
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

		// Token: 0x1700718A RID: 29066
		// (get) Token: 0x06015CB5 RID: 89269 RVA: 0x002E49F3 File Offset: 0x002E2BF3
		// (set) Token: 0x06015CB6 RID: 89270 RVA: 0x002E49FC File Offset: 0x002E2BFC
		[OfficeAvailability(FileFormatVersions.Office2010)]
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

		// Token: 0x06015CB7 RID: 89271 RVA: 0x00293ECF File Offset: 0x002920CF
		protected Color3Type()
		{
		}

		// Token: 0x06015CB8 RID: 89272 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected Color3Type(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015CB9 RID: 89273 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected Color3Type(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015CBA RID: 89274 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected Color3Type(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x040094D4 RID: 38100
		private static readonly string[] eleTagNames = new string[] { "scrgbClr", "srgbClr", "hslClr", "sysClr", "schemeClr", "prstClr" };

		// Token: 0x040094D5 RID: 38101
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
	}
}
