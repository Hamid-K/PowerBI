using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002646 RID: 9798
	[ChildElementInfo(typeof(ExtensionList))]
	[ChildElementInfo(typeof(ColorTransformDescription))]
	[ChildElementInfo(typeof(ColorTransformCategories))]
	[ChildElementInfo(typeof(ColorTransformStyleLabel))]
	[ChildElementInfo(typeof(ColorDefinitionTitle))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorsDefinition : OpenXmlPartRootElement
	{
		// Token: 0x17005AFC RID: 23292
		// (get) Token: 0x0601295D RID: 76125 RVA: 0x002FCE17 File Offset: 0x002FB017
		public override string LocalName
		{
			get
			{
				return "colorsDef";
			}
		}

		// Token: 0x17005AFD RID: 23293
		// (get) Token: 0x0601295E RID: 76126 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005AFE RID: 23294
		// (get) Token: 0x0601295F RID: 76127 RVA: 0x002FCE1E File Offset: 0x002FB01E
		internal override int ElementTypeId
		{
			get
			{
				return 10616;
			}
		}

		// Token: 0x06012960 RID: 76128 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005AFF RID: 23295
		// (get) Token: 0x06012961 RID: 76129 RVA: 0x002FCE25 File Offset: 0x002FB025
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorsDefinition.attributeTagNames;
			}
		}

		// Token: 0x17005B00 RID: 23296
		// (get) Token: 0x06012962 RID: 76130 RVA: 0x002FCE2C File Offset: 0x002FB02C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorsDefinition.attributeNamespaceIds;
			}
		}

		// Token: 0x17005B01 RID: 23297
		// (get) Token: 0x06012963 RID: 76131 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06012964 RID: 76132 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17005B02 RID: 23298
		// (get) Token: 0x06012965 RID: 76133 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06012966 RID: 76134 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x06012967 RID: 76135 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal ColorsDefinition(DiagramColorsPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06012968 RID: 76136 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(DiagramColorsPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17005B03 RID: 23299
		// (get) Token: 0x06012969 RID: 76137 RVA: 0x002FCE33 File Offset: 0x002FB033
		// (set) Token: 0x0601296A RID: 76138 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public DiagramColorsPart DiagramColorsPart
		{
			get
			{
				return base.OpenXmlPart as DiagramColorsPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x0601296B RID: 76139 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public ColorsDefinition(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601296C RID: 76140 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public ColorsDefinition(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601296D RID: 76141 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public ColorsDefinition(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601296E RID: 76142 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public ColorsDefinition()
		{
		}

		// Token: 0x0601296F RID: 76143 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(DiagramColorsPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06012970 RID: 76144 RVA: 0x002FCE40 File Offset: 0x002FB040
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "title" == name)
			{
				return new ColorDefinitionTitle();
			}
			if (14 == namespaceId && "desc" == name)
			{
				return new ColorTransformDescription();
			}
			if (14 == namespaceId && "catLst" == name)
			{
				return new ColorTransformCategories();
			}
			if (14 == namespaceId && "styleLbl" == name)
			{
				return new ColorTransformStyleLabel();
			}
			if (14 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06012971 RID: 76145 RVA: 0x002FCEC6 File Offset: 0x002FB0C6
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

		// Token: 0x06012972 RID: 76146 RVA: 0x002FCEFC File Offset: 0x002FB0FC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorsDefinition>(deep);
		}

		// Token: 0x06012973 RID: 76147 RVA: 0x002FCF08 File Offset: 0x002FB108
		// Note: this type is marked as 'beforefieldinit'.
		static ColorsDefinition()
		{
			byte[] array = new byte[2];
			ColorsDefinition.attributeNamespaceIds = array;
		}

		// Token: 0x040080CB RID: 32971
		private const string tagName = "colorsDef";

		// Token: 0x040080CC RID: 32972
		private const byte tagNsId = 14;

		// Token: 0x040080CD RID: 32973
		internal const int ElementTypeIdConst = 10616;

		// Token: 0x040080CE RID: 32974
		private static string[] attributeTagNames = new string[] { "uniqueId", "minVer" };

		// Token: 0x040080CF RID: 32975
		private static byte[] attributeNamespaceIds;
	}
}
