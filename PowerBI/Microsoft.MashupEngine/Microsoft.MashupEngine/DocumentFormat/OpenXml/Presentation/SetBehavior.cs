using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A1F RID: 10783
	[ChildElementInfo(typeof(ToVariantValue))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CommonBehavior))]
	internal class SetBehavior : OpenXmlCompositeElement
	{
		// Token: 0x17007054 RID: 28756
		// (get) Token: 0x06015A01 RID: 88577 RVA: 0x0032162E File Offset: 0x0031F82E
		public override string LocalName
		{
			get
			{
				return "set";
			}
		}

		// Token: 0x17007055 RID: 28757
		// (get) Token: 0x06015A02 RID: 88578 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007056 RID: 28758
		// (get) Token: 0x06015A03 RID: 88579 RVA: 0x00321635 File Offset: 0x0031F835
		internal override int ElementTypeId
		{
			get
			{
				return 12209;
			}
		}

		// Token: 0x06015A04 RID: 88580 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015A05 RID: 88581 RVA: 0x00293ECF File Offset: 0x002920CF
		public SetBehavior()
		{
		}

		// Token: 0x06015A06 RID: 88582 RVA: 0x00293ED7 File Offset: 0x002920D7
		public SetBehavior(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015A07 RID: 88583 RVA: 0x00293EE0 File Offset: 0x002920E0
		public SetBehavior(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015A08 RID: 88584 RVA: 0x00293EE9 File Offset: 0x002920E9
		public SetBehavior(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015A09 RID: 88585 RVA: 0x0032163C File Offset: 0x0031F83C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cBhvr" == name)
			{
				return new CommonBehavior();
			}
			if (24 == namespaceId && "to" == name)
			{
				return new ToVariantValue();
			}
			return null;
		}

		// Token: 0x17007057 RID: 28759
		// (get) Token: 0x06015A0A RID: 88586 RVA: 0x0032166F File Offset: 0x0031F86F
		internal override string[] ElementTagNames
		{
			get
			{
				return SetBehavior.eleTagNames;
			}
		}

		// Token: 0x17007058 RID: 28760
		// (get) Token: 0x06015A0B RID: 88587 RVA: 0x00321676 File Offset: 0x0031F876
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return SetBehavior.eleNamespaceIds;
			}
		}

		// Token: 0x17007059 RID: 28761
		// (get) Token: 0x06015A0C RID: 88588 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700705A RID: 28762
		// (get) Token: 0x06015A0D RID: 88589 RVA: 0x00320C27 File Offset: 0x0031EE27
		// (set) Token: 0x06015A0E RID: 88590 RVA: 0x00320C30 File Offset: 0x0031EE30
		public CommonBehavior CommonBehavior
		{
			get
			{
				return base.GetElement<CommonBehavior>(0);
			}
			set
			{
				base.SetElement<CommonBehavior>(0, value);
			}
		}

		// Token: 0x1700705B RID: 28763
		// (get) Token: 0x06015A0F RID: 88591 RVA: 0x0032167D File Offset: 0x0031F87D
		// (set) Token: 0x06015A10 RID: 88592 RVA: 0x00321686 File Offset: 0x0031F886
		public ToVariantValue ToVariantValue
		{
			get
			{
				return base.GetElement<ToVariantValue>(1);
			}
			set
			{
				base.SetElement<ToVariantValue>(1, value);
			}
		}

		// Token: 0x06015A11 RID: 88593 RVA: 0x00321690 File Offset: 0x0031F890
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SetBehavior>(deep);
		}

		// Token: 0x04009424 RID: 37924
		private const string tagName = "set";

		// Token: 0x04009425 RID: 37925
		private const byte tagNsId = 24;

		// Token: 0x04009426 RID: 37926
		internal const int ElementTypeIdConst = 12209;

		// Token: 0x04009427 RID: 37927
		private static readonly string[] eleTagNames = new string[] { "cBhvr", "to" };

		// Token: 0x04009428 RID: 37928
		private static readonly byte[] eleNamespaceIds = new byte[] { 24, 24 };
	}
}
