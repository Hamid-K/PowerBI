using System;
using System.Runtime.ExceptionServices;
using System.Runtime.InteropServices;
using ATL;

namespace MsolapWrapper
{
	// Token: 0x02000081 RID: 129
	public class Command : IDisposable
	{
		// Token: 0x060001B5 RID: 437 RVA: 0x00008EEC File Offset: 0x000082EC
		public unsafe Command(Connection connection, string commandText)
		{
			string text = "The connection should be opened before creating a command on it.";
			if (!connection.IsOpen())
			{
				WrapperContract.Fail(text);
			}
			this._connection = connection;
			this._commandText = commandText;
			CSession* ptr = <Module>.@new(8UL);
			CSession* ptr2;
			if (ptr != null)
			{
				initblk(ptr, 0, 8L);
				*(long*)ptr = 0L;
				ptr2 = ptr;
			}
			else
			{
				ptr2 = null;
			}
			this._session = ptr2;
			this._propertySets = new CommandPropertySetCollection();
			Utils.ThrowErrorIfHrFailed(<Module>.ATL.CSession.Open(this._session, connection.GetConnection(), null, 0), "Failure encountered while opening new session on the specified connection");
			CCommandWrapper ccommandWrapper = new CCommandWrapper();
			this._command = ccommandWrapper;
			*(int*)(ccommandWrapper.GetCommand() + 60L / (long)sizeof(CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>)) = 1;
			Utils.ThrowErrorIfHrFailed(1, "Failure encountered while setting some BLOB handling properties for the current command");
			GC.KeepAlive(this);
		}

		// Token: 0x060001B6 RID: 438 RVA: 0x00008344 File Offset: 0x00007744
		public void Cancel()
		{
			CCommandWrapper command = this._command;
			if (command != null)
			{
				Utils.ThrowErrorIfHrFailed(command.Cancel(), "Failure while cancelling command");
			}
		}

		// Token: 0x060001B7 RID: 439 RVA: 0x000086B8 File Offset: 0x00007AB8
		public unsafe void Close()
		{
			CCommandWrapper command = this._command;
			if (command != null)
			{
				command.Close();
			}
			CSession* session = this._session;
			if (session != null)
			{
				CSession* ptr = session;
				IOpenRowset* ptr2 = *ptr;
				if (ptr2 != null)
				{
					*ptr = 0L;
					IOpenRowset* ptr3 = ptr2;
					uint num = calli(System.UInt32 modopt(System.Runtime.CompilerServices.IsLong) modopt(System.Runtime.CompilerServices.CallConvCdecl)(System.IntPtr), ptr3, (IntPtr)(*(*(long*)ptr3 + 16L)));
				}
			}
			GC.KeepAlive(this);
		}

		// Token: 0x060001B8 RID: 440 RVA: 0x000092E8 File Offset: 0x000086E8
		public DataReader ExecuteReader()
		{
			this.InternalOpenCommand();
			byte b = ((!this._propertySets.HasForwardOnly()) ? 1 : 0);
			return new DataReader(new CommandAccessorWrapper(this), b != 0);
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x0000931C File Offset: 0x0000871C
		public void ExecuteNonQuery()
		{
			this.InternalOpenCommand();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00009338 File Offset: 0x00008738
		[return: MarshalAs(UnmanagedType.U1)]
		public bool IsOpen()
		{
			int num;
			if (this.IsReady())
			{
				CCommandWrapper command = this._command;
				if (command != null && command.IsOpen())
				{
					num = 1;
					goto IL_0020;
				}
			}
			num = 0;
			IL_0020:
			return (byte)num != 0;
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00008374 File Offset: 0x00007774
		public void AddProperty(CommandProperties property, object value)
		{
			this._propertySets.AddProperty(property, value);
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00008FF0 File Offset: 0x000083F0
		private unsafe void !Command()
		{
			CSession* session = this._session;
			if (session != null)
			{
				CSession* ptr = session;
				<Module>.ATL.CSession.{dtor}(ptr);
				<Module>.delete((void*)ptr, 8UL);
				this._session = null;
			}
			CCommandWrapper command = this._command;
			if (command != null)
			{
				((IDisposable)command).Dispose();
				this._command = null;
			}
			CommandPropertySetCollection propertySets = this._propertySets;
			if (propertySets != null)
			{
				((IDisposable)propertySets).Dispose();
				this._propertySets = null;
			}
			this._connection = null;
			GC.KeepAlive(this);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x00009060 File Offset: 0x00008460
		private void ~Command()
		{
			this.Close();
			this.!Command();
		}

		// Token: 0x060001BE RID: 446 RVA: 0x00008FA4 File Offset: 0x000083A4
		[return: MarshalAs(UnmanagedType.U1)]
		internal unsafe bool IsReady()
		{
			Connection connection = this._connection;
			int num;
			if (connection != null && connection.IsOpen())
			{
				CSession* session = this._session;
				if (session != null && ((((*(long*)session == 0L) ? 1 : 0) == 0) ? 1 : 0) != 0)
				{
					num = 1;
					goto IL_002F;
				}
			}
			num = 0;
			IL_002F:
			GC.KeepAlive(this);
			return (byte)num != 0;
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00008394 File Offset: 0x00007794
		internal unsafe CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>* GetCommand()
		{
			return this._command.GetCommand();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00009080 File Offset: 0x00008480
		private unsafe void InternalOpenCommand()
		{
			this.IsReady();
			CBulkRowset<ATL::CDynamicAccessor>* ptr = this._command.GetCommand() + 72L / (long)sizeof(CCommand<ATL::CDynamicAccessor,MsolapWrapper::CChapterBulkRowset,ATL::CMultipleResults>);
			if (1000L != *(ptr + 56L))
			{
				<Module>.delete(*(ptr + 48L), 8UL);
				*(ptr + 48L) = 0L;
				*(ptr + 56L) = 1000L;
			}
			tagDBPROPSET* ptr2 = this._propertySets.ToDbPropSet();
			Utils.ThrowErrorIfHrFailed(this._command.OpenWithMultipleResultsOnly(this._session, this._commandText, ptr2, this._propertySets.Size), "Failure encountered while executing the DAX query.");
			GC.KeepAlive(this);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x00005F64 File Offset: 0x00005364
		[HandleProcessCorruptedStateExceptions]
		protected virtual void Dispose([MarshalAs(UnmanagedType.U1)] bool A_0)
		{
			if (A_0)
			{
				this.~Command();
			}
			else
			{
				try
				{
					this.!Command();
				}
				finally
				{
					base.Finalize();
				}
			}
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x000064D8 File Offset: 0x000058D8
		public sealed void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00005FB0 File Offset: 0x000053B0
		protected override void Finalize()
		{
			this.Dispose(false);
		}

		// Token: 0x04000207 RID: 519
		private string _commandText;

		// Token: 0x04000208 RID: 520
		private Connection _connection;

		// Token: 0x04000209 RID: 521
		private CCommandWrapper _command;

		// Token: 0x0400020A RID: 522
		private unsafe CSession* _session;

		// Token: 0x0400020B RID: 523
		private CommandPropertySetCollection _propertySets;
	}
}
