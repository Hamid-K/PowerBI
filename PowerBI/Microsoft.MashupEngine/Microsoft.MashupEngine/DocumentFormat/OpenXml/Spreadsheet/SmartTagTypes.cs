using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C5E RID: 11358
	[ChildElementInfo(typeof(SmartTagType))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SmartTagTypes : OpenXmlCompositeElement
	{
		// Token: 0x1700826E RID: 33390
		// (get) Token: 0x0601821C RID: 98844 RVA: 0x0033ECBF File Offset: 0x0033CEBF
		public override string LocalName
		{
			get
			{
				return "smartTagTypes";
			}
		}

		// Token: 0x1700826F RID: 33391
		// (get) Token: 0x0601821D RID: 98845 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008270 RID: 33392
		// (get) Token: 0x0601821E RID: 98846 RVA: 0x0033ECC6 File Offset: 0x0033CEC6
		internal override int ElementTypeId
		{
			get
			{
				return 11339;
			}
		}

		// Token: 0x0601821F RID: 98847 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018220 RID: 98848 RVA: 0x00293ECF File Offset: 0x002920CF
		public SmartTagTypes()
		{
		}

		// Token: 0x06018221 RID: 98849 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SmartTagTypes(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018222 RID: 98850 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SmartTagTypes(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018223 RID: 98851 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SmartTagTypes(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018224 RID: 98852 RVA: 0x0033ECCD File Offset: 0x0033CECD
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "smartTagType" == name)
			{
				return new SmartTagType();
			}
			return null;
		}

		// Token: 0x06018225 RID: 98853 RVA: 0x0033ECE8 File Offset: 0x0033CEE8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SmartTagTypes>(deep);
		}

		// Token: 0x04009EFB RID: 40699
		private const string tagName = "smartTagTypes";

		// Token: 0x04009EFC RID: 40700
		private const byte tagNsId = 22;

		// Token: 0x04009EFD RID: 40701
		internal const int ElementTypeIdConst = 11339;
	}
}
