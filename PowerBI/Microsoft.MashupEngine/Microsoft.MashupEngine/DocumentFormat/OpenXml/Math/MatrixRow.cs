using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029BF RID: 10687
	[ChildElementInfo(typeof(Base))]
	[GeneratedCode("DomGen", "2.0")]
	internal class MatrixRow : OpenXmlCompositeElement
	{
		// Token: 0x17006DC1 RID: 28097
		// (get) Token: 0x06015458 RID: 87128 RVA: 0x0031D76E File Offset: 0x0031B96E
		public override string LocalName
		{
			get
			{
				return "mr";
			}
		}

		// Token: 0x17006DC2 RID: 28098
		// (get) Token: 0x06015459 RID: 87129 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006DC3 RID: 28099
		// (get) Token: 0x0601545A RID: 87130 RVA: 0x0031D775 File Offset: 0x0031B975
		internal override int ElementTypeId
		{
			get
			{
				return 10922;
			}
		}

		// Token: 0x0601545B RID: 87131 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601545C RID: 87132 RVA: 0x00293ECF File Offset: 0x002920CF
		public MatrixRow()
		{
		}

		// Token: 0x0601545D RID: 87133 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MatrixRow(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601545E RID: 87134 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MatrixRow(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601545F RID: 87135 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MatrixRow(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015460 RID: 87136 RVA: 0x0031D77C File Offset: 0x0031B97C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			return null;
		}

		// Token: 0x06015461 RID: 87137 RVA: 0x0031D797 File Offset: 0x0031B997
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MatrixRow>(deep);
		}

		// Token: 0x0400926F RID: 37487
		private const string tagName = "mr";

		// Token: 0x04009270 RID: 37488
		private const byte tagNsId = 21;

		// Token: 0x04009271 RID: 37489
		internal const int ElementTypeIdConst = 10922;
	}
}
