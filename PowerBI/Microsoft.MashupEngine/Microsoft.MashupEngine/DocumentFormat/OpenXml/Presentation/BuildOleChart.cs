using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A41 RID: 10817
	[GeneratedCode("DomGen", "2.0")]
	internal class BuildOleChart : OpenXmlLeafElement
	{
		// Token: 0x1700715A RID: 29018
		// (get) Token: 0x06015C4C RID: 89164 RVA: 0x00322EE7 File Offset: 0x003210E7
		public override string LocalName
		{
			get
			{
				return "bldOleChart";
			}
		}

		// Token: 0x1700715B RID: 29019
		// (get) Token: 0x06015C4D RID: 89165 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700715C RID: 29020
		// (get) Token: 0x06015C4E RID: 89166 RVA: 0x00322EEE File Offset: 0x003210EE
		internal override int ElementTypeId
		{
			get
			{
				return 12236;
			}
		}

		// Token: 0x06015C4F RID: 89167 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700715D RID: 29021
		// (get) Token: 0x06015C50 RID: 89168 RVA: 0x00322EF5 File Offset: 0x003210F5
		internal override string[] AttributeTagNames
		{
			get
			{
				return BuildOleChart.attributeTagNames;
			}
		}

		// Token: 0x1700715E RID: 29022
		// (get) Token: 0x06015C51 RID: 89169 RVA: 0x00322EFC File Offset: 0x003210FC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BuildOleChart.attributeNamespaceIds;
			}
		}

		// Token: 0x1700715F RID: 29023
		// (get) Token: 0x06015C52 RID: 89170 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06015C53 RID: 89171 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x17007160 RID: 29024
		// (get) Token: 0x06015C54 RID: 89172 RVA: 0x002E33EC File Offset: 0x002E15EC
		// (set) Token: 0x06015C55 RID: 89173 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x17007161 RID: 29025
		// (get) Token: 0x06015C56 RID: 89174 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06015C57 RID: 89175 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x17007162 RID: 29026
		// (get) Token: 0x06015C58 RID: 89176 RVA: 0x00322F03 File Offset: 0x00321103
		// (set) Token: 0x06015C59 RID: 89177 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "bld")]
		public EnumValue<OleChartBuildValues> Build
		{
			get
			{
				return (EnumValue<OleChartBuildValues>)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17007163 RID: 29027
		// (get) Token: 0x06015C5A RID: 89178 RVA: 0x002CBE2D File Offset: 0x002CA02D
		// (set) Token: 0x06015C5B RID: 89179 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "animBg")]
		public BooleanValue AnimateBackground
		{
			get
			{
				return (BooleanValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x06015C5D RID: 89181 RVA: 0x00322F14 File Offset: 0x00321114
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
				return new EnumValue<OleChartBuildValues>();
			}
			if (namespaceId == 0 && "animBg" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06015C5E RID: 89182 RVA: 0x00322F97 File Offset: 0x00321197
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BuildOleChart>(deep);
		}

		// Token: 0x06015C5F RID: 89183 RVA: 0x00322FA0 File Offset: 0x003211A0
		// Note: this type is marked as 'beforefieldinit'.
		static BuildOleChart()
		{
			byte[] array = new byte[5];
			BuildOleChart.attributeNamespaceIds = array;
		}

		// Token: 0x040094BB RID: 38075
		private const string tagName = "bldOleChart";

		// Token: 0x040094BC RID: 38076
		private const byte tagNsId = 24;

		// Token: 0x040094BD RID: 38077
		internal const int ElementTypeIdConst = 12236;

		// Token: 0x040094BE RID: 38078
		private static string[] attributeTagNames = new string[] { "spid", "grpId", "uiExpand", "bld", "animBg" };

		// Token: 0x040094BF RID: 38079
		private static byte[] attributeNamespaceIds;
	}
}
