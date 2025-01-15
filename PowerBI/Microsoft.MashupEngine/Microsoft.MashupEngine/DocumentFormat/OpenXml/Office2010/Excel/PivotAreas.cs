using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Spreadsheet;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200242E RID: 9262
	[ChildElementInfo(typeof(PivotArea))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class PivotAreas : OpenXmlCompositeElement
	{
		// Token: 0x17004FCB RID: 20427
		// (get) Token: 0x06011073 RID: 69747 RVA: 0x002E9D77 File Offset: 0x002E7F77
		public override string LocalName
		{
			get
			{
				return "pivotAreas";
			}
		}

		// Token: 0x17004FCC RID: 20428
		// (get) Token: 0x06011074 RID: 69748 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004FCD RID: 20429
		// (get) Token: 0x06011075 RID: 69749 RVA: 0x002E9D7E File Offset: 0x002E7F7E
		internal override int ElementTypeId
		{
			get
			{
				return 12986;
			}
		}

		// Token: 0x06011076 RID: 69750 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004FCE RID: 20430
		// (get) Token: 0x06011077 RID: 69751 RVA: 0x002E9D85 File Offset: 0x002E7F85
		internal override string[] AttributeTagNames
		{
			get
			{
				return PivotAreas.attributeTagNames;
			}
		}

		// Token: 0x17004FCF RID: 20431
		// (get) Token: 0x06011078 RID: 69752 RVA: 0x002E9D8C File Offset: 0x002E7F8C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PivotAreas.attributeNamespaceIds;
			}
		}

		// Token: 0x17004FD0 RID: 20432
		// (get) Token: 0x06011079 RID: 69753 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x0601107A RID: 69754 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x0601107B RID: 69755 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotAreas()
		{
		}

		// Token: 0x0601107C RID: 69756 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotAreas(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601107D RID: 69757 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotAreas(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601107E RID: 69758 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotAreas(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601107F RID: 69759 RVA: 0x002E9D93 File Offset: 0x002E7F93
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "pivotArea" == name)
			{
				return new PivotArea();
			}
			return null;
		}

		// Token: 0x06011080 RID: 69760 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011081 RID: 69761 RVA: 0x002E9DAE File Offset: 0x002E7FAE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotAreas>(deep);
		}

		// Token: 0x06011082 RID: 69762 RVA: 0x002E9DB8 File Offset: 0x002E7FB8
		// Note: this type is marked as 'beforefieldinit'.
		static PivotAreas()
		{
			byte[] array = new byte[1];
			PivotAreas.attributeNamespaceIds = array;
		}

		// Token: 0x04007757 RID: 30551
		private const string tagName = "pivotAreas";

		// Token: 0x04007758 RID: 30552
		private const byte tagNsId = 53;

		// Token: 0x04007759 RID: 30553
		internal const int ElementTypeIdConst = 12986;

		// Token: 0x0400775A RID: 30554
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400775B RID: 30555
		private static byte[] attributeNamespaceIds;
	}
}
