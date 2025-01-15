using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Ink
{
	// Token: 0x02002266 RID: 8806
	[ChildElementInfo(typeof(DestinationLink))]
	[ChildElementInfo(typeof(ContextNodeProperty))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(SourceLink))]
	internal class ContextNode : OpenXmlCompositeElement
	{
		// Token: 0x17003CB2 RID: 15538
		// (get) Token: 0x0600E74F RID: 59215 RVA: 0x002C8263 File Offset: 0x002C6463
		public override string LocalName
		{
			get
			{
				return "context";
			}
		}

		// Token: 0x17003CB3 RID: 15539
		// (get) Token: 0x0600E750 RID: 59216 RVA: 0x002C826A File Offset: 0x002C646A
		internal override byte NamespaceId
		{
			get
			{
				return 45;
			}
		}

		// Token: 0x17003CB4 RID: 15540
		// (get) Token: 0x0600E751 RID: 59217 RVA: 0x002C826E File Offset: 0x002C646E
		internal override int ElementTypeId
		{
			get
			{
				return 12687;
			}
		}

		// Token: 0x0600E752 RID: 59218 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17003CB5 RID: 15541
		// (get) Token: 0x0600E753 RID: 59219 RVA: 0x002C8275 File Offset: 0x002C6475
		internal override string[] AttributeTagNames
		{
			get
			{
				return ContextNode.attributeTagNames;
			}
		}

		// Token: 0x17003CB6 RID: 15542
		// (get) Token: 0x0600E754 RID: 59220 RVA: 0x002C827C File Offset: 0x002C647C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ContextNode.attributeNamespaceIds;
			}
		}

		// Token: 0x17003CB7 RID: 15543
		// (get) Token: 0x0600E755 RID: 59221 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600E756 RID: 59222 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public StringValue Id
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17003CB8 RID: 15544
		// (get) Token: 0x0600E757 RID: 59223 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x0600E758 RID: 59224 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "type")]
		public StringValue Type
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17003CB9 RID: 15545
		// (get) Token: 0x0600E759 RID: 59225 RVA: 0x002C8283 File Offset: 0x002C6483
		// (set) Token: 0x0600E75A RID: 59226 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "rotatedBoundingBox")]
		public ListValue<StringValue> RotatedBoundingBox
		{
			get
			{
				return (ListValue<StringValue>)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17003CBA RID: 15546
		// (get) Token: 0x0600E75B RID: 59227 RVA: 0x002BFA76 File Offset: 0x002BDC76
		// (set) Token: 0x0600E75C RID: 59228 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "alignmentLevel")]
		public Int32Value AlignmentLevel
		{
			get
			{
				return (Int32Value)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x17003CBB RID: 15547
		// (get) Token: 0x0600E75D RID: 59229 RVA: 0x002C8292 File Offset: 0x002C6492
		// (set) Token: 0x0600E75E RID: 59230 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "contentType")]
		public Int32Value ContentType
		{
			get
			{
				return (Int32Value)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x17003CBC RID: 15548
		// (get) Token: 0x0600E75F RID: 59231 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x0600E760 RID: 59232 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "ascender")]
		public StringValue Ascender
		{
			get
			{
				return (StringValue)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x17003CBD RID: 15549
		// (get) Token: 0x0600E761 RID: 59233 RVA: 0x002BD4ED File Offset: 0x002BB6ED
		// (set) Token: 0x0600E762 RID: 59234 RVA: 0x002BD4FC File Offset: 0x002BB6FC
		[SchemaAttr(0, "descender")]
		public StringValue Descender
		{
			get
			{
				return (StringValue)base.Attributes[6];
			}
			set
			{
				base.Attributes[6] = value;
			}
		}

		// Token: 0x17003CBE RID: 15550
		// (get) Token: 0x0600E763 RID: 59235 RVA: 0x002BDB07 File Offset: 0x002BBD07
		// (set) Token: 0x0600E764 RID: 59236 RVA: 0x002BD516 File Offset: 0x002BB716
		[SchemaAttr(0, "baseline")]
		public StringValue Baseline
		{
			get
			{
				return (StringValue)base.Attributes[7];
			}
			set
			{
				base.Attributes[7] = value;
			}
		}

		// Token: 0x17003CBF RID: 15551
		// (get) Token: 0x0600E765 RID: 59237 RVA: 0x002BDB16 File Offset: 0x002BBD16
		// (set) Token: 0x0600E766 RID: 59238 RVA: 0x002BD530 File Offset: 0x002BB730
		[SchemaAttr(0, "midline")]
		public StringValue Midline
		{
			get
			{
				return (StringValue)base.Attributes[8];
			}
			set
			{
				base.Attributes[8] = value;
			}
		}

		// Token: 0x17003CC0 RID: 15552
		// (get) Token: 0x0600E767 RID: 59239 RVA: 0x002BDB25 File Offset: 0x002BBD25
		// (set) Token: 0x0600E768 RID: 59240 RVA: 0x002BD54B File Offset: 0x002BB74B
		[SchemaAttr(0, "customRecognizerId")]
		public StringValue CustomRecognizerId
		{
			get
			{
				return (StringValue)base.Attributes[9];
			}
			set
			{
				base.Attributes[9] = value;
			}
		}

		// Token: 0x17003CC1 RID: 15553
		// (get) Token: 0x0600E769 RID: 59241 RVA: 0x002BDB35 File Offset: 0x002BBD35
		// (set) Token: 0x0600E76A RID: 59242 RVA: 0x002BDB45 File Offset: 0x002BBD45
		[SchemaAttr(0, "mathML")]
		public StringValue MathML
		{
			get
			{
				return (StringValue)base.Attributes[10];
			}
			set
			{
				base.Attributes[10] = value;
			}
		}

		// Token: 0x17003CC2 RID: 15554
		// (get) Token: 0x0600E76B RID: 59243 RVA: 0x002BDB51 File Offset: 0x002BBD51
		// (set) Token: 0x0600E76C RID: 59244 RVA: 0x002BDB61 File Offset: 0x002BBD61
		[SchemaAttr(0, "mathStruct")]
		public StringValue MathStruct
		{
			get
			{
				return (StringValue)base.Attributes[11];
			}
			set
			{
				base.Attributes[11] = value;
			}
		}

		// Token: 0x17003CC3 RID: 15555
		// (get) Token: 0x0600E76D RID: 59245 RVA: 0x002BDB6D File Offset: 0x002BBD6D
		// (set) Token: 0x0600E76E RID: 59246 RVA: 0x002BDB7D File Offset: 0x002BBD7D
		[SchemaAttr(0, "mathSymbol")]
		public StringValue MathSymbol
		{
			get
			{
				return (StringValue)base.Attributes[12];
			}
			set
			{
				base.Attributes[12] = value;
			}
		}

		// Token: 0x17003CC4 RID: 15556
		// (get) Token: 0x0600E76F RID: 59247 RVA: 0x002BEF1F File Offset: 0x002BD11F
		// (set) Token: 0x0600E770 RID: 59248 RVA: 0x002BE209 File Offset: 0x002BC409
		[SchemaAttr(0, "beginModifierType")]
		public StringValue BeginModifierType
		{
			get
			{
				return (StringValue)base.Attributes[13];
			}
			set
			{
				base.Attributes[13] = value;
			}
		}

		// Token: 0x17003CC5 RID: 15557
		// (get) Token: 0x0600E771 RID: 59249 RVA: 0x002BE215 File Offset: 0x002BC415
		// (set) Token: 0x0600E772 RID: 59250 RVA: 0x002BE225 File Offset: 0x002BC425
		[SchemaAttr(0, "endModifierType")]
		public StringValue EndModifierType
		{
			get
			{
				return (StringValue)base.Attributes[14];
			}
			set
			{
				base.Attributes[14] = value;
			}
		}

		// Token: 0x17003CC6 RID: 15558
		// (get) Token: 0x0600E773 RID: 59251 RVA: 0x002C82A1 File Offset: 0x002C64A1
		// (set) Token: 0x0600E774 RID: 59252 RVA: 0x002BE241 File Offset: 0x002BC441
		[SchemaAttr(0, "rotationAngle")]
		public Int32Value RotationAngle
		{
			get
			{
				return (Int32Value)base.Attributes[15];
			}
			set
			{
				base.Attributes[15] = value;
			}
		}

		// Token: 0x17003CC7 RID: 15559
		// (get) Token: 0x0600E775 RID: 59253 RVA: 0x002C82B1 File Offset: 0x002C64B1
		// (set) Token: 0x0600E776 RID: 59254 RVA: 0x002BE25D File Offset: 0x002BC45D
		[SchemaAttr(0, "hotPoints")]
		public ListValue<StringValue> HotPoints
		{
			get
			{
				return (ListValue<StringValue>)base.Attributes[16];
			}
			set
			{
				base.Attributes[16] = value;
			}
		}

		// Token: 0x17003CC8 RID: 15560
		// (get) Token: 0x0600E777 RID: 59255 RVA: 0x002C02CC File Offset: 0x002BE4CC
		// (set) Token: 0x0600E778 RID: 59256 RVA: 0x002BE279 File Offset: 0x002BC479
		[SchemaAttr(0, "centroid")]
		public StringValue Centroid
		{
			get
			{
				return (StringValue)base.Attributes[17];
			}
			set
			{
				base.Attributes[17] = value;
			}
		}

		// Token: 0x17003CC9 RID: 15561
		// (get) Token: 0x0600E779 RID: 59257 RVA: 0x002BE285 File Offset: 0x002BC485
		// (set) Token: 0x0600E77A RID: 59258 RVA: 0x002BE295 File Offset: 0x002BC495
		[SchemaAttr(0, "semanticType")]
		public StringValue SemanticType
		{
			get
			{
				return (StringValue)base.Attributes[18];
			}
			set
			{
				base.Attributes[18] = value;
			}
		}

		// Token: 0x17003CCA RID: 15562
		// (get) Token: 0x0600E77B RID: 59259 RVA: 0x002BE2A1 File Offset: 0x002BC4A1
		// (set) Token: 0x0600E77C RID: 59260 RVA: 0x002BE2B1 File Offset: 0x002BC4B1
		[SchemaAttr(0, "shapeName")]
		public StringValue ShapeName
		{
			get
			{
				return (StringValue)base.Attributes[19];
			}
			set
			{
				base.Attributes[19] = value;
			}
		}

		// Token: 0x17003CCB RID: 15563
		// (get) Token: 0x0600E77D RID: 59261 RVA: 0x002C82C1 File Offset: 0x002C64C1
		// (set) Token: 0x0600E77E RID: 59262 RVA: 0x002BE2CD File Offset: 0x002BC4CD
		[SchemaAttr(0, "shapeGeometry")]
		public ListValue<StringValue> ShapeGeometry
		{
			get
			{
				return (ListValue<StringValue>)base.Attributes[20];
			}
			set
			{
				base.Attributes[20] = value;
			}
		}

		// Token: 0x0600E77F RID: 59263 RVA: 0x00293ECF File Offset: 0x002920CF
		public ContextNode()
		{
		}

		// Token: 0x0600E780 RID: 59264 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ContextNode(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E781 RID: 59265 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ContextNode(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600E782 RID: 59266 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ContextNode(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600E783 RID: 59267 RVA: 0x002C82D4 File Offset: 0x002C64D4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (45 == namespaceId && "property" == name)
			{
				return new ContextNodeProperty();
			}
			if (45 == namespaceId && "sourceLink" == name)
			{
				return new SourceLink();
			}
			if (45 == namespaceId && "destinationLink" == name)
			{
				return new DestinationLink();
			}
			return null;
		}

		// Token: 0x0600E784 RID: 59268 RVA: 0x002C832C File Offset: 0x002C652C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "type" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "rotatedBoundingBox" == name)
			{
				return new ListValue<StringValue>();
			}
			if (namespaceId == 0 && "alignmentLevel" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "contentType" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "ascender" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "descender" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "baseline" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "midline" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "customRecognizerId" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "mathML" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "mathStruct" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "mathSymbol" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "beginModifierType" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "endModifierType" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "rotationAngle" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "hotPoints" == name)
			{
				return new ListValue<StringValue>();
			}
			if (namespaceId == 0 && "centroid" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "semanticType" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "shapeName" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "shapeGeometry" == name)
			{
				return new ListValue<StringValue>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600E785 RID: 59269 RVA: 0x002C850F File Offset: 0x002C670F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ContextNode>(deep);
		}

		// Token: 0x0600E786 RID: 59270 RVA: 0x002C8518 File Offset: 0x002C6718
		// Note: this type is marked as 'beforefieldinit'.
		static ContextNode()
		{
			byte[] array = new byte[21];
			ContextNode.attributeNamespaceIds = array;
		}

		// Token: 0x04006F4B RID: 28491
		private const string tagName = "context";

		// Token: 0x04006F4C RID: 28492
		private const byte tagNsId = 45;

		// Token: 0x04006F4D RID: 28493
		internal const int ElementTypeIdConst = 12687;

		// Token: 0x04006F4E RID: 28494
		private static string[] attributeTagNames = new string[]
		{
			"id", "type", "rotatedBoundingBox", "alignmentLevel", "contentType", "ascender", "descender", "baseline", "midline", "customRecognizerId",
			"mathML", "mathStruct", "mathSymbol", "beginModifierType", "endModifierType", "rotationAngle", "hotPoints", "centroid", "semanticType", "shapeName",
			"shapeGeometry"
		};

		// Token: 0x04006F4F RID: 28495
		private static byte[] attributeNamespaceIds;
	}
}
