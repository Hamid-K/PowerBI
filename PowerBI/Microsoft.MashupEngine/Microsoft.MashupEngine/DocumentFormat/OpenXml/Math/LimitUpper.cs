using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200295C RID: 10588
	[ChildElementInfo(typeof(Limit))]
	[ChildElementInfo(typeof(Base))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(LimitUpperProperties))]
	internal class LimitUpper : OpenXmlCompositeElement
	{
		// Token: 0x17006BD6 RID: 27606
		// (get) Token: 0x0601501F RID: 86047 RVA: 0x00319D9C File Offset: 0x00317F9C
		public override string LocalName
		{
			get
			{
				return "limUpp";
			}
		}

		// Token: 0x17006BD7 RID: 27607
		// (get) Token: 0x06015020 RID: 86048 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006BD8 RID: 27608
		// (get) Token: 0x06015021 RID: 86049 RVA: 0x00319DA3 File Offset: 0x00317FA3
		internal override int ElementTypeId
		{
			get
			{
				return 10852;
			}
		}

		// Token: 0x06015022 RID: 86050 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015023 RID: 86051 RVA: 0x00293ECF File Offset: 0x002920CF
		public LimitUpper()
		{
		}

		// Token: 0x06015024 RID: 86052 RVA: 0x00293ED7 File Offset: 0x002920D7
		public LimitUpper(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015025 RID: 86053 RVA: 0x00293EE0 File Offset: 0x002920E0
		public LimitUpper(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015026 RID: 86054 RVA: 0x00293EE9 File Offset: 0x002920E9
		public LimitUpper(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015027 RID: 86055 RVA: 0x00319DAC File Offset: 0x00317FAC
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "limUppPr" == name)
			{
				return new LimitUpperProperties();
			}
			if (21 == namespaceId && "e" == name)
			{
				return new Base();
			}
			if (21 == namespaceId && "lim" == name)
			{
				return new Limit();
			}
			return null;
		}

		// Token: 0x17006BD9 RID: 27609
		// (get) Token: 0x06015028 RID: 86056 RVA: 0x00319E02 File Offset: 0x00318002
		internal override string[] ElementTagNames
		{
			get
			{
				return LimitUpper.eleTagNames;
			}
		}

		// Token: 0x17006BDA RID: 27610
		// (get) Token: 0x06015029 RID: 86057 RVA: 0x00319E09 File Offset: 0x00318009
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return LimitUpper.eleNamespaceIds;
			}
		}

		// Token: 0x17006BDB RID: 27611
		// (get) Token: 0x0601502A RID: 86058 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006BDC RID: 27612
		// (get) Token: 0x0601502B RID: 86059 RVA: 0x00319E10 File Offset: 0x00318010
		// (set) Token: 0x0601502C RID: 86060 RVA: 0x00319E19 File Offset: 0x00318019
		public LimitUpperProperties LimitUpperProperties
		{
			get
			{
				return base.GetElement<LimitUpperProperties>(0);
			}
			set
			{
				base.SetElement<LimitUpperProperties>(0, value);
			}
		}

		// Token: 0x17006BDD RID: 27613
		// (get) Token: 0x0601502D RID: 86061 RVA: 0x00319656 File Offset: 0x00317856
		// (set) Token: 0x0601502E RID: 86062 RVA: 0x0031965F File Offset: 0x0031785F
		public Base Base
		{
			get
			{
				return base.GetElement<Base>(1);
			}
			set
			{
				base.SetElement<Base>(1, value);
			}
		}

		// Token: 0x17006BDE RID: 27614
		// (get) Token: 0x0601502F RID: 86063 RVA: 0x00319D37 File Offset: 0x00317F37
		// (set) Token: 0x06015030 RID: 86064 RVA: 0x00319D40 File Offset: 0x00317F40
		public Limit Limit
		{
			get
			{
				return base.GetElement<Limit>(2);
			}
			set
			{
				base.SetElement<Limit>(2, value);
			}
		}

		// Token: 0x06015031 RID: 86065 RVA: 0x00319E23 File Offset: 0x00318023
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<LimitUpper>(deep);
		}

		// Token: 0x0400910B RID: 37131
		private const string tagName = "limUpp";

		// Token: 0x0400910C RID: 37132
		private const byte tagNsId = 21;

		// Token: 0x0400910D RID: 37133
		internal const int ElementTypeIdConst = 10852;

		// Token: 0x0400910E RID: 37134
		private static readonly string[] eleTagNames = new string[] { "limUppPr", "e", "lim" };

		// Token: 0x0400910F RID: 37135
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21 };
	}
}
