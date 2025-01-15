using System;
using System.IO;
using System.Runtime.InteropServices;
using Microsoft.Mashup.Common;
using Microsoft.Mashup.Engine.Ast;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language.Ast;
using Microsoft.Mashup.Engine1.Library;
using Microsoft.Mashup.Engine1.Library.File;
using Microsoft.Win32.SafeHandles;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020012FF RID: 4863
	public class FileStreamBinaryValue : StreamedBinaryValue
	{
		// Token: 0x0600808D RID: 32909 RVA: 0x001B6D65 File Offset: 0x001B4F65
		public FileStreamBinaryValue(string fileName, IHostProgress progress, Func<IDisposable> impersonate, bool preserveLastAccessTimes = false)
			: this(null, fileName, progress, impersonate, preserveLastAccessTimes)
		{
		}

		// Token: 0x0600808E RID: 32910 RVA: 0x001B6D73 File Offset: 0x001B4F73
		public FileStreamBinaryValue(IEngineHost engineHost, string fileName, IHostProgress progress, Func<IDisposable> impersonate, bool preserveLastAccessTimes = false)
		{
			this.engineHost = engineHost;
			this.fileName = fileName;
			this.progress = progress;
			this.impersonate = impersonate;
			this.preserveLastAccessTimes = preserveLastAccessTimes;
		}

		// Token: 0x170022D2 RID: 8914
		// (get) Token: 0x0600808F RID: 32911 RVA: 0x001B6DA0 File Offset: 0x001B4FA0
		public bool RequiresImpersonation
		{
			get
			{
				bool flag;
				using (IDisposable disposable = this.impersonate())
				{
					flag = disposable != null;
				}
				return flag;
			}
		}

		// Token: 0x170022D3 RID: 8915
		// (get) Token: 0x06008090 RID: 32912 RVA: 0x001B6DDC File Offset: 0x001B4FDC
		public override BinaryValue End
		{
			get
			{
				return new FileStreamBinaryValue.EndFileStreamBinaryValue(this);
			}
		}

		// Token: 0x170022D4 RID: 8916
		// (get) Token: 0x06008091 RID: 32913 RVA: 0x001B6DE4 File Offset: 0x001B4FE4
		public override IExpression Expression
		{
			get
			{
				ArrayBuilder<IExpression> arrayBuilder = new ArrayBuilder<IExpression>(2);
				arrayBuilder.Add(new ConstantExpressionSyntaxNode(TextValue.New(this.fileName)));
				if (this.preserveLastAccessTimes)
				{
					arrayBuilder.Add(new ConstantExpressionSyntaxNode(RecordValue.New(Keys.New("PreserveLastAccessTimes"), new Value[] { LogicalValue.True })));
				}
				return new InvocationExpressionSyntaxNodeN(new ConstantExpressionSyntaxNode(new FileModule.FileContentsFunctionValue(this.engineHost)), arrayBuilder.ToArray());
			}
		}

		// Token: 0x06008092 RID: 32914 RVA: 0x001B6E5D File Offset: 0x001B505D
		public override Stream Open()
		{
			return this.Open(FileMode.Open, FileAccess.Read);
		}

		// Token: 0x06008093 RID: 32915 RVA: 0x001B6E67 File Offset: 0x001B5067
		public override Stream OpenForWrite()
		{
			this.VerifyActionPermitted();
			return this.Open(FileMode.Create, FileAccess.Write);
		}

		// Token: 0x06008094 RID: 32916 RVA: 0x001B6E78 File Offset: 0x001B5078
		protected Stream Open(FileMode fileMode, FileAccess fileAccess)
		{
			Stream stream;
			try
			{
				using (new ProgressRequest(this.progress))
				{
					using (this.impersonate())
					{
						if (this.preserveLastAccessTimes)
						{
							SafeFileHandle fileHandle = this.CreateFileHandleToPreserveLastAccessTime(fileMode);
							stream = this.ExecuteWithRetries(() => new ProgressStream(new ExceptionHandlingFileStream(fileHandle, fileAccess), this.progress));
						}
						else
						{
							stream = this.ExecuteWithRetries(() => new ProgressStream(new ExceptionHandlingFileStream(this.fileName, fileMode, fileAccess, FileShare.ReadWrite), this.progress));
						}
					}
				}
			}
			catch (ArgumentException ex)
			{
				throw ValueException.NewDataFormatError(ex.Message, TextValue.New(this.fileName), ex);
			}
			catch (IOException ex2)
			{
				throw ValueException.NewDataSourceError(ex2.Message, TextValue.New(this.fileName), ex2);
			}
			catch (NotSupportedException ex3)
			{
				throw ValueException.NewExpressionError(ex3.Message, TextValue.New(this.fileName), ex3);
			}
			catch (UnauthorizedAccessException ex4)
			{
				throw ValueException.NewDataSourceError(ex4.Message, TextValue.New(this.fileName), ex4);
			}
			return stream;
		}

		// Token: 0x06008095 RID: 32917 RVA: 0x001B6FE8 File Offset: 0x001B51E8
		protected void VerifyActionPermitted()
		{
			this.engineHost.VerifyActionPermitted(Resource.New("File", this.fileName));
		}

		// Token: 0x06008096 RID: 32918 RVA: 0x001B7005 File Offset: 0x001B5205
		private Stream ExecuteWithRetries(Func<ProgressStream> createStream)
		{
			return createStream();
		}

		// Token: 0x06008097 RID: 32919 RVA: 0x001B7010 File Offset: 0x001B5210
		private SafeFileHandle CreateFileHandleToPreserveLastAccessTime(FileMode fileMode)
		{
			SafeFileHandle safeFileHandle = FileStreamBinaryValue.NativeMethods.CreateFile(this.fileName, FileStreamBinaryValue.NativeMethods.FILE_READ_ATTRIBUTES | FileStreamBinaryValue.NativeMethods.FILE_WRITE_ATTRIBUTES, FileShare.ReadWrite, IntPtr.Zero, fileMode, 0, IntPtr.Zero);
			try
			{
				if (safeFileHandle.IsInvalid)
				{
					throw FileErrors.WinIOError(Marshal.GetLastWin32Error(), this.fileName);
				}
				if (!FileStreamBinaryValue.NativeMethods.SetFileTime(safeFileHandle, ref FileStreamBinaryValue.NativeMethods.NullFileTime, ref FileStreamBinaryValue.NativeMethods.PreserveFileTime, ref FileStreamBinaryValue.NativeMethods.NullFileTime))
				{
					throw FileErrors.WinIOError(Marshal.GetLastWin32Error(), this.fileName);
				}
			}
			catch (Exception ex)
			{
				if (SafeExceptions.IsSafeException(ex))
				{
					throw FileErrors.HandleException(ex, TextValue.New(this.fileName));
				}
				throw;
			}
			return safeFileHandle;
		}

		// Token: 0x0400460C RID: 17932
		private readonly IEngineHost engineHost;

		// Token: 0x0400460D RID: 17933
		private readonly string fileName;

		// Token: 0x0400460E RID: 17934
		private readonly IHostProgress progress;

		// Token: 0x0400460F RID: 17935
		private readonly Func<IDisposable> impersonate;

		// Token: 0x04004610 RID: 17936
		private readonly bool preserveLastAccessTimes;

		// Token: 0x02001300 RID: 4864
		private sealed class EndFileStreamBinaryValue : FileStreamBinaryValue
		{
			// Token: 0x06008098 RID: 32920 RVA: 0x001B70B4 File Offset: 0x001B52B4
			public EndFileStreamBinaryValue(FileStreamBinaryValue fileStream)
				: base(fileStream.engineHost, fileStream.fileName, fileStream.progress, fileStream.impersonate, false)
			{
				this.fileStream = fileStream;
			}

			// Token: 0x06008099 RID: 32921 RVA: 0x001B70DC File Offset: 0x001B52DC
			public override Stream Open()
			{
				return new MemoryStream(new byte[0]);
			}

			// Token: 0x0600809A RID: 32922 RVA: 0x001B70E9 File Offset: 0x001B52E9
			public override Stream OpenForWrite()
			{
				base.VerifyActionPermitted();
				return base.Open(FileMode.Append, FileAccess.Write);
			}

			// Token: 0x04004611 RID: 17937
			private readonly FileStreamBinaryValue fileStream;
		}

		// Token: 0x02001301 RID: 4865
		private static class NativeMethods
		{
			// Token: 0x0600809B RID: 32923
			[DllImport("kernel32.dll", BestFitMapping = false, CharSet = CharSet.Unicode, SetLastError = true, ThrowOnUnmappableChar = true)]
			public static extern SafeFileHandle CreateFile(string lpFileName, uint dwDesiredAccess, FileShare dwShareMode, IntPtr pSecurityAttrs, FileMode dwCreationDisposition, int dwFlagsAndAttributes, IntPtr hTemplateFile);

			// Token: 0x0600809C RID: 32924
			[DllImport("kernel32.dll", SetLastError = true)]
			[return: MarshalAs(UnmanagedType.Bool)]
			public static extern bool SetFileTime(SafeFileHandle hFile, ref ulong lpCreationTime, ref ulong lpLastAccessTime, ref ulong lpLastWriteTime);

			// Token: 0x04004612 RID: 17938
			public static uint FILE_READ_ATTRIBUTES = 2147483648U;

			// Token: 0x04004613 RID: 17939
			public static uint FILE_WRITE_ATTRIBUTES = 1073741824U;

			// Token: 0x04004614 RID: 17940
			public static ulong PreserveFileTime = ulong.MaxValue;

			// Token: 0x04004615 RID: 17941
			public static ulong NullFileTime = 0UL;
		}
	}
}
