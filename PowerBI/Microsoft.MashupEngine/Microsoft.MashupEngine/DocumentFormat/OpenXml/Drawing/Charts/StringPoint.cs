using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x0200257E RID: 9598
	[ChildElementInfo(typeof(NumericValue))]
	[GeneratedCode("DomGen", "2.0")]
	internal class StringPoint : OpenXmlCompositeElement
	{
		// Token: 0x17005611 RID: 22033
		// (get) Token: 0x06011E8A RID: 73354 RVA: 0x002F359C File Offset: 0x002F179C
		public override string LocalName
		{
			get
			{
				return "pt";
			}
		}

		// Token: 0x17005612 RID: 22034
		// (get) Token: 0x06011E8B RID: 73355 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x17005613 RID: 22035
		// (get) Token: 0x06011E8C RID: 73356 RVA: 0x002F3904 File Offset: 0x002F1B04
		internal override int ElementTypeId
		{
			get
			{
				return 10399;
			}
		}

		// Token: 0x06011E8D RID: 73357 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17005614 RID: 22036
		// (get) Token: 0x06011E8E RID: 73358 RVA: 0x002F390B File Offset: 0x002F1B0B
		internal override string[] AttributeTagNames
		{
			get
			{
				return StringPoint.attributeTagNames;
			}
		}

		// Token: 0x17005615 RID: 22037
		// (get) Token: 0x06011E8F RID: 73359 RVA: 0x002F3912 File Offset: 0x002F1B12
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return StringPoint.attributeNamespaceIds;
			}
		}

		// Token: 0x17005616 RID: 22038
		// (get) Token: 0x06011E90 RID: 73360 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06011E91 RID: 73361 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "idx")]
		public UInt32Value Index
		{
			get
			{
				return (UInt32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06011E92 RID: 73362 RVA: 0x00293ECF File Offset: 0x002920CF
		public StringPoint()
		{
		}

		// Token: 0x06011E93 RID: 73363 RVA: 0x00293ED7 File Offset: 0x002920D7
		public StringPoint(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011E94 RID: 73364 RVA: 0x00293EE0 File Offset: 0x002920E0
		public StringPoint(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011E95 RID: 73365 RVA: 0x00293EE9 File Offset: 0x002920E9
		public StringPoint(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011E96 RID: 73366 RVA: 0x002F35B8 File Offset: 0x002F17B8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "v" == name)
			{
				return new NumericValue();
			}
			return null;
		}

		// Token: 0x17005617 RID: 22039
		// (get) Token: 0x06011E97 RID: 73367 RVA: 0x002F3919 File Offset: 0x002F1B19
		internal override string[] ElementTagNames
		{
			get
			{
				return StringPoint.eleTagNames;
			}
		}

		// Token: 0x17005618 RID: 22040
		// (get) Token: 0x06011E98 RID: 73368 RVA: 0x002F3920 File Offset: 0x002F1B20
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return StringPoint.eleNamespaceIds;
			}
		}

		// Token: 0x17005619 RID: 22041
		// (get) Token: 0x06011E99 RID: 73369 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700561A RID: 22042
		// (get) Token: 0x06011E9A RID: 73370 RVA: 0x002F35E1 File Offset: 0x002F17E1
		// (set) Token: 0x06011E9B RID: 73371 RVA: 0x002F35EA File Offset: 0x002F17EA
		public NumericValue NumericValue
		{
			get
			{
				return base.GetElement<NumericValue>(0);
			}
			set
			{
				base.SetElement<NumericValue>(0, value);
			}
		}

		// Token: 0x06011E9C RID: 73372 RVA: 0x002F3927 File Offset: 0x002F1B27
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "idx" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011E9D RID: 73373 RVA: 0x002F3947 File Offset: 0x002F1B47
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<StringPoint>(deep);
		}

		// Token: 0x06011E9E RID: 73374 RVA: 0x002F3950 File Offset: 0x002F1B50
		// Note: this type is marked as 'beforefieldinit'.
		static StringPoint()
		{
			byte[] array = new byte[1];
			StringPoint.attributeNamespaceIds = array;
			StringPoint.eleTagNames = new string[] { "v" };
			StringPoint.eleNamespaceIds = new byte[] { 11 };
		}

		// Token: 0x04007D32 RID: 32050
		private const string tagName = "pt";

		// Token: 0x04007D33 RID: 32051
		private const byte tagNsId = 11;

		// Token: 0x04007D34 RID: 32052
		internal const int ElementTypeIdConst = 10399;

		// Token: 0x04007D35 RID: 32053
		private static string[] attributeTagNames = new string[] { "idx" };

		// Token: 0x04007D36 RID: 32054
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007D37 RID: 32055
		private static readonly string[] eleTagNames;

		// Token: 0x04007D38 RID: 32056
		private static readonly byte[] eleNamespaceIds;
	}
}
