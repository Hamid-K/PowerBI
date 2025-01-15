using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Office.Word
{
	// Token: 0x02002468 RID: 9320
	[GeneratedCode("DomGen", "2.0")]
	internal class AllocatedCommand : OpenXmlLeafElement
	{
		// Token: 0x170050DE RID: 20702
		// (get) Token: 0x060112E4 RID: 70372 RVA: 0x002EB364 File Offset: 0x002E9564
		public override string LocalName
		{
			get
			{
				return "acd";
			}
		}

		// Token: 0x170050DF RID: 20703
		// (get) Token: 0x060112E5 RID: 70373 RVA: 0x002EAFCE File Offset: 0x002E91CE
		internal override byte NamespaceId
		{
			get
			{
				return 33;
			}
		}

		// Token: 0x170050E0 RID: 20704
		// (get) Token: 0x060112E6 RID: 70374 RVA: 0x002EB683 File Offset: 0x002E9883
		internal override int ElementTypeId
		{
			get
			{
				return 12547;
			}
		}

		// Token: 0x060112E7 RID: 70375 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170050E1 RID: 20705
		// (get) Token: 0x060112E8 RID: 70376 RVA: 0x002EB68A File Offset: 0x002E988A
		internal override string[] AttributeTagNames
		{
			get
			{
				return AllocatedCommand.attributeTagNames;
			}
		}

		// Token: 0x170050E2 RID: 20706
		// (get) Token: 0x060112E9 RID: 70377 RVA: 0x002EB691 File Offset: 0x002E9891
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return AllocatedCommand.attributeNamespaceIds;
			}
		}

		// Token: 0x170050E3 RID: 20707
		// (get) Token: 0x060112EA RID: 70378 RVA: 0x0029401C File Offset: 0x0029221C
		// (set) Token: 0x060112EB RID: 70379 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(33, "argValue")]
		public StringValue ArgumentValue
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

		// Token: 0x170050E4 RID: 20708
		// (get) Token: 0x060112EC RID: 70380 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060112ED RID: 70381 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(33, "fciBasedOn")]
		public StringValue CommandBasedOn
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

		// Token: 0x170050E5 RID: 20709
		// (get) Token: 0x060112EE RID: 70382 RVA: 0x002E82CD File Offset: 0x002E64CD
		// (set) Token: 0x060112EF RID: 70383 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(33, "fciIndexBasedOn")]
		public HexBinaryValue CommandIndexBasedOn
		{
			get
			{
				return (HexBinaryValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x170050E6 RID: 20710
		// (get) Token: 0x060112F0 RID: 70384 RVA: 0x002BDADA File Offset: 0x002BBCDA
		// (set) Token: 0x060112F1 RID: 70385 RVA: 0x002BD4AE File Offset: 0x002BB6AE
		[SchemaAttr(33, "acdName")]
		public StringValue AcceleratorName
		{
			get
			{
				return (StringValue)base.Attributes[3];
			}
			set
			{
				base.Attributes[3] = value;
			}
		}

		// Token: 0x060112F3 RID: 70387 RVA: 0x002EB698 File Offset: 0x002E9898
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (33 == namespaceId && "argValue" == name)
			{
				return new StringValue();
			}
			if (33 == namespaceId && "fciBasedOn" == name)
			{
				return new StringValue();
			}
			if (33 == namespaceId && "fciIndexBasedOn" == name)
			{
				return new HexBinaryValue();
			}
			if (33 == namespaceId && "acdName" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060112F4 RID: 70388 RVA: 0x002EB70D File Offset: 0x002E990D
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<AllocatedCommand>(deep);
		}

		// Token: 0x04007888 RID: 30856
		private const string tagName = "acd";

		// Token: 0x04007889 RID: 30857
		private const byte tagNsId = 33;

		// Token: 0x0400788A RID: 30858
		internal const int ElementTypeIdConst = 12547;

		// Token: 0x0400788B RID: 30859
		private static string[] attributeTagNames = new string[] { "argValue", "fciBasedOn", "fciIndexBasedOn", "acdName" };

		// Token: 0x0400788C RID: 30860
		private static byte[] attributeNamespaceIds = new byte[] { 33, 33, 33, 33 };
	}
}
