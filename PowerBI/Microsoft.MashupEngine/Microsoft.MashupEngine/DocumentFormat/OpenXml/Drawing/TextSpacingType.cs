using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002826 RID: 10278
	[ChildElementInfo(typeof(SpacingPercent))]
	[ChildElementInfo(typeof(SpacingPoints))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class TextSpacingType : OpenXmlCompositeElement
	{
		// Token: 0x06014209 RID: 82441 RVA: 0x0030F9E7 File Offset: 0x0030DBE7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "spcPct" == name)
			{
				return new SpacingPercent();
			}
			if (10 == namespaceId && "spcPts" == name)
			{
				return new SpacingPoints();
			}
			return null;
		}

		// Token: 0x170065D5 RID: 26069
		// (get) Token: 0x0601420A RID: 82442 RVA: 0x0030FA1A File Offset: 0x0030DC1A
		internal override string[] ElementTagNames
		{
			get
			{
				return TextSpacingType.eleTagNames;
			}
		}

		// Token: 0x170065D6 RID: 26070
		// (get) Token: 0x0601420B RID: 82443 RVA: 0x0030FA21 File Offset: 0x0030DC21
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return TextSpacingType.eleNamespaceIds;
			}
		}

		// Token: 0x170065D7 RID: 26071
		// (get) Token: 0x0601420C RID: 82444 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x170065D8 RID: 26072
		// (get) Token: 0x0601420D RID: 82445 RVA: 0x0030FA28 File Offset: 0x0030DC28
		// (set) Token: 0x0601420E RID: 82446 RVA: 0x0030FA31 File Offset: 0x0030DC31
		public SpacingPercent SpacingPercent
		{
			get
			{
				return base.GetElement<SpacingPercent>(0);
			}
			set
			{
				base.SetElement<SpacingPercent>(0, value);
			}
		}

		// Token: 0x170065D9 RID: 26073
		// (get) Token: 0x0601420F RID: 82447 RVA: 0x0030FA3B File Offset: 0x0030DC3B
		// (set) Token: 0x06014210 RID: 82448 RVA: 0x0030FA44 File Offset: 0x0030DC44
		public SpacingPoints SpacingPoints
		{
			get
			{
				return base.GetElement<SpacingPoints>(1);
			}
			set
			{
				base.SetElement<SpacingPoints>(1, value);
			}
		}

		// Token: 0x06014211 RID: 82449 RVA: 0x00293ECF File Offset: 0x002920CF
		protected TextSpacingType()
		{
		}

		// Token: 0x06014212 RID: 82450 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected TextSpacingType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014213 RID: 82451 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected TextSpacingType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014214 RID: 82452 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected TextSpacingType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04008925 RID: 35109
		private static readonly string[] eleTagNames = new string[] { "spcPct", "spcPts" };

		// Token: 0x04008926 RID: 35110
		private static readonly byte[] eleNamespaceIds = new byte[] { 10, 10 };
	}
}
