using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200272F RID: 10031
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PresetColor))]
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(SystemColor))]
	[ChildElementInfo(typeof(SchemeColor))]
	internal abstract class StyleMatrixReferenceType : OpenXmlCompositeElement
	{
		// Token: 0x17006004 RID: 24580
		// (get) Token: 0x06013483 RID: 78979 RVA: 0x00305BA0 File Offset: 0x00303DA0
		internal override string[] AttributeTagNames
		{
			get
			{
				return StyleMatrixReferenceType.attributeTagNames;
			}
		}

		// Token: 0x17006005 RID: 24581
		// (get) Token: 0x06013484 RID: 78980 RVA: 0x00305BA7 File Offset: 0x00303DA7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StyleMatrixReferenceType.attributeNamespaceIds;
			}
		}

		// Token: 0x17006006 RID: 24582
		// (get) Token: 0x06013485 RID: 78981 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06013486 RID: 78982 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06013487 RID: 78983 RVA: 0x00305BB0 File Offset: 0x00303DB0
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

		// Token: 0x17006007 RID: 24583
		// (get) Token: 0x06013488 RID: 78984 RVA: 0x00305C4E File Offset: 0x00303E4E
		internal override string[] ElementTagNames
		{
			get
			{
				return StyleMatrixReferenceType.eleTagNames;
			}
		}

		// Token: 0x17006008 RID: 24584
		// (get) Token: 0x06013489 RID: 78985 RVA: 0x00305C55 File Offset: 0x00303E55
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return StyleMatrixReferenceType.eleNamespaceIds;
			}
		}

		// Token: 0x17006009 RID: 24585
		// (get) Token: 0x0601348A RID: 78986 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x1700600A RID: 24586
		// (get) Token: 0x0601348B RID: 78987 RVA: 0x002E4994 File Offset: 0x002E2B94
		// (set) Token: 0x0601348C RID: 78988 RVA: 0x002E499D File Offset: 0x002E2B9D
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

		// Token: 0x1700600B RID: 24587
		// (get) Token: 0x0601348D RID: 78989 RVA: 0x002E49A7 File Offset: 0x002E2BA7
		// (set) Token: 0x0601348E RID: 78990 RVA: 0x002E49B0 File Offset: 0x002E2BB0
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

		// Token: 0x1700600C RID: 24588
		// (get) Token: 0x0601348F RID: 78991 RVA: 0x002E49BA File Offset: 0x002E2BBA
		// (set) Token: 0x06013490 RID: 78992 RVA: 0x002E49C3 File Offset: 0x002E2BC3
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

		// Token: 0x1700600D RID: 24589
		// (get) Token: 0x06013491 RID: 78993 RVA: 0x002E49CD File Offset: 0x002E2BCD
		// (set) Token: 0x06013492 RID: 78994 RVA: 0x002E49D6 File Offset: 0x002E2BD6
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

		// Token: 0x1700600E RID: 24590
		// (get) Token: 0x06013493 RID: 78995 RVA: 0x002E49E0 File Offset: 0x002E2BE0
		// (set) Token: 0x06013494 RID: 78996 RVA: 0x002E49E9 File Offset: 0x002E2BE9
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

		// Token: 0x1700600F RID: 24591
		// (get) Token: 0x06013495 RID: 78997 RVA: 0x002E49F3 File Offset: 0x002E2BF3
		// (set) Token: 0x06013496 RID: 78998 RVA: 0x002E49FC File Offset: 0x002E2BFC
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

		// Token: 0x06013497 RID: 78999 RVA: 0x002F3927 File Offset: 0x002F1B27
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "idx" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013498 RID: 79000 RVA: 0x00293ECF File Offset: 0x002920CF
		protected StyleMatrixReferenceType()
		{
		}

		// Token: 0x06013499 RID: 79001 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected StyleMatrixReferenceType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601349A RID: 79002 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected StyleMatrixReferenceType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601349B RID: 79003 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected StyleMatrixReferenceType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601349C RID: 79004 RVA: 0x00305C5C File Offset: 0x00303E5C
		// Note: this type is marked as 'beforefieldinit'.
		static StyleMatrixReferenceType()
		{
			byte[] array = new byte[1];
			StyleMatrixReferenceType.attributeNamespaceIds = array;
			StyleMatrixReferenceType.eleTagNames = new string[] { "scrgbClr", "srgbClr", "hslClr", "sysClr", "schemeClr", "prstClr" };
			StyleMatrixReferenceType.eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10, 10 };
		}

		// Token: 0x04008574 RID: 34164
		private static string[] attributeTagNames = new string[] { "idx" };

		// Token: 0x04008575 RID: 34165
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008576 RID: 34166
		private static readonly string[] eleTagNames;

		// Token: 0x04008577 RID: 34167
		private static readonly byte[] eleNamespaceIds;
	}
}
