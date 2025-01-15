using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x020004E5 RID: 1253
	internal class LookupObjResult : IStorable, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable, IErrorContext
	{
		// Token: 0x06003F48 RID: 16200 RVA: 0x0010C90D File Offset: 0x0010AB0D
		internal LookupObjResult()
		{
		}

		// Token: 0x06003F49 RID: 16201 RVA: 0x0010C920 File Offset: 0x0010AB20
		internal LookupObjResult(LookupTable lookupTable)
		{
			this.m_lookupTable = lookupTable;
		}

		// Token: 0x17001ACC RID: 6860
		// (get) Token: 0x06003F4A RID: 16202 RVA: 0x0010C93A File Offset: 0x0010AB3A
		internal bool ErrorOccured
		{
			get
			{
				return this.m_hasErrorCode || this.m_dataFieldStatus > DataFieldStatus.None;
			}
		}

		// Token: 0x17001ACD RID: 6861
		// (get) Token: 0x06003F4B RID: 16203 RVA: 0x0010C94F File Offset: 0x0010AB4F
		// (set) Token: 0x06003F4C RID: 16204 RVA: 0x0010C957 File Offset: 0x0010AB57
		internal DataFieldStatus DataFieldStatus
		{
			get
			{
				return this.m_dataFieldStatus;
			}
			set
			{
				this.m_dataFieldStatus = value;
			}
		}

		// Token: 0x17001ACE RID: 6862
		// (get) Token: 0x06003F4D RID: 16205 RVA: 0x0010C960 File Offset: 0x0010AB60
		internal bool HasErrorCode
		{
			get
			{
				return this.m_hasErrorCode;
			}
		}

		// Token: 0x17001ACF RID: 6863
		// (get) Token: 0x06003F4E RID: 16206 RVA: 0x0010C968 File Offset: 0x0010AB68
		internal ProcessingErrorCode ErrorCode
		{
			get
			{
				return this.m_errorCode;
			}
		}

		// Token: 0x17001AD0 RID: 6864
		// (get) Token: 0x06003F4F RID: 16207 RVA: 0x0010C970 File Offset: 0x0010AB70
		internal Severity ErrorSeverity
		{
			get
			{
				return this.m_errorSeverity;
			}
		}

		// Token: 0x17001AD1 RID: 6865
		// (get) Token: 0x06003F50 RID: 16208 RVA: 0x0010C978 File Offset: 0x0010AB78
		internal string[] ErrorMessageArgs
		{
			get
			{
				return this.m_errorMessageArgs;
			}
		}

		// Token: 0x17001AD2 RID: 6866
		// (get) Token: 0x06003F51 RID: 16209 RVA: 0x0010C980 File Offset: 0x0010AB80
		internal bool HasBeenTransferred
		{
			get
			{
				return this.m_lookupTablePartitionId != TreePartitionManager.EmptyTreePartitionID;
			}
		}

		// Token: 0x06003F52 RID: 16210 RVA: 0x0010C994 File Offset: 0x0010AB94
		internal LookupTable GetLookupTable(OnDemandProcessingContext odpContext)
		{
			if (this.m_lookupTable == null)
			{
				Global.Tracer.Assert(this.HasBeenTransferred, "Invalid LookupObjResult: PartitionID for LookupTable is empty.");
				OnDemandMetadata odpMetadata = odpContext.OdpMetadata;
				odpMetadata.EnsureLookupScalabilitySetup(odpContext.ChunkFactory, odpContext.GetActiveCompatibilityVersion(), odpContext.ProhibitSerializableValues);
				long treePartitionOffset = odpMetadata.LookupPartitionManager.GetTreePartitionOffset(this.m_lookupTablePartitionId);
				LookupScalabilityCache lookupScalabilityCache = odpMetadata.LookupScalabilityCache;
				this.m_lookupTable = (LookupTable)lookupScalabilityCache.Storage.Retrieve(treePartitionOffset);
				this.m_lookupTable.SetEqualityComparer(odpContext.ProcessingComparer);
			}
			return this.m_lookupTable;
		}

		// Token: 0x06003F53 RID: 16211 RVA: 0x0010CA24 File Offset: 0x0010AC24
		internal void TransferToLookupCache(OnDemandProcessingContext odpContext)
		{
			Global.Tracer.Assert(this.m_lookupTable != null, "Can't transfer a missing LookupTable");
			Global.Tracer.Assert(!this.HasBeenTransferred, "Can't transfer a LookupTable twice");
			OnDemandMetadata odpMetadata = odpContext.OdpMetadata;
			odpMetadata.EnsureLookupScalabilitySetup(odpContext.ChunkFactory, odpContext.GetActiveCompatibilityVersion(), odpContext.ProhibitSerializableValues);
			LookupScalabilityCache lookupScalabilityCache = odpMetadata.LookupScalabilityCache;
			IReference<LookupTable> reference = lookupScalabilityCache.AllocateEmptyTreePartition<LookupTable>(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupTableReference);
			this.m_lookupTable.TransferTo(lookupScalabilityCache);
			lookupScalabilityCache.SetTreePartitionContentsAndPin<LookupTable>(reference, this.m_lookupTable);
			this.m_lookupTablePartitionId = reference.Id;
			reference.UnPinValue();
		}

		// Token: 0x06003F54 RID: 16212 RVA: 0x0010CABC File Offset: 0x0010ACBC
		void IErrorContext.Register(ProcessingErrorCode code, Severity severity, params string[] arguments)
		{
			if (!this.m_hasErrorCode)
			{
				this.m_hasErrorCode = true;
				this.m_errorCode = code;
				this.m_errorSeverity = severity;
				this.m_errorMessageArgs = arguments;
			}
		}

		// Token: 0x06003F55 RID: 16213 RVA: 0x0010CAE2 File Offset: 0x0010ACE2
		void IErrorContext.Register(ProcessingErrorCode code, Severity severity, Microsoft.ReportingServices.ReportProcessing.ObjectType objectType, string objectName, string propertyName, params string[] arguments)
		{
			((IErrorContext)this).Register(code, severity, arguments);
		}

		// Token: 0x17001AD3 RID: 6867
		// (get) Token: 0x06003F56 RID: 16214 RVA: 0x0010CAEE File Offset: 0x0010ACEE
		public int Size
		{
			get
			{
				return 1 + ItemSizes.SizeOf(this.m_lookupTable);
			}
		}

		// Token: 0x06003F57 RID: 16215 RVA: 0x0010CB00 File Offset: 0x0010AD00
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(LookupObjResult.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.Severity)
				{
					if (memberName == MemberName.FieldStatus)
					{
						writer.WriteEnum((int)this.m_dataFieldStatus);
						continue;
					}
					if (memberName == MemberName.Code)
					{
						writer.WriteEnum((int)this.m_errorCode);
						continue;
					}
					if (memberName == MemberName.Severity)
					{
						writer.WriteEnum((int)this.m_errorSeverity);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.HasCode)
					{
						writer.Write(this.m_hasErrorCode);
						continue;
					}
					if (memberName == MemberName.Arguments)
					{
						writer.Write(this.m_errorMessageArgs);
						continue;
					}
					if (memberName == MemberName.LookupTablePartitionID)
					{
						writer.Write(this.m_lookupTablePartitionId.Value);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003F58 RID: 16216 RVA: 0x0010CBD8 File Offset: 0x0010ADD8
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(LookupObjResult.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.Severity)
				{
					if (memberName == MemberName.FieldStatus)
					{
						this.m_dataFieldStatus = (DataFieldStatus)reader.ReadEnum();
						continue;
					}
					if (memberName == MemberName.Code)
					{
						this.m_errorCode = (ProcessingErrorCode)reader.ReadEnum();
						continue;
					}
					if (memberName == MemberName.Severity)
					{
						this.m_errorSeverity = (Severity)reader.ReadEnum();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.HasCode)
					{
						this.m_hasErrorCode = reader.ReadBoolean();
						continue;
					}
					if (memberName == MemberName.Arguments)
					{
						this.m_errorMessageArgs = reader.ReadStringArray();
						continue;
					}
					if (memberName == MemberName.LookupTablePartitionID)
					{
						this.m_lookupTablePartitionId = new ReferenceID(reader.ReadInt64());
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06003F59 RID: 16217 RVA: 0x0010CCAF File Offset: 0x0010AEAF
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
		}

		// Token: 0x06003F5A RID: 16218 RVA: 0x0010CCB1 File Offset: 0x0010AEB1
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupObjResult;
		}

		// Token: 0x06003F5B RID: 16219 RVA: 0x0010CCB8 File Offset: 0x0010AEB8
		public static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			if (LookupObjResult.m_Declaration == null)
			{
				return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.LookupInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
				{
					new MemberInfo(MemberName.LookupTablePartitionID, Token.Int64),
					new MemberInfo(MemberName.HasCode, Token.Boolean),
					new MemberInfo(MemberName.Code, Token.Enum),
					new MemberInfo(MemberName.Severity, Token.Enum),
					new MemberInfo(MemberName.FieldStatus, Token.Enum),
					new MemberInfo(MemberName.Arguments, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveArray, Token.String)
				});
			}
			return LookupObjResult.m_Declaration;
		}

		// Token: 0x04001D36 RID: 7478
		private ProcessingErrorCode m_errorCode;

		// Token: 0x04001D37 RID: 7479
		private bool m_hasErrorCode;

		// Token: 0x04001D38 RID: 7480
		private Severity m_errorSeverity;

		// Token: 0x04001D39 RID: 7481
		private string[] m_errorMessageArgs;

		// Token: 0x04001D3A RID: 7482
		private DataFieldStatus m_dataFieldStatus;

		// Token: 0x04001D3B RID: 7483
		private ReferenceID m_lookupTablePartitionId = TreePartitionManager.EmptyTreePartitionID;

		// Token: 0x04001D3C RID: 7484
		[NonSerialized]
		private LookupTable m_lookupTable;

		// Token: 0x04001D3D RID: 7485
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = LookupObjResult.GetDeclaration();
	}
}
