using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200297D RID: 10621
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowPhantom : OnOffType
	{
		// Token: 0x17006C82 RID: 27778
		// (get) Token: 0x06015192 RID: 86418 RVA: 0x0031B5A0 File Offset: 0x003197A0
		public override string LocalName
		{
			get
			{
				return "show";
			}
		}

		// Token: 0x17006C83 RID: 27779
		// (get) Token: 0x06015193 RID: 86419 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C84 RID: 27780
		// (get) Token: 0x06015194 RID: 86420 RVA: 0x0031B5A7 File Offset: 0x003197A7
		internal override int ElementTypeId
		{
			get
			{
				return 10929;
			}
		}

		// Token: 0x06015195 RID: 86421 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015197 RID: 86423 RVA: 0x0031B5AE File Offset: 0x003197AE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowPhantom>(deep);
		}

		// Token: 0x04009183 RID: 37251
		private const string tagName = "show";

		// Token: 0x04009184 RID: 37252
		private const byte tagNsId = 21;

		// Token: 0x04009185 RID: 37253
		internal const int ElementTypeIdConst = 10929;
	}
}
