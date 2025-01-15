using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing
{
	// Token: 0x02002753 RID: 10067
	[ChildElementInfo(typeof(Blip))]
	[GeneratedCode("DomGen", "2.0")]
	internal class PictureBullet : OpenXmlCompositeElement
	{
		// Token: 0x170060A7 RID: 24743
		// (get) Token: 0x060135FF RID: 79359 RVA: 0x003066CF File Offset: 0x003048CF
		public override string LocalName
		{
			get
			{
				return "buBlip";
			}
		}

		// Token: 0x170060A8 RID: 24744
		// (get) Token: 0x06013600 RID: 79360 RVA: 0x0014025A File Offset: 0x0013E45A
		internal override byte NamespaceId
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x170060A9 RID: 24745
		// (get) Token: 0x06013601 RID: 79361 RVA: 0x003066D6 File Offset: 0x003048D6
		internal override int ElementTypeId
		{
			get
			{
				return 10112;
			}
		}

		// Token: 0x06013602 RID: 79362 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06013603 RID: 79363 RVA: 0x00293ECF File Offset: 0x002920CF
		public PictureBullet()
		{
		}

		// Token: 0x06013604 RID: 79364 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PictureBullet(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013605 RID: 79365 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PictureBullet(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06013606 RID: 79366 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PictureBullet(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06013607 RID: 79367 RVA: 0x003066DD File Offset: 0x003048DD
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (10 == namespaceId && "blip" == name)
			{
				return new Blip();
			}
			return null;
		}

		// Token: 0x170060AA RID: 24746
		// (get) Token: 0x06013608 RID: 79368 RVA: 0x003066F8 File Offset: 0x003048F8
		internal override string[] ElementTagNames
		{
			get
			{
				return PictureBullet.eleTagNames;
			}
		}

		// Token: 0x170060AB RID: 24747
		// (get) Token: 0x06013609 RID: 79369 RVA: 0x003066FF File Offset: 0x003048FF
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PictureBullet.eleNamespaceIds;
			}
		}

		// Token: 0x170060AC RID: 24748
		// (get) Token: 0x0601360A RID: 79370 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170060AD RID: 24749
		// (get) Token: 0x0601360B RID: 79371 RVA: 0x002FC5FC File Offset: 0x002FA7FC
		// (set) Token: 0x0601360C RID: 79372 RVA: 0x002FC605 File Offset: 0x002FA805
		public Blip Blip
		{
			get
			{
				return base.GetElement<Blip>(0);
			}
			set
			{
				base.SetElement<Blip>(0, value);
			}
		}

		// Token: 0x0601360D RID: 79373 RVA: 0x00306706 File Offset: 0x00304906
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PictureBullet>(deep);
		}

		// Token: 0x040085EE RID: 34286
		private const string tagName = "buBlip";

		// Token: 0x040085EF RID: 34287
		private const byte tagNsId = 10;

		// Token: 0x040085F0 RID: 34288
		internal const int ElementTypeIdConst = 10112;

		// Token: 0x040085F1 RID: 34289
		private static readonly string[] eleTagNames = new string[] { "blip" };

		// Token: 0x040085F2 RID: 34290
		private static readonly byte[] eleNamespaceIds = new byte[] { 10 };
	}
}
