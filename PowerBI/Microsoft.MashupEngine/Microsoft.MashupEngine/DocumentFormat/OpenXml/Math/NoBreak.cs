using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200296D RID: 10605
	[GeneratedCode("DomGen", "2.0")]
	internal class NoBreak : OnOffType
	{
		// Token: 0x17006C52 RID: 27730
		// (get) Token: 0x06015132 RID: 86322 RVA: 0x0031B430 File Offset: 0x00319630
		public override string LocalName
		{
			get
			{
				return "noBreak";
			}
		}

		// Token: 0x17006C53 RID: 27731
		// (get) Token: 0x06015133 RID: 86323 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C54 RID: 27732
		// (get) Token: 0x06015134 RID: 86324 RVA: 0x0031B437 File Offset: 0x00319637
		internal override int ElementTypeId
		{
			get
			{
				return 10877;
			}
		}

		// Token: 0x06015135 RID: 86325 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015137 RID: 86327 RVA: 0x0031B43E File Offset: 0x0031963E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NoBreak>(deep);
		}

		// Token: 0x04009153 RID: 37203
		private const string tagName = "noBreak";

		// Token: 0x04009154 RID: 37204
		private const byte tagNsId = 21;

		// Token: 0x04009155 RID: 37205
		internal const int ElementTypeIdConst = 10877;
	}
}
