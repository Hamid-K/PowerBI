using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing
{
	// Token: 0x02002353 RID: 9043
	[ChildElementInfo(typeof(ContentPartLocks), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(OfficeArtExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class NonVisualInkContentPartPropertiesType : OpenXmlCompositeElement
	{
		// Token: 0x170049FE RID: 18942
		// (get) Token: 0x0601039B RID: 66459 RVA: 0x002E137E File Offset: 0x002DF57E
		internal override string[] AttributeTagNames
		{
			get
			{
				return NonVisualInkContentPartPropertiesType.attributeTagNames;
			}
		}

		// Token: 0x170049FF RID: 18943
		// (get) Token: 0x0601039C RID: 66460 RVA: 0x002E1385 File Offset: 0x002DF585
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NonVisualInkContentPartPropertiesType.attributeNamespaceIds;
			}
		}

		// Token: 0x17004A00 RID: 18944
		// (get) Token: 0x0601039D RID: 66461 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x0601039E RID: 66462 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "isComment")]
		public BooleanValue IsComment
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

		// Token: 0x0601039F RID: 66463 RVA: 0x002DFD24 File Offset: 0x002DDF24
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (48 == namespaceId && "cpLocks" == name)
			{
				return new ContentPartLocks();
			}
			if (48 == namespaceId && "extLst" == name)
			{
				return new OfficeArtExtensionList();
			}
			return null;
		}

		// Token: 0x17004A01 RID: 18945
		// (get) Token: 0x060103A0 RID: 66464 RVA: 0x002E138C File Offset: 0x002DF58C
		internal override string[] ElementTagNames
		{
			get
			{
				return NonVisualInkContentPartPropertiesType.eleTagNames;
			}
		}

		// Token: 0x17004A02 RID: 18946
		// (get) Token: 0x060103A1 RID: 66465 RVA: 0x002E1393 File Offset: 0x002DF593
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NonVisualInkContentPartPropertiesType.eleNamespaceIds;
			}
		}

		// Token: 0x17004A03 RID: 18947
		// (get) Token: 0x060103A2 RID: 66466 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004A04 RID: 18948
		// (get) Token: 0x060103A3 RID: 66467 RVA: 0x002DFD65 File Offset: 0x002DDF65
		// (set) Token: 0x060103A4 RID: 66468 RVA: 0x002DFD6E File Offset: 0x002DDF6E
		public ContentPartLocks ContentPartLocks
		{
			get
			{
				return base.GetElement<ContentPartLocks>(0);
			}
			set
			{
				base.SetElement<ContentPartLocks>(0, value);
			}
		}

		// Token: 0x17004A05 RID: 18949
		// (get) Token: 0x060103A5 RID: 66469 RVA: 0x002DFD78 File Offset: 0x002DDF78
		// (set) Token: 0x060103A6 RID: 66470 RVA: 0x002DFD81 File Offset: 0x002DDF81
		public OfficeArtExtensionList OfficeArtExtensionList
		{
			get
			{
				return base.GetElement<OfficeArtExtensionList>(1);
			}
			set
			{
				base.SetElement<OfficeArtExtensionList>(1, value);
			}
		}

		// Token: 0x060103A7 RID: 66471 RVA: 0x002DFD8B File Offset: 0x002DDF8B
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "isComment" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060103A8 RID: 66472 RVA: 0x00293ECF File Offset: 0x002920CF
		protected NonVisualInkContentPartPropertiesType()
		{
		}

		// Token: 0x060103A9 RID: 66473 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected NonVisualInkContentPartPropertiesType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060103AA RID: 66474 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected NonVisualInkContentPartPropertiesType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060103AB RID: 66475 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected NonVisualInkContentPartPropertiesType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060103AC RID: 66476 RVA: 0x002E139C File Offset: 0x002DF59C
		// Note: this type is marked as 'beforefieldinit'.
		static NonVisualInkContentPartPropertiesType()
		{
			byte[] array = new byte[1];
			NonVisualInkContentPartPropertiesType.attributeNamespaceIds = array;
			NonVisualInkContentPartPropertiesType.eleTagNames = new string[] { "cpLocks", "extLst" };
			NonVisualInkContentPartPropertiesType.eleNamespaceIds = new byte[] { 48, 48 };
		}

		// Token: 0x0400739C RID: 29596
		private static string[] attributeTagNames = new string[] { "isComment" };

		// Token: 0x0400739D RID: 29597
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400739E RID: 29598
		private static readonly string[] eleTagNames;

		// Token: 0x0400739F RID: 29599
		private static readonly byte[] eleNamespaceIds;
	}
}
