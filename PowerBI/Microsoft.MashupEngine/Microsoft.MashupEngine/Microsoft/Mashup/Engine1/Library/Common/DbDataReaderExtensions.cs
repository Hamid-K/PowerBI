using System;
using Microsoft.Data.Serialization;

namespace Microsoft.Mashup.Engine1.Library.Common
{
	// Token: 0x02001055 RID: 4181
	internal static class DbDataReaderExtensions
	{
		// Token: 0x06006D3D RID: 27965 RVA: 0x00178636 File Offset: 0x00176836
		public static DbDataReaderWithTableSchema OnDispose(this DbDataReaderWithTableSchema reader, Action action)
		{
			return new DbDataReaderExtensions.NotifyingDbDataReader(reader, action);
		}

		// Token: 0x06006D3E RID: 27966 RVA: 0x00178640 File Offset: 0x00176840
		public static DbDataReaderWithTableSchema AfterDispose(this DbDataReaderWithTableSchema reader, Action action)
		{
			return new DbDataReaderExtensions.NotifyingDbDataReader(reader, delegate
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

		// Token: 0x02001056 RID: 4182
		private sealed class NotifyingDbDataReader : DelegatingDbDataReaderWithTableSchema
		{
			// Token: 0x06006D3F RID: 27967 RVA: 0x00178678 File Offset: 0x00176878
			public NotifyingDbDataReader(DbDataReaderWithTableSchema reader, Action callback)
				: base(reader)
			{
				this.callback = callback;
			}

			// Token: 0x06006D40 RID: 27968 RVA: 0x00178688 File Offset: 0x00176888
			public override void Close()
			{
				this.HandleCallback();
				base.Close();
			}

			// Token: 0x06006D41 RID: 27969 RVA: 0x00178696 File Offset: 0x00176896
			protected override void Dispose(bool disposing)
			{
				if (disposing)
				{
					this.HandleCallback();
				}
				base.Dispose(disposing);
			}

			// Token: 0x06006D42 RID: 27970 RVA: 0x001786A8 File Offset: 0x001768A8
			private void HandleCallback()
			{
				if (this.callback != null)
				{
					Action action = this.callback;
					this.callback = null;
					action();
				}
			}

			// Token: 0x04003C9A RID: 15514
			private Action callback;
		}
	}
}
