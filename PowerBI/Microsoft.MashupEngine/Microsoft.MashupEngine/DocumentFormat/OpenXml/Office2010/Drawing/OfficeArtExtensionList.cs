using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002350 RID: 9040
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Extension))]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class OfficeArtExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x170049D8 RID: 18904
		// (get) Token: 0x06010349 RID: 66377 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x170049D9 RID: 18905
		// (get) Token: 0x0601034A RID: 66378 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x170049DA RID: 18906
		// (get) Token: 0x0601034B RID: 66379 RVA: 0x002E0FE6 File Offset: 0x002DF1E6
		internal override int ElementTypeId
		{
			get
			{
				return 12725;
			}
		}

		// Token: 0x0601034C RID: 66380 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0601034D RID: 66381 RVA: 0x00293ECF File Offset: 0x002920CF
		public OfficeArtExtensionList()
		{
		}

		// Token: 0x0601034E RID: 66382 RVA: 0x00293ED7 File Offset: 0x002920D7
		public OfficeArtExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601034F RID: 66383 RVA: 0x00293EE0 File Offset: 0x002920E0
		public OfficeArtExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010350 RID: 66384 RVA: 0x00293EE9 File Offset: 0x002920E9
		public OfficeArtExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010351 RID: 66385 RVA: 0x002DF2C5 File Offset: 0x002DD4C5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "ext" == name)
			{
				return new Extension();
			}
			return null;
		}

		// Token: 0x06010352 RID: 66386 RVA: 0x002E0FED File Offset: 0x002DF1ED
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<OfficeArtExtensionList>(deep);
		}

		// Token: 0x0400738B RID: 29579
		private const string tagName = "extLst";

		// Token: 0x0400738C RID: 29580
		private const byte tagNsId = 48;

		// Token: 0x0400738D RID: 29581
		internal const int ElementTypeIdConst = 12725;
	}
}
