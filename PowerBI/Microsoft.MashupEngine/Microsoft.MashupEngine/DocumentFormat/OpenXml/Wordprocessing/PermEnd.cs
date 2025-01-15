using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002EC1 RID: 11969
	[GeneratedCode("DomGen", "2.0")]
	internal class PermEnd : OpenXmlLeafElement
	{
		// Token: 0x17008C69 RID: 35945
		// (get) Token: 0x060197EB RID: 104427 RVA: 0x0034C6B4 File Offset: 0x0034A8B4
		public override string LocalName
		{
			get
			{
				return "permEnd";
			}
		}

		// Token: 0x17008C6A RID: 35946
		// (get) Token: 0x060197EC RID: 104428 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008C6B RID: 35947
		// (get) Token: 0x060197ED RID: 104429 RVA: 0x0034C6BB File Offset: 0x0034A8BB
		internal override int ElementTypeId
		{
			get
			{
				return 11625;
			}
		}

		// Token: 0x060197EE RID: 104430 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008C6C RID: 35948
		// (get) Token: 0x060197EF RID: 104431 RVA: 0x0034C6C2 File Offset: 0x0034A8C2
		internal override string[] AttributeTagNames
		{
			get
			{
				return PermEnd.attributeTagNames;
			}
		}

		// Token: 0x17008C6D RID: 35949
		// (get) Token: 0x060197F0 RID: 104432 RVA: 0x0034C6C9 File Offset: 0x0034A8C9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return PermEnd.attributeNamespaceIds;
			}
		}

		// Token: 0x17008C6E RID: 35950
		// (get) Token: 0x060197F1 RID: 104433 RVA: 0x002BF6A0 File Offset: 0x002BD8A0
		// (set) Token: 0x060197F2 RID: 104434 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "id")]
		public Int32Value Id
		{
			get
			{
				return (Int32Value)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008C6F RID: 35951
		// (get) Token: 0x060197F3 RID: 104435 RVA: 0x0034C6D0 File Offset: 0x0034A8D0
		// (set) Token: 0x060197F4 RID: 104436 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "displacedByCustomXml")]
		public EnumValue<DisplacedByCustomXmlValues> DisplacedByCustomXml
		{
			get
			{
				return (EnumValue<DisplacedByCustomXmlValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x060197F6 RID: 104438 RVA: 0x0034C6DF File Offset: 0x0034A8DF
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "id" == name)
			{
				return new Int32Value();
			}
			if (23 == namespaceId && "displacedByCustomXml" == name)
			{
				return new EnumValue<DisplacedByCustomXmlValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060197F7 RID: 104439 RVA: 0x0034C719 File Offset: 0x0034A919
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<PermEnd>(deep);
		}

		// Token: 0x0400A917 RID: 43287
		private const string tagName = "permEnd";

		// Token: 0x0400A918 RID: 43288
		private const byte tagNsId = 23;

		// Token: 0x0400A919 RID: 43289
		internal const int ElementTypeIdConst = 11625;

		// Token: 0x0400A91A RID: 43290
		private static string[] attributeTagNames = new string[] { "id", "displacedByCustomXml" };

		// Token: 0x0400A91B RID: 43291
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
