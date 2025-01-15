using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F47 RID: 12103
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(MatchSource))]
	internal class AltChunkProperties : OpenXmlCompositeElement
	{
		// Token: 0x17008FE8 RID: 36840
		// (get) Token: 0x06019FAE RID: 106414 RVA: 0x0035A7EB File Offset: 0x003589EB
		public override string LocalName
		{
			get
			{
				return "altChunkPr";
			}
		}

		// Token: 0x17008FE9 RID: 36841
		// (get) Token: 0x06019FAF RID: 106415 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008FEA RID: 36842
		// (get) Token: 0x06019FB0 RID: 106416 RVA: 0x0035A7F2 File Offset: 0x003589F2
		internal override int ElementTypeId
		{
			get
			{
				return 11750;
			}
		}

		// Token: 0x06019FB1 RID: 106417 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019FB2 RID: 106418 RVA: 0x00293ECF File Offset: 0x002920CF
		public AltChunkProperties()
		{
		}

		// Token: 0x06019FB3 RID: 106419 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AltChunkProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019FB4 RID: 106420 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AltChunkProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019FB5 RID: 106421 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AltChunkProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019FB6 RID: 106422 RVA: 0x0035A7F9 File Offset: 0x003589F9
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "matchSrc" == name)
			{
				return new MatchSource();
			}
			return null;
		}

		// Token: 0x17008FEB RID: 36843
		// (get) Token: 0x06019FB7 RID: 106423 RVA: 0x0035A814 File Offset: 0x00358A14
		internal override string[] ElementTagNames
		{
			get
			{
				return AltChunkProperties.eleTagNames;
			}
		}

		// Token: 0x17008FEC RID: 36844
		// (get) Token: 0x06019FB8 RID: 106424 RVA: 0x0035A81B File Offset: 0x00358A1B
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return AltChunkProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17008FED RID: 36845
		// (get) Token: 0x06019FB9 RID: 106425 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008FEE RID: 36846
		// (get) Token: 0x06019FBA RID: 106426 RVA: 0x0035A822 File Offset: 0x00358A22
		// (set) Token: 0x06019FBB RID: 106427 RVA: 0x0035A82B File Offset: 0x00358A2B
		public MatchSource MatchSource
		{
			get
			{
				return base.GetElement<MatchSource>(0);
			}
			set
			{
				base.SetElement<MatchSource>(0, value);
			}
		}

		// Token: 0x06019FBC RID: 106428 RVA: 0x0035A835 File Offset: 0x00358A35
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AltChunkProperties>(deep);
		}

		// Token: 0x0400AB39 RID: 43833
		private const string tagName = "altChunkPr";

		// Token: 0x0400AB3A RID: 43834
		private const byte tagNsId = 23;

		// Token: 0x0400AB3B RID: 43835
		internal const int ElementTypeIdConst = 11750;

		// Token: 0x0400AB3C RID: 43836
		private static readonly string[] eleTagNames = new string[] { "matchSrc" };

		// Token: 0x0400AB3D RID: 43837
		private static readonly byte[] eleNamespaceIds = new byte[] { 23 };
	}
}
