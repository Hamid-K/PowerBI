using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FAE RID: 12206
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(LatentStyleExceptionInfo))]
	internal class LatentStyles : OpenXmlCompositeElement
	{
		// Token: 0x17009351 RID: 37713
		// (get) Token: 0x0601A701 RID: 108289 RVA: 0x00362401 File Offset: 0x00360601
		public override string LocalName
		{
			get
			{
				return "latentStyles";
			}
		}

		// Token: 0x17009352 RID: 37714
		// (get) Token: 0x0601A702 RID: 108290 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009353 RID: 37715
		// (get) Token: 0x0601A703 RID: 108291 RVA: 0x00362408 File Offset: 0x00360608
		internal override int ElementTypeId
		{
			get
			{
				return 11913;
			}
		}

		// Token: 0x0601A704 RID: 108292 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009354 RID: 37716
		// (get) Token: 0x0601A705 RID: 108293 RVA: 0x0036240F File Offset: 0x0036060F
		internal override string[] AttributeTagNames
		{
			get
			{
				return LatentStyles.attributeTagNames;
			}
		}

		// Token: 0x17009355 RID: 37717
		// (get) Token: 0x0601A706 RID: 108294 RVA: 0x00362416 File Offset: 0x00360616
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return LatentStyles.attributeNamespaceIds;
			}
		}

		// Token: 0x17009356 RID: 37718
		// (get) Token: 0x0601A707 RID: 108295 RVA: 0x002EBFC4 File Offset: 0x002EA1C4
		// (set) Token: 0x0601A708 RID: 108296 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "defLockedState")]
		public OnOffValue DefaultLockedState
		{
			get
			{
				return (OnOffValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17009357 RID: 37719
		// (get) Token: 0x0601A709 RID: 108297 RVA: 0x002BF6AF File Offset: 0x002BD8AF
		// (set) Token: 0x0601A70A RID: 108298 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "defUIPriority")]
		public Int32Value DefaultUiPriority
		{
			get
			{
				return (Int32Value)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17009358 RID: 37720
		// (get) Token: 0x0601A70B RID: 108299 RVA: 0x003461ED File Offset: 0x003443ED
		// (set) Token: 0x0601A70C RID: 108300 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "defSemiHidden")]
		public OnOffValue DefaultSemiHidden
		{
			get
			{
				return (OnOffValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x17009359 RID: 37721
		// (get) Token: 0x0601A70D RID: 108301 RVA: 0x003474AC File Offset: 0x003456AC
		// (set) Token: 0x0601A70E RID: 108302 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(23, "defUnhideWhenUsed")]
		public OnOffValue DefaultUnhideWhenUsed
		{
			get
			{
				return (OnOffValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x1700935A RID: 37722
		// (get) Token: 0x0601A70F RID: 108303 RVA: 0x002EB443 File Offset: 0x002E9643
		// (set) Token: 0x0601A710 RID: 108304 RVA: 0x002BD4C8 File Offset: 0x002BB6C8
		[SchemaAttr(23, "defQFormat")]
		public OnOffValue DefaultPrimaryStyle
		{
			get
			{
				return (OnOffValue)base.Attributes[4];
			}
			set
			{
				base.Attributes[4] = value;
			}
		}

		// Token: 0x1700935B RID: 37723
		// (get) Token: 0x0601A711 RID: 108305 RVA: 0x002ED371 File Offset: 0x002EB571
		// (set) Token: 0x0601A712 RID: 108306 RVA: 0x002BD4E2 File Offset: 0x002BB6E2
		[SchemaAttr(23, "count")]
		public Int32Value Count
		{
			get
			{
				return (Int32Value)base.Attributes[5];
			}
			set
			{
				base.Attributes[5] = value;
			}
		}

		// Token: 0x0601A713 RID: 108307 RVA: 0x00293ECF File Offset: 0x002920CF
		public LatentStyles()
		{
		}

		// Token: 0x0601A714 RID: 108308 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LatentStyles(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A715 RID: 108309 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LatentStyles(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A716 RID: 108310 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LatentStyles(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A717 RID: 108311 RVA: 0x0036241D File Offset: 0x0036061D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "lsdException" == name)
			{
				return new LatentStyleExceptionInfo();
			}
			return null;
		}

		// Token: 0x0601A718 RID: 108312 RVA: 0x00362438 File Offset: 0x00360638
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "defLockedState" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "defUIPriority" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "defSemiHidden" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "defUnhideWhenUsed" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "defQFormat" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "count" == name)
			{
				return new Int32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A719 RID: 108313 RVA: 0x003624DD File Offset: 0x003606DD
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LatentStyles>(deep);
		}

		// Token: 0x0400ACF9 RID: 44281
		private const string tagName = "latentStyles";

		// Token: 0x0400ACFA RID: 44282
		private const byte tagNsId = 23;

		// Token: 0x0400ACFB RID: 44283
		internal const int ElementTypeIdConst = 11913;

		// Token: 0x0400ACFC RID: 44284
		private static string[] attributeTagNames = new string[] { "defLockedState", "defUIPriority", "defSemiHidden", "defUnhideWhenUsed", "defQFormat", "count" };

		// Token: 0x0400ACFD RID: 44285
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23, 23, 23, 23 };
	}
}
