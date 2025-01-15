using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002690 RID: 9872
	[ChildElementInfo(typeof(TextProperties))]
	[ChildElementInfo(typeof(Style))]
	[ChildElementInfo(typeof(Shape3D))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Scene3D))]
	[ChildElementInfo(typeof(ExtensionList))]
	internal class StyleLabel : OpenXmlCompositeElement
	{
		// Token: 0x17005D12 RID: 23826
		// (get) Token: 0x06012E38 RID: 77368 RVA: 0x002FDC19 File Offset: 0x002FBE19
		public override string LocalName
		{
			get
			{
				return "styleLbl";
			}
		}

		// Token: 0x17005D13 RID: 23827
		// (get) Token: 0x06012E39 RID: 77369 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005D14 RID: 23828
		// (get) Token: 0x06012E3A RID: 77370 RVA: 0x0030063E File Offset: 0x002FE83E
		internal override int ElementTypeId
		{
			get
			{
				return 10687;
			}
		}

		// Token: 0x06012E3B RID: 77371 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005D15 RID: 23829
		// (get) Token: 0x06012E3C RID: 77372 RVA: 0x00300645 File Offset: 0x002FE845
		internal override string[] AttributeTagNames
		{
			get
			{
				return StyleLabel.attributeTagNames;
			}
		}

		// Token: 0x17005D16 RID: 23830
		// (get) Token: 0x06012E3D RID: 77373 RVA: 0x0030064C File Offset: 0x002FE84C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StyleLabel.attributeNamespaceIds;
			}
		}

		// Token: 0x17005D17 RID: 23831
		// (get) Token: 0x06012E3E RID: 77374 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012E3F RID: 77375 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "name")]
		public StringValue Name
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06012E40 RID: 77376 RVA: 0x00293ECF File Offset: 0x002920CF
		public StyleLabel()
		{
		}

		// Token: 0x06012E41 RID: 77377 RVA: 0x00293ED7 File Offset: 0x002920D7
		public StyleLabel(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012E42 RID: 77378 RVA: 0x00293EE0 File Offset: 0x002920E0
		public StyleLabel(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012E43 RID: 77379 RVA: 0x00293EE9 File Offset: 0x002920E9
		public StyleLabel(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012E44 RID: 77380 RVA: 0x00300654 File Offset: 0x002FE854
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "scene3d" == name)
			{
				return new Scene3D();
			}
			if (14 == namespaceId && "sp3d" == name)
			{
				return new Shape3D();
			}
			if (14 == namespaceId && "txPr" == name)
			{
				return new TextProperties();
			}
			if (14 == namespaceId && "style" == name)
			{
				return new Style();
			}
			if (14 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005D18 RID: 23832
		// (get) Token: 0x06012E45 RID: 77381 RVA: 0x003006DA File Offset: 0x002FE8DA
		internal override string[] ElementTagNames
		{
			get
			{
				return StyleLabel.eleTagNames;
			}
		}

		// Token: 0x17005D19 RID: 23833
		// (get) Token: 0x06012E46 RID: 77382 RVA: 0x003006E1 File Offset: 0x002FE8E1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return StyleLabel.eleNamespaceIds;
			}
		}

		// Token: 0x17005D1A RID: 23834
		// (get) Token: 0x06012E47 RID: 77383 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005D1B RID: 23835
		// (get) Token: 0x06012E48 RID: 77384 RVA: 0x003006E8 File Offset: 0x002FE8E8
		// (set) Token: 0x06012E49 RID: 77385 RVA: 0x003006F1 File Offset: 0x002FE8F1
		public Scene3D Scene3D
		{
			get
			{
				return base.GetElement<Scene3D>(0);
			}
			set
			{
				base.SetElement<Scene3D>(0, value);
			}
		}

		// Token: 0x17005D1C RID: 23836
		// (get) Token: 0x06012E4A RID: 77386 RVA: 0x003006FB File Offset: 0x002FE8FB
		// (set) Token: 0x06012E4B RID: 77387 RVA: 0x00300704 File Offset: 0x002FE904
		public Shape3D Shape3D
		{
			get
			{
				return base.GetElement<Shape3D>(1);
			}
			set
			{
				base.SetElement<Shape3D>(1, value);
			}
		}

		// Token: 0x17005D1D RID: 23837
		// (get) Token: 0x06012E4C RID: 77388 RVA: 0x0030070E File Offset: 0x002FE90E
		// (set) Token: 0x06012E4D RID: 77389 RVA: 0x00300717 File Offset: 0x002FE917
		public TextProperties TextProperties
		{
			get
			{
				return base.GetElement<TextProperties>(2);
			}
			set
			{
				base.SetElement<TextProperties>(2, value);
			}
		}

		// Token: 0x17005D1E RID: 23838
		// (get) Token: 0x06012E4E RID: 77390 RVA: 0x00300721 File Offset: 0x002FE921
		// (set) Token: 0x06012E4F RID: 77391 RVA: 0x0030072A File Offset: 0x002FE92A
		public Style Style
		{
			get
			{
				return base.GetElement<Style>(3);
			}
			set
			{
				base.SetElement<Style>(3, value);
			}
		}

		// Token: 0x17005D1F RID: 23839
		// (get) Token: 0x06012E50 RID: 77392 RVA: 0x002FD198 File Offset: 0x002FB398
		// (set) Token: 0x06012E51 RID: 77393 RVA: 0x002FD1A1 File Offset: 0x002FB3A1
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(4);
			}
			set
			{
				base.SetElement<ExtensionList>(4, value);
			}
		}

		// Token: 0x06012E52 RID: 77394 RVA: 0x002D1473 File Offset: 0x002CF673
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012E53 RID: 77395 RVA: 0x00300734 File Offset: 0x002FE934
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleLabel>(deep);
		}

		// Token: 0x06012E54 RID: 77396 RVA: 0x00300740 File Offset: 0x002FE940
		// Note: this type is marked as 'beforefieldinit'.
		static StyleLabel()
		{
			byte[] array = new byte[1];
			StyleLabel.attributeNamespaceIds = array;
			StyleLabel.eleTagNames = new string[] { "scene3d", "sp3d", "txPr", "style", "extLst" };
			StyleLabel.eleNamespaceIds = new byte[] { 14, 14, 14, 14, 14 };
		}

		// Token: 0x04008216 RID: 33302
		private const string tagName = "styleLbl";

		// Token: 0x04008217 RID: 33303
		private const byte tagNsId = 14;

		// Token: 0x04008218 RID: 33304
		internal const int ElementTypeIdConst = 10687;

		// Token: 0x04008219 RID: 33305
		private static string[] attributeTagNames = new string[] { "name" };

		// Token: 0x0400821A RID: 33306
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400821B RID: 33307
		private static readonly string[] eleTagNames;

		// Token: 0x0400821C RID: 33308
		private static readonly byte[] eleNamespaceIds;
	}
}
