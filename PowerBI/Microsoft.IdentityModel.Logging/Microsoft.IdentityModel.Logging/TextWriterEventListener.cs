using System;
using System.Diagnostics.Tracing;
using System.IO;

namespace Microsoft.IdentityModel.Logging
{
	// Token: 0x0200000E RID: 14
	public class TextWriterEventListener : EventListener
	{
		// Token: 0x06000076 RID: 118 RVA: 0x00003084 File Offset: 0x00001284
		public TextWriterEventListener()
		{
			try
			{
				Stream stream = new FileStream(TextWriterEventListener.DefaultLogFileName, FileMode.OpenOrCreate, FileAccess.Write);
				this._streamWriter = new StreamWriter(stream);
				this._streamWriter.AutoFlush = true;
			}
			catch (Exception ex)
			{
				LogHelper.LogExceptionMessage(new InvalidOperationException("MIML10001: Cannot create the fileStream or StreamWriter to write logs. See inner exception.", ex));
				throw;
			}
		}

		// Token: 0x06000077 RID: 119 RVA: 0x000030EC File Offset: 0x000012EC
		public TextWriterEventListener(string filePath)
		{
			if (string.IsNullOrEmpty(filePath))
			{
				throw LogHelper.LogArgumentNullException("filePath");
			}
			try
			{
				Stream stream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write);
				this._streamWriter = new StreamWriter(stream);
				this._streamWriter.AutoFlush = true;
			}
			catch (Exception ex)
			{
				LogHelper.LogExceptionMessage(new InvalidOperationException("MIML10001: Cannot create the fileStream or StreamWriter to write logs. See inner exception.", ex));
				throw;
			}
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003164 File Offset: 0x00001364
		public TextWriterEventListener(StreamWriter streamWriter)
		{
			if (streamWriter == null)
			{
				throw LogHelper.LogArgumentNullException("streamWriter");
			}
			this._streamWriter = streamWriter;
			this._disposeStreamWriter = false;
		}

		// Token: 0x06000079 RID: 121 RVA: 0x00003190 File Offset: 0x00001390
		protected override void OnEventWritten(EventWrittenEventArgs eventData)
		{
			if (eventData == null)
			{
				throw LogHelper.LogArgumentNullException("eventData");
			}
			if (eventData.Payload == null || eventData.Payload.Count <= 0)
			{
				LogHelper.LogInformation("MIML10000: eventData.Payload is null or empty. Not logging any messages.", Array.Empty<object>());
				return;
			}
			for (int i = 0; i < eventData.Payload.Count; i++)
			{
				this._streamWriter.WriteLine(eventData.Payload[i].ToString());
			}
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003203 File Offset: 0x00001403
		public override void Dispose()
		{
			if (this._disposeStreamWriter && this._streamWriter != null)
			{
				this._streamWriter.Flush();
				this._streamWriter.Dispose();
			}
			GC.SuppressFinalize(this);
			base.Dispose();
		}

		// Token: 0x0400003C RID: 60
		private StreamWriter _streamWriter;

		// Token: 0x0400003D RID: 61
		private bool _disposeStreamWriter = true;

		// Token: 0x0400003E RID: 62
		public static readonly string DefaultLogFileName = "IdentityModelLogs.txt";
	}
}
