using System;
using System.Collections.Generic;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Detection.FileType.Detectors
{
	// Token: 0x02000AC1 RID: 2753
	internal class AvroDetector : BinaryFormatDetector
	{
		// Token: 0x17000C52 RID: 3154
		// (get) Token: 0x0600451D RID: 17693 RVA: 0x000D86A8 File Offset: 0x000D68A8
		internal override IEnumerable<FileType> SupportedFileTypes
		{
			get
			{
				return Seq.Of<FileType>(new FileType[] { FileType.Avro });
			}
		}

		// Token: 0x17000C53 RID: 3155
		// (get) Token: 0x0600451E RID: 17694 RVA: 0x000D86B9 File Offset: 0x000D68B9
		internal override IEnumerable<string> SupportedExtensions
		{
			get
			{
				return Seq.Of<string>(new string[] { "avro" });
			}
		}

		// Token: 0x17000C54 RID: 3156
		// (get) Token: 0x0600451F RID: 17695 RVA: 0x0000FA11 File Offset: 0x0000DC11
		internal override int Precedence
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x06004520 RID: 17696 RVA: 0x000D86CE File Offset: 0x000D68CE
		internal override FileType MatchFormat(FileTypeIdentifier caller, byte[] header, byte[] footer)
		{
			if (header.Length < 4)
			{
				return FileType.Unknown;
			}
			if (header[0] == 79 && header[1] == 98 && header[2] == 106 && header[3] == 1)
			{
				return FileType.Avro;
			}
			return FileType.Unknown;
		}
	}
}
