using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Azure
{
	// Token: 0x02000028 RID: 40
	[NullableContext(1)]
	[Nullable(0)]
	public abstract class Page<[Nullable(2)] T>
	{
		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000A8 RID: 168
		public abstract IReadOnlyList<T> Values { get; }

		// Token: 0x1700002A RID: 42
		// (get) Token: 0x060000A9 RID: 169
		[Nullable(2)]
		public abstract string ContinuationToken
		{
			[NullableContext(2)]
			get;
		}

		// Token: 0x060000AA RID: 170
		public abstract Response GetRawResponse();

		// Token: 0x060000AB RID: 171 RVA: 0x00003123 File Offset: 0x00001323
		public static Page<T> FromValues(IReadOnlyList<T> values, [Nullable(2)] string continuationToken, Response response)
		{
			return new Page<T>.PageCore(values, continuationToken, response);
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000312D File Offset: 0x0000132D
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060000AD RID: 173 RVA: 0x00003135 File Offset: 0x00001335
		[NullableContext(2)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x0000313E File Offset: 0x0000133E
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x020000D8 RID: 216
		[Nullable(new byte[] { 0, 1 })]
		private class PageCore : Page<T>
		{
			// Token: 0x060006F1 RID: 1777 RVA: 0x00017CA2 File Offset: 0x00015EA2
			public PageCore(IReadOnlyList<T> values, [Nullable(2)] string continuationToken, Response response)
			{
				this._response = response;
				this.Values = values;
				this.ContinuationToken = continuationToken;
			}

			// Token: 0x170001A7 RID: 423
			// (get) Token: 0x060006F2 RID: 1778 RVA: 0x00017CBF File Offset: 0x00015EBF
			public override IReadOnlyList<T> Values { get; }

			// Token: 0x170001A8 RID: 424
			// (get) Token: 0x060006F3 RID: 1779 RVA: 0x00017CC7 File Offset: 0x00015EC7
			[Nullable(2)]
			public override string ContinuationToken
			{
				[NullableContext(2)]
				get;
			}

			// Token: 0x060006F4 RID: 1780 RVA: 0x00017CCF File Offset: 0x00015ECF
			public override Response GetRawResponse()
			{
				return this._response;
			}

			// Token: 0x040002EE RID: 750
			private readonly Response _response;
		}
	}
}
