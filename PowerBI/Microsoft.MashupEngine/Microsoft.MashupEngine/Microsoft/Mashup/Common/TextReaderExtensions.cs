using System;
using System.IO;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001C2C RID: 7212
	public static class TextReaderExtensions
	{
		// Token: 0x0600B40F RID: 46095 RVA: 0x00248DD6 File Offset: 0x00246FD6
		public static TextReader OnDispose(this TextReader reader, Action action)
		{
			return new TextReaderExtensions.NotifyingReader(reader, action);
		}

		// Token: 0x0600B410 RID: 46096 RVA: 0x00248DE0 File Offset: 0x00246FE0
		public static TextReader AfterDispose(this TextReader reader, Action action)
		{
			return new TextReaderExtensions.NotifyingReader(reader, delegate
			{
				try
				{
					reader.Dispose();
				}
				finally
				{
					action();
				}
			});
		}

		// Token: 0x02001C2D RID: 7213
		private sealed class NotifyingReader : DelegatingTextReader
		{
			// Token: 0x0600B411 RID: 46097 RVA: 0x00248E18 File Offset: 0x00247018
			public NotifyingReader(TextReader reader, Action callback)
				: base(reader)
			{
				this.callback = callback;
			}

			// Token: 0x0600B412 RID: 46098 RVA: 0x00248E28 File Offset: 0x00247028
			public override void Close()
			{
				this.HandleCallback();
				base.Close();
			}

			// Token: 0x0600B413 RID: 46099 RVA: 0x00248E36 File Offset: 0x00247036
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.HandleCallback();
				}
				base.Dispose(disposing);
			}

			// Token: 0x0600B414 RID: 46100 RVA: 0x00248E48 File Offset: 0x00247048
			private void HandleCallback()
			{
				if (this.callback != null)
				{
					Action action = this.callback;
					this.callback = null;
					action();
				}
			}

			// Token: 0x04005BAB RID: 23467
			private Action callback;
		}
	}
}
