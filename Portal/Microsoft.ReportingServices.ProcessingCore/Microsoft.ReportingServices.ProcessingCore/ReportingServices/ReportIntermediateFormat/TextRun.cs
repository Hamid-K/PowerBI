using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandReportRendering;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000503 RID: 1283
	[Serializable]
	internal sealed class TextRun : IDOwner, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IStyleContainer, IActionOwner
	{
		// Token: 0x06004300 RID: 17152 RVA: 0x00119B64 File Offset: 0x00117D64
		internal TextRun(Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph paragraph, int index, int id)
			: base(id)
		{
			this.m_indexInCollection = index;
			this.m_paragraph = paragraph;
		}

		// Token: 0x06004301 RID: 17153 RVA: 0x00119B92 File Offset: 0x00117D92
		internal TextRun()
		{
		}

		// Token: 0x17001C25 RID: 7205
		// (get) Token: 0x06004302 RID: 17154 RVA: 0x00119BB1 File Offset: 0x00117DB1
		internal string IDString
		{
			get
			{
				if (this.m_idString == null)
				{
					this.m_idString = this.m_paragraph.IDString + "x" + this.m_indexInCollection.ToString(CultureInfo.InvariantCulture);
				}
				return this.m_idString;
			}
		}

		// Token: 0x17001C26 RID: 7206
		// (get) Token: 0x06004303 RID: 17155 RVA: 0x00119BEC File Offset: 0x00117DEC
		// (set) Token: 0x06004304 RID: 17156 RVA: 0x00119BF4 File Offset: 0x00117DF4
		internal ExpressionInfo Value
		{
			get
			{
				return this.m_value;
			}
			set
			{
				this.m_value = value;
			}
		}

		// Token: 0x17001C27 RID: 7207
		// (get) Token: 0x06004305 RID: 17157 RVA: 0x00119BFD File Offset: 0x00117DFD
		// (set) Token: 0x06004306 RID: 17158 RVA: 0x00119C05 File Offset: 0x00117E05
		internal string Label
		{
			get
			{
				return this.m_label;
			}
			set
			{
				this.m_label = value;
			}
		}

		// Token: 0x17001C28 RID: 7208
		// (get) Token: 0x06004307 RID: 17159 RVA: 0x00119C0E File Offset: 0x00117E0E
		// (set) Token: 0x06004308 RID: 17160 RVA: 0x00119C16 File Offset: 0x00117E16
		internal ExpressionInfo MarkupType
		{
			get
			{
				return this.m_markupType;
			}
			set
			{
				this.m_markupType = value;
			}
		}

		// Token: 0x17001C29 RID: 7209
		// (get) Token: 0x06004309 RID: 17161 RVA: 0x00119C1F File Offset: 0x00117E1F
		// (set) Token: 0x0600430A RID: 17162 RVA: 0x00119C27 File Offset: 0x00117E27
		internal ExpressionInfo ToolTip
		{
			get
			{
				return this.m_toolTip;
			}
			set
			{
				this.m_toolTip = value;
			}
		}

		// Token: 0x17001C2A RID: 7210
		// (get) Token: 0x0600430B RID: 17163 RVA: 0x00119C30 File Offset: 0x00117E30
		// (set) Token: 0x0600430C RID: 17164 RVA: 0x00119C38 File Offset: 0x00117E38
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Style StyleClass
		{
			get
			{
				return this.m_styleClass;
			}
			set
			{
				this.m_styleClass = value;
			}
		}

		// Token: 0x17001C2B RID: 7211
		// (get) Token: 0x0600430D RID: 17165 RVA: 0x00119C41 File Offset: 0x00117E41
		// (set) Token: 0x0600430E RID: 17166 RVA: 0x00119C49 File Offset: 0x00117E49
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph Paragraph
		{
			get
			{
				return this.m_paragraph;
			}
			set
			{
				this.m_paragraph = value;
			}
		}

		// Token: 0x17001C2C RID: 7212
		// (get) Token: 0x0600430F RID: 17167 RVA: 0x00119C52 File Offset: 0x00117E52
		// (set) Token: 0x06004310 RID: 17168 RVA: 0x00119C5A File Offset: 0x00117E5A
		internal int IndexInCollection
		{
			get
			{
				return this.m_indexInCollection;
			}
			set
			{
				this.m_indexInCollection = value;
			}
		}

		// Token: 0x17001C2D RID: 7213
		// (get) Token: 0x06004311 RID: 17169 RVA: 0x00119C63 File Offset: 0x00117E63
		// (set) Token: 0x06004312 RID: 17170 RVA: 0x00119C6B File Offset: 0x00117E6B
		internal DataType DataType
		{
			get
			{
				return this.m_constantDataType;
			}
			set
			{
				this.m_constantDataType = value;
			}
		}

		// Token: 0x17001C2E RID: 7214
		// (get) Token: 0x06004313 RID: 17171 RVA: 0x00119C74 File Offset: 0x00117E74
		// (set) Token: 0x06004314 RID: 17172 RVA: 0x00119C7C File Offset: 0x00117E7C
		internal bool ValueReferenced
		{
			get
			{
				return this.m_valueReferenced;
			}
			set
			{
				this.m_valueReferenced = value;
			}
		}

		// Token: 0x17001C2F RID: 7215
		// (get) Token: 0x06004315 RID: 17173 RVA: 0x00119C85 File Offset: 0x00117E85
		IInstancePath IStyleContainer.InstancePath
		{
			get
			{
				return this.m_paragraph.TextBox;
			}
		}

		// Token: 0x17001C30 RID: 7216
		// (get) Token: 0x06004316 RID: 17174 RVA: 0x00119C92 File Offset: 0x00117E92
		Microsoft.ReportingServices.ReportIntermediateFormat.Style IStyleContainer.StyleClass
		{
			get
			{
				return this.m_styleClass;
			}
		}

		// Token: 0x17001C31 RID: 7217
		// (get) Token: 0x06004317 RID: 17175 RVA: 0x00119C9A File Offset: 0x00117E9A
		public Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.TextRun;
			}
		}

		// Token: 0x17001C32 RID: 7218
		// (get) Token: 0x06004318 RID: 17176 RVA: 0x00119C9E File Offset: 0x00117E9E
		public string Name
		{
			get
			{
				if (this.m_name == null)
				{
					this.m_name = this.m_paragraph.Name + ".TextRuns[" + this.m_indexInCollection.ToString(CultureInfo.InvariantCulture) + "]";
				}
				return this.m_name;
			}
		}

		// Token: 0x17001C33 RID: 7219
		// (get) Token: 0x06004319 RID: 17177 RVA: 0x00119CE0 File Offset: 0x00117EE0
		internal TypeCode ValueTypeCode
		{
			get
			{
				if (!this.m_valueTypeSet)
				{
					this.m_valueTypeSet = true;
					if (this.m_value == null)
					{
						this.m_valueType = TypeCode.String;
					}
					else if (!this.m_value.IsExpression)
					{
						this.m_valueType = this.m_value.ConstantTypeCode;
					}
					else
					{
						this.m_valueType = TypeCode.Object;
					}
				}
				return this.m_valueType;
			}
		}

		// Token: 0x17001C34 RID: 7220
		// (get) Token: 0x0600431A RID: 17178 RVA: 0x00119D3B File Offset: 0x00117F3B
		// (set) Token: 0x0600431B RID: 17179 RVA: 0x00119D43 File Offset: 0x00117F43
		internal Microsoft.ReportingServices.ReportIntermediateFormat.Action Action
		{
			get
			{
				return this.m_action;
			}
			set
			{
				this.m_action = value;
			}
		}

		// Token: 0x17001C35 RID: 7221
		// (get) Token: 0x0600431C RID: 17180 RVA: 0x00119D4C File Offset: 0x00117F4C
		Microsoft.ReportingServices.ReportIntermediateFormat.Action IActionOwner.Action
		{
			get
			{
				return this.m_action;
			}
		}

		// Token: 0x17001C36 RID: 7222
		// (get) Token: 0x0600431D RID: 17181 RVA: 0x00119D54 File Offset: 0x00117F54
		// (set) Token: 0x0600431E RID: 17182 RVA: 0x00119D5C File Offset: 0x00117F5C
		List<string> IActionOwner.FieldsUsedInValueExpression
		{
			get
			{
				return this.m_fieldsUsedInValueExpression;
			}
			set
			{
				this.m_fieldsUsedInValueExpression = value;
			}
		}

		// Token: 0x17001C37 RID: 7223
		// (get) Token: 0x0600431F RID: 17183 RVA: 0x00119D65 File Offset: 0x00117F65
		internal TextRunExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06004320 RID: 17184 RVA: 0x00119D70 File Offset: 0x00117F70
		internal bool Initialize(InitializationContext context, out bool hasExpressionBasedValue)
		{
			bool flag = false;
			hasExpressionBasedValue = false;
			context.ExprHostBuilder.TextRunStart(this.m_indexInCollection);
			if (this.m_value != null)
			{
				flag = true;
				hasExpressionBasedValue = this.m_value.IsExpression;
				this.m_value.Initialize("Value", context);
				context.ExprHostBuilder.TextRunValue(this.m_value);
			}
			if (this.m_toolTip != null)
			{
				this.m_toolTip.Initialize("ToolTip", context);
				context.ExprHostBuilder.TextRunToolTip(this.m_toolTip);
			}
			if (this.m_markupType != null)
			{
				this.m_markupType.Initialize("MarkupType", context);
				context.ExprHostBuilder.TextRunMarkupType(this.m_markupType);
			}
			if (this.m_action != null)
			{
				this.m_action.Initialize(context);
			}
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
			this.m_exprHostID = context.ExprHostBuilder.TextRunEnd();
			return flag;
		}

		// Token: 0x06004321 RID: 17185 RVA: 0x00119E60 File Offset: 0x00118060
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.TextRun textRun = (Microsoft.ReportingServices.ReportIntermediateFormat.TextRun)base.PublishClone(context);
			if (this.m_value != null)
			{
				textRun.m_value = (ExpressionInfo)this.m_value.PublishClone(context);
			}
			if (this.m_toolTip != null)
			{
				textRun.m_toolTip = (ExpressionInfo)this.m_toolTip.PublishClone(context);
			}
			if (this.m_styleClass != null)
			{
				textRun.m_styleClass = (Microsoft.ReportingServices.ReportIntermediateFormat.Style)this.m_styleClass.PublishClone(context);
			}
			if (this.m_markupType != null)
			{
				textRun.m_markupType = (ExpressionInfo)this.m_markupType.PublishClone(context);
			}
			if (this.m_action != null)
			{
				textRun.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)this.m_action.PublishClone(context);
			}
			return textRun;
		}

		// Token: 0x06004322 RID: 17186 RVA: 0x00119F18 File Offset: 0x00118118
		internal bool DetermineSimplicity()
		{
			if (this.m_markupType == null || (!this.m_markupType.IsExpression && string.Equals(this.m_markupType.StringValue, "None", StringComparison.Ordinal)))
			{
				Microsoft.ReportingServices.ReportIntermediateFormat.TextBox textBox = this.m_paragraph.TextBox;
				if ((this.m_action == null || textBox.Action == null) && (this.m_toolTip == null || textBox.ToolTip == null))
				{
					if (this.m_action != null)
					{
						textBox.Action = this.m_action;
						this.m_action = null;
					}
					if (this.m_toolTip != null)
					{
						textBox.ToolTip = this.m_toolTip;
						this.m_toolTip = null;
					}
					return true;
				}
			}
			return false;
		}

		// Token: 0x06004323 RID: 17187 RVA: 0x00119FB8 File Offset: 0x001181B8
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TextRun, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IDOwner, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Value, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ToolTip, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Label, Token.String),
				new MemberInfo(MemberName.MarkupType, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Style, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Style),
				new MemberInfo(MemberName.Action, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Action),
				new MemberInfo(MemberName.DataType, Token.Enum),
				new MemberInfo(MemberName.IndexInCollection, Token.Int32),
				new MemberInfo(MemberName.Paragraph, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Paragraph, Token.Reference),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.ValueReferenced, Token.Boolean)
			});
		}

		// Token: 0x06004324 RID: 17188 RVA: 0x0011A0BC File Offset: 0x001182BC
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.TextRun.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.ExprHostID)
				{
					if (memberName <= MemberName.DataType)
					{
						if (memberName == MemberName.Value)
						{
							writer.Write(this.m_value);
							continue;
						}
						if (memberName == MemberName.DataType)
						{
							writer.WriteEnum((int)this.m_constantDataType);
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.Label)
						{
							writer.Write(this.m_label);
							continue;
						}
						if (memberName == MemberName.ToolTip)
						{
							writer.Write(this.m_toolTip);
							continue;
						}
						if (memberName == MemberName.ExprHostID)
						{
							writer.Write(this.m_exprHostID);
							continue;
						}
					}
				}
				else if (memberName <= MemberName.IndexInCollection)
				{
					if (memberName == MemberName.ValueReferenced)
					{
						writer.Write(this.m_valueReferenced);
						continue;
					}
					if (memberName == MemberName.Action)
					{
						writer.Write(this.m_action);
						continue;
					}
					if (memberName == MemberName.IndexInCollection)
					{
						writer.Write(this.m_indexInCollection);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Style)
					{
						writer.Write(this.m_styleClass);
						continue;
					}
					if (memberName == MemberName.Paragraph)
					{
						writer.WriteReference(this.m_paragraph);
						continue;
					}
					if (memberName == MemberName.MarkupType)
					{
						writer.Write(this.m_markupType);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004325 RID: 17189 RVA: 0x0011A244 File Offset: 0x00118444
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.TextRun.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.ExprHostID)
				{
					if (memberName <= MemberName.DataType)
					{
						if (memberName == MemberName.Value)
						{
							this.m_value = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.DataType)
						{
							this.m_constantDataType = (DataType)reader.ReadEnum();
							continue;
						}
					}
					else
					{
						if (memberName == MemberName.Label)
						{
							this.m_label = reader.ReadString();
							continue;
						}
						if (memberName == MemberName.ToolTip)
						{
							this.m_toolTip = (ExpressionInfo)reader.ReadRIFObject();
							continue;
						}
						if (memberName == MemberName.ExprHostID)
						{
							this.m_exprHostID = reader.ReadInt32();
							continue;
						}
					}
				}
				else if (memberName <= MemberName.IndexInCollection)
				{
					if (memberName == MemberName.ValueReferenced)
					{
						this.m_valueReferenced = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.Action)
					{
						this.m_action = (Microsoft.ReportingServices.ReportIntermediateFormat.Action)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.IndexInCollection)
					{
						this.m_indexInCollection = reader.ReadInt32();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.Style)
					{
						this.m_styleClass = (Microsoft.ReportingServices.ReportIntermediateFormat.Style)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.Paragraph)
					{
						this.m_paragraph = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph>(this);
						continue;
					}
					if (memberName == MemberName.MarkupType)
					{
						this.m_markupType = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06004326 RID: 17190 RVA: 0x0011A3E8 File Offset: 0x001185E8
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(Microsoft.ReportingServices.ReportIntermediateFormat.TextRun.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.Paragraph)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						Global.Tracer.Assert(referenceableItems[memberReference.RefID] is Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph);
						this.m_paragraph = (Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x06004327 RID: 17191 RVA: 0x0011A4AC File Offset: 0x001186AC
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TextRun;
		}

		// Token: 0x06004328 RID: 17192 RVA: 0x0011A4B3 File Offset: 0x001186B3
		internal void SetExprHost(TextRunExprHost textRunExprHost)
		{
			this.m_exprHost = textRunExprHost;
		}

		// Token: 0x06004329 RID: 17193 RVA: 0x0011A4BC File Offset: 0x001186BC
		internal void SetExprHost(ParagraphExprHost paragraphExprHost, ObjectModelImpl reportObjectModel)
		{
			if (this.m_exprHostID >= 0)
			{
				this.m_exprHost = paragraphExprHost.TextRunHostsRemotable[this.m_exprHostID];
				Global.Tracer.Assert(this.m_exprHost != null && reportObjectModel != null);
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
				if (this.m_action != null && this.m_exprHost.ActionInfoHost != null)
				{
					this.m_action.SetExprHost(this.m_exprHost.ActionInfoHost, reportObjectModel);
				}
				if (this.m_styleClass != null)
				{
					this.m_styleClass.SetStyleExprHost(this.m_exprHost);
				}
			}
		}

		// Token: 0x0600432A RID: 17194 RVA: 0x0011A556 File Offset: 0x00118756
		internal string EvaluateMarkupType(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_paragraph.TextBox, instance);
			return context.ReportRuntime.EvaluateTextRunMarkupTypeExpression(this);
		}

		// Token: 0x0600432B RID: 17195 RVA: 0x0011A576 File Offset: 0x00118776
		internal string EvaluateToolTip(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_paragraph.TextBox, instance);
			return context.ReportRuntime.EvaluateTextRunToolTipExpression(this);
		}

		// Token: 0x0600432C RID: 17196 RVA: 0x0011A596 File Offset: 0x00118796
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateValue(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			return this.GetTextRunImpl(context).GetResult(instance);
		}

		// Token: 0x0600432D RID: 17197 RVA: 0x0011A5A5 File Offset: 0x001187A5
		internal List<string> GetFieldsUsedInValueExpression(IReportScopeInstance romInstance, OnDemandProcessingContext context)
		{
			return this.GetTextRunImpl(context).GetFieldsUsedInValueExpression(romInstance);
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x0011A5B4 File Offset: 0x001187B4
		private TextRunImpl GetTextRunImpl(OnDemandProcessingContext context)
		{
			if (this.m_textRunImpl == null)
			{
				this.m_textRunImpl = (TextRunImpl)this.m_paragraph.GetParagraphImpl(context).TextRuns[this.m_indexInCollection];
			}
			return this.m_textRunImpl;
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x0011A5EC File Offset: 0x001187EC
		internal string FormatTextRunValue(Microsoft.ReportingServices.RdlExpressions.VariantResult textRunResult, OnDemandProcessingContext context)
		{
			string text = null;
			if (textRunResult.ErrorOccurred)
			{
				text = RPRes.rsExpressionErrorValue;
			}
			else if (textRunResult.Value != null)
			{
				text = this.FormatTextRunValue(textRunResult.Value, textRunResult.TypeCode, null, context);
			}
			return text;
		}

		// Token: 0x06004330 RID: 17200 RVA: 0x0011A629 File Offset: 0x00118829
		internal string FormatTextRunValue(object textRunValue, TypeCode typeCode, OnDemandProcessingContext context)
		{
			return this.FormatTextRunValue(textRunValue, typeCode, string.Empty, context);
		}

		// Token: 0x06004331 RID: 17201 RVA: 0x0011A639 File Offset: 0x00118839
		private string FormatTextRunValue(object textRunValue, TypeCode typeCode, string formatCode, OnDemandProcessingContext context)
		{
			if (this.m_formatter == null)
			{
				this.m_formatter = new Formatter(this.m_styleClass, context, Microsoft.ReportingServices.ReportProcessing.ObjectType.TextRun, this.Name);
			}
			return this.m_formatter.FormatValue(textRunValue, formatCode, typeCode);
		}

		// Token: 0x04001E95 RID: 7829
		private ExpressionInfo m_value;

		// Token: 0x04001E96 RID: 7830
		private ExpressionInfo m_toolTip;

		// Token: 0x04001E97 RID: 7831
		private Microsoft.ReportingServices.ReportIntermediateFormat.Style m_styleClass;

		// Token: 0x04001E98 RID: 7832
		private Microsoft.ReportingServices.ReportIntermediateFormat.Action m_action;

		// Token: 0x04001E99 RID: 7833
		private string m_label;

		// Token: 0x04001E9A RID: 7834
		private ExpressionInfo m_markupType;

		// Token: 0x04001E9B RID: 7835
		private DataType m_constantDataType = DataType.String;

		// Token: 0x04001E9C RID: 7836
		private int m_indexInCollection;

		// Token: 0x04001E9D RID: 7837
		private int m_exprHostID = -1;

		// Token: 0x04001E9E RID: 7838
		private bool m_valueReferenced;

		// Token: 0x04001E9F RID: 7839
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph m_paragraph;

		// Token: 0x04001EA0 RID: 7840
		[NonSerialized]
		private string m_idString;

		// Token: 0x04001EA1 RID: 7841
		[NonSerialized]
		private string m_name;

		// Token: 0x04001EA2 RID: 7842
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.TextRun.GetDeclaration();

		// Token: 0x04001EA3 RID: 7843
		[NonSerialized]
		private TextRunImpl m_textRunImpl;

		// Token: 0x04001EA4 RID: 7844
		[NonSerialized]
		private TextRunExprHost m_exprHost;

		// Token: 0x04001EA5 RID: 7845
		[NonSerialized]
		private TypeCode m_valueType = TypeCode.String;

		// Token: 0x04001EA6 RID: 7846
		[NonSerialized]
		private bool m_valueTypeSet;

		// Token: 0x04001EA7 RID: 7847
		[NonSerialized]
		private Formatter m_formatter;

		// Token: 0x04001EA8 RID: 7848
		[NonSerialized]
		private List<string> m_fieldsUsedInValueExpression;
	}
}
