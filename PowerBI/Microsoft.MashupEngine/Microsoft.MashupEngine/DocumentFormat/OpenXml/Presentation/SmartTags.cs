using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002AB4 RID: 10932
	[GeneratedCode("DomGen", "2.0")]
	internal class SmartTags : OpenXmlLeafElement
	{
		// Token: 0x170074CE RID: 29902
		// (get) Token: 0x06016412 RID: 91154 RVA: 0x002AC71B File Offset: 0x002AA91B
		public override string LocalName
		{
			get
			{
				return "smartTags";
			}
		}

		// Token: 0x170074CF RID: 29903
		// (get) Token: 0x06016413 RID: 91155 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x170074D0 RID: 29904
		// (get) Token: 0x06016414 RID: 91156 RVA: 0x003283D7 File Offset: 0x003265D7
		internal override int ElementTypeId
		{
			get
			{
				return 12347;
			}
		}

		// Token: 0x06016415 RID: 91157 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170074D1 RID: 29905
		// (get) Token: 0x06016416 RID: 91158 RVA: 0x003283DE File Offset: 0x003265DE
		internal override string[] AttributeTagNames
		{
			get
			{
				return SmartTags.attributeTagNames;
			}
		}

		// Token: 0x170074D2 RID: 29906
		// (get) Token: 0x06016417 RID: 91159 RVA: 0x003283E5 File Offset: 0x003265E5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return SmartTags.attributeNamespaceIds;
			}
		}

		// Token: 0x170074D3 RID: 29907
		// (get) Token: 0x06016418 RID: 91160 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x06016419 RID: 91161 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(19, "id")]
		public StringValue Id
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

		// Token: 0x0601641B RID: 91163 RVA: 0x002D0AD5 File Offset: 0x002CECD5
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (19 == namespaceId && "id" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601641C RID: 91164 RVA: 0x003283EC File Offset: 0x003265EC
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<SmartTags>(deep);
		}

		// Token: 0x040096E4 RID: 38628
		private const string tagName = "smartTags";

		// Token: 0x040096E5 RID: 38629
		private const byte tagNsId = 24;

		// Token: 0x040096E6 RID: 38630
		internal const int ElementTypeIdConst = 12347;

		// Token: 0x040096E7 RID: 38631
		private static string[] attributeTagNames = new string[] { "id" };

		// Token: 0x040096E8 RID: 38632
		private static byte[] attributeNamespaceIds = new byte[] { 19 };
	}
}
