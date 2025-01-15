using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A45 RID: 10821
	[ChildElementInfo(typeof(HslColor))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RgbColor))]
	internal class ByColor : OpenXmlCompositeElement
	{
		// Token: 0x1700717A RID: 29050
		// (get) Token: 0x06015C95 RID: 89237 RVA: 0x0032321F File Offset: 0x0032141F
		public override string LocalName
		{
			get
			{
				return "by";
			}
		}

		// Token: 0x1700717B RID: 29051
		// (get) Token: 0x06015C96 RID: 89238 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700717C RID: 29052
		// (get) Token: 0x06015C97 RID: 89239 RVA: 0x00323226 File Offset: 0x00321426
		internal override int ElementTypeId
		{
			get
			{
				return 12240;
			}
		}

		// Token: 0x06015C98 RID: 89240 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015C99 RID: 89241 RVA: 0x00293ECF File Offset: 0x002920CF
		public ByColor()
		{
		}

		// Token: 0x06015C9A RID: 89242 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ByColor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015C9B RID: 89243 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ByColor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015C9C RID: 89244 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ByColor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015C9D RID: 89245 RVA: 0x0032322D File Offset: 0x0032142D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "rgb" == name)
			{
				return new RgbColor();
			}
			if (24 == namespaceId && "hsl" == name)
			{
				return new HslColor();
			}
			return null;
		}

		// Token: 0x1700717D RID: 29053
		// (get) Token: 0x06015C9E RID: 89246 RVA: 0x00323260 File Offset: 0x00321460
		internal override string[] ElementTagNames
		{
			get
			{
				return ByColor.eleTagNames;
			}
		}

		// Token: 0x1700717E RID: 29054
		// (get) Token: 0x06015C9F RID: 89247 RVA: 0x00323267 File Offset: 0x00321467
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ByColor.eleNamespaceIds;
			}
		}

		// Token: 0x1700717F RID: 29055
		// (get) Token: 0x06015CA0 RID: 89248 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17007180 RID: 29056
		// (get) Token: 0x06015CA1 RID: 89249 RVA: 0x0032326E File Offset: 0x0032146E
		// (set) Token: 0x06015CA2 RID: 89250 RVA: 0x00323277 File Offset: 0x00321477
		public RgbColor RgbColor
		{
			get
			{
				return base.GetElement<RgbColor>(0);
			}
			set
			{
				base.SetElement<RgbColor>(0, value);
			}
		}

		// Token: 0x17007181 RID: 29057
		// (get) Token: 0x06015CA3 RID: 89251 RVA: 0x00323281 File Offset: 0x00321481
		// (set) Token: 0x06015CA4 RID: 89252 RVA: 0x0032328A File Offset: 0x0032148A
		public HslColor HslColor
		{
			get
			{
				return base.GetElement<HslColor>(1);
			}
			set
			{
				base.SetElement<HslColor>(1, value);
			}
		}

		// Token: 0x06015CA5 RID: 89253 RVA: 0x00323294 File Offset: 0x00321494
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ByColor>(deep);
		}

		// Token: 0x040094CF RID: 38095
		private const string tagName = "by";

		// Token: 0x040094D0 RID: 38096
		private const byte tagNsId = 24;

		// Token: 0x040094D1 RID: 38097
		internal const int ElementTypeIdConst = 12240;

		// Token: 0x040094D2 RID: 38098
		private static readonly string[] eleTagNames = new string[] { "rgb", "hsl" };

		// Token: 0x040094D3 RID: 38099
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24 };
	}
}
