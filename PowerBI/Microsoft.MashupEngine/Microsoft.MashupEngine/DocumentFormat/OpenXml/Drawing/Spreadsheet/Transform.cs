using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002888 RID: 10376
	[ChildElementInfo(typeof(Extents))]
	[ChildElementInfo(typeof(Offset))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Transform : OpenXmlCompositeElement
	{
		// Token: 0x17006779 RID: 26489
		// (get) Token: 0x060145CA RID: 83402 RVA: 0x002E002B File Offset: 0x002DE22B
		public override string LocalName
		{
			get
			{
				return "xfrm";
			}
		}

		// Token: 0x1700677A RID: 26490
		// (get) Token: 0x060145CB RID: 83403 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x1700677B RID: 26491
		// (get) Token: 0x060145CC RID: 83404 RVA: 0x00312871 File Offset: 0x00310A71
		internal override int ElementTypeId
		{
			get
			{
				return 10738;
			}
		}

		// Token: 0x060145CD RID: 83405 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700677C RID: 26492
		// (get) Token: 0x060145CE RID: 83406 RVA: 0x00312878 File Offset: 0x00310A78
		internal override string[] AttributeTagNames
		{
			get
			{
				return Transform.attributeTagNames;
			}
		}

		// Token: 0x1700677D RID: 26493
		// (get) Token: 0x060145CF RID: 83407 RVA: 0x0031287F File Offset: 0x00310A7F
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Transform.attributeNamespaceIds;
			}
		}

		// Token: 0x1700677E RID: 26494
		// (get) Token: 0x060145D0 RID: 83408 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060145D1 RID: 83409 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "rot")]
		public Int32Value Rotation
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700677F RID: 26495
		// (get) Token: 0x060145D2 RID: 83410 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x060145D3 RID: 83411 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "flipH")]
		public BooleanValue HorizontalFlip
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17006780 RID: 26496
		// (get) Token: 0x060145D4 RID: 83412 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x060145D5 RID: 83413 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "flipV")]
		public BooleanValue VerticalFlip
		{
			get
			{
				return (BooleanValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x060145D6 RID: 83414 RVA: 0x00293ECF File Offset: 0x002920CF
		public Transform()
		{
		}

		// Token: 0x060145D7 RID: 83415 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Transform(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060145D8 RID: 83416 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Transform(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060145D9 RID: 83417 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Transform(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060145DA RID: 83418 RVA: 0x002DF17C File Offset: 0x002DD37C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "off" == name)
			{
				return new Offset();
			}
			if (10 == namespaceId && "ext" == name)
			{
				return new Extents();
			}
			return null;
		}

		// Token: 0x17006781 RID: 26497
		// (get) Token: 0x060145DB RID: 83419 RVA: 0x00312886 File Offset: 0x00310A86
		internal override string[] ElementTagNames
		{
			get
			{
				return Transform.eleTagNames;
			}
		}

		// Token: 0x17006782 RID: 26498
		// (get) Token: 0x060145DC RID: 83420 RVA: 0x0031288D File Offset: 0x00310A8D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Transform.eleNamespaceIds;
			}
		}

		// Token: 0x17006783 RID: 26499
		// (get) Token: 0x060145DD RID: 83421 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006784 RID: 26500
		// (get) Token: 0x060145DE RID: 83422 RVA: 0x002DF1BD File Offset: 0x002DD3BD
		// (set) Token: 0x060145DF RID: 83423 RVA: 0x002DF1C6 File Offset: 0x002DD3C6
		public Offset Offset
		{
			get
			{
				return base.GetElement<Offset>(0);
			}
			set
			{
				base.SetElement<Offset>(0, value);
			}
		}

		// Token: 0x17006785 RID: 26501
		// (get) Token: 0x060145E0 RID: 83424 RVA: 0x002DF1D0 File Offset: 0x002DD3D0
		// (set) Token: 0x060145E1 RID: 83425 RVA: 0x002DF1D9 File Offset: 0x002DD3D9
		public Extents Extents
		{
			get
			{
				return base.GetElement<Extents>(1);
			}
			set
			{
				base.SetElement<Extents>(1, value);
			}
		}

		// Token: 0x060145E2 RID: 83426 RVA: 0x00312894 File Offset: 0x00310A94
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "rot" == name)
			{
				return new Int32Value();
			}
			if (namespaceId == 0 && "flipH" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "flipV" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060145E3 RID: 83427 RVA: 0x003128EB File Offset: 0x00310AEB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Transform>(deep);
		}

		// Token: 0x060145E4 RID: 83428 RVA: 0x003128F4 File Offset: 0x00310AF4
		// Note: this type is marked as 'beforefieldinit'.
		static Transform()
		{
			byte[] array = new byte[3];
			Transform.attributeNamespaceIds = array;
			Transform.eleTagNames = new string[] { "off", "ext" };
			Transform.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x04008DBF RID: 36287
		private const string tagName = "xfrm";

		// Token: 0x04008DC0 RID: 36288
		private const byte tagNsId = 18;

		// Token: 0x04008DC1 RID: 36289
		internal const int ElementTypeIdConst = 10738;

		// Token: 0x04008DC2 RID: 36290
		private static string[] attributeTagNames = new string[] { "rot", "flipH", "flipV" };

		// Token: 0x04008DC3 RID: 36291
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008DC4 RID: 36292
		private static readonly string[] eleTagNames;

		// Token: 0x04008DC5 RID: 36293
		private static readonly byte[] eleNamespaceIds;
	}
}
