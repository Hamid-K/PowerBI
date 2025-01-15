using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C30 RID: 11312
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Main))]
	internal class VolatileType : OpenXmlCompositeElement
	{
		// Token: 0x170080EC RID: 33004
		// (get) Token: 0x06017EA9 RID: 97961 RVA: 0x0033C8BA File Offset: 0x0033AABA
		public override string LocalName
		{
			get
			{
				return "volType";
			}
		}

		// Token: 0x170080ED RID: 33005
		// (get) Token: 0x06017EAA RID: 97962 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170080EE RID: 33006
		// (get) Token: 0x06017EAB RID: 97963 RVA: 0x0033C8C1 File Offset: 0x0033AAC1
		internal override int ElementTypeId
		{
			get
			{
				return 11293;
			}
		}

		// Token: 0x06017EAC RID: 97964 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170080EF RID: 33007
		// (get) Token: 0x06017EAD RID: 97965 RVA: 0x0033C8C8 File Offset: 0x0033AAC8
		internal override string[] AttributeTagNames
		{
			get
			{
				return VolatileType.attributeTagNames;
			}
		}

		// Token: 0x170080F0 RID: 33008
		// (get) Token: 0x06017EAE RID: 97966 RVA: 0x0033C8CF File Offset: 0x0033AACF
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return VolatileType.attributeNamespaceIds;
			}
		}

		// Token: 0x170080F1 RID: 33009
		// (get) Token: 0x06017EAF RID: 97967 RVA: 0x0033C8D6 File Offset: 0x0033AAD6
		// (set) Token: 0x06017EB0 RID: 97968 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<VolatileDependencyValues> Type
		{
			get
			{
				return (EnumValue<VolatileDependencyValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06017EB1 RID: 97969 RVA: 0x00293ECF File Offset: 0x002920CF
		public VolatileType()
		{
		}

		// Token: 0x06017EB2 RID: 97970 RVA: 0x00293ED7 File Offset: 0x002920D7
		public VolatileType(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017EB3 RID: 97971 RVA: 0x00293EE0 File Offset: 0x002920E0
		public VolatileType(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017EB4 RID: 97972 RVA: 0x00293EE9 File Offset: 0x002920E9
		public VolatileType(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017EB5 RID: 97973 RVA: 0x0033C8E5 File Offset: 0x0033AAE5
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "main" == name)
			{
				return new Main();
			}
			return null;
		}

		// Token: 0x06017EB6 RID: 97974 RVA: 0x0033C900 File Offset: 0x0033AB00
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<VolatileDependencyValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017EB7 RID: 97975 RVA: 0x0033C920 File Offset: 0x0033AB20
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<VolatileType>(deep);
		}

		// Token: 0x06017EB8 RID: 97976 RVA: 0x0033C92C File Offset: 0x0033AB2C
		// Note: this type is marked as 'beforefieldinit'.
		static VolatileType()
		{
			byte[] array = new byte[1];
			VolatileType.attributeNamespaceIds = array;
		}

		// Token: 0x04009E20 RID: 40480
		private const string tagName = "volType";

		// Token: 0x04009E21 RID: 40481
		private const byte tagNsId = 22;

		// Token: 0x04009E22 RID: 40482
		internal const int ElementTypeIdConst = 11293;

		// Token: 0x04009E23 RID: 40483
		private static string[] attributeTagNames = new string[] { "type" };

		// Token: 0x04009E24 RID: 40484
		private static byte[] attributeNamespaceIds;
	}
}
