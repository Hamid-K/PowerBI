using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Text;
using NLog.Internal;

namespace NLog.Filters
{
	// Token: 0x02000179 RID: 377
	[Filter("whenRepeated")]
	public class WhenRepeatedFilter : LayoutBasedFilter
	{
		// Token: 0x17000349 RID: 841
		// (get) Token: 0x06001161 RID: 4449 RVA: 0x0002CE64 File Offset: 0x0002B064
		// (set) Token: 0x06001162 RID: 4450 RVA: 0x0002CE6C File Offset: 0x0002B06C
		[DefaultValue(10)]
		public int TimeoutSeconds { get; set; }

		// Token: 0x1700034A RID: 842
		// (get) Token: 0x06001163 RID: 4451 RVA: 0x0002CE75 File Offset: 0x0002B075
		// (set) Token: 0x06001164 RID: 4452 RVA: 0x0002CE7D File Offset: 0x0002B07D
		[DefaultValue(1000)]
		public int MaxLength { get; set; }

		// Token: 0x1700034B RID: 843
		// (get) Token: 0x06001165 RID: 4453 RVA: 0x0002CE86 File Offset: 0x0002B086
		// (set) Token: 0x06001166 RID: 4454 RVA: 0x0002CE8E File Offset: 0x0002B08E
		[DefaultValue(false)]
		public bool IncludeFirst { get; set; }

		// Token: 0x1700034C RID: 844
		// (get) Token: 0x06001167 RID: 4455 RVA: 0x0002CE97 File Offset: 0x0002B097
		// (set) Token: 0x06001168 RID: 4456 RVA: 0x0002CE9F File Offset: 0x0002B09F
		[DefaultValue(50000)]
		public int MaxFilterCacheSize { get; set; }

		// Token: 0x1700034D RID: 845
		// (get) Token: 0x06001169 RID: 4457 RVA: 0x0002CEA8 File Offset: 0x0002B0A8
		// (set) Token: 0x0600116A RID: 4458 RVA: 0x0002CEB0 File Offset: 0x0002B0B0
		[DefaultValue(1000)]
		public int DefaultFilterCacheSize { get; set; }

		// Token: 0x1700034E RID: 846
		// (get) Token: 0x0600116B RID: 4459 RVA: 0x0002CEB9 File Offset: 0x0002B0B9
		// (set) Token: 0x0600116C RID: 4460 RVA: 0x0002CEC1 File Offset: 0x0002B0C1
		[DefaultValue(null)]
		public string FilterCountPropertyName { get; set; }

		// Token: 0x1700034F RID: 847
		// (get) Token: 0x0600116D RID: 4461 RVA: 0x0002CECA File Offset: 0x0002B0CA
		// (set) Token: 0x0600116E RID: 4462 RVA: 0x0002CED2 File Offset: 0x0002B0D2
		[DefaultValue(null)]
		public string FilterCountMessageAppendFormat { get; set; }

		// Token: 0x17000350 RID: 848
		// (get) Token: 0x0600116F RID: 4463 RVA: 0x0002CEDB File Offset: 0x0002B0DB
		// (set) Token: 0x06001170 RID: 4464 RVA: 0x0002CEE3 File Offset: 0x0002B0E3
		[DefaultValue(true)]
		public bool OptimizeBufferReuse { get; set; }

		// Token: 0x17000351 RID: 849
		// (get) Token: 0x06001171 RID: 4465 RVA: 0x0002CEEC File Offset: 0x0002B0EC
		// (set) Token: 0x06001172 RID: 4466 RVA: 0x0002CEF4 File Offset: 0x0002B0F4
		[DefaultValue(1000)]
		public int OptimizeBufferDefaultLength { get; set; }

