using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200250B RID: 9483
	[GeneratedCode("DomGen", "2.0")]
	internal class ShowValue : BooleanType
	{
		// Token: 0x1700543A RID: 21562
		// (get) Token: 0x06011A6F RID: 72303 RVA: 0x002F1226 File Offset: 0x002EF426
		public override string LocalName
		{
			get
			{
				return "showVal";
			}
		}

		// Token: 0x1700543B RID: 21563
		// (get) Token: 0x06011A70 RID: 72304 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700543C RID: 21564
		// (get) Token: 0x06011A71 RID: 72305 RVA: 0x002F122D File Offset: 0x002EF42D
		internal override int ElementTypeId
		{
			get
			{
				return 10347;
			}
		}

		// Token: 0x06011A72 RID: 72306 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011A74 RID: 72308 RVA: 0x002F1234 File Offset: 0x002EF434
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShowValue>(deep);
		}

		// Token: 0x04007BB5 RID: 31669
		private const string tagName = "showVal";

		// Token: 0x04007BB6 RID: 31670
		private const byte tagNsId = 11;

		// Token: 0x04007BB7 RID: 31671
		internal const int ElementTypeIdConst = 10347;
	}
}
