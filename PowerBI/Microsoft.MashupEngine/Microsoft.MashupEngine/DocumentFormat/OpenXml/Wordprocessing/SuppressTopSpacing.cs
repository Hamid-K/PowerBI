using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DF4 RID: 11764
	[GeneratedCode("DomGen", "2.0")]
	internal class SuppressTopSpacing : OnOffType
	{
		// Token: 0x17008878 RID: 34936
		// (get) Token: 0x06018FB0 RID: 102320 RVA: 0x003455F9 File Offset: 0x003437F9
		public override string LocalName
		{
			get
			{
				return "suppressTopSpacing";
			}
		}

		// Token: 0x17008879 RID: 34937
		// (get) Token: 0x06018FB1 RID: 102321 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700887A RID: 34938
		// (get) Token: 0x06018FB2 RID: 102322 RVA: 0x00345600 File Offset: 0x00343800
		internal override int ElementTypeId
		{
			get
			{
				return 12074;
			}
		}

		// Token: 0x06018FB3 RID: 102323 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018FB5 RID: 102325 RVA: 0x00345607 File Offset: 0x00343807
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SuppressTopSpacing>(deep);
		}

		// Token: 0x0400A647 RID: 42567
		private const string tagName = "suppressTopSpacing";

		// Token: 0x0400A648 RID: 42568
		private const byte tagNsId = 23;

		// Token: 0x0400A649 RID: 42569
		internal const int ElementTypeIdConst = 12074;
	}
}
