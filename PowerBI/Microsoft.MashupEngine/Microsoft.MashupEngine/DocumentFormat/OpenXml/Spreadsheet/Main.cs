using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C31 RID: 11313
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(Topic))]
	internal class Main : OpenXmlCompositeElement
	{
		// Token: 0x170080F2 RID: 33010
		// (get) Token: 0x06017EB9 RID: 97977 RVA: 0x0033C95B File Offset: 0x0033AB5B
		public override string LocalName
		{
			get
			{
				return "main";
			}
		}

		// Token: 0x170080F3 RID: 33011
		// (get) Token: 0x06017EBA RID: 97978 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170080F4 RID: 33012
		// (get) Token: 0x06017EBB RID: 97979 RVA: 0x0033C962 File Offset: 0x0033AB62
		internal override int ElementTypeId
		{
			get
			{
				return 11294;
			}
		}

		// Token: 0x06017EBC RID: 97980 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170080F5 RID: 33013
		// (get) Token: 0x06017EBD RID: 97981 RVA: 0x0033C969 File Offset: 0x0033AB69
		internal override string[] AttributeTagNames
		{
			get
			{
				return Main.attributeTagNames;
			}
		}

		// Token: 0x170080F6 RID: 33014
		// (get) Token: 0x06017EBE RID: 97982 RVA: 0x0033C970 File Offset: 0x0033AB70
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Main.attributeNamespaceIds;
			}
		}

		// Token: 0x170080F7 RID: 33015
		// (get) Token: 0x06017EBF RID: 97983 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06017EC0 RID: 97984 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "first")]
		public StringValue First
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

		// Token: 0x06017EC1 RID: 97985 RVA: 0x00293ECF File Offset: 0x002920CF
		public Main()
		{
		}

		// Token: 0x06017EC2 RID: 97986 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Main(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017EC3 RID: 97987 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Main(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017EC4 RID: 97988 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Main(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017EC5 RID: 97989 RVA: 0x0033C977 File Offset: 0x0033AB77
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "tp" == name)
			{
				return new Topic();
			}
			return null;
		}

		// Token: 0x06017EC6 RID: 97990 RVA: 0x0033C992 File Offset: 0x0033AB92
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "first" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017EC7 RID: 97991 RVA: 0x0033C9B2 File Offset: 0x0033ABB2
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Main>(deep);
		}

		// Token: 0x06017EC8 RID: 97992 RVA: 0x0033C9BC File Offset: 0x0033ABBC
		// Note: this type is marked as 'beforefieldinit'.
		static Main()
		{
			byte[] array = new byte[1];
			Main.attributeNamespaceIds = array;
		}

		// Token: 0x04009E25 RID: 40485
		private const string tagName = "main";

		// Token: 0x04009E26 RID: 40486
		private const byte tagNsId = 22;

		// Token: 0x04009E27 RID: 40487
		internal const int ElementTypeIdConst = 11294;

		// Token: 0x04009E28 RID: 40488
		private static string[] attributeTagNames = new string[] { "first" };

		// Token: 0x04009E29 RID: 40489
		private static byte[] attributeNamespaceIds;
	}
}
