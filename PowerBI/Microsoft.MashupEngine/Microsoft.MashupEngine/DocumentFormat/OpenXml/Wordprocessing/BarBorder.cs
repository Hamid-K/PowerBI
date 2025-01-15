using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EA3 RID: 11939
	[GeneratedCode("DomGen", "2.0")]
	internal class BarBorder : BorderType
	{
		// Token: 0x17008B84 RID: 35716
		// (get) Token: 0x060195FA RID: 103930 RVA: 0x003196B5 File Offset: 0x003178B5
		public override string LocalName
		{
			get
			{
				return "bar";
			}
		}

		// Token: 0x17008B85 RID: 35717
		// (get) Token: 0x060195FB RID: 103931 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008B86 RID: 35718
		// (get) Token: 0x060195FC RID: 103932 RVA: 0x00349088 File Offset: 0x00347288
		internal override int ElementTypeId
		{
			get
			{
				return 11720;
			}
		}

		// Token: 0x060195FD RID: 103933 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060195FF RID: 103935 RVA: 0x0034908F File Offset: 0x0034728F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BarBorder>(deep);
		}

		// Token: 0x0400A89F RID: 43167
		private const string tagName = "bar";

		// Token: 0x0400A8A0 RID: 43168
		private const byte tagNsId = 23;

		// Token: 0x0400A8A1 RID: 43169
		internal const int ElementTypeIdConst = 11720;
	}
}
