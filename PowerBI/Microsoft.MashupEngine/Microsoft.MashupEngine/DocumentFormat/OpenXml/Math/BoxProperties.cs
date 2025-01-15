using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Math
{
	// Token: 0x0200299E RID: 10654
	[ChildElementInfo(typeof(Alignment))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(OperatorEmulator))]
	[ChildElementInfo(typeof(NoBreak))]
	[ChildElementInfo(typeof(Differential))]
	[ChildElementInfo(typeof(ControlProperties))]
	[ChildElementInfo(typeof(Break))]
	internal class BoxProperties : OpenXmlCompositeElement
	{
		// Token: 0x17006CFB RID: 27899
		// (get) Token: 0x060152AD RID: 86701 RVA: 0x0031C545 File Offset: 0x0031A745
		public override string LocalName
		{
			get
			{
				return "boxPr";
			}
		}

		// Token: 0x17006CFC RID: 27900
		// (get) Token: 0x060152AE RID: 86702 RVA: 0x002436D1 File Offset: 0x002418D1
		internal override byte NamespaceId
		{
			get
			{
				return 21;
			}
		}

		// Token: 0x17006CFD RID: 27901
		// (get) Token: 0x060152AF RID: 86703 RVA: 0x0031C54C File Offset: 0x0031A74C
		internal override int ElementTypeId
		{
			get
			{
				return 10879;
			}
		}

		// Token: 0x060152B0 RID: 86704 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060152B1 RID: 86705 RVA: 0x00293ECF File Offset: 0x002920CF
		public BoxProperties()
		{
		}

		// Token: 0x060152B2 RID: 86706 RVA: 0x00293ED7 File Offset: 0x002920D7
		public BoxProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060152B3 RID: 86707 RVA: 0x00293EE0 File Offset: 0x002920E0
		public BoxProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060152B4 RID: 86708 RVA: 0x00293EE9 File Offset: 0x002920E9
		public BoxProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060152B5 RID: 86709 RVA: 0x0031C554 File Offset: 0x0031A754
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (21 == namespaceId && "opEmu" == name)
			{
				return new OperatorEmulator();
			}
			if (21 == namespaceId && "noBreak" == name)
			{
				return new NoBreak();
			}
			if (21 == namespaceId && "diff" == name)
			{
				return new Differential();
			}
			if (21 == namespaceId && "brk" == name)
			{
				return new Break();
			}
			if (21 == namespaceId && "aln" == name)
			{
				return new Alignment();
			}
			if (21 == namespaceId && "ctrlPr" == name)
			{
				return new ControlProperties();
			}
			return null;
		}

		// Token: 0x17006CFE RID: 27902
		// (get) Token: 0x060152B6 RID: 86710 RVA: 0x0031C5F2 File Offset: 0x0031A7F2
		internal override string[] ElementTagNames
		{
			get
			{
				return BoxProperties.eleTagNames;
			}
		}

		// Token: 0x17006CFF RID: 27903
		// (get) Token: 0x060152B7 RID: 86711 RVA: 0x0031C5F9 File Offset: 0x0031A7F9
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return BoxProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17006D00 RID: 27904
		// (get) Token: 0x060152B8 RID: 86712 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17006D01 RID: 27905
		// (get) Token: 0x060152B9 RID: 86713 RVA: 0x0031C600 File Offset: 0x0031A800
		// (set) Token: 0x060152BA RID: 86714 RVA: 0x0031C609 File Offset: 0x0031A809
		public OperatorEmulator OperatorEmulator
		{
			get
			{
				return base.GetElement<OperatorEmulator>(0);
			}
			set
			{
				base.SetElement<OperatorEmulator>(0, value);
			}
		}

		// Token: 0x17006D02 RID: 27906
		// (get) Token: 0x060152BB RID: 86715 RVA: 0x0031C613 File Offset: 0x0031A813
		// (set) Token: 0x060152BC RID: 86716 RVA: 0x0031C61C File Offset: 0x0031A81C
		public NoBreak NoBreak
		{
			get
			{
				return base.GetElement<NoBreak>(1);
			}
			set
			{
				base.SetElement<NoBreak>(1, value);
			}
		}

		// Token: 0x17006D03 RID: 27907
		// (get) Token: 0x060152BD RID: 86717 RVA: 0x0031C626 File Offset: 0x0031A826
		// (set) Token: 0x060152BE RID: 86718 RVA: 0x0031C62F File Offset: 0x0031A82F
		public Differential Differential
		{
			get
			{
				return base.GetElement<Differential>(2);
			}
			set
			{
				base.SetElement<Differential>(2, value);
			}
		}

		// Token: 0x17006D04 RID: 27908
		// (get) Token: 0x060152BF RID: 86719 RVA: 0x0031C639 File Offset: 0x0031A839
		// (set) Token: 0x060152C0 RID: 86720 RVA: 0x0031C642 File Offset: 0x0031A842
		public Break Break
		{
			get
			{
				return base.GetElement<Break>(3);
			}
			set
			{
				base.SetElement<Break>(3, value);
			}
		}

		// Token: 0x17006D05 RID: 27909
		// (get) Token: 0x060152C1 RID: 86721 RVA: 0x0031C64C File Offset: 0x0031A84C
		// (set) Token: 0x060152C2 RID: 86722 RVA: 0x0031C655 File Offset: 0x0031A855
		public Alignment Alignment
		{
			get
			{
				return base.GetElement<Alignment>(4);
			}
			set
			{
				base.SetElement<Alignment>(4, value);
			}
		}

		// Token: 0x17006D06 RID: 27910
		// (get) Token: 0x060152C3 RID: 86723 RVA: 0x0031C65F File Offset: 0x0031A85F
		// (set) Token: 0x060152C4 RID: 86724 RVA: 0x0031C668 File Offset: 0x0031A868
		public ControlProperties ControlProperties
		{
			get
			{
				return base.GetElement<ControlProperties>(5);
			}
			set
			{
				base.SetElement<ControlProperties>(5, value);
			}
		}

		// Token: 0x060152C5 RID: 86725 RVA: 0x0031C672 File Offset: 0x0031A872
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<BoxProperties>(deep);
		}

		// Token: 0x040091ED RID: 37357
		private const string tagName = "boxPr";

		// Token: 0x040091EE RID: 37358
		private const byte tagNsId = 21;

		// Token: 0x040091EF RID: 37359
		internal const int ElementTypeIdConst = 10879;

		// Token: 0x040091F0 RID: 37360
		private static readonly string[] eleTagNames = new string[] { "opEmu", "noBreak", "diff", "brk", "aln", "ctrlPr" };

		// Token: 0x040091F1 RID: 37361
		private static readonly byte[] eleNamespaceIds = new byte[] { 21, 21, 21, 21, 21, 21 };
	}
}
