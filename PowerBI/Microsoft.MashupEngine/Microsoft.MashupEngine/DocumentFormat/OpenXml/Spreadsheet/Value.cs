using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Spreadsheet
{
	// Token: 0x02002C24 RID: 11300
	[ChildElementInfo(typeof(DdeLinkValue))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Value : OpenXmlCompositeElement
	{
		// Token: 0x17008085 RID: 32901
		// (get) Token: 0x06017DC8 RID: 97736 RVA: 0x000DE4CB File Offset: 0x000DC6CB
		public override string LocalName
		{
			get
			{
				return "value";
			}
		}

		// Token: 0x17008086 RID: 32902
		// (get) Token: 0x06017DC9 RID: 97737 RVA: 0x00243BDB File Offset: 0x00241DDB
		internal override byte NamespaceId
		{
			get
			{
				return 22;
			}
		}

		// Token: 0x17008087 RID: 32903
		// (get) Token: 0x06017DCA RID: 97738 RVA: 0x0033BEC7 File Offset: 0x0033A0C7
		internal override int ElementTypeId
		{
			get
			{
				return 11281;
			}
		}

		// Token: 0x06017DCB RID: 97739 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008088 RID: 32904
		// (get) Token: 0x06017DCC RID: 97740 RVA: 0x0033BECE File Offset: 0x0033A0CE
		internal override string[] AttributeTagNames
		{
			get
			{
				return Value.attributeTagNames;
			}
		}

		// Token: 0x17008089 RID: 32905
		// (get) Token: 0x06017DCD RID: 97741 RVA: 0x0033BED5 File Offset: 0x0033A0D5
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Value.attributeNamespaceIds;
			}
		}

		// Token: 0x1700808A RID: 32906
		// (get) Token: 0x06017DCE RID: 97742 RVA: 0x0033BEDC File Offset: 0x0033A0DC
		// (set) Token: 0x06017DCF RID: 97743 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "t")]
		public EnumValue<DdeValues> ValueType
		{
			get
			{
				return (EnumValue<DdeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x06017DD0 RID: 97744 RVA: 0x00293ECF File Offset: 0x002920CF
		public Value()
		{
		}

		// Token: 0x06017DD1 RID: 97745 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Value(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017DD2 RID: 97746 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Value(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06017DD3 RID: 97747 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Value(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06017DD4 RID: 97748 RVA: 0x0033BEEB File Offset: 0x0033A0EB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (22 == namespaceId && "val" == name)
			{
				return new DdeLinkValue();
			}
			return null;
		}

		// Token: 0x1700808B RID: 32907
		// (get) Token: 0x06017DD5 RID: 97749 RVA: 0x0033BF06 File Offset: 0x0033A106
		internal override string[] ElementTagNames
		{
			get
			{
				return Value.eleTagNames;
			}
		}

		// Token: 0x1700808C RID: 32908
		// (get) Token: 0x06017DD6 RID: 97750 RVA: 0x0033BF0D File Offset: 0x0033A10D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Value.eleNamespaceIds;
			}
		}

		// Token: 0x1700808D RID: 32909
		// (get) Token: 0x06017DD7 RID: 97751 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x1700808E RID: 32910
		// (get) Token: 0x06017DD8 RID: 97752 RVA: 0x0033BF14 File Offset: 0x0033A114
		// (set) Token: 0x06017DD9 RID: 97753 RVA: 0x0033BF1D File Offset: 0x0033A11D
		public DdeLinkValue DdeLinkValue
		{
			get
			{
				return base.GetElement<DdeLinkValue>(0);
			}
			set
			{
				base.SetElement<DdeLinkValue>(0, value);
			}
		}

		// Token: 0x06017DDA RID: 97754 RVA: 0x0033BF27 File Offset: 0x0033A127
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "t" == name)
			{
				return new EnumValue<DdeValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06017DDB RID: 97755 RVA: 0x0033BF47 File Offset: 0x0033A147
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Value>(deep);
		}

		// Token: 0x06017DDC RID: 97756 RVA: 0x0033BF50 File Offset: 0x0033A150
		// Note: this type is marked as 'beforefieldinit'.
		static Value()
		{
			byte[] array = new byte[1];
			Value.attributeNamespaceIds = array;
			Value.eleTagNames = new string[] { "val" };
			Value.eleNamespaceIds = new byte[] { 22 };
		}

		// Token: 0x04009DE3 RID: 40419
		private const string tagName = "value";

		// Token: 0x04009DE4 RID: 40420
		private const byte tagNsId = 22;

		// Token: 0x04009DE5 RID: 40421
		internal const int ElementTypeIdConst = 11281;

		// Token: 0x04009DE6 RID: 40422
		private static string[] attributeTagNames = new string[] { "t" };

		// Token: 0x04009DE7 RID: 40423
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009DE8 RID: 40424
		private static readonly string[] eleTagNames;

		// Token: 0x04009DE9 RID: 40425
		private static readonly byte[] eleNamespaceIds;
	}
}
