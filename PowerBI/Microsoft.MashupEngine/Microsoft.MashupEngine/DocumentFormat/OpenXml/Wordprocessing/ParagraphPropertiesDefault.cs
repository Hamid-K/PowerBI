using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F80 RID: 12160
	[ChildElementInfo(typeof(ParagraphPropertiesBaseStyle))]
	[GeneratedCode("DomGen", "2.0")]
	internal class ParagraphPropertiesDefault : OpenXmlCompositeElement
	{
		// Token: 0x17009188 RID: 37256
		// (get) Token: 0x0601A33A RID: 107322 RVA: 0x0035F0DC File Offset: 0x0035D2DC
		public override string LocalName
		{
			get
			{
				return "pPrDefault";
			}
		}

		// Token: 0x17009189 RID: 37257
		// (get) Token: 0x0601A33B RID: 107323 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700918A RID: 37258
		// (get) Token: 0x0601A33C RID: 107324 RVA: 0x0035F0E3 File Offset: 0x0035D2E3
		internal override int ElementTypeId
		{
			get
			{
				return 11834;
			}
		}

		// Token: 0x0601A33D RID: 107325 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A33E RID: 107326 RVA: 0x00293ECF File Offset: 0x002920CF
		public ParagraphPropertiesDefault()
		{
		}

		// Token: 0x0601A33F RID: 107327 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ParagraphPropertiesDefault(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A340 RID: 107328 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ParagraphPropertiesDefault(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A341 RID: 107329 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ParagraphPropertiesDefault(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A342 RID: 107330 RVA: 0x0035F0EA File Offset: 0x0035D2EA
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "pPr" == name)
			{
				return new ParagraphPropertiesBaseStyle();
			}
			return null;
		}

		// Token: 0x1700918B RID: 37259
		// (get) Token: 0x0601A343 RID: 107331 RVA: 0x0035F105 File Offset: 0x0035D305
		internal override string[] ElementTagNames
		{
			get
			{
				return ParagraphPropertiesDefault.eleTagNames;
			}
		}

		// Token: 0x1700918C RID: 37260
		// (get) Token: 0x0601A344 RID: 107332 RVA: 0x0035F10C File Offset: 0x0035D30C
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ParagraphPropertiesDefault.eleNamespaceIds;
			}
		}

		// Token: 0x1700918D RID: 37261
		// (get) Token: 0x0601A345 RID: 107333 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700918E RID: 37262
		// (get) Token: 0x0601A346 RID: 107334 RVA: 0x0035F113 File Offset: 0x0035D313
		// (set) Token: 0x0601A347 RID: 107335 RVA: 0x0035F11C File Offset: 0x0035D31C
		public ParagraphPropertiesBaseStyle ParagraphPropertiesBaseStyle
		{
			get
			{
				return base.GetElement<ParagraphPropertiesBaseStyle>(0);
			}
			set
			{
				base.SetElement<ParagraphPropertiesBaseStyle>(0, value);
			}
		}

		// Token: 0x0601A348 RID: 107336 RVA: 0x0035F126 File Offset: 0x0035D326
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ParagraphPropertiesDefault>(deep);
		}

		// Token: 0x0400AC2A RID: 44074
		private const string tagName = "pPrDefault";

		// Token: 0x0400AC2B RID: 44075
		private const byte tagNsId = 23;

		// Token: 0x0400AC2C RID: 44076
		internal const int ElementTypeIdConst = 11834;

		// Token: 0x0400AC2D RID: 44077
		private static readonly string[] eleTagNames = new string[] { "pPr" };

		// Token: 0x0400AC2E RID: 44078
		private static readonly byte[] eleNamespaceIds = new byte[] { 23 };
	}
}
