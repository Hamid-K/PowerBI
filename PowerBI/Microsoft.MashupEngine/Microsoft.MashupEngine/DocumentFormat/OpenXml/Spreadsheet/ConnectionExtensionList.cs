using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CD8 RID: 11480
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ConnectionExtension))]
	internal class ConnectionExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x170085BA RID: 34234
		// (get) Token: 0x06018A21 RID: 100897 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x170085BB RID: 34235
		// (get) Token: 0x06018A22 RID: 100898 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170085BC RID: 34236
		// (get) Token: 0x06018A23 RID: 100899 RVA: 0x003435A7 File Offset: 0x003417A7
		internal override int ElementTypeId
		{
			get
			{
				return 11461;
			}
		}

		// Token: 0x06018A24 RID: 100900 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018A25 RID: 100901 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConnectionExtensionList()
		{
		}

		// Token: 0x06018A26 RID: 100902 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConnectionExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018A27 RID: 100903 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConnectionExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018A28 RID: 100904 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConnectionExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018A29 RID: 100905 RVA: 0x003435AE File Offset: 0x003417AE
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ext" == name)
			{
				return new ConnectionExtension();
			}
			return null;
		}

		// Token: 0x06018A2A RID: 100906 RVA: 0x003435C9 File Offset: 0x003417C9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConnectionExtensionList>(deep);
		}

		// Token: 0x0400A119 RID: 41241
		private const string tagName = "extLst";

		// Token: 0x0400A11A RID: 41242
		private const byte tagNsId = 22;

		// Token: 0x0400A11B RID: 41243
		internal const int ElementTypeIdConst = 11461;
	}
}
