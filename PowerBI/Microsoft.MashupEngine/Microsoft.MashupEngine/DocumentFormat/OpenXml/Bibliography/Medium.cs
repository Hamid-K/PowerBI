using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028D3 RID: 10451
	[GeneratedCode("DomGen", "2.0")]
	internal class Medium : OpenXmlLeafTextElement
	{
		// Token: 0x17006921 RID: 26913
		// (get) Token: 0x0601498C RID: 84364 RVA: 0x00314DB0 File Offset: 0x00312FB0
		public override string LocalName
		{
			get
			{
				return "Medium";
			}
		}

		// Token: 0x17006922 RID: 26914
		// (get) Token: 0x0601498D RID: 84365 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006923 RID: 26915
		// (get) Token: 0x0601498E RID: 84366 RVA: 0x00314DB7 File Offset: 0x00312FB7
		internal override int ElementTypeId
		{
			get
			{
				return 10805;
			}
		}

		// Token: 0x0601498F RID: 84367 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014990 RID: 84368 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Medium()
		{
		}

		// Token: 0x06014991 RID: 84369 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Medium(string text)
			: base(text)
		{
		}

		// Token: 0x06014992 RID: 84370 RVA: 0x00314DC0 File Offset: 0x00312FC0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014993 RID: 84371 RVA: 0x00314DDB File Offset: 0x00312FDB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Medium>(deep);
		}

		// Token: 0x04008F05 RID: 36613
		private const string tagName = "Medium";

		// Token: 0x04008F06 RID: 36614
		private const byte tagNsId = 9;

		// Token: 0x04008F07 RID: 36615
		internal const int ElementTypeIdConst = 10805;
	}
}
