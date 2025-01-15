using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028D9 RID: 10457
	[GeneratedCode("DomGen", "2.0")]
	internal class PeriodicalTitle : OpenXmlLeafTextElement
	{
		// Token: 0x17006933 RID: 26931
		// (get) Token: 0x060149BC RID: 84412 RVA: 0x00314EE8 File Offset: 0x003130E8
		public override string LocalName
		{
			get
			{
				return "PeriodicalTitle";
			}
		}

		// Token: 0x17006934 RID: 26932
		// (get) Token: 0x060149BD RID: 84413 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006935 RID: 26933
		// (get) Token: 0x060149BE RID: 84414 RVA: 0x00314EEF File Offset: 0x003130EF
		internal override int ElementTypeId
		{
			get
			{
				return 10811;
			}
		}

		// Token: 0x060149BF RID: 84415 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060149C0 RID: 84416 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public PeriodicalTitle()
		{
		}

		// Token: 0x060149C1 RID: 84417 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public PeriodicalTitle(string text)
			: base(text)
		{
		}

		// Token: 0x060149C2 RID: 84418 RVA: 0x00314EF8 File Offset: 0x003130F8
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060149C3 RID: 84419 RVA: 0x00314F13 File Offset: 0x00313113
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PeriodicalTitle>(deep);
		}

		// Token: 0x04008F17 RID: 36631
		private const string tagName = "PeriodicalTitle";

		// Token: 0x04008F18 RID: 36632
		private const byte tagNsId = 9;

		// Token: 0x04008F19 RID: 36633
		internal const int ElementTypeIdConst = 10811;
	}
}
