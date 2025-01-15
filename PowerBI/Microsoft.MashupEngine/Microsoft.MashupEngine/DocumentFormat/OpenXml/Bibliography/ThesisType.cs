using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028E6 RID: 10470
	[GeneratedCode("DomGen", "2.0")]
	internal class ThesisType : OpenXmlLeafTextElement
	{
		// Token: 0x1700695A RID: 26970
		// (get) Token: 0x06014A24 RID: 84516 RVA: 0x0031518C File Offset: 0x0031338C
		public override string LocalName
		{
			get
			{
				return "ThesisType";
			}
		}

		// Token: 0x1700695B RID: 26971
		// (get) Token: 0x06014A25 RID: 84517 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700695C RID: 26972
		// (get) Token: 0x06014A26 RID: 84518 RVA: 0x00315193 File Offset: 0x00313393
		internal override int ElementTypeId
		{
			get
			{
				return 10825;
			}
		}

		// Token: 0x06014A27 RID: 84519 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A28 RID: 84520 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ThesisType()
		{
		}

		// Token: 0x06014A29 RID: 84521 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ThesisType(string text)
			: base(text)
		{
		}

		// Token: 0x06014A2A RID: 84522 RVA: 0x0031519C File Offset: 0x0031339C
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014A2B RID: 84523 RVA: 0x003151B7 File Offset: 0x003133B7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ThesisType>(deep);
		}

		// Token: 0x04008F3E RID: 36670
		private const string tagName = "ThesisType";

		// Token: 0x04008F3F RID: 36671
		private const byte tagNsId = 9;

		// Token: 0x04008F40 RID: 36672
		internal const int ElementTypeIdConst = 10825;
	}
}
