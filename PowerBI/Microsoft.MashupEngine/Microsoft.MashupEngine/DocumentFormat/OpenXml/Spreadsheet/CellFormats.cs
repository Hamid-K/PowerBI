using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C76 RID: 11382
	[ChildElementInfo(typeof(CellFormat))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CellFormats : OpenXmlCompositeElement
	{
		// Token: 0x170082ED RID: 33517
		// (get) Token: 0x0601835F RID: 99167 RVA: 0x0033F60B File Offset: 0x0033D80B
		public override string LocalName
		{
			get
			{
				return "cellXfs";
			}
		}

		// Token: 0x170082EE RID: 33518
		// (get) Token: 0x06018360 RID: 99168 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082EF RID: 33519
		// (get) Token: 0x06018361 RID: 99169 RVA: 0x0033F612 File Offset: 0x0033D812
		internal override int ElementTypeId
		{
			get
			{
				return 11362;
			}
		}

		// Token: 0x06018362 RID: 99170 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170082F0 RID: 33520
		// (get) Token: 0x06018363 RID: 99171 RVA: 0x0033F619 File Offset: 0x0033D819
		internal override string[] AttributeTagNames
		{
			get
			{
				return CellFormats.attributeTagNames;
			}
		}

		// Token: 0x170082F1 RID: 33521
		// (get) Token: 0x06018364 RID: 99172 RVA: 0x0033F620 File Offset: 0x0033D820
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CellFormats.attributeNamespaceIds;
			}
		}

		// Token: 0x170082F2 RID: 33522
		// (get) Token: 0x06018365 RID: 99173 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018366 RID: 99174 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018367 RID: 99175 RVA: 0x00293ECF File Offset: 0x002920CF
		public CellFormats()
		{
		}

		// Token: 0x06018368 RID: 99176 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CellFormats(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018369 RID: 99177 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CellFormats(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601836A RID: 99178 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CellFormats(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601836B RID: 99179 RVA: 0x0033F5B7 File Offset: 0x0033D7B7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "xf" == name)
			{
				return new CellFormat();
			}
			return null;
		}

		// Token: 0x0601836C RID: 99180 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601836D RID: 99181 RVA: 0x0033F627 File Offset: 0x0033D827
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CellFormats>(deep);
		}

		// Token: 0x0601836E RID: 99182 RVA: 0x0033F630 File Offset: 0x0033D830
		// Note: this type is marked as 'beforefieldinit'.
		static CellFormats()
		{
			byte[] array = new byte[1];
			CellFormats.attributeNamespaceIds = array;
		}

		// Token: 0x04009F5A RID: 40794
		private const string tagName = "cellXfs";

		// Token: 0x04009F5B RID: 40795
		private const byte tagNsId = 22;

		// Token: 0x04009F5C RID: 40796
		internal const int ElementTypeIdConst = 11362;

		// Token: 0x04009F5D RID: 40797
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009F5E RID: 40798
		private static byte[] attributeNamespaceIds;
	}
}
