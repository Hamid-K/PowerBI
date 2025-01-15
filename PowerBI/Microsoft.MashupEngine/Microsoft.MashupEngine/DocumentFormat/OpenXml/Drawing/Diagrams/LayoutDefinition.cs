using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x0200264A RID: 9802
	[ChildElementInfo(typeof(LayoutNode))]
	[ChildElementInfo(typeof(CategoryList))]
	[ChildElementInfo(typeof(StyleData))]
	[ChildElementInfo(typeof(ColorData))]
	[ChildElementInfo(typeof(Title))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SampleData))]
	[ChildElementInfo(typeof(Description))]
	internal class LayoutDefinition : OpenXmlPartRootElement
	{
		// Token: 0x17005B1B RID: 23323
		// (get) Token: 0x060129AF RID: 76207 RVA: 0x002FD20C File Offset: 0x002FB40C
		public override string LocalName
		{
			get
			{
				return "layoutDef";
			}
		}

		// Token: 0x17005B1C RID: 23324
		// (get) Token: 0x060129B0 RID: 76208 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B1D RID: 23325
		// (get) Token: 0x060129B1 RID: 76209 RVA: 0x002FD213 File Offset: 0x002FB413
		internal override int ElementTypeId
		{
			get
			{
				return 10620;
			}
		}

		// Token: 0x060129B2 RID: 76210 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005B1E RID: 23326
		// (get) Token: 0x060129B3 RID: 76211 RVA: 0x002FD21A File Offset: 0x002FB41A
		internal override string[] AttributeTagNames
		{
			get
			{
				return LayoutDefinition.attributeTagNames;
			}
		}

		// Token: 0x17005B1F RID: 23327
		// (get) Token: 0x060129B4 RID: 76212 RVA: 0x002FD221 File Offset: 0x002FB421
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LayoutDefinition.attributeNamespaceIds;
			}
		}

		// Token: 0x17005B20 RID: 23328
		// (get) Token: 0x060129B5 RID: 76213 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060129B6 RID: 76214 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17005B21 RID: 23329
		// (get) Token: 0x060129B7 RID: 76215 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060129B8 RID: 76216 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17005B22 RID: 23330
		// (get) Token: 0x060129B9 RID: 76217 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x060129BA RID: 76218 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "defStyle")]
		public StringValue DefaultStyle
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

		// Token: 0x060129BB RID: 76219 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal LayoutDefinition(DiagramLayoutDefinitionPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x060129BC RID: 76220 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(DiagramLayoutDefinitionPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x17005B23 RID: 23331
		// (get) Token: 0x060129BD RID: 76221 RVA: 0x002FD228 File Offset: 0x002FB428
		// (set) Token: 0x060129BE RID: 76222 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public DiagramLayoutDefinitionPart DiagramLayoutDefinitionPart
		{
			get
			{
				return base.OpenXmlPart as DiagramLayoutDefinitionPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x060129BF RID: 76223 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public LayoutDefinition(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060129C0 RID: 76224 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public LayoutDefinition(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060129C1 RID: 76225 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public LayoutDefinition(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060129C2 RID: 76226 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public LayoutDefinition()
		{
		}

		// Token: 0x060129C3 RID: 76227 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(DiagramLayoutDefinitionPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x060129C4 RID: 76228 RVA: 0x002FD238 File Offset: 0x002FB438
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (14 == namespaceId && "title" == name)
			{
				return new Title();
			}
			if (14 == namespaceId && "desc" == name)
			{
				return new Description();
			}
			if (14 == namespaceId && "catLst" == name)
			{
				return new CategoryList();
			}
			if (14 == namespaceId && "sampData" == name)
			{
				return new SampleData();
			}
			if (14 == namespaceId && "styleData" == name)
			{
				return new StyleData();
			}
			if (14 == namespaceId && "clrData" == name)
			{
				return new ColorData();
			}
			if (14 == namespaceId && "layoutNode" == name)
			{
				return new LayoutNode();
			}
			if (14 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x060129C5 RID: 76229 RVA: 0x002FD308 File Offset: 0x002FB508
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
			if (namespaceId == 0 && "defStyle" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060129C6 RID: 76230 RVA: 0x002FD35F File Offset: 0x002FB55F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LayoutDefinition>(deep);
		}

		// Token: 0x060129C7 RID: 76231 RVA: 0x002FD368 File Offset: 0x002FB568
		// Note: this type is marked as 'beforefieldinit'.
		static LayoutDefinition()
		{
			byte[] array = new byte[3];
			LayoutDefinition.attributeNamespaceIds = array;
		}

		// Token: 0x040080DD RID: 32989
		private const string tagName = "layoutDef";

		// Token: 0x040080DE RID: 32990
		private const byte tagNsId = 14;

		// Token: 0x040080DF RID: 32991
		internal const int ElementTypeIdConst = 10620;

		// Token: 0x040080E0 RID: 32992
		private static string[] attributeTagNames = new string[] { "uniqueId", "minVer", "defStyle" };

		// Token: 0x040080E1 RID: 32993
		private static byte[] attributeNamespaceIds;
	}
}
