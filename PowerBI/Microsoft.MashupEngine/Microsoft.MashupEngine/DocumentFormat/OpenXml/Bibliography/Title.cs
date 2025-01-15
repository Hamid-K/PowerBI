using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028E7 RID: 10471
	[GeneratedCode("DomGen", "2.0")]
	internal class Title : OpenXmlLeafTextElement
	{
		// Token: 0x1700695D RID: 26973
		// (get) Token: 0x06014A2C RID: 84524 RVA: 0x003151C0 File Offset: 0x003133C0
		public override string LocalName
		{
			get
			{
				return "Title";
			}
		}

		// Token: 0x1700695E RID: 26974
		// (get) Token: 0x06014A2D RID: 84525 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700695F RID: 26975
		// (get) Token: 0x06014A2E RID: 84526 RVA: 0x003151C7 File Offset: 0x003133C7
		internal override int ElementTypeId
		{
			get
			{
				return 10826;
			}
		}

		// Token: 0x06014A2F RID: 84527 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A30 RID: 84528 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Title()
		{
		}

		// Token: 0x06014A31 RID: 84529 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Title(string text)
			: base(text)
		{
		}

		// Token: 0x06014A32 RID: 84530 RVA: 0x003151D0 File Offset: 0x003133D0
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014A33 RID: 84531 RVA: 0x003151EB File Offset: 0x003133EB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Title>(deep);
		}

		// Token: 0x04008F41 RID: 36673
		private const string tagName = "Title";

		// Token: 0x04008F42 RID: 36674
		private const byte tagNsId = 9;

		// Token: 0x04008F43 RID: 36675
		internal const int ElementTypeIdConst = 10826;
	}
}
