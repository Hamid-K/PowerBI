using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Drawing;

namespace DocumentFormat.OpenXml.Office2010.Word.DrawingCanvas
{
	// Token: 0x020024E4 RID: 9444
	[ChildElementInfo(typeof(PatternFill))]
	[ChildElementInfo(typeof(SolidFill))]
	[ChildElementInfo(typeof(GradientFill))]
	[ChildElementInfo(typeof(BlipFill))]
	[ChildElementInfo(typeof(NoFill))]
	[ChildElementInfo(typeof(GroupFill))]
	[ChildElementInfo(typeof(EffectList))]
	[ChildElementInfo(typeof(EffectDag))]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class BackgroundFormatting : OpenXmlCompositeElement
	{
		// Token: 0x1700531C RID: 21276
		// (get) Token: 0x060117EF RID: 71663 RVA: 0x002EF0F4 File Offset: 0x002ED2F4
		public override string LocalName
		{
			get
			{
				return "bg";
			}
		}

		// Token: 0x1700531D RID: 21277
		// (get) Token: 0x060117F0 RID: 71664 RVA: 0x002EEF8A File Offset: 0x002ED18A
		internal override byte NamespaceId
		{
			get
			{
				return 59;
			}
		}

		// Token: 0x1700531E RID: 21278
		// (get) Token: 0x060117F1 RID: 71665 RVA: 0x002EF0FB File Offset: 0x002ED2FB
		internal override int ElementTypeId
		{
			get
			{
				return 13119;
			}
		}

		// Token: 0x060117F2 RID: 71666 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x060117F3 RID: 71667 RVA: 0x00293ECF File Offset: 0x002920CF
		public BackgroundFormatting()
		{
		}

		// Token: 0x060117F4 RID: 71668 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BackgroundFormatting(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060117F5 RID: 71669 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BackgroundFormatting(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060117F6 RID: 71670 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BackgroundFormatting(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060117F7 RID: 71671 RVA: 0x002EF104 File Offset: 0x002ED304
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "noFill" == name)
			{
				return new NoFill();
			}
			if (10 == namespaceId && "solidFill" == name)
			{
				return new SolidFill();
			}
			if (10 == namespaceId && "gradFill" == name)
			{
				return new GradientFill();
			}
			if (10 == namespaceId && "blipFill" == name)
			{
				return new BlipFill();
			}
			if (10 == namespaceId && "pattFill" == name)
			{
				return new PatternFill();
			}
			if (10 == namespaceId && "grpFill" == name)
			{
				return new GroupFill();
			}
			if (10 == namespaceId && "effectLst" == name)
			{
				return new EffectList();
			}
			if (10 == namespaceId && "effectDag" == name)
			{
				return new EffectDag();
			}
			return null;
		}

		// Token: 0x060117F8 RID: 71672 RVA: 0x002EF1D2 File Offset: 0x002ED3D2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BackgroundFormatting>(deep);
		}

		// Token: 0x04007AF7 RID: 31479
		private const string tagName = "bg";

		// Token: 0x04007AF8 RID: 31480
		private const byte tagNsId = 59;

		// Token: 0x04007AF9 RID: 31481
		internal const int ElementTypeIdConst = 13119;
	}
}
