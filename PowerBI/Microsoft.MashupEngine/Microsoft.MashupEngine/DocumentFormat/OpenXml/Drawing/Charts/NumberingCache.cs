using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200257B RID: 9595
	[GeneratedCode("DomGen", "2.0")]
	internal class NumberingCache : NumberDataType
	{
		// Token: 0x17005602 RID: 22018
		// (get) Token: 0x06011E64 RID: 73316 RVA: 0x002F37B4 File Offset: 0x002F19B4
		public override string LocalName
		{
			get
			{
				return "numCache";
			}
		}

		// Token: 0x17005603 RID: 22019
		// (get) Token: 0x06011E65 RID: 73317 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005604 RID: 22020
		// (get) Token: 0x06011E66 RID: 73318 RVA: 0x002F37BB File Offset: 0x002F19BB
		internal override int ElementTypeId
		{
			get
			{
				return 10396;
			}
		}

		// Token: 0x06011E67 RID: 73319 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011E68 RID: 73320 RVA: 0x002F37C2 File Offset: 0x002F19C2
		public NumberingCache()
		{
		}

		// Token: 0x06011E69 RID: 73321 RVA: 0x002F37CA File Offset: 0x002F19CA
		public NumberingCache(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011E6A RID: 73322 RVA: 0x002F37D3 File Offset: 0x002F19D3
		public NumberingCache(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011E6B RID: 73323 RVA: 0x002F37DC File Offset: 0x002F19DC
		public NumberingCache(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011E6C RID: 73324 RVA: 0x002F37E5 File Offset: 0x002F19E5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberingCache>(deep);
		}

		// Token: 0x04007D27 RID: 32039
		private const string tagName = "numCache";

		// Token: 0x04007D28 RID: 32040
		private const byte tagNsId = 11;

		// Token: 0x04007D29 RID: 32041
		internal const int ElementTypeIdConst = 10396;
	}
}
