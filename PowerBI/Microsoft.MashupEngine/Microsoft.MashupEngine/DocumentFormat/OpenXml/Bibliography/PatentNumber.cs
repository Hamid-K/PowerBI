using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028D8 RID: 10456
	[GeneratedCode("DomGen", "2.0")]
	internal class PatentNumber : OpenXmlLeafTextElement
	{
		// Token: 0x17006930 RID: 26928
		// (get) Token: 0x060149B4 RID: 84404 RVA: 0x00314EB4 File Offset: 0x003130B4
		public override string LocalName
		{
			get
			{
				return "PatentNumber";
			}
		}

		// Token: 0x17006931 RID: 26929
		// (get) Token: 0x060149B5 RID: 84405 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006932 RID: 26930
		// (get) Token: 0x060149B6 RID: 84406 RVA: 0x00314EBB File Offset: 0x003130BB
		internal override int ElementTypeId
		{
			get
			{
				return 10810;
			}
		}

		// Token: 0x060149B7 RID: 84407 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060149B8 RID: 84408 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public PatentNumber()
		{
		}

		// Token: 0x060149B9 RID: 84409 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public PatentNumber(string text)
			: base(text)
		{
		}

		// Token: 0x060149BA RID: 84410 RVA: 0x00314EC4 File Offset: 0x003130C4
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060149BB RID: 84411 RVA: 0x00314EDF File Offset: 0x003130DF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PatentNumber>(deep);
		}

		// Token: 0x04008F14 RID: 36628
		private const string tagName = "PatentNumber";

		// Token: 0x04008F15 RID: 36629
		private const byte tagNsId = 9;

		// Token: 0x04008F16 RID: 36630
		internal const int ElementTypeIdConst = 10810;
	}
}