		// Token: 0x06001173 RID: 4467 RVA: 0x0002CF00 File Offset: 0x0002B100
		public WhenRepeatedFilter()
		{
			this.TimeoutSeconds = 10;
			this.MaxLength = 1000;
			this.DefaultFilterCacheSize = 1000;
			this.MaxFilterCacheSize = 50000;
			this.OptimizeBufferReuse = true;
			this.OptimizeBufferDefaultLength = this.MaxLength;
		}

		// Token: 0x06001174 RID: 4468 RVA: 0x0002CF7C File Offset: 0x0002B17C
		protected override FilterResult Check(LogEventInfo logEvent)
		{
			FilterResult filterResult = FilterResult.Neutral;
			bool flag = false;
			Dictionary<WhenRepeatedFilter.FilterInfoKey, WhenRepeatedFilter.FilterInfo> repeatFilter = this._repeatFilter;
			lock (repeatFilter)
			{
				using (ReusableObjectCreator<StringBuilder>.LockOject lockOject = (this.OptimizeBufferReuse ? this.ReusableLayoutBuilder.Allocate() : this.ReusableLayoutBuilder.None))
				{
					if (this.OptimizeBufferReuse && lockOject.Result.Capacity != this.OptimizeBufferDefaultLength)
					{
						if (this.OptimizeBufferDefaultLength < 16384)
						{
							this.OptimizeBufferDefaultLength = this.MaxLength;
							while (this.OptimizeBufferDefaultLength < lockOject.Result.Capacity && this.OptimizeBufferDefaultLength < 16384)
							{
								this.OptimizeBufferDefaultLength *= 2;
							}
						}
						lockOject.Result.Capacity = this.OptimizeBufferDefaultLength;
					}
					WhenRepeatedFilter.FilterInfoKey filterInfoKey = this.RenderFilterInfoKey(logEvent, this.OptimizeBufferReuse ? lockOject.Result : null);
					WhenRepeatedFilter.FilterInfo filterInfo;
					if (!this._repeatFilter.TryGetValue(filterInfoKey, out filterInfo))
					{
						filterInfo = this.CreateFilterInfo(logEvent);
						if (this.OptimizeBufferReuse && filterInfo.StringBuffer != null)
						{
							filterInfo.StringBuffer.ClearBuilder();
							int num = Math.Min(lockOject.Result.Length, this.MaxLength);
							for (int i = 0; i < num; i++)
							{
								filterInfo.StringBuffer.Append(lockOject.Result[i]);
							}
						}
						filterInfo.Refresh(logEvent.Level, logEvent.TimeStamp, 0);
						this._repeatFilter.Add(new WhenRepeatedFilter.FilterInfoKey(filterInfo.StringBuffer, filterInfoKey.StringValue, new int?(filterInfoKey.StringHashCode)), filterInfo);
						flag = true;
					}
					else
					{
						if (this.IncludeFirst)
						{
							flag = filterInfo.IsObsolete(logEvent.TimeStamp, this.TimeoutSeconds);
						}
						filterResult = this.RefreshFilterInfo(logEvent, filterInfo);
					}
				}
			}
			if (this.IncludeFirst && flag)
			{
				filterResult = base.Action;
			}
			return filterResult;
		}

		// Token: 0x06001175 RID: 4469 RVA: 0x0002D19C File Offset: 0x0002B39C
		private WhenRepeatedFilter.FilterInfo CreateFilterInfo(LogEventInfo logEvent)
		{
			if (this._objectPool.Count == 0 && this._repeatFilter.Count > this.DefaultFilterCacheSize)
			{
				int num = ((this._repeatFilter.Count > this.MaxFilterCacheSize) ? (this.TimeoutSeconds * 2 / 3) : this.TimeoutSeconds);
				this.PruneFilterCache(logEvent, Math.Max(1, num));
				if (this._repeatFilter.Count > this.MaxFilterCacheSize)
				{
					this.PruneFilterCache(logEvent, Math.Max(1, this.TimeoutSeconds / 2));
				}
			}
			WhenRepeatedFilter.FilterInfo filterInfo;
			if (this._objectPool.Count == 0)
			{
				filterInfo = new WhenRepeatedFilter.FilterInfo(this.OptimizeBufferReuse ? new StringBuilder(this.OptimizeBufferDefaultLength) : null);
			}
			else
			{
				filterInfo = this._objectPool.Pop().Value;
				if (this.OptimizeBufferReuse && filterInfo.StringBuffer != null && filterInfo.StringBuffer.Capacity != this.OptimizeBufferDefaultLength)
				{
					filterInfo.StringBuffer.Capacity = this.OptimizeBufferDefaultLength;
				}
			}
			return filterInfo;
		}

