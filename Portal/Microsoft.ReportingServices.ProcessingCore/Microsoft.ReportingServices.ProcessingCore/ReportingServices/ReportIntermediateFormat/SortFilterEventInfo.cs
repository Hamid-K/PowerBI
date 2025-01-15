using System;
using System.Collections.Generic;
using Microsoft.ReportingServices.ReportIntermediateFormat.Persistence;
using Microsoft.ReportingServices.ReportProcessing;

namespace Microsoft.ReportingServices.ReportIntermediateFormat
{
	// Token: 0x02000511 RID: 1297
	[Serializable]
	internal sealed class SortFilterEventInfo : Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IPersistable
	{
		// Token: 0x06004510 RID: 17680 RVA: 0x00120505 File Offset: 0x0011E705
		internal SortFilterEventInfo()
		{
		}

		// Token: 0x06004511 RID: 17681 RVA: 0x0012050D File Offset: 0x0011E70D
		internal SortFilterEventInfo(IInScopeEventSource eventSource)
		{
			this.m_eventSource = eventSource;
		}

		// Token: 0x17001CFC RID: 7420
		// (get) Token: 0x06004512 RID: 17682 RVA: 0x0012051C File Offset: 0x0011E71C
		// (set) Token: 0x06004513 RID: 17683 RVA: 0x00120524 File Offset: 0x0011E724
		internal IInScopeEventSource EventSource
		{
			get
			{
				return this.m_eventSource;
			}
			set
			{
				this.m_eventSource = value;
			}
		}

		// Token: 0x17001CFD RID: 7421
		// (get) Token: 0x06004514 RID: 17684 RVA: 0x0012052D File Offset: 0x0011E72D
		// (set) Token: 0x06004515 RID: 17685 RVA: 0x00120535 File Offset: 0x0011E735
		internal List<object>[] EventSourceScopeInfo
		{
			get
			{
				return this.m_eventSourceScopeInfo;
			}
			set
			{
				this.m_eventSourceScopeInfo = value;
			}
		}

		// Token: 0x06004516 RID: 17686 RVA: 0x00120540 File Offset: 0x0011E740
		internal static Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration GetDeclaration()
		{
			return new Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortFilterEventInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.None, new List<MemberInfo>
			{
				new MemberInfo(MemberName.EventSource, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.IInScopeEventSource, Token.GlobalReference),
				new MemberInfo(MemberName.EventSourceScopeInfo, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveArray, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.PrimitiveList)
			});
		}

		// Token: 0x06004517 RID: 17687 RVA: 0x00120588 File Offset: 0x0011E788
		public void Serialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatWriter writer)
		{
			writer.RegisterDeclaration(SortFilterEventInfo.m_Declaration);
			while (writer.NextMember())
			{
				MemberName memberName = writer.CurrentMember.MemberName;
				if (memberName != MemberName.EventSource)
				{
					if (memberName != MemberName.EventSourceScopeInfo)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						writer.WriteArrayOfListsOfPrimitives<object>(this.m_eventSourceScopeInfo);
					}
				}
				else
				{
					writer.WriteGlobalReference(this.m_eventSource);
				}
			}
		}

		// Token: 0x06004518 RID: 17688 RVA: 0x001205F4 File Offset: 0x0011E7F4
		public void Deserialize(Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IntermediateFormatReader reader)
		{
			reader.RegisterDeclaration(SortFilterEventInfo.m_Declaration);
			while (reader.NextMember())
			{
				MemberName memberName = reader.CurrentMember.MemberName;
				if (memberName != MemberName.EventSource)
				{
					if (memberName != MemberName.EventSourceScopeInfo)
					{
						Global.Tracer.Assert(false);
					}
					else
					{
						this.m_eventSourceScopeInfo = reader.ReadArrayOfListsOfPrimitives<object>();
					}
				}
				else
				{
					this.m_eventSource = reader.ReadGlobalReference<IInScopeEventSource>();
				}
			}
		}

		// Token: 0x06004519 RID: 17689 RVA: 0x00120660 File Offset: 0x0011E860
		public void ResolveReferences(Dictionary<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType, List<Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.MemberReference>> memberReferencesCollection, Dictionary<int, Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.IReferenceable> referenceableItems)
		{
			Global.Tracer.Assert(false);
		}

		// Token: 0x0600451A RID: 17690 RVA: 0x0012066D File Offset: 0x0011E86D
		public Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType GetObjectType()
		{
			return Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.ObjectType.SortFilterEventInfo;
		}

		// Token: 0x04001F31 RID: 7985
		[Reference]
		private IInScopeEventSource m_eventSource;

		// Token: 0x04001F32 RID: 7986
		private List<object>[] m_eventSourceScopeInfo;

		// Token: 0x04001F33 RID: 7987
		[NonSerialized]
		private static readonly Microsoft.ReportingServices.ReportIntermediateFormat.Persistence.Declaration m_Declaration = SortFilterEventInfo.GetDeclaration();
	}
}
