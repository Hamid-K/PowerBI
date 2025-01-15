using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C96 RID: 11414
	[ChildElementInfo(typeof(ProtectedRange))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ProtectedRanges : OpenXmlCompositeElement
	{
		// Token: 0x170083FE RID: 33790
		// (get) Token: 0x060185C8 RID: 99784 RVA: 0x002E5C5B File Offset: 0x002E3E5B
		public override string LocalName
		{
			get
			{
				return "protectedRanges";
			}
		}

		// Token: 0x170083FF RID: 33791
		// (get) Token: 0x060185C9 RID: 99785 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008400 RID: 33792
		// (get) Token: 0x060185CA RID: 99786 RVA: 0x00340EF1 File Offset: 0x0033F0F1
		internal override int ElementTypeId
		{
			get
			{
				return 11394;
			}
		}

		// Token: 0x060185CB RID: 99787 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060185CC RID: 99788 RVA: 0x00293ECF File Offset: 0x002920CF
		public ProtectedRanges()
		{
		}

		// Token: 0x060185CD RID: 99789 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ProtectedRanges(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060185CE RID: 99790 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ProtectedRanges(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060185CF RID: 99791 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ProtectedRanges(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060185D0 RID: 99792 RVA: 0x00340EF8 File Offset: 0x0033F0F8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "protectedRange" == name)
			{
				return new ProtectedRange();
			}
			return null;
		}

		// Token: 0x060185D1 RID: 99793 RVA: 0x00340F13 File Offset: 0x0033F113
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ProtectedRanges>(deep);
		}

		// Token: 0x04009FEB RID: 40939
		private const string tagName = "protectedRanges";

		// Token: 0x04009FEC RID: 40940
		private const byte tagNsId = 22;

		// Token: 0x04009FED RID: 40941
		internal const int ElementTypeIdConst = 11394;
	}
}
