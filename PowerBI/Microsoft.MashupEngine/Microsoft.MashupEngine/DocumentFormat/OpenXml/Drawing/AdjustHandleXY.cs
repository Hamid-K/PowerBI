using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027C8 RID: 10184
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Position))]
	internal class AdjustHandleXY : OpenXmlCompositeElement
	{
		// Token: 0x17006389 RID: 25481
		// (get) Token: 0x06013C85 RID: 81029 RVA: 0x0030BA76 File Offset: 0x00309C76
		public override string LocalName
		{
			get
			{
				return "ahXY";
			}
		}

		// Token: 0x1700638A RID: 25482
		// (get) Token: 0x06013C86 RID: 81030 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x1700638B RID: 25483
		// (get) Token: 0x06013C87 RID: 81031 RVA: 0x0030BA7D File Offset: 0x00309C7D
		internal override int ElementTypeId
		{
			get
			{
				return 10217;
			}
		}

		// Token: 0x06013C88 RID: 81032 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700638C RID: 25484
		// (get) Token: 0x06013C89 RID: 81033 RVA: 0x0030BA84 File Offset: 0x00309C84
		internal override string[] AttributeTagNames
		{
			get
			{
				return AdjustHandleXY.attributeTagNames;
			}
		}

		// Token: 0x1700638D RID: 25485
		// (get) Token: 0x06013C8A RID: 81034 RVA: 0x0030BA8B File Offset: 0x00309C8B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AdjustHandleXY.attributeNamespaceIds;
			}
		}

		// Token: 0x1700638E RID: 25486
		// (get) Token: 0x06013C8B RID: 81035 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06013C8C RID: 81036 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "gdRefX")]
		public StringValue XAdjustmentGuide
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

		// Token: 0x1700638F RID: 25487
		// (get) Token: 0x06013C8D RID: 81037 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06013C8E RID: 81038 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "minX")]
		public StringValue MinX
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

		// Token: 0x17006390 RID: 25488
		// (get) Token: 0x06013C8F RID: 81039 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06013C90 RID: 81040 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "maxX")]
		public StringValue MaxX
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

		// Token: 0x17006391 RID: 25489
		// (get) Token: 0x06013C91 RID: 81041 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x06013C92 RID: 81042 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "gdRefY")]
		public StringValue YAdjustmentGuide
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

		// Token: 0x17006392 RID: 25490
		// (get) Token: 0x06013C93 RID: 81043 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06013C94 RID: 81044 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "minY")]
		public StringValue MinY
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

		// Token: 0x17006393 RID: 25491
		// (get) Token: 0x06013C95 RID: 81045 RVA: 0x002BE081 File Offset: 0x002BC281
		// (set) Token: 0x06013C96 RID: 81046 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(0, "maxY")]
		public StringValue MaxY
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

		// Token: 0x06013C97 RID: 81047 RVA: 0x00293ECF File Offset: 0x002920CF
		public AdjustHandleXY()
		{
		}

		// Token: 0x06013C98 RID: 81048 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AdjustHandleXY(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013C99 RID: 81049 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AdjustHandleXY(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013C9A RID: 81050 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AdjustHandleXY(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013C9B RID: 81051 RVA: 0x0030BA92 File Offset: 0x00309C92
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "pos" == name)
			{
				return new Position();
			}
			return null;
		}

		// Token: 0x17006394 RID: 25492
		// (get) Token: 0x06013C9C RID: 81052 RVA: 0x0030BAAD File Offset: 0x00309CAD
		internal override string[] ElementTagNames
		{
			get
			{
				return AdjustHandleXY.eleTagNames;
			}
		}

		// Token: 0x17006395 RID: 25493
		// (get) Token: 0x06013C9D RID: 81053 RVA: 0x0030BAB4 File Offset: 0x00309CB4
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AdjustHandleXY.eleNamespaceIds;
			}
		}

		// Token: 0x17006396 RID: 25494
		// (get) Token: 0x06013C9E RID: 81054 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006397 RID: 25495
		// (get) Token: 0x06013C9F RID: 81055 RVA: 0x0030BABB File Offset: 0x00309CBB
		// (set) Token: 0x06013CA0 RID: 81056 RVA: 0x0030BAC4 File Offset: 0x00309CC4
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

		// Token: 0x06013CA1 RID: 81057 RVA: 0x0030BAD0 File Offset: 0x00309CD0
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "gdRefX" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "minX" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "maxX" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "gdRefY" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "minY" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "maxY" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06013CA2 RID: 81058 RVA: 0x0030BB69 File Offset: 0x00309D69
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AdjustHandleXY>(deep);
		}

		// Token: 0x06013CA3 RID: 81059 RVA: 0x0030BB74 File Offset: 0x00309D74
		// Note: this type is marked as 'beforefieldinit'.
		static AdjustHandleXY()
		{
			byte[] array = new byte[6];
			AdjustHandleXY.attributeNamespaceIds = array;
			AdjustHandleXY.eleTagNames = new string[] { "pos" };
			AdjustHandleXY.eleNamespaceIds = new byte[] { 10 };
		}

		// Token: 0x040087C4 RID: 34756
		private const string tagName = "ahXY";

		// Token: 0x040087C5 RID: 34757
		private const byte tagNsId = 10;

		// Token: 0x040087C6 RID: 34758
		internal const int ElementTypeIdConst = 10217;

		// Token: 0x040087C7 RID: 34759
		private static string[] attributeTagNames = new string[] { "gdRefX", "minX", "maxX", "gdRefY", "minY", "maxY" };

		// Token: 0x040087C8 RID: 34760
		private static byte[] attributeNamespaceIds;

		// Token: 0x040087C9 RID: 34761
		private static readonly string[] eleTagNames;

		// Token: 0x040087CA RID: 34762
		private static readonly byte[] eleNamespaceIds;
	}
}
