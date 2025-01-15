using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000510 RID: 1296
	[Serializable]
	internal sealed class SortFilterEventInfoMap : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06004505 RID: 17669 RVA: 0x00120339 File Offset: 0x0011E539
		internal SortFilterEventInfoMap()
		{
		}

		// Token: 0x17001CFA RID: 7418
		internal SortFilterEventInfo this[string eventSourceUniqueName]
		{
			get
			{
				SortFilterEventInfo sortFilterEventInfo;
				this.m_sortFilterEventInfos.TryGetValue(eventSourceUniqueName, out sortFilterEventInfo);
				return sortFilterEventInfo;
			}
		}

		// Token: 0x17001CFB RID: 7419
		// (get) Token: 0x06004507 RID: 17671 RVA: 0x00120361 File Offset: 0x0011E561
		internal int Count
		{
			get
			{
				if (this.m_sortFilterEventInfos == null)
				{
					return 0;
				}
				return this.m_sortFilterEventInfos.Count;
			}
		}

		// Token: 0x06004508 RID: 17672 RVA: 0x00120378 File Offset: 0x0011E578
		internal void Add(string eventSourceUniqueName, SortFilterEventInfo eventInfo)
		{
			if (this.m_sortFilterEventInfos == null)
			{
				this.m_sortFilterEventInfos = new Dictionary<string, SortFilterEventInfo>();
			}
			this.m_sortFilterEventInfos.Add(eventSourceUniqueName, eventInfo);
		}

		// Token: 0x06004509 RID: 17673 RVA: 0x0012039C File Offset: 0x0011E59C
		internal void Merge(SortFilterEventInfoMap partition)
		{
			if (partition.Count == 0)
			{
				return;
			}
			foreach (KeyValuePair<string, SortFilterEventInfo> keyValuePair in partition.m_sortFilterEventInfos)
			{
				this.Add(keyValuePair.Key, keyValuePair.Value);
			}
		}

		// Token: 0x0600450A RID: 17674 RVA: 0x00120408 File Offset: 0x0011E608
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortFilterEventInfoMap, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.SortFilterEventInfos, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.StringRIFObjectDictionary, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortFilterEventInfo)
			});
		}

		// Token: 0x0600450B RID: 17675 RVA: 0x00120440 File Offset: 0x0011E640
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(SortFilterEventInfoMap.m_Declaration);
			while (writer.NextMember())
			{
				if (writer.CurrentMember.MemberName == MemberName.SortFilterEventInfos)
				{
					writer.WriteStringRIFObjectDictionary<SortFilterEventInfo>(this.m_sortFilterEventInfos);
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x0600450C RID: 17676 RVA: 0x00120494 File Offset: 0x0011E694
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(SortFilterEventInfoMap.m_Declaration);
			while (reader.NextMember())
			{
				if (reader.CurrentMember.MemberName == MemberName.SortFilterEventInfos)
				{
					this.m_sortFilterEventInfos = reader.ReadStringRIFObjectDictionary<SortFilterEventInfo>();
				}
				else
				{
					Global.Tracer.Assert(false);
				}
			}
		}

		// Token: 0x0600450D RID: 17677 RVA: 0x001204E5 File Offset: 0x0011E6E5
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x0600450E RID: 17678 RVA: 0x001204F2 File Offset: 0x0011E6F2
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortFilterEventInfoMap;
		}

		// Token: 0x04001F2F RID: 7983
		private Dictionary<string, SortFilterEventInfo> m_sortFilterEventInfos;

		// Token: 0x04001F30 RID: 7984
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = SortFilterEventInfoMap.GetDeclaration();
	}
}
