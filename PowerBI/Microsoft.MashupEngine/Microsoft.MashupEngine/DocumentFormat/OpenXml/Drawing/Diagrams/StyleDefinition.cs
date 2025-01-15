using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200264E RID: 9806
	[ChildElementInfo(typeof(Scene3D))]
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(StyleDefinitionTitle))]
	[ChildElementInfo(typeof(StyleLabel))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(StyleLabelDescription))]
	[ChildElementInfo(typeof(StyleDisplayCategories))]
	internal class StyleDefinition : OpenXmlPartRootElement
	{
		// Token: 0x17005B39 RID: 23353
		// (get) Token: 0x060129FA RID: 76282 RVA: 0x002FD614 File Offset: 0x002FB814
		public override string LocalName
		{
			get
			{
				return "styleDef";
			}
		}

		// Token: 0x17005B3A RID: 23354
		// (get) Token: 0x060129FB RID: 76283 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B3B RID: 23355
		// (get) Token: 0x060129FC RID: 76284 RVA: 0x002FD61B File Offset: 0x002FB81B
		internal override int ElementTypeId
		{
			get
			{
				return 10624;
			}
		}

		// Token: 0x060129FD RID: 76285 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005B3C RID: 23356
		// (get) Token: 0x060129FE RID: 76286 RVA: 0x002FD622 File Offset: 0x002FB822
		internal override string[] AttributeTagNames
		{
			get
			{
				return StyleDefinition.attributeTagNames;
			}
		}

		// Token: 0x17005B3D RID: 23357
		// (get) Token: 0x060129FF RID: 76287 RVA: 0x002FD629 File Offset: 0x002FB829
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StyleDefinition.attributeNamespaceIds;
			}
		}

		// Token: 0x17005B3E RID: 23358
		// (get) Token: 0x06012A00 RID: 76288 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012A01 RID: 76289 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uniqueId")]
		public StringValue UniqueId
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

		// Token: 0x17005B3F RID: 23359
		// (get) Token: 0x06012A02 RID: 76290 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06012A03 RID: 76291 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "minVer")]
		public StringValue MinVersion
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

		// Token: 0x06012A04 RID: 76292 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal StyleDefinition(DiagramStylePart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06012A05 RID: 76293 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(DiagramStylePart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17005B40 RID: 23360
		// (get) Token: 0x06012A06 RID: 76294 RVA: 0x002FD630 File Offset: 0x002FB830
		// (set) Token: 0x06012A07 RID: 76295 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public DiagramStylePart DiagramStylePart
		{
			get
			{
				return base.OpenXmlPart as DiagramStylePart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06012A08 RID: 76296 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public StyleDefinition(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A09 RID: 76297 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public StyleDefinition(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012A0A RID: 76298 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public StyleDefinition(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012A0B RID: 76299 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public StyleDefinition()
		{
		}

		// Token: 0x06012A0C RID: 76300 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(DiagramStylePart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06012A0D RID: 76301 RVA: 0x002FD640 File Offset: 0x002FB840
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "title" == name)
			{
				return new StyleDefinitionTitle();
			}
			if (14 == namespaceId && "desc" == name)
			{
				return new StyleLabelDescription();
			}
			if (14 == namespaceId && "catLst" == name)
			{
				return new StyleDisplayCategories();
			}
			if (14 == namespaceId && "scene3d" == name)
			{
				return new Scene3D();
			}
			if (14 == namespaceId && "styleLbl" == name)
			{
				return new StyleLabel();
			}
			if (14 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06012A0E RID: 76302 RVA: 0x002FCEC6 File Offset: 0x002FB0C6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uniqueId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "minVer" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012A0F RID: 76303 RVA: 0x002FD6DE File Offset: 0x002FB8DE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StyleDefinition>(deep);
		}

		// Token: 0x06012A10 RID: 76304 RVA: 0x002FD6E8 File Offset: 0x002FB8E8
		// Note: this type is marked as 'beforefieldinit'.
		static StyleDefinition()
		{
			byte[] array = new byte[2];
			StyleDefinition.attributeNamespaceIds = array;
		}

		// Token: 0x040080EF RID: 33007
		private const string tagName = "styleDef";

		// Token: 0x040080F0 RID: 33008
		private const byte tagNsId = 14;

		// Token: 0x040080F1 RID: 33009
		internal const int ElementTypeIdConst = 10624;

		// Token: 0x040080F2 RID: 33010
		private static string[] attributeTagNames = new string[] { "uniqueId", "minVer" };

		// Token: 0x040080F3 RID: 33011
		private static byte[] attributeNamespaceIds;
	}
}
