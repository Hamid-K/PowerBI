using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C75 RID: 11381
	[ChildElementInfo(typeof(CellFormat))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CellStyleFormats : OpenXmlCompositeElement
	{
		// Token: 0x170082E7 RID: 33511
		// (get) Token: 0x0601834F RID: 99151 RVA: 0x0033F59B File Offset: 0x0033D79B
		public override string LocalName
		{
			get
			{
				return "cellStyleXfs";
			}
		}

		// Token: 0x170082E8 RID: 33512
		// (get) Token: 0x06018350 RID: 99152 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170082E9 RID: 33513
		// (get) Token: 0x06018351 RID: 99153 RVA: 0x0033F5A2 File Offset: 0x0033D7A2
		internal override int ElementTypeId
		{
			get
			{
				return 11361;
			}
		}

		// Token: 0x06018352 RID: 99154 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170082EA RID: 33514
		// (get) Token: 0x06018353 RID: 99155 RVA: 0x0033F5A9 File Offset: 0x0033D7A9
		internal override string[] AttributeTagNames
		{
			get
			{
				return CellStyleFormats.attributeTagNames;
			}
		}

		// Token: 0x170082EB RID: 33515
		// (get) Token: 0x06018354 RID: 99156 RVA: 0x0033F5B0 File Offset: 0x0033D7B0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CellStyleFormats.attributeNamespaceIds;
			}
		}

		// Token: 0x170082EC RID: 33516
		// (get) Token: 0x06018355 RID: 99157 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018356 RID: 99158 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06018357 RID: 99159 RVA: 0x00293ECF File Offset: 0x002920CF
		public CellStyleFormats()
		{
		}

		// Token: 0x06018358 RID: 99160 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CellStyleFormats(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018359 RID: 99161 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CellStyleFormats(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601835A RID: 99162 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CellStyleFormats(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601835B RID: 99163 RVA: 0x0033F5B7 File Offset: 0x0033D7B7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "xf" == name)
			{
				return new CellFormat();
			}
			return null;
		}

		// Token: 0x0601835C RID: 99164 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601835D RID: 99165 RVA: 0x0033F5D2 File Offset: 0x0033D7D2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CellStyleFormats>(deep);
		}

		// Token: 0x0601835E RID: 99166 RVA: 0x0033F5DC File Offset: 0x0033D7DC
		// Note: this type is marked as 'beforefieldinit'.
		static CellStyleFormats()
		{
			byte[] array = new byte[1];
			CellStyleFormats.attributeNamespaceIds = array;
		}

		// Token: 0x04009F55 RID: 40789
		private const string tagName = "cellStyleXfs";

		// Token: 0x04009F56 RID: 40790
		private const byte tagNsId = 22;

		// Token: 0x04009F57 RID: 40791
		internal const int ElementTypeIdConst = 11361;

		// Token: 0x04009F58 RID: 40792
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009F59 RID: 40793
		private static byte[] attributeNamespaceIds;
	}
}
