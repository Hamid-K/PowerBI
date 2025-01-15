using System;

namespace Microsoft.ApplicationServer.Caching
{
	// Token: 0x020002AA RID: 682
	internal class EndpointID : IEquatable<EndpointID>
	{
		// Token: 0x17000546 RID: 1350
		// (get) Token: 0x060018FD RID: 6397 RVA: 0x0004B144 File Offset: 0x00049344
		// (set) Token: 0x060018FE RID: 6398 RVA: 0x0004B14C File Offset: 0x0004934C
		internal string UriString
		{
			get
			{
				return this._uriString;
			}
			set
			{
				this._uriString = value;
				this._uri = new VelocityUri(this._uriString);
			}
		}

		// Token: 0x17000547 RID: 1351
		// (get) Token: 0x060018FF RID: 6399 RVA: 0x0004B166 File Offset: 0x00049366
		internal Uri URI
		{
			get
			{
				return this._uri;
			}
		}

		// Token: 0x06001900 RID: 6400 RVA: 0x00002061 File Offset: 0x00000261
		internal EndpointID()
		{
		}

		// Token: 0x06001901 RID: 6401 RVA: 0x0004B16E File Offset: 0x0004936E
		internal EndpointID(string uriString)
		{
			this._uriString = uriString;
			this._uri = new VelocityUri(uriString);
		}

		// Token: 0x06001902 RID: 6402 RVA: 0x0004B189 File Offset: 0x00049389
		internal bool IsTcp()
		{
			return this._uriString.StartsWith(EndpointID.TcpIdentifier, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001903 RID: 6403 RVA: 0x0004B19C File Offset: 0x0004939C
		public bool Equals(EndpointID other)
		{
			return other != null && this._uriString.Equals(other._uriString, StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x06001904 RID: 6404 RVA: 0x0004B1B5 File Offset: 0x000493B5
		public override int GetHashCode()
		{
			return this._uri.GetHashCode();
		}

		// Token: 0x06001905 RID: 6405 RVA: 0x0004B1C2 File Offset: 0x000493C2
		public override string ToString()
		{
			return "[" + this._uriString + "]";
		}

		// Token: 0x04000D9F RID: 3487
		internal static string TcpIdentifier = "net.tcp://";

		// Token: 0x04000DA0 RID: 3488
		private string _uriString;

		// Token: 0x04000DA1 RID: 3489
		private VelocityUri _uri;
	}
}
