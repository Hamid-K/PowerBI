using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F83 RID: 12163
	[GeneratedCode("DomGen", "2.0")]
	internal class Divs : DivsType
	{
		// Token: 0x17009198 RID: 37272
		// (get) Token: 0x0601A363 RID: 107363 RVA: 0x0035F2BF File Offset: 0x0035D4BF
		public override string LocalName
		{
			get
			{
				return "divs";
			}
		}

		// Token: 0x17009199 RID: 37273
		// (get) Token: 0x0601A364 RID: 107364 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700919A RID: 37274
		// (get) Token: 0x0601A365 RID: 107365 RVA: 0x0035F2C6 File Offset: 0x0035D4C6
		internal override int ElementTypeId
		{
			get
			{
				return 11836;
			}
		}

		// Token: 0x0601A366 RID: 107366 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A367 RID: 107367 RVA: 0x0035F2CD File Offset: 0x0035D4CD
		public Divs()
		{
		}

		// Token: 0x0601A368 RID: 107368 RVA: 0x0035F2D5 File Offset: 0x0035D4D5
		public Divs(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A369 RID: 107369 RVA: 0x0035F2DE File Offset: 0x0035D4DE
		public Divs(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A36A RID: 107370 RVA: 0x0035F2E7 File Offset: 0x0035D4E7
		public Divs(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A36B RID: 107371 RVA: 0x0035F2F0 File Offset: 0x0035D4F0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Divs>(deep);
		}

		// Token: 0x0400AC34 RID: 44084
		private const string tagName = "divs";

		// Token: 0x0400AC35 RID: 44085
		private const byte tagNsId = 23;

		// Token: 0x0400AC36 RID: 44086
		internal const int ElementTypeIdConst = 11836;
	}
}
