using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C62 RID: 11362
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(WorkbookExtension))]
	internal class WorkbookExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x1700828D RID: 33421
		// (get) Token: 0x06018262 RID: 98914 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x1700828E RID: 33422
		// (get) Token: 0x06018263 RID: 98915 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700828F RID: 33423
		// (get) Token: 0x06018264 RID: 98916 RVA: 0x0033EF9F File Offset: 0x0033D19F
		internal override int ElementTypeId
		{
			get
			{
				return 11343;
			}
		}

		// Token: 0x06018265 RID: 98917 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018266 RID: 98918 RVA: 0x00293ECF File Offset: 0x002920CF
		public WorkbookExtensionList()
		{
		}

		// Token: 0x06018267 RID: 98919 RVA: 0x00293ED7 File Offset: 0x002920D7
		public WorkbookExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018268 RID: 98920 RVA: 0x00293EE0 File Offset: 0x002920E0
		public WorkbookExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018269 RID: 98921 RVA: 0x00293EE9 File Offset: 0x002920E9
		public WorkbookExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601826A RID: 98922 RVA: 0x0033EFA6 File Offset: 0x0033D1A6
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ext" == name)
			{
				return new WorkbookExtension();
			}
			return null;
		}

		// Token: 0x0601826B RID: 98923 RVA: 0x0033EFC1 File Offset: 0x0033D1C1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WorkbookExtensionList>(deep);
		}

		// Token: 0x04009F0D RID: 40717
		private const string tagName = "extLst";

		// Token: 0x04009F0E RID: 40718
		private const byte tagNsId = 22;

		// Token: 0x04009F0F RID: 40719
		internal const int ElementTypeIdConst = 11343;
	}
}
