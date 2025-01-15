using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029AE RID: 10670
	[ChildElementInfo(typeof(ControlProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class LimitLowerProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006D6B RID: 28011
		// (get) Token: 0x0601539D RID: 86941 RVA: 0x0031D140 File Offset: 0x0031B340
		public override string LocalName
		{
			get
			{
				return "limLowPr";
			}
		}

		// Token: 0x17006D6C RID: 28012
		// (get) Token: 0x0601539E RID: 86942 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006D6D RID: 28013
		// (get) Token: 0x0601539F RID: 86943 RVA: 0x0031D147 File Offset: 0x0031B347
		internal override int ElementTypeId
		{
			get
			{
				return 10909;
			}
		}

		// Token: 0x060153A0 RID: 86944 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060153A1 RID: 86945 RVA: 0x00293ECF File Offset: 0x002920CF
		public LimitLowerProperties()
		{
		}

		// Token: 0x060153A2 RID: 86946 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LimitLowerProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060153A3 RID: 86947 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LimitLowerProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060153A4 RID: 86948 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LimitLowerProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060153A5 RID: 86949 RVA: 0x0031CFA7 File Offset: 0x0031B1A7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006D6E RID: 28014
		// (get) Token: 0x060153A6 RID: 86950 RVA: 0x0031D14E File Offset: 0x0031B34E
		internal override string[] ElementTagNames
		{
			get
			{
				return LimitLowerProperties.eleTagNames;
			}
		}

		// Token: 0x17006D6F RID: 28015
		// (get) Token: 0x060153A7 RID: 86951 RVA: 0x0031D155 File Offset: 0x0031B355
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return LimitLowerProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006D70 RID: 28016
		// (get) Token: 0x060153A8 RID: 86952 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006D71 RID: 28017
		// (get) Token: 0x060153A9 RID: 86953 RVA: 0x0031CFD0 File Offset: 0x0031B1D0
		// (set) Token: 0x060153AA RID: 86954 RVA: 0x0031CFD9 File Offset: 0x0031B1D9
		public ControlProperties ControlProperties
		{
			get
			{
				return base.GetElement<ControlProperties>(0);
			}
			set
			{
				base.SetElement<ControlProperties>(0, value);
			}
		}

		// Token: 0x060153AB RID: 86955 RVA: 0x0031D15C File Offset: 0x0031B35C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LimitLowerProperties>(deep);
		}

		// Token: 0x0400922F RID: 37423
		private const string tagName = "limLowPr";

		// Token: 0x04009230 RID: 37424
		private const byte tagNsId = 21;

		// Token: 0x04009231 RID: 37425
		internal const int ElementTypeIdConst = 10909;

		// Token: 0x04009232 RID: 37426
		private static readonly string[] eleTagNames = new string[] { "ctrlPr" };

		// Token: 0x04009233 RID: 37427
		private static readonly byte[] eleNamespaceIds = new byte[] { 21 };
	}
}
