using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x020029C8 RID: 10696
	[ChildElementInfo(typeof(ControlProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal class SubscriptProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006DF7 RID: 28151
		// (get) Token: 0x060154D1 RID: 87249 RVA: 0x0031DC60 File Offset: 0x0031BE60
		public override string LocalName
		{
			get
			{
				return "sSubPr";
			}
		}

		// Token: 0x17006DF8 RID: 28152
		// (get) Token: 0x060154D2 RID: 87250 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006DF9 RID: 28153
		// (get) Token: 0x060154D3 RID: 87251 RVA: 0x0031DC67 File Offset: 0x0031BE67
		internal override int ElementTypeId
		{
			get
			{
				return 10939;
			}
		}

		// Token: 0x060154D4 RID: 87252 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060154D5 RID: 87253 RVA: 0x00293ECF File Offset: 0x002920CF
		public SubscriptProperties()
		{
		}

		// Token: 0x060154D6 RID: 87254 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SubscriptProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060154D7 RID: 87255 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SubscriptProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060154D8 RID: 87256 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SubscriptProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060154D9 RID: 87257 RVA: 0x0031CFA7 File Offset: 0x0031B1A7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006DFA RID: 28154
		// (get) Token: 0x060154DA RID: 87258 RVA: 0x0031DC6E File Offset: 0x0031BE6E
		internal override string[] ElementTagNames
		{
			get
			{
				return SubscriptProperties.eleTagNames;
			}
		}

		// Token: 0x17006DFB RID: 28155
		// (get) Token: 0x060154DB RID: 87259 RVA: 0x0031DC75 File Offset: 0x0031BE75
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SubscriptProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006DFC RID: 28156
		// (get) Token: 0x060154DC RID: 87260 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006DFD RID: 28157
		// (get) Token: 0x060154DD RID: 87261 RVA: 0x0031CFD0 File Offset: 0x0031B1D0
		// (set) Token: 0x060154DE RID: 87262 RVA: 0x0031CFD9 File Offset: 0x0031B1D9
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

		// Token: 0x060154DF RID: 87263 RVA: 0x0031DC7C File Offset: 0x0031BE7C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SubscriptProperties>(deep);
		}

		// Token: 0x04009291 RID: 37521
		private const string tagName = "sSubPr";

		// Token: 0x04009292 RID: 37522
		private const byte tagNsId = 21;

		// Token: 0x04009293 RID: 37523
		internal const int ElementTypeIdConst = 10939;

		// Token: 0x04009294 RID: 37524
		private static readonly string[] eleTagNames = new string[] { "ctrlPr" };

		// Token: 0x04009295 RID: 37525
		private static readonly byte[] eleNamespaceIds = new byte[] { 21 };
	}
}