		// Token: 0x06001176 RID: 4470 RVA: 0x0002D29C File Offset: 0x0002B49C
		private void PruneFilterCache(LogEventInfo logEvent, int aggressiveTimeoutSeconds)
		{
			foreach (KeyValuePair<WhenRepeatedFilter.FilterInfoKey, WhenRepeatedFilter.FilterInfo> keyValuePair in this._repeatFilter)
			{
				if (keyValuePair.Value.IsObsolete(logEvent.TimeStamp, aggressiveTimeoutSeconds))
				{
					this._objectPool.Push(keyValuePair);
				}
			}
			foreach (KeyValuePair<WhenRepeatedFilter.FilterInfoKey, WhenRepeatedFilter.FilterInfo> keyValuePair2 in this._objectPool)
			{
				this._repeatFilter.Remove(keyValuePair2.Key);
			}
			if (this._repeatFilter.Count * 2 > this.DefaultFilterCacheSize && this.DefaultFilterCacheSize < this.MaxFilterCacheSize)
			{
				this.DefaultFilterCacheSize *= 2;
			}
			while (this._objectPool.Count != 0 && this._objectPool.Count > this.DefaultFilterCacheSize)
			{
				this._objectPool.Pop();
			}
		}

		// Token: 0x06001177 RID: 4471 RVA: 0x0002D3B8 File Offset: 0x0002B5B8
		private WhenRepeatedFilter.FilterInfoKey RenderFilterInfoKey(LogEventInfo logEvent, StringBuilder targetBuilder)
		{
			if (targetBuilder != null)
			{
				base.Layout.RenderAppendBuilder(logEvent, targetBuilder, false);
				if (targetBuilder.Length > this.MaxLength)
				{
					targetBuilder.Length = this.MaxLength;
				}
				return new WhenRepeatedFilter.FilterInfoKey(targetBuilder, null, null);
			}
			string text = base.Layout.Render(logEvent) ?? string.Empty;
			if (text.Length > this.MaxLength)
			{
				text = text.Substring(0, this.MaxLength);
			}
			return new WhenRepeatedFilter.FilterInfoKey(null, text, null);
		}

		// Token: 0x06001178 RID: 4472 RVA: 0x0002D444 File Offset: 0x0002B644
		private FilterResult RefreshFilterInfo(LogEventInfo logEvent, WhenRepeatedFilter.FilterInfo filterInfo)
		{
			if (filterInfo.HasExpired(logEvent.TimeStamp, this.TimeoutSeconds) || logEvent.Level.Ordinal > filterInfo.LogLevel.Ordinal)
			{
				int num = filterInfo.FilterCount;
				if (num > 0 && filterInfo.IsObsolete(logEvent.TimeStamp, this.TimeoutSeconds))
				{
					num = 0;
				}
				filterInfo.Refresh(logEvent.Level, logEvent.TimeStamp, 0);
				if (num > 0)
				{
					if (!string.IsNullOrEmpty(this.FilterCountPropertyName))
					{
						object obj;
						object obj2;
						if (!logEvent.Properties.TryGetValue(this.FilterCountPropertyName, out obj))
						{
							logEvent.Properties[this.FilterCountPropertyName] = num;
						}
						else if ((obj2 = obj) is int)
						{
							int num2 = (int)obj2;
							num = Math.Max(num2, num);
							logEvent.Properties[this.FilterCountPropertyName] = num;
						}
					}
					if (!string.IsNullOrEmpty(this.FilterCountMessageAppendFormat) && logEvent.Message != null)
					{
						logEvent.Message += string.Format(this.FilterCountMessageAppendFormat, num.ToString(CultureInfo.InvariantCulture));
					}
				}
				return FilterResult.Neutral;
			}
			filterInfo.Refresh(logEvent.Level, logEvent.TimeStamp, filterInfo.FilterCount + 1);
			return base.Action;
		}

