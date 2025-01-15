using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Azure
{
	// Token: 0x02000025 RID: 37
	[NullableContext(2)]
	[Nullable(0)]
	public abstract class NullableResponse<T>
	{
		// Token: 0x17000023 RID: 35
		// (get) Token: 0x06000084 RID: 132
		public abstract bool HasValue { get; }

		// Token: 0x17000024 RID: 36
		// (get) Token: 0x06000085 RID: 133
		public abstract T Value { get; }

		// Token: 0x06000086 RID: 134
		[NullableContext(1)]
		public abstract Response GetRawResponse();

		// Token: 0x06000087 RID: 135 RVA: 0x00002CF6 File Offset: 0x00000EF6
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06000088 RID: 136 RVA: 0x00002CFF File Offset: 0x00000EFF
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002D08 File Offset: 0x00000F08
		[NullableContext(1)]
		public override string ToString()
		{
			string text = "Status: {0}, Value: {1}";
			Response rawResponse = this.GetRawResponse();
			return string.Format(text, (rawResponse != null) ? new int?(rawResponse.Status) : null, this.HasValue ? this.Value : "<null>");
		}

		// Token: 0x04000045 RID: 69
		[Nullable(1)]
		private const string NoValue = "<null>";
	}
}
