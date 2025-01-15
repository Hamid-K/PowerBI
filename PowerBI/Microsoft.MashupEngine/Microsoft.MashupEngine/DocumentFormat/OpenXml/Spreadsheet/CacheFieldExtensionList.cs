using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CC7 RID: 11463
	[ChildElementInfo(typeof(CacheFieldExtension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class CacheFieldExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x17008521 RID: 34081
		// (get) Token: 0x060188BE RID: 100542 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x17008522 RID: 34082
		// (get) Token: 0x060188BF RID: 100543 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008523 RID: 34083
		// (get) Token: 0x060188C0 RID: 100544 RVA: 0x00342757 File Offset: 0x00340957
		internal override int ElementTypeId
		{
			get
			{
				return 11444;
			}
		}

		// Token: 0x060188C1 RID: 100545 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060188C2 RID: 100546 RVA: 0x00293ECF File Offset: 0x002920CF
		public CacheFieldExtensionList()
		{
		}

		// Token: 0x060188C3 RID: 100547 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CacheFieldExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060188C4 RID: 100548 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CacheFieldExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060188C5 RID: 100549 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CacheFieldExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060188C6 RID: 100550 RVA: 0x0034275E File Offset: 0x0034095E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ext" == name)
			{
				return new CacheFieldExtension();
			}
			return null;
		}

		// Token: 0x060188C7 RID: 100551 RVA: 0x00342779 File Offset: 0x00340979
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CacheFieldExtensionList>(deep);
		}

		// Token: 0x0400A0C2 RID: 41154
		private const string tagName = "extLst";

		// Token: 0x0400A0C3 RID: 41155
		private const byte tagNsId = 22;

		// Token: 0x0400A0C4 RID: 41156
		internal const int ElementTypeIdConst = 11444;
	}
}
