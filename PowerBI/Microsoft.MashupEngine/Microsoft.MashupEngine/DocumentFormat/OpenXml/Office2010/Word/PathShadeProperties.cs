using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Office2010.Word
{
	// Token: 0x02002499 RID: 9369
	[OfficeAvailability(FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FillToRectangle), FileFormatVersions.Office2010)]
	internal class PathShadeProperties : OpenXmlCompositeElement
	{
		// Token: 0x170051AA RID: 20906
		// (get) Token: 0x060114C7 RID: 70855 RVA: 0x002BFFB6 File Offset: 0x002BE1B6
		public override string LocalName
		{
			get
			{
				return "path";
			}
		}

		// Token: 0x170051AB RID: 20907
		// (get) Token: 0x060114C8 RID: 70856 RVA: 0x002EC7B7 File Offset: 0x002EA9B7
		internal override byte NamespaceId
		{
			get
			{
				return 52;
			}
		}

		// Token: 0x170051AC RID: 20908
		// (get) Token: 0x060114C9 RID: 70857 RVA: 0x002ECEB1 File Offset: 0x002EB0B1
		internal override int ElementTypeId
		{
			get
			{
				return 12845;
			}
		}

		// Token: 0x060114CA RID: 70858 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x170051AD RID: 20909
		// (get) Token: 0x060114CB RID: 70859 RVA: 0x002ECEB8 File Offset: 0x002EB0B8
		internal override string[] AttributeTagNames
		{
			get
			{
				return PathShadeProperties.attributeTagNames;
			}
		}

		// Token: 0x170051AE RID: 20910
		// (get) Token: 0x060114CC RID: 70860 RVA: 0x002ECEBF File Offset: 0x002EB0BF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PathShadeProperties.attributeNamespaceIds;
			}
		}

		// Token: 0x170051AF RID: 20911
		// (get) Token: 0x060114CD RID: 70861 RVA: 0x002ECEC6 File Offset: 0x002EB0C6
		// (set) Token: 0x060114CE RID: 70862 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(52, "path")]
		public EnumValue<PathShadeTypeValues> Path
		{
			get
			{
				return (EnumValue<PathShadeTypeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x060114CF RID: 70863 RVA: 0x00293ECF File Offset: 0x002920CF
		public PathShadeProperties()
		{
		}

		// Token: 0x060114D0 RID: 70864 RVA: 0x00293ED7 File Offset: 0x002920D7
		public PathShadeProperties(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060114D1 RID: 70865 RVA: 0x00293EE0 File Offset: 0x002920E0
		public PathShadeProperties(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060114D2 RID: 70866 RVA: 0x00293EE9 File Offset: 0x002920E9
		public PathShadeProperties(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060114D3 RID: 70867 RVA: 0x002ECED5 File Offset: 0x002EB0D5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "fillToRect" == name)
			{
				return new FillToRectangle();
			}
			return null;
		}

		// Token: 0x170051B0 RID: 20912
		// (get) Token: 0x060114D4 RID: 70868 RVA: 0x002ECEF0 File Offset: 0x002EB0F0
		internal override string[] ElementTagNames
		{
			get
			{
				return PathShadeProperties.eleTagNames;
			}
		}

		// Token: 0x170051B1 RID: 20913
		// (get) Token: 0x060114D5 RID: 70869 RVA: 0x002ECEF7 File Offset: 0x002EB0F7
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return PathShadeProperties.eleNamespaceIds;
			}
		}

		// Token: 0x170051B2 RID: 20914
		// (get) Token: 0x060114D6 RID: 70870 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170051B3 RID: 20915
		// (get) Token: 0x060114D7 RID: 70871 RVA: 0x002ECEFE File Offset: 0x002EB0FE
		// (set) Token: 0x060114D8 RID: 70872 RVA: 0x002ECF07 File Offset: 0x002EB107
		public FillToRectangle FillToRectangle
		{
			get
			{
				return base.GetElement<FillToRectangle>(0);
			}
			set
			{
				base.SetElement<FillToRectangle>(0, value);
			}
		}

		// Token: 0x060114D9 RID: 70873 RVA: 0x002ECF11 File Offset: 0x002EB111
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (52 == namespaceId && "path" == name)
			{
				return new EnumValue<PathShadeTypeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060114DA RID: 70874 RVA: 0x002ECF33 File Offset: 0x002EB133
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PathShadeProperties>(deep);
		}

		// Token: 0x0400792A RID: 31018
		private const string tagName = "path";

		// Token: 0x0400792B RID: 31019
		private const byte tagNsId = 52;

		// Token: 0x0400792C RID: 31020
		internal const int ElementTypeIdConst = 12845;

		// Token: 0x0400792D RID: 31021
		private static string[] attributeTagNames = new string[] { "path" };

		// Token: 0x0400792E RID: 31022
		private static byte[] attributeNamespaceIds = new byte[] { 52 };

		// Token: 0x0400792F RID: 31023
		private static readonly string[] eleTagNames = new string[] { "fillToRect" };

		// Token: 0x04007930 RID: 31024
		private static readonly byte[] eleNamespaceIds = new byte[] { 52 };
	}
}