		// Token: 0x040004B0 RID: 1200
		private const int MaxInitialRenderBufferLength = 16384;

		// Token: 0x040004BA RID: 1210
		internal readonly ReusableBuilderCreator ReusableLayoutBuilder = new ReusableBuilderCreator();

		// Token: 0x040004BB RID: 1211
		private readonly Dictionary<WhenRepeatedFilter.FilterInfoKey, WhenRepeatedFilter.FilterInfo> _repeatFilter = new Dictionary<WhenRepeatedFilter.FilterInfoKey, WhenRepeatedFilter.FilterInfo>(1000);

		// Token: 0x040004BC RID: 1212
		private readonly Stack<KeyValuePair<WhenRepeatedFilter.FilterInfoKey, WhenRepeatedFilter.FilterInfo>> _objectPool = new Stack<KeyValuePair<WhenRepeatedFilter.FilterInfoKey, WhenRepeatedFilter.FilterInfo>>(1000);

		// Token: 0x0200029A RID: 666
		private class FilterInfo
		{
			// Token: 0x060016D6 RID: 5846 RVA: 0x0003BF09 File Offset: 0x0003A109
			public FilterInfo(StringBuilder stringBuilder)
			{
				this.StringBuffer = stringBuilder;
			}

			// Token: 0x060016D7 RID: 5847 RVA: 0x0003BF18 File Offset: 0x0003A118
			public void Refresh(LogLevel logLevel, DateTime logTimeStamp, int filterCount)
			{
				if (filterCount == 0)
				{
					this.LastLogTime = logTimeStamp;
					this.LogLevel = logLevel;
				}
				else if (this.LogLevel == null || logLevel.Ordinal > this.LogLevel.Ordinal)
				{
					this.LogLevel = logLevel;
				}
				this.LastFilterTime = logTimeStamp;
				this.FilterCount = filterCount;
			}

			// Token: 0x060016D8 RID: 5848 RVA: 0x0003BF70 File Offset: 0x0003A170
			public bool IsObsolete(DateTime logEventTime, int timeoutSeconds)
			{
				if (this.FilterCount == 0)
				{
					return this.HasExpired(logEventTime, timeoutSeconds);
				}
				return (logEventTime - this.LastFilterTime).TotalSeconds > (double)timeoutSeconds && this.HasExpired(logEventTime, timeoutSeconds * 2);
			}

			// Token: 0x060016D9 RID: 5849 RVA: 0x0003BFB4 File Offset: 0x0003A1B4
			public bool HasExpired(DateTime logEventTime, int timeoutSeconds)
			{
				return (logEventTime - this.LastLogTime).TotalSeconds > (double)timeoutSeconds;
			}

			// Token: 0x17000432 RID: 1074
			// (get) Token: 0x060016DA RID: 5850 RVA: 0x0003BFD9 File Offset: 0x0003A1D9
			// (set) Token: 0x060016DB RID: 5851 RVA: 0x0003BFE1 File Offset: 0x0003A1E1
			public StringBuilder StringBuffer { get; private set; }

