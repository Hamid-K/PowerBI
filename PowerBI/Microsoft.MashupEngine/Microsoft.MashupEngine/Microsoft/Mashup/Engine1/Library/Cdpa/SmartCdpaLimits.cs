using System;
using System.Runtime.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Cdpa
{
	// Token: 0x02000DF9 RID: 3577
	[DataContract]
	internal class SmartCdpaLimits : CdpaLimits
	{
		// Token: 0x06006066 RID: 24678 RVA: 0x00149906 File Offset: 0x00147B06
		public override int GetHashCode()
		{
			return base.Count;
		}

		// Token: 0x06006067 RID: 24679 RVA: 0x0014990E File Offset: 0x00147B0E
		public override bool Equals(CdpaLimits other)
		{
			return this.Equals(other as SmartCdpaLimits);
		}

		// Token: 0x06006068 RID: 24680 RVA: 0x0014991C File Offset: 0x00147B1C
		public bool Equals(SmartCdpaLimits other)
		{
			return other != null && base.Count == other.Count;
		}

		// Token: 0x06006069 RID: 24681 RVA: 0x00149934 File Offset: 0x00147B34
		public override CdpaLimits Intersect(CdpaLimits other)
		{
			SmartCdpaLimits smartCdpaLimits = other as SmartCdpaLimits;
			if (smartCdpaLimits != null)
			{
				return new SmartCdpaLimits
				{
					Count = Math.Min(base.Count, smartCdpaLimits.Count)
				};
			}
			throw new NotSupportedException();
		}

		// Token: 0x0600606A RID: 24682 RVA: 0x00149970 File Offset: 0x00147B70
		public override CdpaLimits Union(CdpaLimits other)
		{
			SmartCdpaLimits smartCdpaLimits = other as SmartCdpaLimits;
			if (smartCdpaLimits != null)
			{
				return new SmartCdpaLimits
				{
					Count = Math.Max(base.Count, smartCdpaLimits.Count)
				};
			}
			throw new NotSupportedException();
		}
	}
}
