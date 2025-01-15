using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002352 RID: 9042
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(HyperlinkOnClick))]
	[ChildElementInfo(typeof(HyperlinkOnHover))]
	[ChildElementInfo(typeof(NonVisualDrawingPropertiesExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class NonVisualDrawingProperties : OpenXmlCompositeElement
	{
		// Token: 0x170049EE RID: 18926
		// (get) Token: 0x0601037A RID: 66426 RVA: 0x002DE917 File Offset: 0x002DCB17
		public override string LocalName
		{
			get
			{
				return "cNvPr";
			}
		}

		// Token: 0x170049EF RID: 18927
		// (get) Token: 0x0601037B RID: 66427 RVA: 0x002E03A2 File Offset: 0x002DE5A2
		internal override byte NamespaceId
		{
			get
			{
				return 48;
			}
		}

		// Token: 0x170049F0 RID: 18928
		// (get) Token: 0x0601037C RID: 66428 RVA: 0x002E11ED File Offset: 0x002DF3ED
		internal override int ElementTypeId
		{
			get
			{
				return 12727;
			}
		}

		// Token: 0x0601037D RID: 66429 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170049F1 RID: 18929
		// (get) Token: 0x0601037E RID: 66430 RVA: 0x002E11F4 File Offset: 0x002DF3F4
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualDrawingProperties.attributeTagNames;
			}
		}

		// Token: 0x170049F2 RID: 18930
		// (get) Token: 0x0601037F RID: 66431 RVA: 0x002E11FB File Offset: 0x002DF3FB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170049F3 RID: 18931
		// (get) Token: 0x06010380 RID: 66432 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06010381 RID: 66433 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170049F4 RID: 18932
		// (get) Token: 0x06010382 RID: 66434 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06010383 RID: 66435 RVA: 0x002BD47A File Offset: 0x002BB67A
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

		// Token: 0x170049F5 RID: 18933
		// (get) Token: 0x06010384 RID: 66436 RVA: 0x002BD485 File Offset: 0x002BB685
		// (set) Token: 0x06010385 RID: 66437 RVA: 0x002BD494 File Offset: 0x002BB694
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

		// Token: 0x170049F6 RID: 18934
		// (get) Token: 0x06010386 RID: 66438 RVA: 0x002CB9D9 File Offset: 0x002C9BD9
		// (set) Token: 0x06010387 RID: 66439 RVA: 0x002BD4AE File Offset: 0x002BB6AE
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

		// Token: 0x170049F7 RID: 18935
		// (get) Token: 0x06010388 RID: 66440 RVA: 0x002BD4B9 File Offset: 0x002BB6B9
		// (set) Token: 0x06010389 RID: 66441 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
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

		// Token: 0x0601038A RID: 66442 RVA: 0x00293ECF File Offset: 0x002920CF
		public NonVisualDrawingProperties()
		{
		}

		// Token: 0x0601038B RID: 66443 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NonVisualDrawingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601038C RID: 66444 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NonVisualDrawingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601038D RID: 66445 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NonVisualDrawingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601038E RID: 66446 RVA: 0x002E1204 File Offset: 0x002DF404
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

		// Token: 0x170049F8 RID: 18936
		// (get) Token: 0x0601038F RID: 66447 RVA: 0x002E125A File Offset: 0x002DF45A
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualDrawingProperties.eleTagNames;
			}
		}

		// Token: 0x170049F9 RID: 18937
		// (get) Token: 0x06010390 RID: 66448 RVA: 0x002E1261 File Offset: 0x002DF461
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualDrawingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170049FA RID: 18938
		// (get) Token: 0x06010391 RID: 66449 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170049FB RID: 18939
		// (get) Token: 0x06010392 RID: 66450 RVA: 0x002DE9A8 File Offset: 0x002DCBA8
		// (set) Token: 0x06010393 RID: 66451 RVA: 0x002DE9B1 File Offset: 0x002DCBB1
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

		// Token: 0x170049FC RID: 18940
		// (get) Token: 0x06010394 RID: 66452 RVA: 0x002DE9BB File Offset: 0x002DCBBB
		// (set) Token: 0x06010395 RID: 66453 RVA: 0x002DE9C4 File Offset: 0x002DCBC4
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

		// Token: 0x170049FD RID: 18941
		// (get) Token: 0x06010396 RID: 66454 RVA: 0x002DE9CE File Offset: 0x002DCBCE
		// (set) Token: 0x06010397 RID: 66455 RVA: 0x002DE9D7 File Offset: 0x002DCBD7
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

		// Token: 0x06010398 RID: 66456 RVA: 0x002E1268 File Offset: 0x002DF468
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

		// Token: 0x06010399 RID: 66457 RVA: 0x002E12EB File Offset: 0x002DF4EB
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NonVisualDrawingProperties>(deep);
		}

		// Token: 0x0601039A RID: 66458 RVA: 0x002E12F4 File Offset: 0x002DF4F4
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualDrawingProperties()
		{
			byte[] array = new byte[5];
			NonVisualDrawingProperties.attributeNamespaceIds = array;
			NonVisualDrawingProperties.eleTagNames = new string[] { "hlinkClick", "hlinkHover", "extLst" };
			NonVisualDrawingProperties.eleNamespaceIds = new byte[] { 10, 10, 10 };
		}

		// Token: 0x04007395 RID: 29589
		private const string tagName = "cNvPr";

		// Token: 0x04007396 RID: 29590
		private const byte tagNsId = 48;

		// Token: 0x04007397 RID: 29591
		internal const int ElementTypeIdConst = 12727;

		// Token: 0x04007398 RID: 29592
		private static string[] attributeTagNames = new string[] { "id", "name", "descr", "hidden", "title" };

		// Token: 0x04007399 RID: 29593
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400739A RID: 29594
		private static readonly string[] eleTagNames;

		// Token: 0x0400739B RID: 29595
		private static readonly byte[] eleNamespaceIds;
	}
}
