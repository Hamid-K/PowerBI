using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x0200275A RID: 10074
	[GeneratedCode("DomGen", "2.0")]
	internal class TopBorderLineProperties : LinePropertiesType
	{
		// Token: 0x170060C3 RID: 24771
		// (get) Token: 0x0601364A RID: 79434 RVA: 0x003069D2 File Offset: 0x00304BD2
		public override string LocalName
		{
			get
			{
				return "lnT";
			}
		}

		// Token: 0x170060C4 RID: 24772
		// (get) Token: 0x0601364B RID: 79435 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170060C5 RID: 24773
		// (get) Token: 0x0601364C RID: 79436 RVA: 0x003069D9 File Offset: 0x00304BD9
		internal override int ElementTypeId
		{
			get
			{
				return 10254;
			}
		}

		// Token: 0x0601364D RID: 79437 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601364E RID: 79438 RVA: 0x00306961 File Offset: 0x00304B61
		public TopBorderLineProperties()
		{
		}

		// Token: 0x0601364F RID: 79439 RVA: 0x00306969 File Offset: 0x00304B69
		public TopBorderLineProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013650 RID: 79440 RVA: 0x00306972 File Offset: 0x00304B72
		public TopBorderLineProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013651 RID: 79441 RVA: 0x0030697B File Offset: 0x00304B7B
		public TopBorderLineProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013652 RID: 79442 RVA: 0x003069E0 File Offset: 0x00304BE0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TopBorderLineProperties>(deep);
		}

		// Token: 0x04008604 RID: 34308
		private const string tagName = "lnT";

		// Token: 0x04008605 RID: 34309
		private const byte tagNsId = 10;

		// Token: 0x04008606 RID: 34310
		internal const int ElementTypeIdConst = 10254;
	}
}
