using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D96 RID: 11670
	[GeneratedCode("DomGen", "2.0")]
	internal class AutomaticallySizeFormField : OnOffType
	{
		// Token: 0x1700875E RID: 34654
		// (get) Token: 0x06018D7C RID: 101756 RVA: 0x00344D95 File Offset: 0x00342F95
		public override string LocalName
		{
			get
			{
				return "sizeAuto";
			}
		}

		// Token: 0x1700875F RID: 34655
		// (get) Token: 0x06018D7D RID: 101757 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008760 RID: 34656
		// (get) Token: 0x06018D7E RID: 101758 RVA: 0x00344D9C File Offset: 0x00342F9C
		internal override int ElementTypeId
		{
			get
			{
				return 11737;
			}
		}

		// Token: 0x06018D7F RID: 101759 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018D81 RID: 101761 RVA: 0x00344DA3 File Offset: 0x00342FA3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutomaticallySizeFormField>(deep);
		}

		// Token: 0x0400A52D RID: 42285
		private const string tagName = "sizeAuto";

		// Token: 0x0400A52E RID: 42286
		private const byte tagNsId = 23;

		// Token: 0x0400A52F RID: 42287
		internal const int ElementTypeIdConst = 11737;
	}
}
