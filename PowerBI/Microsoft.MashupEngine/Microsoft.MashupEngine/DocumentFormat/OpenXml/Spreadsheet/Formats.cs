using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002CB5 RID: 11445
	[ChildElementInfo(typeof(Format))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Formats : OpenXmlCompositeElement
	{
		// Token: 0x170084A6 RID: 33958
		// (get) Token: 0x0601878B RID: 100235 RVA: 0x00341BFB File Offset: 0x0033FDFB
		public override string LocalName
		{
			get
			{
				return "formats";
			}
		}

		// Token: 0x170084A7 RID: 33959
		// (get) Token: 0x0601878C RID: 100236 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x170084A8 RID: 33960
		// (get) Token: 0x0601878D RID: 100237 RVA: 0x00341C02 File Offset: 0x0033FE02
		internal override int ElementTypeId
		{
			get
			{
				return 11425;
			}
		}

		// Token: 0x0601878E RID: 100238 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170084A9 RID: 33961
		// (get) Token: 0x0601878F RID: 100239 RVA: 0x00341C09 File Offset: 0x0033FE09
		internal override string[] AttributeTagNames
		{
			get
			{
				return Formats.attributeTagNames;
			}
		}

		// Token: 0x170084AA RID: 33962
		// (get) Token: 0x06018790 RID: 100240 RVA: 0x00341C10 File Offset: 0x0033FE10
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Formats.attributeNamespaceIds;
			}
		}

		// Token: 0x170084AB RID: 33963
		// (get) Token: 0x06018791 RID: 100241 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06018792 RID: 100242 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "count")]
		public UInt32Value Count
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

		// Token: 0x06018793 RID: 100243 RVA: 0x00293ECF File Offset: 0x002920CF
		public Formats()
		{
		}

		// Token: 0x06018794 RID: 100244 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Formats(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018795 RID: 100245 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Formats(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06018796 RID: 100246 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Formats(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06018797 RID: 100247 RVA: 0x00341C17 File Offset: 0x0033FE17
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "format" == name)
			{
				return new Format();
			}
			return null;
		}

		// Token: 0x06018798 RID: 100248 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06018799 RID: 100249 RVA: 0x00341C32 File Offset: 0x0033FE32
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Formats>(deep);
		}

		// Token: 0x0601879A RID: 100250 RVA: 0x00341C3C File Offset: 0x0033FE3C
		// Note: this type is marked as 'beforefieldinit'.
		static Formats()
		{
			byte[] array = new byte[1];
			Formats.attributeNamespaceIds = array;
		}

		// Token: 0x0400A06E RID: 41070
		private const string tagName = "formats";

		// Token: 0x0400A06F RID: 41071
		private const byte tagNsId = 22;

		// Token: 0x0400A070 RID: 41072
		internal const int ElementTypeIdConst = 11425;

		// Token: 0x0400A071 RID: 41073
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x0400A072 RID: 41074
		private static byte[] attributeNamespaceIds;
	}
}
