using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DF3 RID: 11763
	[GeneratedCode("DomGen", "2.0")]
	internal class SuppressBottomSpacing : OnOffType
	{
		// Token: 0x17008875 RID: 34933
		// (get) Token: 0x06018FAA RID: 102314 RVA: 0x003455E2 File Offset: 0x003437E2
		public override string LocalName
		{
			get
			{
				return "suppressBottomSpacing";
			}
		}

		// Token: 0x17008876 RID: 34934
		// (get) Token: 0x06018FAB RID: 102315 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008877 RID: 34935
		// (get) Token: 0x06018FAC RID: 102316 RVA: 0x003455E9 File Offset: 0x003437E9
		internal override int ElementTypeId
		{
			get
			{
				return 12073;
			}
		}

		// Token: 0x06018FAD RID: 102317 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018FAF RID: 102319 RVA: 0x003455F0 File Offset: 0x003437F0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SuppressBottomSpacing>(deep);
		}

		// Token: 0x0400A644 RID: 42564
		private const string tagName = "suppressBottomSpacing";

		// Token: 0x0400A645 RID: 42565
		private const byte tagNsId = 23;

		// Token: 0x0400A646 RID: 42566
		internal const int ElementTypeIdConst = 12073;
	}
}
