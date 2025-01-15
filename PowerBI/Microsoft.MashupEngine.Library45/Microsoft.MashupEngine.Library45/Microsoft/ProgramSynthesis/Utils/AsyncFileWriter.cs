using System;
using System.Collections.Concurrent;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

namespace Microsoft.ProgramSynthesis.Utils
{
	// Token: 0x020003F0 RID: 1008
	public class AsyncFileWriter : IDisposable
	{
		// Token: 0x060016E8 RID: 5864 RVA: 0x000460A0 File Offset: 0x000442A0
		public AsyncFileWriter(string fileName, FileMode fileMode = FileMode.CreateNew, FileShare fileShare = FileShare.Read)
		{
			this._fileStream = new FileStream(fileName, fileMode, FileAccess.Write, fileShare);
			this._streamWriter = new StreamWriter(this._fileStream);
			this._collection = new BlockingCollection<string>();
			this._task = Task.Factory.StartNew(new Action(this.WriteWorker), CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
		}

		// Token: 0x060016E9 RID: 5865 RVA: 0x00046105 File Offset: 0x00044305
		public void WriteLine(string text)
		{
			this.Write(text + Environment.NewLine);
		}

		// Token: 0x060016EA RID: 5866 RVA: 0x00046118 File Offset: 0x00044318
		public void Write(string text)
		{
			this._collection.Add(text);
		}

		// Token: 0x060016EB RID: 5867 RVA: 0x00046126 File Offset: 0x00044326
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060016EC RID: 5868 RVA: 0x00046135 File Offset: 0x00044335
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				this._collection.CompleteAdding();
				this._task.Wait();
				this._streamWriter.Dispose();
				this._fileStream.Dispose();
			}
		}

		// Token: 0x060016ED RID: 5869 RVA: 0x00046168 File Offset: 0x00044368
		private void WriteWorker()
		{
			foreach (string text in this._collection.GetConsumingEnumerable())
			{
				this._streamWriter.Write(text);
			}
			this._streamWriter.Flush();
		}

		// Token: 0x04000AFE RID: 2814
		private readonly BlockingCollection<string> _collection;

		// Token: 0x04000AFF RID: 2815
		private readonly StreamWriter _streamWriter;

		// Token: 0x04000B00 RID: 2816
		private readonly FileStream _fileStream;

		// Token: 0x04000B01 RID: 2817
		private readonly Task _task;
	}
}
