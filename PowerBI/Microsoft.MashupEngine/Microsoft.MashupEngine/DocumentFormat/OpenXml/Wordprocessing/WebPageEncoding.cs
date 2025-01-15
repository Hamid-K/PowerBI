using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F3F RID: 12095
	[GeneratedCode("DomGen", "2.0")]
	internal class WebPageEncoding : String255Type
	{
		// Token: 0x17008FB1 RID: 36785
		// (get) Token: 0x06019F3E RID: 106302 RVA: 0x0035A37B File Offset: 0x0035857B
		public override string LocalName
		{
			get
			{
				return "encoding";
			}
		}

		// Token: 0x17008FB2 RID: 36786
		// (get) Token: 0x06019F3F RID: 106303 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008FB3 RID: 36787
		// (get) Token: 0x06019F40 RID: 106304 RVA: 0x0035A382 File Offset: 0x00358582
		internal override int ElementTypeId
		{
			get
			{
				return 11837;
			}
		}

		// Token: 0x06019F41 RID: 106305 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019F43 RID: 106307 RVA: 0x0035A389 File Offset: 0x00358589
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<WebPageEncoding>(deep);
		}

		// Token: 0x0400AB11 RID: 43793
		private const string tagName = "encoding";

		// Token: 0x0400AB12 RID: 43794
		private const byte tagNsId = 23;

		// Token: 0x0400AB13 RID: 43795
		internal const int ElementTypeIdConst = 11837;
	}
}
