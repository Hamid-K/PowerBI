using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A36 RID: 10806
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FloatVariantValue))]
	internal class Progress : OpenXmlCompositeElement
	{
		// Token: 0x17007100 RID: 28928
		// (get) Token: 0x06015B82 RID: 88962 RVA: 0x00322662 File Offset: 0x00320862
		public override string LocalName
		{
			get
			{
				return "progress";
			}
		}

		// Token: 0x17007101 RID: 28929
		// (get) Token: 0x06015B83 RID: 88963 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007102 RID: 28930
		// (get) Token: 0x06015B84 RID: 88964 RVA: 0x00322669 File Offset: 0x00320869
		internal override int ElementTypeId
		{
			get
			{
				return 12226;
			}
		}

		// Token: 0x06015B85 RID: 88965 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015B86 RID: 88966 RVA: 0x00293ECF File Offset: 0x002920CF
		public Progress()
		{
		}

		// Token: 0x06015B87 RID: 88967 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Progress(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015B88 RID: 88968 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Progress(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015B89 RID: 88969 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Progress(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015B8A RID: 88970 RVA: 0x00322670 File Offset: 0x00320870
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "fltVal" == name)
			{
				return new FloatVariantValue();
			}
			return null;
		}

		// Token: 0x17007103 RID: 28931
		// (get) Token: 0x06015B8B RID: 88971 RVA: 0x0032268B File Offset: 0x0032088B
		internal override string[] ElementTagNames
		{
			get
			{
				return Progress.eleTagNames;
			}
		}

		// Token: 0x17007104 RID: 28932
		// (get) Token: 0x06015B8C RID: 88972 RVA: 0x00322692 File Offset: 0x00320892
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Progress.eleNamespaceIds;
			}
		}

		// Token: 0x17007105 RID: 28933
		// (get) Token: 0x06015B8D RID: 88973 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17007106 RID: 28934
		// (get) Token: 0x06015B8E RID: 88974 RVA: 0x00322699 File Offset: 0x00320899
		// (set) Token: 0x06015B8F RID: 88975 RVA: 0x003226A2 File Offset: 0x003208A2
		public FloatVariantValue FloatVariantValue
		{
			get
			{
				return base.GetElement<FloatVariantValue>(0);
			}
			set
			{
				base.SetElement<FloatVariantValue>(0, value);
			}
		}

		// Token: 0x06015B90 RID: 88976 RVA: 0x003226AC File Offset: 0x003208AC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Progress>(deep);
		}

		// Token: 0x04009487 RID: 38023
		private const string tagName = "progress";

		// Token: 0x04009488 RID: 38024
		private const byte tagNsId = 24;

		// Token: 0x04009489 RID: 38025
		internal const int ElementTypeIdConst = 12226;

		// Token: 0x0400948A RID: 38026
		private static readonly string[] eleTagNames = new string[] { "fltVal" };

		// Token: 0x0400948B RID: 38027
		private static readonly byte[] eleNamespaceIds = new byte[] { 24 };
	}
}
