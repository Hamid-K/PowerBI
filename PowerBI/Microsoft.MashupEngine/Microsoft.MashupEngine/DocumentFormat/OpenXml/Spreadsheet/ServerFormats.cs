using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002B6B RID: 11115
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(ServerFormat))]
	internal class ServerFormats : OpenXmlCompositeElement
	{
		// Token: 0x17007929 RID: 31017
		// (get) Token: 0x06016E07 RID: 93703 RVA: 0x003300B3 File Offset: 0x0032E2B3
		public override string LocalName
		{
			get
			{
				return "serverFormats";
			}
		}

		// Token: 0x1700792A RID: 31018
		// (get) Token: 0x06016E08 RID: 93704 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x1700792B RID: 31019
		// (get) Token: 0x06016E09 RID: 93705 RVA: 0x003300BA File Offset: 0x0032E2BA
		internal override int ElementTypeId
		{
			get
			{
				return 11094;
			}
		}

		// Token: 0x06016E0A RID: 93706 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700792C RID: 31020
		// (get) Token: 0x06016E0B RID: 93707 RVA: 0x003300C1 File Offset: 0x0032E2C1
		internal override string[] AttributeTagNames
		{
			get
			{
				return ServerFormats.attributeTagNames;
			}
		}

		// Token: 0x1700792D RID: 31021
		// (get) Token: 0x06016E0C RID: 93708 RVA: 0x003300C8 File Offset: 0x0032E2C8
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ServerFormats.attributeNamespaceIds;
			}
		}

		// Token: 0x1700792E RID: 31022
		// (get) Token: 0x06016E0D RID: 93709 RVA: 0x002DE933 File Offset: 0x002DCB33
		// (set) Token: 0x06016E0E RID: 93710 RVA: 0x0029402B File Offset: 0x0029222B
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

		// Token: 0x06016E0F RID: 93711 RVA: 0x00293ECF File Offset: 0x002920CF
		public ServerFormats()
		{
		}

		// Token: 0x06016E10 RID: 93712 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ServerFormats(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016E11 RID: 93713 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ServerFormats(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06016E12 RID: 93714 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ServerFormats(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06016E13 RID: 93715 RVA: 0x003300CF File Offset: 0x0032E2CF
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "serverFormat" == name)
			{
				return new ServerFormat();
			}
			return null;
		}

		// Token: 0x06016E14 RID: 93716 RVA: 0x002E67DA File Offset: 0x002E49DA
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "count" == name)
			{
				return new UInt32Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06016E15 RID: 93717 RVA: 0x003300EA File Offset: 0x0032E2EA
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ServerFormats>(deep);
		}

		// Token: 0x06016E16 RID: 93718 RVA: 0x003300F4 File Offset: 0x0032E2F4
		// Note: this type is marked as 'beforefieldinit'.
		static ServerFormats()
		{
			byte[] array = new byte[1];
			ServerFormats.attributeNamespaceIds = array;
		}

		// Token: 0x04009A4B RID: 39499
		private const string tagName = "serverFormats";

		// Token: 0x04009A4C RID: 39500
		private const byte tagNsId = 22;

		// Token: 0x04009A4D RID: 39501
		internal const int ElementTypeIdConst = 11094;

		// Token: 0x04009A4E RID: 39502
		private static string[] attributeTagNames = new string[] { "count" };

		// Token: 0x04009A4F RID: 39503
		private static byte[] attributeNamespaceIds;
	}
}
