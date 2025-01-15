using System;

namespace Microsoft.ProgramSynthesis.Transformation.Formula.Semantics.Learning
{
	// Token: 0x02001671 RID: 5745
	public class FindNumberCacheItem
	{
		// Token: 0x170020D3 RID: 8403
		// (get) Token: 0x0600C018 RID: 49176 RVA: 0x002963DB File Offset: 0x002945DB
		// (set) Token: 0x0600C019 RID: 49177 RVA: 0x002963E3 File Offset: 0x002945E3
		public int StartIndex { get; set; }

		// Token: 0x170020D4 RID: 8404
		// (get) Token: 0x0600C01A RID: 49178 RVA: 0x002963EC File Offset: 0x002945EC
		public int EndIndex
		{
			get
			{
				int num = this._endIndex.GetValueOrDefault();
				if (this._endIndex == null)
				{
					num = this.StartIndex + this.Substring.Length - 1;
					this._endIndex = new int?(num);
					return num;
				}
				return num;
			}
		}

		// Token: 0x170020D5 RID: 8405
		// (get) Token: 0x0600C01B RID: 49179 RVA: 0x00296436 File Offset: 0x00294636
		// (set) Token: 0x0600C01C RID: 49180 RVA: 0x0029643E File Offset: 0x0029463E
		public string Locale { get; set; }

		// Token: 0x170020D6 RID: 8406
		// (get) Token: 0x0600C01D RID: 49181 RVA: 0x00296447 File Offset: 0x00294647
		// (set) Token: 0x0600C01E RID: 49182 RVA: 0x0029644F File Offset: 0x0029464F
		public string Substring { get; set; }

		// Token: 0x170020D7 RID: 8407
		// (get) Token: 0x0600C01F RID: 49183 RVA: 0x00296458 File Offset: 0x00294658
		// (set) Token: 0x0600C020 RID: 49184 RVA: 0x00296460 File Offset: 0x00294660
		public decimal Value { get; set; }

		// Token: 0x0600C021 RID: 49185 RVA: 0x00296469 File Offset: 0x00294669
		public override string ToString()
		{
			return string.Format("Locale={0}, Value={1}, Substring={2}", this.Locale, this.Value, this.Substring);
		}

		// Token: 0x04004A20 RID: 18976
		private int? _endIndex;
	}
}
