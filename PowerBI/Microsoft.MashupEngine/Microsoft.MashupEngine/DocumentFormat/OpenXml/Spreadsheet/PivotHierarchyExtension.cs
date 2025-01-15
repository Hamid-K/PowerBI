using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C46 RID: 11334
	[ChildElementInfo(typeof(PivotHierarchy), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotHierarchyExtension : OpenXmlCompositeElement
	{
		// Token: 0x170081BB RID: 33211
		// (get) Token: 0x06018072 RID: 98418 RVA: 0x002F335B File Offset: 0x002F155B
		public override string LocalName
		{
			get
			{
				return "ext";
			}
		}

		// Token: 0x170081BC RID: 33212
		// (get) Token: 0x06018073 RID: 98419 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170081BD RID: 33213
		// (get) Token: 0x06018074 RID: 98420 RVA: 0x0033DCF3 File Offset: 0x0033BEF3
		internal override int ElementTypeId
		{
			get
			{
				return 11315;
			}
		}

		// Token: 0x06018075 RID: 98421 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170081BE RID: 33214
		// (get) Token: 0x06018076 RID: 98422 RVA: 0x0033DCFA File Offset: 0x0033BEFA
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotHierarchyExtension.attributeTagNames;
			}
		}

		// Token: 0x170081BF RID: 33215
		// (get) Token: 0x06018077 RID: 98423 RVA: 0x0033DD01 File Offset: 0x0033BF01
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotHierarchyExtension.attributeNamespaceIds;
			}
		}

		// Token: 0x170081C0 RID: 33216
		// (get) Token: 0x06018078 RID: 98424 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06018079 RID: 98425 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601807A RID: 98426 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotHierarchyExtension()
		{
		}

		// Token: 0x0601807B RID: 98427 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotHierarchyExtension(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601807C RID: 98428 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotHierarchyExtension(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601807D RID: 98429 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotHierarchyExtension(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601807E RID: 98430 RVA: 0x0033DD08 File Offset: 0x0033BF08
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (53 == namespaceId && "pivotHierarchy" == name)
			{
				return new PivotHierarchy();
			}
			return null;
		}

		// Token: 0x0601807F RID: 98431 RVA: 0x002F3377 File Offset: 0x002F1577
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "uri" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018080 RID: 98432 RVA: 0x0033DD23 File Offset: 0x0033BF23
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotHierarchyExtension>(deep);
		}

		// Token: 0x06018081 RID: 98433 RVA: 0x0033DD2C File Offset: 0x0033BF2C
		// Note: this type is marked as 'beforefieldinit'.
		static PivotHierarchyExtension()
		{
			byte[] array = new byte[1];
			PivotHierarchyExtension.attributeNamespaceIds = array;
		}

		// Token: 0x04009E8F RID: 40591
		private const string tagName = "ext";

		// Token: 0x04009E90 RID: 40592
		private const byte tagNsId = 22;

		// Token: 0x04009E91 RID: 40593
		internal const int ElementTypeIdConst = 11315;

		// Token: 0x04009E92 RID: 40594
		private static string[] attributeTagNames = new string[] { "uri" };

		// Token: 0x04009E93 RID: 40595
		private static byte[] attributeNamespaceIds;
	}
}
