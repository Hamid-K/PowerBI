using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002978 RID: 10616
	[GeneratedCode("DomGen", "2.0")]
	internal class MaxDistribution : OnOffType
	{
		// Token: 0x17006C73 RID: 27763
		// (get) Token: 0x06015174 RID: 86388 RVA: 0x0031B52D File Offset: 0x0031972D
		public override string LocalName
		{
			get
			{
				return "maxDist";
			}
		}

		// Token: 0x17006C74 RID: 27764
		// (get) Token: 0x06015175 RID: 86389 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C75 RID: 27765
		// (get) Token: 0x06015176 RID: 86390 RVA: 0x0031B534 File Offset: 0x00319734
		internal override int ElementTypeId
		{
			get
			{
				return 10896;
			}
		}

		// Token: 0x06015177 RID: 86391 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015179 RID: 86393 RVA: 0x0031B53B File Offset: 0x0031973B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MaxDistribution>(deep);
		}

		// Token: 0x04009174 RID: 37236
		private const string tagName = "maxDist";

		// Token: 0x04009175 RID: 37237
		private const byte tagNsId = 21;

		// Token: 0x04009176 RID: 37238
		internal const int ElementTypeIdConst = 10896;
	}
}
