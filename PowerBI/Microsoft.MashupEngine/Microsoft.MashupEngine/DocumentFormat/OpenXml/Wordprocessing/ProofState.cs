using System;
using System.CodeDom.Compiler;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002FDD RID: 12253
	[GeneratedCode("DomGen", "2.0")]
	internal class ProofState : OpenXmlLeafElement
	{
		// Token: 0x170094A6 RID: 38054
		// (get) Token: 0x0601A9E7 RID: 109031 RVA: 0x00364FB8 File Offset: 0x003631B8
		public override string LocalName
		{
			get
			{
				return "proofState";
			}
		}

		// Token: 0x170094A7 RID: 38055
		// (get) Token: 0x0601A9E8 RID: 109032 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x170094A8 RID: 38056
		// (get) Token: 0x0601A9E9 RID: 109033 RVA: 0x00364FBF File Offset: 0x003631BF
		internal override int ElementTypeId
		{
			get
			{
				return 11980;
			}
		}

		// Token: 0x0601A9EA RID: 109034 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x170094A9 RID: 38057
		// (get) Token: 0x0601A9EB RID: 109035 RVA: 0x00364FC6 File Offset: 0x003631C6
		internal override string[] AttributeTagNames
		{
			get
			{
				return ProofState.attributeTagNames;
			}
		}

		// Token: 0x170094AA RID: 38058
		// (get) Token: 0x0601A9EC RID: 109036 RVA: 0x00364FCD File Offset: 0x003631CD
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ProofState.attributeNamespaceIds;
			}
		}

		// Token: 0x170094AB RID: 38059
		// (get) Token: 0x0601A9ED RID: 109037 RVA: 0x00364FD4 File Offset: 0x003631D4
		// (set) Token: 0x0601A9EE RID: 109038 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "spelling")]
		public EnumValue<ProofingStateValues> Spelling
		{
			get
			{
				return (EnumValue<ProofingStateValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x170094AC RID: 38060
		// (get) Token: 0x0601A9EF RID: 109039 RVA: 0x00364FE3 File Offset: 0x003631E3
		// (set) Token: 0x0601A9F0 RID: 109040 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "grammar")]
		public EnumValue<ProofingStateValues> Grammar
		{
			get
			{
				return (EnumValue<ProofingStateValues>)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x0601A9F2 RID: 109042 RVA: 0x00364FF2 File Offset: 0x003631F2
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "spelling" == name)
			{
				return new EnumValue<ProofingStateValues>();
			}
			if (23 == namespaceId && "grammar" == name)
			{
				return new EnumValue<ProofingStateValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0601A9F3 RID: 109043 RVA: 0x0036502C File Offset: 0x0036322C
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ProofState>(deep);
		}

		// Token: 0x0400ADC0 RID: 44480
		private const string tagName = "proofState";

		// Token: 0x0400ADC1 RID: 44481
		private const byte tagNsId = 23;

		// Token: 0x0400ADC2 RID: 44482
		internal const int ElementTypeIdConst = 11980;

		// Token: 0x0400ADC3 RID: 44483
		private static string[] attributeTagNames = new string[] { "spelling", "grammar" };

		// Token: 0x0400ADC4 RID: 44484
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23 };
	}
}
