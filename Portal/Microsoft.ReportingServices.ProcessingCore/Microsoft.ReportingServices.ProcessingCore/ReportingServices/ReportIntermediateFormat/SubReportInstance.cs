using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.ReportingServices.OnDemandProcessing;
using Microsoft.ReportingServices.OnDemandProcessing.Scalability;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000413 RID: 1043
	public class SubReportInstance : ScopeInstance, IReportInstanceContainer
	{
		// Token: 0x06002D2C RID: 11564 RVA: 0x000CF2A4 File Offset: 0x000CD4A4
		internal SubReportInstance()
		{
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x000CF2AC File Offset: 0x000CD4AC
		private SubReportInstance(SubReport subreport, OnDemandMetadata odpMetadata)
		{
			this.m_subReportDef = subreport;
			this.m_reportInstance = odpMetadata.GroupTreeScalabilityCache.AllocateEmptyTreePartition<ReportInstance>(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportInstanceReference);
		}

		// Token: 0x170015A8 RID: 5544
		// (get) Token: 0x06002D2E RID: 11566 RVA: 0x000CF2CE File Offset: 0x000CD4CE
		internal override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType ObjectType
		{
			get
			{
				return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReportInstance;
			}
		}

		// Token: 0x170015A9 RID: 5545
		// (get) Token: 0x06002D2F RID: 11567 RVA: 0x000CF2D5 File Offset: 0x000CD4D5
		internal SubReport SubReportDef
		{
			get
			{
				return this.m_subReportDef;
			}
		}

		// Token: 0x170015AA RID: 5546
		// (get) Token: 0x06002D30 RID: 11568 RVA: 0x000CF2DD File Offset: 0x000CD4DD
		// (set) Token: 0x06002D31 RID: 11569 RVA: 0x000CF2E5 File Offset: 0x000CD4E5
		internal bool Initialized
		{
			get
			{
				return this.m_initialized;
			}
			set
			{
				this.m_initialized = value;
			}
		}

		// Token: 0x170015AB RID: 5547
		// (get) Token: 0x06002D32 RID: 11570 RVA: 0x000CF2EE File Offset: 0x000CD4EE
		// (set) Token: 0x06002D33 RID: 11571 RVA: 0x000CF2F6 File Offset: 0x000CD4F6
		internal ParametersImpl Parameters
		{
			get
			{
				return this.m_parameters;
			}
			set
			{
				this.m_parameters = value;
			}
		}

		// Token: 0x170015AC RID: 5548
		// (get) Token: 0x06002D34 RID: 11572 RVA: 0x000CF2FF File Offset: 0x000CD4FF
		internal bool NoRows
		{
			get
			{
				return this.m_reportInstance != null && this.m_reportInstance.Value().NoRows;
			}
		}

		// Token: 0x170015AD RID: 5549
		// (get) Token: 0x06002D35 RID: 11573 RVA: 0x000CF31B File Offset: 0x000CD51B
		public IReference<ReportInstance> ReportInstance
		{
			get
			{
				return this.m_reportInstance;
			}
		}

		// Token: 0x06002D36 RID: 11574 RVA: 0x000CF323 File Offset: 0x000CD523
		public IReference<ReportInstance> SetReportInstance(ReportInstance reportInstance, OnDemandMetadata odpMetadata)
		{
			odpMetadata.GroupTreeScalabilityCache.SetTreePartitionContentsAndPin<ReportInstance>(this.m_reportInstance, reportInstance);
			return this.m_reportInstance;
		}

		// Token: 0x170015AE RID: 5550
		// (get) Token: 0x06002D37 RID: 11575 RVA: 0x000CF33D File Offset: 0x000CD53D
		// (set) Token: 0x06002D38 RID: 11576 RVA: 0x000CF345 File Offset: 0x000CD545
		internal string InstanceUniqueName
		{
			get
			{
				return this.m_instanceUniqueName;
			}
			set
			{
				this.m_instanceUniqueName = value;
			}
		}

		// Token: 0x170015AF RID: 5551
		// (get) Token: 0x06002D39 RID: 11577 RVA: 0x000CF34E File Offset: 0x000CD54E
		// (set) Token: 0x06002D3A RID: 11578 RVA: 0x000CF356 File Offset: 0x000CD556
		internal CultureInfo ThreadCulture
		{
			get
			{
				return this.m_threadCulture;
			}
			set
			{
				this.m_threadCulture = value;
			}
		}

		// Token: 0x170015B0 RID: 5552
		// (get) Token: 0x06002D3B RID: 11579 RVA: 0x000CF35F File Offset: 0x000CD55F
		// (set) Token: 0x06002D3C RID: 11580 RVA: 0x000CF367 File Offset: 0x000CD567
		internal SubReport.Status RetrievalStatus
		{
			get
			{
				return this.m_status;
			}
			set
			{
				this.m_status = value;
			}
		}

		// Token: 0x170015B1 RID: 5553
		// (get) Token: 0x06002D3D RID: 11581 RVA: 0x000CF370 File Offset: 0x000CD570
		// (set) Token: 0x06002D3E RID: 11582 RVA: 0x000CF378 File Offset: 0x000CD578
		internal bool ProcessedWithError
		{
			get
			{
				return this.m_processedWithError;
			}
			set
			{
				this.m_processedWithError = value;
			}
		}

		// Token: 0x06002D3F RID: 11583 RVA: 0x000CF381 File Offset: 0x000CD581
		internal override void AddChildScope(IReference<ScopeInstance> child, int indexInCollection)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06002D40 RID: 11584 RVA: 0x000CF390 File Offset: 0x000CD590
		internal string GetChunkNameModifier(SubReportInfo subReportInfo, bool useCachedValue, bool addEntry, out bool isShared)
		{
			if (!useCachedValue || this.m_dataChunkNameModifier == null)
			{
				if (!useCachedValue)
				{
					this.m_isInstanceShared = null;
				}
				this.m_dataChunkNameModifier = new int?(subReportInfo.GetChunkNameModifierForParamValues(this.m_parameters, addEntry, ref this.m_isInstanceShared, out this.m_parameters));
			}
			isShared = this.m_isInstanceShared.Value;
			return this.m_dataChunkNameModifier.Value.ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06002D41 RID: 11585 RVA: 0x000CF408 File Offset: 0x000CD608
		internal override void InstanceComplete()
		{
			if (this.m_reportInstance != null)
			{
				ReportInstance reportInstance = this.m_reportInstance.Value();
				if (reportInstance != null)
				{
					reportInstance.InstanceComplete();
				}
			}
			IReference reference = (IReference<SubReportInstance>)this.m_cleanupRef;
			base.InstanceComplete();
			reference.PinValue();
		}

		// Token: 0x06002D42 RID: 11586 RVA: 0x000CF44C File Offset: 0x000CD64C
		internal static IReference<SubReportInstance> CreateInstance(ScopeInstance parentInstance, SubReport subReport, OnDemandMetadata odpMetadata)
		{
			SubReportInstance subReportInstance = new SubReportInstance(subReport, odpMetadata);
			IReference<SubReportInstance> reference = odpMetadata.GroupTreeScalabilityCache.AllocateAndPin<SubReportInstance>(subReportInstance, 0);
			subReportInstance.m_cleanupRef = (IDisposable)reference;
			parentInstance.AddChildScope((IReference<ScopeInstance>)reference, subReport.IndexInCollection);
			return reference;
		}

		// Token: 0x06002D43 RID: 11587 RVA: 0x000CF490 File Offset: 0x000CD690
		internal new static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReportInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ScopeInstance, new List<MemberInfo>
			{
				new MemberInfo(MemberName.SubReport, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReport, Token.GlobalReference),
				new MemberInfo(MemberName.ReportInstance, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.ReportInstanceReference),
				new MemberInfo(MemberName.DataSetUniqueName, Token.String),
				new MemberInfo(MemberName.ThreadCulture, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.CultureInfo),
				new MemberInfo(MemberName.Parameters, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Parameters),
				new MemberInfo(MemberName.Status, Token.Enum),
				new MemberInfo(MemberName.ProcessedWithError, Token.Boolean),
				new MemberInfo(MemberName.IsInstanceShared, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Nullable, Token.Boolean),
				new MemberInfo(MemberName.DataChunkNameModifier, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Nullable, Token.Int32)
			});
		}

		// Token: 0x06002D44 RID: 11588 RVA: 0x000CF554 File Offset: 0x000CD754
		public override void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(SubReportInstance.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.SubReport:
					writer.WriteGlobalReference(this.m_subReportDef);
					break;
				case MemberName.ReportInstance:
					writer.Write(this.m_reportInstance);
					break;
				case MemberName.DataSetUniqueName:
					writer.Write(this.m_instanceUniqueName);
					break;
				case MemberName.ThreadCulture:
					writer.Write(this.m_threadCulture);
					break;
				case MemberName.Parameters:
					if (this.m_parameters != null)
					{
						writer.Write(new ParametersImplWrapper(this.m_parameters));
					}
					else
					{
						writer.WriteNull();
					}
					break;
				case MemberName.Status:
					writer.WriteEnum((int)this.m_status);
					break;
				case MemberName.ProcessedWithError:
					writer.Write(this.m_processedWithError);
					break;
				default:
					if (memberName != MemberName.IsInstanceShared)
					{
						if (memberName != MemberName.DataChunkNameModifier)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							writer.Write(this.m_dataChunkNameModifier);
						}
					}
					else
					{
						writer.Write(this.m_isInstanceShared);
					}
					break;
				}
			}
		}

		// Token: 0x06002D45 RID: 11589 RVA: 0x000CF680 File Offset: 0x000CD880
		public override void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(SubReportInstance.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				switch (memberName)
				{
				case MemberName.SubReport:
					this.m_subReportDef = reader.ReadGlobalReference<SubReport>();
					break;
				case MemberName.ReportInstance:
					this.m_reportInstance = (IReference<ReportInstance>)reader.ReadRIFObject();
					break;
				case MemberName.DataSetUniqueName:
					this.m_instanceUniqueName = reader.ReadString();
					break;
				case MemberName.ThreadCulture:
					this.m_threadCulture = reader.ReadCultureInfo();
					break;
				case MemberName.Parameters:
				{
					ParametersImplWrapper parametersImplWrapper = (ParametersImplWrapper)reader.ReadRIFObject();
					if (parametersImplWrapper != null)
					{
						this.m_parameters = parametersImplWrapper.WrappedParametersImpl;
					}
					break;
				}
				case MemberName.Status:
					this.m_status = (SubReport.Status)reader.ReadEnum();
					break;
				case MemberName.ProcessedWithError:
					this.m_processedWithError = reader.ReadBoolean();
					break;
				default:
					if (memberName != MemberName.IsInstanceShared)
					{
						if (memberName != MemberName.DataChunkNameModifier)
						{
							Global.Tracer.Assert(false);
						}
						else
						{
							object obj = reader.ReadVariant();
							if (obj != null)
							{
								this.m_dataChunkNameModifier = new int?((int)obj);
							}
						}
					}
					else
					{
						object obj2 = reader.ReadVariant();
						if (obj2 != null)
						{
							this.m_isInstanceShared = new bool?((bool)obj2);
						}
					}
					break;
				}
			}
		}

		// Token: 0x06002D46 RID: 11590 RVA: 0x000CF7C1 File Offset: 0x000CD9C1
		public override void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06002D47 RID: 11591 RVA: 0x000CF7CE File Offset: 0x000CD9CE
		public override Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReportInstance;
		}

		// Token: 0x170015B2 RID: 5554
		// (get) Token: 0x06002D48 RID: 11592 RVA: 0x000CF7D8 File Offset: 0x000CD9D8
		public override int Size
		{
			get
			{
				return base.Size + ItemSizes.SizeOf(this.m_parameters) + ItemSizes.SizeOf(this.m_reportInstance) + ItemSizes.SizeOf(this.m_instanceUniqueName) + ItemSizes.ReferenceSize + 4 + 1 + ItemSizes.ReferenceSize + 1 + ItemSizes.SizeOf(this.m_dataChunkNameModifier) + ItemSizes.NullableInt32Size + ItemSizes.NullableBoolSize;
			}
		}

		// Token: 0x0400181C RID: 6172
		private ParametersImpl m_parameters;

		// Token: 0x0400181D RID: 6173
		private IReference<ReportInstance> m_reportInstance;

		// Token: 0x0400181E RID: 6174
		private string m_instanceUniqueName;

		// Token: 0x0400181F RID: 6175
		private CultureInfo m_threadCulture;

		// Token: 0x04001820 RID: 6176
		private SubReport.Status m_status;

		// Token: 0x04001821 RID: 6177
		private bool m_processedWithError;

		// Token: 0x04001822 RID: 6178
		private SubReport m_subReportDef;

		// Token: 0x04001823 RID: 6179
		private bool? m_isInstanceShared;

		// Token: 0x04001824 RID: 6180
		private int? m_dataChunkNameModifier;

		// Token: 0x04001825 RID: 6181
		[NonSerialized]
		private bool m_initialized;

		// Token: 0x04001826 RID: 6182
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = SubReportInstance.GetDeclaration();
	}
}
