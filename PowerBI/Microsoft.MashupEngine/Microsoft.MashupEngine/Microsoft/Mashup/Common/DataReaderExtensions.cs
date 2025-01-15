using System;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Common
{
	// Token: 0x02001BDD RID: 7133
	public static class DataReaderExtensions
	{
		// Token: 0x0600B216 RID: 45590 RVA: 0x00245164 File Offset: 0x00243364
		public static IDataReaderWithTableSchema AfterDispose(this IDataReaderWithTableSchema reader, Action action)
		{
			return reader.OnDispose(delegate
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

		// Token: 0x0600B217 RID: 45591 RVA: 0x0024519C File Offset: 0x0024339C
		public static IDataReaderWithTableSchema OnDispose(this IDataReaderWithTableSchema reader, Action action)
		{
			return new DataReaderExtensions.NotifyingDataReader(reader, action);
		}

		// Token: 0x02001BDE RID: 7134
		private sealed class NotifyingDataReader : DelegatingDataReaderWithTableSchema
		{
			// Token: 0x0600B218 RID: 45592 RVA: 0x002451A5 File Offset: 0x002433A5
			public NotifyingDataReader(IDataReaderWithTableSchema reader, Action callback)
				: base(reader)
			{
				this.callback = callback;
			}

			// Token: 0x0600B219 RID: 45593 RVA: 0x002451B5 File Offset: 0x002433B5
			public override void Close()
			{
				this.HandleCallback();
				base.Close();
			}

			// Token: 0x0600B21A RID: 45594 RVA: 0x002451C3 File Offset: 0x002433C3
			public override void Dispose()
			{
				this.HandleCallback();
				base.Dispose();
			}

			// Token: 0x0600B21B RID: 45595 RVA: 0x002451D1 File Offset: 0x002433D1
			private void HandleCallback()
			{
				if (this.callback != null)
				{
					Action action = this.callback;
					this.callback = null;
					action();
				}
			}

			// Token: 0x04005B2D RID: 23341
			private Action callback;
		}
	}
}
