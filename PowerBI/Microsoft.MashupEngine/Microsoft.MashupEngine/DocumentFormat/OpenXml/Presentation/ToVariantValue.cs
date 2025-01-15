using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A38 RID: 10808
	[GeneratedCode("DomGen", "2.0")]
	internal class ToVariantValue : TimeListAnimationVariantType
	{
		// Token: 0x1700710F RID: 28943
		// (get) Token: 0x06015BA5 RID: 88997 RVA: 0x002FCA83 File Offset: 0x002FAC83
		public override string LocalName
		{
			get
			{
				return "to";
			}
		}

		// Token: 0x17007110 RID: 28944
		// (get) Token: 0x06015BA6 RID: 88998 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007111 RID: 28945
		// (get) Token: 0x06015BA7 RID: 88999 RVA: 0x00322838 File Offset: 0x00320A38
		internal override int ElementTypeId
		{
			get
			{
				return 12227;
			}
		}

		// Token: 0x06015BA8 RID: 89000 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015BA9 RID: 89001 RVA: 0x0032283F File Offset: 0x00320A3F
		public ToVariantValue()
		{
		}

		// Token: 0x06015BAA RID: 89002 RVA: 0x00322847 File Offset: 0x00320A47
		public ToVariantValue(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015BAB RID: 89003 RVA: 0x00322850 File Offset: 0x00320A50
		public ToVariantValue(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015BAC RID: 89004 RVA: 0x00322859 File Offset: 0x00320A59
		public ToVariantValue(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015BAD RID: 89005 RVA: 0x00322862 File Offset: 0x00320A62
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ToVariantValue>(deep);
		}

		// Token: 0x0400948E RID: 38030
		private const string tagName = "to";

		// Token: 0x0400948F RID: 38031
		private const byte tagNsId = 24;

		// Token: 0x04009490 RID: 38032
		internal const int ElementTypeIdConst = 12227;
	}
}
