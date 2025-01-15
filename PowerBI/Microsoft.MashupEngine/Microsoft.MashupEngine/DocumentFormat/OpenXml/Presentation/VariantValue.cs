using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A39 RID: 10809
	[GeneratedCode("DomGen", "2.0")]
	internal class VariantValue : TimeListAnimationVariantType
	{
		// Token: 0x17007112 RID: 28946
		// (get) Token: 0x06015BAE RID: 89006 RVA: 0x002F2F88 File Offset: 0x002F1188
		public override string LocalName
		{
			get
			{
				return "val";
			}
		}

		// Token: 0x17007113 RID: 28947
		// (get) Token: 0x06015BAF RID: 89007 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x17007114 RID: 28948
		// (get) Token: 0x06015BB0 RID: 89008 RVA: 0x0032286B File Offset: 0x00320A6B
		internal override int ElementTypeId
		{
			get
			{
				return 12243;
			}
		}

		// Token: 0x06015BB1 RID: 89009 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06015BB2 RID: 89010 RVA: 0x0032283F File Offset: 0x00320A3F
		public VariantValue()
		{
		}

		// Token: 0x06015BB3 RID: 89011 RVA: 0x00322847 File Offset: 0x00320A47
		public VariantValue(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015BB4 RID: 89012 RVA: 0x00322850 File Offset: 0x00320A50
		public VariantValue(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06015BB5 RID: 89013 RVA: 0x00322859 File Offset: 0x00320A59
		public VariantValue(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06015BB6 RID: 89014 RVA: 0x00322872 File Offset: 0x00320A72
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VariantValue>(deep);
		}

		// Token: 0x04009491 RID: 38033
		private const string tagName = "val";

		// Token: 0x04009492 RID: 38034
		private const byte tagNsId = 24;

		// Token: 0x04009493 RID: 38035
		internal const int ElementTypeIdConst = 12243;
	}
}
