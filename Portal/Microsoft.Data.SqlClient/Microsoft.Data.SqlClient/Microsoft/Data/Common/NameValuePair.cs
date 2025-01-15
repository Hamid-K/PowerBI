using System;
using System.Runtime.Serialization;

namespace Microsoft.Data.Common
{
	// Token: 0x02000180 RID: 384
	[Serializable]
	internal sealed class NameValuePair
	{
		// Token: 0x06001CCF RID: 7375 RVA: 0x00075904 File Offset: 0x00073B04
		internal NameValuePair(string name, string value, int length)
		{
			this._name = name;
			this._value = value;
			this._length = length;
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06001CD0 RID: 7376 RVA: 0x00075921 File Offset: 0x00073B21
		internal int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06001CD1 RID: 7377 RVA: 0x00075929 File Offset: 0x00073B29
		internal string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06001CD2 RID: 7378 RVA: 0x00075931 File Offset: 0x00073B31
		internal string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06001CD3 RID: 7379 RVA: 0x00075939 File Offset: 0x00073B39
		// (set) Token: 0x06001CD4 RID: 7380 RVA: 0x00075941 File Offset: 0x00073B41
		internal NameValuePair Next
		{
			get
			{
				return this._next;
			}
			set
			{
				if (this._next != null || value == null)
				{
					throw ADP.InternalError(ADP.InternalErrorCode.NameValuePairNext);
				}
				this._next = value;
			}
		}

		// Token: 0x04000C18 RID: 3096
		private readonly string _name;

		// Token: 0x04000C19 RID: 3097
		private readonly string _value;

		// Token: 0x04000C1A RID: 3098
		[OptionalField(VersionAdded = 2)]
		private readonly int _length;

		// Token: 0x04000C1B RID: 3099
		private NameValuePair _next;
	}
}
