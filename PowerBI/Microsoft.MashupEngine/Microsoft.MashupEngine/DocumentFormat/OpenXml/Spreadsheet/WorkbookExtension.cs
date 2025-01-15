using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C3D RID: 11325
	[ChildElementInfo(typeof(DefinedNames), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PivotCaches), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SlicerCaches), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(WorkbookProperties), FileFormatVersions.Office2010)]
	internal class WorkbookExtension : OpenXmlCompositeElement
	{
		// Token: 0x17008183 RID: 33155
		// (get) Token: 0x06017FE4 RID: 98276 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x17008184 RID: 33156
		// (get) Token: 0x06017FE5 RID: 98277 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008185 RID: 33157
		// (get) Token: 0x06017FE6 RID: 98278 RVA: 0x0033D79B File Offset: 0x0033B99B
		internal override int ElementTypeId
		{
			get
			{
				return 11307;
			}
		}

		// Token: 0x06017FE7 RID: 98279 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008186 RID: 33158
		// (get) Token: 0x06017FE8 RID: 98280 RVA: 0x0033D7A2 File Offset: 0x0033B9A2
		internal override string[] AttributeTagNames
		{
			get
			{
				return WorkbookExtension.attributeTagNames;
			}
		}

		// Token: 0x17008187 RID: 33159
		// (get) Token: 0x06017FE9 RID: 98281 RVA: 0x0033D7A9 File Offset: 0x0033B9A9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WorkbookExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x17008188 RID: 33160
		// (get) Token: 0x06017FEA RID: 98282 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017FEB RID: 98283 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06017FEC RID: 98284 RVA: 0x00293ECF File Offset: 0x002920CF
		public WorkbookExtension()
		{
		}

		// Token: 0x06017FED RID: 98285 RVA: 0x00293ED7 File Offset: 0x002920D7
		public WorkbookExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017FEE RID: 98286 RVA: 0x00293EE0 File Offset: 0x002920E0
		public WorkbookExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017FEF RID: 98287 RVA: 0x00293EE9 File Offset: 0x002920E9
		public WorkbookExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017FF0 RID: 98288 RVA: 0x0033D7B0 File Offset: 0x0033B9B0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "definedNames" == name)
			{
				return new DefinedNames();
			}
			if (53 == namespaceId && "pivotCaches" == name)
			{
				return new PivotCaches();
			}
			if (53 == namespaceId && "slicerCaches" == name)
			{
				return new SlicerCaches();
			}
			if (53 == namespaceId && "workbookPr" == name)
			{
				return new WorkbookProperties();
			}
			return null;
		}

		// Token: 0x06017FF1 RID: 98289 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017FF2 RID: 98290 RVA: 0x0033D81E File Offset: 0x0033BA1E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WorkbookExtension>(deep);
		}

		// Token: 0x06017FF3 RID: 98291 RVA: 0x0033D828 File Offset: 0x0033BA28
		// Note: this type is marked as 'beforefieldinit'.
		static WorkbookExtension()
		{
			byte[] array = new byte[1];
			WorkbookExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009E67 RID: 40551
		private const string tagName = "ext";

		// Token: 0x04009E68 RID: 40552
		private const byte tagNsId = 22;

		// Token: 0x04009E69 RID: 40553
		internal const int ElementTypeIdConst = 11307;

		// Token: 0x04009E6A RID: 40554
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009E6B RID: 40555
		private static byte[] attributeNamespaceIds;
	}
}
