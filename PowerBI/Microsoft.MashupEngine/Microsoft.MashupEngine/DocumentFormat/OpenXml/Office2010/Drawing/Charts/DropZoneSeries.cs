using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Charts
{
	// Token: 0x02002320 RID: 8992
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class DropZoneSeries : BooleanFalseType
	{
		// Token: 0x1700485C RID: 18524
		// (get) Token: 0x06010008 RID: 65544 RVA: 0x002DE758 File Offset: 0x002DC958
		public override string LocalName
		{
			get
			{
				return "dropZoneSeries";
			}
		}

		// Token: 0x1700485D RID: 18525
		// (get) Token: 0x06010009 RID: 65545 RVA: 0x002DE0C4 File Offset: 0x002DC2C4
		internal override byte NamespaceId
		{
			get
			{
				return 46;
			}
		}

		// Token: 0x1700485E RID: 18526
		// (get) Token: 0x0601000A RID: 65546 RVA: 0x002DE75F File Offset: 0x002DC95F
		internal override int ElementTypeId
		{
			get
			{
				return 12699;
			}
		}

		// Token: 0x0601000B RID: 65547 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601000D RID: 65549 RVA: 0x002DE766 File Offset: 0x002DC966
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DropZoneSeries>(deep);
		}

		// Token: 0x040072A2 RID: 29346
		private const string tagName = "dropZoneSeries";

		// Token: 0x040072A3 RID: 29347
		private const byte tagNsId = 46;

		// Token: 0x040072A4 RID: 29348
		internal const int ElementTypeIdConst = 12699;
	}
}
