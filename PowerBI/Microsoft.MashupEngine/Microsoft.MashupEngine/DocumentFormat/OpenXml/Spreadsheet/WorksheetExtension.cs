using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C45 RID: 11333
	[ChildElementInfo(typeof(DataValidations), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ProtectedRanges), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SlicerList), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(IgnoredErrors), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ConditionalFormattings), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(SparklineGroups), FileFormatVersions.Office2010)]
	internal class WorksheetExtension : OpenXmlCompositeElement
	{
		// Token: 0x170081B5 RID: 33205
		// (get) Token: 0x06018062 RID: 98402 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170081B6 RID: 33206
		// (get) Token: 0x06018063 RID: 98403 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170081B7 RID: 33207
		// (get) Token: 0x06018064 RID: 98404 RVA: 0x0033DC07 File Offset: 0x0033BE07
		internal override int ElementTypeId
		{
			get
			{
				return 11314;
			}
		}

		// Token: 0x06018065 RID: 98405 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170081B8 RID: 33208
		// (get) Token: 0x06018066 RID: 98406 RVA: 0x0033DC0E File Offset: 0x0033BE0E
		internal override string[] AttributeTagNames
		{
			get
			{
				return WorksheetExtension.attributeTagNames;
			}
		}

		// Token: 0x170081B9 RID: 33209
		// (get) Token: 0x06018067 RID: 98407 RVA: 0x0033DC15 File Offset: 0x0033BE15
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return WorksheetExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170081BA RID: 33210
		// (get) Token: 0x06018068 RID: 98408 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018069 RID: 98409 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601806A RID: 98410 RVA: 0x00293ECF File Offset: 0x002920CF
		public WorksheetExtension()
		{
		}

		// Token: 0x0601806B RID: 98411 RVA: 0x00293ED7 File Offset: 0x002920D7
		public WorksheetExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601806C RID: 98412 RVA: 0x00293EE0 File Offset: 0x002920E0
		public WorksheetExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601806D RID: 98413 RVA: 0x00293EE9 File Offset: 0x002920E9
		public WorksheetExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601806E RID: 98414 RVA: 0x0033DC1C File Offset: 0x0033BE1C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "conditionalFormattings" == name)
			{
				return new ConditionalFormattings();
			}
			if (53 == namespaceId && "dataValidations" == name)
			{
				return new DataValidations();
			}
			if (53 == namespaceId && "sparklineGroups" == name)
			{
				return new SparklineGroups();
			}
			if (53 == namespaceId && "slicerList" == name)
			{
				return new SlicerList();
			}
			if (53 == namespaceId && "protectedRanges" == name)
			{
				return new ProtectedRanges();
			}
			if (53 == namespaceId && "ignoredErrors" == name)
			{
				return new IgnoredErrors();
			}
			return null;
		}

		// Token: 0x0601806F RID: 98415 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018070 RID: 98416 RVA: 0x0033DCBA File Offset: 0x0033BEBA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WorksheetExtension>(deep);
		}

		// Token: 0x06018071 RID: 98417 RVA: 0x0033DCC4 File Offset: 0x0033BEC4
		// Note: this type is marked as 'beforefieldinit'.
		static WorksheetExtension()
		{
			byte[] array = new byte[1];
			WorksheetExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009E8A RID: 40586
		private const string tagName = "ext";

		// Token: 0x04009E8B RID: 40587
		private const byte tagNsId = 22;

		// Token: 0x04009E8C RID: 40588
		internal const int ElementTypeIdConst = 11314;

		// Token: 0x04009E8D RID: 40589
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009E8E RID: 40590
		private static byte[] attributeNamespaceIds;
	}
}
