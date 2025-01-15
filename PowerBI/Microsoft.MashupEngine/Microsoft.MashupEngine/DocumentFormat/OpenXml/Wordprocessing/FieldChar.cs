using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002E79 RID: 11897
	[ChildElementInfo(typeof(FieldData))]
	[ChildElementInfo(typeof(NumberingChange))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FormFieldData))]
	internal class FieldChar : OpenXmlCompositeElement
	{
		// Token: 0x17008AB8 RID: 35512
		// (get) Token: 0x06019459 RID: 103513 RVA: 0x003480C4 File Offset: 0x003462C4
		public override string LocalName
		{
			get
			{
				return "fldChar";
			}
		}

		// Token: 0x17008AB9 RID: 35513
		// (get) Token: 0x0601945A RID: 103514 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008ABA RID: 35514
		// (get) Token: 0x0601945B RID: 103515 RVA: 0x003480CB File Offset: 0x003462CB
		internal override int ElementTypeId
		{
			get
			{
				return 11567;
			}
		}

		// Token: 0x0601945C RID: 103516 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x17008ABB RID: 35515
		// (get) Token: 0x0601945D RID: 103517 RVA: 0x003480D2 File Offset: 0x003462D2
		internal override string[] AttributeTagNames
		{
			get
			{
				return FieldChar.attributeTagNames;
			}
		}

		// Token: 0x17008ABC RID: 35516
		// (get) Token: 0x0601945E RID: 103518 RVA: 0x003480D9 File Offset: 0x003462D9
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return FieldChar.attributeNamespaceIds;
			}
		}

		// Token: 0x17008ABD RID: 35517
		// (get) Token: 0x0601945F RID: 103519 RVA: 0x003480E0 File Offset: 0x003462E0
		// (set) Token: 0x06019460 RID: 103520 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(23, "fldCharType")]
		public EnumValue<FieldCharValues> FieldCharType
		{
			get
			{
				return (EnumValue<FieldCharValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x17008ABE RID: 35518
		// (get) Token: 0x06019461 RID: 103521 RVA: 0x003480EF File Offset: 0x003462EF
		// (set) Token: 0x06019462 RID: 103522 RVA: 0x002BD47A File Offset: 0x002BB67A
		[SchemaAttr(23, "fldLock")]
		public OnOffValue FieldLock
		{
			get
			{
				return (OnOffValue)base.Attributes[1];
			}
			set
			{
				base.Attributes[1] = value;
			}
		}

		// Token: 0x17008ABF RID: 35519
		// (get) Token: 0x06019463 RID: 103523 RVA: 0x003461ED File Offset: 0x003443ED
		// (set) Token: 0x06019464 RID: 103524 RVA: 0x002BD494 File Offset: 0x002BB694
		[SchemaAttr(23, "dirty")]
		public OnOffValue Dirty
		{
			get
			{
				return (OnOffValue)base.Attributes[2];
			}
			set
			{
				base.Attributes[2] = value;
			}
		}

		// Token: 0x06019465 RID: 103525 RVA: 0x00293ECF File Offset: 0x002920CF
		public FieldChar()
		{
		}

		// Token: 0x06019466 RID: 103526 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FieldChar(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019467 RID: 103527 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FieldChar(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019468 RID: 103528 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FieldChar(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019469 RID: 103529 RVA: 0x00348100 File Offset: 0x00346300
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "fldData" == name)
			{
				return new FieldData();
			}
			if (23 == namespaceId && "ffData" == name)
			{
				return new FormFieldData();
			}
			if (23 == namespaceId && "numberingChange" == name)
			{
				return new NumberingChange();
			}
			return null;
		}

		// Token: 0x17008AC0 RID: 35520
		// (get) Token: 0x0601946A RID: 103530 RVA: 0x00348156 File Offset: 0x00346356
		internal override string[] ElementTagNames
		{
			get
			{
				return FieldChar.eleTagNames;
			}
		}

		// Token: 0x17008AC1 RID: 35521
		// (get) Token: 0x0601946B RID: 103531 RVA: 0x0034815D File Offset: 0x0034635D
		internal override byte[] ElementNamespaceIds
		{
			get
			{
				return FieldChar.eleNamespaceIds;
			}
		}

		// Token: 0x17008AC2 RID: 35522
		// (get) Token: 0x0601946C RID: 103532 RVA: 0x000023C4 File Offset: 0x000005C4
		internal override OpenXmlCompositeType OpenXmlCompositeType
		{
			get
			{
				return OpenXmlCompositeType.OneChoice;
			}
		}

		// Token: 0x17008AC3 RID: 35523
		// (get) Token: 0x0601946D RID: 103533 RVA: 0x00348164 File Offset: 0x00346364
		// (set) Token: 0x0601946E RID: 103534 RVA: 0x0034816D File Offset: 0x0034636D
		public FieldData FieldData
		{
			get
			{
				return base.GetElement<FieldData>(0);
			}
			set
			{
				base.SetElement<FieldData>(0, value);
			}
		}

		// Token: 0x17008AC4 RID: 35524
		// (get) Token: 0x0601946F RID: 103535 RVA: 0x00348177 File Offset: 0x00346377
		// (set) Token: 0x06019470 RID: 103536 RVA: 0x00348180 File Offset: 0x00346380
		public FormFieldData FormFieldData
		{
			get
			{
				return base.GetElement<FormFieldData>(1);
			}
			set
			{
				base.SetElement<FormFieldData>(1, value);
			}
		}

		// Token: 0x17008AC5 RID: 35525
		// (get) Token: 0x06019471 RID: 103537 RVA: 0x00345DCE File Offset: 0x00343FCE
		// (set) Token: 0x06019472 RID: 103538 RVA: 0x00345DD7 File Offset: 0x00343FD7
		public NumberingChange NumberingChange
		{
			get
			{
				return base.GetElement<NumberingChange>(2);
			}
			set
			{
				base.SetElement<NumberingChange>(2, value);
			}
		}

		// Token: 0x06019473 RID: 103539 RVA: 0x0034818C File Offset: 0x0034638C
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "fldCharType" == name)
			{
				return new EnumValue<FieldCharValues>();
			}
			if (23 == namespaceId && "fldLock" == name)
			{
				return new OnOffValue();
			}
			if (23 == namespaceId && "dirty" == name)
			{
				return new OnOffValue();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x06019474 RID: 103540 RVA: 0x003481E9 File Offset: 0x003463E9
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FieldChar>(deep);
		}

		// Token: 0x0400A809 RID: 43017
		private const string tagName = "fldChar";

		// Token: 0x0400A80A RID: 43018
		private const byte tagNsId = 23;

		// Token: 0x0400A80B RID: 43019
		internal const int ElementTypeIdConst = 11567;

		// Token: 0x0400A80C RID: 43020
		private static string[] attributeTagNames = new string[] { "fldCharType", "fldLock", "dirty" };

		// Token: 0x0400A80D RID: 43021
		private static byte[] attributeNamespaceIds = new byte[] { 23, 23, 23 };

		// Token: 0x0400A80E RID: 43022
		private static readonly string[] eleTagNames = new string[] { "fldData", "ffData", "numberingChange" };

		// Token: 0x0400A80F RID: 43023
		private static readonly byte[] eleNamespaceIds = new byte[] { 23, 23, 23 };
	}
}
