using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x020024BA RID: 9402
	[ChildElementInfo(typeof(NonVisualDrawingPropertiesExtensionList))]
	[ChildElementInfo(typeof(HyperlinkOnHover))]
	[ChildElementInfo(typeof(HyperlinkOnClick))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class NonVisualDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005283 RID: 21123
		// (get) Token: 0x06011693 RID: 71315 RVA: 0x002DE917 File Offset: 0x002DCB17
		public override string LocalName
		{
			get
			{
				return "cNvPr";
			}
		}

		// Token: 0x17005284 RID: 21124
		// (get) Token: 0x06011694 RID: 71316 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x17005285 RID: 21125
		// (get) Token: 0x06011695 RID: 71317 RVA: 0x002EE39C File Offset: 0x002EC59C
		internal override int ElementTypeId
		{
			get
			{
				return 12876;
			}
		}

		// Token: 0x06011696 RID: 71318 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005286 RID: 21126
		// (get) Token: 0x06011697 RID: 71319 RVA: 0x002EE3A3 File Offset: 0x002EC5A3
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x17005287 RID: 21127
		// (get) Token: 0x06011698 RID: 71320 RVA: 0x002EE3AA File Offset: 0x002EC5AA
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17005288 RID: 21128
		// (get) Token: 0x06011699 RID: 71321 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601169A RID: 71322 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public UInt32Value Id
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

		// Token: 0x17005289 RID: 21129
		// (get) Token: 0x0601169B RID: 71323 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601169C RID: 71324 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700528A RID: 21130
		// (get) Token: 0x0601169D RID: 71325 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x0601169E RID: 71326 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "descr")]
		public StringValue Description
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x1700528B RID: 21131
		// (get) Token: 0x0601169F RID: 71327 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060116A0 RID: 71328 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "hidden")]
		public BooleanValue Hidden
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700528C RID: 21132
		// (get) Token: 0x060116A1 RID: 71329 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x060116A2 RID: 71330 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "title")]
		public StringValue Title
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x060116A3 RID: 71331 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingProperties()
		{
		}

		// Token: 0x060116A4 RID: 71332 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060116A5 RID: 71333 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060116A6 RID: 71334 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060116A7 RID: 71335 RVA: 0x002EE3B4 File Offset: 0x002EC5B4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "hlinkClick" == name)
			{
				return new HyperlinkOnClick();
			}
			if (10 == namespaceId && "hlinkHover" == name)
			{
				return new HyperlinkOnHover();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new NonVisualDrawingPropertiesExtensionList();
			}
			return null;
		}

		// Token: 0x1700528D RID: 21133
		// (get) Token: 0x060116A8 RID: 71336 RVA: 0x002EE40A File Offset: 0x002EC60A
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x1700528E RID: 21134
		// (get) Token: 0x060116A9 RID: 71337 RVA: 0x002EE411 File Offset: 0x002EC611
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700528F RID: 21135
		// (get) Token: 0x060116AA RID: 71338 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005290 RID: 21136
		// (get) Token: 0x060116AB RID: 71339 RVA: 0x002DE9A8 File Offset: 0x002DCBA8
		// (set) Token: 0x060116AC RID: 71340 RVA: 0x002DE9B1 File Offset: 0x002DCBB1
		public HyperlinkOnClick HyperlinkOnClick
		{
			get
			{
				return base.GetElement<HyperlinkOnClick>(0);
			}
			set
			{
				base.SetElement<HyperlinkOnClick>(0, value);
			}
		}

		// Token: 0x17005291 RID: 21137
		// (get) Token: 0x060116AD RID: 71341 RVA: 0x002DE9BB File Offset: 0x002DCBBB
		// (set) Token: 0x060116AE RID: 71342 RVA: 0x002DE9C4 File Offset: 0x002DCBC4
		public HyperlinkOnHover HyperlinkOnHover
		{
			get
			{
				return base.GetElement<HyperlinkOnHover>(1);
			}
			set
			{
				base.SetElement<HyperlinkOnHover>(1, value);
			}
		}

		// Token: 0x17005292 RID: 21138
		// (get) Token: 0x060116AF RID: 71343 RVA: 0x002DE9CE File Offset: 0x002DCBCE
		// (set) Token: 0x060116B0 RID: 71344 RVA: 0x002DE9D7 File Offset: 0x002DCBD7
		public NonVisualDrawingPropertiesExtensionList NonVisualDrawingPropertiesExtensionList
		{
			get
			{
				return base.GetElement<NonVisualDrawingPropertiesExtensionList>(2);
			}
			set
			{
				base.SetElement<NonVisualDrawingPropertiesExtensionList>(2, value);
			}
		}

		// Token: 0x060116B1 RID: 71345 RVA: 0x002EE418 File Offset: 0x002EC618
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "descr" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "hidden" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "title" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060116B2 RID: 71346 RVA: 0x002EE49B File Offset: 0x002EC69B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingProperties>(deep);
		}

		// Token: 0x060116B3 RID: 71347 RVA: 0x002EE4A4 File Offset: 0x002EC6A4
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualDrawingProperties()
		{
			byte[] array = new byte[5];
			NonVisualDrawingProperties.attributeNamespaceIds = array;
			NonVisualDrawingProperties.eleTagNames = new string[] { "hlinkClick", "hlinkHover", "extLst" };
			NonVisualDrawingProperties.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x040079B8 RID: 31160
		private const string tagName = "cNvPr";

		// Token: 0x040079B9 RID: 31161
		private const byte tagNsId = 52;

		// Token: 0x040079BA RID: 31162
		internal const int ElementTypeIdConst = 12876;

		// Token: 0x040079BB RID: 31163
		private static string[] attributeTagNames = new string[] { "id", "name", "descr", "hidden", "title" };

		// Token: 0x040079BC RID: 31164
		private static byte[] attributeNamespaceIds;

		// Token: 0x040079BD RID: 31165
		private static readonly string[] eleTagNames;

		// Token: 0x040079BE RID: 31166
		private static readonly byte[] eleNamespaceIds;
	}
}
