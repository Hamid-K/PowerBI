using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B93 RID: 11155
	[ChildElementInfo(typeof(Text))]
	[ChildElementInfo(typeof(Run))]
	[ChildElementInfo(typeof(PhoneticRun))]
	[ChildElementInfo(typeof(PhoneticProperties))]
	[GeneratedCode("DomGen", "2.0")]
	internal abstract class RstType : OpenXmlCompositeElement
	{
		// Token: 0x06017219 RID: 94745 RVA: 0x00333214 File Offset: 0x00331414
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "t" == name)
			{
				return new Text();
			}
			if (22 == namespaceId && "r" == name)
			{
				return new Run();
			}
			if (22 == namespaceId && "rPh" == name)
			{
				return new PhoneticRun();
			}
			if (22 == namespaceId && "phoneticPr" == name)
			{
				return new PhoneticProperties();
			}
			return null;
		}

		// Token: 0x17007B14 RID: 31508
		// (get) Token: 0x0601721A RID: 94746 RVA: 0x00333282 File Offset: 0x00331482
		internal override string[] ElementTagNames
		{
			get
			{
				return RstType.eleTagNames;
			}
		}

		// Token: 0x17007B15 RID: 31509
		// (get) Token: 0x0601721B RID: 94747 RVA: 0x00333289 File Offset: 0x00331489
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return RstType.eleNamespaceIds;
			}
		}

		// Token: 0x17007B16 RID: 31510
		// (get) Token: 0x0601721C RID: 94748 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007B17 RID: 31511
		// (get) Token: 0x0601721D RID: 94749 RVA: 0x00333290 File Offset: 0x00331490
		// (set) Token: 0x0601721E RID: 94750 RVA: 0x00333299 File Offset: 0x00331499
		public Text Text
		{
			get
			{
				return base.GetElement<Text>(0);
			}
			set
			{
				base.SetElement<Text>(0, value);
			}
		}

		// Token: 0x0601721F RID: 94751 RVA: 0x00293ECF File Offset: 0x002920CF
		protected RstType()
		{
		}

		// Token: 0x06017220 RID: 94752 RVA: 0x00293ED7 File Offset: 0x002920D7
		protected RstType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017221 RID: 94753 RVA: 0x00293EE0 File Offset: 0x002920E0
		protected RstType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017222 RID: 94754 RVA: 0x00293EE9 File Offset: 0x002920E9
		protected RstType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x04009B2A RID: 39722
		private static readonly string[] eleTagNames = new string[] { "t", "r", "rPh", "phoneticPr" };

		// Token: 0x04009B2B RID: 39723
		private static readonly byte[] eleNamespaceIds = new byte[] { 22, 22, 22, 22 };
	}
}
