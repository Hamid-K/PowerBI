using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.VariantTypes
{
	// Token: 0x02002929 RID: 10537
	[GeneratedCode("DomGen", "2.0")]
	internal class VTFileTime : OpenXmlLeafTextElement
	{
		// Token: 0x17006AD6 RID: 27350
		// (get) Token: 0x06014DBB RID: 85435 RVA: 0x00318184 File Offset: 0x00316384
		public override string LocalName
		{
			get
			{
				return "filetime";
			}
		}

		// Token: 0x17006AD7 RID: 27351
		// (get) Token: 0x06014DBC RID: 85436 RVA: 0x00075E2C File Offset: 0x0007402C
		internal override byte NamespaceId
		{
			get
			{
				return 5;
			}
		}

		// Token: 0x17006AD8 RID: 27352
		// (get) Token: 0x06014DBD RID: 85437 RVA: 0x0031818B File Offset: 0x0031638B
		internal override int ElementTypeId
		{
			get
			{
				return 10987;
			}
		}

		// Token: 0x06014DBE RID: 85438 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014DBF RID: 85439 RVA: 0x002BC6A1 File Offset: 0x002BA8A1
		public VTFileTime()
		{
		}

		// Token: 0x06014DC0 RID: 85440 RVA: 0x002BC6A9 File Offset: 0x002BA8A9
		public VTFileTime(string text)
			: base(text)
		{
		}

		// Token: 0x06014DC1 RID: 85441 RVA: 0x00318194 File Offset: 0x00316394
		internal override OpenXmlSimpleType InnerTextToValue(string text)
		{
			return new DateTimeValue
			{
				InnerText = text
			};
		}

		// Token: 0x06014DC2 RID: 85442 RVA: 0x003181AF File Offset: 0x003163AF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VTFileTime>(deep);
		}

		// Token: 0x0400902D RID: 36909
		private const string tagName = "filetime";

		// Token: 0x0400902E RID: 36910
		private const byte tagNsId = 5;

		// Token: 0x0400902F RID: 36911
		internal const int ElementTypeIdConst = 10987;
	}
}
