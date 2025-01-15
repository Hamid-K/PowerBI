using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml
{
	// Token: 0x020020E4 RID: 8420
	internal class AlternateContent : OpenXmlCompositeElement
	{
		// Token: 0x0600CF06 RID: 52998 RVA: 0x00293ECF File Offset: 0x002920CF
		public AlternateContent()
		{
		}

		// Token: 0x0600CF07 RID: 52999 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AlternateContent(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600CF08 RID: 53000 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AlternateContent(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600CF09 RID: 53001 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AlternateContent(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x170031BB RID: 12731
		// (get) Token: 0x0600CF0A RID: 53002 RVA: 0x00293EF2 File Offset: 0x002920F2
		public static string MarkupCompatibilityNamespace
		{
			get
			{
				return AlternateContent._mcNamespace;
			}
		}

		// Token: 0x170031BC RID: 12732
		// (get) Token: 0x0600CF0B RID: 53003 RVA: 0x00293EF9 File Offset: 0x002920F9
		public static byte MarkupCompatibilityNamespaceId
		{
			get
			{
				if (AlternateContent._mcNamespaceId == 255)
				{
					AlternateContent._mcNamespaceId = NamespaceIdMap.GetNamespaceId(AlternateContent._mcNamespace);
				}
				return AlternateContent._mcNamespaceId;
			}
		}

		// Token: 0x170031BD RID: 12733
		// (get) Token: 0x0600CF0C RID: 53004 RVA: 0x00293F1B File Offset: 0x0029211B
		public static string TagName
		{
			get
			{
				return AlternateContent.tagName;
			}
		}

		// Token: 0x170031BE RID: 12734
		// (get) Token: 0x0600CF0D RID: 53005 RVA: 0x00293F1B File Offset: 0x0029211B
		public override string LocalName
		{
			get
			{
				return AlternateContent.tagName;
			}
		}

		// Token: 0x170031BF RID: 12735
		// (get) Token: 0x0600CF0E RID: 53006 RVA: 0x00293F22 File Offset: 0x00292122
		internal override byte NamespaceId
		{
			get
			{
				return AlternateContent.MarkupCompatibilityNamespaceId;
			}
		}

		// Token: 0x170031C0 RID: 12736
		// (get) Token: 0x0600CF0F RID: 53007 RVA: 0x00293F29 File Offset: 0x00292129
		internal override string[] AttributeTagNames
		{
			get
			{
				return AlternateContent.attributeTagNames;
			}
		}

		// Token: 0x170031C1 RID: 12737
		// (get) Token: 0x0600CF10 RID: 53008 RVA: 0x00293F30 File Offset: 0x00292130
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AlternateContent.attributeNamespaceIds;
			}
		}

		// Token: 0x0600CF11 RID: 53009 RVA: 0x00293F37 File Offset: 0x00292137
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (AlternateContent.MarkupCompatibilityNamespaceId == namespaceId && AlternateContentChoice.TagName == name)
			{
				return new AlternateContentChoice();
			}
			if (AlternateContent.MarkupCompatibilityNamespaceId == namespaceId && AlternateContentFallback.TagName == name)
			{
				return new AlternateContentFallback();
			}
			return null;
		}

		// Token: 0x0600CF12 RID: 53010 RVA: 0x00293F70 File Offset: 0x00292170
		public AlternateContentChoice AppendNewAlternateContentChoice()
		{
			AlternateContentChoice alternateContentChoice = new AlternateContentChoice();
			this.AppendChild<AlternateContentChoice>(alternateContentChoice);
			return alternateContentChoice;
		}

		// Token: 0x0600CF13 RID: 53011 RVA: 0x00293F8C File Offset: 0x0029218C
		public AlternateContentFallback AppendNewAlternateContentFallback()
		{
			AlternateContentFallback alternateContentFallback = new AlternateContentFallback();
			this.AppendChild<AlternateContentFallback>(alternateContentFallback);
			return alternateContentFallback;
		}

		// Token: 0x0600CF14 RID: 53012 RVA: 0x000020FA File Offset: 0x000002FA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			return null;
		}

		// Token: 0x0600CF15 RID: 53013 RVA: 0x00293FA8 File Offset: 0x002921A8
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlternateContent>(deep);
		}

		// Token: 0x170031C2 RID: 12738
		// (get) Token: 0x0600CF16 RID: 53014 RVA: 0x00293FB1 File Offset: 0x002921B1
		internal override int ElementTypeId
		{
			get
			{
				return 9003;
			}
		}

		// Token: 0x0600CF17 RID: 53015 RVA: 0x00002139 File Offset: 0x00000339
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return true;
		}

		// Token: 0x04006869 RID: 26729
		private static string _mcNamespace = "http://schemas.openxmlformats.org/markup-compatibility/2006";

		// Token: 0x0400686A RID: 26730
		private static byte _mcNamespaceId = byte.MaxValue;

		// Token: 0x0400686B RID: 26731
		private static string tagName = "AlternateContent";

		// Token: 0x0400686C RID: 26732
		private static string[] attributeTagNames = new string[0];

		// Token: 0x0400686D RID: 26733
		private static byte[] attributeNamespaceIds = new byte[0];
	}
}
