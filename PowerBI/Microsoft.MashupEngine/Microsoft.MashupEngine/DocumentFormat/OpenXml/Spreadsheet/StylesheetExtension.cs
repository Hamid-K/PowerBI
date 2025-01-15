using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C3F RID: 11327
	[ChildElementInfo(typeof(SlicerStyles), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(DifferentialFormats), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class StylesheetExtension : OpenXmlCompositeElement
	{
		// Token: 0x1700818F RID: 33167
		// (get) Token: 0x06018004 RID: 98308 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x17008190 RID: 33168
		// (get) Token: 0x06018005 RID: 98309 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008191 RID: 33169
		// (get) Token: 0x06018006 RID: 98310 RVA: 0x0033D8BF File Offset: 0x0033BABF
		internal override int ElementTypeId
		{
			get
			{
				return 11309;
			}
		}

		// Token: 0x06018007 RID: 98311 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008192 RID: 33170
		// (get) Token: 0x06018008 RID: 98312 RVA: 0x0033D8C6 File Offset: 0x0033BAC6
		internal override string[] AttributeTagNames
		{
			get
			{
				return StylesheetExtension.attributeTagNames;
			}
		}

		// Token: 0x17008193 RID: 33171
		// (get) Token: 0x06018009 RID: 98313 RVA: 0x0033D8CD File Offset: 0x0033BACD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StylesheetExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x17008194 RID: 33172
		// (get) Token: 0x0601800A RID: 98314 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0601800B RID: 98315 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "uri")]
		public StringValue Uri
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

		// Token: 0x0601800C RID: 98316 RVA: 0x00293ECF File Offset: 0x002920CF
		public StylesheetExtension()
		{
		}

		// Token: 0x0601800D RID: 98317 RVA: 0x00293ED7 File Offset: 0x002920D7
		public StylesheetExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601800E RID: 98318 RVA: 0x00293EE0 File Offset: 0x002920E0
		public StylesheetExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601800F RID: 98319 RVA: 0x00293EE9 File Offset: 0x002920E9
		public StylesheetExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018010 RID: 98320 RVA: 0x0033D8D4 File Offset: 0x0033BAD4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "dxfs" == name)
			{
				return new DifferentialFormats();
			}
			if (53 == namespaceId && "slicerStyles" == name)
			{
				return new SlicerStyles();
			}
			return null;
		}

		// Token: 0x06018011 RID: 98321 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018012 RID: 98322 RVA: 0x0033D907 File Offset: 0x0033BB07
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StylesheetExtension>(deep);
		}

		// Token: 0x06018013 RID: 98323 RVA: 0x0033D910 File Offset: 0x0033BB10
		// Note: this type is marked as 'beforefieldinit'.
		static StylesheetExtension()
		{
			byte[] array = new byte[1];
			StylesheetExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009E71 RID: 40561
		private const string tagName = "ext";

		// Token: 0x04009E72 RID: 40562
		private const byte tagNsId = 22;

		// Token: 0x04009E73 RID: 40563
		internal const int ElementTypeIdConst = 11309;

		// Token: 0x04009E74 RID: 40564
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009E75 RID: 40565
		private static byte[] attributeNamespaceIds;
	}
}
