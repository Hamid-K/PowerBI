using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A6B RID: 10859
	[ChildElementInfo(typeof(Extents))]
	[ChildElementInfo(typeof(Offset))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Transform : OpenXmlCompositeElement
	{
		// Token: 0x170072BD RID: 29373
		// (get) Token: 0x06015F4F RID: 89935 RVA: 0x002E002B File Offset: 0x002DE22B
		public override string LocalName
		{
			get
			{
				return "xfrm";
			}
		}

		// Token: 0x170072BE RID: 29374
		// (get) Token: 0x06015F50 RID: 89936 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170072BF RID: 29375
		// (get) Token: 0x06015F51 RID: 89937 RVA: 0x00324E78 File Offset: 0x00323078
		internal override int ElementTypeId
		{
			get
			{
				return 12277;
			}
		}

		// Token: 0x06015F52 RID: 89938 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170072C0 RID: 29376
		// (get) Token: 0x06015F53 RID: 89939 RVA: 0x00324E7F File Offset: 0x0032307F
		internal override string[] AttributeTagNames
		{
			get
			{
				return Transform.attributeTagNames;
			}
		}

		// Token: 0x170072C1 RID: 29377
		// (get) Token: 0x06015F54 RID: 89940 RVA: 0x00324E86 File Offset: 0x00323086
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Transform.attributeNamespaceIds;
			}
		}

		// Token: 0x170072C2 RID: 29378
		// (get) Token: 0x06015F55 RID: 89941 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06015F56 RID: 89942 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170072C3 RID: 29379
		// (get) Token: 0x06015F57 RID: 89943 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06015F58 RID: 89944 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170072C4 RID: 29380
		// (get) Token: 0x06015F59 RID: 89945 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06015F5A RID: 89946 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x06015F5B RID: 89947 RVA: 0x00293ECF File Offset: 0x002920CF
		public Transform()
		{
		}

		// Token: 0x06015F5C RID: 89948 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Transform(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015F5D RID: 89949 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Transform(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015F5E RID: 89950 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Transform(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015F5F RID: 89951 RVA: 0x002DF17C File Offset: 0x002DD37C
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

		// Token: 0x170072C5 RID: 29381
		// (get) Token: 0x06015F60 RID: 89952 RVA: 0x00324E8D File Offset: 0x0032308D
		internal override string[] ElementTagNames
		{
			get
			{
				return Transform.eleTagNames;
			}
		}

		// Token: 0x170072C6 RID: 29382
		// (get) Token: 0x06015F61 RID: 89953 RVA: 0x00324E94 File Offset: 0x00323094
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Transform.eleNamespaceIds;
			}
		}

		// Token: 0x170072C7 RID: 29383
		// (get) Token: 0x06015F62 RID: 89954 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170072C8 RID: 29384
		// (get) Token: 0x06015F63 RID: 89955 RVA: 0x002DF1BD File Offset: 0x002DD3BD
		// (set) Token: 0x06015F64 RID: 89956 RVA: 0x002DF1C6 File Offset: 0x002DD3C6
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

		// Token: 0x170072C9 RID: 29385
		// (get) Token: 0x06015F65 RID: 89957 RVA: 0x002DF1D0 File Offset: 0x002DD3D0
		// (set) Token: 0x06015F66 RID: 89958 RVA: 0x002DF1D9 File Offset: 0x002DD3D9
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

		// Token: 0x06015F67 RID: 89959 RVA: 0x00324E9C File Offset: 0x0032309C
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

		// Token: 0x06015F68 RID: 89960 RVA: 0x00324EF3 File Offset: 0x003230F3
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Transform>(deep);
		}

		// Token: 0x06015F69 RID: 89961 RVA: 0x00324EFC File Offset: 0x003230FC
		// Note: this type is marked as 'beforefieldinit'.
		static Transform()
		{
			byte[] array = new byte[3];
			Transform.attributeNamespaceIds = array;
			Transform.eleTagNames = new string[] { "off", "ext" };
			Transform.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x04009593 RID: 38291
		private const string tagName = "xfrm";

		// Token: 0x04009594 RID: 38292
		private const byte tagNsId = 24;

		// Token: 0x04009595 RID: 38293
		internal const int ElementTypeIdConst = 12277;

		// Token: 0x04009596 RID: 38294
		private static string[] attributeTagNames = new string[] { "rot", "flipH", "flipV" };

		// Token: 0x04009597 RID: 38295
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009598 RID: 38296
		private static readonly string[] eleTagNames;

		// Token: 0x04009599 RID: 38297
		private static readonly byte[] eleNamespaceIds;
	}
}
