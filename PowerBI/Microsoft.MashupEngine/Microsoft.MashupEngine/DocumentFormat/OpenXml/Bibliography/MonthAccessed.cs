using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028D5 RID: 10453
	[GeneratedCode("DomGen", "2.0")]
	internal class MonthAccessed : OpenXmlLeafTextElement
	{
		// Token: 0x17006927 RID: 26919
		// (get) Token: 0x0601499C RID: 84380 RVA: 0x00314E18 File Offset: 0x00313018
		public override string LocalName
		{
			get
			{
				return "MonthAccessed";
			}
		}

		// Token: 0x17006928 RID: 26920
		// (get) Token: 0x0601499D RID: 84381 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x17006929 RID: 26921
		// (get) Token: 0x0601499E RID: 84382 RVA: 0x00314E1F File Offset: 0x0031301F
		internal override int ElementTypeId
		{
			get
			{
				return 10807;
			}
		}

		// Token: 0x0601499F RID: 84383 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060149A0 RID: 84384 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public MonthAccessed()
		{
		}

		// Token: 0x060149A1 RID: 84385 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public MonthAccessed(string text)
			: base(text)
		{
		}

		// Token: 0x060149A2 RID: 84386 RVA: 0x00314E28 File Offset: 0x00313028
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060149A3 RID: 84387 RVA: 0x00314E43 File Offset: 0x00313043
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MonthAccessed>(deep);
		}

		// Token: 0x04008F0B RID: 36619
		private const string tagName = "MonthAccessed";

		// Token: 0x04008F0C RID: 36620
		private const byte tagNsId = 9;

		// Token: 0x04008F0D RID: 36621
		internal const int ElementTypeIdConst = 10807;
	}
}
