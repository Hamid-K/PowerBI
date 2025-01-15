using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CA1 RID: 11425
	[ChildElementInfo(typeof(CellSmartTags))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SmartTags : OpenXmlCompositeElement
	{
		// Token: 0x1700843C RID: 33852
		// (get) Token: 0x0601866D RID: 99949 RVA: 0x002AC71B File Offset: 0x002AA91B
		public override string LocalName
		{
			get
			{
				return "smartTags";
			}
		}

		// Token: 0x1700843D RID: 33853
		// (get) Token: 0x0601866E RID: 99950 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700843E RID: 33854
		// (get) Token: 0x0601866F RID: 99951 RVA: 0x00341484 File Offset: 0x0033F684
		internal override int ElementTypeId
		{
			get
			{
				return 11405;
			}
		}

		// Token: 0x06018670 RID: 99952 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018671 RID: 99953 RVA: 0x00293ECF File Offset: 0x002920CF
		public SmartTags()
		{
		}

		// Token: 0x06018672 RID: 99954 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SmartTags(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018673 RID: 99955 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SmartTags(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018674 RID: 99956 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SmartTags(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018675 RID: 99957 RVA: 0x0034148B File Offset: 0x0033F68B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "cellSmartTags" == name)
			{
				return new CellSmartTags();
			}
			return null;
		}

		// Token: 0x06018676 RID: 99958 RVA: 0x003414A6 File Offset: 0x0033F6A6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SmartTags>(deep);
		}

		// Token: 0x0400A018 RID: 40984
		private const string tagName = "smartTags";

		// Token: 0x0400A019 RID: 40985
		private const byte tagNsId = 22;

		// Token: 0x0400A01A RID: 40986
		internal const int ElementTypeIdConst = 11405;
	}
}
