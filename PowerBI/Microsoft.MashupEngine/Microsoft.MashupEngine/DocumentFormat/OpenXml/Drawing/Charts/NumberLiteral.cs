using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200257C RID: 9596
	[GeneratedCode("DomGen", "2.0")]
	internal class NumberLiteral : NumberDataType
	{
		// Token: 0x17005605 RID: 22021
		// (get) Token: 0x06011E6D RID: 73325 RVA: 0x002F37EE File Offset: 0x002F19EE
		public override string LocalName
		{
			get
			{
				return "numLit";
			}
		}

		// Token: 0x17005606 RID: 22022
		// (get) Token: 0x06011E6E RID: 73326 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005607 RID: 22023
		// (get) Token: 0x06011E6F RID: 73327 RVA: 0x002F37F5 File Offset: 0x002F19F5
		internal override int ElementTypeId
		{
			get
			{
				return 10398;
			}
		}

		// Token: 0x06011E70 RID: 73328 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011E71 RID: 73329 RVA: 0x002F37C2 File Offset: 0x002F19C2
		public NumberLiteral()
		{
		}

		// Token: 0x06011E72 RID: 73330 RVA: 0x002F37CA File Offset: 0x002F19CA
		public NumberLiteral(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011E73 RID: 73331 RVA: 0x002F37D3 File Offset: 0x002F19D3
		public NumberLiteral(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011E74 RID: 73332 RVA: 0x002F37DC File Offset: 0x002F19DC
		public NumberLiteral(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011E75 RID: 73333 RVA: 0x002F37FC File Offset: 0x002F19FC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberLiteral>(deep);
		}

		// Token: 0x04007D2A RID: 32042
		private const string tagName = "numLit";

		// Token: 0x04007D2B RID: 32043
		private const byte tagNsId = 11;

		// Token: 0x04007D2C RID: 32044
		internal const int ElementTypeIdConst = 10398;
	}
}
