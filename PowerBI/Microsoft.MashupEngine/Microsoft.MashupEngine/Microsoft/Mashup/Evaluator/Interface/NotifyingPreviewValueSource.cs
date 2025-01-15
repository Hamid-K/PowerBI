using System;
using Microsoft.Mashup.Engine.Interface;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001E3F RID: 7743
	public sealed class NotifyingPreviewValueSource : IPreviewValueSource, IDisposable
	{
		// Token: 0x0600BE6B RID: 48747 RVA: 0x00268314 File Offset: 0x00266514
		public NotifyingPreviewValueSource(IPreviewValueSource previewValueSource, Action callback)
		{
			this.previewValueSource = previewValueSource;
			this.callback = callback;
		}

		// Token: 0x17002ED5 RID: 11989
		// (get) Token: 0x0600BE6C RID: 48748 RVA: 0x0026832A File Offset: 0x0026652A
		public bool IsComplete
		{
			get
			{
				return this.previewValueSource.IsComplete;
			}
		}

		// Token: 0x17002ED6 RID: 11990
		// (get) Token: 0x0600BE6D RID: 48749 RVA: 0x00268337 File Offset: 0x00266537
		public ITableSource TableSource
		{
			get
			{
				return this.previewValueSource.TableSource;
			}
		}

		// Token: 0x17002ED7 RID: 11991
		// (get) Token: 0x0600BE6E RID: 48750 RVA: 0x00268344 File Offset: 0x00266544
		public string SmallValue
		{
			get
			{
				return this.previewValueSource.SmallValue;
			}
		}

		// Token: 0x17002ED8 RID: 11992
		// (get) Token: 0x0600BE6F RID: 48751 RVA: 0x00268351 File Offset: 0x00266551
		public string Value
		{
			get
			{
				return this.previewValueSource.Value;
			}
		}

		// Token: 0x0600BE70 RID: 48752 RVA: 0x0026835E File Offset: 0x0026655E
		public void Dispose()
		{
			if (this.callback != null)
			{
				Action action = this.callback;
				this.callback = null;
				action();
			}
			if (this.previewValueSource != null)
			{
				this.previewValueSource.Dispose();
				this.previewValueSource = null;
			}
		}

		// Token: 0x040060FC RID: 24828
		private IPreviewValueSource previewValueSource;

		// Token: 0x040060FD RID: 24829
		private Action callback;
	}
}
