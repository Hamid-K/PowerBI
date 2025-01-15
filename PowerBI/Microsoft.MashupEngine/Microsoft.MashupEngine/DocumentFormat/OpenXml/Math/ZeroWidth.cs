using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200297E RID: 10622
	[GeneratedCode("DomGen", "2.0")]
	internal class ZeroWidth : OnOffType
	{
		// Token: 0x17006C85 RID: 27781
		// (get) Token: 0x06015198 RID: 86424 RVA: 0x0031B5B7 File Offset: 0x003197B7
		public override string LocalName
		{
			get
			{
				return "zeroWid";
			}
		}

		// Token: 0x17006C86 RID: 27782
		// (get) Token: 0x06015199 RID: 86425 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C87 RID: 27783
		// (get) Token: 0x0601519A RID: 86426 RVA: 0x0031B5BE File Offset: 0x003197BE
		internal override int ElementTypeId
		{
			get
			{
				return 10930;
			}
		}

		// Token: 0x0601519B RID: 86427 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601519D RID: 86429 RVA: 0x0031B5C5 File Offset: 0x003197C5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ZeroWidth>(deep);
		}

		// Token: 0x04009186 RID: 37254
		private const string tagName = "zeroWid";

		// Token: 0x04009187 RID: 37255
		private const byte tagNsId = 21;

		// Token: 0x04009188 RID: 37256
		internal const int ElementTypeIdConst = 10930;
	}
}
