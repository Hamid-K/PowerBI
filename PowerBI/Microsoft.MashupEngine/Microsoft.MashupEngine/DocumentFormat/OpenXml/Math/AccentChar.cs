using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200298B RID: 10635
	[GeneratedCode("DomGen", "2.0")]
	internal class AccentChar : CharType
	{
		// Token: 0x17006CB7 RID: 27831
		// (get) Token: 0x06015201 RID: 86529 RVA: 0x0031B95C File Offset: 0x00319B5C
		public override string LocalName
		{
			get
			{
				return "chr";
			}
		}

		// Token: 0x17006CB8 RID: 27832
		// (get) Token: 0x06015202 RID: 86530 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CB9 RID: 27833
		// (get) Token: 0x06015203 RID: 86531 RVA: 0x0031B963 File Offset: 0x00319B63
		internal override int ElementTypeId
		{
			get
			{
				return 10870;
			}
		}

		// Token: 0x06015204 RID: 86532 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015206 RID: 86534 RVA: 0x0031B972 File Offset: 0x00319B72
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AccentChar>(deep);
		}

		// Token: 0x040091B2 RID: 37298
		private const string tagName = "chr";

		// Token: 0x040091B3 RID: 37299
		private const byte tagNsId = 21;

		// Token: 0x040091B4 RID: 37300
		internal const int ElementTypeIdConst = 10870;
	}
}
