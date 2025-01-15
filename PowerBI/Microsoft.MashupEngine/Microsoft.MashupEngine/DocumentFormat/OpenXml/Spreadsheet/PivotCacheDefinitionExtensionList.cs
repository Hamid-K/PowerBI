using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CD2 RID: 11474
	[ChildElementInfo(typeof(PivotCacheDefinitionExtension))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PivotCacheDefinitionExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x1700856C RID: 34156
		// (get) Token: 0x0601897B RID: 100731 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x1700856D RID: 34157
		// (get) Token: 0x0601897C RID: 100732 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700856E RID: 34158
		// (get) Token: 0x0601897D RID: 100733 RVA: 0x00342DB3 File Offset: 0x00340FB3
		internal override int ElementTypeId
		{
			get
			{
				return 11455;
			}
		}

		// Token: 0x0601897E RID: 100734 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601897F RID: 100735 RVA: 0x00293ECF File Offset: 0x002920CF
		public PivotCacheDefinitionExtensionList()
		{
		}

		// Token: 0x06018980 RID: 100736 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PivotCacheDefinitionExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018981 RID: 100737 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PivotCacheDefinitionExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018982 RID: 100738 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PivotCacheDefinitionExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018983 RID: 100739 RVA: 0x00342DBA File Offset: 0x00340FBA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ext" == name)
			{
				return new PivotCacheDefinitionExtension();
			}
			return null;
		}

		// Token: 0x06018984 RID: 100740 RVA: 0x00342DD5 File Offset: 0x00340FD5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PivotCacheDefinitionExtensionList>(deep);
		}

		// Token: 0x0400A0F9 RID: 41209
		private const string tagName = "extLst";

		// Token: 0x0400A0FA RID: 41210
		private const byte tagNsId = 22;

		// Token: 0x0400A0FB RID: 41211
		internal const int ElementTypeIdConst = 11455;
	}
}
