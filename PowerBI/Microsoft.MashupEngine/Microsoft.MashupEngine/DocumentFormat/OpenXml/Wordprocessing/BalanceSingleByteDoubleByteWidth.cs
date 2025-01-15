using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002DE7 RID: 11751
	[GeneratedCode("DomGen", "2.0")]
	internal class BalanceSingleByteDoubleByteWidth : OnOffType
	{
		// Token: 0x17008851 RID: 34897
		// (get) Token: 0x06018F62 RID: 102242 RVA: 0x003454CE File Offset: 0x003436CE
		public override string LocalName
		{
			get
			{
				return "balanceSingleByteDoubleByteWidth";
			}
		}

		// Token: 0x17008852 RID: 34898
		// (get) Token: 0x06018F63 RID: 102243 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008853 RID: 34899
		// (get) Token: 0x06018F64 RID: 102244 RVA: 0x003454D5 File Offset: 0x003436D5
		internal override int ElementTypeId
		{
			get
			{
				return 12061;
			}
		}

		// Token: 0x06018F65 RID: 102245 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018F67 RID: 102247 RVA: 0x003454DC File Offset: 0x003436DC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BalanceSingleByteDoubleByteWidth>(deep);
		}

		// Token: 0x0400A620 RID: 42528
		private const string tagName = "balanceSingleByteDoubleByteWidth";

		// Token: 0x0400A621 RID: 42529
		private const byte tagNsId = 23;

		// Token: 0x0400A622 RID: 42530
		internal const int ElementTypeIdConst = 12061;
	}
}
