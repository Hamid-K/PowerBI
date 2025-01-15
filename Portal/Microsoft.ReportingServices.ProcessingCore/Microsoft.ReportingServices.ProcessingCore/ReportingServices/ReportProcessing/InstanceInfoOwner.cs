using System;
using Microsoft.ReportingServices.ReportProcessing.Persistence;

namespace Microsoft.ReportingServices.ReportProcessing
{
	// Token: 0x02000717 RID: 1815
	[Serializable]
	internal abstract class InstanceInfoOwner
	{
		// Token: 0x170023CF RID: 9167
		// (get) Token: 0x0600652A RID: 25898 RVA: 0x0018F163 File Offset: 0x0018D363
		// (set) Token: 0x0600652B RID: 25899 RVA: 0x0018F192 File Offset: 0x0018D392
		internal OffsetInfo OffsetInfo
		{
			get
			{
				if (this.m_instanceInfo == null)
				{
					return null;
				}
				Global.Tracer.Assert(this.m_instanceInfo is OffsetInfo);
				return (OffsetInfo)this.m_instanceInfo;
			}
			set
			{
				this.m_instanceInfo = value;
			}
		}

		// Token: 0x170023D0 RID: 9168
		// (get) Token: 0x0600652C RID: 25900 RVA: 0x0018F19B File Offset: 0x0018D39B
		internal long ChunkOffset
		{
			get
			{
				if (this.m_instanceInfo == null || !(this.m_instanceInfo is OffsetInfo))
				{
					return 0L;
				}
				return ((OffsetInfo)this.m_instanceInfo).Offset;
			}
		}

		// Token: 0x0600652D RID: 25901 RVA: 0x0018F1C5 File Offset: 0x0018D3C5
		internal void SetOffset(long offset)
		{
			this.m_instanceInfo = new OffsetInfo(offset);
		}

		// Token: 0x0600652E RID: 25902 RVA: 0x0018F1D4 File Offset: 0x0018D3D4
		internal static Declaration GetDeclaration()
		{
			return new Declaration(ObjectType.None, new MemberInfoList
			{
				new MemberInfo(MemberName.OffsetInfo, ObjectType.OffsetInfo)
			});
		}

		// Token: 0x040032A1 RID: 12961
		protected InfoBase m_instanceInfo;
	}
}
