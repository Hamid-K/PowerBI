using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;

namespace DocumentFormat.OpenXml.Vml.Spreadsheet
{
	// Token: 0x020021BA RID: 8634
	[ChildElementInfo(typeof(Anchor))]
	[ChildElementInfo(typeof(AutoFill))]
	[ChildElementInfo(typeof(AutoLine))]
	[ChildElementInfo(typeof(AutoSizePicture))]
	[ChildElementInfo(typeof(FormulaMacro))]
	[ChildElementInfo(typeof(HorizontalTextAlignment))]
	[ChildElementInfo(typeof(VerticalTextAlignment))]
	[ChildElementInfo(typeof(LockText))]
	[ChildElementInfo(typeof(JustifyLastLine))]
	[ChildElementInfo(typeof(SecretEdit))]
	[ChildElementInfo(typeof(DefaultButton))]
	[ChildElementInfo(typeof(HelpButton))]
	[ChildElementInfo(typeof(CancelButton))]
	[ChildElementInfo(typeof(DismissButton))]
	[ChildElementInfo(typeof(AcceleratorPrimary))]
	[ChildElementInfo(typeof(Disabled))]
	[ChildElementInfo(typeof(CommentRowTarget))]
	[ChildElementInfo(typeof(CommentColumnTarget))]
	[ChildElementInfo(typeof(Visible))]
	[ChildElementInfo(typeof(RowHidden))]
	[ChildElementInfo(typeof(ColumnHidden))]
	[ChildElementInfo(typeof(InputValidationType))]
	[ChildElementInfo(typeof(MultiLine))]
	[ChildElementInfo(typeof(VerticalScrollBar))]
	[ChildElementInfo(typeof(ValidIds))]
	[ChildElementInfo(typeof(FormulaRange))]
	[ChildElementInfo(typeof(MinDropDownWidth))]
	[ChildElementInfo(typeof(SelectionEntry))]
	[ChildElementInfo(typeof(Disable3DForListBoxAndDropDown))]
	[ChildElementInfo(typeof(SelectionType))]
	[ChildElementInfo(typeof(MultiSelections))]
	[ChildElementInfo(typeof(ListBoxCallbackType))]
	[ChildElementInfo(typeof(ListItem))]
	[ChildElementInfo(typeof(DropStyle))]
	[ChildElementInfo(typeof(Colored))]
	[ChildElementInfo(typeof(DropLines))]
	[ChildElementInfo(typeof(Checked))]
	[ChildElementInfo(typeof(FormulaLink))]
	[ChildElementInfo(typeof(FormulaPicture))]
	[ChildElementInfo(typeof(Disable3D))]
	[ChildElementInfo(typeof(FirstButton))]
	[ChildElementInfo(typeof(FormulaGroup))]
	[ChildElementInfo(typeof(ScrollBarPosition))]
	[ChildElementInfo(typeof(ScrollBarMin))]
	[ChildElementInfo(typeof(ScrollBarMax))]
	[ChildElementInfo(typeof(ScrollBarIncrement))]
	[ChildElementInfo(typeof(ScrollBarPageIncrement))]
	[ChildElementInfo(typeof(HorizontalScrollBar))]
	[ChildElementInfo(typeof(ScrollBarWidth))]
	[ChildElementInfo(typeof(MapOcxControl))]
	[ChildElementInfo(typeof(ClipboardFormat))]
	[ChildElementInfo(typeof(CameraObject))]
	[ChildElementInfo(typeof(RecalculateAlways))]
	[ChildElementInfo(typeof(AutoScaleFont))]
	[ChildElementInfo(typeof(DdeObject))]
	[ChildElementInfo(typeof(UIObject))]
	[ChildElementInfo(typeof(ScriptText))]
	[ChildElementInfo(typeof(ScriptExtended))]
	[ChildElementInfo(typeof(PrintObject))]
	[ChildElementInfo(typeof(ScriptLocation))]
	[ChildElementInfo(typeof(FormulaTextBox))]
	[GeneratedCode("DomGen", "2.0")]
	[ChildElementInfo(typeof(AcceleratorSecondary))]
	[ChildElementInfo(typeof(ScriptLanguage))]
	[ChildElementInfo(typeof(MoveWithCells))]
	[ChildElementInfo(typeof(ResizeWithCells))]
	[ChildElementInfo(typeof(Locked))]
	[ChildElementInfo(typeof(DefaultSize))]
	internal class ClientData : OpenXmlCompositeElement
	{
		// Token: 0x17003738 RID: 14136
		// (get) Token: 0x0600DB7A RID: 56186 RVA: 0x002BBFAA File Offset: 0x002BA1AA
		public override string LocalName
		{
			get
			{
				return "ClientData";
			}
		}

