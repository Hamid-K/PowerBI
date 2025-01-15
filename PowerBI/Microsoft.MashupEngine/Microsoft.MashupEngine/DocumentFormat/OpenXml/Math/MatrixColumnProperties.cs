using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029B2 RID: 10674
	[ChildElementInfo(typeof(MatrixColumnCount))]
	[ChildElementInfo(typeof(MatrixColumnJustification))]
	[GeneratedCode("DomGen", "2.0")]
	internal class MatrixColumnProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006D85 RID: 28037
		// (get) Token: 0x060153D5 RID: 86997 RVA: 0x0031D2D8 File Offset: 0x0031B4D8
		public override string LocalName
		{
			get
			{
				return "mcPr";
			}
		}

		// Token: 0x17006D86 RID: 28038
		// (get) Token: 0x060153D6 RID: 86998 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D87 RID: 28039
		// (get) Token: 0x060153D7 RID: 86999 RVA: 0x0031D2DF File Offset: 0x0031B4DF
		internal override int ElementTypeId
		{
			get
			{
				return 10914;
			}
		}

		// Token: 0x060153D8 RID: 87000 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060153D9 RID: 87001 RVA: 0x00293ECF File Offset: 0x002920CF
		public MatrixColumnProperties()
		{
		}

		// Token: 0x060153DA RID: 87002 RVA: 0x00293ED7 File Offset: 0x002920D7
		public MatrixColumnProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060153DB RID: 87003 RVA: 0x00293EE0 File Offset: 0x002920E0
		public MatrixColumnProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060153DC RID: 87004 RVA: 0x00293EE9 File Offset: 0x002920E9
		public MatrixColumnProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060153DD RID: 87005 RVA: 0x0031D2E6 File Offset: 0x0031B4E6
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "count" == name)
			{
				return new MatrixColumnCount();
			}
			if (21 == namespaceId && "mcJc" == name)
			{
				return new MatrixColumnJustification();
			}
			return null;
		}

		// Token: 0x17006D88 RID: 28040
		// (get) Token: 0x060153DE RID: 87006 RVA: 0x0031D319 File Offset: 0x0031B519
		internal override string[] ElementTagNames
		{
			get
			{
				return MatrixColumnProperties.eleTagNames;
			}
		}

		// Token: 0x17006D89 RID: 28041
		// (get) Token: 0x060153DF RID: 87007 RVA: 0x0031D320 File Offset: 0x0031B520
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return MatrixColumnProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006D8A RID: 28042
		// (get) Token: 0x060153E0 RID: 87008 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006D8B RID: 28043
		// (get) Token: 0x060153E1 RID: 87009 RVA: 0x0031D327 File Offset: 0x0031B527
		// (set) Token: 0x060153E2 RID: 87010 RVA: 0x0031D330 File Offset: 0x0031B530
		public MatrixColumnCount MatrixColumnCount
		{
			get
			{
				return base.GetElement<MatrixColumnCount>(0);
			}
			set
			{
				base.SetElement<MatrixColumnCount>(0, value);
			}
		}

		// Token: 0x17006D8C RID: 28044
		// (get) Token: 0x060153E3 RID: 87011 RVA: 0x0031D33A File Offset: 0x0031B53A
		// (set) Token: 0x060153E4 RID: 87012 RVA: 0x0031D343 File Offset: 0x0031B543
		public MatrixColumnJustification MatrixColumnJustification
		{
			get
			{
				return base.GetElement<MatrixColumnJustification>(1);
			}
			set
			{
				base.SetElement<MatrixColumnJustification>(1, value);
			}
		}

		// Token: 0x060153E5 RID: 87013 RVA: 0x0031D34D File Offset: 0x0031B54D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<MatrixColumnProperties>(deep);
		}

		// Token: 0x04009243 RID: 37443
		private const string tagName = "mcPr";

		// Token: 0x04009244 RID: 37444
		private const byte tagNsId = 21;

		// Token: 0x04009245 RID: 37445
		internal const int ElementTypeIdConst = 10914;

		// Token: 0x04009246 RID: 37446
		private static readonly string[] eleTagNames = new string[] { "count", "mcJc" };

		// Token: 0x04009247 RID: 37447
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21 };
	}
}
