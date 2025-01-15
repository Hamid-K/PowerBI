using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A40 RID: 10816
	[GeneratedCode("DomGen", "2.0")]
	internal class BuildDiagram : OpenXmlLeafElement
	{
		// Token: 0x17007151 RID: 29009
		// (get) Token: 0x06015C3A RID: 89146 RVA: 0x0030A1CF File Offset: 0x003083CF
		public override string LocalName
		{
			get
			{
				return "bldDgm";
			}
		}

		// Token: 0x17007152 RID: 29010
		// (get) Token: 0x06015C3B RID: 89147 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007153 RID: 29011
		// (get) Token: 0x06015C3C RID: 89148 RVA: 0x00322E04 File Offset: 0x00321004
		internal override int ElementTypeId
		{
			get
			{
				return 12235;
			}
		}

		// Token: 0x06015C3D RID: 89149 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007154 RID: 29012
		// (get) Token: 0x06015C3E RID: 89150 RVA: 0x00322E0B File Offset: 0x0032100B
		internal override string[] AttributeTagNames
		{
			get
			{
				return BuildDiagram.attributeTagNames;
			}
		}

		// Token: 0x17007155 RID: 29013
		// (get) Token: 0x06015C3F RID: 89151 RVA: 0x00322E12 File Offset: 0x00321012
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BuildDiagram.attributeNamespaceIds;
			}
		}

		// Token: 0x17007156 RID: 29014
		// (get) Token: 0x06015C40 RID: 89152 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015C41 RID: 89153 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007157 RID: 29015
		// (get) Token: 0x06015C42 RID: 89154 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06015C43 RID: 89155 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17007158 RID: 29016
		// (get) Token: 0x06015C44 RID: 89156 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06015C45 RID: 89157 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17007159 RID: 29017
		// (get) Token: 0x06015C46 RID: 89158 RVA: 0x00322E19 File Offset: 0x00321019
		// (set) Token: 0x06015C47 RID: 89159 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "bld")]
		public EnumValue<DiagramBuildValues> Build
		{
			get
			{
				return (EnumValue<DiagramBuildValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06015C49 RID: 89161 RVA: 0x00322E28 File Offset: 0x00321028
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
			if (namespaceId == 0 && "bld" == name)
			{
				return new EnumValue<DiagramBuildValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015C4A RID: 89162 RVA: 0x00322E95 File Offset: 0x00321095
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BuildDiagram>(deep);
		}

		// Token: 0x06015C4B RID: 89163 RVA: 0x00322EA0 File Offset: 0x003210A0
		// Note: this type is marked as 'beforefieldinit'.
		static BuildDiagram()
		{
			byte[] array = new byte[4];
			BuildDiagram.attributeNamespaceIds = array;
		}

		// Token: 0x040094B6 RID: 38070
		private const string tagName = "bldDgm";

		// Token: 0x040094B7 RID: 38071
		private const byte tagNsId = 24;

		// Token: 0x040094B8 RID: 38072
		internal const int ElementTypeIdConst = 12235;

		// Token: 0x040094B9 RID: 38073
		private static string[] attributeTagNames = new string[] { "spid", "grpId", "uiExpand", "bld" };

		// Token: 0x040094BA RID: 38074
		private static byte[] attributeNamespaceIds;
	}
}
