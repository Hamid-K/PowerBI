using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.RdlExpressions;
using Microsoft.ReportingServices.RdlExpressions.ExpressionHostObjectModel;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;
using Microsoft.ReportingServices.ReportPublishing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004E4 RID: 1252
	internal class LookupDestinationInfo : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06003F30 RID: 16176 RVA: 0x0010C56A File Offset: 0x0010A76A
		internal LookupDestinationInfo()
		{
		}

		// Token: 0x17001AC5 RID: 6853
		// (get) Token: 0x06003F31 RID: 16177 RVA: 0x0010C572 File Offset: 0x0010A772
		// (set) Token: 0x06003F32 RID: 16178 RVA: 0x0010C57A File Offset: 0x0010A77A
		internal string Scope
		{
			get
			{
				return this.m_scope;
			}
			set
			{
				this.m_scope = value;
			}
		}

		// Token: 0x17001AC6 RID: 6854
		// (get) Token: 0x06003F33 RID: 16179 RVA: 0x0010C583 File Offset: 0x0010A783
		// (set) Token: 0x06003F34 RID: 16180 RVA: 0x0010C58B File Offset: 0x0010A78B
		internal bool IsMultiValue
		{
			get
			{
				return this.m_isMultiValue;
			}
			set
			{
				this.m_isMultiValue = value;
			}
		}

		// Token: 0x17001AC7 RID: 6855
		// (get) Token: 0x06003F35 RID: 16181 RVA: 0x0010C594 File Offset: 0x0010A794
		// (set) Token: 0x06003F36 RID: 16182 RVA: 0x0010C59C File Offset: 0x0010A79C
		internal ExpressionInfo DestinationExpr
		{
			get
			{
				return this.m_destinationExpr;
			}
			set
			{
				this.m_destinationExpr = value;
			}
		}

		// Token: 0x17001AC8 RID: 6856
		// (get) Token: 0x06003F37 RID: 16183 RVA: 0x0010C5A5 File Offset: 0x0010A7A5
		// (set) Token: 0x06003F38 RID: 16184 RVA: 0x0010C5AD File Offset: 0x0010A7AD
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

		// Token: 0x17001AC9 RID: 6857
		// (get) Token: 0x06003F39 RID: 16185 RVA: 0x0010C5B6 File Offset: 0x0010A7B6
		// (set) Token: 0x06003F3A RID: 16186 RVA: 0x0010C5BE File Offset: 0x0010A7BE
		internal bool UsedInSameDataSetTablixProcessing
		{
			get
			{
				return this.m_usedInSameDataSetTablixProcessing;
			}
			set
			{
				this.m_usedInSameDataSetTablixProcessing = value;
			}
		}

		// Token: 0x17001ACA RID: 6858
		// (get) Token: 0x06003F3B RID: 16187 RVA: 0x0010C5C7 File Offset: 0x0010A7C7
		// (set) Token: 0x06003F3C RID: 16188 RVA: 0x0010C5CF File Offset: 0x0010A7CF
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

		// Token: 0x17001ACB RID: 6859
		// (get) Token: 0x06003F3D RID: 16189 RVA: 0x0010C5D8 File Offset: 0x0010A7D8
		internal LookupDestExprHost ExprHost
		{
			get
			{
				return this.m_exprHost;
			}
		}

		// Token: 0x06003F3E RID: 16190 RVA: 0x0010C5E0 File Offset: 0x0010A7E0
		internal object PublishClone(AutomaticSubtotalContext context)
		{
			LookupDestinationInfo lookupDestinationInfo = (LookupDestinationInfo)base.MemberwiseClone();
			if (this.m_destinationExpr != null)
			{
				lookupDestinationInfo.m_destinationExpr = (ExpressionInfo)this.m_destinationExpr.PublishClone(context);
			}
			return lookupDestinationInfo;
		}

		// Token: 0x06003F3F RID: 16191 RVA: 0x0010C61C File Offset: 0x0010A81C
		internal void Initialize(InitializationContext context, string dataSetName, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName)
		{
			context.ExprHostBuilder.LookupDestStart();
			if (this.m_destinationExpr != null)
			{
				this.m_destinationExpr.LookupInitialize(dataSetName, objectType, objectName, propertyName, context);
				context.ExprHostBuilder.LookupDestExpr(this.m_destinationExpr);
			}
			this.m_exprHostID = context.ExprHostBuilder.LookupDestEnd();
		}

		// Token: 0x06003F40 RID: 16192 RVA: 0x0010C674 File Offset: 0x0010A874
		internal void SetExprHost(ReportExprHost reportExprHost, ObjectModelImpl reportObjectModel)
		{
			if (this.ExprHostID >= 0)
			{
				Global.Tracer.Assert(reportExprHost != null && reportObjectModel != null, "(reportExprHost != null && reportObjectModel != null)");
				this.m_exprHost = reportExprHost.LookupDestExprHostsRemotable[this.ExprHostID];
				this.m_exprHost.SetReportObjectModel(reportObjectModel);
			}
		}

		// Token: 0x06003F41 RID: 16193 RVA: 0x0010C6C6 File Offset: 0x0010A8C6
		internal Microsoft.ReportingServices.RdlExpressions.VariantResult EvaluateDestExpr(OnDemandProcessingContext odpContext, IErrorContext errorContext)
		{
			return odpContext.ReportRuntime.EvaluateLookupDestExpression(this, errorContext);
		}

		// Token: 0x06003F42 RID: 16194 RVA: 0x0010C6D8 File Offset: 0x0010A8D8
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(LookupDestinationInfo.m_Declaration);
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
					if (memberName == MemberName.IsMultiValue)
					{
						writer.Write(this.m_isMultiValue);
						continue;
					}
					if (memberName == MemberName.DestinationExpr)
					{
						writer.Write(this.m_destinationExpr);
						continue;
					}
					if (memberName == MemberName.UsedInSameDataSetTablixProcessing)
					{
						writer.Write(this.m_usedInSameDataSetTablixProcessing);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003F43 RID: 16195 RVA: 0x0010C79C File Offset: 0x0010A99C
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(LookupDestinationInfo.m_Declaration);
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
					if (memberName == MemberName.IsMultiValue)
					{
						this.m_isMultiValue = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.DestinationExpr)
					{
						this.m_destinationExpr = (ExpressionInfo)reader.ReadRIFObject();
						continue;
					}
					if (memberName == MemberName.UsedInSameDataSetTablixProcessing)
					{
						this.m_usedInSameDataSetTablixProcessing = reader.ReadBoolean();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003F44 RID: 16196 RVA: 0x0010C862 File Offset: 0x0010AA62
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06003F45 RID: 16197 RVA: 0x0010C864 File Offset: 0x0010AA64
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupDestinationInfo;
		}

		// Token: 0x06003F46 RID: 16198 RVA: 0x0010C86C File Offset: 0x0010AA6C
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (LookupDestinationInfo.m_Declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupDestinationInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.IsMultiValue, Token.Boolean),
					new MemberInfo(MemberName.DestinationExpr, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ExpressionInfo),
					new MemberInfo(MemberName.IndexInCollection, Token.Int32),
					new MemberInfo(MemberName.UsedInSameDataSetTablixProcessing, Token.Boolean),
					new MemberInfo(MemberName.ExprHostID, Token.Int32)
				});
			}
			return LookupDestinationInfo.m_Declaration;
		}

		// Token: 0x04001D2E RID: 7470
		private bool m_isMultiValue;

		// Token: 0x04001D2F RID: 7471
		private ExpressionInfo m_destinationExpr;

		// Token: 0x04001D30 RID: 7472
		private int m_indexInCollection;

		// Token: 0x04001D31 RID: 7473
		private bool m_usedInSameDataSetTablixProcessing;

		// Token: 0x04001D32 RID: 7474
		private int m_exprHostID;

		// Token: 0x04001D33 RID: 7475
		[NonSerialized]
		private LookupDestExprHost m_exprHost;

		// Token: 0x04001D34 RID: 7476
		[NonSerialized]
		private string m_scope;

		// Token: 0x04001D35 RID: 7477
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = LookupDestinationInfo.GetDeclaration();
	}
}
