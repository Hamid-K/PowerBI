using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028D4 RID: 10452
	[GeneratedCode("DomGen", "2.0")]
	internal class Month : OpenXmlLeafTextElement
	{
		// Token: 0x17006924 RID: 26916
		// (get) Token: 0x06014994 RID: 84372 RVA: 0x00314DE4 File Offset: 0x00312FE4
		public override string LocalName
		{
			get
			{
				return "Month";
			}
		}

		// Token: 0x17006925 RID: 26917
		// (get) Token: 0x06014995 RID: 84373 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006926 RID: 26918
		// (get) Token: 0x06014996 RID: 84374 RVA: 0x00314DEB File Offset: 0x00312FEB
		internal override int ElementTypeId
		{
			get
			{
				return 10806;
			}
		}

		// Token: 0x06014997 RID: 84375 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014998 RID: 84376 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Month()
		{
		}

		// Token: 0x06014999 RID: 84377 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Month(string text)
			: base(text)
		{
		}

		// Token: 0x0601499A RID: 84378 RVA: 0x00314DF4 File Offset: 0x00312FF4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x0601499B RID: 84379 RVA: 0x00314E0F File Offset: 0x0031300F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Month>(deep);
		}

		// Token: 0x04008F08 RID: 36616
		private const string tagName = "Month";

		// Token: 0x04008F09 RID: 36617
		private const byte tagNsId = 9;

		// Token: 0x04008F0A RID: 36618
		internal const int ElementTypeIdConst = 10806;
	}
}