		// Token: 0x17003739 RID: 14137
		// (get) Token: 0x0600DB7B RID: 56187 RVA: 0x002BBFB1 File Offset: 0x002BA1B1
		internal override byte NamespaceId
		{
			get
			{
				return 29;
			}
		}

		// Token: 0x1700373A RID: 14138
		// (get) Token: 0x0600DB7C RID: 56188 RVA: 0x002BBFB5 File Offset: 0x002BA1B5
		internal override int ElementTypeId
		{
			get
			{
				return 12436;
			}
		}

		// Token: 0x0600DB7D RID: 56189 RVA: 0x002BBFBC File Offset: 0x002BA1BC
		internal override bool IsInVersion(FileFormatVersions version)
		{
			return ((FileFormatVersions.Office2007 | FileFormatVersions.Office2010) & version) > FileFormatVersions.None;
		}

		// Token: 0x1700373B RID: 14139
		// (get) Token: 0x0600DB7E RID: 56190 RVA: 0x002BBFC7 File Offset: 0x002BA1C7
		internal override string[] AttributeTagNames
		{
			get
			{
				return ClientData.attributeTagNames;
			}
		}

		// Token: 0x1700373C RID: 14140
		// (get) Token: 0x0600DB7F RID: 56191 RVA: 0x002BBFCE File Offset: 0x002BA1CE
		internal override byte[] AttributeNamespaceIds
		{
			get
			{
				return ClientData.attributeNamespaceIds;
			}
		}

		// Token: 0x1700373D RID: 14141
		// (get) Token: 0x0600DB80 RID: 56192 RVA: 0x002BBFD5 File Offset: 0x002BA1D5
		// (set) Token: 0x0600DB81 RID: 56193 RVA: 0x0029402B File Offset: 0x0029222B
		[SchemaAttr(0, "ObjectType")]
		public EnumValue<ObjectValues> ObjectType
		{
			get
			{
				return (EnumValue<ObjectValues>)base.Attributes[0];
			}
			set
			{
				base.Attributes[0] = value;
			}
		}

		// Token: 0x0600DB82 RID: 56194 RVA: 0x00293ECF File Offset: 0x002920CF
		public ClientData()
		{
		}

