using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using DocumentFormat.OpenXml.Office.Excel;

namespace DocumentFormat.OpenXml.Office2010.Excel
{
	// Token: 0x0200241E RID: 9246
	[ChildElementInfo(typeof(Formula))]
	[ChildElementInfo(typeof(ExtensionList), FileFormatVersions.Office2010)]
	[GeneratedCode("DomGen", "2.0")]
	[OfficeAvailability(FileFormatVersions.Office2010)]
	internal class ConditionalFormattingValueObject : OpenXmlCompositeElement
	{
		// Token: 0x17004F4D RID: 20301
		// (get) Token: 0x06010F4F RID: 69455 RVA: 0x002E9180 File Offset: 0x002E7380
		public override string LocalName
		{
			get
			{
				return "cfvo";
			}
		}

		// Token: 0x17004F4E RID: 20302
		// (get) Token: 0x06010F50 RID: 69456 RVA: 0x002E5AC2 File Offset: 0x002E3CC2
		internal override byte NamespaceId
		{
			get
			{
				return 53;
			}
		}

		// Token: 0x17004F4F RID: 20303
		// (get) Token: 0x06010F51 RID: 69457 RVA: 0x002E9187 File Offset: 0x002E7387
		internal override int ElementTypeId
		{
			get
			{
				return 12964;
			}
		}

		// Token: 0x06010F52 RID: 69458 RVA: 0x002D1774 File Offset: 0x002CF974
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return (FileFormatVersions.Office2010 & version) > FileFormatVersions.None;
		}

		// Token: 0x17004F50 RID: 20304
		// (get) Token: 0x06010F53 RID: 69459 RVA: 0x002E918E File Offset: 0x002E738E
		internal override string[] AttributeTagNames
		{
			get
			{
				return ConditionalFormattingValueObject.attributeTagNames;
			}
		}

		// Token: 0x17004F51 RID: 20305
		// (get) Token: 0x06010F54 RID: 69460 RVA: 0x002E9195 File Offset: 0x002E7395
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ConditionalFormattingValueObject.attributeNamespaceIds;
			}
		}

		// Token: 0x17004F52 RID: 20306
		// (get) Token: 0x06010F55 RID: 69461 RVA: 0x002E919C File Offset: 0x002E739C
		// (set) Token: 0x06010F56 RID: 69462 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "type")]
		public EnumValue<ConditionalFormattingValueObjectTypeValues> Type
		{
			get
			{
				return (EnumValue<ConditionalFormattingValueObjectTypeValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17004F53 RID: 20307
		// (get) Token: 0x06010F57 RID: 69463 RVA: 0x002C8B36 File Offset: 0x002C6D36
		// (set) Token: 0x06010F58 RID: 69464 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(0, "gte")]
		public BooleanValue GreaterThanOrEqual
		{
			get
			{
				return (BooleanValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x06010F59 RID: 69465 RVA: 0x00293ECF File Offset: 0x002920CF
		public ConditionalFormattingValueObject()
		{
		}

		// Token: 0x06010F5A RID: 69466 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ConditionalFormattingValueObject(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010F5B RID: 69467 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ConditionalFormattingValueObject(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06010F5C RID: 69468 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ConditionalFormattingValueObject(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06010F5D RID: 69469 RVA: 0x002E91AB File Offset: 0x002E73AB
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (32 == namespaceId && "f" == name)
			{
				return new Formula();
			}
			if (53 == namespaceId && "extLst" == name)
			{
				return new ExtensionList();
			}
			return null;
		}

		// Token: 0x17004F54 RID: 20308
		// (get) Token: 0x06010F5E RID: 69470 RVA: 0x002E91DE File Offset: 0x002E73DE
		internal override string[] ElementTagNames
		{
			get
			{
				return ConditionalFormattingValueObject.eleTagNames;
			}
		}

		// Token: 0x17004F55 RID: 20309
		// (get) Token: 0x06010F5F RID: 69471 RVA: 0x002E91E5 File Offset: 0x002E73E5
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return ConditionalFormattingValueObject.eleNamespaceIds;
			}
		}

		// Token: 0x17004F56 RID: 20310
		// (get) Token: 0x06010F60 RID: 69472 RVA: 0x00002139 File Offset: 0x00000339
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneSequence;
			}
		}

		// Token: 0x17004F57 RID: 20311
		// (get) Token: 0x06010F61 RID: 69473 RVA: 0x002E7D10 File Offset: 0x002E5F10
		// (set) Token: 0x06010F62 RID: 69474 RVA: 0x002E7D19 File Offset: 0x002E5F19
		public Formula Formula
		{
			get
			{
				return base.GetElement<Formula>(0);
			}
			set
			{
				base.SetElement<Formula>(0, value);
			}
		}

		// Token: 0x17004F58 RID: 20312
		// (get) Token: 0x06010F63 RID: 69475 RVA: 0x002E700B File Offset: 0x002E520B
		// (set) Token: 0x06010F64 RID: 69476 RVA: 0x002E7014 File Offset: 0x002E5214
		public ExtensionList ExtensionList
		{
			get
			{
				return base.GetElement<ExtensionList>(1);
			}
			set
			{
				base.SetElement<ExtensionList>(1, value);
			}
		}

		// Token: 0x06010F65 RID: 69477 RVA: 0x002E91EC File Offset: 0x002E73EC
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "type" == name)
			{
				return new EnumValue<ConditionalFormattingValueObjectTypeValues>();
			}
			if (namespaceId == 0 && "gte" == name)
			{
				return new BooleanValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06010F66 RID: 69478 RVA: 0x002E9222 File Offset: 0x002E7422
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ConditionalFormattingValueObject>(deep);
		}

		// Token: 0x06010F67 RID: 69479 RVA: 0x002E922C File Offset: 0x002E742C
		// Note: this type is marked as 'beforefieldinit'.
		static ConditionalFormattingValueObject()
		{
			byte[] array = new byte[2];
			ConditionalFormattingValueObject.attributeNamespaceIds = array;
			ConditionalFormattingValueObject.eleTagNames = new string[] { "f", "extLst" };
			ConditionalFormattingValueObject.eleNamespaceIds = new byte[] { 32, 53 };
		}

		// Token: 0x04007709 RID: 30473
		private const string tagName = "cfvo";

		// Token: 0x0400770A RID: 30474
		private const byte tagNsId = 53;

		// Token: 0x0400770B RID: 30475
		internal const int ElementTypeIdConst = 12964;

		// Token: 0x0400770C RID: 30476
		private static string[] attributeTagNames = new string[] { "type", "gte" };

		// Token: 0x0400770D RID: 30477
		private static byte[] attributeNamespaceIds;

		// Token: 0x0400770E RID: 30478
		private static readonly string[] eleTagNames;

		// Token: 0x0400770F RID: 30479
		private static readonly byte[] eleNamespaceIds;
	}
}
