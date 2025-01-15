using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Pictures
{
	// Token: 0x02002379 RID: 9081
	[ChildElementInfo(typeof(EffectReference))]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(FillReference))]
	[ChildElementInfo(typeof(FontReference))]
	[ChildElementInfo(typeof(LineReference))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ShapeStyle : OpenXmlCompositeElement
	{
		// Token: 0x17004B1C RID: 19228
		// (get) Token: 0x060105F2 RID: 67058 RVA: 0x002DE36C File Offset: 0x002DC56C
		public override string LocalName
		{
			get
			{
				return "style";
			}
		}

		// Token: 0x17004B1D RID: 19229
		// (get) Token: 0x060105F3 RID: 67059 RVA: 0x002E2CB4 File Offset: 0x002E0EB4
		internal override byte NamespaceId
		{
			get
			{
				return 50;
			}
		}

		// Token: 0x17004B1E RID: 19230
		// (get) Token: 0x060105F4 RID: 67060 RVA: 0x002E2CB8 File Offset: 0x002E0EB8
		internal override int ElementTypeId
		{
			get
			{
				return 12820;
			}
		}

		// Token: 0x060105F5 RID: 67061 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060105F6 RID: 67062 RVA: 0x00293ECF File Offset: 0x002920CF
		public ShapeStyle()
		{
		}

		// Token: 0x060105F7 RID: 67063 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ShapeStyle(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060105F8 RID: 67064 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ShapeStyle(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060105F9 RID: 67065 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ShapeStyle(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060105FA RID: 67066 RVA: 0x002E2CC0 File Offset: 0x002E0EC0
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "lnRef" == name)
			{
				return new LineReference();
			}
			if (10 == namespaceId && "fillRef" == name)
			{
				return new FillReference();
			}
			if (10 == namespaceId && "effectRef" == name)
			{
				return new EffectReference();
			}
			if (10 == namespaceId && "fontRef" == name)
			{
				return new FontReference();
			}
			return null;
		}

		// Token: 0x17004B1F RID: 19231
		// (get) Token: 0x060105FB RID: 67067 RVA: 0x002E2D2E File Offset: 0x002E0F2E
		internal override string[] ElementTagNames
		{
			get
			{
				return ShapeStyle.eleTagNames;
			}
		}

		// Token: 0x17004B20 RID: 19232
		// (get) Token: 0x060105FC RID: 67068 RVA: 0x002E2D35 File Offset: 0x002E0F35
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ShapeStyle.eleNamespaceIds;
			}
		}

		// Token: 0x17004B21 RID: 19233
		// (get) Token: 0x060105FD RID: 67069 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004B22 RID: 19234
		// (get) Token: 0x060105FE RID: 67070 RVA: 0x002DEFCC File Offset: 0x002DD1CC
		// (set) Token: 0x060105FF RID: 67071 RVA: 0x002DEFD5 File Offset: 0x002DD1D5
		public LineReference LineReference
		{
			get
			{
				return base.GetElement<LineReference>(0);
			}
			set
			{
				base.SetElement<LineReference>(0, value);
			}
		}

		// Token: 0x17004B23 RID: 19235
		// (get) Token: 0x06010600 RID: 67072 RVA: 0x002DEFDF File Offset: 0x002DD1DF
		// (set) Token: 0x06010601 RID: 67073 RVA: 0x002DEFE8 File Offset: 0x002DD1E8
		public FillReference FillReference
		{
			get
			{
				return base.GetElement<FillReference>(1);
			}
			set
			{
				base.SetElement<FillReference>(1, value);
			}
		}

		// Token: 0x17004B24 RID: 19236
		// (get) Token: 0x06010602 RID: 67074 RVA: 0x002DEFF2 File Offset: 0x002DD1F2
		// (set) Token: 0x06010603 RID: 67075 RVA: 0x002DEFFB File Offset: 0x002DD1FB
		public EffectReference EffectReference
		{
			get
			{
				return base.GetElement<EffectReference>(2);
			}
			set
			{
				base.SetElement<EffectReference>(2, value);
			}
		}

		// Token: 0x17004B25 RID: 19237
		// (get) Token: 0x06010604 RID: 67076 RVA: 0x002DF005 File Offset: 0x002DD205
		// (set) Token: 0x06010605 RID: 67077 RVA: 0x002DF00E File Offset: 0x002DD20E
		public FontReference FontReference
		{
			get
			{
				return base.GetElement<FontReference>(3);
			}
			set
			{
				base.SetElement<FontReference>(3, value);
			}
		}

		// Token: 0x06010606 RID: 67078 RVA: 0x002E2D3C File Offset: 0x002E0F3C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ShapeStyle>(deep);
		}

		// Token: 0x04007452 RID: 29778
		private const string tagName = "style";

		// Token: 0x04007453 RID: 29779
		private const byte tagNsId = 50;

		// Token: 0x04007454 RID: 29780
		internal const int ElementTypeIdConst = 12820;

		// Token: 0x04007455 RID: 29781
		private static readonly string[] eleTagNames = new string[] { "lnRef", "fillRef", "effectRef", "fontRef" };

		// Token: 0x04007456 RID: 29782
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10, 10, 10 };
	}
}
