using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Presentation
{
	// Token: 0x02002A1E RID: 10782
	[ChildElementInfo(typeof(CommonBehavior))]
	[GeneratedCode("DomGen", "2.0")]
	internal class Command : OpenXmlCompositeElement
	{
		// Token: 0x17007049 RID: 28745
		// (get) Token: 0x060159EA RID: 88554 RVA: 0x00321557 File Offset: 0x0031F757
		public override string LocalName
		{
			get
			{
				return "cmd";
			}
		}

		// Token: 0x1700704A RID: 28746
		// (get) Token: 0x060159EB RID: 88555 RVA: 0x000E78AA File Offset: 0x000E5AAA
		internal override byte NamespaceId
		{
			get
			{
				return 24;
			}
		}

		// Token: 0x1700704B RID: 28747
		// (get) Token: 0x060159EC RID: 88556 RVA: 0x0032155E File Offset: 0x0031F75E
		internal override int ElementTypeId
		{
			get
			{
				return 12208;
			}
		}

		// Token: 0x060159ED RID: 88557 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700704C RID: 28748
		// (get) Token: 0x060159EE RID: 88558 RVA: 0x00321565 File Offset: 0x0031F765
		internal override string[] AttributeTagNames
		{
			get
			{
				return Command.attributeTagNames;
			}
		}

		// Token: 0x1700704D RID: 28749
		// (get) Token: 0x060159EF RID: 88559 RVA: 0x0032156C File Offset: 0x0031F76C
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return Command.attributeNamespaceIds;
			}
		}

		// Token: 0x1700704E RID: 28750
		// (get) Token: 0x060159F0 RID: 88560 RVA: 0x00321573 File Offset: 0x0031F773
		// (set) Token: 0x060159F1 RID: 88561 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<CommandValues> Type
		{
			get
			{
				return (EnumValue<CommandValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x1700704F RID: 28751
		// (get) Token: 0x060159F2 RID: 88562 RVA: 0x002BE072 File Offset: 0x002BC272
		// (set) Token: 0x060159F3 RID: 88563 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "cmd")]
		public StringValue CommandName
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

		// Token: 0x060159F4 RID: 88564 RVA: 0x00293ECF File Offset: 0x002920CF
		public Command()
		{
		}

		// Token: 0x060159F5 RID: 88565 RVA: 0x00293ED7 File Offset: 0x002920D7
		public Command(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x060159F6 RID: 88566 RVA: 0x00293EE0 File Offset: 0x002920E0
		public Command(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x060159F7 RID: 88567 RVA: 0x00293EE9 File Offset: 0x002920E9
		public Command(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x060159F8 RID: 88568 RVA: 0x00321307 File Offset: 0x0031F507
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (24 == namespaceId && "cBhvr" == name)
			{
				return new CommonBehavior();
			}
			return null;
		}

		// Token: 0x17007050 RID: 28752
		// (get) Token: 0x060159F9 RID: 88569 RVA: 0x00321582 File Offset: 0x0031F782
		internal override string[] ElementTagNames
		{
			get
			{
				return Command.eleTagNames;
			}
		}

		// Token: 0x17007051 RID: 28753
		// (get) Token: 0x060159FA RID: 88570 RVA: 0x00321589 File Offset: 0x0031F789
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return Command.eleNamespaceIds;
			}
		}

		// Token: 0x17007052 RID: 28754
		// (get) Token: 0x060159FB RID: 88571 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17007053 RID: 28755
		// (get) Token: 0x060159FC RID: 88572 RVA: 0x00320C27 File Offset: 0x0031EE27
		// (set) Token: 0x060159FD RID: 88573 RVA: 0x00320C30 File Offset: 0x0031EE30
		public CommonBehavior CommonBehavior
		{
			get
			{
				return base.GetElement<CommonBehavior>(0);
			}
			set
			{
				base.SetElement<CommonBehavior>(0, value);
			}
		}

		// Token: 0x060159FE RID: 88574 RVA: 0x00321590 File Offset: 0x0031F790
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<CommandValues>();
			}
			if (namespaceId == 0 && "cmd" == name)
			{
				return new StringValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x060159FF RID: 88575 RVA: 0x003215C6 File Offset: 0x0031F7C6
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<Command>(deep);
		}

		// Token: 0x06015A00 RID: 88576 RVA: 0x003215D0 File Offset: 0x0031F7D0
		// Note: this type is marked as 'beforefieldinit'.
		static Command()
		{
			byte[] array = new byte[2];
			Command.attributeNamespaceIds = array;
			Command.eleTagNames = new string[] { "cBhvr" };
			Command.eleNamespaceIds = new byte[] { 24 };
		}

		// Token: 0x0400941D RID: 37917
		private const string tagName = "cmd";

		// Token: 0x0400941E RID: 37918
		private const byte tagNsId = 24;

		// Token: 0x0400941F RID: 37919
		internal const int ElementTypeIdConst = 12208;

		// Token: 0x04009420 RID: 37920
		private static string[] attributeTagNames = new string[] { "type", "cmd" };

		// Token: 0x04009421 RID: 37921
		private static byte[] attributeNamespaceIds;

		// Token: 0x04009422 RID: 37922
		private static readonly string[] eleTagNames;

		// Token: 0x04009423 RID: 37923
		private static readonly byte[] eleNamespaceIds;
	}
}
