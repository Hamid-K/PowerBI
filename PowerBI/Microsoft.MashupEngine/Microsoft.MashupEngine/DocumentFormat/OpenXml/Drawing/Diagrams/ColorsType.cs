using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002652 RID: 9810
	[ChildElementInfo(typeof(SchemeColor))]
	[ChildElementInfo(typeof(SystemColor))]
	[ChildElementInfo(typeof(HslColor))]
	[ChildElementInfo(typeof(RgbColorModelPercentage))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RgbColorModelHex))]
	[ChildElementInfo(typeof(PresetColor))]
	internal abstract class ColorsType : OpenXmlCompositeElement
	{
		// Token: 0x17005B53 RID: 23379
		// (get) Token: 0x06012A3D RID: 76349 RVA: 0x002FD90B File Offset: 0x002FBB0B
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorsType.attributeTagNames;
			}
		}

		// Token: 0x17005B54 RID: 23380
		// (get) Token: 0x06012A3E RID: 76350 RVA: 0x002FD912 File Offset: 0x002FBB12
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorsType.attributeNamespaceIds;
			}
		}

		// Token: 0x17005B55 RID: 23381
		// (get) Token: 0x06012A3F RID: 76351 RVA: 0x002FD919 File Offset: 0x002FBB19
		// (set) Token: 0x06012A40 RID: 76352 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "meth")]
		public EnumValue<ColorApplicationMethodValues> Method
		{
			get
			{
				return (EnumValue<ColorApplicationMethodValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17005B56 RID: 23382
		// (get) Token: 0x06012A41 RID: 76353 RVA: 0x002FD928 File Offset: 0x002FBB28
		// (set) Token: 0x06012A42 RID: 76354 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "hueDir")]
		public EnumValue<HueDirectionValues> HueDirection
		{
			get
			{
				return (EnumValue<HueDirectionValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06012A43 RID: 76355 RVA: 0x002FD938 File Offset: 0x002FBB38
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

		// Token: 0x06012A44 RID: 76356 RVA: 0x002FD9D6 File Offset: 0x002FBBD6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "meth" == name)
			{
				return new EnumValue<ColorApplicationMethodValues>();
			}
			if (namespaceId == 0 && "hueDir" == name)
			{
				return new EnumValue<HueDirectionValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012A45 RID: 76357 RVA: 0x00293ECF File Offset: 0x002920CF
		protected ColorsType()
		{
		}

		// Token: 0x06012A46 RID: 76358 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected ColorsType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A47 RID: 76359 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected ColorsType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A48 RID: 76360 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected ColorsType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012A49 RID: 76361 RVA: 0x002FDA0C File Offset: 0x002FBC0C
		// Note: this type is marked as 'beforefieldinit'.
		static ColorsType()
		{
			byte[] array = new byte[2];
			ColorsType.attributeNamespaceIds = array;
		}

		// Token: 0x04008101 RID: 33025
		private static string[] attributeTagNames = new string[] { "meth", "hueDir" };

		// Token: 0x04008102 RID: 33026
		private static byte[] attributeNamespaceIds;
	}
}
