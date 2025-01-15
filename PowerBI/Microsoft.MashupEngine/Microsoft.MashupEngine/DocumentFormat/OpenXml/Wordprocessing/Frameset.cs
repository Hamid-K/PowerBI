using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F81 RID: 12161
	[ChildElementInfo(typeof(FrameSize))]
	[ChildElementInfo(typeof(FrameLayout))]
	[ChildElementInfo(typeof(FramesetSplitbar))]
	[ChildElementInfo(typeof(Frame))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Frameset))]
	internal class Frameset : OpenXmlCompositeElement
	{
		// Token: 0x1700918F RID: 37263
		// (get) Token: 0x0601A34A RID: 107338 RVA: 0x0035F164 File Offset: 0x0035D364
		public override string LocalName
		{
			get
			{
				return "frameset";
			}
		}

		// Token: 0x17009190 RID: 37264
		// (get) Token: 0x0601A34B RID: 107339 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009191 RID: 37265
		// (get) Token: 0x0601A34C RID: 107340 RVA: 0x0035F16B File Offset: 0x0035D36B
		internal override int ElementTypeId
		{
			get
			{
				return 11835;
			}
		}

		// Token: 0x0601A34D RID: 107341 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A34E RID: 107342 RVA: 0x00293ECF File Offset: 0x002920CF
		public Frameset()
		{
		}

		// Token: 0x0601A34F RID: 107343 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Frameset(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A350 RID: 107344 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Frameset(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A351 RID: 107345 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Frameset(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A352 RID: 107346 RVA: 0x0035F174 File Offset: 0x0035D374
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "sz" == name)
			{
				return new FrameSize();
			}
			if (23 == namespaceId && "framesetSplitbar" == name)
			{
				return new FramesetSplitbar();
			}
			if (23 == namespaceId && "frameLayout" == name)
			{
				return new FrameLayout();
			}
			if (23 == namespaceId && "frameset" == name)
			{
				return new Frameset();
			}
			if (23 == namespaceId && "frame" == name)
			{
				return new Frame();
			}
			return null;
		}

		// Token: 0x17009192 RID: 37266
		// (get) Token: 0x0601A353 RID: 107347 RVA: 0x0035F1FA File Offset: 0x0035D3FA
		internal override string[] ElementTagNames
		{
			get
			{
				return Frameset.eleTagNames;
			}
		}

		// Token: 0x17009193 RID: 37267
		// (get) Token: 0x0601A354 RID: 107348 RVA: 0x0035F201 File Offset: 0x0035D401
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Frameset.eleNamespaceIds;
			}
		}

		// Token: 0x17009194 RID: 37268
		// (get) Token: 0x0601A355 RID: 107349 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009195 RID: 37269
		// (get) Token: 0x0601A356 RID: 107350 RVA: 0x0035F208 File Offset: 0x0035D408
		// (set) Token: 0x0601A357 RID: 107351 RVA: 0x0035F211 File Offset: 0x0035D411
		public FrameSize FrameSize
		{
			get
			{
				return base.GetElement<FrameSize>(0);
			}
			set
			{
				base.SetElement<FrameSize>(0, value);
			}
		}

		// Token: 0x17009196 RID: 37270
		// (get) Token: 0x0601A358 RID: 107352 RVA: 0x0035F21B File Offset: 0x0035D41B
		// (set) Token: 0x0601A359 RID: 107353 RVA: 0x0035F224 File Offset: 0x0035D424
		public FramesetSplitbar FramesetSplitbar
		{
			get
			{
				return base.GetElement<FramesetSplitbar>(1);
			}
			set
			{
				base.SetElement<FramesetSplitbar>(1, value);
			}
		}

		// Token: 0x17009197 RID: 37271
		// (get) Token: 0x0601A35A RID: 107354 RVA: 0x0035F22E File Offset: 0x0035D42E
		// (set) Token: 0x0601A35B RID: 107355 RVA: 0x0035F237 File Offset: 0x0035D437
		public FrameLayout FrameLayout
		{
			get
			{
				return base.GetElement<FrameLayout>(2);
			}
			set
			{
				base.SetElement<FrameLayout>(2, value);
			}
		}

		// Token: 0x0601A35C RID: 107356 RVA: 0x0035F241 File Offset: 0x0035D441
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Frameset>(deep);
		}

		// Token: 0x0400AC2F RID: 44079
		private const string tagName = "frameset";

		// Token: 0x0400AC30 RID: 44080
		private const byte tagNsId = 23;

		// Token: 0x0400AC31 RID: 44081
		internal const int ElementTypeIdConst = 11835;

		// Token: 0x0400AC32 RID: 44082
		private static readonly string[] eleTagNames = new string[] { "sz", "framesetSplitbar", "frameLayout", "frameset", "frame" };

		// Token: 0x0400AC33 RID: 44083
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23, 23 };
	}
}
