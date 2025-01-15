using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FF2 RID: 12274
	[GeneratedCode("DomGen", "2.0")]
	internal class ShapeDefaults : ShapeDefaultsType
	{
		// Token: 0x17009542 RID: 38210
		// (get) Token: 0x0601AB2B RID: 109355 RVA: 0x00366032 File Offset: 0x00364232
		public override string LocalName
		{
			get
			{
				return "shapeDefaults";
			}
		}

		// Token: 0x17009543 RID: 38211
		// (get) Token: 0x0601AB2C RID: 109356 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009544 RID: 38212
		// (get) Token: 0x0601AB2D RID: 109357 RVA: 0x00366039 File Offset: 0x00364239
		internal override int ElementTypeId
		{
			get
			{
				return 12051;
			}
		}

		// Token: 0x0601AB2E RID: 109358 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601AB2F RID: 109359 RVA: 0x00366006 File Offset: 0x00364206
		public ShapeDefaults()
		{
		}

		// Token: 0x0601AB30 RID: 109360 RVA: 0x0036600E File Offset: 0x0036420E
		public ShapeDefaults(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AB31 RID: 109361 RVA: 0x00366017 File Offset: 0x00364217
		public ShapeDefaults(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AB32 RID: 109362 RVA: 0x00366020 File Offset: 0x00364220
		public ShapeDefaults(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AB33 RID: 109363 RVA: 0x00366040 File Offset: 0x00364240
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeDefaults>(deep);
		}

		// Token: 0x0400AE14 RID: 44564
		private const string tagName = "shapeDefaults";

		// Token: 0x0400AE15 RID: 44565
		private const byte tagNsId = 23;

		// Token: 0x0400AE16 RID: 44566
		internal const int ElementTypeIdConst = 12051;
	}
}
