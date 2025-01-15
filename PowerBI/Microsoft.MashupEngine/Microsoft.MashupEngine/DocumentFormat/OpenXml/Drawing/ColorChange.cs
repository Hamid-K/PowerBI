using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002714 RID: 10004
	[ChildElementInfo(typeof(ColorTo))]
	[ChildElementInfo(typeof(ColorFrom))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ColorChange : OpenXmlCompositeElement
	{
		// Token: 0x17005EF4 RID: 24308
		// (get) Token: 0x06013249 RID: 78409 RVA: 0x0030418B File Offset: 0x0030238B
		public override string LocalName
		{
			get
			{
				return "clrChange";
			}
		}

		// Token: 0x17005EF5 RID: 24309
		// (get) Token: 0x0601324A RID: 78410 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005EF6 RID: 24310
		// (get) Token: 0x0601324B RID: 78411 RVA: 0x00304192 File Offset: 0x00302392
		internal override int ElementTypeId
		{
			get
			{
				return 10066;
			}
		}

		// Token: 0x0601324C RID: 78412 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005EF7 RID: 24311
		// (get) Token: 0x0601324D RID: 78413 RVA: 0x00304199 File Offset: 0x00302399
		internal override string[] AttributeTagNames
		{
			get
			{
				return ColorChange.attributeTagNames;
			}
		}

		// Token: 0x17005EF8 RID: 24312
		// (get) Token: 0x0601324E RID: 78414 RVA: 0x003041A0 File Offset: 0x003023A0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ColorChange.attributeNamespaceIds;
			}
		}

		// Token: 0x17005EF9 RID: 24313
		// (get) Token: 0x0601324F RID: 78415 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06013250 RID: 78416 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "useA")]
		public BooleanValue UseAlpha
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

		// Token: 0x06013251 RID: 78417 RVA: 0x00293ECF File Offset: 0x002920CF
		public ColorChange()
		{
		}

		// Token: 0x06013252 RID: 78418 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ColorChange(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013253 RID: 78419 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ColorChange(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013254 RID: 78420 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ColorChange(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013255 RID: 78421 RVA: 0x003041A7 File Offset: 0x003023A7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "clrFrom" == name)
			{
				return new ColorFrom();
			}
			if (10 == namespaceId && "clrTo" == name)
			{
				return new ColorTo();
			}
			return null;
		}

		// Token: 0x17005EFA RID: 24314
		// (get) Token: 0x06013256 RID: 78422 RVA: 0x003041DA File Offset: 0x003023DA
		internal override string[] ElementTagNames
		{
			get
			{
				return ColorChange.eleTagNames;
			}
		}

		// Token: 0x17005EFB RID: 24315
		// (get) Token: 0x06013257 RID: 78423 RVA: 0x003041E1 File Offset: 0x003023E1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ColorChange.eleNamespaceIds;
			}
		}

		// Token: 0x17005EFC RID: 24316
		// (get) Token: 0x06013258 RID: 78424 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005EFD RID: 24317
		// (get) Token: 0x06013259 RID: 78425 RVA: 0x003041E8 File Offset: 0x003023E8
		// (set) Token: 0x0601325A RID: 78426 RVA: 0x003041F1 File Offset: 0x003023F1
		public ColorFrom ColorFrom
		{
			get
			{
				return base.GetElement<ColorFrom>(0);
			}
			set
			{
				base.SetElement<ColorFrom>(0, value);
			}
		}

		// Token: 0x17005EFE RID: 24318
		// (get) Token: 0x0601325B RID: 78427 RVA: 0x003041FB File Offset: 0x003023FB
		// (set) Token: 0x0601325C RID: 78428 RVA: 0x00304204 File Offset: 0x00302404
		public ColorTo ColorTo
		{
			get
			{
				return base.GetElement<ColorTo>(1);
			}
			set
			{
				base.SetElement<ColorTo>(1, value);
			}
		}

		// Token: 0x0601325D RID: 78429 RVA: 0x0030420E File Offset: 0x0030240E
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "useA" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601325E RID: 78430 RVA: 0x0030422E File Offset: 0x0030242E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ColorChange>(deep);
		}

		// Token: 0x0601325F RID: 78431 RVA: 0x00304238 File Offset: 0x00302438
		// Note: this type is marked as 'beforefieldinit'.
		static ColorChange()
		{
			byte[] array = new byte[1];
			ColorChange.attributeNamespaceIds = array;
			ColorChange.eleTagNames = new string[] { "clrFrom", "clrTo" };
			ColorChange.eleNamespaceIds = new byte[] { 10, 10 };
		}

		// Token: 0x040084E7 RID: 34023
		private const string tagName = "clrChange";

		// Token: 0x040084E8 RID: 34024
		private const byte tagNsId = 10;

		// Token: 0x040084E9 RID: 34025
		internal const int ElementTypeIdConst = 10066;

		// Token: 0x040084EA RID: 34026
		private static string[] attributeTagNames = new string[] { "useA" };

		// Token: 0x040084EB RID: 34027
		private static byte[] attributeNamespaceIds;

		// Token: 0x040084EC RID: 34028
		private static readonly string[] eleTagNames;

		// Token: 0x040084ED RID: 34029
		private static readonly byte[] eleNamespaceIds;
	}
}