		// Token: 0x0600DB83 RID: 56195 RVA: 0x00293ED7 File Offset: 0x002920D7
		public ClientData(IEnumerable<OpenXmlElement> childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DB84 RID: 56196 RVA: 0x00293EE0 File Offset: 0x002920E0
		public ClientData(params OpenXmlElement[] childElements)
			: base(childElements)
		{
		}

		// Token: 0x0600DB85 RID: 56197 RVA: 0x00293EE9 File Offset: 0x002920E9
		public ClientData(string outerXml)
			: base(outerXml)
		{
		}

		// Token: 0x0600DB86 RID: 56198 RVA: 0x002BBFE4 File Offset: 0x002BA1E4
		internal override OpenXmlElement ElementFactory(byte namespaceId, string name)
		{
			if (29 == namespaceId && "MoveWithCells" == name)
			{
				return new MoveWithCells();
			}
			if (29 == namespaceId && "SizeWithCells" == name)
			{
				return new ResizeWithCells();
			}
			if (29 == namespaceId && "Anchor" == name)
			{
				return new Anchor();
			}
			if (29 == namespaceId && "Locked" == name)
			{
				return new Locked();
			}
			if (29 == namespaceId && "DefaultSize" == name)
			{
				return new DefaultSize();
			}
			if (29 == namespaceId && "PrintObject" == name)
			{
				return new PrintObject();
			}
			if (29 == namespaceId && "Disabled" == name)
			{
				return new Disabled();
			}
			if (29 == namespaceId && "AutoFill" == name)
			{
				return new AutoFill();
			}
			if (29 == namespaceId && "AutoLine" == name)
			{
				return new AutoLine();
			}
			if (29 == namespaceId && "AutoPict" == name)
			{
				return new AutoSizePicture();
			}
			if (29 == namespaceId && "FmlaMacro" == name)
			{
				return new FormulaMacro();
			}
			if (29 == namespaceId && "TextHAlign" == name)
			{
				return new HorizontalTextAlignment();
			}
			if (29 == namespaceId && "TextVAlign" == name)
			{
				return new VerticalTextAlignment();
			}
			if (29 == namespaceId && "LockText" == name)
			{
				return new LockText();
			}
			if (29 == namespaceId && "JustLastX" == name)
			{
				return new JustifyLastLine();
			}
			if (29 == namespaceId && "SecretEdit" == name)
			{
				return new SecretEdit();
			}
			if (29 == namespaceId && "Default" == name)
			{
				return new DefaultButton();
			}
			if (29 == namespaceId && "Help" == name)
			{
				return new HelpButton();
			}
			if (29 == namespaceId && "Cancel" == name)
			{
				return new CancelButton();
			}
			if (29 == namespaceId && "Dismiss" == name)
			{
				return new DismissButton();
			}
			if (29 == namespaceId && "Accel" == name)
			{
				return new AcceleratorPrimary();
			}
			if (29 == namespaceId && "Accel2" == name)
			{
				return new AcceleratorSecondary();
			}
			if (29 == namespaceId && "Row" == name)
			{
				return new CommentRowTarget();
			}
			if (29 == namespaceId && "Column" == name)
			{
				return new CommentColumnTarget();
			}
			if (29 == namespaceId && "Visible" == name)
			{
				return new Visible();
			}
			if (29 == namespaceId && "RowHidden" == name)
			{
				return new RowHidden();
			}
			if (29 == namespaceId && "ColHidden" == name)
			{
				return new ColumnHidden();
			}
			if (29 == namespaceId && "VTEdit" == name)
			{
				return new InputValidationType();
			}
			if (29 == namespaceId && "MultiLine" == name)
			{
				return new MultiLine();
			}
			if (29 == namespaceId && "VScroll" == name)
			{
				return new VerticalScrollBar();
			}
			if (29 == namespaceId && "ValidIds" == name)
			{
				return new ValidIds();
			}
			if (29 == namespaceId && "FmlaRange" == name)
			{
				return new FormulaRange();
			}
			if (29 == namespaceId && "WidthMin" == name)
			{
				return new MinDropDownWidth();
			}
			if (29 == namespaceId && "Sel" == name)
			{
				return new SelectionEntry();
			}
			if (29 == namespaceId && "NoThreeD2" == name)
			{
				return new Disable3DForListBoxAndDropDown();
			}
			if (29 == namespaceId && "SelType" == name)
			{
				return new SelectionType();
			}
			if (29 == namespaceId && "MultiSel" == name)
			{
				return new MultiSelections();
			}
			if (29 == namespaceId && "LCT" == name)
			{
				return new ListBoxCallbackType();
			}
			if (29 == namespaceId && "ListItem" == name)
			{
				return new ListItem();
			}
			if (29 == namespaceId && "DropStyle" == name)
			{
				return new DropStyle();
			}
			if (29 == namespaceId && "Colored" == name)
			{
				return new Colored();
			}
			if (29 == namespaceId && "DropLines" == name)
			{
				return new DropLines();
			}
			if (29 == namespaceId && "Checked" == name)
			{
				return new Checked();
			}
			if (29 == namespaceId && "FmlaLink" == name)
			{
				return new FormulaLink();
			}
			if (29 == namespaceId && "FmlaPict" == name)
			{
				return new FormulaPicture();
			}
			if (29 == namespaceId && "NoThreeD" == name)
			{
				return new Disable3D();
			}
			if (29 == namespaceId && "FirstButton" == name)
			{
				return new FirstButton();
			}
			if (29 == namespaceId && "FmlaGroup" == name)
			{
				return new FormulaGroup();
			}
			if (29 == namespaceId && "Val" == name)
			{
				return new ScrollBarPosition();
			}
			if (29 == namespaceId && "Min" == name)
			{
				return new ScrollBarMin();
			}
			if (29 == namespaceId && "Max" == name)
			{
				return new ScrollBarMax();
			}
			if (29 == namespaceId && "Inc" == name)
			{
				return new ScrollBarIncrement();
			}
			if (29 == namespaceId && "Page" == name)
			{
				return new ScrollBarPageIncrement();
			}
			if (29 == namespaceId && "Horiz" == name)
			{
				return new HorizontalScrollBar();
			}
			if (29 == namespaceId && "Dx" == name)
			{
				return new ScrollBarWidth();
			}
			if (29 == namespaceId && "MapOCX" == name)
			{
				return new MapOcxControl();
			}
			if (29 == namespaceId && "CF" == name)
			{
				return new ClipboardFormat();
			}
			if (29 == namespaceId && "Camera" == name)
			{
				return new CameraObject();
			}
			if (29 == namespaceId && "RecalcAlways" == name)
			{
				return new RecalculateAlways();
			}
			if (29 == namespaceId && "AutoScale" == name)
			{
				return new AutoScaleFont();
			}
			if (29 == namespaceId && "DDE" == name)
			{
				return new DdeObject();
			}
			if (29 == namespaceId && "UIObj" == name)
			{
				return new UIObject();
			}
			if (29 == namespaceId && "ScriptText" == name)
			{
				return new ScriptText();
			}
			if (29 == namespaceId && "ScriptExtended" == name)
			{
				return new ScriptExtended();
			}
			if (29 == namespaceId && "ScriptLanguage" == name)
			{
				return new ScriptLanguage();
			}
			if (29 == namespaceId && "ScriptLocation" == name)
			{
				return new ScriptLocation();
			}
			if (29 == namespaceId && "FmlaTxbx" == name)
			{
				return new FormulaTextBox();
			}
			return null;
		}

		// Token: 0x0600DB87 RID: 56199 RVA: 0x002BC63A File Offset: 0x002BA83A
		internal override OpenXmlSimpleType AttributeFactory(byte namespaceId, string name)
		{
			if (namespaceId == 0 && "ObjectType" == name)
			{
				return new EnumValue<ObjectValues>();
			}
			return base.AttributeFactory(namespaceId, name);
		}

		// Token: 0x0600DB88 RID: 56200 RVA: 0x002BC65A File Offset: 0x002BA85A
		public override OpenXmlElement CloneNode(bool deep)
		{
			return this.CloneImp<ClientData>(deep);
		}

		// Token: 0x0600DB89 RID: 56201 RVA: 0x002BC664 File Offset: 0x002BA864
		// Note: this type is marked as 'beforefieldinit'.
		static ClientData()
		{
			byte[] array = new byte[1];
			ClientData.attributeNamespaceIds = array;
		}

		// Token: 0x04006C60 RID: 27744
		private const string tagName = "ClientData";

		// Token: 0x04006C61 RID: 27745
		private const byte tagNsId = 29;

		// Token: 0x04006C62 RID: 27746
		internal const int ElementTypeIdConst = 12436;

		// Token: 0x04006C63 RID: 27747
		private static string[] attributeTagNames = new string[] { "ObjectType" };

		// Token: 0x04006C64 RID: 27748
		private static byte[] attributeNamespaceIds;
	}
}
