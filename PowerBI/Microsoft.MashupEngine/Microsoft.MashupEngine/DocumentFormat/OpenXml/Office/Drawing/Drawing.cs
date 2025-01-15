using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Office.Drawing
{
	// Token: 0x02002324 RID: 8996
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ShapeTree))]
	internal class Drawing : OpenXmlPartRootElement
	{
		// Token: 0x1700486B RID: 18539
		// (get) Token: 0x06010026 RID: 65574 RVA: 0x002A7FB6 File Offset: 0x002A61B6
		public override string LocalName
		{
			get
			{
				return "drawing";
			}
		}

		// Token: 0x1700486C RID: 18540
		// (get) Token: 0x06010027 RID: 65575 RVA: 0x002DE7F3 File Offset: 0x002DC9F3
		internal override byte NamespaceId
		{
			get
			{
				return 56;
			}
		}

		// Token: 0x1700486D RID: 18541
		// (get) Token: 0x06010028 RID: 65576 RVA: 0x002DE7F7 File Offset: 0x002DC9F7
		internal override int ElementTypeId
		{
			get
			{
				return 13019;
			}
		}

		// Token: 0x06010029 RID: 65577 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601002A RID: 65578 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Drawing(DiagramPersistLayoutPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x0601002B RID: 65579 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(DiagramPersistLayoutPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x1700486E RID: 18542
		// (get) Token: 0x0601002C RID: 65580 RVA: 0x002DE7FE File Offset: 0x002DC9FE
		// (set) Token: 0x0601002D RID: 65581 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public DiagramPersistLayoutPart DiagramPersistLayoutPart
		{
			get
			{
				return base.OpenXmlPart as DiagramPersistLayoutPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x0601002E RID: 65582 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Drawing(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601002F RID: 65583 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Drawing(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010030 RID: 65584 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Drawing(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010031 RID: 65585 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Drawing()
		{
		}

		// Token: 0x06010032 RID: 65586 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(DiagramPersistLayoutPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x06010033 RID: 65587 RVA: 0x002DE80B File Offset: 0x002DCA0B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (56 == namespaceId && "spTree" == name)
			{
				return new ShapeTree();
			}
			return null;
		}

		// Token: 0x1700486F RID: 18543
		// (get) Token: 0x06010034 RID: 65588 RVA: 0x002DE826 File Offset: 0x002DCA26
		internal override string[] ElementTagNames
		{
			get
			{
				return Drawing.eleTagNames;
			}
		}

		// Token: 0x17004870 RID: 18544
		// (get) Token: 0x06010035 RID: 65589 RVA: 0x002DE82D File Offset: 0x002DCA2D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Drawing.eleNamespaceIds;
			}
		}

		// Token: 0x17004871 RID: 18545
		// (get) Token: 0x06010036 RID: 65590 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004872 RID: 18546
		// (get) Token: 0x06010037 RID: 65591 RVA: 0x002DE834 File Offset: 0x002DCA34
		// (set) Token: 0x06010038 RID: 65592 RVA: 0x002DE83D File Offset: 0x002DCA3D
		public ShapeTree ShapeTree
		{
			get
			{
				return base.GetElement<ShapeTree>(0);
			}
			set
			{
				base.SetElement<ShapeTree>(0, value);
			}
		}

		// Token: 0x06010039 RID: 65593 RVA: 0x002DE847 File Offset: 0x002DCA47
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Drawing>(deep);
		}

		// Token: 0x040072B0 RID: 29360
		private const string tagName = "drawing";

		// Token: 0x040072B1 RID: 29361
		private const byte tagNsId = 56;

		// Token: 0x040072B2 RID: 29362
		internal const int ElementTypeIdConst = 13019;

		// Token: 0x040072B3 RID: 29363
		private static readonly string[] eleTagNames = new string[] { "spTree" };

		// Token: 0x040072B4 RID: 29364
		private static readonly byte[] eleNamespaceIds = new byte[] { 56 };
	}
}
