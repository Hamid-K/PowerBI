using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002973 RID: 10611
	[GeneratedCode("DomGen", "2.0")]
	internal class StrikeHorizontal : OnOffType
	{
		// Token: 0x17006C64 RID: 27748
		// (get) Token: 0x06015156 RID: 86358 RVA: 0x0031B4BA File Offset: 0x003196BA
		public override string LocalName
		{
			get
			{
				return "strikeH";
			}
		}

		// Token: 0x17006C65 RID: 27749
		// (get) Token: 0x06015157 RID: 86359 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C66 RID: 27750
		// (get) Token: 0x06015158 RID: 86360 RVA: 0x0031B4C1 File Offset: 0x003196C1
		internal override int ElementTypeId
		{
			get
			{
				return 10884;
			}
		}

		// Token: 0x06015159 RID: 86361 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601515B RID: 86363 RVA: 0x0031B4C8 File Offset: 0x003196C8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StrikeHorizontal>(deep);
		}

		// Token: 0x04009165 RID: 37221
		private const string tagName = "strikeH";

		// Token: 0x04009166 RID: 37222
		private const byte tagNsId = 21;

		// Token: 0x04009167 RID: 37223
		internal const int ElementTypeIdConst = 10884;
	}
}
