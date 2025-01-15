using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FAD RID: 12205
	[ChildElementInfo(typeof(ParagraphPropertiesDefault))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(RunPropertiesDefault))]
	internal class DocDefaults : OpenXmlCompositeElement
	{
		// Token: 0x17009349 RID: 37705
		// (get) Token: 0x0601A6EF RID: 108271 RVA: 0x00362340 File Offset: 0x00360540
		public override string LocalName
		{
			get
			{
				return "docDefaults";
			}
		}

		// Token: 0x1700934A RID: 37706
		// (get) Token: 0x0601A6F0 RID: 108272 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700934B RID: 37707
		// (get) Token: 0x0601A6F1 RID: 108273 RVA: 0x00362347 File Offset: 0x00360547
		internal override int ElementTypeId
		{
			get
			{
				return 11912;
			}
		}

		// Token: 0x0601A6F2 RID: 108274 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A6F3 RID: 108275 RVA: 0x00293ECF File Offset: 0x002920CF
		public DocDefaults()
		{
		}

		// Token: 0x0601A6F4 RID: 108276 RVA: 0x00293ED7 File Offset: 0x002920D7
		public DocDefaults(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A6F5 RID: 108277 RVA: 0x00293EE0 File Offset: 0x002920E0
		public DocDefaults(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A6F6 RID: 108278 RVA: 0x00293EE9 File Offset: 0x002920E9
		public DocDefaults(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A6F7 RID: 108279 RVA: 0x0036234E File Offset: 0x0036054E
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "rPrDefault" == name)
			{
				return new RunPropertiesDefault();
			}
			if (23 == namespaceId && "pPrDefault" == name)
			{
				return new ParagraphPropertiesDefault();
			}
			return null;
		}

		// Token: 0x1700934C RID: 37708
		// (get) Token: 0x0601A6F8 RID: 108280 RVA: 0x00362381 File Offset: 0x00360581
		internal override string[] ElementTagNames
		{
			get
			{
				return DocDefaults.eleTagNames;
			}
		}

		// Token: 0x1700934D RID: 37709
		// (get) Token: 0x0601A6F9 RID: 108281 RVA: 0x00362388 File Offset: 0x00360588
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DocDefaults.eleNamespaceIds;
			}
		}

		// Token: 0x1700934E RID: 37710
		// (get) Token: 0x0601A6FA RID: 108282 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700934F RID: 37711
		// (get) Token: 0x0601A6FB RID: 108283 RVA: 0x0036238F File Offset: 0x0036058F
		// (set) Token: 0x0601A6FC RID: 108284 RVA: 0x00362398 File Offset: 0x00360598
		public RunPropertiesDefault RunPropertiesDefault
		{
			get
			{
				return base.GetElement<RunPropertiesDefault>(0);
			}
			set
			{
				base.SetElement<RunPropertiesDefault>(0, value);
			}
		}

		// Token: 0x17009350 RID: 37712
		// (get) Token: 0x0601A6FD RID: 108285 RVA: 0x003623A2 File Offset: 0x003605A2
		// (set) Token: 0x0601A6FE RID: 108286 RVA: 0x003623AB File Offset: 0x003605AB
		public ParagraphPropertiesDefault ParagraphPropertiesDefault
		{
			get
			{
				return base.GetElement<ParagraphPropertiesDefault>(1);
			}
			set
			{
				base.SetElement<ParagraphPropertiesDefault>(1, value);
			}
		}

		// Token: 0x0601A6FF RID: 108287 RVA: 0x003623B5 File Offset: 0x003605B5
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<DocDefaults>(deep);
		}

		// Token: 0x0400ACF4 RID: 44276
		private const string tagName = "docDefaults";

		// Token: 0x0400ACF5 RID: 44277
		private const byte tagNsId = 23;

		// Token: 0x0400ACF6 RID: 44278
		internal const int ElementTypeIdConst = 11912;

		// Token: 0x0400ACF7 RID: 44279
		private static readonly string[] eleTagNames = new string[] { "rPrDefault", "pPrDefault" };

		// Token: 0x0400ACF8 RID: 44280
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23 };
	}
}
