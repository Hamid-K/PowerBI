using System;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml
{
	// Token: 0x020020E5 RID: 8421
	internal class AlternateContentChoice : OpenXmlCompositeElement
	{
		// Token: 0x0600CF19 RID: 53017 RVA: 0x00293ECF File Offset: 0x002920CF
		public AlternateContentChoice()
		{
		}

		// Token: 0x0600CF1A RID: 53018 RVA: 0x00293ED7 File Offset: 0x002920D7
		public AlternateContentChoice(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600CF1B RID: 53019 RVA: 0x00293EE0 File Offset: 0x002920E0
		public AlternateContentChoice(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600CF1C RID: 53020 RVA: 0x00293EE9 File Offset: 0x002920E9
		public AlternateContentChoice(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x170031C3 RID: 12739
		// (get) Token: 0x0600CF1D RID: 53021 RVA: 0x00293FEE File Offset: 0x002921EE
		public static string TagName
		{
			get
			{
				return AlternateContentChoice.tagName;
			}
		}

		// Token: 0x170031C4 RID: 12740
		// (get) Token: 0x0600CF1E RID: 53022 RVA: 0x00293FEE File Offset: 0x002921EE
		public override string LocalName
		{
			get
			{
				return AlternateContentChoice.tagName;
			}
		}

		// Token: 0x170031C5 RID: 12741
		// (get) Token: 0x0600CF1F RID: 53023 RVA: 0x00293F22 File Offset: 0x00292122
		internal override byte NamespaceId
		{
			get
			{
				return AlternateContent.MarkupCompatibilityNamespaceId;
			}
		}

		// Token: 0x170031C6 RID: 12742
		// (get) Token: 0x0600CF20 RID: 53024 RVA: 0x00293FF5 File Offset: 0x002921F5
		internal override string[] AttributeTagNames
		{
			get
			{
				return AlternateContentChoice.attributeTagNames;
			}
		}

		// Token: 0x170031C7 RID: 12743
		// (get) Token: 0x0600CF21 RID: 53025 RVA: 0x00293FFC File Offset: 0x002921FC
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AlternateContentChoice.attributeNamespaceIds;
			}
		}

		// Token: 0x0600CF22 RID: 53026 RVA: 0x00294003 File Offset: 0x00292203
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "Requires" == name)
			{
				return new StringValue();
			}
			return null;
		}

		// Token: 0x170031C8 RID: 12744
		// (get) Token: 0x0600CF23 RID: 53027 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x0600CF24 RID: 53028 RVA: 0x0029402B File Offset: 0x0029222B
		public StringValue Requires
		{
			get
			{
				return (StringValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0600CF25 RID: 53029 RVA: 0x00294038 File Offset: 0x00292238
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			OpenXmlElement openXmlElement = null;
			if (base.Parent != null && base.Parent is AlternateContent)
			{
				OpenXmlElement parent = base.Parent.Parent;
				if (parent != null)
				{
					openXmlElement = parent.ElementFactory(namespaceId, name);
					if (openXmlElement == null)
					{
						openXmlElement = parent.AlternateContentElementFactory(namespaceId, name);
					}
				}
			}
			return openXmlElement;
		}

		// Token: 0x0600CF26 RID: 53030 RVA: 0x00294081 File Offset: 0x00292281
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AlternateContentChoice>(deep);
		}

		// Token: 0x170031C9 RID: 12745
		// (get) Token: 0x0600CF27 RID: 53031 RVA: 0x0029408A File Offset: 0x0029228A
		internal override int ElementTypeId
		{
			get
			{
				return 9004;
			}
		}

		// Token: 0x0600CF28 RID: 53032 RVA: 0x00002139 File Offset: 0x00000339
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return true;
		}

		// Token: 0x0600CF29 RID: 53033 RVA: 0x00294094 File Offset: 0x00292294
		// Note: this type is marked as 'beforefieldinit'.
		static AlternateContentChoice()
		{
			byte[] array = new byte[1];
			AlternateContentChoice.attributeNamespaceIds = array;
		}

		// Token: 0x0400686E RID: 26734
		private static string tagName = "Choice";

		// Token: 0x0400686F RID: 26735
		private static string[] attributeTagNames = new string[] { "Requires" };

		// Token: 0x04006870 RID: 26736
		private static byte[] attributeNamespaceIds;
	}
}
