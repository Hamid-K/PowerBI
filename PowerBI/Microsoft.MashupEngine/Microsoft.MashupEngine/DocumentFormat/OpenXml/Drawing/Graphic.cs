using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002763 RID: 10083
	[ChildElementInfo(typeof(GraphicData))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Graphic : OpenXmlCompositeElement
	{
		// Token: 0x170060FA RID: 24826
		// (get) Token: 0x060136CB RID: 79563 RVA: 0x00306DF6 File Offset: 0x00304FF6
		public override string LocalName
		{
			get
			{
				return "graphic";
			}
		}

		// Token: 0x170060FB RID: 24827
		// (get) Token: 0x060136CC RID: 79564 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170060FC RID: 24828
		// (get) Token: 0x060136CD RID: 79565 RVA: 0x00306DFD File Offset: 0x00304FFD
		internal override int ElementTypeId
		{
			get
			{
				return 10120;
			}
		}

		// Token: 0x060136CE RID: 79566 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060136CF RID: 79567 RVA: 0x00293ECF File Offset: 0x002920CF
		public Graphic()
		{
		}

		// Token: 0x060136D0 RID: 79568 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Graphic(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060136D1 RID: 79569 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Graphic(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060136D2 RID: 79570 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Graphic(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060136D3 RID: 79571 RVA: 0x00306E04 File Offset: 0x00305004
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "graphicData" == name)
			{
				return new GraphicData();
			}
			return null;
		}

		// Token: 0x170060FD RID: 24829
		// (get) Token: 0x060136D4 RID: 79572 RVA: 0x00306E1F File Offset: 0x0030501F
		internal override string[] ElementTagNames
		{
			get
			{
				return Graphic.eleTagNames;
			}
		}

		// Token: 0x170060FE RID: 24830
		// (get) Token: 0x060136D5 RID: 79573 RVA: 0x00306E26 File Offset: 0x00305026
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Graphic.eleNamespaceIds;
			}
		}

		// Token: 0x170060FF RID: 24831
		// (get) Token: 0x060136D6 RID: 79574 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006100 RID: 24832
		// (get) Token: 0x060136D7 RID: 79575 RVA: 0x00306E2D File Offset: 0x0030502D
		// (set) Token: 0x060136D8 RID: 79576 RVA: 0x00306E36 File Offset: 0x00305036
		public GraphicData GraphicData
		{
			get
			{
				return base.GetElement<GraphicData>(0);
			}
			set
			{
				base.SetElement<GraphicData>(0, value);
			}
		}

		// Token: 0x060136D9 RID: 79577 RVA: 0x00306E40 File Offset: 0x00305040
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Graphic>(deep);
		}

		// Token: 0x04008629 RID: 34345
		private const string tagName = "graphic";

		// Token: 0x0400862A RID: 34346
		private const byte tagNsId = 10;

		// Token: 0x0400862B RID: 34347
		internal const int ElementTypeIdConst = 10120;

		// Token: 0x0400862C RID: 34348
		private static readonly string[] eleTagNames = new string[] { "graphicData" };

		// Token: 0x0400862D RID: 34349
		private static readonly byte[] eleNamespaceIds = new byte[] { 10 };
	}
}
