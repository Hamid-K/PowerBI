using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office.Excel;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x020023F8 RID: 9208
	[ChildElementInfo(typeof(Formula))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class DataValidationFormulaType : OpenXmlCompositeElement
	{
		// Token: 0x06010D54 RID: 68948 RVA: 0x002E7CE7 File Offset: 0x002E5EE7
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (32 == namespaceId && "f" == name)
			{
				return new Formula();
			}
			return null;
		}

		// Token: 0x17004E6B RID: 20075
		// (get) Token: 0x06010D55 RID: 68949 RVA: 0x002E7D02 File Offset: 0x002E5F02
		internal override string[] ElementTagNames
		{
			get
			{
				return DataValidationFormulaType.eleTagNames;
			}
		}

		// Token: 0x17004E6C RID: 20076
		// (get) Token: 0x06010D56 RID: 68950 RVA: 0x002E7D09 File Offset: 0x002E5F09
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return DataValidationFormulaType.eleNamespaceIds;
			}
		}

		// Token: 0x17004E6D RID: 20077
		// (get) Token: 0x06010D57 RID: 68951 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004E6E RID: 20078
		// (get) Token: 0x06010D58 RID: 68952 RVA: 0x002E7D10 File Offset: 0x002E5F10
		// (set) Token: 0x06010D59 RID: 68953 RVA: 0x002E7D19 File Offset: 0x002E5F19
		public Formula Formula
		{
			get
			{
				return base.GetElement<Formula>(0);
			}
			set
			{
				base.SetElement<Formula>(0, value);
			}
		}

		// Token: 0x06010D5A RID: 68954 RVA: 0x00293ECF File Offset: 0x002920CF
		protected DataValidationFormulaType()
		{
		}

		// Token: 0x06010D5B RID: 68955 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected DataValidationFormulaType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010D5C RID: 68956 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected DataValidationFormulaType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010D5D RID: 68957 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected DataValidationFormulaType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04007675 RID: 30325
		private static readonly string[] eleTagNames = new string[] { "f" };

		// Token: 0x04007676 RID: 30326
		private static readonly byte[] eleNamespaceIds = new byte[] { 32 };
	}
}
