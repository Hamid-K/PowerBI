using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200277B RID: 10107
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(SystemColor))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(PresetColor))]
	internal abstract class Color2Type : OpenXmlCompositeElement
	{
		// Token: 0x06013853 RID: 79955 RVA: 0x0030810C File Offset: 0x0030630C
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
			if (10 == namespaceId && "prstClr" == name)
			{
				return new PresetColor();
			}
			return null;
		}

		// Token: 0x170061A2 RID: 24994
		// (get) Token: 0x06013854 RID: 79956 RVA: 0x00308192 File Offset: 0x00306392
		internal override string[] ElementTagNames
		{
			get
			{
				return Color2Type.eleTagNames;
			}
		}

		// Token: 0x170061A3 RID: 24995
		// (get) Token: 0x06013855 RID: 79957 RVA: 0x00308199 File Offset: 0x00306399
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Color2Type.eleNamespaceIds;
			}
		}

		// Token: 0x170061A4 RID: 24996
		// (get) Token: 0x06013856 RID: 79958 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x170061A5 RID: 24997
		// (get) Token: 0x06013857 RID: 79959 RVA: 0x002E4994 File Offset: 0x002E2B94
		// (set) Token: 0x06013858 RID: 79960 RVA: 0x002E499D File Offset: 0x002E2B9D
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

		// Token: 0x170061A6 RID: 24998
		// (get) Token: 0x06013859 RID: 79961 RVA: 0x002E49A7 File Offset: 0x002E2BA7
		// (set) Token: 0x0601385A RID: 79962 RVA: 0x002E49B0 File Offset: 0x002E2BB0
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

		// Token: 0x170061A7 RID: 24999
		// (get) Token: 0x0601385B RID: 79963 RVA: 0x002E49BA File Offset: 0x002E2BBA
		// (set) Token: 0x0601385C RID: 79964 RVA: 0x002E49C3 File Offset: 0x002E2BC3
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

		// Token: 0x170061A8 RID: 25000
		// (get) Token: 0x0601385D RID: 79965 RVA: 0x002E49CD File Offset: 0x002E2BCD
		// (set) Token: 0x0601385E RID: 79966 RVA: 0x002E49D6 File Offset: 0x002E2BD6
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

		// Token: 0x170061A9 RID: 25001
		// (get) Token: 0x0601385F RID: 79967 RVA: 0x003081A0 File Offset: 0x003063A0
		// (set) Token: 0x06013860 RID: 79968 RVA: 0x003081A9 File Offset: 0x003063A9
		public PresetColor PresetColor
		{
			get
			{
				return base.GetElement<PresetColor>(4);
			}
			set
			{
				base.SetElement<PresetColor>(4, value);
			}
		}

		// Token: 0x06013861 RID: 79969 RVA: 0x00293ECF File Offset: 0x002920CF
		protected Color2Type()
		{
		}

		// Token: 0x06013862 RID: 79970 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected Color2Type(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013863 RID: 79971 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected Color2Type(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013864 RID: 79972 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected Color2Type(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0400868F RID: 34447
		private static readonly string[] eleTagNames = new string[] { "scrgbClr", "srgbClr", "hslClr", "sysClr", "prstClr" };

		// Token: 0x04008690 RID: 34448
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10, 10 };
	}
}
