using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CBC RID: 11452
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ColumnHierarchyUsage))]
	internal class ColumnHierarchiesUsage : OpenXmlCompositeElement
	{
		// Token: 0x170084D5 RID: 34005
		// (get) Token: 0x06018801 RID: 100353 RVA: 0x00341FAB File Offset: 0x003401AB
		public override string LocalName
		{
			get
			{
				return "colHierarchiesUsage";
			}
		}

		// Token: 0x170084D6 RID: 34006
		// (get) Token: 0x06018802 RID: 100354 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170084D7 RID: 34007
		// (get) Token: 0x06018803 RID: 100355 RVA: 0x00341FB2 File Offset: 0x003401B2
		internal override int ElementTypeId
		{
			get
			{
				return 11432;
			}
		}

		// Token: 0x06018804 RID: 100356 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170084D8 RID: 34008
		// (get) Token: 0x06018805 RID: 100357 RVA: 0x00341FB9 File Offset: 0x003401B9
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColumnHierarchiesUsage.attributeTagNames;
			}
		}

		// Token: 0x170084D9 RID: 34009
		// (get) Token: 0x06018806 RID: 100358 RVA: 0x00341FC0 File Offset: 0x003401C0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColumnHierarchiesUsage.attributeNamespaceIds;
			}
		}

		// Token: 0x170084DA RID: 34010
		// (get) Token: 0x06018807 RID: 100359 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018808 RID: 100360 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06018809 RID: 100361 RVA: 0x00293ECF File Offset: 0x002920CF
		public ColumnHierarchiesUsage()
		{
		}

		// Token: 0x0601880A RID: 100362 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ColumnHierarchiesUsage(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601880B RID: 100363 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ColumnHierarchiesUsage(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601880C RID: 100364 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ColumnHierarchiesUsage(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601880D RID: 100365 RVA: 0x00341FC7 File Offset: 0x003401C7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "colHierarchyUsage" == name)
			{
				return new ColumnHierarchyUsage();
			}
			return null;
		}

		// Token: 0x0601880E RID: 100366 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601880F RID: 100367 RVA: 0x00341FE2 File Offset: 0x003401E2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColumnHierarchiesUsage>(deep);
		}

		// Token: 0x06018810 RID: 100368 RVA: 0x00341FEC File Offset: 0x003401EC
		// Note: this type is marked as 'beforefieldinit'.
		static ColumnHierarchiesUsage()
		{
			byte[] array = new byte[1];
			ColumnHierarchiesUsage.attributeNamespaceIds = array;
		}

		// Token: 0x0400A091 RID: 41105
		private const string tagName = "colHierarchiesUsage";

		// Token: 0x0400A092 RID: 41106
		private const byte tagNsId = 22;

		// Token: 0x0400A093 RID: 41107
		internal const int ElementTypeIdConst = 11432;

		// Token: 0x0400A094 RID: 41108
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A095 RID: 41109
		private static byte[] attributeNamespaceIds;
	}
}
