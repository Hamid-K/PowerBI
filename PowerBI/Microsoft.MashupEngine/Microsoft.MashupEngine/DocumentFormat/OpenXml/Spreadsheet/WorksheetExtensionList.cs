using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CA5 RID: 11429
	[ChildElementInfo(typeof(WorksheetExtension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class WorksheetExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x1700844B RID: 33867
		// (get) Token: 0x0601869B RID: 99995 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x1700844C RID: 33868
		// (get) Token: 0x0601869C RID: 99996 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700844D RID: 33869
		// (get) Token: 0x0601869D RID: 99997 RVA: 0x0034157B File Offset: 0x0033F77B
		internal override int ElementTypeId
		{
			get
			{
				return 11409;
			}
		}

		// Token: 0x0601869E RID: 99998 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601869F RID: 99999 RVA: 0x00293ECF File Offset: 0x002920CF
		public WorksheetExtensionList()
		{
		}

		// Token: 0x060186A0 RID: 100000 RVA: 0x00293ED7 File Offset: 0x002920D7
		public WorksheetExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060186A1 RID: 100001 RVA: 0x00293EE0 File Offset: 0x002920E0
		public WorksheetExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060186A2 RID: 100002 RVA: 0x00293EE9 File Offset: 0x002920E9
		public WorksheetExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060186A3 RID: 100003 RVA: 0x00341582 File Offset: 0x0033F782
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ext" == name)
			{
				return new WorksheetExtension();
			}
			return null;
		}

		// Token: 0x060186A4 RID: 100004 RVA: 0x0034159D File Offset: 0x0033F79D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WorksheetExtensionList>(deep);
		}

		// Token: 0x0400A026 RID: 40998
		private const string tagName = "extLst";

		// Token: 0x0400A027 RID: 40999
		private const byte tagNsId = 22;

		// Token: 0x0400A028 RID: 41000
		internal const int ElementTypeIdConst = 11409;
	}
}
