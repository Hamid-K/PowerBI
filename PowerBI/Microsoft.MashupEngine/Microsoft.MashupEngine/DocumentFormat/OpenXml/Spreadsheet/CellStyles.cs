using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C77 RID: 11383
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CellStyle))]
	internal class CellStyles : OpenXmlCompositeElement
	{
		// Token: 0x170082F3 RID: 33523
		// (get) Token: 0x0601836F RID: 99183 RVA: 0x0033F65F File Offset: 0x0033D85F
		public override string LocalName
		{
			get
			{
				return "cellStyles";
			}
		}

		// Token: 0x170082F4 RID: 33524
		// (get) Token: 0x06018370 RID: 99184 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082F5 RID: 33525
		// (get) Token: 0x06018371 RID: 99185 RVA: 0x0033F666 File Offset: 0x0033D866
		internal override int ElementTypeId
		{
			get
			{
				return 11363;
			}
		}

		// Token: 0x06018372 RID: 99186 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170082F6 RID: 33526
		// (get) Token: 0x06018373 RID: 99187 RVA: 0x0033F66D File Offset: 0x0033D86D
		internal override string[] AttributeTagNames
		{
			get
			{
				return CellStyles.attributeTagNames;
			}
		}

		// Token: 0x170082F7 RID: 33527
		// (get) Token: 0x06018374 RID: 99188 RVA: 0x0033F674 File Offset: 0x0033D874
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CellStyles.attributeNamespaceIds;
			}
		}

		// Token: 0x170082F8 RID: 33528
		// (get) Token: 0x06018375 RID: 99189 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018376 RID: 99190 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018377 RID: 99191 RVA: 0x00293ECF File Offset: 0x002920CF
		public CellStyles()
		{
		}

		// Token: 0x06018378 RID: 99192 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CellStyles(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018379 RID: 99193 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CellStyles(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601837A RID: 99194 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CellStyles(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601837B RID: 99195 RVA: 0x0033F67B File Offset: 0x0033D87B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "cellStyle" == name)
			{
				return new CellStyle();
			}
			return null;
		}

		// Token: 0x0601837C RID: 99196 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601837D RID: 99197 RVA: 0x0033F696 File Offset: 0x0033D896
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CellStyles>(deep);
		}

		// Token: 0x0601837E RID: 99198 RVA: 0x0033F6A0 File Offset: 0x0033D8A0
		// Note: this type is marked as 'beforefieldinit'.
		static CellStyles()
		{
			byte[] array = new byte[1];
			CellStyles.attributeNamespaceIds = array;
		}

		// Token: 0x04009F5F RID: 40799
		private const string tagName = "cellStyles";

		// Token: 0x04009F60 RID: 40800
		private const byte tagNsId = 22;

		// Token: 0x04009F61 RID: 40801
		internal const int ElementTypeIdConst = 11363;

		// Token: 0x04009F62 RID: 40802
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009F63 RID: 40803
		private static byte[] attributeNamespaceIds;
	}
}
