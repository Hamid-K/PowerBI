using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x020026FE RID: 9982
	[ChildElementInfo(typeof(FillRectangle))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Stretch : OpenXmlCompositeElement
	{
		// Token: 0x17005E61 RID: 24161
		// (get) Token: 0x0601310C RID: 78092 RVA: 0x00303297 File Offset: 0x00301497
		public override string LocalName
		{
			get
			{
				return "stretch";
			}
		}

		// Token: 0x17005E62 RID: 24162
		// (get) Token: 0x0601310D RID: 78093 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x17005E63 RID: 24163
		// (get) Token: 0x0601310E RID: 78094 RVA: 0x0030329E File Offset: 0x0030149E
		internal override int ElementTypeId
		{
			get
			{
				return 10046;
			}
		}

		// Token: 0x0601310F RID: 78095 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013110 RID: 78096 RVA: 0x00293ECF File Offset: 0x002920CF
		public Stretch()
		{
		}

		// Token: 0x06013111 RID: 78097 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Stretch(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013112 RID: 78098 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Stretch(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013113 RID: 78099 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Stretch(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013114 RID: 78100 RVA: 0x003032A5 File Offset: 0x003014A5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "fillRect" == name)
			{
				return new FillRectangle();
			}
			return null;
		}

		// Token: 0x17005E64 RID: 24164
		// (get) Token: 0x06013115 RID: 78101 RVA: 0x003032C0 File Offset: 0x003014C0
		internal override string[] ElementTagNames
		{
			get
			{
				return Stretch.eleTagNames;
			}
		}

		// Token: 0x17005E65 RID: 24165
		// (get) Token: 0x06013116 RID: 78102 RVA: 0x003032C7 File Offset: 0x003014C7
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Stretch.eleNamespaceIds;
			}
		}

		// Token: 0x17005E66 RID: 24166
		// (get) Token: 0x06013117 RID: 78103 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17005E67 RID: 24167
		// (get) Token: 0x06013118 RID: 78104 RVA: 0x003032CE File Offset: 0x003014CE
		// (set) Token: 0x06013119 RID: 78105 RVA: 0x003032D7 File Offset: 0x003014D7
		public FillRectangle FillRectangle
		{
			get
			{
				return base.GetElement<FillRectangle>(0);
			}
			set
			{
				base.SetElement<FillRectangle>(0, value);
			}
		}

		// Token: 0x0601311A RID: 78106 RVA: 0x003032E1 File Offset: 0x003014E1
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Stretch>(deep);
		}

		// Token: 0x04008480 RID: 33920
		private const string tagName = "stretch";

		// Token: 0x04008481 RID: 33921
		private const byte tagNsId = 10;

		// Token: 0x04008482 RID: 33922
		internal const int ElementTypeIdConst = 10046;

		// Token: 0x04008483 RID: 33923
		private static readonly string[] eleTagNames = new string[] { "fillRect" };

		// Token: 0x04008484 RID: 33924
		private static readonly byte[] eleNamespaceIds = new byte[] { 10 };
	}
}
