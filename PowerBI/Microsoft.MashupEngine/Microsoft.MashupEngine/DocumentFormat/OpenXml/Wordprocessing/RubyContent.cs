using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F4D RID: 12109
	[GeneratedCode("DomGen", "2.0")]
	internal class RubyContent : RubyContentType
	{
		// Token: 0x1700900D RID: 36877
		// (get) Token: 0x0601A001 RID: 106497 RVA: 0x0035B0B6 File Offset: 0x003592B6
		public override string LocalName
		{
			get
			{
				return "rt";
			}
		}

		// Token: 0x1700900E RID: 36878
		// (get) Token: 0x0601A002 RID: 106498 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700900F RID: 36879
		// (get) Token: 0x0601A003 RID: 106499 RVA: 0x0035B0BD File Offset: 0x003592BD
		internal override int ElementTypeId
		{
			get
			{
				return 11759;
			}
		}

		// Token: 0x0601A004 RID: 106500 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A005 RID: 106501 RVA: 0x0035B0C4 File Offset: 0x003592C4
		public RubyContent()
		{
		}

		// Token: 0x0601A006 RID: 106502 RVA: 0x0035B0CC File Offset: 0x003592CC
		public RubyContent(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A007 RID: 106503 RVA: 0x0035B0D5 File Offset: 0x003592D5
		public RubyContent(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A008 RID: 106504 RVA: 0x0035B0DE File Offset: 0x003592DE
		public RubyContent(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A009 RID: 106505 RVA: 0x0035B0E7 File Offset: 0x003592E7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RubyContent>(deep);
		}

		// Token: 0x0400AB52 RID: 43858
		private const string tagName = "rt";

		// Token: 0x0400AB53 RID: 43859
		private const byte tagNsId = 23;

		// Token: 0x0400AB54 RID: 43860
		internal const int ElementTypeIdConst = 11759;
	}
}
