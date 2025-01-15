using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002886 RID: 10374
	[ChildElementInfo(typeof(Blip))]
	[ChildElementInfo(typeof(Tile))]
	[ChildElementInfo(typeof(Stretch))]
	[ChildElementInfo(typeof(SourceRectangle))]
	[GeneratedCode("DomGen", "2.0")]
	internal class BlipFill : OpenXmlCompositeElement
	{
		// Token: 0x17006766 RID: 26470
		// (get) Token: 0x060145A1 RID: 83361 RVA: 0x002FC561 File Offset: 0x002FA761
		public override string LocalName
		{
			get
			{
				return "blipFill";
			}
		}

		// Token: 0x17006767 RID: 26471
		// (get) Token: 0x060145A2 RID: 83362 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x17006768 RID: 26472
		// (get) Token: 0x060145A3 RID: 83363 RVA: 0x00312699 File Offset: 0x00310899
		internal override int ElementTypeId
		{
			get
			{
				return 10736;
			}
		}

		// Token: 0x060145A4 RID: 83364 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006769 RID: 26473
		// (get) Token: 0x060145A5 RID: 83365 RVA: 0x003126A0 File Offset: 0x003108A0
		internal override string[] AttributeTagNames
		{
			get
			{
				return BlipFill.attributeTagNames;
			}
		}

		// Token: 0x1700676A RID: 26474
		// (get) Token: 0x060145A6 RID: 83366 RVA: 0x003126A7 File Offset: 0x003108A7
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return BlipFill.attributeNamespaceIds;
			}
		}

		// Token: 0x1700676B RID: 26475
		// (get) Token: 0x060145A7 RID: 83367 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060145A8 RID: 83368 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rotWithShape")]
		public BooleanValue RotateWithShape
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060145A9 RID: 83369 RVA: 0x00293ECF File Offset: 0x002920CF
		public BlipFill()
		{
		}

		// Token: 0x060145AA RID: 83370 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BlipFill(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060145AB RID: 83371 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BlipFill(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060145AC RID: 83372 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BlipFill(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060145AD RID: 83373 RVA: 0x003126B0 File Offset: 0x003108B0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "blip" == name)
			{
				return new Blip();
			}
			if (10 == namespaceId && "srcRect" == name)
			{
				return new SourceRectangle();
			}
			if (10 == namespaceId && "tile" == name)
			{
				return new Tile();
			}
			if (10 == namespaceId && "stretch" == name)
			{
				return new Stretch();
			}
			return null;
		}

		// Token: 0x1700676C RID: 26476
		// (get) Token: 0x060145AE RID: 83374 RVA: 0x0031271E File Offset: 0x0031091E
		internal override string[] ElementTagNames
		{
			get
			{
				return BlipFill.eleTagNames;
			}
		}

		// Token: 0x1700676D RID: 26477
		// (get) Token: 0x060145AF RID: 83375 RVA: 0x00312725 File Offset: 0x00310925
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BlipFill.eleNamespaceIds;
			}
		}

		// Token: 0x1700676E RID: 26478
		// (get) Token: 0x060145B0 RID: 83376 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700676F RID: 26479
		// (get) Token: 0x060145B1 RID: 83377 RVA: 0x002FC5FC File Offset: 0x002FA7FC
		// (set) Token: 0x060145B2 RID: 83378 RVA: 0x002FC605 File Offset: 0x002FA805
		public Blip Blip
		{
			get
			{
				return base.GetElement<Blip>(0);
			}
			set
			{
				base.SetElement<Blip>(0, value);
			}
		}

		// Token: 0x17006770 RID: 26480
		// (get) Token: 0x060145B3 RID: 83379 RVA: 0x002FC60F File Offset: 0x002FA80F
		// (set) Token: 0x060145B4 RID: 83380 RVA: 0x002FC618 File Offset: 0x002FA818
		public SourceRectangle SourceRectangle
		{
			get
			{
				return base.GetElement<SourceRectangle>(1);
			}
			set
			{
				base.SetElement<SourceRectangle>(1, value);
			}
		}

		// Token: 0x060145B5 RID: 83381 RVA: 0x0031272C File Offset: 0x0031092C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rotWithShape" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060145B6 RID: 83382 RVA: 0x0031274C File Offset: 0x0031094C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BlipFill>(deep);
		}

		// Token: 0x060145B7 RID: 83383 RVA: 0x00312758 File Offset: 0x00310958
		// Note: this type is marked as 'beforefieldinit'.
		static BlipFill()
		{
			byte[] array = new byte[1];
			BlipFill.attributeNamespaceIds = array;
			BlipFill.eleTagNames = new string[] { "blip", "srcRect", "tile", "stretch" };
			BlipFill.eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
		}

		// Token: 0x04008DB3 RID: 36275
		private const string tagName = "blipFill";

		// Token: 0x04008DB4 RID: 36276
		private const byte tagNsId = 18;

		// Token: 0x04008DB5 RID: 36277
		internal const int ElementTypeIdConst = 10736;

		// Token: 0x04008DB6 RID: 36278
		private static string[] attributeTagNames = new string[] { "rotWithShape" };

		// Token: 0x04008DB7 RID: 36279
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008DB8 RID: 36280
		private static readonly string[] eleTagNames;

		// Token: 0x04008DB9 RID: 36281
		private static readonly byte[] eleNamespaceIds;
	}
}
