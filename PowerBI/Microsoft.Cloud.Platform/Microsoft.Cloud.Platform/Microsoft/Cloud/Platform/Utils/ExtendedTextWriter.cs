using System;
using System.IO;
using System.Text;
using JetBrains.Annotations;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001C9 RID: 457
	public class ExtendedTextWriter : IDisposable
	{
		// Token: 0x06000BB1 RID: 2993 RVA: 0x000289C0 File Offset: 0x00026BC0
		public ExtendedTextWriter()
			: this(ExtendedTextWriter.WriteOptions.WriteToConsole, string.Empty)
		{
		}

		// Token: 0x06000BB2 RID: 2994 RVA: 0x000289CE File Offset: 0x00026BCE
		public ExtendedTextWriter(string filePath)
			: this(ExtendedTextWriter.WriteOptions.WriteToConsole | ExtendedTextWriter.WriteOptions.WriteToFile, filePath)
		{
		}

		// Token: 0x06000BB3 RID: 2995 RVA: 0x000289D8 File Offset: 0x00026BD8
		public ExtendedTextWriter(ExtendedTextWriter.WriteOptions option, string filePath)
			: this(option, filePath, Encoding.UTF8)
		{
		}

		// Token: 0x06000BB4 RID: 2996 RVA: 0x000289E8 File Offset: 0x00026BE8
		public ExtendedTextWriter(ExtendedTextWriter.WriteOptions option, string filePath, Encoding encoding)
		{
			this.m_defaultOptions = option;
			this.m_outputFileWriter = null;
			if (this.m_defaultOptions.HasFlag(ExtendedTextWriter.WriteOptions.WriteToFile))
			{
				ExtendedDiagnostics.EnsureStringNotNullOrEmpty(filePath, "filePath");
				string directoryName = Path.GetDirectoryName(filePath);
				if (!Directory.Exists(directoryName))
				{
					Directory.CreateDirectory(directoryName);
				}
				this.m_outputFileWriter = new StreamWriter(filePath, false, encoding);
			}
		}

		// Token: 0x06000BB5 RID: 2997 RVA: 0x00028A50 File Offset: 0x00026C50
		[StringFormatMethod("format")]
		public void WriteLine(ConsoleColor color, [NotNull] string format, params object[] args)
		{
			this.WriteLine(this.m_defaultOptions, color, format, args);
		}

		// Token: 0x06000BB6 RID: 2998 RVA: 0x00028A64 File Offset: 0x00026C64
		[StringFormatMethod("format")]
		public void WriteLine(ExtendedTextWriter.WriteOptions options, ConsoleColor color, [NotNull] string format, params object[] args)
		{
			if (this.m_defaultOptions.HasFlag(ExtendedTextWriter.WriteOptions.WriteToFile) && options.HasFlag(ExtendedTextWriter.WriteOptions.WriteToFile))
			{
				if (args.Length != 0)
				{
					this.m_outputFileWriter.WriteLine(format, args);
				}
				else
				{
					this.m_outputFileWriter.WriteLine(format);
				}
			}
			if (options.HasFlag(ExtendedTextWriter.WriteOptions.WriteToConsole))
			{
				ExtendedConsole.WriteLine(color, format, args);
			}
		}

		// Token: 0x06000BB7 RID: 2999 RVA: 0x00028AD9 File Offset: 0x00026CD9
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06000BB8 RID: 3000 RVA: 0x00028AE8 File Offset: 0x00026CE8
		protected void Dispose(bool isDisposing)
		{
			if (isDisposing && this.m_outputFileWriter != null)
			{
				this.m_outputFileWriter.Dispose();
				this.m_outputFileWriter = null;
			}
		}

		// Token: 0x0400048D RID: 1165
		private TextWriter m_outputFileWriter;

		// Token: 0x0400048E RID: 1166
		private ExtendedTextWriter.WriteOptions m_defaultOptions;

		// Token: 0x02000683 RID: 1667
		[Flags]
		public enum WriteOptions
		{
			// Token: 0x04001264 RID: 4708
			None = 0,
			// Token: 0x04001265 RID: 4709
			WriteToConsole = 1,
			// Token: 0x04001266 RID: 4710
			WriteToFile = 2
		}
	}
}
