using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml
{
	// Token: 0x020020E6 RID: 8422
	internal class AlternateContentFallback : OpenXmlCompositeElement
	{
		// Token: 0x0600CF2A RID: 53034 RVA: 0x00293ECF File Offset: 0x002920CF
		public AlternateContentFallback()
		{
		}

		// Token: 0x0600CF2B RID: 53035 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AlternateContentFallback(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600CF2C RID: 53036 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AlternateContentFallback(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600CF2D RID: 53037 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AlternateContentFallback(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x170031CA RID: 12746
		// (get) Token: 0x0600CF2E RID: 53038 RVA: 0x002940CD File Offset: 0x002922CD
		public static string TagName
		{
			get
			{
				return AlternateContentFallback.tagName;
			}
		}

		// Token: 0x170031CB RID: 12747
		// (get) Token: 0x0600CF2F RID: 53039 RVA: 0x002940CD File Offset: 0x002922CD
		public override string LocalName
		{
			get
			{
				return AlternateContentFallback.tagName;
			}
		}

		// Token: 0x170031CC RID: 12748
		// (get) Token: 0x0600CF30 RID: 53040 RVA: 0x00293F22 File Offset: 0x00292122
		internal override byte NamespaceId
		{
			get
			{
				return AlternateContent.MarkupCompatibilityNamespaceId;
			}
		}

		// Token: 0x170031CD RID: 12749
		// (get) Token: 0x0600CF31 RID: 53041 RVA: 0x002940D4 File Offset: 0x002922D4
		internal override string[] AttributeTagNames
		{
			get
			{
				return AlternateContentFallback.attributeTagNames;
			}
		}

		// Token: 0x170031CE RID: 12750
		// (get) Token: 0x0600CF32 RID: 53042 RVA: 0x002940DB File Offset: 0x002922DB
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AlternateContentFallback.attributeNamespaceIds;
			}
		}

		// Token: 0x0600CF33 RID: 53043 RVA: 0x000020FA File Offset: 0x000002FA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			return null;
		}

		// Token: 0x0600CF34 RID: 53044 RVA: 0x002940E4 File Offset: 0x002922E4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			OpenXmlElement openXmlElement = null;
			if (base.Parent != null && base.Parent is AlternateContent)
			{
				OpenXmlElement parent = base.Parent.Parent;
				if (parent != null)
				{
					openXmlElement = parent.ElementFactory(namespaceId, name);
				}
			}
			return openXmlElement;
		}

		// Token: 0x0600CF35 RID: 53045 RVA: 0x00294121 File Offset: 0x00292321
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlternateContentFallback>(deep);
		}

		// Token: 0x170031CF RID: 12751
		// (get) Token: 0x0600CF36 RID: 53046 RVA: 0x0029412A File Offset: 0x0029232A
		internal override int ElementTypeId
		{
			get
			{
				return 9005;
			}
		}

		// Token: 0x0600CF37 RID: 53047 RVA: 0x00002139 File Offset: 0x00000339
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return true;
		}

		// Token: 0x04006871 RID: 26737
		private static string tagName = "Fallback";

		// Token: 0x04006872 RID: 26738
		private static string[] attributeTagNames = new string[0];

		// Token: 0x04006873 RID: 26739
		private static byte[] attributeNamespaceIds = new byte[0];
	}
}
