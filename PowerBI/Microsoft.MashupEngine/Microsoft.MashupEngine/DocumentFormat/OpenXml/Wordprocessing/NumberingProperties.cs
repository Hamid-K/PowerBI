using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E25 RID: 11813
	[ChildElementInfo(typeof(NumberingLevelReference))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NumberingId))]
	[ChildElementInfo(typeof(NumberingChange))]
	[ChildElementInfo(typeof(Inserted))]
	internal class NumberingProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700891C RID: 35100
		// (get) Token: 0x060190F8 RID: 102648 RVA: 0x00345D1C File Offset: 0x00343F1C
		public override string LocalName
		{
			get
			{
				return "numPr";
			}
		}

		// Token: 0x1700891D RID: 35101
		// (get) Token: 0x060190F9 RID: 102649 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700891E RID: 35102
		// (get) Token: 0x060190FA RID: 102650 RVA: 0x00345D23 File Offset: 0x00343F23
		internal override int ElementTypeId
		{
			get
			{
				return 11498;
			}
		}

		// Token: 0x060190FB RID: 102651 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x060190FC RID: 102652 RVA: 0x00293ECF File Offset: 0x002920CF
		public NumberingProperties()
		{
		}

		// Token: 0x060190FD RID: 102653 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NumberingProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060190FE RID: 102654 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NumberingProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060190FF RID: 102655 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NumberingProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019100 RID: 102656 RVA: 0x00345D2C File Offset: 0x00343F2C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "ilvl" == name)
			{
				return new NumberingLevelReference();
			}
			if (23 == namespaceId && "numId" == name)
			{
				return new NumberingId();
			}
			if (23 == namespaceId && "numberingChange" == name)
			{
				return new NumberingChange();
			}
			if (23 == namespaceId && "ins" == name)
			{
				return new Inserted();
			}
			return null;
		}

		// Token: 0x1700891F RID: 35103
		// (get) Token: 0x06019101 RID: 102657 RVA: 0x00345D9A File Offset: 0x00343F9A
		internal override string[] ElementTagNames
		{
			get
			{
				return NumberingProperties.eleTagNames;
			}
		}

		// Token: 0x17008920 RID: 35104
		// (get) Token: 0x06019102 RID: 102658 RVA: 0x00345DA1 File Offset: 0x00343FA1
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NumberingProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17008921 RID: 35105
		// (get) Token: 0x06019103 RID: 102659 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17008922 RID: 35106
		// (get) Token: 0x06019104 RID: 102660 RVA: 0x00345DA8 File Offset: 0x00343FA8
		// (set) Token: 0x06019105 RID: 102661 RVA: 0x00345DB1 File Offset: 0x00343FB1
		public NumberingLevelReference NumberingLevelReference
		{
			get
			{
				return base.GetElement<NumberingLevelReference>(0);
			}
			set
			{
				base.SetElement<NumberingLevelReference>(0, value);
			}
		}

		// Token: 0x17008923 RID: 35107
		// (get) Token: 0x06019106 RID: 102662 RVA: 0x00345DBB File Offset: 0x00343FBB
		// (set) Token: 0x06019107 RID: 102663 RVA: 0x00345DC4 File Offset: 0x00343FC4
		public NumberingId NumberingId
		{
			get
			{
				return base.GetElement<NumberingId>(1);
			}
			set
			{
				base.SetElement<NumberingId>(1, value);
			}
		}

		// Token: 0x17008924 RID: 35108
		// (get) Token: 0x06019108 RID: 102664 RVA: 0x00345DCE File Offset: 0x00343FCE
		// (set) Token: 0x06019109 RID: 102665 RVA: 0x00345DD7 File Offset: 0x00343FD7
		public NumberingChange NumberingChange
		{
			get
			{
				return base.GetElement<NumberingChange>(2);
			}
			set
			{
				base.SetElement<NumberingChange>(2, value);
			}
		}

		// Token: 0x17008925 RID: 35109
		// (get) Token: 0x0601910A RID: 102666 RVA: 0x00345DE1 File Offset: 0x00343FE1
		// (set) Token: 0x0601910B RID: 102667 RVA: 0x00345DEA File Offset: 0x00343FEA
		public Inserted Inserted
		{
			get
			{
				return base.GetElement<Inserted>(3);
			}
			set
			{
				base.SetElement<Inserted>(3, value);
			}
		}

		// Token: 0x0601910C RID: 102668 RVA: 0x00345DF4 File Offset: 0x00343FF4
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumberingProperties>(deep);
		}

		// Token: 0x0400A6DC RID: 42716
		private const string tagName = "numPr";

		// Token: 0x0400A6DD RID: 42717
		private const byte tagNsId = 23;

		// Token: 0x0400A6DE RID: 42718
		internal const int ElementTypeIdConst = 11498;

		// Token: 0x0400A6DF RID: 42719
		private static readonly string[] eleTagNames = new string[] { "ilvl", "numId", "numberingChange", "ins" };

		// Token: 0x0400A6E0 RID: 42720
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23, 23 };
	}
}
