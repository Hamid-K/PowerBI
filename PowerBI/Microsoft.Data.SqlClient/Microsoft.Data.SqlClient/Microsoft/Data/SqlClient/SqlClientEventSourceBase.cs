using System;
using System.Diagnostics.Tracing;

namespace Microsoft.Data.SqlClient
{
	// Token: 0x0200005B RID: 91
	internal abstract class SqlClientEventSourceBase : EventSource
	{
		// Token: 0x06000864 RID: 2148 RVA: 0x0001347C File Offset: 0x0001167C
		protected override void OnEventCommand(EventCommandEventArgs command)
		{
			base.OnEventCommand(command);
			this.EventCommandMethodCall(command);
		}

		// Token: 0x06000865 RID: 2149 RVA: 0x0000BB08 File Offset: 0x00009D08
		protected virtual void EventCommandMethodCall(EventCommandEventArgs command)
		{
		}

		// Token: 0x06000866 RID: 2150 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void HardConnectRequest()
		{
		}

		// Token: 0x06000867 RID: 2151 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void HardDisconnectRequest()
		{
		}

		// Token: 0x06000868 RID: 2152 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void SoftConnectRequest()
		{
		}

		// Token: 0x06000869 RID: 2153 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void SoftDisconnectRequest()
		{
		}

		// Token: 0x0600086A RID: 2154 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void EnterNonPooledConnection()
		{
		}

		// Token: 0x0600086B RID: 2155 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void ExitNonPooledConnection()
		{
		}

		// Token: 0x0600086C RID: 2156 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void EnterPooledConnection()
		{
		}

		// Token: 0x0600086D RID: 2157 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void ExitPooledConnection()
		{
		}

		// Token: 0x0600086E RID: 2158 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void EnterActiveConnectionPoolGroup()
		{
		}

		// Token: 0x0600086F RID: 2159 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void ExitActiveConnectionPoolGroup()
		{
		}

		// Token: 0x06000870 RID: 2160 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void EnterInactiveConnectionPoolGroup()
		{
		}

		// Token: 0x06000871 RID: 2161 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void ExitInactiveConnectionPoolGroup()
		{
		}

		// Token: 0x06000872 RID: 2162 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void EnterActiveConnectionPool()
		{
		}

		// Token: 0x06000873 RID: 2163 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void ExitActiveConnectionPool()
		{
		}

		// Token: 0x06000874 RID: 2164 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void EnterInactiveConnectionPool()
		{
		}

		// Token: 0x06000875 RID: 2165 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void ExitInactiveConnectionPool()
		{
		}

		// Token: 0x06000876 RID: 2166 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void EnterActiveConnection()
		{
		}

		// Token: 0x06000877 RID: 2167 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void ExitActiveConnection()
		{
		}

		// Token: 0x06000878 RID: 2168 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void EnterFreeConnection()
		{
		}

		// Token: 0x06000879 RID: 2169 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void ExitFreeConnection()
		{
		}

		// Token: 0x0600087A RID: 2170 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void EnterStasisConnection()
		{
		}

		// Token: 0x0600087B RID: 2171 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void ExitStasisConnection()
		{
		}

		// Token: 0x0600087C RID: 2172 RVA: 0x0000BB08 File Offset: 0x00009D08
		internal virtual void ReclaimedConnectionRequest()
		{
		}
	}
}
