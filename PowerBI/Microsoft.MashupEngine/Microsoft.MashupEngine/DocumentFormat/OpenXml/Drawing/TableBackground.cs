using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002803 RID: 10243
	[ChildElementInfo(typeof(EffectReference))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FillProperties))]
	[ChildElementInfo(typeof(FillReference))]
	[ChildElementInfo(typeof(EffectPropertiesType))]
	internal class TableBackground : OpenXmlCompositeElement
	{
		// Token: 0x17006536 RID: 25910
		// (get) Token: 0x0601406D RID: 82029 RVA: 0x0030E924 File Offset: 0x0030CB24
		public override string LocalName
		{
			get
			{
				return "tblBg";
			}
		}

		// Token: 0x17006537 RID: 25911
		// (get) Token: 0x0601406E RID: 82030 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17006538 RID: 25912
		// (get) Token: 0x0601406F RID: 82031 RVA: 0x0030E92B File Offset: 0x0030CB2B
		internal override int ElementTypeId
		{
			get
			{
				return 10279;
			}
		}

		// Token: 0x06014070 RID: 82032 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06014071 RID: 82033 RVA: 0x00293ECF File Offset: 0x002920CF
		public TableBackground()
		{
		}

		// Token: 0x06014072 RID: 82034 RVA: 0x00293ED7 File Offset: 0x002920D7
		public TableBackground(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014073 RID: 82035 RVA: 0x00293EE0 File Offset: 0x002920E0
		public TableBackground(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06014074 RID: 82036 RVA: 0x00293EE9 File Offset: 0x002920E9
		public TableBackground(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06014075 RID: 82037 RVA: 0x0030E934 File Offset: 0x0030CB34
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "fill" == name)
			{
				return new FillProperties();
			}
			if (10 == namespaceId && "fillRef" == name)
			{
				return new FillReference();
			}
			if (10 == namespaceId && "effect" == name)
			{
				return new EffectPropertiesType();
			}
			if (10 == namespaceId && "effectRef" == name)
			{
				return new EffectReference();
			}
			return null;
		}

		// Token: 0x06014076 RID: 82038 RVA: 0x0030E9A2 File Offset: 0x0030CBA2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<TableBackground>(deep);
		}

		// Token: 0x040088B3 RID: 34995
		private const string tagName = "tblBg";

		// Token: 0x040088B4 RID: 34996
		private const byte tagNsId = 10;

		// Token: 0x040088B5 RID: 34997
		internal const int ElementTypeIdConst = 10279;
	}
}
