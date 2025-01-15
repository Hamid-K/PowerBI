using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Drawing.Charts
{
	// Token: 0x02002318 RID: 8984
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(ShowSketchButton), FileFormatVersions.Office2010)]
	[ChildElementInfo(typeof(InSketchMode), FileFormatVersions.Office2010)]
	internal class SketchOptions : OpenXmlCompositeElement
	{
		// Token: 0x17004831 RID: 18481
		// (get) Token: 0x0600FFAC RID: 65452 RVA: 0x002DE224 File Offset: 0x002DC424
		public override string LocalName
		{
			get
			{
				return "sketchOptions";
			}
		}

		// Token: 0x17004832 RID: 18482
		// (get) Token: 0x0600FFAD RID: 65453 RVA: 0x002DE0C4 File Offset: 0x002DC2C4
		internal override byte NamespaceId
		{
			get
			{
				return 46;
			}
		}

		// Token: 0x17004833 RID: 18483
		// (get) Token: 0x0600FFAE RID: 65454 RVA: 0x002DE22B File Offset: 0x002DC42B
		internal override int ElementTypeId
		{
			get
			{
				return 12692;
			}
		}

		// Token: 0x0600FFAF RID: 65455 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x0600FFB0 RID: 65456 RVA: 0x00293ECF File Offset: 0x002920CF
		public SketchOptions()
		{
		}

		// Token: 0x0600FFB1 RID: 65457 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SketchOptions(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FFB2 RID: 65458 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SketchOptions(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600FFB3 RID: 65459 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SketchOptions(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600FFB4 RID: 65460 RVA: 0x002DE232 File Offset: 0x002DC432
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (46 == namespaceId && "inSketchMode" == name)
			{
				return new InSketchMode();
			}
			if (46 == namespaceId && "showSketchBtn" == name)
			{
				return new ShowSketchButton();
			}
			return null;
		}

		// Token: 0x17004834 RID: 18484
		// (get) Token: 0x0600FFB5 RID: 65461 RVA: 0x002DE265 File Offset: 0x002DC465
		internal override string[] ElementTagNames
		{
			get
			{
				return SketchOptions.eleTagNames;
			}
		}

		// Token: 0x17004835 RID: 18485
		// (get) Token: 0x0600FFB6 RID: 65462 RVA: 0x002DE26C File Offset: 0x002DC46C
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SketchOptions.eleNamespaceIds;
			}
		}

		// Token: 0x17004836 RID: 18486
		// (get) Token: 0x0600FFB7 RID: 65463 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004837 RID: 18487
		// (get) Token: 0x0600FFB8 RID: 65464 RVA: 0x002DE273 File Offset: 0x002DC473
		// (set) Token: 0x0600FFB9 RID: 65465 RVA: 0x002DE27C File Offset: 0x002DC47C
		public InSketchMode InSketchMode
		{
			get
			{
				return base.GetElement<InSketchMode>(0);
			}
			set
			{
				base.SetElement<InSketchMode>(0, value);
			}
		}

		// Token: 0x17004838 RID: 18488
		// (get) Token: 0x0600FFBA RID: 65466 RVA: 0x002DE286 File Offset: 0x002DC486
		// (set) Token: 0x0600FFBB RID: 65467 RVA: 0x002DE28F File Offset: 0x002DC48F
		public ShowSketchButton ShowSketchButton
		{
			get
			{
				return base.GetElement<ShowSketchButton>(1);
			}
			set
			{
				base.SetElement<ShowSketchButton>(1, value);
			}
		}

		// Token: 0x0600FFBC RID: 65468 RVA: 0x002DE299 File Offset: 0x002DC499
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SketchOptions>(deep);
		}

		// Token: 0x04007281 RID: 29313
		private const string tagName = "sketchOptions";

		// Token: 0x04007282 RID: 29314
		private const byte tagNsId = 46;

		// Token: 0x04007283 RID: 29315
		internal const int ElementTypeIdConst = 12692;

		// Token: 0x04007284 RID: 29316
		private static readonly string[] eleTagNames = new string[] { "inSketchMode", "showSketchBtn" };

		// Token: 0x04007285 RID: 29317
		private static readonly byte[] eleNamespaceIds = new byte[] { 46, 46 };
	}
}