			// Token: 0x17000433 RID: 1075
			// (get) Token: 0x060016DC RID: 5852 RVA: 0x0003BFEA File Offset: 0x0003A1EA
			// (set) Token: 0x060016DD RID: 5853 RVA: 0x0003BFF2 File Offset: 0x0003A1F2
			public LogLevel LogLevel { get; private set; }

			// Token: 0x17000434 RID: 1076
			// (get) Token: 0x060016DE RID: 5854 RVA: 0x0003BFFB File Offset: 0x0003A1FB
			// (set) Token: 0x060016DF RID: 5855 RVA: 0x0003C003 File Offset: 0x0003A203
			private DateTime LastLogTime { get; set; }

			// Token: 0x17000435 RID: 1077
			// (get) Token: 0x060016E0 RID: 5856 RVA: 0x0003C00C File Offset: 0x0003A20C
			// (set) Token: 0x060016E1 RID: 5857 RVA: 0x0003C014 File Offset: 0x0003A214
			private DateTime LastFilterTime { get; set; }

			// Token: 0x17000436 RID: 1078
			// (get) Token: 0x060016E2 RID: 5858 RVA: 0x0003C01D File Offset: 0x0003A21D
			// (set) Token: 0x060016E3 RID: 5859 RVA: 0x0003C025 File Offset: 0x0003A225
			public int FilterCount { get; private set; }
		}

		// Token: 0x0200029B RID: 667
		private struct FilterInfoKey : IEquatable<WhenRepeatedFilter.FilterInfoKey>
		{
			// Token: 0x060016E4 RID: 5860 RVA: 0x0003C030 File Offset: 0x0003A230
			public FilterInfoKey(StringBuilder stringBuffer, string stringValue, int? stringHashCode = null)
			{
				this._stringBuffer = stringBuffer;
				this.StringValue = stringValue;
				if (stringHashCode != null)
				{
					this.StringHashCode = stringHashCode.Value;
					return;
				}
				if (stringBuffer != null)
				{
					int num = stringBuffer.Length.GetHashCode();
					int num2 = Math.Min(stringBuffer.Length, 100);
					for (int i = 0; i < num2; i++)
					{
						num ^= stringBuffer[i].GetHashCode();
					}
					this.StringHashCode = num;
					return;
				}
				this.StringHashCode = StringComparer.Ordinal.GetHashCode(this.StringValue);
			}

			// Token: 0x060016E5 RID: 5861 RVA: 0x0003C0BF File Offset: 0x0003A2BF
			public override int GetHashCode()
			{
				return this.StringHashCode;
			}

			// Token: 0x060016E6 RID: 5862 RVA: 0x0003C0C8 File Offset: 0x0003A2C8
			public bool Equals(WhenRepeatedFilter.FilterInfoKey other)
			{
				if (this.StringValue != null)
				{
					return string.Equals(this.StringValue, other.StringValue, StringComparison.Ordinal);
				}
				if (this._stringBuffer == null || other._stringBuffer == null)
				{
					return this._stringBuffer == other._stringBuffer && this.StringValue == other.StringValue;
				}
				if (this._stringBuffer.Capacity != other._stringBuffer.Capacity)
				{
					return this._stringBuffer.EqualTo(other._stringBuffer);
				}
				return this._stringBuffer.Equals(other._stringBuffer);
			}

			// Token: 0x060016E7 RID: 5863 RVA: 0x0003C15C File Offset: 0x0003A35C
			public override bool Equals(object obj)
			{
				if (obj is WhenRepeatedFilter.FilterInfoKey)
				{
					WhenRepeatedFilter.FilterInfoKey filterInfoKey = (WhenRepeatedFilter.FilterInfoKey)obj;
					return this.Equals(filterInfoKey);
				}
				return false;
			}

			// Token: 0x0400073F RID: 1855
			private readonly StringBuilder _stringBuffer;

			// Token: 0x04000740 RID: 1856
			public readonly string StringValue;

			// Token: 0x04000741 RID: 1857
			public readonly int StringHashCode;
		}
	}
}
