using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028DC RID: 10460
	[GeneratedCode("DomGen", "2.0")]
	internal class Publisher : OpenXmlLeafTextElement
	{
		// Token: 0x1700693C RID: 26940
		// (get) Token: 0x060149D4 RID: 84436 RVA: 0x00314F84 File Offset: 0x00313184
		public override string LocalName
		{
			get
			{
				return "Publisher";
			}
		}

		// Token: 0x1700693D RID: 26941
		// (get) Token: 0x060149D5 RID: 84437 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700693E RID: 26942
		// (get) Token: 0x060149D6 RID: 84438 RVA: 0x00314F8B File Offset: 0x0031318B
		internal override int ElementTypeId
		{
			get
			{
				return 10814;
			}
		}

		// Token: 0x060149D7 RID: 84439 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060149D8 RID: 84440 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Publisher()
		{
		}

		// Token: 0x060149D9 RID: 84441 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Publisher(string text)
			: base(text)
		{
		}

		// Token: 0x060149DA RID: 84442 RVA: 0x00314F94 File Offset: 0x00313194
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060149DB RID: 84443 RVA: 0x00314FAF File Offset: 0x003131AF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Publisher>(deep);
		}

		// Token: 0x04008F20 RID: 36640
		private const string tagName = "Publisher";

		// Token: 0x04008F21 RID: 36641
		private const byte tagNsId = 9;

		// Token: 0x04008F22 RID: 36642
		internal const int ElementTypeIdConst = 10814;
	}
}
