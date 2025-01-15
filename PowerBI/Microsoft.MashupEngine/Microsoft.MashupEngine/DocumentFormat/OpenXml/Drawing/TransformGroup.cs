using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020027DD RID: 10205
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Extents))]
	[ChildElementInfo(typeof(ChildExtents))]
	[ChildElementInfo(typeof(Offset))]
	[ChildElementInfo(typeof(ChildOffset))]
	internal class TransformGroup : OpenXmlCompositeElement
	{
		// Token: 0x17006405 RID: 25605
		// (get) Token: 0x06013DA8 RID: 81320 RVA: 0x002E002B File Offset: 0x002DE22B
		public override string LocalName
		{
			get
			{
				return "xfrm";
			}
		}

		// Token: 0x17006406 RID: 25606
		// (get) Token: 0x06013DA9 RID: 81321 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006407 RID: 25607
		// (get) Token: 0x06013DAA RID: 81322 RVA: 0x0030C50D File Offset: 0x0030A70D
		internal override int ElementTypeId
		{
			get
			{
				return 10237;
			}
		}

		// Token: 0x06013DAB RID: 81323 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17006408 RID: 25608
		// (get) Token: 0x06013DAC RID: 81324 RVA: 0x0030C514 File Offset: 0x0030A714
		internal override string[] AttributeTagNames
		{
			get
			{
				return TransformGroup.attributeTagNames;
			}
		}

		// Token: 0x17006409 RID: 25609
		// (get) Token: 0x06013DAD RID: 81325 RVA: 0x0030C51B File Offset: 0x0030A71B
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return TransformGroup.attributeNamespaceIds;
			}
		}

		// Token: 0x1700640A RID: 25610
		// (get) Token: 0x06013DAE RID: 81326 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x06013DAF RID: 81327 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x1700640B RID: 25611
		// (get) Token: 0x06013DB0 RID: 81328 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06013DB1 RID: 81329 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x1700640C RID: 25612
		// (get) Token: 0x06013DB2 RID: 81330 RVA: 0x002C8F66 File Offset: 0x002C7166
		// (set) Token: 0x06013DB3 RID: 81331 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x06013DB4 RID: 81332 RVA: 0x00293ECF File Offset: 0x002920CF
		public TransformGroup()
		{
		}

		// Token: 0x06013DB5 RID: 81333 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TransformGroup(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013DB6 RID: 81334 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TransformGroup(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013DB7 RID: 81335 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TransformGroup(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013DB8 RID: 81336 RVA: 0x0030C524 File Offset: 0x0030A724
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
			if (10 == namespaceId && "chOff" == name)
			{
				return new ChildOffset();
			}
			if (10 == namespaceId && "chExt" == name)
			{
				return new ChildExtents();
			}
			return null;
		}

		// Token: 0x1700640D RID: 25613
		// (get) Token: 0x06013DB9 RID: 81337 RVA: 0x0030C592 File Offset: 0x0030A792
		internal override string[] ElementTagNames
		{
			get
			{
				return TransformGroup.eleTagNames;
			}
		}

		// Token: 0x1700640E RID: 25614
		// (get) Token: 0x06013DBA RID: 81338 RVA: 0x0030C599 File Offset: 0x0030A799
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TransformGroup.eleNamespaceIds;
			}
		}

		// Token: 0x1700640F RID: 25615
		// (get) Token: 0x06013DBB RID: 81339 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006410 RID: 25616
		// (get) Token: 0x06013DBC RID: 81340 RVA: 0x002DF1BD File Offset: 0x002DD3BD
		// (set) Token: 0x06013DBD RID: 81341 RVA: 0x002DF1C6 File Offset: 0x002DD3C6
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

		// Token: 0x17006411 RID: 25617
		// (get) Token: 0x06013DBE RID: 81342 RVA: 0x002DF1D0 File Offset: 0x002DD3D0
		// (set) Token: 0x06013DBF RID: 81343 RVA: 0x002DF1D9 File Offset: 0x002DD3D9
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

		// Token: 0x17006412 RID: 25618
		// (get) Token: 0x06013DC0 RID: 81344 RVA: 0x0030C5A0 File Offset: 0x0030A7A0
		// (set) Token: 0x06013DC1 RID: 81345 RVA: 0x0030C5A9 File Offset: 0x0030A7A9
		public ChildOffset ChildOffset
		{
			get
			{
				return base.GetElement<ChildOffset>(2);
			}
			set
			{
				base.SetElement<ChildOffset>(2, value);
			}
		}

		// Token: 0x17006413 RID: 25619
		// (get) Token: 0x06013DC2 RID: 81346 RVA: 0x0030C5B3 File Offset: 0x0030A7B3
		// (set) Token: 0x06013DC3 RID: 81347 RVA: 0x0030C5BC File Offset: 0x0030A7BC
		public ChildExtents ChildExtents
		{
			get
			{
				return base.GetElement<ChildExtents>(3);
			}
			set
			{
				base.SetElement<ChildExtents>(3, value);
			}
		}

		// Token: 0x06013DC4 RID: 81348 RVA: 0x0030C5C8 File Offset: 0x0030A7C8
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

		// Token: 0x06013DC5 RID: 81349 RVA: 0x0030C61F File Offset: 0x0030A81F
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TransformGroup>(deep);
		}

		// Token: 0x06013DC6 RID: 81350 RVA: 0x0030C628 File Offset: 0x0030A828
		// Note: this type is marked as 'beforefieldinit'.
		static TransformGroup()
		{
			byte[] array = new byte[3];
			TransformGroup.attributeNamespaceIds = array;
			TransformGroup.eleTagNames = new string[] { "off", "ext", "chOff", "chExt" };
			TransformGroup.eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
		}

		// Token: 0x04008817 RID: 34839
		private const string tagName = "xfrm";

		// Token: 0x04008818 RID: 34840
		private const byte tagNsId = 10;

		// Token: 0x04008819 RID: 34841
		internal const int ElementTypeIdConst = 10237;

		// Token: 0x0400881A RID: 34842
		private static string[] attributeTagNames = new string[] { "rot", "flipH", "flipV" };

		// Token: 0x0400881B RID: 34843
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400881C RID: 34844
		private static readonly string[] eleTagNames;

		// Token: 0x0400881D RID: 34845
		private static readonly byte[] eleNamespaceIds;
	}
}
