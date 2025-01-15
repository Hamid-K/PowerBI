using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Wordprocessing
{
	// Token: 0x02002F38 RID: 12088
	[ChildElementInfo(typeof(Checked))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AutomaticallySizeFormField))]
	[ChildElementInfo(typeof(DefaultCheckBoxFormFieldState))]
	[ChildElementInfo(typeof(FormFieldSize))]
	internal class CheckBox : OpenXmlCompositeElement
	{
		// Token: 0x17008F8D RID: 36749
		// (get) Token: 0x06019EED RID: 106221 RVA: 0x002C8F4A File Offset: 0x002C714A
		public override string LocalName
		{
			get
			{
				return "checkBox";
			}
		}

		// Token: 0x17008F8E RID: 36750
		// (get) Token: 0x06019EEE RID: 106222 RVA: 0x00243C33 File Offset: 0x00241E33
		internal override byte NamespaceId
		{
			get
			{
				return 23;
			}
		}

		// Token: 0x17008F8F RID: 36751
		// (get) Token: 0x06019EEF RID: 106223 RVA: 0x0035A015 File Offset: 0x00358215
		internal override int ElementTypeId
		{
			get
			{
				return 11733;
			}
		}

		// Token: 0x06019EF0 RID: 106224 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x06019EF1 RID: 106225 RVA: 0x00293ECF File Offset: 0x002920CF
		public CheckBox()
		{
		}

		// Token: 0x06019EF2 RID: 106226 RVA: 0x00293ED7 File Offset: 0x002920D7
		public CheckBox(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019EF3 RID: 106227 RVA: 0x00293EE0 File Offset: 0x002920E0
		public CheckBox(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x06019EF4 RID: 106228 RVA: 0x00293EE9 File Offset: 0x002920E9
		public CheckBox(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x06019EF5 RID: 106229 RVA: 0x0035A01C File Offset: 0x0035821C
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (23 == namespaceId && "size" == name)
			{
				return new FormFieldSize();
			}
			if (23 == namespaceId && "sizeAuto" == name)
			{
				return new AutomaticallySizeFormField();
			}
			if (23 == namespaceId && "default" == name)
			{
				return new DefaultCheckBoxFormFieldState();
			}
			if (23 == namespaceId && "checked" == name)
			{
				return new Checked();
			}
			return null;
		}

		// Token: 0x06019EF6 RID: 106230 RVA: 0x0035A08A File Offset: 0x0035828A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<CheckBox>(deep);
		}

		// Token: 0x0400AAF7 RID: 43767
		private const string tagName = "checkBox";

		// Token: 0x0400AAF8 RID: 43768
		private const byte tagNsId = 23;

		// Token: 0x0400AAF9 RID: 43769
		internal const int ElementTypeIdConst = 11733;
	}
}
