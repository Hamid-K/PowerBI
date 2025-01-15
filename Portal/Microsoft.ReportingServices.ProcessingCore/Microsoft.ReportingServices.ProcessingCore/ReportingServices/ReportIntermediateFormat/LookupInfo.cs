using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004E3 RID: 1251
	internal class LookupInfo : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003F10 RID: 16144 RVA: 0x0010BF07 File Offset: 0x0010A107
		internal LookupInfo()
		{
		}

		// Token: 0x17001ABC RID: 6844
		// (get) Token: 0x06003F11 RID: 16145 RVA: 0x0010BF0F File Offset: 0x0010A10F
		// (set) Token: 0x06003F12 RID: 16146 RVA: 0x0010BF17 File Offset: 0x0010A117
		internal ExpressionInfo ResultExpr
		{
			get
			{
				return this.m_resultExpr;
			}
			set
			{
				this.m_resultExpr = value;
			}
		}

		// Token: 0x17001ABD RID: 6845
		// (get) Token: 0x06003F13 RID: 16147 RVA: 0x0010BF20 File Offset: 0x0010A120
		// (set) Token: 0x06003F14 RID: 16148 RVA: 0x0010BF28 File Offset: 0x0010A128
		internal ExpressionInfo SourceExpr
		{
			get
			{
				return this.m_sourceExpr;
			}
			set
			{
				this.m_sourceExpr = value;
			}
		}

		// Token: 0x17001ABE RID: 6846
		// (get) Token: 0x06003F15 RID: 16149 RVA: 0x0010BF31 File Offset: 0x0010A131
		// (set) Token: 0x06003F16 RID: 16150 RVA: 0x0010BF39 File Offset: 0x0010A139
		internal int DestinationIndexInCollection
		{
			get
			{
				return this.m_destinationIndexInCollection;
			}
			set
			{
				this.m_destinationIndexInCollection = value;
			}
		}

		// Token: 0x17001ABF RID: 6847
		// (get) Token: 0x06003F17 RID: 16151 RVA: 0x0010BF42 File Offset: 0x0010A142
		// (set) Token: 0x06003F18 RID: 16152 RVA: 0x0010BF4A File Offset: 0x0010A14A
		internal int DataSetIndexInCollection
		{
			get
			{
				return this.m_dataSetIndexInCollection;
			}
			set
			{
				this.m_dataSetIndexInCollection = value;
			}
		}

		// Token: 0x17001AC0 RID: 6848
		// (get) Token: 0x06003F19 RID: 16153 RVA: 0x0010BF53 File Offset: 0x0010A153
		// (set) Token: 0x06003F1A RID: 16154 RVA: 0x0010BF5B File Offset: 0x0010A15B
		internal LookupType LookupType
		{
			get
			{
				return this.m_lookupType;
			}
			set
			{
				this.m_lookupType = value;
			}
		}

		// Token: 0x06003F1B RID: 16155 RVA: 0x0010BF64 File Offset: 0x0010A164
		internal bool ReturnFirstMatchOnly()
		{
			return this.m_lookupType != LookupType.LookupSet;
		}

		// Token: 0x17001AC1 RID: 6849
		// (get) Token: 0x06003F1C RID: 16156 RVA: 0x0010BF72 File Offset: 0x0010A172
		// (set) Token: 0x06003F1D RID: 16157 RVA: 0x0010BF7A File Offset: 0x0010A17A
		internal string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x17001AC2 RID: 6850
		// (get) Token: 0x06003F1E RID: 16158 RVA: 0x0010BF83 File Offset: 0x0010A183
		// (set) Token: 0x06003F1F RID: 16159 RVA: 0x0010BF8B File Offset: 0x0010A18B
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

		// Token: 0x17001AC3 RID: 6851
		// (get) Token: 0x06003F20 RID: 16160 RVA: 0x0010BF94 File Offset: 0x0010A194
		internal LookupExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x17001AC4 RID: 6852
		// (get) Token: 0x06003F21 RID: 16161 RVA: 0x0010BF9C File Offset: 0x0010A19C
		// (set) Token: 0x06003F22 RID: 16162 RVA: 0x0010BFA4 File Offset: 0x0010A1A4
		internal LookupDestinationInfo DestinationInfo
		{
			get
			{
				return this.m_destinationInfo;
			}
			set
			{
				this.m_destinationInfo = value;
			}
		}

		// Token: 0x06003F23 RID: 16163 RVA: 0x0010BFB0 File Offset: 0x0010A1B0
		internal string GetAsString()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(Enum.GetName(typeof(LookupType), this.m_lookupType));
			stringBuilder.Append("(");
			bool flag = false;
			this.AppendWithSeperator(stringBuilder, this.m_sourceExpr, ref flag);
			this.AppendWithSeperator(stringBuilder, this.m_destinationInfo.DestinationExpr, ref flag);
			this.AppendWithSeperator(stringBuilder, this.m_resultExpr, ref flag);
			if (!string.IsNullOrEmpty(this.m_destinationInfo.Scope))
			{
				if (flag)
				{
					stringBuilder.Append(", ");
				}
				stringBuilder.Append("\"");
				stringBuilder.Append(this.m_destinationInfo.Scope);
				stringBuilder.Append("\"");
			}
			stringBuilder.Append(")");
			return stringBuilder.ToString();
		}

		// Token: 0x06003F24 RID: 16164 RVA: 0x0010C084 File Offset: 0x0010A284
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			LookupInfo lookupInfo = (LookupInfo)base.MemberwiseClone();
			lookupInfo.m_name = context.CreateLookupID(this.m_name);
			if (this.m_resultExpr != null)
			{
				lookupInfo.m_resultExpr = (ExpressionInfo)this.m_resultExpr.PublishClone(context);
			}
			if (this.m_sourceExpr != null)
			{
				lookupInfo.m_sourceExpr = (ExpressionInfo)this.m_sourceExpr.PublishClone(context);
			}
			lookupInfo.m_destinationInfo = (LookupDestinationInfo)this.m_destinationInfo.PublishClone(context);
			return lookupInfo;
		}

		// Token: 0x06003F25 RID: 16165 RVA: 0x0010C106 File Offset: 0x0010A306
		private void AppendWithSeperator(StringBuilder sb, ExpressionInfo expr, ref bool appendSeperator)
		{
			if (expr != null)
			{
				if (appendSeperator)
				{
					sb.Append(", ");
				}
				sb.Append(expr.OriginalText);
				appendSeperator = true;
			}
		}

		// Token: 0x06003F26 RID: 16166 RVA: 0x0010C12C File Offset: 0x0010A32C
		internal void Initialize(InitializationContext context, string dataSetName, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			context.ExprHostBuilder.LookupStart();
			if (this.m_resultExpr != null)
			{
				this.m_resultExpr.LookupInitialize(dataSetName, objectType, objectName, propertyName, context);
				context.ExprHostBuilder.LookupResultExpr(this.m_resultExpr);
			}
			if (this.m_sourceExpr != null)
			{
				this.m_sourceExpr.Initialize(propertyName, context);
				context.ExprHostBuilder.LookupSourceExpr(this.m_sourceExpr);
			}
			this.ExprHostID = context.ExprHostBuilder.LookupEnd();
		}

		// Token: 0x06003F27 RID: 16167 RVA: 0x0010C1AC File Offset: 0x0010A3AC
		internal void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (this.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
				this.m_exprHost = reportExprHost.LookupExprHostsRemotable[this.ExprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
			}
		}

		// Token: 0x06003F28 RID: 16168 RVA: 0x0010C1FE File Offset: 0x0010A3FE
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateSourceExpr(Microsoft.ReportingServices.RdlExpressions.ReportRuntime runtime)
		{
			return runtime.EvaluateLookupSourceExpression(this);
		}

		// Token: 0x06003F29 RID: 16169 RVA: 0x0010C207 File Offset: 0x0010A407
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateResultExpr(Microsoft.ReportingServices.RdlExpressions.ReportRuntime runtime)
		{
			return runtime.EvaluateLookupResultExpression(this);
		}

		// Token: 0x06003F2A RID: 16170 RVA: 0x0010C210 File Offset: 0x0010A410
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(LookupInfo.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.DataSetName)
				{
					if (memberName == MemberName.DataSetIndexInCollection)
					{
						writer.Write(this.m_dataSetIndexInCollection);
						continue;
					}
					if (memberName == MemberName.Name)
					{
						writer.Write(this.m_name);
						continue;
					}
					if (memberName == MemberName.DataSetName)
					{
						writer.Write(this.m_dataSetName);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.ExprHostID)
					{
						writer.Write(this.m_exprHostID);
						continue;
					}
					switch (memberName)
					{
					case MemberName.SourceExpr:
						writer.Write(this.m_sourceExpr);
						continue;
					case MemberName.DestinationExpr:
						break;
					case MemberName.ResultExpr:
						writer.Write(this.m_resultExpr);
						continue;
					case MemberName.DestinationIndexInCollection:
						writer.Write(this.m_destinationIndexInCollection);
						continue;
					default:
						if (memberName == MemberName.LookupType)
						{
							writer.WriteEnum((int)this.m_lookupType);
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003F2B RID: 16171 RVA: 0x0010C31C File Offset: 0x0010A51C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(LookupInfo.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.DataSetName)
				{
					if (memberName == MemberName.DataSetIndexInCollection)
					{
						this.m_dataSetIndexInCollection = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.Name)
					{
						this.m_name = reader.ReadString();
						continue;
					}
					if (memberName == MemberName.DataSetName)
					{
						this.m_dataSetName = reader.ReadString();
						continue;
					}
				}
				else if (memberName <= MemberName.IsMultiValue)
				{
					if (memberName == MemberName.ExprHostID)
					{
						this.m_exprHostID = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.IsMultiValue)
					{
						if (reader.ReadBoolean())
						{
							this.m_lookupType = LookupType.LookupSet;
							continue;
						}
						this.m_lookupType = LookupType.Lookup;
						continue;
					}
				}
				else
				{
					switch (memberName)
					{
					case MemberName.SourceExpr:
						this.m_sourceExpr = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.DestinationExpr:
						break;
					case MemberName.ResultExpr:
						this.m_resultExpr = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					case MemberName.DestinationIndexInCollection:
						this.m_destinationIndexInCollection = reader.ReadInt32();
						continue;
					default:
						if (memberName == MemberName.LookupType)
						{
							this.m_lookupType = (LookupType)reader.ReadEnum();
							continue;
						}
						break;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003F2C RID: 16172 RVA: 0x0010C472 File Offset: 0x0010A672
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06003F2D RID: 16173 RVA: 0x0010C474 File Offset: 0x0010A674
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupInfo;
		}

		// Token: 0x06003F2E RID: 16174 RVA: 0x0010C47C File Offset: 0x0010A67C
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (LookupInfo.m_Declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.ResultExpr, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
					new MemberInfo(MemberName.DataSetName, Token.String),
					new MemberInfo(MemberName.SourceExpr, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
					new ReadOnlyMemberInfo(MemberName.IsMultiValue, Token.Boolean),
					new MemberInfo(MemberName.DestinationIndexInCollection, Token.Int32),
					new MemberInfo(MemberName.Name, Token.String),
					new MemberInfo(MemberName.ExprHostID, Token.Int32),
					new MemberInfo(MemberName.DataSetIndexInCollection, Token.Int32),
					new MemberInfo(MemberName.LookupType, Token.Enum)
				});
			}
			return LookupInfo.m_Declaration;
		}

		// Token: 0x04001D23 RID: 7459
		private ExpressionInfo m_resultExpr;

		// Token: 0x04001D24 RID: 7460
		private string m_dataSetName;

		// Token: 0x04001D25 RID: 7461
		private ExpressionInfo m_sourceExpr;

		// Token: 0x04001D26 RID: 7462
		private int m_destinationIndexInCollection;

		// Token: 0x04001D27 RID: 7463
		private string m_name;

		// Token: 0x04001D28 RID: 7464
		private int m_exprHostID;

		// Token: 0x04001D29 RID: 7465
		private int m_dataSetIndexInCollection;

		// Token: 0x04001D2A RID: 7466
		private LookupType m_lookupType;

		// Token: 0x04001D2B RID: 7467
		[NonSerialized]
		private LookupExprHost m_exprHost;

		// Token: 0x04001D2C RID: 7468
		[NonSerialized]
		private LookupDestinationInfo m_destinationInfo;

		// Token: 0x04001D2D RID: 7469
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = LookupInfo.GetDeclaration();
	}
}
