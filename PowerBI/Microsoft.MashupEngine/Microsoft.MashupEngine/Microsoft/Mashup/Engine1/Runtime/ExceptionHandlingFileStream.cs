using System;
using System.IO;
using Microsoft.Internal;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012EE RID: 4846
	public sealed class ExceptionHandlingFileStream : FileStream, ILeaveEngineContext<Stream>
	{
		// Token: 0x0600804A RID: 32842 RVA: 0x001B5B21 File Offset: 0x001B3D21
		public ExceptionHandlingFileStream(string path, FileMode mode, FileAccess access, FileShare share)
			: base(path, mode, access, share)
		{
		}

		// Token: 0x0600804B RID: 32843 RVA: 0x0000F6A1 File Offset: 0x0000D8A1
		public Stream Leave()
		{
			return this;
		}

		// Token: 0x0600804C RID: 32844 RVA: 0x001B5B2E File Offset: 0x001B3D2E
		public ExceptionHandlingFileStream(SafeFileHandle fileHandle, FileAccess access)
			: base(fileHandle, access)
		{
		}

		// Token: 0x0600804D RID: 32845 RVA: 0x001B5B38 File Offset: 0x001B3D38
		public override int Read(byte[] array, int offset, int count)
		{
			int num;
			try
			{
				num = base.Read(array, offset, count);
			}
			catch (IOException ex)
			{
				using (EngineContext.Enter())
				{
					throw FileErrors.HandleException(ex, TextValue.New(base.Name));
				}
			}
			return num;
		}

		// Token: 0x0600804E RID: 32846 RVA: 0x001B5B98 File Offset: 0x001B3D98
		public override int ReadByte()
		{
			int num;
			try
			{
				num = base.ReadByte();
			}
			catch (IOException ex)
			{
				using (EngineContext.Enter())
				{
					throw FileErrors.HandleException(ex, TextValue.New(base.Name));
				}
			}
			return num;
		}
	}
}
