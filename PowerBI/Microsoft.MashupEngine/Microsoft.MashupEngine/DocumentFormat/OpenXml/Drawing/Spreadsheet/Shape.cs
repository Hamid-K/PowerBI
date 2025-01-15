using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Spreadsheet
{
	// Token: 0x02002879 RID: 10361
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NonVisualShapeProperties))]
	[ChildElementInfo(typeof(ShapeProperties))]
	[ChildElementInfo(typeof(ShapeStyle))]
	[ChildElementInfo(typeof(TextBody))]
	internal class Shape : OpenXmlCompositeElement
	{
		// Token: 0x170066DF RID: 26335
		// (get) Token: 0x0601447A RID: 83066 RVA: 0x002DF64E File Offset: 0x002DD84E
		public override string LocalName
		{
			get
			{
				return "sp";
			}
		}

		// Token: 0x170066E0 RID: 26336
		// (get) Token: 0x0601447B RID: 83067 RVA: 0x0012AF0D File Offset: 0x0012910D
		internal override byte NamespaceId
		{
			get
			{
				return 18;
			}
		}

		// Token: 0x170066E1 RID: 26337
		// (get) Token: 0x0601447C RID: 83068 RVA: 0x003117CE File Offset: 0x0030F9CE
		internal override int ElementTypeId
		{
			get
			{
				return 10723;
			}
		}

		// Token: 0x0601447D RID: 83069 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170066E2 RID: 26338
		// (get) Token: 0x0601447E RID: 83070 RVA: 0x003117D5 File Offset: 0x0030F9D5
		internal override string[] AttributeTagNames
		{
			get
			{
				return Shape.attributeTagNames;
			}
		}

		// Token: 0x170066E3 RID: 26339
		// (get) Token: 0x0601447F RID: 83071 RVA: 0x003117DC File Offset: 0x0030F9DC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Shape.attributeNamespaceIds;
			}
		}

		// Token: 0x170066E4 RID: 26340
		// (get) Token: 0x06014480 RID: 83072 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06014481 RID: 83073 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "macro")]
		public StringValue Macro
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

		// Token: 0x170066E5 RID: 26341
		// (get) Token: 0x06014482 RID: 83074 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06014483 RID: 83075 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "textlink")]
		public StringValue TextLink
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

		// Token: 0x170066E6 RID: 26342
		// (get) Token: 0x06014484 RID: 83076 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06014485 RID: 83077 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "fLocksText")]
		public BooleanValue LockText
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

		// Token: 0x170066E7 RID: 26343
		// (get) Token: 0x06014486 RID: 83078 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06014487 RID: 83079 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "fPublished")]
		public BooleanValue Published
		{
			get
			{
				return (BooleanValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x06014488 RID: 83080 RVA: 0x00293ECF File Offset: 0x002920CF
		public Shape()
		{
		}

		// Token: 0x06014489 RID: 83081 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Shape(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601448A RID: 83082 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Shape(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601448B RID: 83083 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Shape(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601448C RID: 83084 RVA: 0x003117E4 File Offset: 0x0030F9E4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (18 == namespaceId && "nvSpPr" == name)
			{
				return new NonVisualShapeProperties();
			}
			if (18 == namespaceId && "spPr" == name)
			{
				return new ShapeProperties();
			}
			if (18 == namespaceId && "style" == name)
			{
				return new ShapeStyle();
			}
			if (18 == namespaceId && "txBody" == name)
			{
				return new TextBody();
			}
			return null;
		}

		// Token: 0x170066E8 RID: 26344
		// (get) Token: 0x0601448D RID: 83085 RVA: 0x00311852 File Offset: 0x0030FA52
		internal override string[] ElementTagNames
		{
			get
			{
				return Shape.eleTagNames;
			}
		}

		// Token: 0x170066E9 RID: 26345
		// (get) Token: 0x0601448E RID: 83086 RVA: 0x00311859 File Offset: 0x0030FA59
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Shape.eleNamespaceIds;
			}
		}

		// Token: 0x170066EA RID: 26346
		// (get) Token: 0x0601448F RID: 83087 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170066EB RID: 26347
		// (get) Token: 0x06014490 RID: 83088 RVA: 0x00311860 File Offset: 0x0030FA60
		// (set) Token: 0x06014491 RID: 83089 RVA: 0x00311869 File Offset: 0x0030FA69
		public NonVisualShapeProperties NonVisualShapeProperties
		{
			get
			{
				return base.GetElement<NonVisualShapeProperties>(0);
			}
			set
			{
				base.SetElement<NonVisualShapeProperties>(0, value);
			}
		}

		// Token: 0x170066EC RID: 26348
		// (get) Token: 0x06014492 RID: 83090 RVA: 0x00311873 File Offset: 0x0030FA73
		// (set) Token: 0x06014493 RID: 83091 RVA: 0x0031187C File Offset: 0x0030FA7C
		public ShapeProperties ShapeProperties
		{
			get
			{
				return base.GetElement<ShapeProperties>(1);
			}
			set
			{
				base.SetElement<ShapeProperties>(1, value);
			}
		}

		// Token: 0x170066ED RID: 26349
		// (get) Token: 0x06014494 RID: 83092 RVA: 0x00311886 File Offset: 0x0030FA86
		// (set) Token: 0x06014495 RID: 83093 RVA: 0x0031188F File Offset: 0x0030FA8F
		public ShapeStyle ShapeStyle
		{
			get
			{
				return base.GetElement<ShapeStyle>(2);
			}
			set
			{
				base.SetElement<ShapeStyle>(2, value);
			}
		}

		// Token: 0x170066EE RID: 26350
		// (get) Token: 0x06014496 RID: 83094 RVA: 0x00311899 File Offset: 0x0030FA99
		// (set) Token: 0x06014497 RID: 83095 RVA: 0x003118A2 File Offset: 0x0030FAA2
		public TextBody TextBody
		{
			get
			{
				return base.GetElement<TextBody>(3);
			}
			set
			{
				base.SetElement<TextBody>(3, value);
			}
		}

		// Token: 0x06014498 RID: 83096 RVA: 0x003118AC File Offset: 0x0030FAAC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "macro" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "textlink" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "fLocksText" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "fPublished" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06014499 RID: 83097 RVA: 0x00311919 File Offset: 0x0030FB19
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Shape>(deep);
		}

		// Token: 0x0601449A RID: 83098 RVA: 0x00311924 File Offset: 0x0030FB24
		// Note: this type is marked as 'beforefieldinit'.
		static Shape()
		{
			byte[] array = new byte[4];
			Shape.attributeNamespaceIds = array;
			Shape.eleTagNames = new string[] { "nvSpPr", "spPr", "style", "txBody" };
			Shape.eleNamespaceIds = new byte[] { 18, 18, 18, 18 };
		}

		// Token: 0x04008D68 RID: 36200
		private const string tagName = "sp";

		// Token: 0x04008D69 RID: 36201
		private const byte tagNsId = 18;

		// Token: 0x04008D6A RID: 36202
		internal const int ElementTypeIdConst = 10723;

		// Token: 0x04008D6B RID: 36203
		private static string[] attributeTagNames = new string[] { "macro", "textlink", "fLocksText", "fPublished" };

		// Token: 0x04008D6C RID: 36204
		private static byte[] attributeNamespaceIds;

		// Token: 0x04008D6D RID: 36205
		private static readonly string[] eleTagNames;

		// Token: 0x04008D6E RID: 36206
		private static readonly byte[] eleNamespaceIds;
	}
}
