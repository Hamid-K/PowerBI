using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002D64 RID: 11620
	[GeneratedCode("DomGen", "2.0")]
	internal class AttachedSchema : StringType
	{
		// Token: 0x170086C8 RID: 34504
		// (get) Token: 0x06018C4F RID: 101455 RVA: 0x00344906 File Offset: 0x00342B06
		public override string LocalName
		{
			get
			{
				return "attachedSchema";
			}
		}

		// Token: 0x170086C9 RID: 34505
		// (get) Token: 0x06018C50 RID: 101456 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170086CA RID: 34506
		// (get) Token: 0x06018C51 RID: 101457 RVA: 0x0034490D File Offset: 0x00342B0D
		internal override int ElementTypeId
		{
			get
			{
				return 12042;
			}
		}

		// Token: 0x06018C52 RID: 101458 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06018C54 RID: 101460 RVA: 0x00344914 File Offset: 0x00342B14
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AttachedSchema>(deep);
		}

		// Token: 0x0400A498 RID: 42136
		private const string tagName = "attachedSchema";

		// Token: 0x0400A499 RID: 42137
		private const byte tagNsId = 23;

		// Token: 0x0400A49A RID: 42138
		internal const int ElementTypeIdConst = 12042;
	}
}
