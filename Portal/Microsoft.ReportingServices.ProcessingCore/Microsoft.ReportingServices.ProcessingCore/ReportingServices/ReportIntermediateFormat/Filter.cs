using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000509 RID: 1289
	[Serializable]
	public sealed class Filter : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x17001C7F RID: 7295
		// (get) Token: 0x060043DD RID: 17373 RVA: 0x0011C899 File Offset: 0x0011AA99
		// (set) Token: 0x060043DE RID: 17374 RVA: 0x0011C8A1 File Offset: 0x0011AAA1
		internal ExpressionInfo Expression
		{
			get
			{
				return this.m_expression;
			}
			set
			{
				this.m_expression = value;
			}
		}

		// Token: 0x17001C80 RID: 7296
		// (get) Token: 0x060043DF RID: 17375 RVA: 0x0011C8AA File Offset: 0x0011AAAA
		// (set) Token: 0x060043E0 RID: 17376 RVA: 0x0011C8B2 File Offset: 0x0011AAB2
		internal Filter.Operators Operator
		{
			get
			{
				return this.m_operator;
			}
			set
			{
				this.m_operator = value;
			}
		}

		// Token: 0x17001C81 RID: 7297
		// (get) Token: 0x060043E1 RID: 17377 RVA: 0x0011C8BB File Offset: 0x0011AABB
		// (set) Token: 0x060043E2 RID: 17378 RVA: 0x0011C8C3 File Offset: 0x0011AAC3
		internal List<ExpressionInfoTypeValuePair> Values
		{
			get
			{
				return this.m_values;
			}
			set
			{
				this.m_values = value;
			}
		}

		// Token: 0x17001C82 RID: 7298
		// (get) Token: 0x060043E3 RID: 17379 RVA: 0x0011C8CC File Offset: 0x0011AACC
		// (set) Token: 0x060043E4 RID: 17380 RVA: 0x0011C8D4 File Offset: 0x0011AAD4
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

		// Token: 0x17001C83 RID: 7299
		// (get) Token: 0x060043E5 RID: 17381 RVA: 0x0011C8DD File Offset: 0x0011AADD
		internal FilterExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x060043E6 RID: 17382 RVA: 0x0011C8E8 File Offset: 0x0011AAE8
		internal void Initialize(InitializationContext context)
		{
			context.ExprHostBuilder.FilterStart();
			if (this.m_expression != null)
			{
				this.m_expression.Initialize("FilterExpression", context);
				context.ExprHostBuilder.FilterExpression(this.m_expression);
			}
			if (this.m_values != null)
			{
				for (int i = 0; i < this.m_values.Count; i++)
				{
					ExpressionInfo value = this.m_values[i].Value;
					Global.Tracer.Assert(value != null, "(expression != null)");
					value.Initialize("FilterValue", context);
					context.ExprHostBuilder.FilterValue(value);
				}
			}
			this.m_exprHostID = context.ExprHostBuilder.FilterEnd();
		}

		// Token: 0x060043E7 RID: 17383 RVA: 0x0011C99C File Offset: 0x0011AB9C
		internal void SetExprHost(IList<FilterExprHost> filterHosts, ObjectModelImpl reportObjectModel)
		{
			if (this.ExprHostID >= 0)
			{
				Global.Tracer.Assert(filterHosts != null && reportObjectModel != null, "(filterHosts != null && reportObjectModel != null)");
				this.m_exprHost = filterHosts[this.ExprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
			}
		}

		// Token: 0x060043E8 RID: 17384 RVA: 0x0011C9EC File Offset: 0x0011ABEC
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			Filter filter = (Filter)base.MemberwiseClone();
			if (this.m_expression != null)
			{
				filter.m_expression = (ExpressionInfo)this.m_expression.PublishClone(context);
			}
			if (this.m_values != null)
			{
				filter.m_values = new List<ExpressionInfoTypeValuePair>(this.m_values.Count);
				foreach (ExpressionInfoTypeValuePair expressionInfoTypeValuePair in this.m_values)
				{
					filter.m_values.Add(new ExpressionInfoTypeValuePair(expressionInfoTypeValuePair.DataType, expressionInfoTypeValuePair.HadExplicitDataType, (ExpressionInfo)expressionInfoTypeValuePair.Value.PublishClone(context)));
				}
			}
			return filter;
		}

		// Token: 0x060043E9 RID: 17385 RVA: 0x0011CAB0 File Offset: 0x0011ACB0
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Filter, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.Expression, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
				new MemberInfo(MemberName.Operator, Token.Enum),
				new MemberInfo(MemberName.Values, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfoTypeValuePair),
				new MemberInfo(MemberName.ExprHostID, Token.Int32)
			});
		}

		// Token: 0x060043EA RID: 17386 RVA: 0x0011CB20 File Offset: 0x0011AD20
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(Filter.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.Expression:
					writer.Write(this.m_expression);
					break;
				case MemberName.Operator:
					writer.WriteEnum((int)this.m_operator);
					break;
				case MemberName.Values:
					writer.Write<ExpressionInfoTypeValuePair>(this.m_values);
					break;
				default:
					if (memberName != MemberName.ExprHostID)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.Write(this.m_exprHostID);
					}
					break;
				}
			}
		}

		// Token: 0x060043EB RID: 17387 RVA: 0x0011CBBC File Offset: 0x0011ADBC
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(Filter.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.Expression:
					this.m_expression = (ExpressionInfo)reader.ReadRIFObject();
					break;
				case MemberName.Operator:
					this.m_operator = (Filter.Operators)reader.ReadEnum();
					break;
				case MemberName.Values:
					this.m_values = reader.ReadGenericListOfRIFObjects<ExpressionInfoTypeValuePair>();
					break;
				default:
					if (memberName != MemberName.ExprHostID)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_exprHostID = reader.ReadInt32();
					}
					break;
				}
			}
		}

		// Token: 0x060043EC RID: 17388 RVA: 0x0011CC5E File Offset: 0x0011AE5E
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x060043ED RID: 17389 RVA: 0x0011CC6B File Offset: 0x0011AE6B
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Filter;
		}

		// Token: 0x04001EDF RID: 7903
		private ExpressionInfo m_expression;

		// Token: 0x04001EE0 RID: 7904
		private Filter.Operators m_operator;

		// Token: 0x04001EE1 RID: 7905
		private List<ExpressionInfoTypeValuePair> m_values;

		// Token: 0x04001EE2 RID: 7906
		private int m_exprHostID = -1;

		// Token: 0x04001EE3 RID: 7907
		[NonSerialized]
		private FilterExprHost m_exprHost;

		// Token: 0x04001EE4 RID: 7908
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = Filter.GetDeclaration();

		// Token: 0x02000980 RID: 2432
		internal enum Operators
		{
			// Token: 0x04004103 RID: 16643
			Equal,
			// Token: 0x04004104 RID: 16644
			Like,
			// Token: 0x04004105 RID: 16645
			GreaterThan,
			// Token: 0x04004106 RID: 16646
			GreaterThanOrEqual,
			// Token: 0x04004107 RID: 16647
			LessThan,
			// Token: 0x04004108 RID: 16648
			LessThanOrEqual,
			// Token: 0x04004109 RID: 16649
			TopN,
			// Token: 0x0400410A RID: 16650
			BottomN,
			// Token: 0x0400410B RID: 16651
			TopPercent,
			// Token: 0x0400410C RID: 16652
			BottomPercent,
			// Token: 0x0400410D RID: 16653
			In,
			// Token: 0x0400410E RID: 16654
			Between,
			// Token: 0x0400410F RID: 16655
			NotEqual
		}
	}
}
