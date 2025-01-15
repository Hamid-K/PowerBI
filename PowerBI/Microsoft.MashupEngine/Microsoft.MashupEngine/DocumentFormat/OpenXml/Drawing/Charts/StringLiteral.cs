using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002581 RID: 9601
	[GeneratedCode("DomGen", "2.0")]
	internal class StringLiteral : StringDataType
	{
		// Token: 0x17005622 RID: 22050
		// (get) Token: 0x06011EB3 RID: 73395 RVA: 0x002F3AA2 File Offset: 0x002F1CA2
		public override string LocalName
		{
			get
			{
				return "strLit";
			}
		}

		// Token: 0x17005623 RID: 22051
		// (get) Token: 0x06011EB4 RID: 73396 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005624 RID: 22052
		// (get) Token: 0x06011EB5 RID: 73397 RVA: 0x002F3AA9 File Offset: 0x002F1CA9
		internal override int ElementTypeId
		{
			get
			{
				return 10405;
			}
		}

		// Token: 0x06011EB6 RID: 73398 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011EB7 RID: 73399 RVA: 0x002F3A76 File Offset: 0x002F1C76
		public StringLiteral()
		{
		}

		// Token: 0x06011EB8 RID: 73400 RVA: 0x002F3A7E File Offset: 0x002F1C7E
		public StringLiteral(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011EB9 RID: 73401 RVA: 0x002F3A87 File Offset: 0x002F1C87
		public StringLiteral(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011EBA RID: 73402 RVA: 0x002F3A90 File Offset: 0x002F1C90
		public StringLiteral(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011EBB RID: 73403 RVA: 0x002F3AB0 File Offset: 0x002F1CB0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StringLiteral>(deep);
		}

		// Token: 0x04007D3E RID: 32062
		private const string tagName = "strLit";

		// Token: 0x04007D3F RID: 32063
		private const byte tagNsId = 11;

		// Token: 0x04007D40 RID: 32064
		internal const int ElementTypeIdConst = 10405;
	}
}
