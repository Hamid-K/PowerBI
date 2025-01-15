using System;
using System.Collections.Generic;

namespace Microsoft.Owin.BuilderProperties
{
	// Token: 0x02000049 RID: 73
	public struct Capabilities
	{
		// Token: 0x060002A5 RID: 677 RVA: 0x0000750A File Offset: 0x0000570A
		public Capabilities(IDictionary<string, object> dictionary)
		{
			this._dictionary = dictionary;
		}

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x060002A6 RID: 678 RVA: 0x00007513 File Offset: 0x00005713
		public IDictionary<string, object> Dictionary
		{
			get
			{
				return this._dictionary;
			}
		}

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x060002A7 RID: 679 RVA: 0x0000751B File Offset: 0x0000571B
		// (set) Token: 0x060002A8 RID: 680 RVA: 0x00007528 File Offset: 0x00005728
		public string SendFileVersion
		{
			get
			{
				return this.Get<string>("sendfile.Version");
			}
			set
			{
				this.Set("sendfile.Version", value);
			}
		}

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x060002A9 RID: 681 RVA: 0x00007537 File Offset: 0x00005737
		// (set) Token: 0x060002AA RID: 682 RVA: 0x00007544 File Offset: 0x00005744
		public string WebSocketVersion
		{
			get
			{
				return this.Get<string>("websocket.Version");
			}
			set
			{
				this.Set("websocket.Version", value);
			}
		}

		// Token: 0x060002AB RID: 683 RVA: 0x00007553 File Offset: 0x00005753
		public static Capabilities Create()
		{
			return new Capabilities(new Dictionary<string, object>());
		}

		// Token: 0x060002AC RID: 684 RVA: 0x0000755F File Offset: 0x0000575F
		public bool Equals(Capabilities other)
		{
			return object.Equals(this._dictionary, other._dictionary);
		}

		// Token: 0x060002AD RID: 685 RVA: 0x00007572 File Offset: 0x00005772
		public override bool Equals(object obj)
		{
			return obj is Capabilities && this.Equals((Capabilities)obj);
		}

		// Token: 0x060002AE RID: 686 RVA: 0x0000758A File Offset: 0x0000578A
		public override int GetHashCode()
		{
			if (this._dictionary == null)
			{
				return 0;
			}
			return this._dictionary.GetHashCode();
		}

		// Token: 0x060002AF RID: 687 RVA: 0x000075A1 File Offset: 0x000057A1
		public static bool operator ==(Capabilities left, Capabilities right)
		{
			return left.Equals(right);
		}

		// Token: 0x060002B0 RID: 688 RVA: 0x000075AB File Offset: 0x000057AB
		public static bool operator !=(Capabilities left, Capabilities right)
		{
			return !left.Equals(right);
		}

		// Token: 0x060002B1 RID: 689 RVA: 0x000075B8 File Offset: 0x000057B8
		public T Get<T>(string key)
		{
			object value;
			if (!this._dictionary.TryGetValue(key, out value))
			{
				return default(T);
			}
			return (T)((object)value);
		}

		// Token: 0x060002B2 RID: 690 RVA: 0x000075E5 File Offset: 0x000057E5
		public Capabilities Set(string key, object value)
		{
			this._dictionary[key] = value;
			return this;
		}

		// Token: 0x04000081 RID: 129
		private readonly IDictionary<string, object> _dictionary;
	}
}
