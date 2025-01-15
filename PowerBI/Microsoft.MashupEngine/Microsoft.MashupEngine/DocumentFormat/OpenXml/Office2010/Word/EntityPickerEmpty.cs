using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x0200249E RID: 9374
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class EntityPickerEmpty : EmptyType
	{
		// Token: 0x170051BD RID: 20925
		// (get) Token: 0x060114EF RID: 70895 RVA: 0x002ECFE4 File Offset: 0x002EB1E4
		public override string LocalName
		{
			get
			{
				return "entityPicker";
			}
		}

		// Token: 0x170051BE RID: 20926
		// (get) Token: 0x060114F0 RID: 70896 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170051BF RID: 20927
		// (get) Token: 0x060114F1 RID: 70897 RVA: 0x002ECFEB File Offset: 0x002EB1EB
		internal override int ElementTypeId
		{
			get
			{
				return 12874;
			}
		}

		// Token: 0x060114F2 RID: 70898 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060114F4 RID: 70900 RVA: 0x002ECFF2 File Offset: 0x002EB1F2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<EntityPickerEmpty>(deep);
		}

		// Token: 0x0400793A RID: 31034
		private const string tagName = "entityPicker";

		// Token: 0x0400793B RID: 31035
		private const byte tagNsId = 52;

		// Token: 0x0400793C RID: 31036
		internal const int ElementTypeIdConst = 12874;
	}
}
