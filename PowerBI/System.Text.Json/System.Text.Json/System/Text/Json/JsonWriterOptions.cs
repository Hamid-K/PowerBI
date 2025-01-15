using System;
using System.Runtime.CompilerServices;
using System.Text.Encodings.Web;

namespace System.Text.Json
{
	// Token: 0x0200005B RID: 91
	[NullableContext(2)]
	[Nullable(0)]
	public struct JsonWriterOptions
	{
		// Token: 0x17000167 RID: 359
		// (get) Token: 0x06000557 RID: 1367 RVA: 0x00016621 File Offset: 0x00014821
		// (set) Token: 0x06000558 RID: 1368 RVA: 0x00016629 File Offset: 0x00014829
		public JavaScriptEncoder Encoder { readonly get; set; }

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x06000559 RID: 1369 RVA: 0x00016632 File Offset: 0x00014832
		// (set) Token: 0x0600055A RID: 1370 RVA: 0x0001663F File Offset: 0x0001483F
		public bool Indented
		{
			get
			{
				return (this._optionsMask & 1) != 0;
			}
			set
			{
				if (value)
				{
					this._optionsMask |= 1;
					return;
				}
				this._optionsMask &= -2;
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x0600055B RID: 1371 RVA: 0x00016662 File Offset: 0x00014862
		// (set) Token: 0x0600055C RID: 1372 RVA: 0x0001666A File Offset: 0x0001486A
		public int MaxDepth
		{
			readonly get
			{
				return this._maxDepth;
			}
			set
			{
				if (value < 0)
				{
					ThrowHelper.ThrowArgumentOutOfRangeException_MaxDepthMustBePositive("value");
				}
				this._maxDepth = value;
			}
		}

		// Token: 0x1700016A RID: 362
		// (get) Token: 0x0600055D RID: 1373 RVA: 0x00016681 File Offset: 0x00014881
		// (set) Token: 0x0600055E RID: 1374 RVA: 0x0001668E File Offset: 0x0001488E
		public bool SkipValidation
		{
			get
			{
				return (this._optionsMask & 2) != 0;
			}
			set
			{
				if (value)
				{
					this._optionsMask |= 2;
					return;
				}
				this._optionsMask &= -3;
			}
		}

		// Token: 0x1700016B RID: 363
		// (get) Token: 0x0600055F RID: 1375 RVA: 0x000166B1 File Offset: 0x000148B1
		internal bool IndentedOrNotSkipValidation
		{
			get
			{
				return this._optionsMask != 2;
			}
		}

		// Token: 0x0400026A RID: 618
		internal const int DefaultMaxDepth = 1000;

		// Token: 0x0400026B RID: 619
		private int _maxDepth;

		// Token: 0x0400026C RID: 620
		private int _optionsMask;

		// Token: 0x0400026E RID: 622
		private const int IndentBit = 1;

		// Token: 0x0400026F RID: 623
		private const int SkipValidationBit = 2;
	}
}
