using System;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x0200024F RID: 591
	[CannotApplyEqualityOperator]
	[Serializable]
	public class MonitoringScopeId : IEquatable<MonitoringScopeId>
	{
		// Token: 0x06000F3B RID: 3899 RVA: 0x0003451C File Offset: 0x0003271C
		public MonitoringScopeId(string name)
		{
			this.m_name = name;
		}

		// Token: 0x06000F3C RID: 3900 RVA: 0x0003452B File Offset: 0x0003272B
		public bool Equals(MonitoringScopeId other)
		{
			return other != null && this.m_name.Equals(other.m_name, StringComparison.Ordinal);
		}

		// Token: 0x06000F3D RID: 3901 RVA: 0x00034544 File Offset: 0x00032744
		public override int GetHashCode()
		{
			return this.m_name.GetHashCode();
		}

		// Token: 0x06000F3E RID: 3902 RVA: 0x00034551 File Offset: 0x00032751
		public override bool Equals(object obj)
		{
			return this.Equals(obj as MonitoringScopeId);
		}

		// Token: 0x06000F3F RID: 3903 RVA: 0x0003455F File Offset: 0x0003275F
		public override string ToString()
		{
			return this.m_name;
		}

		// Token: 0x040005CC RID: 1484
		private readonly string m_name;
	}
}
