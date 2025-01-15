using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C21 RID: 11297
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DdeItem))]
	internal class DdeItems : OpenXmlCompositeElement
	{
		// Token: 0x1700806E RID: 32878
		// (get) Token: 0x06017D91 RID: 97681 RVA: 0x0033BD02 File Offset: 0x00339F02
		public override string LocalName
		{
			get
			{
				return "ddeItems";
			}
		}

		// Token: 0x1700806F RID: 32879
		// (get) Token: 0x06017D92 RID: 97682 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008070 RID: 32880
		// (get) Token: 0x06017D93 RID: 97683 RVA: 0x0033BD09 File Offset: 0x00339F09
		internal override int ElementTypeId
		{
			get
			{
				return 11278;
			}
		}

		// Token: 0x06017D94 RID: 97684 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06017D95 RID: 97685 RVA: 0x00293ECF File Offset: 0x002920CF
		public DdeItems()
		{
		}

		// Token: 0x06017D96 RID: 97686 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DdeItems(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017D97 RID: 97687 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DdeItems(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017D98 RID: 97688 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DdeItems(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017D99 RID: 97689 RVA: 0x0033BD10 File Offset: 0x00339F10
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ddeItem" == name)
			{
				return new DdeItem();
			}
			return null;
		}

		// Token: 0x06017D9A RID: 97690 RVA: 0x0033BD2B File Offset: 0x00339F2B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DdeItems>(deep);
		}

		// Token: 0x04009DD4 RID: 40404
		private const string tagName = "ddeItems";

		// Token: 0x04009DD5 RID: 40405
		private const byte tagNsId = 22;

		// Token: 0x04009DD6 RID: 40406
		internal const int ElementTypeIdConst = 11278;
	}
}
