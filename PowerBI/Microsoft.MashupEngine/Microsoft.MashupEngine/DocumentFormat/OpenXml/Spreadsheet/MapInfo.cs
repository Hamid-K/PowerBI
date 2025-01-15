using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B17 RID: 11031
	[ChildElementInfo(typeof(Schema))]
	[ChildElementInfo(typeof(Map))]
	[GeneratedCode("DomGen", "2.0")]
	internal class MapInfo : OpenXmlPartRootElement
	{
		// Token: 0x170075C4 RID: 30148
		// (get) Token: 0x0601663C RID: 91708 RVA: 0x00329888 File Offset: 0x00327A88
		public override string LocalName
		{
			get
			{
				return "MapInfo";
			}
		}

		// Token: 0x170075C5 RID: 30149
		// (get) Token: 0x0601663D RID: 91709 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170075C6 RID: 30150
		// (get) Token: 0x0601663E RID: 91710 RVA: 0x0032988F File Offset: 0x00327A8F
		internal override int ElementTypeId
		{
			get
			{
				return 11029;
			}
		}

		// Token: 0x0601663F RID: 91711 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170075C7 RID: 30151
		// (get) Token: 0x06016640 RID: 91712 RVA: 0x00329896 File Offset: 0x00327A96
		internal override string[] AttributeTagNames
		{
			get
			{
				return MapInfo.attributeTagNames;
			}
		}

		// Token: 0x170075C8 RID: 30152
		// (get) Token: 0x06016641 RID: 91713 RVA: 0x0032989D File Offset: 0x00327A9D
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return MapInfo.attributeNamespaceIds;
			}
		}

		// Token: 0x170075C9 RID: 30153
		// (get) Token: 0x06016642 RID: 91714 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016643 RID: 91715 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "SelectionNamespaces")]
		public StringValue SelectionNamespaces
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

		// Token: 0x06016644 RID: 91716 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal MapInfo(CustomXmlMappingsPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06016645 RID: 91717 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(CustomXmlMappingsPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x170075CA RID: 30154
		// (get) Token: 0x06016646 RID: 91718 RVA: 0x003298A4 File Offset: 0x00327AA4
		// (set) Token: 0x06016647 RID: 91719 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public CustomXmlMappingsPart CustomXmlMappingsPart
		{
			get
			{
				return base.OpenXmlPart as CustomXmlMappingsPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x06016648 RID: 91720 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public MapInfo(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016649 RID: 91721 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public MapInfo(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601664A RID: 91722 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public MapInfo(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601664B RID: 91723 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public MapInfo()
		{
		}

		// Token: 0x0601664C RID: 91724 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(CustomXmlMappingsPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x0601664D RID: 91725 RVA: 0x003298B1 File Offset: 0x00327AB1
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "Schema" == name)
			{
				return new Schema();
			}
			if (22 == namespaceId && "Map" == name)
			{
				return new Map();
			}
			return null;
		}

		// Token: 0x0601664E RID: 91726 RVA: 0x003298E4 File Offset: 0x00327AE4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "SelectionNamespaces" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601664F RID: 91727 RVA: 0x00329904 File Offset: 0x00327B04
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MapInfo>(deep);
		}

		// Token: 0x06016650 RID: 91728 RVA: 0x00329910 File Offset: 0x00327B10
		// Note: this type is marked as 'beforefieldinit'.
		static MapInfo()
		{
			byte[] array = new byte[1];
			MapInfo.attributeNamespaceIds = array;
		}

		// Token: 0x040098D2 RID: 39122
		private const string tagName = "MapInfo";

		// Token: 0x040098D3 RID: 39123
		private const byte tagNsId = 22;

		// Token: 0x040098D4 RID: 39124
		internal const int ElementTypeIdConst = 11029;

		// Token: 0x040098D5 RID: 39125
		private static string[] attributeTagNames = new string[] { "SelectionNamespaces" };

		// Token: 0x040098D6 RID: 39126
		private static byte[] attributeNamespaceIds;
	}
}
