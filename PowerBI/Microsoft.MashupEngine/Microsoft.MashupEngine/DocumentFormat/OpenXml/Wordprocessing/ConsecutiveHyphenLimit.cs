using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FE7 RID: 12263
	[GeneratedCode("DomGen", "2.0")]
	internal class ConsecutiveHyphenLimit : OpenXmlLeafElement
	{
		// Token: 0x1700950F RID: 38159
		// (get) Token: 0x0601AABC RID: 109244 RVA: 0x00365C62 File Offset: 0x00363E62
		public override string LocalName
		{
			get
			{
				return "consecutiveHyphenLimit";
			}
		}

		// Token: 0x17009510 RID: 38160
		// (get) Token: 0x0601AABD RID: 109245 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17009511 RID: 38161
		// (get) Token: 0x0601AABE RID: 109246 RVA: 0x00365C69 File Offset: 0x00363E69
		internal override int ElementTypeId
		{
			get
			{
				return 11998;
			}
		}

		// Token: 0x0601AABF RID: 109247 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17009512 RID: 38162
		// (get) Token: 0x0601AAC0 RID: 109248 RVA: 0x00365C70 File Offset: 0x00363E70
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConsecutiveHyphenLimit.attributeTagNames;
			}
		}

		// Token: 0x17009513 RID: 38163
		// (get) Token: 0x0601AAC1 RID: 109249 RVA: 0x00365C77 File Offset: 0x00363E77
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConsecutiveHyphenLimit.attributeNamespaceIds;
			}
		}

		// Token: 0x17009514 RID: 38164
		// (get) Token: 0x0601AAC2 RID: 109250 RVA: 0x002F0704 File Offset: 0x002EE904
		// (set) Token: 0x0601AAC3 RID: 109251 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "val")]
		public UInt16Value Val
		{
			get
			{
				return (UInt16Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601AAC5 RID: 109253 RVA: 0x003544D8 File Offset: 0x003526D8
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "val" == name)
			{
				return new UInt16Value();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601AAC6 RID: 109254 RVA: 0x00365C7E File Offset: 0x00363E7E
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConsecutiveHyphenLimit>(deep);
		}

		// Token: 0x0400ADEB RID: 44523
		private const string tagName = "consecutiveHyphenLimit";

		// Token: 0x0400ADEC RID: 44524
		private const byte tagNsId = 23;

		// Token: 0x0400ADED RID: 44525
		internal const int ElementTypeIdConst = 11998;

		// Token: 0x0400ADEE RID: 44526
		private static string[] attributeTagNames = new string[] { "val" };

		// Token: 0x0400ADEF RID: 44527
		private static byte[] attributeNamespaceIds = new byte[] { 23 };
	}
}
