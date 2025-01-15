using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using ATL;

namespace MsolapWrapper
{
	// Token: 0x0200007D RID: 125
	internal class CommandAccessorWrapper : ICAccessorWrapper, IDisposable
	{
		// Token: 0x06000196 RID: 406 RVA: 0x000066D0 File Offset: 0x00005AD0
		public CommandAccessorWrapper(Command command)
		{
			this._daxCommand = command;
		}

		// Token: 0x06000197 RID: 407 RVA: 0x000062DC File Offset: 0x000056DC
		[return: MarshalAs(UnmanagedType.U1)]
		public virtual bool IsOpen()
		{
			return this._daxCommand.IsOpen();
		}

		// Token: 0x06000198 RID: 408 RVA: 0x000062FC File Offset: 0x000056FC
		public virtual void Close()
		{
			Command daxCommand = this._daxCommand;
			if (daxCommand != null)
			{
				daxCommand.Close();
			}
		}

		// Token: 0x06000199 RID: 409 RVA: 0x00006320 File Offset: 0x00005720
		public unsafe virtual CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>* GetAccessor()
		{
			return (CAccessorRowset<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset>*)this._daxCommand.GetCommand();
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00006340 File Offset: 0x00005740
		[return: MarshalAs(UnmanagedType.U1)]
		public virtual bool SupportsNextResult()
		{
			return true;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00007DB8 File Offset: 0x000071B8
		[return: MarshalAs(UnmanagedType.U1)]
		public unsafe virtual bool NextResult()
		{
			this._daxCommand.IsOpen();
			long num2;
			int num = <Module>.ATL.CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>.GetNextResult(this._daxCommand.GetCommand(), &num2, true);
			if (num == 265929)
			{
				GC.KeepAlive(this);
				return false;
			}
			Utils.ThrowErrorIfHrFailed(num, "Failure encountered while getting the next result-set");
			GC.KeepAlive(this);
			return true;
		}

		// Token: 0x0600019C RID: 412 RVA: 0x00006354 File Offset: 0x00005754
		private void !CommandAccessorWrapper()
		{
			Command daxCommand = this._daxCommand;
			if (daxCommand != null)
			{
				((IDisposable)daxCommand).Dispose();
				this._daxCommand = null;
			}
		}

		// Token: 0x0600019D RID: 413 RVA: 0x00006380 File Offset: 0x00005780
		private void ~CommandAccessorWrapper()
		{
			this.Close();
			this.!CommandAccessorWrapper();
		}

		// Token: 0x0600019E RID: 414 RVA: 0x00006468 File Offset: 0x00005868
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.Close();
				this.!CommandAccessorWrapper();
			}
			else
			{
				try
				{
					this.!CommandAccessorWrapper();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x0600019F RID: 415 RVA: 0x000069B4 File Offset: 0x00005DB4
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001A0 RID: 416 RVA: 0x000064BC File Offset: 0x000058BC
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x04000193 RID: 403
		private Command _daxCommand;
	}
}
