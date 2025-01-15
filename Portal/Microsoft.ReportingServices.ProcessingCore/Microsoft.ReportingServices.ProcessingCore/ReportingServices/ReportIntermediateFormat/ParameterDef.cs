using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.Diagnostics.Utilities;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004F0 RID: 1264
	[Serializable]
	internal sealed class ParameterDef : ParameterBase, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IParameterDef, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable
	{
		// Token: 0x06004030 RID: 16432 RVA: 0x0010F0AA File Offset: 0x0010D2AA
		internal ParameterDef()
		{
		}

		// Token: 0x06004031 RID: 16433 RVA: 0x0010F0C1 File Offset: 0x0010D2C1
		internal ParameterDef(int referenceId)
		{
			this.m_referenceId = referenceId;
		}

		// Token: 0x17001B15 RID: 6933
		// (get) Token: 0x06004032 RID: 16434 RVA: 0x0010F0DF File Offset: 0x0010D2DF
		int IParameterDef.DefaultValuesExpressionCount
		{
			get
			{
				if (this.DefaultExpressions == null)
				{
					return 0;
				}
				return this.DefaultExpressions.Count;
			}
		}

		// Token: 0x17001B16 RID: 6934
		// (get) Token: 0x06004033 RID: 16435 RVA: 0x0010F0F6 File Offset: 0x0010D2F6
		int IParameterDef.ValidValuesValueExpressionCount
		{
			get
			{
				if (this.ValidValuesValueExpressions == null)
				{
					return 0;
				}
				return this.ValidValuesValueExpressions.Count;
			}
		}

		// Token: 0x17001B17 RID: 6935
		// (get) Token: 0x06004034 RID: 16436 RVA: 0x0010F10D File Offset: 0x0010D30D
		int IParameterDef.ValidValuesLabelExpressionCount
		{
			get
			{
				if (this.ValidValuesLabelExpressions == null)
				{
					return 0;
				}
				return this.ValidValuesLabelExpressions.Count;
			}
		}

		// Token: 0x06004035 RID: 16437 RVA: 0x0010F124 File Offset: 0x0010D324
		bool IParameterDef.HasDefaultValuesExpressions()
		{
			return this.DefaultExpressions != null;
		}

		// Token: 0x06004036 RID: 16438 RVA: 0x0010F12F File Offset: 0x0010D32F
		bool IParameterDef.HasValidValuesLabelExpressions()
		{
			return this.ValidValuesLabelExpressions != null;
		}

		// Token: 0x06004037 RID: 16439 RVA: 0x0010F13A File Offset: 0x0010D33A
		bool IParameterDef.HasValidValuesValueExpressions()
		{
			return this.ValidValuesValueExpressions != null;
		}

		// Token: 0x06004038 RID: 16440 RVA: 0x0010F145 File Offset: 0x0010D345
		bool IParameterDef.HasDefaultValuesDataSource()
		{
			return this.DefaultDataSource != null;
		}

		// Token: 0x06004039 RID: 16441 RVA: 0x0010F150 File Offset: 0x0010D350
		bool IParameterDef.HasValidValuesDataSource()
		{
			return this.ValidValuesDataSource != null;
		}

		// Token: 0x17001B18 RID: 6936
		// (get) Token: 0x0600403A RID: 16442 RVA: 0x0010F15B File Offset: 0x0010D35B
		string IParameterDef.Name
		{
			get
			{
				return base.Name;
			}
		}

		// Token: 0x17001B19 RID: 6937
		// (get) Token: 0x0600403B RID: 16443 RVA: 0x0010F163 File Offset: 0x0010D363
		Microsoft.ReportingServices.ReportProcessing.ObjectType IParameterDef.ParameterObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter;
			}
		}

		// Token: 0x17001B1A RID: 6938
		// (get) Token: 0x0600403C RID: 16444 RVA: 0x0010F167 File Offset: 0x0010D367
		DataType IParameterDef.DataType
		{
			get
			{
				return base.DataType;
			}
		}

		// Token: 0x0600403D RID: 16445 RVA: 0x0010F16F File Offset: 0x0010D36F
		bool IParameterDef.ValidateValueForNull(object newValue, ErrorContext errorContext, string parameterValueProperty)
		{
			return ParameterBase.ValidateValueForNull(newValue, base.Nullable, errorContext, Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter, base.Name, parameterValueProperty);
		}

		// Token: 0x0600403E RID: 16446 RVA: 0x0010F187 File Offset: 0x0010D387
		bool IParameterDef.ValidateValueForBlank(object newValue, ErrorContext errorContext, string parameterValueProperty)
		{
			return base.ValidateValueForBlank(newValue, errorContext, parameterValueProperty);
		}

		// Token: 0x17001B1B RID: 6939
		// (get) Token: 0x0600403F RID: 16447 RVA: 0x0010F192 File Offset: 0x0010D392
		bool IParameterDef.MultiValue
		{
			get
			{
				return base.MultiValue;
			}
		}

		// Token: 0x17001B1C RID: 6940
		// (get) Token: 0x06004040 RID: 16448 RVA: 0x0010F19A File Offset: 0x0010D39A
		IParameterDataSource IParameterDef.DefaultDataSource
		{
			get
			{
				return this.DefaultDataSource;
			}
		}

		// Token: 0x17001B1D RID: 6941
		// (get) Token: 0x06004041 RID: 16449 RVA: 0x0010F1A2 File Offset: 0x0010D3A2
		IParameterDataSource IParameterDef.ValidValuesDataSource
		{
			get
			{
				return this.ValidValuesDataSource;
			}
		}

		// Token: 0x17001B1E RID: 6942
		// (get) Token: 0x06004042 RID: 16450 RVA: 0x0010F1AA File Offset: 0x0010D3AA
		// (set) Token: 0x06004043 RID: 16451 RVA: 0x0010F1B7 File Offset: 0x0010D3B7
		public override string Prompt
		{
			get
			{
				return this.m_prompt.StringValue;
			}
			set
			{
				this.m_prompt = ExpressionInfo.CreateConstExpression(value);
			}
		}

		// Token: 0x17001B1F RID: 6943
		// (get) Token: 0x06004044 RID: 16452 RVA: 0x0010F1C5 File Offset: 0x0010D3C5
		// (set) Token: 0x06004045 RID: 16453 RVA: 0x0010F1CD File Offset: 0x0010D3CD
		public ExpressionInfo PromptExpression
		{
			get
			{
				return this.m_prompt;
			}
			set
			{
				this.m_prompt = value;
			}
		}

		// Token: 0x17001B20 RID: 6944
		// (get) Token: 0x06004046 RID: 16454 RVA: 0x0010F1D6 File Offset: 0x0010D3D6
		// (set) Token: 0x06004047 RID: 16455 RVA: 0x0010F1DE File Offset: 0x0010D3DE
		internal List<ExpressionInfo> DefaultExpressions
		{
			get
			{
				return this.m_defaultExpressions;
			}
			set
			{
				this.m_defaultExpressions = value;
			}
		}

		// Token: 0x17001B21 RID: 6945
		// (get) Token: 0x06004048 RID: 16456 RVA: 0x0010F1E7 File Offset: 0x0010D3E7
		// (set) Token: 0x06004049 RID: 16457 RVA: 0x0010F1EF File Offset: 0x0010D3EF
		internal ParameterDataSource ValidValuesDataSource
		{
			get
			{
				return this.m_validValuesDataSource;
			}
			set
			{
				this.m_validValuesDataSource = value;
			}
		}

		// Token: 0x17001B22 RID: 6946
		// (get) Token: 0x0600404A RID: 16458 RVA: 0x0010F1F8 File Offset: 0x0010D3F8
		// (set) Token: 0x0600404B RID: 16459 RVA: 0x0010F200 File Offset: 0x0010D400
		internal List<ExpressionInfo> ValidValuesValueExpressions
		{
			get
			{
				return this.m_validValuesValueExpressions;
			}
			set
			{
				this.m_validValuesValueExpressions = value;
			}
		}

		// Token: 0x17001B23 RID: 6947
		// (get) Token: 0x0600404C RID: 16460 RVA: 0x0010F209 File Offset: 0x0010D409
		// (set) Token: 0x0600404D RID: 16461 RVA: 0x0010F211 File Offset: 0x0010D411
		internal List<ExpressionInfo> ValidValuesLabelExpressions
		{
			get
			{
				return this.m_validValuesLabelExpressions;
			}
			set
			{
				this.m_validValuesLabelExpressions = value;
			}
		}

		// Token: 0x17001B24 RID: 6948
		// (get) Token: 0x0600404E RID: 16462 RVA: 0x0010F21A File Offset: 0x0010D41A
		// (set) Token: 0x0600404F RID: 16463 RVA: 0x0010F222 File Offset: 0x0010D422
		internal ParameterDataSource DefaultDataSource
		{
			get
			{
				return this.m_defaultDataSource;
			}
			set
			{
				this.m_defaultDataSource = value;
			}
		}

		// Token: 0x17001B25 RID: 6949
		// (get) Token: 0x06004050 RID: 16464 RVA: 0x0010F22B File Offset: 0x0010D42B
		// (set) Token: 0x06004051 RID: 16465 RVA: 0x0010F233 File Offset: 0x0010D433
		internal List<ParameterDef> DependencyList
		{
			get
			{
				return this.m_dependencyList;
			}
			set
			{
				this.m_dependencyList = value;
			}
		}

		// Token: 0x17001B26 RID: 6950
		// (get) Token: 0x06004052 RID: 16466 RVA: 0x0010F23C File Offset: 0x0010D43C
		// (set) Token: 0x06004053 RID: 16467 RVA: 0x0010F244 File Offset: 0x0010D444
		internal int ExprHostID
		{
			get
			{
				return this.m_exprHostID;
			}
			set
			{
				this.m_exprHostID = value;
			}
		}

		// Token: 0x17001B27 RID: 6951
		// (get) Token: 0x06004054 RID: 16468 RVA: 0x0010F24D File Offset: 0x0010D44D
		internal ReportParamExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06004055 RID: 16469 RVA: 0x0010F258 File Offset: 0x0010D458
		internal void Initialize(InitializationContext context)
		{
			context.Location |= Microsoft.ReportingServices.ReportPublishing.LocationFlags.InParameter;
			context.ExprHostBuilder.ReportParameterStart(base.Name);
			if (this.m_defaultExpressions != null)
			{
				for (int i = this.m_defaultExpressions.Count - 1; i >= 0; i--)
				{
					context.ExprHostBuilder.ReportParameterDefaultValue(this.m_defaultExpressions[i]);
				}
			}
			if (this.m_validValuesValueExpressions != null)
			{
				context.ExprHostBuilder.ReportParameterValidValuesStart();
				for (int j = this.m_validValuesValueExpressions.Count - 1; j >= 0; j--)
				{
					ExpressionInfo expressionInfo = this.m_validValuesValueExpressions[j];
					if (expressionInfo != null)
					{
						context.ExprHostBuilder.ReportParameterValidValue(expressionInfo);
					}
				}
				context.ExprHostBuilder.ReportParameterValidValuesEnd();
			}
			if (this.m_validValuesLabelExpressions != null)
			{
				context.ExprHostBuilder.ReportParameterValidValueLabelsStart();
				for (int k = this.m_validValuesLabelExpressions.Count - 1; k >= 0; k--)
				{
					ExpressionInfo expressionInfo2 = this.m_validValuesLabelExpressions[k];
					if (expressionInfo2 != null)
					{
						context.ExprHostBuilder.ReportParameterValidValueLabel(expressionInfo2);
					}
				}
				context.ExprHostBuilder.ReportParameterValidValueLabelsEnd();
			}
			if (this.m_prompt != null)
			{
				context.ExprHostBuilder.ReportParameterPromptExpression(this.m_prompt);
			}
			this.ExprHostID = context.ExprHostBuilder.ReportParameterEnd();
		}

		// Token: 0x06004056 RID: 16470 RVA: 0x0010F39C File Offset: 0x0010D59C
		internal void SetExprHost(ReportExprHost reportExprHost, OnDemandObjectModel reportObjectModel)
		{
			Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
			if (this.ExprHostID >= 0)
			{
				this.m_exprHost = reportExprHost.ReportParameterHostsRemotable[this.ExprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
				if (this.m_exprHost.ValidValuesHost != null)
				{
					this.m_exprHost.ValidValuesHost.SetReportObjectModel(reportObjectModel);
				}
				if (this.m_exprHost.ValidValueLabelsHost != null)
				{
					this.m_exprHost.ValidValueLabelsHost.SetReportObjectModel(reportObjectModel);
				}
			}
		}

		// Token: 0x06004057 RID: 16471 RVA: 0x0010F42C File Offset: 0x0010D62C
		internal void Parse(string name, List<string> defaultValues, string type, string nullable, ExpressionInfo prompt, string promptUser, string allowBlank, string multiValue, string usedInQuery, bool hidden, ErrorContext errorContext, CultureInfo language, string useAllValidValues)
		{
			base.Parse(name, defaultValues, type, nullable, prompt, promptUser, allowBlank, multiValue, usedInQuery, hidden, errorContext, language, useAllValidValues);
			if (hidden)
			{
				this.m_prompt = ExpressionInfo.CreateConstExpression("");
			}
			else if (prompt == null)
			{
				this.m_prompt = ExpressionInfo.CreateConstExpression(name + ":");
			}
			else
			{
				this.m_prompt = prompt;
			}
			this.ValidateExpressionDataTypes(this.m_validValuesValueExpressions, errorContext, name, "ValidValue", true, language);
			this.ValidateExpressionDataTypes(this.m_defaultExpressions, errorContext, name, "DefaultValue", false, language);
		}

		// Token: 0x06004058 RID: 16472 RVA: 0x0010F4C0 File Offset: 0x0010D6C0
		private void ValidateExpressionDataTypes(List<ExpressionInfo> expressions, ErrorContext errorContext, string paramName, string memberName, bool fromValidValues, CultureInfo language)
		{
			if (expressions == null)
			{
				return;
			}
			for (int i = expressions.Count - 1; i >= 0; i--)
			{
				ExpressionInfo expressionInfo = expressions[i];
				if (fromValidValues && expressionInfo == null && base.MultiValue)
				{
					this.m_validValuesValueExpressions.RemoveAt(i);
				}
				else if (expressionInfo != null && ExpressionInfo.Types.Constant == expressionInfo.Type)
				{
					object obj;
					if (!ParameterBase.CastFromString(expressionInfo.StringValue, out obj, base.DataType, language))
					{
						if (errorContext == null)
						{
							throw new ReportParameterTypeMismatchException(paramName);
						}
						errorContext.Register(ProcessingErrorCode.rsParameterPropertyTypeMismatch, Severity.Error, Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter, paramName, memberName, Array.Empty<string>());
					}
					else
					{
						base.ValidateValue(obj, errorContext, Microsoft.ReportingServices.ReportProcessing.ObjectType.ReportParameter, memberName);
						if (obj != null && base.DataType != DataType.String)
						{
							ExpressionInfo expressionInfo2 = new ExpressionInfo();
							expressionInfo2.Type = ExpressionInfo.Types.Constant;
							expressionInfo2.OriginalText = expressionInfo.OriginalText;
							expressionInfo2.ConstantType = base.DataType;
							expressions[i] = expressionInfo2;
							DataType dataType = base.DataType;
							if (dataType <= DataType.Integer)
							{
								if (dataType != DataType.Boolean)
								{
									if (dataType == DataType.Integer)
									{
										expressionInfo2.IntValue = (int)obj;
									}
								}
								else
								{
									expressionInfo2.BoolValue = (bool)obj;
								}
							}
							else if (dataType != DataType.Float)
							{
								if (dataType == DataType.DateTime)
								{
									if (obj is DateTimeOffset)
									{
										expressionInfo2.SetDateTimeValue((DateTimeOffset)obj);
									}
									else
									{
										expressionInfo2.SetDateTimeValue((DateTime)obj);
									}
								}
							}
							else
							{
								expressionInfo2.FloatValue = (double)obj;
							}
						}
					}
				}
			}
		}

		// Token: 0x06004059 RID: 16473 RVA: 0x0010F624 File Offset: 0x0010D824
		[SkipMemberStaticValidation(MemberName.DependencyList)]
		private new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterDef, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterBase, new List<MemberInfo>
			{
				new MemberInfo(MemberName.ValidValuesDataSource, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterDataSource),
				new MemberInfo(MemberName.ValidValuesValueExpression, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ValidValuesLabelExpression, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DefaultValueDataSource, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterDataSource),
				new MemberInfo(MemberName.ExpressionList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.DependencyList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterDef),
				new MemberInfo(MemberName.DependencyRefList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Token.Reference, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterDef),
				new MemberInfo(MemberName.ExprHostID, Token.Int32),
				new MemberInfo(MemberName.Prompt, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.ReferenceID, Token.Int32)
			});
		}

		// Token: 0x0600405A RID: 16474 RVA: 0x0010F720 File Offset: 0x0010D920
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			base.Serialize(writer);
			writer.RegisterDeclaration(ParameterDef.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.DefaultValueDataSource)
				{
					if (memberName == MemberName.ExpressionList)
					{
						writer.Write<ExpressionInfo>(this.m_defaultExpressions);
						continue;
					}
					if (memberName == MemberName.Prompt)
					{
						writer.Write(this.m_prompt);
						continue;
					}
					switch (memberName)
					{
					case MemberName.ValidValuesDataSource:
						writer.Write(this.m_validValuesDataSource);
						continue;
					case MemberName.ValidValuesValueExpression:
						writer.Write<ExpressionInfo>(this.m_validValuesValueExpressions);
						continue;
					case MemberName.ValidValuesLabelExpression:
						writer.Write<ExpressionInfo>(this.m_validValuesLabelExpressions);
						continue;
					case MemberName.DefaultValueDataSource:
						writer.Write(this.m_defaultDataSource);
						continue;
					}
				}
				else if (memberName <= MemberName.ExprHostID)
				{
					if (memberName == MemberName.DependencyList)
					{
						writer.Write<ParameterDef>(null);
						continue;
					}
					if (memberName == MemberName.ExprHostID)
					{
						writer.Write(this.m_exprHostID);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.ReferenceID)
					{
						writer.Write(this.m_referenceId);
						continue;
					}
					if (memberName == MemberName.DependencyRefList)
					{
						writer.WriteListOfReferences(this.m_dependencyList);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600405B RID: 16475 RVA: 0x0010F874 File Offset: 0x0010DA74
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			base.Deserialize(reader);
			reader.RegisterDeclaration(ParameterDef.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.DefaultValueDataSource)
				{
					if (memberName == MemberName.ExpressionList)
					{
						this.m_defaultExpressions = reader.ReadGenericListOfRIFObjects<ExpressionInfo>();
						continue;
					}
					if (memberName == MemberName.Prompt)
					{
						this.m_prompt = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					switch (memberName)
					{
					case MemberName.ValidValuesDataSource:
						this.m_validValuesDataSource = (ParameterDataSource)reader.ReadRIFObject();
						continue;
					case MemberName.ValidValuesValueExpression:
						this.m_validValuesValueExpressions = reader.ReadGenericListOfRIFObjects<ExpressionInfo>();
						continue;
					case MemberName.ValidValuesLabelExpression:
						this.m_validValuesLabelExpressions = reader.ReadGenericListOfRIFObjects<ExpressionInfo>();
						continue;
					case MemberName.DefaultValueDataSource:
						this.m_defaultDataSource = (ParameterDataSource)reader.ReadRIFObject();
						continue;
					}
				}
				else if (memberName <= MemberName.ExprHostID)
				{
					if (memberName != MemberName.DependencyList)
					{
						if (memberName == MemberName.ExprHostID)
						{
							this.m_exprHostID = reader.ReadInt32();
							continue;
						}
					}
					else
					{
						List<ParameterDef> list = reader.ReadGenericListOfRIFObjects<ParameterDef>();
						if (list != null)
						{
							this.m_dependencyList = list;
							continue;
						}
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.ReferenceID)
					{
						this.m_referenceId = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.DependencyRefList)
					{
						this.m_dependencyList = reader.ReadGenericListOfReferences<ParameterDef>(this);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x0600405C RID: 16476 RVA: 0x0010F9E8 File Offset: 0x0010DBE8
		void Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable.ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference> list;
			if (memberReferencesCollection.TryGetValue(ParameterDef.m_Declaration.ObjectType, out list))
			{
				foreach (Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference memberReference in list)
				{
					if (memberReference.MemberName == MemberName.DependencyRefList)
					{
						Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable referenceable;
						referenceableItems.TryGetValue(memberReference.RefID, out referenceable);
						ParameterDef parameterDef = referenceable as ParameterDef;
						if (this.m_dependencyList == null)
						{
							this.m_dependencyList = new List<ParameterDef>();
						}
						this.m_dependencyList.Add(parameterDef);
					}
					else
					{
						Global.Tracer.Assert(false);
					}
				}
			}
		}

		// Token: 0x0600405D RID: 16477 RVA: 0x0010FA94 File Offset: 0x0010DC94
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ParameterDef;
		}

		// Token: 0x17001B28 RID: 6952
		// (get) Token: 0x0600405E RID: 16478 RVA: 0x0010FA9B File Offset: 0x0010DC9B
		public int ID
		{
			get
			{
				return this.m_referenceId;
			}
		}

		// Token: 0x04001D86 RID: 7558
		private ParameterDataSource m_validValuesDataSource;

		// Token: 0x04001D87 RID: 7559
		private List<ExpressionInfo> m_validValuesValueExpressions;

		// Token: 0x04001D88 RID: 7560
		private List<ExpressionInfo> m_validValuesLabelExpressions;

		// Token: 0x04001D89 RID: 7561
		private ParameterDataSource m_defaultDataSource;

		// Token: 0x04001D8A RID: 7562
		private List<ExpressionInfo> m_defaultExpressions;

		// Token: 0x04001D8B RID: 7563
		[Reference]
		private List<ParameterDef> m_dependencyList;

		// Token: 0x04001D8C RID: 7564
		private int m_exprHostID = -1;

		// Token: 0x04001D8D RID: 7565
		private ExpressionInfo m_prompt;

		// Token: 0x04001D8E RID: 7566
		private int m_referenceId = -2;

		// Token: 0x04001D8F RID: 7567
		[NonSerialized]
		private ReportParamExprHost m_exprHost;

		// Token: 0x04001D90 RID: 7568
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = ParameterDef.GetDeclaration();
	}
}
