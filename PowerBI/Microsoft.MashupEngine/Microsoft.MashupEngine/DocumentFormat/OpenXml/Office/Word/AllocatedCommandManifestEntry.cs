using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002465 RID: 9317
	[GeneratedCode("DomGen", "2.0")]
	internal class AllocatedCommandManifestEntry : AcceleratorKeymapType
	{
		// Token: 0x170050C3 RID: 20675
		// (get) Token: 0x060112AD RID: 70317 RVA: 0x002EB383 File Offset: 0x002E9583
		public override string LocalName
		{
			get
			{
				return "acdEntry";
			}
		}

		// Token: 0x170050C4 RID: 20676
		// (get) Token: 0x060112AE RID: 70318 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x170050C5 RID: 20677
		// (get) Token: 0x060112AF RID: 70319 RVA: 0x002EB38A File Offset: 0x002E958A
		internal override int ElementTypeId
		{
			get
			{
				return 12563;
			}
		}

		// Token: 0x060112B0 RID: 70320 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060112B2 RID: 70322 RVA: 0x002EB391 File Offset: 0x002E9591
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AllocatedCommandManifestEntry>(deep);
		}

		// Token: 0x04007879 RID: 30841
		private const string tagName = "acdEntry";

		// Token: 0x0400787A RID: 30842
		private const byte tagNsId = 33;

		// Token: 0x0400787B RID: 30843
		internal const int ElementTypeIdConst = 12563;
	}
}
