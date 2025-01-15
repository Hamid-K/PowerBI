using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.ExtendedProperties
{
	// Token: 0x0200293E RID: 10558
	[GeneratedCode("DomGen", "2.0")]
	internal class Paragraphs : OpenXmlLeafTextElement
	{
		// Token: 0x17006B35 RID: 27445
		// (get) Token: 0x06014EA2 RID: 85666 RVA: 0x00318BDC File Offset: 0x00316DDC
		public override string LocalName
		{
			get
			{
				return "Paragraphs";
			}
		}

		// Token: 0x17006B36 RID: 27446
		// (get) Token: 0x06014EA3 RID: 85667 RVA: 0x0000240C File Offset: 0x0000060C
		internal override byte NamespaceId
		{
			get
			{
				return 3;
			}
		}

		// Token: 0x17006B37 RID: 27447
		// (get) Token: 0x06014EA4 RID: 85668 RVA: 0x00318BE3 File Offset: 0x00316DE3
		internal override int ElementTypeId
		{
			get
			{
				return 11007;
			}
		}

		// Token: 0x06014EA5 RID: 85669 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014EA6 RID: 85670 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public Paragraphs()
		{
		}

		// Token: 0x06014EA7 RID: 85671 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public Paragraphs(string text)
			: base(text)
		{
		}

		// Token: 0x06014EA8 RID: 85672 RVA: 0x00318BEC File Offset: 0x00316DEC
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new Int32Value
			{
				InnerText = text
			};
		}

		// Token: 0x06014EA9 RID: 85673 RVA: 0x00318C07 File Offset: 0x00316E07
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Paragraphs>(deep);
		}

		// Token: 0x04009094 RID: 37012
		private const string tagName = "Paragraphs";

		// Token: 0x04009095 RID: 37013
		private const byte tagNsId = 3;

		// Token: 0x04009096 RID: 37014
		internal const int ElementTypeIdConst = 11007;
	}
}
