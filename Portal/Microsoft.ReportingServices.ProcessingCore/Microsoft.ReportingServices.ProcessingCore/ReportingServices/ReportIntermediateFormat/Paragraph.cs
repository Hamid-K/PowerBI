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
	// Token: 0x02000502 RID: 1282
	[Serializable]
	internal sealed class Paragraph : IDOwner, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IStyleContainer
	{
		// Token: 0x060042CE RID: 17102 RVA: 0x00118E83 File Offset: 0x00117083
		internal Paragraph(Microsoft.ReportingServices.ReportIntermediateFormat.TextBox textbox, int index, int id)
			: base(id)
		{
			this.m_indexInCollection = index;
			this.m_textBox = textbox;
			this.m_textRuns = new List<Microsoft.ReportingServices.ReportIntermediateFormat.TextRun>();
		}

		// Token: 0x060042CF RID: 17103 RVA: 0x00118EAC File Offset: 0x001170AC
		internal Paragraph()
		{
		}

		// Token: 0x17001C13 RID: 7187
		// (get) Token: 0x060042D0 RID: 17104 RVA: 0x00118EBC File Offset: 0x001170BC
		internal string IDString
		{
			get
			{
				if (this.m_idString == null)
				{
					this.m_idString = this.m_textBox.GlobalID.ToString(CultureInfo.InvariantCulture) + "x" + this.m_indexInCollection.ToString(CultureInfo.InvariantCulture);
				}
				return this.m_idString;
			}
		}

		// Token: 0x17001C14 RID: 7188
		// (get) Token: 0x060042D1 RID: 17105 RVA: 0x00118F0F File Offset: 0x0011710F
		// (set) Token: 0x060042D2 RID: 17106 RVA: 0x00118F17 File Offset: 0x00117117
		internal List<Microsoft.ReportingServices.ReportIntermediateFormat.TextRun> TextRuns
		{
			get
			{
				return this.m_textRuns;
			}
			set
			{
				this.m_textRuns = value;
			}
		}

		// Token: 0x17001C15 RID: 7189
		// (get) Token: 0x060042D3 RID: 17107 RVA: 0x00118F20 File Offset: 0x00117120
		// (set) Token: 0x060042D4 RID: 17108 RVA: 0x00118F28 File Offset: 0x00117128
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

		// Token: 0x17001C16 RID: 7190
		// (get) Token: 0x060042D5 RID: 17109 RVA: 0x00118F31 File Offset: 0x00117131
		// (set) Token: 0x060042D6 RID: 17110 RVA: 0x00118F39 File Offset: 0x00117139
		internal ExpressionInfo LeftIndent
		{
			get
			{
				return this.m_leftIndent;
			}
			set
			{
				this.m_leftIndent = value;
			}
		}

		// Token: 0x17001C17 RID: 7191
		// (get) Token: 0x060042D7 RID: 17111 RVA: 0x00118F42 File Offset: 0x00117142
		// (set) Token: 0x060042D8 RID: 17112 RVA: 0x00118F4A File Offset: 0x0011714A
		internal ExpressionInfo RightIndent
		{
			get
			{
				return this.m_rightIndent;
			}
			set
			{
				this.m_rightIndent = value;
			}
		}

		// Token: 0x17001C18 RID: 7192
		// (get) Token: 0x060042D9 RID: 17113 RVA: 0x00118F53 File Offset: 0x00117153
		// (set) Token: 0x060042DA RID: 17114 RVA: 0x00118F5B File Offset: 0x0011715B
		internal ExpressionInfo HangingIndent
		{
			get
			{
				return this.m_hangingIndent;
			}
			set
			{
				this.m_hangingIndent = value;
			}
		}

		// Token: 0x17001C19 RID: 7193
		// (get) Token: 0x060042DB RID: 17115 RVA: 0x00118F64 File Offset: 0x00117164
		// (set) Token: 0x060042DC RID: 17116 RVA: 0x00118F6C File Offset: 0x0011716C
		internal ExpressionInfo SpaceBefore
		{
			get
			{
				return this.m_spaceBefore;
			}
			set
			{
				this.m_spaceBefore = value;
			}
		}

		// Token: 0x17001C1A RID: 7194
		// (get) Token: 0x060042DD RID: 17117 RVA: 0x00118F75 File Offset: 0x00117175
		// (set) Token: 0x060042DE RID: 17118 RVA: 0x00118F7D File Offset: 0x0011717D
		internal ExpressionInfo SpaceAfter
		{
			get
			{
				return this.m_spaceAfter;
			}
			set
			{
				this.m_spaceAfter = value;
			}
		}

		// Token: 0x17001C1B RID: 7195
		// (get) Token: 0x060042DF RID: 17119 RVA: 0x00118F86 File Offset: 0x00117186
		// (set) Token: 0x060042E0 RID: 17120 RVA: 0x00118F8E File Offset: 0x0011718E
		internal ExpressionInfo ListStyle
		{
			get
			{
				return this.m_listStyle;
			}
			set
			{
				this.m_listStyle = value;
			}
		}

		// Token: 0x17001C1C RID: 7196
		// (get) Token: 0x060042E1 RID: 17121 RVA: 0x00118F97 File Offset: 0x00117197
		// (set) Token: 0x060042E2 RID: 17122 RVA: 0x00118F9F File Offset: 0x0011719F
		internal ExpressionInfo ListLevel
		{
			get
			{
				return this.m_listLevel;
			}
			set
			{
				this.m_listLevel = value;
			}
		}

		// Token: 0x17001C1D RID: 7197
		// (get) Token: 0x060042E3 RID: 17123 RVA: 0x00118FA8 File Offset: 0x001171A8
		// (set) Token: 0x060042E4 RID: 17124 RVA: 0x00118FB0 File Offset: 0x001171B0
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

		// Token: 0x17001C1E RID: 7198
		// (get) Token: 0x060042E5 RID: 17125 RVA: 0x00118FB9 File Offset: 0x001171B9
		// (set) Token: 0x060042E6 RID: 17126 RVA: 0x00118FC1 File Offset: 0x001171C1
		internal Microsoft.ReportingServices.ReportIntermediateFormat.TextBox TextBox
		{
			get
			{
				return this.m_textBox;
			}
			set
			{
				this.m_textBox = value;
			}
		}

		// Token: 0x17001C1F RID: 7199
		// (get) Token: 0x060042E7 RID: 17127 RVA: 0x00118FCA File Offset: 0x001171CA
		// (set) Token: 0x060042E8 RID: 17128 RVA: 0x00118FD2 File Offset: 0x001171D2
		internal bool TextRunValueReferenced
		{
			get
			{
				return this.m_textRunValueReferenced;
			}
			set
			{
				this.m_textRunValueReferenced = value;
			}
		}

		// Token: 0x17001C20 RID: 7200
		// (get) Token: 0x060042E9 RID: 17129 RVA: 0x00118FDB File Offset: 0x001171DB
		IInstancePath IStyleContainer.InstancePath
		{
			get
			{
				return this.m_textBox;
			}
		}

		// Token: 0x17001C21 RID: 7201
		// (get) Token: 0x060042EA RID: 17130 RVA: 0x00118FE3 File Offset: 0x001171E3
		Microsoft.ReportingServices.ReportIntermediateFormat.Style IStyleContainer.StyleClass
		{
			get
			{
				return this.m_styleClass;
			}
		}

		// Token: 0x17001C22 RID: 7202
		// (get) Token: 0x060042EB RID: 17131 RVA: 0x00118FEB File Offset: 0x001171EB
		public Microsoft.ReportingServices.ReportProcessing.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.Paragraph;
			}
		}

		// Token: 0x17001C23 RID: 7203
		// (get) Token: 0x060042EC RID: 17132 RVA: 0x00118FEF File Offset: 0x001171EF
		public string Name
		{
			get
			{
				if (this.m_name == null)
				{
					this.m_name = this.m_textBox.Name + ".Paragraphs[" + this.m_indexInCollection.ToString(CultureInfo.InvariantCulture) + "]";
				}
				return this.m_name;
			}
		}

		// Token: 0x17001C24 RID: 7204
		// (get) Token: 0x060042ED RID: 17133 RVA: 0x0011902F File Offset: 0x0011722F
		internal ParagraphExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x060042EE RID: 17134 RVA: 0x00119038 File Offset: 0x00117238
		internal bool Initialize(InitializationContext context, out bool aHasExpressionBasedValue)
		{
			bool flag = false;
			bool flag2 = false;
			aHasExpressionBasedValue = false;
			context.ExprHostBuilder.ParagraphStart(this.m_indexInCollection);
			if (this.m_textRuns != null)
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.TextRun textRun in this.m_textRuns)
				{
					flag |= textRun.Initialize(context, out flag2);
					aHasExpressionBasedValue = aHasExpressionBasedValue || flag2;
				}
			}
			if (this.m_styleClass != null)
			{
				this.m_styleClass.Initialize(context);
			}
			if (this.m_leftIndent != null)
			{
				this.m_leftIndent.Initialize("LeftIndent", context);
				context.ExprHostBuilder.ParagraphLeftIndent(this.m_leftIndent);
			}
			if (this.m_rightIndent != null)
			{
				this.m_rightIndent.Initialize("RightIndent", context);
				context.ExprHostBuilder.ParagraphRightIndent(this.m_rightIndent);
			}
			if (this.m_hangingIndent != null)
			{
				this.m_hangingIndent.Initialize("HangingIndent", context);
				context.ExprHostBuilder.ParagraphHangingIndent(this.m_hangingIndent);
			}
			if (this.m_spaceBefore != null)
			{
				this.m_spaceBefore.Initialize("SpaceBefore", context);
				context.ExprHostBuilder.ParagraphSpaceBefore(this.m_spaceBefore);
			}
			if (this.m_spaceAfter != null)
			{
				this.m_spaceAfter.Initialize("SpaceAfter", context);
				context.ExprHostBuilder.ParagraphSpaceAfter(this.m_spaceAfter);
			}
			if (this.m_listStyle != null)
			{
				this.m_listStyle.Initialize("ListStyle", context);
				context.ExprHostBuilder.ParagraphListStyle(this.m_listStyle);
			}
			if (this.m_listLevel != null)
			{
				this.m_listLevel.Initialize("ListLevel", context);
				context.ExprHostBuilder.ParagraphListLevel(this.m_listLevel);
			}
			this.m_exprHostID = context.ExprHostBuilder.ParagraphEnd();
			return flag;
		}

		// Token: 0x060042EF RID: 17135 RVA: 0x0011920C File Offset: 0x0011740C
		internal override object PublishClone(AutomaticSubtotalContext context)
		{
			Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph paragraph = (Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph)base.PublishClone(context);
			if (this.m_textRuns != null)
			{
				paragraph.m_textRuns = new List<Microsoft.ReportingServices.ReportIntermediateFormat.TextRun>(this.m_textRuns.Count);
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.TextRun textRun in this.m_textRuns)
				{
					Microsoft.ReportingServices.ReportIntermediateFormat.TextRun textRun2 = (Microsoft.ReportingServices.ReportIntermediateFormat.TextRun)textRun.PublishClone(context);
					textRun2.Paragraph = paragraph;
					paragraph.m_textRuns.Add(textRun2);
				}
			}
			if (this.m_styleClass != null)
			{
				paragraph.m_styleClass = (Microsoft.ReportingServices.ReportIntermediateFormat.Style)this.m_styleClass.PublishClone(context);
			}
			if (this.m_leftIndent != null)
			{
				paragraph.m_leftIndent = (ExpressionInfo)this.m_leftIndent.PublishClone(context);
			}
			if (this.m_rightIndent != null)
			{
				paragraph.m_rightIndent = (ExpressionInfo)this.m_rightIndent.PublishClone(context);
			}
			if (this.m_hangingIndent != null)
			{
				paragraph.m_hangingIndent = (ExpressionInfo)this.m_hangingIndent.PublishClone(context);
			}
			if (this.m_spaceBefore != null)
			{
				paragraph.m_spaceBefore = (ExpressionInfo)this.m_spaceBefore.PublishClone(context);
			}
			if (this.m_spaceAfter != null)
			{
				paragraph.m_spaceAfter = (ExpressionInfo)this.m_spaceAfter.PublishClone(context);
			}
			if (this.m_listStyle != null)
			{
				paragraph.m_listStyle = (ExpressionInfo)this.m_listStyle.PublishClone(context);
			}
			if (this.m_listLevel != null)
			{
				paragraph.m_listLevel = (ExpressionInfo)this.m_listLevel.PublishClone(context);
			}
			return paragraph;
		}

		// Token: 0x060042F0 RID: 17136 RVA: 0x0011939C File Offset: 0x0011759C
		internal bool DetermineSimplicity()
		{
			return this.m_textRuns.Count == 1 && this.m_listLevel == null && this.m_listStyle == null && this.m_leftIndent == null && this.m_rightIndent == null && this.m_hangingIndent == null && this.m_spaceBefore == null && this.m_spaceAfter == null && this.m_textRuns[0].DetermineSimplicity();
		}

		// Token: 0x060042F1 RID: 17137 RVA: 0x00119404 File Offset: 0x00117604
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Paragraph, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IDOwner, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Style, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Style),
				new MemberInfo(MemberName.TextRuns, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TextRun),
				new MemberInfo(MemberName.LeftIndent, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.RightIndent, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.HangingIndent, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.SpaceBefore, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.SpaceAfter, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ListStyle, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ListLevel, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.IndexInCollection, Token.Int32),
				new MemberInfo(MemberName.TextBox, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.TextBox, Token.Reference),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.TextRunValueReferenced, Token.Boolean)
			});
		}

		// Token: 0x060042F2 RID: 17138 RVA: 0x0011953C File Offset: 0x0011773C
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.IndexInCollection)
				{
					if (memberName == MemberName.ExprHostID)
					{
						writer.Write(this.m_exprHostID);
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
					switch (memberName)
					{
					case MemberName.TextBox:
						writer.WriteReference(this.m_textBox);
						continue;
					case MemberName.TextRuns:
						writer.Write<Microsoft.ReportingServices.ReportIntermediateFormat.TextRun>(this.m_textRuns);
						continue;
					case MemberName.LeftIndent:
						writer.Write(this.m_leftIndent);
						continue;
					case MemberName.RightIndent:
						writer.Write(this.m_rightIndent);
						continue;
					case MemberName.HangingIndent:
						writer.Write(this.m_hangingIndent);
						continue;
					case MemberName.SpaceBefore:
						writer.Write(this.m_spaceBefore);
						continue;
					case MemberName.SpaceAfter:
						writer.Write(this.m_spaceAfter);
						continue;
					case MemberName.ListStyle:
						writer.Write(this.m_listStyle);
						continue;
					case MemberName.ListLevel:
						writer.Write(this.m_listLevel);
						continue;
					case MemberName.TextRunValueReferenced:
						writer.Write(this.m_textRunValueReferenced);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060042F3 RID: 17139 RVA: 0x001196CC File Offset: 0x001178CC
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.IndexInCollection)
				{
					if (memberName == MemberName.ExprHostID)
					{
						this.m_exprHostID = reader.ReadInt32();
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
					switch (memberName)
					{
					case MemberName.TextBox:
						this.m_textBox = reader.ReadReference<Microsoft.ReportingServices.ReportIntermediateFormat.TextBox>(this);
						continue;
					case MemberName.TextRuns:
						this.m_textRuns = reader.ReadGenericListOfRIFObjects<Microsoft.ReportingServices.ReportIntermediateFormat.TextRun>();
						continue;
					case MemberName.LeftIndent:
						this.m_leftIndent = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.RightIndent:
						this.m_rightIndent = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.HangingIndent:
						this.m_hangingIndent = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.SpaceBefore:
						this.m_spaceBefore = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.SpaceAfter:
						this.m_spaceAfter = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.ListStyle:
						this.m_listStyle = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.ListLevel:
						this.m_listLevel = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.TextRunValueReferenced:
						this.m_textRunValueReferenced = reader.ReadBoolean();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x060042F4 RID: 17140 RVA: 0x00119888 File Offset: 0x00117A88
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.TextBox)
					{
						Global.Tracer.Assert(referenceableItems.ContainsKey(memberReference.RefID));
						Global.Tracer.Assert(referenceableItems[memberReference.RefID] is Microsoft.ReportingServices.ReportIntermediateFormat.TextBox);
						this.m_textBox = (Microsoft.ReportingServices.ReportIntermediateFormat.TextBox)referenceableItems[memberReference.RefID];
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x060042F5 RID: 17141 RVA: 0x0011994C File Offset: 0x00117B4C
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Paragraph;
		}

		// Token: 0x060042F6 RID: 17142 RVA: 0x00119954 File Offset: 0x00117B54
		internal void SetExprHost(TextBoxExprHost textBoxExprHost, ObjectModelImpl reportObjectModel)
		{
			if (this.m_exprHostID >= 0)
			{
				this.m_exprHost = textBoxExprHost.ParagraphHostsRemotable[this.m_exprHostID];
				Global.Tracer.Assert(this.m_exprHost != null && reportObjectModel != null);
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
				if (this.m_styleClass != null)
				{
					this.m_styleClass.SetStyleExprHost(this.m_exprHost);
				}
				if (this.m_textRuns == null)
				{
					return;
				}
				using (List<Microsoft.ReportingServices.ReportIntermediateFormat.TextRun>.Enumerator enumerator = this.m_textRuns.GetEnumerator())
				{
					while (enumerator.MoveNext())
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.TextRun textRun = enumerator.Current;
						textRun.SetExprHost(this.m_exprHost, reportObjectModel);
					}
					return;
				}
			}
			if (this.m_ID == -1)
			{
				if (this.m_styleClass != null)
				{
					this.m_styleClass.SetStyleExprHost(textBoxExprHost);
					this.m_textRuns[0].StyleClass.SetStyleExprHost(textBoxExprHost);
				}
				this.m_textRuns[0].SetExprHost(new Microsoft.ReportingServices.RdlExpressions.ReportRuntime.TextRunExprHostWrapper(textBoxExprHost));
			}
		}

		// Token: 0x060042F7 RID: 17143 RVA: 0x00119A64 File Offset: 0x00117C64
		internal string EvaluateSpaceAfter(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_textBox, instance);
			return context.ReportRuntime.EvaluateParagraphSpaceAfterExpression(this);
		}

		// Token: 0x060042F8 RID: 17144 RVA: 0x00119A7F File Offset: 0x00117C7F
		internal string EvaluateSpaceBefore(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_textBox, instance);
			return context.ReportRuntime.EvaluateParagraphSpaceBeforeExpression(this);
		}

		// Token: 0x060042F9 RID: 17145 RVA: 0x00119A9A File Offset: 0x00117C9A
		internal string EvaluateListStyle(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_textBox, instance);
			return context.ReportRuntime.EvaluateParagraphListStyleExpression(this);
		}

		// Token: 0x060042FA RID: 17146 RVA: 0x00119AB5 File Offset: 0x00117CB5
		internal int? EvaluateListLevel(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_textBox, instance);
			return context.ReportRuntime.EvaluateParagraphListLevelExpression(this);
		}

		// Token: 0x060042FB RID: 17147 RVA: 0x00119AD0 File Offset: 0x00117CD0
		internal string EvaluateLeftIndent(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_textBox, instance);
			return context.ReportRuntime.EvaluateParagraphLeftIndentExpression(this);
		}

		// Token: 0x060042FC RID: 17148 RVA: 0x00119AEB File Offset: 0x00117CEB
		internal string EvaluateRightIndent(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_textBox, instance);
			return context.ReportRuntime.EvaluateParagraphRightIndentExpression(this);
		}

		// Token: 0x060042FD RID: 17149 RVA: 0x00119B06 File Offset: 0x00117D06
		internal string EvaluateHangingIndent(IReportScopeInstance instance, OnDemandProcessingContext context)
		{
			context.SetupContext(this.m_textBox, instance);
			return context.ReportRuntime.EvaluateParagraphHangingIndentExpression(this);
		}

		// Token: 0x060042FE RID: 17150 RVA: 0x00119B21 File Offset: 0x00117D21
		internal ParagraphImpl GetParagraphImpl(OnDemandProcessingContext context)
		{
			if (this.m_paragraphImpl == null)
			{
				this.m_paragraphImpl = (ParagraphImpl)this.m_textBox.GetTextBoxImpl(context).Paragraphs[this.m_indexInCollection];
			}
			return this.m_paragraphImpl;
		}

		// Token: 0x04001E83 RID: 7811
		private List<Microsoft.ReportingServices.ReportIntermediateFormat.TextRun> m_textRuns;

		// Token: 0x04001E84 RID: 7812
		private Microsoft.ReportingServices.ReportIntermediateFormat.Style m_styleClass;

		// Token: 0x04001E85 RID: 7813
		private ExpressionInfo m_leftIndent;

		// Token: 0x04001E86 RID: 7814
		private ExpressionInfo m_rightIndent;

		// Token: 0x04001E87 RID: 7815
		private ExpressionInfo m_hangingIndent;

		// Token: 0x04001E88 RID: 7816
		private ExpressionInfo m_spaceBefore;

		// Token: 0x04001E89 RID: 7817
		private ExpressionInfo m_spaceAfter;

		// Token: 0x04001E8A RID: 7818
		private ExpressionInfo m_listLevel;

		// Token: 0x04001E8B RID: 7819
		private ExpressionInfo m_listStyle;

		// Token: 0x04001E8C RID: 7820
		private int m_indexInCollection;

		// Token: 0x04001E8D RID: 7821
		private int m_exprHostID = -1;

		// Token: 0x04001E8E RID: 7822
		private bool m_textRunValueReferenced;

		// Token: 0x04001E8F RID: 7823
		[Reference]
		private Microsoft.ReportingServices.ReportIntermediateFormat.TextBox m_textBox;

		// Token: 0x04001E90 RID: 7824
		[NonSerialized]
		private string m_idString;

		// Token: 0x04001E91 RID: 7825
		[NonSerialized]
		private ParagraphImpl m_paragraphImpl;

		// Token: 0x04001E92 RID: 7826
		[NonSerialized]
		private string m_name;

		// Token: 0x04001E93 RID: 7827
		[NonSerialized]
		private ParagraphExprHost m_exprHost;

		// Token: 0x04001E94 RID: 7828
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Microsoft.ReportingServices.ReportIntermediateFormat.Paragraph.GetDeclaration();
	}
}
