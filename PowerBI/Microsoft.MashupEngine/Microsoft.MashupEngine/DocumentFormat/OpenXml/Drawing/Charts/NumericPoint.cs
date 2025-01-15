using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Drawing.Charts
{
	// Token: 0x02002578 RID: 9592
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(NumericValue))]
	internal class NumericPoint : OpenXmlCompositeElement
	{
		// Token: 0x170055EF RID: 21999
		// (get) Token: 0x06011E36 RID: 73270 RVA: 0x002F359C File Offset: 0x002F179C
		public override string LocalName
		{
			get
			{
				return "pt";
			}
		}

		// Token: 0x170055F0 RID: 22000
		// (get) Token: 0x06011E37 RID: 73271 RVA: 0x0014213C File Offset: 0x0014033C
		internal override byte NamespaceId
		{
			get
			{
				return 11;
			}
		}

		// Token: 0x170055F1 RID: 22001
		// (get) Token: 0x06011E38 RID: 73272 RVA: 0x002F35A3 File Offset: 0x002F17A3
		internal override int ElementTypeId
		{
			get
			{
				return 10393;
			}
		}

		// Token: 0x06011E39 RID: 73273 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170055F2 RID: 22002
		// (get) Token: 0x06011E3A RID: 73274 RVA: 0x002F35AA File Offset: 0x002F17AA
		internal override string[] AttributeTagNames
		{
			get
			{
				return NumericPoint.attributeTagNames;
			}
		}

		// Token: 0x170055F3 RID: 22003
		// (get) Token: 0x06011E3B RID: 73275 RVA: 0x002F35B1 File Offset: 0x002F17B1
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return NumericPoint.attributeNamespaceIds;
			}
		}

		// Token: 0x170055F4 RID: 22004
		// (get) Token: 0x06011E3C RID: 73276 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06011E3D RID: 73277 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x170055F5 RID: 22005
		// (get) Token: 0x06011E3E RID: 73278 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x06011E3F RID: 73279 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "formatCode")]
		public StringValue FormatCode
		{
			get
			{
				return (StringValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06011E40 RID: 73280 RVA: 0x00293ECF File Offset: 0x002920CF
		public NumericPoint()
		{
		}

		// Token: 0x06011E41 RID: 73281 RVA: 0x00293ED7 File Offset: 0x002920D7
		public NumericPoint(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011E42 RID: 73282 RVA: 0x00293EE0 File Offset: 0x002920E0
		public NumericPoint(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06011E43 RID: 73283 RVA: 0x00293EE9 File Offset: 0x002920E9
		public NumericPoint(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06011E44 RID: 73284 RVA: 0x002F35B8 File Offset: 0x002F17B8
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (11 == namespaceId && "v" == name)
			{
				return new NumericValue();
			}
			return null;
		}

		// Token: 0x170055F6 RID: 22006
		// (get) Token: 0x06011E45 RID: 73285 RVA: 0x002F35D3 File Offset: 0x002F17D3
		internal override string[] ElementTagNames
		{
			get
			{
				return NumericPoint.eleTagNames;
			}
		}

		// Token: 0x170055F7 RID: 22007
		// (get) Token: 0x06011E46 RID: 73286 RVA: 0x002F35DA File Offset: 0x002F17DA
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return NumericPoint.eleNamespaceIds;
			}
		}

		// Token: 0x170055F8 RID: 22008
		// (get) Token: 0x06011E47 RID: 73287 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x170055F9 RID: 22009
		// (get) Token: 0x06011E48 RID: 73288 RVA: 0x002F35E1 File Offset: 0x002F17E1
		// (set) Token: 0x06011E49 RID: 73289 RVA: 0x002F35EA File Offset: 0x002F17EA
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

		// Token: 0x06011E4A RID: 73290 RVA: 0x002F35F4 File Offset: 0x002F17F4
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "idx" == name)
			{
				return new UInt32Value();
			}
			if (namespaceId == 0 && "formatCode" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06011E4B RID: 73291 RVA: 0x002F362A File Offset: 0x002F182A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<NumericPoint>(deep);
		}

		// Token: 0x06011E4C RID: 73292 RVA: 0x002F3634 File Offset: 0x002F1834
		// Note: this type is marked as 'beforefieldinit'.
		static NumericPoint()
		{
			byte[] array = new byte[2];
			NumericPoint.attributeNamespaceIds = array;
			NumericPoint.eleTagNames = new string[] { "v" };
			NumericPoint.eleNamespaceIds = new byte[] { 11 };
		}

		// Token: 0x04007D1B RID: 32027
		private const string tagName = "pt";

		// Token: 0x04007D1C RID: 32028
		private const byte tagNsId = 11;

		// Token: 0x04007D1D RID: 32029
		internal const int ElementTypeIdConst = 10393;

		// Token: 0x04007D1E RID: 32030
		private static string[] attributeTagNames = new string[] { "idx", "formatCode" };

		// Token: 0x04007D1F RID: 32031
		private static byte[] attributeNamespaceIds;

		// Token: 0x04007D20 RID: 32032
		private static readonly string[] eleTagNames;

		// Token: 0x04007D21 RID: 32033
		private static readonly byte[] eleNamespaceIds;
	}
}
