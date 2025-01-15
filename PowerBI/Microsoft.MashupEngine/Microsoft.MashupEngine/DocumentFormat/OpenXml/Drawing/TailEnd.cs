using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027DC RID: 10204
	[GeneratedCode("DomGen", "2.0")]
	internal class TailEnd : LineEndPropertiesType
	{
		// Token: 0x17006402 RID: 25602
		// (get) Token: 0x06013DA2 RID: 81314 RVA: 0x0030C4F6 File Offset: 0x0030A6F6
		public override string LocalName
		{
			get
			{
				return "tailEnd";
			}
		}

		// Token: 0x17006403 RID: 25603
		// (get) Token: 0x06013DA3 RID: 81315 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006404 RID: 25604
		// (get) Token: 0x06013DA4 RID: 81316 RVA: 0x0030C4FD File Offset: 0x0030A6FD
		internal override int ElementTypeId
		{
			get
			{
				return 10236;
			}
		}

		// Token: 0x06013DA5 RID: 81317 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013DA7 RID: 81319 RVA: 0x0030C504 File Offset: 0x0030A704
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TailEnd>(deep);
		}

		// Token: 0x04008814 RID: 34836
		private const string tagName = "tailEnd";

		// Token: 0x04008815 RID: 34837
		private const byte tagNsId = 10;

		// Token: 0x04008816 RID: 34838
		internal const int ElementTypeIdConst = 10236;
	}
}
