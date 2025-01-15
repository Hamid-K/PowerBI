using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002552 RID: 9554
	[ChildElementInfo(typeof(MaxAxisValue))]
	[ChildElementInfo(typeof(Orientation))]
	[ChildElementInfo(typeof(LogBase))]
	[ChildElementInfo(typeof(MinAxisValue))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Scaling : OpenXmlCompositeElement
	{
		// Token: 0x17005558 RID: 21848
		// (get) Token: 0x06011CDF RID: 72927 RVA: 0x002F2954 File Offset: 0x002F0B54
		public override string LocalName
		{
			get
			{
				return "scaling";
			}
		}

		// Token: 0x17005559 RID: 21849
		// (get) Token: 0x06011CE0 RID: 72928 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x1700555A RID: 21850
		// (get) Token: 0x06011CE1 RID: 72929 RVA: 0x002F295B File Offset: 0x002F0B5B
		internal override int ElementTypeId
		{
			get
			{
				return 10374;
			}
		}

		// Token: 0x06011CE2 RID: 72930 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06011CE3 RID: 72931 RVA: 0x00293ECF File Offset: 0x002920CF
		public Scaling()
		{
		}

		// Token: 0x06011CE4 RID: 72932 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Scaling(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011CE5 RID: 72933 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Scaling(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011CE6 RID: 72934 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Scaling(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011CE7 RID: 72935 RVA: 0x002F2964 File Offset: 0x002F0B64
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "logBase" == name)
			{
				return new LogBase();
			}
			if (11 == namespaceId && "orientation" == name)
			{
				return new Orientation();
			}
			if (11 == namespaceId && "max" == name)
			{
				return new MaxAxisValue();
			}
			if (11 == namespaceId && "min" == name)
			{
				return new MinAxisValue();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x1700555B RID: 21851
		// (get) Token: 0x06011CE8 RID: 72936 RVA: 0x002F29EA File Offset: 0x002F0BEA
		internal override string[] ElementTagNames
		{
			get
			{
				return Scaling.eleTagNames;
			}
		}

		// Token: 0x1700555C RID: 21852
		// (get) Token: 0x06011CE9 RID: 72937 RVA: 0x002F29F1 File Offset: 0x002F0BF1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Scaling.eleNamespaceIds;
			}
		}

		// Token: 0x1700555D RID: 21853
		// (get) Token: 0x06011CEA RID: 72938 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700555E RID: 21854
		// (get) Token: 0x06011CEB RID: 72939 RVA: 0x002F29F8 File Offset: 0x002F0BF8
		// (set) Token: 0x06011CEC RID: 72940 RVA: 0x002F2A01 File Offset: 0x002F0C01
		public LogBase LogBase
		{
			get
			{
				return base.GetElement<LogBase>(0);
			}
			set
			{
				base.SetElement<LogBase>(0, value);
			}
		}

		// Token: 0x1700555F RID: 21855
		// (get) Token: 0x06011CED RID: 72941 RVA: 0x002F2A0B File Offset: 0x002F0C0B
		// (set) Token: 0x06011CEE RID: 72942 RVA: 0x002F2A14 File Offset: 0x002F0C14
		public Orientation Orientation
		{
			get
			{
				return base.GetElement<Orientation>(1);
			}
			set
			{
				base.SetElement<Orientation>(1, value);
			}
		}

		// Token: 0x17005560 RID: 21856
		// (get) Token: 0x06011CEF RID: 72943 RVA: 0x002F2A1E File Offset: 0x002F0C1E
		// (set) Token: 0x06011CF0 RID: 72944 RVA: 0x002F2A27 File Offset: 0x002F0C27
		public MaxAxisValue MaxAxisValue
		{
			get
			{
				return base.GetElement<MaxAxisValue>(2);
			}
			set
			{
				base.SetElement<MaxAxisValue>(2, value);
			}
		}

		// Token: 0x17005561 RID: 21857
		// (get) Token: 0x06011CF1 RID: 72945 RVA: 0x002F2A31 File Offset: 0x002F0C31
		// (set) Token: 0x06011CF2 RID: 72946 RVA: 0x002F2A3A File Offset: 0x002F0C3A
		public MinAxisValue MinAxisValue
		{
			get
			{
				return base.GetElement<MinAxisValue>(3);
			}
			set
			{
				base.SetElement<MinAxisValue>(3, value);
			}
		}

		// Token: 0x17005562 RID: 21858
		// (get) Token: 0x06011CF3 RID: 72947 RVA: 0x002F2A44 File Offset: 0x002F0C44
		// (set) Token: 0x06011CF4 RID: 72948 RVA: 0x002F2A4D File Offset: 0x002F0C4D
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(4);
			}
			set
			{
				base.SetElement<ExtensionList>(4, value);
			}
		}

		// Token: 0x06011CF5 RID: 72949 RVA: 0x002F2A57 File Offset: 0x002F0C57
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Scaling>(deep);
		}

		// Token: 0x04007C9E RID: 31902
		private const string tagName = "scaling";

		// Token: 0x04007C9F RID: 31903
		private const byte tagNsId = 11;

		// Token: 0x04007CA0 RID: 31904
		internal const int ElementTypeIdConst = 10374;

		// Token: 0x04007CA1 RID: 31905
		private static readonly string[] eleTagNames = new string[] { "logBase", "orientation", "max", "min", "extLst" };

		// Token: 0x04007CA2 RID: 31906
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11 };
	}
}
