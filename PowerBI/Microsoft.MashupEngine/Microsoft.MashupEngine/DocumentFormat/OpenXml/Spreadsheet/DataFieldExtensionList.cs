using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CA9 RID: 11433
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DataFieldExtension))]
	internal class DataFieldExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x1700845E RID: 33886
		// (get) Token: 0x060186D1 RID: 100049 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x1700845F RID: 33887
		// (get) Token: 0x060186D2 RID: 100050 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008460 RID: 33888
		// (get) Token: 0x060186D3 RID: 100051 RVA: 0x003416F2 File Offset: 0x0033F8F2
		internal override int ElementTypeId
		{
			get
			{
				return 11413;
			}
		}

		// Token: 0x060186D4 RID: 100052 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060186D5 RID: 100053 RVA: 0x00293ECF File Offset: 0x002920CF
		public DataFieldExtensionList()
		{
		}

		// Token: 0x060186D6 RID: 100054 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DataFieldExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060186D7 RID: 100055 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DataFieldExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060186D8 RID: 100056 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DataFieldExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060186D9 RID: 100057 RVA: 0x003416F9 File Offset: 0x0033F8F9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ext" == name)
			{
				return new DataFieldExtension();
			}
			return null;
		}

		// Token: 0x060186DA RID: 100058 RVA: 0x00341714 File Offset: 0x0033F914
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DataFieldExtensionList>(deep);
		}

		// Token: 0x0400A036 RID: 41014
		private const string tagName = "extLst";

		// Token: 0x0400A037 RID: 41015
		private const byte tagNsId = 22;

		// Token: 0x0400A038 RID: 41016
		internal const int ElementTypeIdConst = 11413;
	}
}
