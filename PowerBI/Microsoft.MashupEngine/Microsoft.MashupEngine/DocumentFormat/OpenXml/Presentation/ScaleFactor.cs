using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A7E RID: 10878
	[ChildElementInfo(typeof(ScaleY))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ScaleX))]
	internal class ScaleFactor : OpenXmlCompositeElement
	{
		// Token: 0x17007345 RID: 29509
		// (get) Token: 0x06016084 RID: 90244 RVA: 0x00325DF1 File Offset: 0x00323FF1
		public override string LocalName
		{
			get
			{
				return "scale";
			}
		}

		// Token: 0x17007346 RID: 29510
		// (get) Token: 0x06016085 RID: 90245 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007347 RID: 29511
		// (get) Token: 0x06016086 RID: 90246 RVA: 0x00325DF8 File Offset: 0x00323FF8
		internal override int ElementTypeId
		{
			get
			{
				return 12293;
			}
		}

		// Token: 0x06016087 RID: 90247 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06016088 RID: 90248 RVA: 0x00293ECF File Offset: 0x002920CF
		public ScaleFactor()
		{
		}

		// Token: 0x06016089 RID: 90249 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ScaleFactor(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601608A RID: 90250 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ScaleFactor(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601608B RID: 90251 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ScaleFactor(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601608C RID: 90252 RVA: 0x00325DFF File Offset: 0x00323FFF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "sx" == name)
			{
				return new ScaleX();
			}
			if (10 == namespaceId && "sy" == name)
			{
				return new ScaleY();
			}
			return null;
		}

		// Token: 0x17007348 RID: 29512
		// (get) Token: 0x0601608D RID: 90253 RVA: 0x00325E32 File Offset: 0x00324032
		internal override string[] ElementTagNames
		{
			get
			{
				return ScaleFactor.eleTagNames;
			}
		}

		// Token: 0x17007349 RID: 29513
		// (get) Token: 0x0601608E RID: 90254 RVA: 0x00325E39 File Offset: 0x00324039
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ScaleFactor.eleNamespaceIds;
			}
		}

		// Token: 0x1700734A RID: 29514
		// (get) Token: 0x0601608F RID: 90255 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700734B RID: 29515
		// (get) Token: 0x06016090 RID: 90256 RVA: 0x00325E40 File Offset: 0x00324040
		// (set) Token: 0x06016091 RID: 90257 RVA: 0x00325E49 File Offset: 0x00324049
		public ScaleX ScaleX
		{
			get
			{
				return base.GetElement<ScaleX>(0);
			}
			set
			{
				base.SetElement<ScaleX>(0, value);
			}
		}

		// Token: 0x1700734C RID: 29516
		// (get) Token: 0x06016092 RID: 90258 RVA: 0x00325E53 File Offset: 0x00324053
		// (set) Token: 0x06016093 RID: 90259 RVA: 0x00325E5C File Offset: 0x0032405C
		public ScaleY ScaleY
		{
			get
			{
				return base.GetElement<ScaleY>(1);
			}
			set
			{
				base.SetElement<ScaleY>(1, value);
			}
		}

		// Token: 0x06016094 RID: 90260 RVA: 0x00325E66 File Offset: 0x00324066
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ScaleFactor>(deep);
		}

		// Token: 0x040095E4 RID: 38372
		private const string tagName = "scale";

		// Token: 0x040095E5 RID: 38373
		private const byte tagNsId = 24;

		// Token: 0x040095E6 RID: 38374
		internal const int ElementTypeIdConst = 12293;

		// Token: 0x040095E7 RID: 38375
		private static readonly string[] eleTagNames = new string[] { "sx", "sy" };

		// Token: 0x040095E8 RID: 38376
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
