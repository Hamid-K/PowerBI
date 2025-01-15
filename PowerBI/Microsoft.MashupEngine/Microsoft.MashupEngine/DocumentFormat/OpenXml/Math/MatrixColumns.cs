using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029BD RID: 10685
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MatrixColumn))]
	internal class MatrixColumns : OpenXmlCompositeElement
	{
		// Token: 0x17006DAF RID: 28079
		// (get) Token: 0x0601542E RID: 87086 RVA: 0x0031D544 File Offset: 0x0031B744
		public override string LocalName
		{
			get
			{
				return "mcs";
			}
		}

		// Token: 0x17006DB0 RID: 28080
		// (get) Token: 0x0601542F RID: 87087 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006DB1 RID: 28081
		// (get) Token: 0x06015430 RID: 87088 RVA: 0x0031D54B File Offset: 0x0031B74B
		internal override int ElementTypeId
		{
			get
			{
				return 10920;
			}
		}

		// Token: 0x06015431 RID: 87089 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015432 RID: 87090 RVA: 0x00293ECF File Offset: 0x002920CF
		public MatrixColumns()
		{
		}

		// Token: 0x06015433 RID: 87091 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MatrixColumns(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015434 RID: 87092 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MatrixColumns(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015435 RID: 87093 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MatrixColumns(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015436 RID: 87094 RVA: 0x0031D552 File Offset: 0x0031B752
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "mc" == name)
			{
				return new MatrixColumn();
			}
			return null;
		}

		// Token: 0x06015437 RID: 87095 RVA: 0x0031D56D File Offset: 0x0031B76D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MatrixColumns>(deep);
		}

		// Token: 0x04009267 RID: 37479
		private const string tagName = "mcs";

		// Token: 0x04009268 RID: 37480
		private const byte tagNsId = 21;

		// Token: 0x04009269 RID: 37481
		internal const int ElementTypeIdConst = 10920;
	}
}
