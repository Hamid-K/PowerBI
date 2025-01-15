using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Packaging;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F1C RID: 12060
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(DocDefaults))]
	[ChildElementInfo(typeof(LatentStyles))]
	[ChildElementInfo(typeof(Style))]
	internal class Styles : OpenXmlPartRootElement
	{
		// Token: 0x17008E8C RID: 36492
		// (get) Token: 0x06019CBB RID: 105659 RVA: 0x002A6BA7 File Offset: 0x002A4DA7
		public override string LocalName
		{
			get
			{
				return "styles";
			}
		}

		// Token: 0x17008E8D RID: 36493
		// (get) Token: 0x06019CBC RID: 105660 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008E8E RID: 36494
		// (get) Token: 0x06019CBD RID: 105661 RVA: 0x00356987 File Offset: 0x00354B87
		internal override int ElementTypeId
		{
			get
			{
				return 11701;
			}
		}

		// Token: 0x06019CBE RID: 105662 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019CBF RID: 105663 RVA: 0x002CEB10 File Offset: 0x002CCD10
		public Styles()
		{
		}

		// Token: 0x06019CC0 RID: 105664 RVA: 0x002CEB18 File Offset: 0x002CCD18
		public Styles(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019CC1 RID: 105665 RVA: 0x002CEB21 File Offset: 0x002CCD21
		public Styles(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019CC2 RID: 105666 RVA: 0x002CEB2A File Offset: 0x002CCD2A
		public Styles(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019CC3 RID: 105667 RVA: 0x00356990 File Offset: 0x00354B90
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "docDefaults" == name)
			{
				return new DocDefaults();
			}
			if (23 == namespaceId && "latentStyles" == name)
			{
				return new LatentStyles();
			}
			if (23 == namespaceId && "style" == name)
			{
				return new Style();
			}
			return null;
		}

		// Token: 0x17008E8F RID: 36495
		// (get) Token: 0x06019CC4 RID: 105668 RVA: 0x003569E6 File Offset: 0x00354BE6
		internal override string[] ElementTagNames
		{
			get
			{
				return Styles.eleTagNames;
			}
		}

		// Token: 0x17008E90 RID: 36496
		// (get) Token: 0x06019CC5 RID: 105669 RVA: 0x003569ED File Offset: 0x00354BED
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Styles.eleNamespaceIds;
			}
		}

		// Token: 0x17008E91 RID: 36497
		// (get) Token: 0x06019CC6 RID: 105670 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008E92 RID: 36498
		// (get) Token: 0x06019CC7 RID: 105671 RVA: 0x003569F4 File Offset: 0x00354BF4
		// (set) Token: 0x06019CC8 RID: 105672 RVA: 0x003569FD File Offset: 0x00354BFD
		public DocDefaults DocDefaults
		{
			get
			{
				return base.GetElement<DocDefaults>(0);
			}
			set
			{
				base.SetElement<DocDefaults>(0, value);
			}
		}

		// Token: 0x17008E93 RID: 36499
		// (get) Token: 0x06019CC9 RID: 105673 RVA: 0x00356A07 File Offset: 0x00354C07
		// (set) Token: 0x06019CCA RID: 105674 RVA: 0x00356A10 File Offset: 0x00354C10
		public LatentStyles LatentStyles
		{
			get
			{
				return base.GetElement<LatentStyles>(1);
			}
			set
			{
				base.SetElement<LatentStyles>(1, value);
			}
		}

		// Token: 0x06019CCB RID: 105675 RVA: 0x00356A1A File Offset: 0x00354C1A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Styles>(deep);
		}

		// Token: 0x06019CCC RID: 105676 RVA: 0x002CEBD9 File Offset: 0x002CCDD9
		internal Styles(StylesPart ownerPart)
			: base(ownerPart)
		{
		}

		// Token: 0x06019CCD RID: 105677 RVA: 0x002CEBE2 File Offset: 0x002CCDE2
		public void Load(StylesPart openXmlPart)
		{
			base.LoadFromPart(openXmlPart);
		}

		// Token: 0x06019CCE RID: 105678 RVA: 0x002CEBEB File Offset: 0x002CCDEB
		public void Save(StylesPart openXmlPart)
		{
			base.SaveToPart(openXmlPart);
		}

		// Token: 0x17008E94 RID: 36500
		// (get) Token: 0x06019CCF RID: 105679 RVA: 0x00356A23 File Offset: 0x00354C23
		// (set) Token: 0x06019CD0 RID: 105680 RVA: 0x002CEC01 File Offset: 0x002CCE01
		public StylesPart StylesPart
		{
			get
			{
				return base.OpenXmlPart as StylesPart;
			}
			internal set
			{
				base.OpenXmlPart = value;
			}
		}

		// Token: 0x0400AA81 RID: 43649
		private const string tagName = "styles";

		// Token: 0x0400AA82 RID: 43650
		private const byte tagNsId = 23;

		// Token: 0x0400AA83 RID: 43651
		internal const int ElementTypeIdConst = 11701;

		// Token: 0x0400AA84 RID: 43652
		private static readonly string[] eleTagNames = new string[] { "docDefaults", "latentStyles", "style" };

		// Token: 0x0400AA85 RID: 43653
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
