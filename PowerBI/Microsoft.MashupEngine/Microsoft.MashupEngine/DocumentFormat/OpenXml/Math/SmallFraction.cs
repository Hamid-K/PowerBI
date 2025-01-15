using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x02002984 RID: 10628
	[GeneratedCode("DomGen", "2.0")]
	internal class SmallFraction : OnOffType
	{
		// Token: 0x17006C97 RID: 27799
		// (get) Token: 0x060151BC RID: 86460 RVA: 0x0031B641 File Offset: 0x00319841
		public override string LocalName
		{
			get
			{
				return "smallFrac";
			}
		}

		// Token: 0x17006C98 RID: 27800
		// (get) Token: 0x060151BD RID: 86461 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006C99 RID: 27801
		// (get) Token: 0x060151BE RID: 86462 RVA: 0x0031B648 File Offset: 0x00319848
		internal override int ElementTypeId
		{
			get
			{
				return 10949;
			}
		}

		// Token: 0x060151BF RID: 86463 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060151C1 RID: 86465 RVA: 0x0031B64F File Offset: 0x0031984F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SmallFraction>(deep);
		}

		// Token: 0x04009198 RID: 37272
		private const string tagName = "smallFrac";

		// Token: 0x04009199 RID: 37273
		private const byte tagNsId = 21;

		// Token: 0x0400919A RID: 37274
		internal const int ElementTypeIdConst = 10949;
	}
}
