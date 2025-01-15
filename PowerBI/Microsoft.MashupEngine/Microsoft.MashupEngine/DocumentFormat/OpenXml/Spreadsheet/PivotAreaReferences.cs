using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B8C RID: 11148
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(PivotAreaReference))]
	internal class PivotAreaReferences : OpenXmlCompositeElement
	{
		// Token: 0x17007AC3 RID: 31427
		// (get) Token: 0x06017165 RID: 94565 RVA: 0x00332A1E File Offset: 0x00330C1E
		public override string LocalName
		{
			get
			{
				return "references";
			}
		}

		// Token: 0x17007AC4 RID: 31428
		// (get) Token: 0x06017166 RID: 94566 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17007AC5 RID: 31429
		// (get) Token: 0x06017167 RID: 94567 RVA: 0x00332A25 File Offset: 0x00330C25
		internal override int ElementTypeId
		{
			get
			{
				return 11127;
			}
		}

		// Token: 0x06017168 RID: 94568 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17007AC6 RID: 31430
		// (get) Token: 0x06017169 RID: 94569 RVA: 0x00332A2C File Offset: 0x00330C2C
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotAreaReferences.attributeTagNames;
			}
		}

		// Token: 0x17007AC7 RID: 31431
		// (get) Token: 0x0601716A RID: 94570 RVA: 0x00332A33 File Offset: 0x00330C33
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotAreaReferences.attributeNamespaceIds;
			}
		}

		// Token: 0x17007AC8 RID: 31432
		// (get) Token: 0x0601716B RID: 94571 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601716C RID: 94572 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601716D RID: 94573 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotAreaReferences()
		{
		}

		// Token: 0x0601716E RID: 94574 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotAreaReferences(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601716F RID: 94575 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotAreaReferences(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017170 RID: 94576 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotAreaReferences(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017171 RID: 94577 RVA: 0x00332A3A File Offset: 0x00330C3A
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "reference" == name)
			{
				return new PivotAreaReference();
			}
			return null;
		}

		// Token: 0x06017172 RID: 94578 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017173 RID: 94579 RVA: 0x00332A55 File Offset: 0x00330C55
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotAreaReferences>(deep);
		}

		// Token: 0x06017174 RID: 94580 RVA: 0x00332A60 File Offset: 0x00330C60
		// Note: this type is marked as 'beforefieldinit'.
		static PivotAreaReferences()
		{
			byte[] array = new byte[1];
			PivotAreaReferences.attributeNamespaceIds = array;
		}

		// Token: 0x04009B03 RID: 39683
		private const string tagName = "references";

		// Token: 0x04009B04 RID: 39684
		private const byte tagNsId = 22;

		// Token: 0x04009B05 RID: 39685
		internal const int ElementTypeIdConst = 11127;

		// Token: 0x04009B06 RID: 39686
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009B07 RID: 39687
		private static byte[] attributeNamespaceIds;
	}
}
