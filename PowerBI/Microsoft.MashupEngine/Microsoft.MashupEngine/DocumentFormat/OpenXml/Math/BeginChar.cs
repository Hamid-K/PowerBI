using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200298C RID: 10636
	[GeneratedCode("DomGen", "2.0")]
	internal class BeginChar : CharType
	{
		// Token: 0x17006CBA RID: 27834
		// (get) Token: 0x06015207 RID: 86535 RVA: 0x0031B97B File Offset: 0x00319B7B
		public override string LocalName
		{
			get
			{
				return "begChr";
			}
		}

		// Token: 0x17006CBB RID: 27835
		// (get) Token: 0x06015208 RID: 86536 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CBC RID: 27836
		// (get) Token: 0x06015209 RID: 86537 RVA: 0x0031B982 File Offset: 0x00319B82
		internal override int ElementTypeId
		{
			get
			{
				return 10889;
			}
		}

		// Token: 0x0601520A RID: 86538 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601520C RID: 86540 RVA: 0x0031B989 File Offset: 0x00319B89
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BeginChar>(deep);
		}

		// Token: 0x040091B5 RID: 37301
		private const string tagName = "begChr";

		// Token: 0x040091B6 RID: 37302
		private const byte tagNsId = 21;

		// Token: 0x040091B7 RID: 37303
		internal const int ElementTypeIdConst = 10889;
	}
}
