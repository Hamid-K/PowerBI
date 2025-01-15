using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingGroup
{
	// Token: 0x020024F2 RID: 9458
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(HyperlinkOnHover))]
	[ChildElementInfo(typeof(NonVisualDrawingPropertiesExtensionList))]
	[ChildElementInfo(typeof(HyperlinkOnClick))]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class NonVisualDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x17005355 RID: 21333
		// (get) Token: 0x0601187E RID: 71806 RVA: 0x002DE917 File Offset: 0x002DCB17
		public override string LocalName
		{
			get
			{
				return "cNvPr";
			}
		}

		// Token: 0x17005356 RID: 21334
		// (get) Token: 0x0601187F RID: 71807 RVA: 0x002EF715 File Offset: 0x002ED915
		internal override byte NamespaceId
		{
			get
			{
				return 60;
			}
		}

		// Token: 0x17005357 RID: 21335
		// (get) Token: 0x06011880 RID: 71808 RVA: 0x002EF75C File Offset: 0x002ED95C
		internal override int ElementTypeId
		{
			get
			{
				return 13123;
			}
		}

		// Token: 0x06011881 RID: 71809 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17005358 RID: 21336
		// (get) Token: 0x06011882 RID: 71810 RVA: 0x002EF763 File Offset: 0x002ED963
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x17005359 RID: 21337
		// (get) Token: 0x06011883 RID: 71811 RVA: 0x002EF76A File Offset: 0x002ED96A
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700535A RID: 21338
		// (get) Token: 0x06011884 RID: 71812 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06011885 RID: 71813 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "id")]
		public UInt32Value Id
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700535B RID: 21339
		// (get) Token: 0x06011886 RID: 71814 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06011887 RID: 71815 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "name")]
		public StringValue Name
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

		// Token: 0x1700535C RID: 21340
		// (get) Token: 0x06011888 RID: 71816 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06011889 RID: 71817 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(0, "descr")]
		public StringValue Description
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

		// Token: 0x1700535D RID: 21341
		// (get) Token: 0x0601188A RID: 71818 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x0601188B RID: 71819 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(0, "hidden")]
		public BooleanValue Hidden
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

		// Token: 0x1700535E RID: 21342
		// (get) Token: 0x0601188C RID: 71820 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x0601188D RID: 71821 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(0, "title")]
		public StringValue Title
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

		// Token: 0x0601188E RID: 71822 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingProperties()
		{
		}

		// Token: 0x0601188F RID: 71823 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011890 RID: 71824 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011891 RID: 71825 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011892 RID: 71826 RVA: 0x002EF774 File Offset: 0x002ED974
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "hlinkClick" == name)
			{
				return new HyperlinkOnClick();
			}
			if (10 == namespaceId && "hlinkHover" == name)
			{
				return new HyperlinkOnHover();
			}
			if (10 == namespaceId && "extLst" == name)
			{
				return new NonVisualDrawingPropertiesExtensionList();
			}
			return null;
		}

		// Token: 0x1700535F RID: 21343
		// (get) Token: 0x06011893 RID: 71827 RVA: 0x002EF7CA File Offset: 0x002ED9CA
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x17005360 RID: 21344
		// (get) Token: 0x06011894 RID: 71828 RVA: 0x002EF7D1 File Offset: 0x002ED9D1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17005361 RID: 21345
		// (get) Token: 0x06011895 RID: 71829 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005362 RID: 21346
		// (get) Token: 0x06011896 RID: 71830 RVA: 0x002DE9A8 File Offset: 0x002DCBA8
		// (set) Token: 0x06011897 RID: 71831 RVA: 0x002DE9B1 File Offset: 0x002DCBB1
		public HyperlinkOnClick HyperlinkOnClick
		{
			get
			{
				return base.GetElement<HyperlinkOnClick>(0);
			}
			set
			{
				base.SetElement<HyperlinkOnClick>(0, value);
			}
		}

		// Token: 0x17005363 RID: 21347
		// (get) Token: 0x06011898 RID: 71832 RVA: 0x002DE9BB File Offset: 0x002DCBBB
		// (set) Token: 0x06011899 RID: 71833 RVA: 0x002DE9C4 File Offset: 0x002DCBC4
		public HyperlinkOnHover HyperlinkOnHover
		{
			get
			{
				return base.GetElement<HyperlinkOnHover>(1);
			}
			set
			{
				base.SetElement<HyperlinkOnHover>(1, value);
			}
		}

		// Token: 0x17005364 RID: 21348
		// (get) Token: 0x0601189A RID: 71834 RVA: 0x002DE9CE File Offset: 0x002DCBCE
		// (set) Token: 0x0601189B RID: 71835 RVA: 0x002DE9D7 File Offset: 0x002DCBD7
		public NonVisualDrawingPropertiesExtensionList NonVisualDrawingPropertiesExtensionList
		{
			get
			{
				return base.GetElement<NonVisualDrawingPropertiesExtensionList>(2);
			}
			set
			{
				base.SetElement<NonVisualDrawingPropertiesExtensionList>(2, value);
			}
		}

		// Token: 0x0601189C RID: 71836 RVA: 0x002EF7D8 File Offset: 0x002ED9D8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "id" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "name" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "descr" == name)
			{
				return new StringValue();
			}
			if (namespaceId == 0 && "hidden" == name)
			{
				return new BooleanValue();
			}
			if (namespaceId == 0 && "title" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601189D RID: 71837 RVA: 0x002EF85B File Offset: 0x002EDA5B
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingProperties>(deep);
		}

		// Token: 0x0601189E RID: 71838 RVA: 0x002EF864 File Offset: 0x002EDA64
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualDrawingProperties()
		{
			byte[] array = new byte[5];
			NonVisualDrawingProperties.attributeNamespaceIds = array;
			NonVisualDrawingProperties.eleTagNames = new string[] { "hlinkClick", "hlinkHover", "extLst" };
			NonVisualDrawingProperties.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x04007B32 RID: 31538
		private const string tagName = "cNvPr";

		// Token: 0x04007B33 RID: 31539
		private const byte tagNsId = 60;

		// Token: 0x04007B34 RID: 31540
		internal const int ElementTypeIdConst = 13123;

		// Token: 0x04007B35 RID: 31541
		private static string[] attributeTagNames = new string[] { "id", "name", "descr", "hidden", "title" };

		// Token: 0x04007B36 RID: 31542
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007B37 RID: 31543
		private static readonly string[] eleTagNames;

		// Token: 0x04007B38 RID: 31544
		private static readonly byte[] eleNamespaceIds;
	}
}
