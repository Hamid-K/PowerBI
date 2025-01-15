using System;
using System.Collections.Specialized;
using System.Globalization;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportRendering
{
	// Token: 0x0200002B RID: 43
	internal sealed class TextBox : Microsoft.ReportingServices.ReportRendering.ReportItem
	{
		// Token: 0x06000476 RID: 1142 RVA: 0x0000D813 File Offset: 0x0000BA13
		internal TextBox(string uniqueName, int intUniqueName, Microsoft.ReportingServices.ReportProcessing.TextBox reportItemDef, Microsoft.ReportingServices.ReportProcessing.TextBoxInstance reportItemInstance, Microsoft.ReportingServices.ReportRendering.RenderingContext renderingContext)
			: base(uniqueName, intUniqueName, reportItemDef, reportItemInstance, renderingContext)
		{
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000477 RID: 1143 RVA: 0x0000D824 File Offset: 0x0000BA24
		internal SimpleTextBoxInstanceInfo SimpleInstanceInfo
		{
			get
			{
				if (base.IsCustomControl)
				{
					throw new RenderingObjectModelException(ProcessingErrorCode.rsInvalidOperation);
				}
				if (base.ReportItemInstance == null)
				{
					return null;
				}
				if (this.m_simpleInstanceInfo == null)
				{
					Microsoft.ReportingServices.ReportProcessing.TextBoxInstance textBoxInstance = (Microsoft.ReportingServices.ReportProcessing.TextBoxInstance)base.ReportItemInstance;
					this.m_simpleInstanceInfo = textBoxInstance.GetSimpleInstanceInfo(base.RenderingContext.ChunkManager, base.RenderingContext.InPageSection);
				}
				return this.m_simpleInstanceInfo;
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000478 RID: 1144 RVA: 0x0000D88A File Offset: 0x0000BA8A
		public Microsoft.ReportingServices.ReportRendering.Report.DataElementStyles DataElementStyle
		{
			get
			{
				if (!((Microsoft.ReportingServices.ReportProcessing.TextBox)base.ReportItemDef).DataElementStyleAttribute)
				{
					return Microsoft.ReportingServices.ReportRendering.Report.DataElementStyles.ElementNormal;
				}
				return Microsoft.ReportingServices.ReportRendering.Report.DataElementStyles.AttributeNormal;
			}
		}

		// Token: 0x1700039B RID: 923
		// (get) Token: 0x06000479 RID: 1145 RVA: 0x0000D8A1 File Offset: 0x0000BAA1
		public bool CanGrow
		{
			get
			{
				return ((Microsoft.ReportingServices.ReportProcessing.TextBox)base.ReportItemDef).CanGrow;
			}
		}

		// Token: 0x1700039C RID: 924
		// (get) Token: 0x0600047A RID: 1146 RVA: 0x0000D8B3 File Offset: 0x0000BAB3
		public bool CanShrink
		{
			get
			{
				return ((Microsoft.ReportingServices.ReportProcessing.TextBox)base.ReportItemDef).CanShrink;
			}
		}

		// Token: 0x1700039D RID: 925
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0000D8C8 File Offset: 0x0000BAC8
		public string Value
		{
			get
			{
				Microsoft.ReportingServices.ReportRendering.RenderingContext renderingContext = base.RenderingContext;
				string text = this.m_value;
				if (this.m_value == null)
				{
					Microsoft.ReportingServices.ReportProcessing.TextBox textBox = (Microsoft.ReportingServices.ReportProcessing.TextBox)base.ReportItemDef;
					if (textBox.Value.Type == ExpressionInfo.Types.Constant)
					{
						text = textBox.Value.Value;
					}
					else if (base.ReportItemInstance == null)
					{
						text = null;
					}
					else if (textBox.IsSimpleTextBox(base.RenderingContext.IntermediateFormatVersion))
					{
						text = this.SimpleInstanceInfo.FormattedValue;
						if (text == null)
						{
							text = this.SimpleInstanceInfo.OriginalValue as string;
						}
					}
					else
					{
						TextBoxInstanceInfo textBoxInstanceInfo = (TextBoxInstanceInfo)base.InstanceInfo;
						text = textBoxInstanceInfo.FormattedValue;
						if (text == null)
						{
							text = textBoxInstanceInfo.OriginalValue as string;
						}
					}
					if (base.RenderingContext.CacheState)
					{
						this.m_value = text;
					}
				}
				return text;
			}
		}

		// Token: 0x1700039E RID: 926
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x0000D990 File Offset: 0x0000BB90
		public ReportUrl HyperLinkURL
		{
			get
			{
				ActionInfo actionInfo = this.m_actionInfo;
				if (actionInfo == null)
				{
					actionInfo = this.ActionInfo;
				}
				if (actionInfo != null)
				{
					return actionInfo.Actions[0].HyperLinkURL;
				}
				return null;
			}
		}

		// Token: 0x1700039F RID: 927
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0000D9C4 File Offset: 0x0000BBC4
		public ReportUrl DrillthroughReport
		{
			get
			{
				ActionInfo actionInfo = this.m_actionInfo;
				if (actionInfo == null)
				{
					actionInfo = this.ActionInfo;
				}
				if (actionInfo != null)
				{
					return actionInfo.Actions[0].DrillthroughReport;
				}
				return null;
			}
		}

		// Token: 0x170003A0 RID: 928
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000D9F8 File Offset: 0x0000BBF8
		public NameValueCollection DrillthroughParameters
		{
			get
			{
				ActionInfo actionInfo = this.m_actionInfo;
				if (actionInfo == null)
				{
					actionInfo = this.ActionInfo;
				}
				if (actionInfo != null)
				{
					return actionInfo.Actions[0].DrillthroughParameters;
				}
				return null;
			}
		}

		// Token: 0x170003A1 RID: 929
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000DA2C File Offset: 0x0000BC2C
		public string BookmarkLink
		{
			get
			{
				ActionInfo actionInfo = this.m_actionInfo;
				if (actionInfo == null)
				{
					actionInfo = this.ActionInfo;
				}
				if (actionInfo != null)
				{
					return actionInfo.Actions[0].BookmarkLink;
				}
				return null;
			}
		}

		// Token: 0x170003A2 RID: 930
		// (get) Token: 0x06000480 RID: 1152 RVA: 0x0000DA60 File Offset: 0x0000BC60
		public ActionInfo ActionInfo
		{
			get
			{
				ActionInfo actionInfo = this.m_actionInfo;
				if (actionInfo == null)
				{
					Microsoft.ReportingServices.ReportProcessing.Action action = ((Microsoft.ReportingServices.ReportProcessing.TextBox)base.ReportItemDef).Action;
					if (action != null)
					{
						Microsoft.ReportingServices.ReportProcessing.ActionInstance actionInstance = null;
						string text = base.UniqueName;
						if (base.ReportItemInstance != null)
						{
							actionInstance = ((TextBoxInstanceInfo)base.InstanceInfo).Action;
							if (base.RenderingContext.InPageSection)
							{
								text = base.ReportItemInstance.UniqueName.ToString(CultureInfo.InvariantCulture);
							}
						}
						else if (base.RenderingContext.InPageSection && this.m_intUniqueName != 0)
						{
							text = this.m_intUniqueName.ToString(CultureInfo.InvariantCulture);
						}
						actionInfo = new ActionInfo(action, actionInstance, text, base.RenderingContext);
						if (base.RenderingContext.CacheState)
						{
							this.m_actionInfo = actionInfo;
						}
					}
				}
				return actionInfo;
			}
		}

		// Token: 0x170003A3 RID: 931
		// (get) Token: 0x06000481 RID: 1153 RVA: 0x0000DB26 File Offset: 0x0000BD26
		public bool Duplicate
		{
			get
			{
				return this.HideDuplicates && base.ReportItemInstance != null && ((TextBoxInstanceInfo)base.InstanceInfo).Duplicate;
			}
		}

		// Token: 0x170003A4 RID: 932
		// (get) Token: 0x06000482 RID: 1154 RVA: 0x0000DB4C File Offset: 0x0000BD4C
		public bool HideDuplicates
		{
			get
			{
				return ((Microsoft.ReportingServices.ReportProcessing.TextBox)base.ReportItemDef).HideDuplicates != null;
			}
		}

		// Token: 0x170003A5 RID: 933
		// (get) Token: 0x06000483 RID: 1155 RVA: 0x0000DB61 File Offset: 0x0000BD61
		public string Formula
		{
			get
			{
				return ((Microsoft.ReportingServices.ReportProcessing.TextBox)base.ReportItemDef).Formula;
			}
		}

		// Token: 0x170003A6 RID: 934
		// (get) Token: 0x06000484 RID: 1156 RVA: 0x0000DB74 File Offset: 0x0000BD74
		public object OriginalValue
		{
			get
			{
				object obj = this.m_originalValue;
				if (this.m_originalValue == null)
				{
					Microsoft.ReportingServices.ReportProcessing.TextBox textBox = (Microsoft.ReportingServices.ReportProcessing.TextBox)base.ReportItemDef;
					if (textBox.Value.Type == ExpressionInfo.Types.Constant)
					{
						obj = textBox.Value.Value;
					}
					else if (base.ReportItemInstance == null)
					{
						obj = null;
					}
					else if (textBox.IsSimpleTextBox(base.RenderingContext.IntermediateFormatVersion))
					{
						obj = this.SimpleInstanceInfo.OriginalValue;
					}
					else
					{
						obj = ((TextBoxInstanceInfo)base.InstanceInfo).OriginalValue;
					}
					if (base.RenderingContext.CacheState)
					{
						this.m_originalValue = obj;
					}
				}
				return obj;
			}
		}

		// Token: 0x170003A7 RID: 935
		// (get) Token: 0x06000485 RID: 1157 RVA: 0x0000DC0B File Offset: 0x0000BE0B
		public TypeCode SharedTypeCode
		{
			get
			{
				return ((Microsoft.ReportingServices.ReportProcessing.TextBox)base.ReportItemDef).ValueType;
			}
		}

		// Token: 0x170003A8 RID: 936
		// (get) Token: 0x06000486 RID: 1158 RVA: 0x0000DC20 File Offset: 0x0000BE20
		public override bool Hidden
		{
			get
			{
				if (base.ReportItemInstance == null)
				{
					return Microsoft.ReportingServices.ReportRendering.RenderingContext.GetDefinitionHidden(base.ReportItemDef.Visibility);
				}
				if (base.ReportItemDef.Visibility == null)
				{
					return false;
				}
				if (base.ReportItemDef.Visibility.Toggle != null)
				{
					return base.RenderingContext.IsItemHidden(base.ReportItemInstance.UniqueName, true);
				}
				return base.InstanceInfo.StartHidden;
			}
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06000487 RID: 1159 RVA: 0x0000DC8A File Offset: 0x0000BE8A
		public bool IsToggleParent
		{
			get
			{
				return base.ReportItemInstance != null && this.IsSharedToggleParent && base.RenderingContext.IsToggleParent(base.ReportItemInstance.UniqueName);
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06000488 RID: 1160 RVA: 0x0000DCB6 File Offset: 0x0000BEB6
		public bool IsSharedToggleParent
		{
			get
			{
				return ((Microsoft.ReportingServices.ReportProcessing.TextBox)base.ReportItemDef).IsToggle;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06000489 RID: 1161 RVA: 0x0000DCC8 File Offset: 0x0000BEC8
		public bool ToggleState
		{
			get
			{
				if (base.ReportItemInstance == null)
				{
					return false;
				}
				if (!this.IsSharedToggleParent)
				{
					return false;
				}
				if (base.RenderingContext.IsToggleStateNegated(base.ReportItemInstance.UniqueName))
				{
					return !((TextBoxInstanceInfo)base.InstanceInfo).InitialToggleState;
				}
				return ((TextBoxInstanceInfo)base.InstanceInfo).InitialToggleState;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x0600048A RID: 1162 RVA: 0x0000DD25 File Offset: 0x0000BF25
		public bool CanSort
		{
			get
			{
				return ((Microsoft.ReportingServices.ReportProcessing.TextBox)base.ReportItemDef).UserSort != null;
			}
		}

		// Token: 0x170003AD RID: 941
		// (get) Token: 0x0600048B RID: 1163 RVA: 0x0000DD3A File Offset: 0x0000BF3A
		public SortOptions SortState
		{
			get
			{
				if (base.IsCustomControl)
				{
					return SortOptions.None;
				}
				return base.RenderingContext.GetSortState(this.m_intUniqueName);
			}
		}

		// Token: 0x0600048C RID: 1164 RVA: 0x0000DD57 File Offset: 0x0000BF57
		internal override bool Search(SearchContext searchContext)
		{
			return !base.SkipSearch && this.SearchTextBox(searchContext.FindValue);
		}

		// Token: 0x0600048D RID: 1165 RVA: 0x0000DD70 File Offset: 0x0000BF70
		private bool SearchTextBox(string findValue)
		{
			string value = this.Value;
			return value != null && value.IndexOf(findValue, 0, StringComparison.OrdinalIgnoreCase) >= 0;
		}

		// Token: 0x040000DF RID: 223
		private SimpleTextBoxInstanceInfo m_simpleInstanceInfo;

		// Token: 0x040000E0 RID: 224
		private string m_value;

		// Token: 0x040000E1 RID: 225
		private ActionInfo m_actionInfo;

		// Token: 0x040000E2 RID: 226
		private object m_originalValue;
	}
}
