using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing.ChartDrawing
{
	// Token: 0x0200233C RID: 9020
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualContentPartProperties : ContentPartNonVisualType
	{
		// Token: 0x17004933 RID: 18739
		// (get) Token: 0x060101E8 RID: 66024 RVA: 0x002DFF82 File Offset: 0x002DE182
		public override string LocalName
		{
			get
			{
				return "nvContentPartPr";
			}
		}

		// Token: 0x17004934 RID: 18740
		// (get) Token: 0x060101E9 RID: 66025 RVA: 0x002DF9A4 File Offset: 0x002DDBA4
		internal override byte NamespaceId
		{
			get
			{
				return 47;
			}
		}

		// Token: 0x17004935 RID: 18741
		// (get) Token: 0x060101EA RID: 66026 RVA: 0x002DFF89 File Offset: 0x002DE189
		internal override int ElementTypeId
		{
			get
			{
				return 12708;
			}
		}

		// Token: 0x060101EB RID: 66027 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060101EC RID: 66028 RVA: 0x002DFF56 File Offset: 0x002DE156
		public NonVisualContentPartProperties()
		{
		}

		// Token: 0x060101ED RID: 66029 RVA: 0x002DFF5E File Offset: 0x002DE15E
		public NonVisualContentPartProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060101EE RID: 66030 RVA: 0x002DFF67 File Offset: 0x002DE167
		public NonVisualContentPartProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060101EF RID: 66031 RVA: 0x002DFF70 File Offset: 0x002DE170
		public NonVisualContentPartProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060101F0 RID: 66032 RVA: 0x002DFF90 File Offset: 0x002DE190
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualContentPartProperties>(deep);
		}

		// Token: 0x04007325 RID: 29477
		private const string tagName = "nvContentPartPr";

		// Token: 0x04007326 RID: 29478
		private const byte tagNsId = 47;

		// Token: 0x04007327 RID: 29479
		internal const int ElementTypeIdConst = 12708;
	}
}
