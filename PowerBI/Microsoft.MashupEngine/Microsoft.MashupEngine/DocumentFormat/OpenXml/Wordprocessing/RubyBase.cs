using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F4E RID: 12110
	[GeneratedCode("DomGen", "2.0")]
	internal class RubyBase : RubyContentType
	{
		// Token: 0x17009010 RID: 36880
		// (get) Token: 0x0601A00A RID: 106506 RVA: 0x0035B0F0 File Offset: 0x003592F0
		public override string LocalName
		{
			get
			{
				return "rubyBase";
			}
		}

		// Token: 0x17009011 RID: 36881
		// (get) Token: 0x0601A00B RID: 106507 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009012 RID: 36882
		// (get) Token: 0x0601A00C RID: 106508 RVA: 0x0035B0F7 File Offset: 0x003592F7
		internal override int ElementTypeId
		{
			get
			{
				return 11760;
			}
		}

		// Token: 0x0601A00D RID: 106509 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A00E RID: 106510 RVA: 0x0035B0C4 File Offset: 0x003592C4
		public RubyBase()
		{
		}

		// Token: 0x0601A00F RID: 106511 RVA: 0x0035B0CC File Offset: 0x003592CC
		public RubyBase(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A010 RID: 106512 RVA: 0x0035B0D5 File Offset: 0x003592D5
		public RubyBase(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A011 RID: 106513 RVA: 0x0035B0DE File Offset: 0x003592DE
		public RubyBase(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A012 RID: 106514 RVA: 0x0035B0FE File Offset: 0x003592FE
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<RubyBase>(deep);
		}

		// Token: 0x0400AB55 RID: 43861
		private const string tagName = "rubyBase";

		// Token: 0x0400AB56 RID: 43862
		private const byte tagNsId = 23;

		// Token: 0x0400AB57 RID: 43863
		internal const int ElementTypeIdConst = 11760;
	}
}
