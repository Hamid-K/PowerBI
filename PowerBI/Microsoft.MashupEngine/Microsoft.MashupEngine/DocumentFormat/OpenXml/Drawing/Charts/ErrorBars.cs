using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x020025D2 RID: 9682
	[ChildElementInfo(typeof(ChartShapeProperties))]
	[ChildElementInfo(typeof(Plus))]
	[ChildElementInfo(typeof(ErrorBarValue))]
	[ChildElementInfo(typeof(Minus))]
	[ChildElementInfo(typeof(ExtensionList))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ErrorDirection))]
	[ChildElementInfo(typeof(ErrorBarType))]
	[ChildElementInfo(typeof(ErrorBarValueType))]
	[ChildElementInfo(typeof(NoEndCap))]
	internal class ErrorBars : OpenXmlCompositeElement
	{
		// Token: 0x17005815 RID: 22549
		// (get) Token: 0x060122FC RID: 74492 RVA: 0x002F6E05 File Offset: 0x002F5005
		public override string LocalName
		{
			get
			{
				return "errBars";
			}
		}

		// Token: 0x17005816 RID: 22550
		// (get) Token: 0x060122FD RID: 74493 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005817 RID: 22551
		// (get) Token: 0x060122FE RID: 74494 RVA: 0x002F6E0C File Offset: 0x002F500C
		internal override int ElementTypeId
		{
			get
			{
				return 10523;
			}
		}

		// Token: 0x060122FF RID: 74495 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06012300 RID: 74496 RVA: 0x00293ECF File Offset: 0x002920CF
		public ErrorBars()
		{
		}

		// Token: 0x06012301 RID: 74497 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ErrorBars(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012302 RID: 74498 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ErrorBars(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06012303 RID: 74499 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ErrorBars(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06012304 RID: 74500 RVA: 0x002F6E14 File Offset: 0x002F5014
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "errDir" == name)
			{
				return new ErrorDirection();
			}
			if (11 == namespaceId && "errBarType" == name)
			{
				return new ErrorBarType();
			}
			if (11 == namespaceId && "errValType" == name)
			{
				return new ErrorBarValueType();
			}
			if (11 == namespaceId && "noEndCap" == name)
			{
				return new NoEndCap();
			}
			if (11 == namespaceId && "plus" == name)
			{
				return new Plus();
			}
			if (11 == namespaceId && "minus" == name)
			{
				return new Minus();
			}
			if (11 == namespaceId && "val" == name)
			{
				return new ErrorBarValue();
			}
			if (11 == namespaceId && "spPr" == name)
			{
				return new ChartShapeProperties();
			}
			if (11 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17005818 RID: 22552
		// (get) Token: 0x06012305 RID: 74501 RVA: 0x002F6EFA File Offset: 0x002F50FA
		internal override string[] ElementTagNames
		{
			get
			{
				return ErrorBars.eleTagNames;
			}
		}

		// Token: 0x17005819 RID: 22553
		// (get) Token: 0x06012306 RID: 74502 RVA: 0x002F6F01 File Offset: 0x002F5101
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ErrorBars.eleNamespaceIds;
			}
		}

		// Token: 0x1700581A RID: 22554
		// (get) Token: 0x06012307 RID: 74503 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700581B RID: 22555
		// (get) Token: 0x06012308 RID: 74504 RVA: 0x002F6F08 File Offset: 0x002F5108
		// (set) Token: 0x06012309 RID: 74505 RVA: 0x002F6F11 File Offset: 0x002F5111
		public ErrorDirection ErrorDirection
		{
			get
			{
				return base.GetElement<ErrorDirection>(0);
			}
			set
			{
				base.SetElement<ErrorDirection>(0, value);
			}
		}

		// Token: 0x1700581C RID: 22556
		// (get) Token: 0x0601230A RID: 74506 RVA: 0x002F6F1B File Offset: 0x002F511B
		// (set) Token: 0x0601230B RID: 74507 RVA: 0x002F6F24 File Offset: 0x002F5124
		public ErrorBarType ErrorBarType
		{
			get
			{
				return base.GetElement<ErrorBarType>(1);
			}
			set
			{
				base.SetElement<ErrorBarType>(1, value);
			}
		}

		// Token: 0x1700581D RID: 22557
		// (get) Token: 0x0601230C RID: 74508 RVA: 0x002F6F2E File Offset: 0x002F512E
		// (set) Token: 0x0601230D RID: 74509 RVA: 0x002F6F37 File Offset: 0x002F5137
		public ErrorBarValueType ErrorBarValueType
		{
			get
			{
				return base.GetElement<ErrorBarValueType>(2);
			}
			set
			{
				base.SetElement<ErrorBarValueType>(2, value);
			}
		}

		// Token: 0x1700581E RID: 22558
		// (get) Token: 0x0601230E RID: 74510 RVA: 0x002F6F41 File Offset: 0x002F5141
		// (set) Token: 0x0601230F RID: 74511 RVA: 0x002F6F4A File Offset: 0x002F514A
		public NoEndCap NoEndCap
		{
			get
			{
				return base.GetElement<NoEndCap>(3);
			}
			set
			{
				base.SetElement<NoEndCap>(3, value);
			}
		}

		// Token: 0x1700581F RID: 22559
		// (get) Token: 0x06012310 RID: 74512 RVA: 0x002F6F54 File Offset: 0x002F5154
		// (set) Token: 0x06012311 RID: 74513 RVA: 0x002F6F5D File Offset: 0x002F515D
		public Plus Plus
		{
			get
			{
				return base.GetElement<Plus>(4);
			}
			set
			{
				base.SetElement<Plus>(4, value);
			}
		}

		// Token: 0x17005820 RID: 22560
		// (get) Token: 0x06012312 RID: 74514 RVA: 0x002F6F67 File Offset: 0x002F5167
		// (set) Token: 0x06012313 RID: 74515 RVA: 0x002F6F70 File Offset: 0x002F5170
		public Minus Minus
		{
			get
			{
				return base.GetElement<Minus>(5);
			}
			set
			{
				base.SetElement<Minus>(5, value);
			}
		}

		// Token: 0x17005821 RID: 22561
		// (get) Token: 0x06012314 RID: 74516 RVA: 0x002F6F7A File Offset: 0x002F517A
		// (set) Token: 0x06012315 RID: 74517 RVA: 0x002F6F83 File Offset: 0x002F5183
		public ErrorBarValue ErrorBarValue
		{
			get
			{
				return base.GetElement<ErrorBarValue>(6);
			}
			set
			{
				base.SetElement<ErrorBarValue>(6, value);
			}
		}

		// Token: 0x17005822 RID: 22562
		// (get) Token: 0x06012316 RID: 74518 RVA: 0x002F6F8D File Offset: 0x002F518D
		// (set) Token: 0x06012317 RID: 74519 RVA: 0x002F6F96 File Offset: 0x002F5196
		public ChartShapeProperties ChartShapeProperties
		{
			get
			{
				return base.GetElement<ChartShapeProperties>(7);
			}
			set
			{
				base.SetElement<ChartShapeProperties>(7, value);
			}
		}

		// Token: 0x17005823 RID: 22563
		// (get) Token: 0x06012318 RID: 74520 RVA: 0x002F2889 File Offset: 0x002F0A89
		// (set) Token: 0x06012319 RID: 74521 RVA: 0x002F2892 File Offset: 0x002F0A92
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(8);
			}
			set
			{
				base.SetElement<ExtensionList>(8, value);
			}
		}

		// Token: 0x0601231A RID: 74522 RVA: 0x002F6FA0 File Offset: 0x002F51A0
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ErrorBars>(deep);
		}

		// Token: 0x04007E8E RID: 32398
		private const string tagName = "errBars";

		// Token: 0x04007E8F RID: 32399
		private const byte tagNsId = 11;

		// Token: 0x04007E90 RID: 32400
		internal const int ElementTypeIdConst = 10523;

		// Token: 0x04007E91 RID: 32401
		private static readonly string[] eleTagNames = new string[] { "errDir", "errBarType", "errValType", "noEndCap", "plus", "minus", "val", "spPr", "extLst" };

		// Token: 0x04007E92 RID: 32402
		private static readonly byte[] eleNamespaceIds = new byte[] { 11, 11, 11, 11, 11, 11, 11, 11, 11 };
	}
}
