using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Excel.Drawing
{
	// Token: 0x02002387 RID: 9095
	[ChildElementInfo(typeof(NonVisualDrawingPropertiesExtensionList))]
	[ChildElementInfo(typeof(HyperlinkOnHover))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HyperlinkOnClick))]
	internal class NonVisualDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x17004B77 RID: 19319
		// (get) Token: 0x060106CB RID: 67275 RVA: 0x002DE917 File Offset: 0x002DCB17
		public override string LocalName
		{
			get
			{
				return "cNvPr";
			}
		}

		// Token: 0x17004B78 RID: 19320
		// (get) Token: 0x060106CC RID: 67276 RVA: 0x002E35B9 File Offset: 0x002E17B9
		internal override byte NamespaceId
		{
			get
			{
				return 54;
			}
		}

		// Token: 0x17004B79 RID: 19321
		// (get) Token: 0x060106CD RID: 67277 RVA: 0x002E3757 File Offset: 0x002E1957
		internal override int ElementTypeId
		{
			get
			{
				return 13011;
			}
		}

		// Token: 0x060106CE RID: 67278 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004B7A RID: 19322
		// (get) Token: 0x060106CF RID: 67279 RVA: 0x002E375E File Offset: 0x002E195E
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x17004B7B RID: 19323
		// (get) Token: 0x060106D0 RID: 67280 RVA: 0x002E3765 File Offset: 0x002E1965
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x17004B7C RID: 19324
		// (get) Token: 0x060106D1 RID: 67281 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x060106D2 RID: 67282 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17004B7D RID: 19325
		// (get) Token: 0x060106D3 RID: 67283 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060106D4 RID: 67284 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17004B7E RID: 19326
		// (get) Token: 0x060106D5 RID: 67285 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x060106D6 RID: 67286 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17004B7F RID: 19327
		// (get) Token: 0x060106D7 RID: 67287 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x060106D8 RID: 67288 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x17004B80 RID: 19328
		// (get) Token: 0x060106D9 RID: 67289 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x060106DA RID: 67290 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x060106DB RID: 67291 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingProperties()
		{
		}

		// Token: 0x060106DC RID: 67292 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060106DD RID: 67293 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060106DE RID: 67294 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060106DF RID: 67295 RVA: 0x002E376C File Offset: 0x002E196C
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

		// Token: 0x17004B81 RID: 19329
		// (get) Token: 0x060106E0 RID: 67296 RVA: 0x002E37C2 File Offset: 0x002E19C2
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17004B82 RID: 19330
		// (get) Token: 0x060106E1 RID: 67297 RVA: 0x002E37C9 File Offset: 0x002E19C9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17004B83 RID: 19331
		// (get) Token: 0x060106E2 RID: 67298 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004B84 RID: 19332
		// (get) Token: 0x060106E3 RID: 67299 RVA: 0x002DE9A8 File Offset: 0x002DCBA8
		// (set) Token: 0x060106E4 RID: 67300 RVA: 0x002DE9B1 File Offset: 0x002DCBB1
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

		// Token: 0x17004B85 RID: 19333
		// (get) Token: 0x060106E5 RID: 67301 RVA: 0x002DE9BB File Offset: 0x002DCBBB
		// (set) Token: 0x060106E6 RID: 67302 RVA: 0x002DE9C4 File Offset: 0x002DCBC4
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

		// Token: 0x17004B86 RID: 19334
		// (get) Token: 0x060106E7 RID: 67303 RVA: 0x002DE9CE File Offset: 0x002DCBCE
		// (set) Token: 0x060106E8 RID: 67304 RVA: 0x002DE9D7 File Offset: 0x002DCBD7
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

		// Token: 0x060106E9 RID: 67305 RVA: 0x002E37D0 File Offset: 0x002E19D0
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

		// Token: 0x060106EA RID: 67306 RVA: 0x002E3853 File Offset: 0x002E1A53
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingProperties>(deep);
		}

		// Token: 0x060106EB RID: 67307 RVA: 0x002E385C File Offset: 0x002E1A5C
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualDrawingProperties()
		{
			byte[] array = new byte[5];
			NonVisualDrawingProperties.attributeNamespaceIds = array;
			NonVisualDrawingProperties.eleTagNames = new string[] { "hlinkClick", "hlinkHover", "extLst" };
			NonVisualDrawingProperties.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x0400748D RID: 29837
		private const string tagName = "cNvPr";

		// Token: 0x0400748E RID: 29838
		private const byte tagNsId = 54;

		// Token: 0x0400748F RID: 29839
		internal const int ElementTypeIdConst = 13011;

		// Token: 0x04007490 RID: 29840
		private static string[] attributeTagNames = new string[] { "id", "name", "descr", "hidden", "title" };

		// Token: 0x04007491 RID: 29841
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007492 RID: 29842
		private static readonly string[] eleTagNames;

		// Token: 0x04007493 RID: 29843
		private static readonly byte[] eleNamespaceIds;
	}
}
