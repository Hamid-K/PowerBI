using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F59 RID: 12121
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CustomXmlPlaceholder))]
	[ChildElementInfo(typeof(CustomXmlAttribute))]
	internal class CustomXmlProperties : OpenXmlCompositeElement
	{
		// Token: 0x1700903B RID: 36923
		// (get) Token: 0x0601A07F RID: 106623 RVA: 0x0035CA3B File Offset: 0x0035AC3B
		public override string LocalName
		{
			get
			{
				return "customXmlPr";
			}
		}

		// Token: 0x1700903C RID: 36924
		// (get) Token: 0x0601A080 RID: 106624 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x1700903D RID: 36925
		// (get) Token: 0x0601A081 RID: 106625 RVA: 0x0035CA42 File Offset: 0x0035AC42
		internal override int ElementTypeId
		{
			get
			{
				return 11776;
			}
		}

		// Token: 0x0601A082 RID: 106626 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601A083 RID: 106627 RVA: 0x00293ECF File Offset: 0x002920CF
		public CustomXmlProperties()
		{
		}

		// Token: 0x0601A084 RID: 106628 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CustomXmlProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A085 RID: 106629 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CustomXmlProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0601A086 RID: 106630 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CustomXmlProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0601A087 RID: 106631 RVA: 0x0035CA49 File Offset: 0x0035AC49
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "placeholder" == name)
			{
				return new CustomXmlPlaceholder();
			}
			if (23 == namespaceId && "attr" == name)
			{
				return new CustomXmlAttribute();
			}
			return null;
		}

		// Token: 0x1700903E RID: 36926
		// (get) Token: 0x0601A088 RID: 106632 RVA: 0x0035CA7C File Offset: 0x0035AC7C
		internal override string[] ElementTagNames
		{
			get
			{
				return CustomXmlProperties.eleTagNames;
			}
		}

		// Token: 0x1700903F RID: 36927
		// (get) Token: 0x0601A089 RID: 106633 RVA: 0x0035CA83 File Offset: 0x0035AC83
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return CustomXmlProperties.eleNamespaceIds;
			}
		}

		// Token: 0x17009040 RID: 36928
		// (get) Token: 0x0601A08A RID: 106634 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17009041 RID: 36929
		// (get) Token: 0x0601A08B RID: 106635 RVA: 0x0035CA8A File Offset: 0x0035AC8A
		// (set) Token: 0x0601A08C RID: 106636 RVA: 0x0035CA93 File Offset: 0x0035AC93
		public CustomXmlPlaceholder CustomXmlPlaceholder
		{
			get
			{
				return base.GetElement<CustomXmlPlaceholder>(0);
			}
			set
			{
				base.SetElement<CustomXmlPlaceholder>(0, value);
			}
		}

		// Token: 0x0601A08D RID: 106637 RVA: 0x0035CA9D File Offset: 0x0035AC9D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CustomXmlProperties>(deep);
		}

		// Token: 0x0400AB7C RID: 43900
		private const string tagName = "customXmlPr";

		// Token: 0x0400AB7D RID: 43901
		private const byte tagNsId = 23;

		// Token: 0x0400AB7E RID: 43902
		internal const int ElementTypeIdConst = 11776;

		// Token: 0x0400AB7F RID: 43903
		private static readonly string[] eleTagNames = new string[] { "placeholder", "attr" };

		// Token: 0x0400AB80 RID: 43904
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23 };
	}
}
