using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029B3 RID: 10675
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MatrixColumnProperties))]
	internal class MatrixColumn : OpenXmlCompositeElement
	{
		// Token: 0x17006D8D RID: 28045
		// (get) Token: 0x060153E7 RID: 87015 RVA: 0x0031D399 File Offset: 0x0031B599
		public override string LocalName
		{
			get
			{
				return "mc";
			}
		}

		// Token: 0x17006D8E RID: 28046
		// (get) Token: 0x060153E8 RID: 87016 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D8F RID: 28047
		// (get) Token: 0x060153E9 RID: 87017 RVA: 0x0031D3A0 File Offset: 0x0031B5A0
		internal override int ElementTypeId
		{
			get
			{
				return 10915;
			}
		}

		// Token: 0x060153EA RID: 87018 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060153EB RID: 87019 RVA: 0x00293ECF File Offset: 0x002920CF
		public MatrixColumn()
		{
		}

		// Token: 0x060153EC RID: 87020 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MatrixColumn(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060153ED RID: 87021 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MatrixColumn(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060153EE RID: 87022 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MatrixColumn(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060153EF RID: 87023 RVA: 0x0031D3A7 File Offset: 0x0031B5A7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "mcPr" == name)
			{
				return new MatrixColumnProperties();
			}
			return null;
		}

		// Token: 0x17006D90 RID: 28048
		// (get) Token: 0x060153F0 RID: 87024 RVA: 0x0031D3C2 File Offset: 0x0031B5C2
		internal override string[] ElementTagNames
		{
			get
			{
				return MatrixColumn.eleTagNames;
			}
		}

		// Token: 0x17006D91 RID: 28049
		// (get) Token: 0x060153F1 RID: 87025 RVA: 0x0031D3C9 File Offset: 0x0031B5C9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return MatrixColumn.eleNamespaceIds;
			}
		}

		// Token: 0x17006D92 RID: 28050
		// (get) Token: 0x060153F2 RID: 87026 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006D93 RID: 28051
		// (get) Token: 0x060153F3 RID: 87027 RVA: 0x0031D3D0 File Offset: 0x0031B5D0
		// (set) Token: 0x060153F4 RID: 87028 RVA: 0x0031D3D9 File Offset: 0x0031B5D9
		public MatrixColumnProperties MatrixColumnProperties
		{
			get
			{
				return base.GetElement<MatrixColumnProperties>(0);
			}
			set
			{
				base.SetElement<MatrixColumnProperties>(0, value);
			}
		}

		// Token: 0x060153F5 RID: 87029 RVA: 0x0031D3E3 File Offset: 0x0031B5E3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MatrixColumn>(deep);
		}

		// Token: 0x04009248 RID: 37448
		private const string tagName = "mc";

		// Token: 0x04009249 RID: 37449
		private const byte tagNsId = 21;

		// Token: 0x0400924A RID: 37450
		internal const int ElementTypeIdConst = 10915;

		// Token: 0x0400924B RID: 37451
		private static readonly string[] eleTagNames = new string[] { "mcPr" };

		// Token: 0x0400924C RID: 37452
		private static readonly byte[] eleNamespaceIds = new byte[] { 21 };
	}
}
