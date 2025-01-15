using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FF7 RID: 12279
	[ChildElementInfo(typeof(RsidRoot))]
	[ChildElementInfo(typeof(Rsid))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Rsids : OpenXmlCompositeElement
	{
		// Token: 0x170095A3 RID: 38307
		// (get) Token: 0x0601ABFA RID: 109562 RVA: 0x0036709D File Offset: 0x0036529D
		public override string LocalName
		{
			get
			{
				return "rsids";
			}
		}

		// Token: 0x170095A4 RID: 38308
		// (get) Token: 0x0601ABFB RID: 109563 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170095A5 RID: 38309
		// (get) Token: 0x0601ABFC RID: 109564 RVA: 0x003670A4 File Offset: 0x003652A4
		internal override int ElementTypeId
		{
			get
			{
				return 12040;
			}
		}

		// Token: 0x0601ABFD RID: 109565 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601ABFE RID: 109566 RVA: 0x00293ECF File Offset: 0x002920CF
		public Rsids()
		{
		}

		// Token: 0x0601ABFF RID: 109567 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Rsids(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AC00 RID: 109568 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Rsids(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601AC01 RID: 109569 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Rsids(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601AC02 RID: 109570 RVA: 0x003670AB File Offset: 0x003652AB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rsidRoot" == name)
			{
				return new RsidRoot();
			}
			if (23 == namespaceId && "rsid" == name)
			{
				return new Rsid();
			}
			return null;
		}

		// Token: 0x170095A6 RID: 38310
		// (get) Token: 0x0601AC03 RID: 109571 RVA: 0x003670DE File Offset: 0x003652DE
		internal override string[] ElementTagNames
		{
			get
			{
				return Rsids.eleTagNames;
			}
		}

		// Token: 0x170095A7 RID: 38311
		// (get) Token: 0x0601AC04 RID: 109572 RVA: 0x003670E5 File Offset: 0x003652E5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Rsids.eleNamespaceIds;
			}
		}

		// Token: 0x170095A8 RID: 38312
		// (get) Token: 0x0601AC05 RID: 109573 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170095A9 RID: 38313
		// (get) Token: 0x0601AC06 RID: 109574 RVA: 0x003670EC File Offset: 0x003652EC
		// (set) Token: 0x0601AC07 RID: 109575 RVA: 0x003670F5 File Offset: 0x003652F5
		public RsidRoot RsidRoot
		{
			get
			{
				return base.GetElement<RsidRoot>(0);
			}
			set
			{
				base.SetElement<RsidRoot>(0, value);
			}
		}

		// Token: 0x0601AC08 RID: 109576 RVA: 0x003670FF File Offset: 0x003652FF
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Rsids>(deep);
		}

		// Token: 0x0400AE29 RID: 44585
		private const string tagName = "rsids";

		// Token: 0x0400AE2A RID: 44586
		private const byte tagNsId = 23;

		// Token: 0x0400AE2B RID: 44587
		internal const int ElementTypeIdConst = 12040;

		// Token: 0x0400AE2C RID: 44588
		private static readonly string[] eleTagNames = new string[] { "rsidRoot", "rsid" };

		// Token: 0x0400AE2D RID: 44589
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23 };
	}
}
