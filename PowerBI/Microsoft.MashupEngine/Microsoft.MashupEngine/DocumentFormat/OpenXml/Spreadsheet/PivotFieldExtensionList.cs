using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CAC RID: 11436
	[ChildElementInfo(typeof(PivotFieldExtension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotFieldExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x1700846E RID: 33902
		// (get) Token: 0x060186FB RID: 100091 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x1700846F RID: 33903
		// (get) Token: 0x060186FC RID: 100092 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008470 RID: 33904
		// (get) Token: 0x060186FD RID: 100093 RVA: 0x003417E0 File Offset: 0x0033F9E0
		internal override int ElementTypeId
		{
			get
			{
				return 11416;
			}
		}

		// Token: 0x060186FE RID: 100094 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060186FF RID: 100095 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotFieldExtensionList()
		{
		}

		// Token: 0x06018700 RID: 100096 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotFieldExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018701 RID: 100097 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotFieldExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018702 RID: 100098 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotFieldExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018703 RID: 100099 RVA: 0x003417E7 File Offset: 0x0033F9E7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ext" == name)
			{
				return new PivotFieldExtension();
			}
			return null;
		}

		// Token: 0x06018704 RID: 100100 RVA: 0x00341802 File Offset: 0x0033FA02
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotFieldExtensionList>(deep);
		}

		// Token: 0x0400A043 RID: 41027
		private const string tagName = "extLst";

		// Token: 0x0400A044 RID: 41028
		private const byte tagNsId = 22;

		// Token: 0x0400A045 RID: 41029
		internal const int ElementTypeIdConst = 11416;
	}
}
