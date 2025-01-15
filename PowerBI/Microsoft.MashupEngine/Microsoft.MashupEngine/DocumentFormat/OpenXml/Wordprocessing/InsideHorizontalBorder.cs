using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EA6 RID: 11942
	[GeneratedCode("DomGen", "2.0")]
	internal class InsideHorizontalBorder : BorderType
	{
		// Token: 0x17008B8D RID: 35725
		// (get) Token: 0x0601960C RID: 103948 RVA: 0x0030E404 File Offset: 0x0030C604
		public override string LocalName
		{
			get
			{
				return "insideH";
			}
		}

		// Token: 0x17008B8E RID: 35726
		// (get) Token: 0x0601960D RID: 103949 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B8F RID: 35727
		// (get) Token: 0x0601960E RID: 103950 RVA: 0x003490B8 File Offset: 0x003472B8
		internal override int ElementTypeId
		{
			get
			{
				return 12123;
			}
		}

		// Token: 0x0601960F RID: 103951 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019611 RID: 103953 RVA: 0x003490BF File Offset: 0x003472BF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<InsideHorizontalBorder>(deep);
		}

		// Token: 0x0400A8A8 RID: 43176
		private const string tagName = "insideH";

		// Token: 0x0400A8A9 RID: 43177
		private const byte tagNsId = 23;

		// Token: 0x0400A8AA RID: 43178
		internal const int ElementTypeIdConst = 12123;
	}
}
