using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E9E RID: 11934
	[GeneratedCode("DomGen", "2.0")]
	internal class TopBorder : BorderType
	{
		// Token: 0x17008B75 RID: 35701
		// (get) Token: 0x060195DC RID: 103900 RVA: 0x002BF37F File Offset: 0x002BD57F
		public override string LocalName
		{
			get
			{
				return "top";
			}
		}

		// Token: 0x17008B76 RID: 35702
		// (get) Token: 0x060195DD RID: 103901 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B77 RID: 35703
		// (get) Token: 0x060195DE RID: 103902 RVA: 0x00349031 File Offset: 0x00347231
		internal override int ElementTypeId
		{
			get
			{
				return 11715;
			}
		}

		// Token: 0x060195DF RID: 103903 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060195E1 RID: 103905 RVA: 0x00349038 File Offset: 0x00347238
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TopBorder>(deep);
		}

		// Token: 0x0400A890 RID: 43152
		private const string tagName = "top";

		// Token: 0x0400A891 RID: 43153
		private const byte tagNsId = 23;

		// Token: 0x0400A892 RID: 43154
		internal const int ElementTypeIdConst = 11715;
	}
}
