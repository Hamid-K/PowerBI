using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A82 RID: 10882
	[ChildElementInfo(typeof(Origin))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ScaleFactor))]
	internal class CommonViewProperties : OpenXmlCompositeElement
	{
		// Token: 0x17007357 RID: 29527
		// (get) Token: 0x060160AB RID: 90283 RVA: 0x00325F1F File Offset: 0x0032411F
		public override string LocalName
		{
			get
			{
				return "cViewPr";
			}
		}

		// Token: 0x17007358 RID: 29528
		// (get) Token: 0x060160AC RID: 90284 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007359 RID: 29529
		// (get) Token: 0x060160AD RID: 90285 RVA: 0x00325F26 File Offset: 0x00324126
		internal override int ElementTypeId
		{
			get
			{
				return 12295;
			}
		}

		// Token: 0x060160AE RID: 90286 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700735A RID: 29530
		// (get) Token: 0x060160AF RID: 90287 RVA: 0x00325F2D File Offset: 0x0032412D
		internal override string[] AttributeTagNames
		{
			get
			{
				return CommonViewProperties.attributeTagNames;
			}
		}

		// Token: 0x1700735B RID: 29531
		// (get) Token: 0x060160B0 RID: 90288 RVA: 0x00325F34 File Offset: 0x00324134
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return CommonViewProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x1700735C RID: 29532
		// (get) Token: 0x060160B1 RID: 90289 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x060160B2 RID: 90290 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "varScale")]
		public BooleanValue VariableScale
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

		// Token: 0x060160B3 RID: 90291 RVA: 0x00293ECF File Offset: 0x002920CF
		public CommonViewProperties()
		{
		}

		// Token: 0x060160B4 RID: 90292 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CommonViewProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060160B5 RID: 90293 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CommonViewProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060160B6 RID: 90294 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CommonViewProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060160B7 RID: 90295 RVA: 0x00325F3B File Offset: 0x0032413B
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "scale" == name)
			{
				return new ScaleFactor();
			}
			if (24 == namespaceId && "origin" == name)
			{
				return new Origin();
			}
			return null;
		}

		// Token: 0x1700735D RID: 29533
		// (get) Token: 0x060160B8 RID: 90296 RVA: 0x00325F6E File Offset: 0x0032416E
		internal override string[] ElementTagNames
		{
			get
			{
				return CommonViewProperties.eleTagNames;
			}
		}

		// Token: 0x1700735E RID: 29534
		// (get) Token: 0x060160B9 RID: 90297 RVA: 0x00325F75 File Offset: 0x00324175
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CommonViewProperties.eleNamespaceIds;
			}
		}

		// Token: 0x1700735F RID: 29535
		// (get) Token: 0x060160BA RID: 90298 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007360 RID: 29536
		// (get) Token: 0x060160BB RID: 90299 RVA: 0x00325F7C File Offset: 0x0032417C
		// (set) Token: 0x060160BC RID: 90300 RVA: 0x00325F85 File Offset: 0x00324185
		public ScaleFactor ScaleFactor
		{
			get
			{
				return base.GetElement<ScaleFactor>(0);
			}
			set
			{
				base.SetElement<ScaleFactor>(0, value);
			}
		}

		// Token: 0x17007361 RID: 29537
		// (get) Token: 0x060160BD RID: 90301 RVA: 0x00325F8F File Offset: 0x0032418F
		// (set) Token: 0x060160BE RID: 90302 RVA: 0x00325F98 File Offset: 0x00324198
		public Origin Origin
		{
			get
			{
				return base.GetElement<Origin>(1);
			}
			set
			{
				base.SetElement<Origin>(1, value);
			}
		}

		// Token: 0x060160BF RID: 90303 RVA: 0x00325FA2 File Offset: 0x003241A2
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "varScale" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060160C0 RID: 90304 RVA: 0x00325FC2 File Offset: 0x003241C2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CommonViewProperties>(deep);
		}

		// Token: 0x060160C1 RID: 90305 RVA: 0x00325FCC File Offset: 0x003241CC
		// Note: this type is marked as 'beforefieldinit'.
		static CommonViewProperties()
		{
			byte[] array = new byte[1];
			CommonViewProperties.attributeNamespaceIds = array;
			CommonViewProperties.eleTagNames = new string[] { "scale", "origin" };
			CommonViewProperties.eleNamespaceIds = new byte[] { 24, 24 };
		}

		// Token: 0x040095F1 RID: 38385
		private const string tagName = "cViewPr";

		// Token: 0x040095F2 RID: 38386
		private const byte tagNsId = 24;

		// Token: 0x040095F3 RID: 38387
		internal const int ElementTypeIdConst = 12295;

		// Token: 0x040095F4 RID: 38388
		private static string[] attributeTagNames = new string[] { "varScale" };

		// Token: 0x040095F5 RID: 38389
		private static byte[] attributeNamespaceIds;

		// Token: 0x040095F6 RID: 38390
		private static readonly string[] eleTagNames;

		// Token: 0x040095F7 RID: 38391
		private static readonly byte[] eleNamespaceIds;
	}
}
