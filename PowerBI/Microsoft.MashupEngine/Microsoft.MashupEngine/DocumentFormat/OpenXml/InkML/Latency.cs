using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.InkML
{
	// Token: 0x02003090 RID: 12432
	[GeneratedCode("DomGen", "2.0")]
	internal class Latency : OpenXmlLeafElement
	{
		// Token: 0x1700979D RID: 38813
		// (get) Token: 0x0601B06B RID: 110699 RVA: 0x0036AD8B File Offset: 0x00368F8B
		public override string LocalName
		{
			get
			{
				return "latency";
			}
		}

		// Token: 0x1700979E RID: 38814
		// (get) Token: 0x0601B06C RID: 110700 RVA: 0x0036A4B3 File Offset: 0x003686B3
		internal override byte NamespaceId
		{
			get
			{
				return 43;
			}
		}

		// Token: 0x1700979F RID: 38815
		// (get) Token: 0x0601B06D RID: 110701 RVA: 0x0036AD92 File Offset: 0x00368F92
		internal override int ElementTypeId
		{
			get
			{
				return 12653;
			}
		}

		// Token: 0x0601B06E RID: 110702 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170097A0 RID: 38816
		// (get) Token: 0x0601B06F RID: 110703 RVA: 0x0036AD99 File Offset: 0x00368F99
		internal override string[] AttributeTagNames
		{
			get
			{
				return Latency.attributeTagNames;
			}
		}

		// Token: 0x170097A1 RID: 38817
		// (get) Token: 0x0601B070 RID: 110704 RVA: 0x0036ADA0 File Offset: 0x00368FA0
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Latency.attributeNamespaceIds;
			}
		}

		// Token: 0x170097A2 RID: 38818
		// (get) Token: 0x0601B071 RID: 110705 RVA: 0x0036ADA7 File Offset: 0x00368FA7
		// (set) Token: 0x0601B072 RID: 110706 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "value")]
		public DecimalValue Value
		{
			get
			{
				return (DecimalValue)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0601B074 RID: 110708 RVA: 0x0036ADB6 File Offset: 0x00368FB6
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "value" == name)
			{
				return new DecimalValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601B075 RID: 110709 RVA: 0x0036ADD6 File Offset: 0x00368FD6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Latency>(deep);
		}

		// Token: 0x0601B076 RID: 110710 RVA: 0x0036ADE0 File Offset: 0x00368FE0
		// Note: this type is marked as 'beforefieldinit'.
		static Latency()
		{
			byte[] array = new byte[1];
			Latency.attributeNamespaceIds = array;
		}

		// Token: 0x0400B29D RID: 45725
		private const string tagName = "latency";

		// Token: 0x0400B29E RID: 45726
		private const byte tagNsId = 43;

		// Token: 0x0400B29F RID: 45727
		internal const int ElementTypeIdConst = 12653;

		// Token: 0x0400B2A0 RID: 45728
		private static string[] attributeTagNames = new string[] { "value" };

		// Token: 0x0400B2A1 RID: 45729
		private static byte[] attributeNamespaceIds;
	}
}
