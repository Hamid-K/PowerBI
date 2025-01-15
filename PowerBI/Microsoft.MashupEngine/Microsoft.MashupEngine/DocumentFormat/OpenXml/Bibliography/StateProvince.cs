using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028E2 RID: 10466
	[GeneratedCode("DomGen", "2.0")]
	internal class StateProvince : OpenXmlLeafTextElement
	{
		// Token: 0x1700694E RID: 26958
		// (get) Token: 0x06014A04 RID: 84484 RVA: 0x003150BC File Offset: 0x003132BC
		public override string LocalName
		{
			get
			{
				return "StateProvince";
			}
		}

		// Token: 0x1700694F RID: 26959
		// (get) Token: 0x06014A05 RID: 84485 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006950 RID: 26960
		// (get) Token: 0x06014A06 RID: 84486 RVA: 0x003150C3 File Offset: 0x003132C3
		internal override int ElementTypeId
		{
			get
			{
				return 10821;
			}
		}

		// Token: 0x06014A07 RID: 84487 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014A08 RID: 84488 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public StateProvince()
		{
		}

		// Token: 0x06014A09 RID: 84489 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public StateProvince(string text)
			: base(text)
		{
		}

		// Token: 0x06014A0A RID: 84490 RVA: 0x003150CC File Offset: 0x003132CC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014A0B RID: 84491 RVA: 0x003150E7 File Offset: 0x003132E7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StateProvince>(deep);
		}

		// Token: 0x04008F32 RID: 36658
		private const string tagName = "StateProvince";

		// Token: 0x04008F33 RID: 36659
		private const byte tagNsId = 9;

		// Token: 0x04008F34 RID: 36660
		internal const int ElementTypeIdConst = 10821;
	}
}
