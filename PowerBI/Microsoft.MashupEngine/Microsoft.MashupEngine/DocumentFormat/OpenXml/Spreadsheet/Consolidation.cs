using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CC3 RID: 11459
	[ChildElementInfo(typeof(RangeSets))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Pages))]
	internal class Consolidation : OpenXmlCompositeElement
	{
		// Token: 0x170084F9 RID: 34041
		// (get) Token: 0x06018861 RID: 100449 RVA: 0x00342260 File Offset: 0x00340460
		public override string LocalName
		{
			get
			{
				return "consolidation";
			}
		}

		// Token: 0x170084FA RID: 34042
		// (get) Token: 0x06018862 RID: 100450 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170084FB RID: 34043
		// (get) Token: 0x06018863 RID: 100451 RVA: 0x00342267 File Offset: 0x00340467
		internal override int ElementTypeId
		{
			get
			{
				return 11439;
			}
		}

		// Token: 0x06018864 RID: 100452 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170084FC RID: 34044
		// (get) Token: 0x06018865 RID: 100453 RVA: 0x0034226E File Offset: 0x0034046E
		internal override string[] AttributeTagNames
		{
			get
			{
				return Consolidation.attributeTagNames;
			}
		}

		// Token: 0x170084FD RID: 34045
		// (get) Token: 0x06018866 RID: 100454 RVA: 0x00342275 File Offset: 0x00340475
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Consolidation.attributeNamespaceIds;
			}
		}

		// Token: 0x170084FE RID: 34046
		// (get) Token: 0x06018867 RID: 100455 RVA: 0x002C9F7B File Offset: 0x002C817B
		// (set) Token: 0x06018868 RID: 100456 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "autoPage")]
		public BooleanValue AutoPage
		{
			get
			{
				return (BooleanValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06018869 RID: 100457 RVA: 0x00293ECF File Offset: 0x002920CF
		public Consolidation()
		{
		}

		// Token: 0x0601886A RID: 100458 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Consolidation(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601886B RID: 100459 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Consolidation(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601886C RID: 100460 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Consolidation(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601886D RID: 100461 RVA: 0x0034227C File Offset: 0x0034047C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "pages" == name)
			{
				return new Pages();
			}
			if (22 == namespaceId && "rangeSets" == name)
			{
				return new RangeSets();
			}
			return null;
		}

		// Token: 0x170084FF RID: 34047
		// (get) Token: 0x0601886E RID: 100462 RVA: 0x003422AF File Offset: 0x003404AF
		internal override string[] ElementTagNames
		{
			get
			{
				return Consolidation.eleTagNames;
			}
		}

		// Token: 0x17008500 RID: 34048
		// (get) Token: 0x0601886F RID: 100463 RVA: 0x003422B6 File Offset: 0x003404B6
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Consolidation.eleNamespaceIds;
			}
		}

		// Token: 0x17008501 RID: 34049
		// (get) Token: 0x06018870 RID: 100464 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008502 RID: 34050
		// (get) Token: 0x06018871 RID: 100465 RVA: 0x003422BD File Offset: 0x003404BD
		// (set) Token: 0x06018872 RID: 100466 RVA: 0x003422C6 File Offset: 0x003404C6
		public Pages Pages
		{
			get
			{
				return base.GetElement<Pages>(0);
			}
			set
			{
				base.SetElement<Pages>(0, value);
			}
		}

		// Token: 0x17008503 RID: 34051
		// (get) Token: 0x06018873 RID: 100467 RVA: 0x003422D0 File Offset: 0x003404D0
		// (set) Token: 0x06018874 RID: 100468 RVA: 0x003422D9 File Offset: 0x003404D9
		public RangeSets RangeSets
		{
			get
			{
				return base.GetElement<RangeSets>(1);
			}
			set
			{
				base.SetElement<RangeSets>(1, value);
			}
		}

		// Token: 0x06018875 RID: 100469 RVA: 0x003422E3 File Offset: 0x003404E3
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "autoPage" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018876 RID: 100470 RVA: 0x00342303 File Offset: 0x00340503
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Consolidation>(deep);
		}

		// Token: 0x06018877 RID: 100471 RVA: 0x0034230C File Offset: 0x0034050C
		// Note: this type is marked as 'beforefieldinit'.
		static Consolidation()
		{
			byte[] array = new byte[1];
			Consolidation.attributeNamespaceIds = array;
			Consolidation.eleTagNames = new string[] { "pages", "rangeSets" };
			Consolidation.eleNamespaceIds = new byte[] { 22, 22 };
		}

		// Token: 0x0400A0AE RID: 41134
		private const string tagName = "consolidation";

		// Token: 0x0400A0AF RID: 41135
		private const byte tagNsId = 22;

		// Token: 0x0400A0B0 RID: 41136
		internal const int ElementTypeIdConst = 11439;

		// Token: 0x0400A0B1 RID: 41137
		private static string[] attributeTagNames = new string[] { "autoPage" };

		// Token: 0x0400A0B2 RID: 41138
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400A0B3 RID: 41139
		private static readonly string[] eleTagNames;

		// Token: 0x0400A0B4 RID: 41140
		private static readonly byte[] eleNamespaceIds;
	}
}
