using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Bibliography
{
	// Token: 0x020028E0 RID: 10464
	[GeneratedCode("DomGen", "2.0")]
	internal class ShortTitle : OpenXmlLeafTextElement
	{
		// Token: 0x17006948 RID: 26952
		// (get) Token: 0x060149F4 RID: 84468 RVA: 0x00315054 File Offset: 0x00313254
		public override string LocalName
		{
			get
			{
				return "ShortTitle";
			}
		}

		// Token: 0x17006949 RID: 26953
		// (get) Token: 0x060149F5 RID: 84469 RVA: 0x00142610 File Offset: 0x00140810
		internal override byte NamespaceId
		{
			get
			{
				return 9;
			}
		}

		// Token: 0x1700694A RID: 26954
		// (get) Token: 0x060149F6 RID: 84470 RVA: 0x0031505B File Offset: 0x0031325B
		internal override int ElementTypeId
		{
			get
			{
				return 10819;
			}
		}

		// Token: 0x060149F7 RID: 84471 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060149F8 RID: 84472 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public ShortTitle()
		{
		}

		// Token: 0x060149F9 RID: 84473 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public ShortTitle(string text)
			: base(text)
		{
		}

		// Token: 0x060149FA RID: 84474 RVA: 0x00315064 File Offset: 0x00313264
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new StringValue
			{
				InnerText = text
			};
		}

		// Token: 0x060149FB RID: 84475 RVA: 0x0031507F File Offset: 0x0031327F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShortTitle>(deep);
		}

		// Token: 0x04008F2C RID: 36652
		private const string tagName = "ShortTitle";

		// Token: 0x04008F2D RID: 36653
		private const byte tagNsId = 9;

		// Token: 0x04008F2E RID: 36654
		internal const int ElementTypeIdConst = 10819;
	}
}
