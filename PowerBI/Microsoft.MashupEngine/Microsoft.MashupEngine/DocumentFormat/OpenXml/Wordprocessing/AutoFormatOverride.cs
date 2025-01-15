using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DC4 RID: 11716
	[GeneratedCode("DomGen", "2.0")]
	internal class AutoFormatOverride : OnOffType
	{
		// Token: 0x170087E8 RID: 34792
		// (get) Token: 0x06018E90 RID: 102032 RVA: 0x003451A9 File Offset: 0x003433A9
		public override string LocalName
		{
			get
			{
				return "autoFormatOverride";
			}
		}

		// Token: 0x170087E9 RID: 34793
		// (get) Token: 0x06018E91 RID: 102033 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170087EA RID: 34794
		// (get) Token: 0x06018E92 RID: 102034 RVA: 0x003451B0 File Offset: 0x003433B0
		internal override int ElementTypeId
		{
			get
			{
				return 11993;
			}
		}

		// Token: 0x06018E93 RID: 102035 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018E95 RID: 102037 RVA: 0x003451B7 File Offset: 0x003433B7
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AutoFormatOverride>(deep);
		}

		// Token: 0x0400A5B7 RID: 42423
		private const string tagName = "autoFormatOverride";

		// Token: 0x0400A5B8 RID: 42424
		private const byte tagNsId = 23;

		// Token: 0x0400A5B9 RID: 42425
		internal const int ElementTypeIdConst = 11993;
	}
}
