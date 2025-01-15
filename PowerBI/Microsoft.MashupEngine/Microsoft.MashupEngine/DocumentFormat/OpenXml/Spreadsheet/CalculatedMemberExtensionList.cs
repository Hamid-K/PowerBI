using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CBE RID: 11454
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(CalculatedMemberExtension))]
	internal class CalculatedMemberExtensionList : OpenXmlCompositeElement
	{
		// Token: 0x170084DE RID: 34014
		// (get) Token: 0x0601881B RID: 100379 RVA: 0x002DF2B7 File Offset: 0x002DD4B7
		public override string LocalName
		{
			get
			{
				return "extLst";
			}
		}

		// Token: 0x170084DF RID: 34015
		// (get) Token: 0x0601881C RID: 100380 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170084E0 RID: 34016
		// (get) Token: 0x0601881D RID: 100381 RVA: 0x00342046 File Offset: 0x00340246
		internal override int ElementTypeId
		{
			get
			{
				return 11434;
			}
		}

		// Token: 0x0601881E RID: 100382 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x0601881F RID: 100383 RVA: 0x00293ECF File Offset: 0x002920CF
		public CalculatedMemberExtensionList()
		{
		}

		// Token: 0x06018820 RID: 100384 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CalculatedMemberExtensionList(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018821 RID: 100385 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CalculatedMemberExtensionList(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018822 RID: 100386 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CalculatedMemberExtensionList(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018823 RID: 100387 RVA: 0x0034204D File Offset: 0x0034024D
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "ext" == name)
			{
				return new CalculatedMemberExtension();
			}
			return null;
		}

		// Token: 0x06018824 RID: 100388 RVA: 0x00342068 File Offset: 0x00340268
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CalculatedMemberExtensionList>(deep);
		}

		// Token: 0x0400A099 RID: 41113
		private const string tagName = "extLst";

		// Token: 0x0400A09A RID: 41114
		private const byte tagNsId = 22;

		// Token: 0x0400A09B RID: 41115
		internal const int ElementTypeIdConst = 11434;
	}
}
