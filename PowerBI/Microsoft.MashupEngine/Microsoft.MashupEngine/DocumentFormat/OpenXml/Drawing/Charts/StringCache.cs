using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002580 RID: 9600
	[GeneratedCode("DomGen", "2.0")]
	internal class StringCache : StringDataType
	{
		// Token: 0x1700561F RID: 22047
		// (get) Token: 0x06011EAA RID: 73386 RVA: 0x002F3A68 File Offset: 0x002F1C68
		public override string LocalName
		{
			get
			{
				return "strCache";
			}
		}

		// Token: 0x17005620 RID: 22048
		// (get) Token: 0x06011EAB RID: 73387 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005621 RID: 22049
		// (get) Token: 0x06011EAC RID: 73388 RVA: 0x002F3A6F File Offset: 0x002F1C6F
		internal override int ElementTypeId
		{
			get
			{
				return 10400;
			}
		}

		// Token: 0x06011EAD RID: 73389 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011EAE RID: 73390 RVA: 0x002F3A76 File Offset: 0x002F1C76
		public StringCache()
		{
		}

		// Token: 0x06011EAF RID: 73391 RVA: 0x002F3A7E File Offset: 0x002F1C7E
		public StringCache(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011EB0 RID: 73392 RVA: 0x002F3A87 File Offset: 0x002F1C87
		public StringCache(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011EB1 RID: 73393 RVA: 0x002F3A90 File Offset: 0x002F1C90
		public StringCache(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011EB2 RID: 73394 RVA: 0x002F3A99 File Offset: 0x002F1C99
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StringCache>(deep);
		}

		// Token: 0x04007D3B RID: 32059
		private const string tagName = "strCache";

		// Token: 0x04007D3C RID: 32060
		private const byte tagNsId = 11;

		// Token: 0x04007D3D RID: 32061
		internal const int ElementTypeIdConst = 10400;
	}
}
