using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200252D RID: 9517
	[GeneratedCode("DomGen", "2.0")]
	internal class UserInterface : BooleanType
	{
		// Token: 0x170054A0 RID: 21664
		// (get) Token: 0x06011B3B RID: 72507 RVA: 0x002F1526 File Offset: 0x002EF726
		public override string LocalName
		{
			get
			{
				return "userInterface";
			}
		}

		// Token: 0x170054A1 RID: 21665
		// (get) Token: 0x06011B3C RID: 72508 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170054A2 RID: 21666
		// (get) Token: 0x06011B3D RID: 72509 RVA: 0x002F152D File Offset: 0x002EF72D
		internal override int ElementTypeId
		{
			get
			{
				return 10509;
			}
		}

		// Token: 0x06011B3E RID: 72510 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011B40 RID: 72512 RVA: 0x002F1534 File Offset: 0x002EF734
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<UserInterface>(deep);
		}

		// Token: 0x04007C1B RID: 31771
		private const string tagName = "userInterface";

		// Token: 0x04007C1C RID: 31772
		private const byte tagNsId = 11;

		// Token: 0x04007C1D RID: 31773
		internal const int ElementTypeIdConst = 10509;
	}
}
