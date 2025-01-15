using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028D7 RID: 10455
	[GeneratedCode("DomGen", "2.0")]
	internal class Pages : OpenXmlLeafTextElement
	{
		// Token: 0x1700692D RID: 26925
		// (get) Token: 0x060149AC RID: 84396 RVA: 0x00314E80 File Offset: 0x00313080
		public override string LocalName
		{
			get
			{
				return "Pages";
			}
		}

		// Token: 0x1700692E RID: 26926
		// (get) Token: 0x060149AD RID: 84397 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700692F RID: 26927
		// (get) Token: 0x060149AE RID: 84398 RVA: 0x00314E87 File Offset: 0x00313087
		internal override int ElementTypeId
		{
			get
			{
				return 10809;
			}
		}

		// Token: 0x060149AF RID: 84399 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060149B0 RID: 84400 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Pages()
		{
		}

		// Token: 0x060149B1 RID: 84401 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Pages(string text)
			: base(text)
		{
		}

		// Token: 0x060149B2 RID: 84402 RVA: 0x00314E90 File Offset: 0x00313090
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060149B3 RID: 84403 RVA: 0x00314EAB File Offset: 0x003130AB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Pages>(deep);
		}

		// Token: 0x04008F11 RID: 36625
		private const string tagName = "Pages";

		// Token: 0x04008F12 RID: 36626
		private const byte tagNsId = 9;

		// Token: 0x04008F13 RID: 36627
		internal const int ElementTypeIdConst = 10809;
	}
}
