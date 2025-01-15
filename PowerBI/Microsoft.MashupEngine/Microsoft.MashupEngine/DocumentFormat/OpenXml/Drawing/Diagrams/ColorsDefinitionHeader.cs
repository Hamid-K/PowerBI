using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Diagrams
{
	// Token: 0x02002647 RID: 9799
	[ChildElementInfo(typeof(ColorDefinitionTitle))]
	[ChildElementInfo(typeof(ColorTransformDescription))]
	[ChildElementInfo(typeof(ColorTransformCategories))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorsDefinitionHeader : OpenXmlCompositeElement
	{
		// Token: 0x17005B04 RID: 23300
		// (get) Token: 0x06012974 RID: 76148 RVA: 0x002FCF3F File Offset: 0x002FB13F
		public override string LocalName
		{
			get
			{
				return "colorsDefHdr";
			}
		}

		// Token: 0x17005B05 RID: 23301
		// (get) Token: 0x06012975 RID: 76149 RVA: 0x0006808E File Offset: 0x0006628E
		internal override byte NamespaceId
		{
			get
			{
				return 14;
			}
		}

		// Token: 0x17005B06 RID: 23302
		// (get) Token: 0x06012976 RID: 76150 RVA: 0x002FCF46 File Offset: 0x002FB146
		internal override int ElementTypeId
		{
			get
			{
				return 10617;
			}
		}

		// Token: 0x06012977 RID: 76151 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005B07 RID: 23303
		// (get) Token: 0x06012978 RID: 76152 RVA: 0x002FCF4D File Offset: 0x002FB14D
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorsDefinitionHeader.attributeTagNames;
			}
		}

		// Token: 0x17005B08 RID: 23304
		// (get) Token: 0x06012979 RID: 76153 RVA: 0x002FCF54 File Offset: 0x002FB154
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorsDefinitionHeader.attributeNamespaceIds;
			}
		}

		// Token: 0x17005B09 RID: 23305
		// (get) Token: 0x0601297A RID: 76154 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601297B RID: 76155 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17005B0A RID: 23306
		// (get) Token: 0x0601297C RID: 76156 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0601297D RID: 76157 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17005B0B RID: 23307
		// (get) Token: 0x0601297E RID: 76158 RVA: 0x002E1683 File Offset: 0x002DF883
		// (set) Token: 0x0601297F RID: 76159 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "resId")]
		public Int32Value ResourceId
		{
			get
			{
				return (Int32Value)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06012980 RID: 76160 RVA: 0x00293ECF File Offset: 0x002920CF
		public ColorsDefinitionHeader()
		{
		}

		// Token: 0x06012981 RID: 76161 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ColorsDefinitionHeader(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012982 RID: 76162 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ColorsDefinitionHeader(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012983 RID: 76163 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ColorsDefinitionHeader(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012984 RID: 76164 RVA: 0x002FCF5C File Offset: 0x002FB15C
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
			if (14 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x06012985 RID: 76165 RVA: 0x002FCFCC File Offset: 0x002FB1CC
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
			if (namespaceId == 0 && "resId" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06012986 RID: 76166 RVA: 0x002FD023 File Offset: 0x002FB223
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorsDefinitionHeader>(deep);
		}

		// Token: 0x06012987 RID: 76167 RVA: 0x002FD02C File Offset: 0x002FB22C
		// Note: this type is marked as 'beforefieldinit'.
		static ColorsDefinitionHeader()
		{
			byte[] array = new byte[3];
			ColorsDefinitionHeader.attributeNamespaceIds = array;
		}

		// Token: 0x040080D0 RID: 32976
		private const string tagName = "colorsDefHdr";

		// Token: 0x040080D1 RID: 32977
		private const byte tagNsId = 14;

		// Token: 0x040080D2 RID: 32978
		internal const int ElementTypeIdConst = 10617;

		// Token: 0x040080D3 RID: 32979
		private static string[] attributeTagNames = new string[] { "uniqueId", "minVer", "resId" };

		// Token: 0x040080D4 RID: 32980
		private static byte[] attributeNamespaceIds;
	}
}
