using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;
using Microsoft.ReportingServices.ReportProcessing.OnDemandReportObjectModel;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000414 RID: 1044
	internal class SubReportInfo : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06002D4A RID: 11594 RVA: 0x000CF84A File Offset: 0x000CDA4A
		internal SubReportInfo()
		{
		}

		// Token: 0x06002D4B RID: 11595 RVA: 0x000CF859 File Offset: 0x000CDA59
		internal SubReportInfo(Guid uniqueName)
		{
			this.m_uniqueName = uniqueName.ToString("N");
		}

		// Token: 0x170015B3 RID: 5555
		// (get) Token: 0x06002D4C RID: 11596 RVA: 0x000CF87A File Offset: 0x000CDA7A
		// (set) Token: 0x06002D4D RID: 11597 RVA: 0x000CF882 File Offset: 0x000CDA82
		internal CommonSubReportInfo CommonSubReportInfo
		{
			get
			{
				return this.m_commonSubReportInfo;
			}
			set
			{
				this.m_commonSubReportInfo = value;
			}
		}

		// Token: 0x170015B4 RID: 5556
		// (get) Token: 0x06002D4E RID: 11598 RVA: 0x000CF88B File Offset: 0x000CDA8B
		internal string UniqueName
		{
			get
			{
				return this.m_uniqueName;
			}
		}

		// Token: 0x170015B5 RID: 5557
		// (get) Token: 0x06002D4F RID: 11599 RVA: 0x000CF893 File Offset: 0x000CDA93
		// (set) Token: 0x06002D50 RID: 11600 RVA: 0x000CF89B File Offset: 0x000CDA9B
		internal int LastID
		{
			get
			{
				return this.m_lastID;
			}
			set
			{
				this.m_lastID = value;
			}
		}

		// Token: 0x170015B6 RID: 5558
		// (get) Token: 0x06002D51 RID: 11601 RVA: 0x000CF8A4 File Offset: 0x000CDAA4
		// (set) Token: 0x06002D52 RID: 11602 RVA: 0x000CF8AC File Offset: 0x000CDAAC
		internal int UserSortDataSetGlobalId
		{
			get
			{
				return this.m_userSortDataSetGlobalId;
			}
			set
			{
				this.m_userSortDataSetGlobalId = value;
			}
		}

		// Token: 0x06002D53 RID: 11603 RVA: 0x000CF8B8 File Offset: 0x000CDAB8
		internal int GetChunkNameModifierForParamValues(ParametersImpl parameterValues, bool addEntry, ref bool? isShared, out ParametersImpl fullParameterValues)
		{
			if (parameterValues == null)
			{
				parameterValues = new ParametersImpl();
			}
			if (this.m_parameterValuesToInfoIndexMap == null)
			{
				this.m_instanceParameterValues = new List<ParametersImplWrapper>();
				this.m_parameterValuesToInfoIndexMap = new Dictionary<ParametersImplWrapper, int>(SubReportInfo.ParametersImplValuesComparer.Instance);
			}
			ParametersImplWrapper parametersImplWrapper = new ParametersImplWrapper(parameterValues);
			int count;
			if (this.m_parameterValuesToInfoIndexMap.TryGetValue(parametersImplWrapper, out count))
			{
				fullParameterValues = this.m_instanceParameterValues[count].WrappedParametersImpl;
				if (isShared == null)
				{
					isShared = new bool?(true);
				}
			}
			else
			{
				isShared = new bool?(false);
				fullParameterValues = parameterValues;
				if (addEntry)
				{
					count = this.m_instanceParameterValues.Count;
					this.m_instanceParameterValues.Add(parametersImplWrapper);
					this.m_parameterValuesToInfoIndexMap.Add(parametersImplWrapper, count);
				}
			}
			return count;
		}

		// Token: 0x06002D54 RID: 11604 RVA: 0x000CF96C File Offset: 0x000CDB6C
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReportInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.LastID, Token.Int32),
				new MemberInfo(MemberName.UniqueName, Token.String),
				new MemberInfo(MemberName.InstanceParameterValues, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.RIFObjectList, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.Parameters),
				new MemberInfo(MemberName.DataSetID, Token.Int32)
			});
		}

		// Token: 0x06002D55 RID: 11605 RVA: 0x000CF9DC File Offset: 0x000CDBDC
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(SubReportInfo.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName <= MemberName.UniqueName)
				{
					if (memberName == MemberName.LastID)
					{
						writer.Write(this.m_lastID);
						continue;
					}
					if (memberName == MemberName.UniqueName)
					{
						writer.Write(this.m_uniqueName);
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DataSetID)
					{
						writer.Write(this.m_userSortDataSetGlobalId);
						continue;
					}
					if (memberName == MemberName.InstanceParameterValues)
					{
						writer.Write<ParametersImplWrapper>(this.m_instanceParameterValues);
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
		}

		// Token: 0x06002D56 RID: 11606 RVA: 0x000CFA78 File Offset: 0x000CDC78
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(SubReportInfo.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName <= MemberName.UniqueName)
				{
					if (memberName == MemberName.LastID)
					{
						this.m_lastID = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.UniqueName)
					{
						this.m_uniqueName = reader.ReadString();
						continue;
					}
				}
				else
				{
					if (memberName == MemberName.DataSetID)
					{
						this.m_userSortDataSetGlobalId = reader.ReadInt32();
						continue;
					}
					if (memberName == MemberName.InstanceParameterValues)
					{
						this.m_instanceParameterValues = reader.ReadListOfRIFObjects<List<ParametersImplWrapper>>();
						continue;
					}
				}
				Global.Tracer.Assert(false);
			}
			if (this.m_instanceParameterValues != null)
			{
				this.m_parameterValuesToInfoIndexMap = new Dictionary<ParametersImplWrapper, int>(SubReportInfo.ParametersImplValuesComparer.Instance);
				for (int i = 0; i < this.m_instanceParameterValues.Count; i++)
				{
					this.m_parameterValuesToInfoIndexMap[this.m_instanceParameterValues[i]] = i;
				}
			}
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000CFB59 File Offset: 0x000CDD59
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000CFB66 File Offset: 0x000CDD66
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SubReportInfo;
		}

		// Token: 0x04001827 RID: 6183
		private int m_lastID;

		// Token: 0x04001828 RID: 6184
		private string m_uniqueName;

		// Token: 0x04001829 RID: 6185
		[NonSerialized]
		private Dictionary<ParametersImplWrapper, int> m_parameterValuesToInfoIndexMap;

		// Token: 0x0400182A RID: 6186
		private List<ParametersImplWrapper> m_instanceParameterValues;

		// Token: 0x0400182B RID: 6187
		private int m_userSortDataSetGlobalId = -1;

		// Token: 0x0400182C RID: 6188
		[NonSerialized]
		private CommonSubReportInfo m_commonSubReportInfo;

		// Token: 0x0400182D RID: 6189
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = SubReportInfo.GetDeclaration();

		// Token: 0x02000969 RID: 2409
		internal class ParametersImplValuesComparer : IEqualityComparer<ParametersImplWrapper>
		{
			// Token: 0x0600803F RID: 32831 RVA: 0x00210CF9 File Offset: 0x0020EEF9
			private ParametersImplValuesComparer()
			{
			}

			// Token: 0x06008040 RID: 32832 RVA: 0x00210D01 File Offset: 0x0020EF01
			public bool Equals(ParametersImplWrapper obj1, ParametersImplWrapper obj2)
			{
				return obj1 == obj2 || (obj1 != null && obj2 != null && obj1.ValuesAreEqual(obj2));
			}

			// Token: 0x06008041 RID: 32833 RVA: 0x00210D18 File Offset: 0x0020EF18
			public int GetHashCode(ParametersImplWrapper obj)
			{
				return obj.GetValuesHashCode();
			}

			// Token: 0x040040A1 RID: 16545
			internal static readonly SubReportInfo.ParametersImplValuesComparer Instance = new SubReportInfo.ParametersImplValuesComparer();
		}
	}
}
