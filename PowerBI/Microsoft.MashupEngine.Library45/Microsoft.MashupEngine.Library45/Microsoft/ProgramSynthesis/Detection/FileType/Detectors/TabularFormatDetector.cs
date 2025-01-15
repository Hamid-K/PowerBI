using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ProgramSynthesis.Read.FlatFile;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.FileType.Detectors
{
	// Token: 0x02000ACD RID: 2765
	internal class TabularFormatDetector : TextualFormatDetector
	{
		// Token: 0x17000C72 RID: 3186
		// (get) Token: 0x0600455E RID: 17758 RVA: 0x000D90CC File Offset: 0x000D72CC
		// (set) Token: 0x0600455F RID: 17759 RVA: 0x000D90D4 File Offset: 0x000D72D4
		public TimeSpan TimeLimit { get; set; }

		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06004560 RID: 17760 RVA: 0x000D90DD File Offset: 0x000D72DD
		internal override IEnumerable<FileType> SupportedFileTypes
		{
			get
			{
				return Seq.Of<FileType>(new FileType[]
				{
					FileType.Csv,
					FileType.FixedWidth
				});
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06004561 RID: 17761 RVA: 0x000D90F3 File Offset: 0x000D72F3
		internal override IEnumerable<string> SupportedExtensions
		{
			get
			{
				return Seq.Of<string>(new string[] { "csv", "tsv" });
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06004562 RID: 17762 RVA: 0x000D9110 File Offset: 0x000D7310
		internal override int Precedence
		{
			get
			{
				return 10;
			}
		}

		// Token: 0x06004563 RID: 17763 RVA: 0x000D9114 File Offset: 0x000D7314
		public TabularFormatDetector()
		{
			this.TimeLimit = TimeSpan.FromSeconds(5.0);
		}

		// Token: 0x06004564 RID: 17764 RVA: 0x000D9130 File Offset: 0x000D7330
		internal override FileType MatchFormat(FileTypeIdentifier caller, string header, string footer)
		{
			string text = header.Substring(0, Math.Min(1048576, header.Length));
			Session session = new Session(null, null, null);
			session.AddInput(text, 200);
			CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
			if (!Debugger.IsAttached)
			{
				cancellationTokenSource.CancelAfter(this.TimeLimit);
			}
			FileType fileType;
			try
			{
				Program program = session.Learn(null, cancellationTokenSource.Token, null);
				if (program == null)
				{
					fileType = FileType.Unknown;
				}
				else
				{
					fileType = program.Switch<FileType>(delegate(CsvProgram csvProgram)
					{
						if (!string.IsNullOrEmpty(csvProgram.Delimiter))
						{
							return FileType.Csv;
						}
						return FileType.Unknown;
					}, (FwProgram fwProgram) => FileType.FixedWidth, (ExtractionTextProgram etextProgram) => FileType.Unknown);
				}
			}
			catch (TaskCanceledException)
			{
				fileType = FileType.Unknown;
			}
			return fileType;
		}

		// Token: 0x04001FAC RID: 8108
		private const int MaxStringLength = 1048576;
	}
}
