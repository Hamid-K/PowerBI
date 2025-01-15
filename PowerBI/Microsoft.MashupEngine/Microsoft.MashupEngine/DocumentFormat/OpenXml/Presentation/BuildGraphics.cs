using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A42 RID: 10818
	[ChildElementInfo(typeof(BuildAsOne))]
	[ChildElementInfo(typeof(BuildSubElement))]
	[GeneratedCode("DomGen", "2.0")]
	internal class BuildGraphics : OpenXmlCompositeElement
	{
		// Token: 0x17007164 RID: 29028
		// (get) Token: 0x06015C60 RID: 89184 RVA: 0x00322FEF File Offset: 0x003211EF
		public override string LocalName
		{
			get
			{
				return "bldGraphic";
			}
		}

		// Token: 0x17007165 RID: 29029
		// (get) Token: 0x06015C61 RID: 89185 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007166 RID: 29030
		// (get) Token: 0x06015C62 RID: 89186 RVA: 0x00322FF6 File Offset: 0x003211F6
		internal override int ElementTypeId
		{
			get
			{
				return 12237;
			}
		}

		// Token: 0x06015C63 RID: 89187 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007167 RID: 29031
		// (get) Token: 0x06015C64 RID: 89188 RVA: 0x00322FFD File Offset: 0x003211FD
		internal override string[] AttributeTagNames
		{
			get
			{
				return BuildGraphics.attributeTagNames;
			}
		}

		// Token: 0x17007168 RID: 29032
		// (get) Token: 0x06015C65 RID: 89189 RVA: 0x00323004 File Offset: 0x00321204
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BuildGraphics.attributeNamespaceIds;
			}
		}

		// Token: 0x17007169 RID: 29033
		// (get) Token: 0x06015C66 RID: 89190 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015C67 RID: 89191 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "spid")]
		public StringValue ShapeId
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

		// Token: 0x1700716A RID: 29034
		// (get) Token: 0x06015C68 RID: 89192 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06015C69 RID: 89193 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "grpId")]
		public UInt32Value GroupId
		{
			get
			{
				return (UInt32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x1700716B RID: 29035
		// (get) Token: 0x06015C6A RID: 89194 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06015C6B RID: 89195 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "uiExpand")]
		public BooleanValue UiExpand
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06015C6C RID: 89196 RVA: 0x00293ECF File Offset: 0x002920CF
		public BuildGraphics()
		{
		}

		// Token: 0x06015C6D RID: 89197 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BuildGraphics(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015C6E RID: 89198 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BuildGraphics(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015C6F RID: 89199 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BuildGraphics(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015C70 RID: 89200 RVA: 0x0032300B File Offset: 0x0032120B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "bldAsOne" == name)
			{
				return new BuildAsOne();
			}
			if (24 == namespaceId && "bldSub" == name)
			{
				return new BuildSubElement();
			}
			return null;
		}

		// Token: 0x1700716C RID: 29036
		// (get) Token: 0x06015C71 RID: 89201 RVA: 0x0032303E File Offset: 0x0032123E
		internal override string[] ElementTagNames
		{
			get
			{
				return BuildGraphics.eleTagNames;
			}
		}

		// Token: 0x1700716D RID: 29037
		// (get) Token: 0x06015C72 RID: 89202 RVA: 0x00323045 File Offset: 0x00321245
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BuildGraphics.eleNamespaceIds;
			}
		}

		// Token: 0x1700716E RID: 29038
		// (get) Token: 0x06015C73 RID: 89203 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x1700716F RID: 29039
		// (get) Token: 0x06015C74 RID: 89204 RVA: 0x0032304C File Offset: 0x0032124C
		// (set) Token: 0x06015C75 RID: 89205 RVA: 0x00323055 File Offset: 0x00321255
		public BuildAsOne BuildAsOne
		{
			get
			{
				return base.GetElement<BuildAsOne>(0);
			}
			set
			{
				base.SetElement<BuildAsOne>(0, value);
			}
		}

		// Token: 0x17007170 RID: 29040
		// (get) Token: 0x06015C76 RID: 89206 RVA: 0x0032305F File Offset: 0x0032125F
		// (set) Token: 0x06015C77 RID: 89207 RVA: 0x00323068 File Offset: 0x00321268
		public BuildSubElement BuildSubElement
		{
			get
			{
				return base.GetElement<BuildSubElement>(1);
			}
			set
			{
				base.SetElement<BuildSubElement>(1, value);
			}
		}

		// Token: 0x06015C78 RID: 89208 RVA: 0x00323074 File Offset: 0x00321274
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "spid" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "grpId" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "uiExpand" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015C79 RID: 89209 RVA: 0x003230CB File Offset: 0x003212CB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BuildGraphics>(deep);
		}

		// Token: 0x06015C7A RID: 89210 RVA: 0x003230D4 File Offset: 0x003212D4
		// Note: this type is marked as 'beforefieldinit'.
		static BuildGraphics()
		{
			byte[] array = new byte[3];
			BuildGraphics.attributeNamespaceIds = array;
			BuildGraphics.eleTagNames = new string[] { "bldAsOne", "bldSub" };
			BuildGraphics.eleNamespaceIds = new byte[] { 24, 24 };
		}

		// Token: 0x040094C0 RID: 38080
		private const string tagName = "bldGraphic";

		// Token: 0x040094C1 RID: 38081
		private const byte tagNsId = 24;

		// Token: 0x040094C2 RID: 38082
		internal const int ElementTypeIdConst = 12237;

		// Token: 0x040094C3 RID: 38083
		private static string[] attributeTagNames = new string[] { "spid", "grpId", "uiExpand" };

		// Token: 0x040094C4 RID: 38084
		private static byte[] attributeNamespaceIds;

		// Token: 0x040094C5 RID: 38085
		private static readonly string[] eleTagNames;

		// Token: 0x040094C6 RID: 38086
		private static readonly byte[] eleNamespaceIds;
	}
}
