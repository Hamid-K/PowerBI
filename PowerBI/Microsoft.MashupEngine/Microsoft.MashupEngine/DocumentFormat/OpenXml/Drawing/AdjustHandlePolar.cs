using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027C9 RID: 10185
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Position))]
	internal class AdjustHandlePolar : OpenXmlCompositeElement
	{
		// Token: 0x17006398 RID: 25496
		// (get) Token: 0x06013CA4 RID: 81060 RVA: 0x0030BBF2 File Offset: 0x00309DF2
		public override string LocalName
		{
			get
			{
				return "ahPolar";
			}
		}

		// Token: 0x17006399 RID: 25497
		// (get) Token: 0x06013CA5 RID: 81061 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700639A RID: 25498
		// (get) Token: 0x06013CA6 RID: 81062 RVA: 0x0030BBF9 File Offset: 0x00309DF9
		internal override int ElementTypeId
		{
			get
			{
				return 10218;
			}
		}

		// Token: 0x06013CA7 RID: 81063 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700639B RID: 25499
		// (get) Token: 0x06013CA8 RID: 81064 RVA: 0x0030BC00 File Offset: 0x00309E00
		internal override string[] AttributeTagNames
		{
			get
			{
				return AdjustHandlePolar.attributeTagNames;
			}
		}

		// Token: 0x1700639C RID: 25500
		// (get) Token: 0x06013CA9 RID: 81065 RVA: 0x0030BC07 File Offset: 0x00309E07
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AdjustHandlePolar.attributeNamespaceIds;
			}
		}

		// Token: 0x1700639D RID: 25501
		// (get) Token: 0x06013CAA RID: 81066 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06013CAB RID: 81067 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "gdRefR")]
		public StringValue RadialAdjustmentGuide
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

		// Token: 0x1700639E RID: 25502
		// (get) Token: 0x06013CAC RID: 81068 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06013CAD RID: 81069 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "minR")]
		public StringValue MinRadial
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

		// Token: 0x1700639F RID: 25503
		// (get) Token: 0x06013CAE RID: 81070 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06013CAF RID: 81071 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "maxR")]
		public StringValue MaxRadial
		{
			get
			{
				return (StringValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170063A0 RID: 25504
		// (get) Token: 0x06013CB0 RID: 81072 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06013CB1 RID: 81073 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "gdRefAng")]
		public StringValue AngleAdjustmentGuide
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x170063A1 RID: 25505
		// (get) Token: 0x06013CB2 RID: 81074 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06013CB3 RID: 81075 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "minAng")]
		public StringValue MinAngle
		{
			get
			{
				return (StringValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x170063A2 RID: 25506
		// (get) Token: 0x06013CB4 RID: 81076 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x06013CB5 RID: 81077 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "maxAng")]
		public StringValue MaxAngle
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

		// Token: 0x06013CB6 RID: 81078 RVA: 0x00293ECF File Offset: 0x002920CF
		public AdjustHandlePolar()
		{
		}

		// Token: 0x06013CB7 RID: 81079 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AdjustHandlePolar(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013CB8 RID: 81080 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AdjustHandlePolar(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013CB9 RID: 81081 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AdjustHandlePolar(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013CBA RID: 81082 RVA: 0x0030BA92 File Offset: 0x00309C92
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "pos" == name)
			{
				return new Position();
			}
			return null;
		}

		// Token: 0x170063A3 RID: 25507
		// (get) Token: 0x06013CBB RID: 81083 RVA: 0x0030BC0E File Offset: 0x00309E0E
		internal override string[] ElementTagNames
		{
			get
			{
				return AdjustHandlePolar.eleTagNames;
			}
		}

		// Token: 0x170063A4 RID: 25508
		// (get) Token: 0x06013CBC RID: 81084 RVA: 0x0030BC15 File Offset: 0x00309E15
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AdjustHandlePolar.eleNamespaceIds;
			}
		}

		// Token: 0x170063A5 RID: 25509
		// (get) Token: 0x06013CBD RID: 81085 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170063A6 RID: 25510
		// (get) Token: 0x06013CBE RID: 81086 RVA: 0x0030BABB File Offset: 0x00309CBB
		// (set) Token: 0x06013CBF RID: 81087 RVA: 0x0030BAC4 File Offset: 0x00309CC4
		public Position Position
		{
			get
			{
				return base.GetElement<Position>(0);
			}
			set
			{
				base.SetElement<Position>(0, value);
			}
		}

		// Token: 0x06013CC0 RID: 81088 RVA: 0x0030BC1C File Offset: 0x00309E1C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "gdRefR" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "minR" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "maxR" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "gdRefAng" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "minAng" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "maxAng" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013CC1 RID: 81089 RVA: 0x0030BCB5 File Offset: 0x00309EB5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AdjustHandlePolar>(deep);
		}

		// Token: 0x06013CC2 RID: 81090 RVA: 0x0030BCC0 File Offset: 0x00309EC0
		// Note: this type is marked as 'beforefieldinit'.
		static AdjustHandlePolar()
		{
			byte[] array = new byte[6];
			AdjustHandlePolar.attributeNamespaceIds = array;
			AdjustHandlePolar.eleTagNames = new string[] { "pos" };
			AdjustHandlePolar.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x040087CB RID: 34763
		private const string tagName = "ahPolar";

		// Token: 0x040087CC RID: 34764
		private const byte tagNsId = 10;

		// Token: 0x040087CD RID: 34765
		internal const int ElementTypeIdConst = 10218;

		// Token: 0x040087CE RID: 34766
		private static string[] attributeTagNames = new string[] { "gdRefR", "minR", "maxR", "gdRefAng", "minAng", "maxAng" };

		// Token: 0x040087CF RID: 34767
		private static byte[] attributeNamespaceIds;

		// Token: 0x040087D0 RID: 34768
		private static readonly string[] eleTagNames;

		// Token: 0x040087D1 RID: 34769
		private static readonly byte[] eleNamespaceIds;
	}
}
