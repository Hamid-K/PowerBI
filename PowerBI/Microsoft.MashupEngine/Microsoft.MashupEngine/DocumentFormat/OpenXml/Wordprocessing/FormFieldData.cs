using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F31 RID: 12081
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(FormFieldName))]
	[ChildElementInfo(typeof(Enabled))]
	[ChildElementInfo(typeof(CalculateOnExit))]
	[ChildElementInfo(typeof(EntryMacro))]
	[ChildElementInfo(typeof(ExitMacro))]
	[ChildElementInfo(typeof(HelpText))]
	[ChildElementInfo(typeof(StatusText))]
	[ChildElementInfo(typeof(CheckBox))]
	[ChildElementInfo(typeof(DropDownListFormField))]
	[ChildElementInfo(typeof(TextInput))]
	internal class FormFieldData : OpenXmlCompositeElement
	{
		// Token: 0x17008F6D RID: 36717
		// (get) Token: 0x06019EA8 RID: 106152 RVA: 0x00359D18 File Offset: 0x00357F18
		public override string LocalName
		{
			get
			{
				return "ffData";
			}
		}

		// Token: 0x17008F6E RID: 36718
		// (get) Token: 0x06019EA9 RID: 106153 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F6F RID: 36719
		// (get) Token: 0x06019EAA RID: 106154 RVA: 0x00359D1F File Offset: 0x00357F1F
		internal override int ElementTypeId
		{
			get
			{
				return 11725;
			}
		}

		// Token: 0x06019EAB RID: 106155 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019EAC RID: 106156 RVA: 0x00293ECF File Offset: 0x002920CF
		public FormFieldData()
		{
		}

		// Token: 0x06019EAD RID: 106157 RVA: 0x00293ED7 File Offset: 0x002920D7
		public FormFieldData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019EAE RID: 106158 RVA: 0x00293EE0 File Offset: 0x002920E0
		public FormFieldData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019EAF RID: 106159 RVA: 0x00293EE9 File Offset: 0x002920E9
		public FormFieldData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019EB0 RID: 106160 RVA: 0x00359D28 File Offset: 0x00357F28
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "name" == name)
			{
				return new FormFieldName();
			}
			if (23 == namespaceId && "enabled" == name)
			{
				return new Enabled();
			}
			if (23 == namespaceId && "calcOnExit" == name)
			{
				return new CalculateOnExit();
			}
			if (23 == namespaceId && "entryMacro" == name)
			{
				return new EntryMacro();
			}
			if (23 == namespaceId && "exitMacro" == name)
			{
				return new ExitMacro();
			}
			if (23 == namespaceId && "helpText" == name)
			{
				return new HelpText();
			}
			if (23 == namespaceId && "statusText" == name)
			{
				return new StatusText();
			}
			if (23 == namespaceId && "checkBox" == name)
			{
				return new CheckBox();
			}
			if (23 == namespaceId && "ddList" == name)
			{
				return new DropDownListFormField();
			}
			if (23 == namespaceId && "textInput" == name)
			{
				return new TextInput();
			}
			return null;
		}

		// Token: 0x06019EB1 RID: 106161 RVA: 0x00359E26 File Offset: 0x00358026
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<FormFieldData>(deep);
		}

		// Token: 0x0400AADD RID: 43741
		private const string tagName = "ffData";

		// Token: 0x0400AADE RID: 43742
		private const byte tagNsId = 23;

		// Token: 0x0400AADF RID: 43743
		internal const int ElementTypeIdConst = 11725;
	}
}
