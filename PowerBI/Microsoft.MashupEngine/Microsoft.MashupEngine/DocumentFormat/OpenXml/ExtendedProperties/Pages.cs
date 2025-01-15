using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x0200293A RID: 10554
	[GeneratedCode("DomGen", "2.0")]
	internal class Pages : OpenXmlLeafTextElement
	{
		// Token: 0x17006B29 RID: 27433
		// (get) Token: 0x06014E82 RID: 85634 RVA: 0x00314E80 File Offset: 0x00313080
		public override string LocalName
		{
			get
			{
				return "Pages";
			}
		}

		// Token: 0x17006B2A RID: 27434
		// (get) Token: 0x06014E83 RID: 85635 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B2B RID: 27435
		// (get) Token: 0x06014E84 RID: 85636 RVA: 0x00318B1C File Offset: 0x00316D1C
		internal override int ElementTypeId
		{
			get
			{
				return 11002;
			}
		}

		// Token: 0x06014E85 RID: 85637 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014E86 RID: 85638 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Pages()
		{
		}

		// Token: 0x06014E87 RID: 85639 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Pages(string text)
			: base(text)
		{
		}

		// Token: 0x06014E88 RID: 85640 RVA: 0x00318B24 File Offset: 0x00316D24
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014E89 RID: 85641 RVA: 0x00318B3F File Offset: 0x00316D3F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Pages>(deep);
		}

		// Token: 0x04009088 RID: 37000
		private const string tagName = "Pages";

		// Token: 0x04009089 RID: 37001
		private const byte tagNsId = 3;

		// Token: 0x0400908A RID: 37002
		internal const int ElementTypeIdConst = 11002;
	}
}
