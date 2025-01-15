using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Threading;
using Microsoft.ProgramSynthesis.Read.FlatFile.Constraints;
using Microsoft.ProgramSynthesis.Read.FlatFile.Learning;
using Microsoft.ProgramSynthesis.Utils;

namespace Microsoft.ProgramSynthesis.Read.FlatFile
{
	// Token: 0x02001254 RID: 4692
	public static class ReadFlatFileLearner
	{
		// Token: 0x06008D23 RID: 36131 RVA: 0x001DA64C File Offset: 0x001D884C
		public static Program Learn(string input, LearningOptions options = null, int? skip = null, int? skipFooter = null, CancellationToken cancel = default(CancellationToken))
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return ReadFlatFileLearner.Learn(input, options, ReadFlatFileLearner.ProgramType.Any, null, null, null, skip, skipFooter, cancel);
		}

		// Token: 0x06008D24 RID: 36132 RVA: 0x001DA678 File Offset: 0x001D8878
		public static CsvProgram LearnCsv(string input, LearningOptions options = null, string delimiter = null, int? skip = null, int? skipFooter = null, CancellationToken cancel = default(CancellationToken))
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return ReadFlatFileLearner.Learn(input, options, ReadFlatFileLearner.ProgramType.Csv, delimiter, null, null, skip, skipFooter, cancel) as CsvProgram;
		}

		// Token: 0x06008D25 RID: 36133 RVA: 0x001DA6A8 File Offset: 0x001D88A8
		public static FwProgram LearnFw(string input, LearningOptions options = null, string fwSchema = null, IReadOnlyList<Record<int, int?>> fieldPositions = null, int? skip = null, int? skipFooter = null, CancellationToken cancel = default(CancellationToken))
		{
			if (input == null && fwSchema == null)
			{
				throw new ArgumentNullException("input and fwSchema are both null");
			}
			return ReadFlatFileLearner.Learn(input, options, ReadFlatFileLearner.ProgramType.Fw, null, fwSchema, fieldPositions, skip, skipFooter, cancel) as FwProgram;
		}

		// Token: 0x06008D26 RID: 36134 RVA: 0x001DA6DC File Offset: 0x001D88DC
		public static Program Learn(TextReader input, LearningOptions options = null, string delimiter = null, int? skip = null, int? skipFooter = null, CancellationToken cancel = default(CancellationToken))
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return ReadFlatFileLearner.Learn(input, options, ReadFlatFileLearner.ProgramType.Any, delimiter, null, null, skip, skipFooter, cancel);
		}

		// Token: 0x06008D27 RID: 36135 RVA: 0x001DA708 File Offset: 0x001D8908
		public static CsvProgram LearnCsv(TextReader input, LearningOptions options = null, string delimiter = null, int? skip = null, int? skipFooter = null, CancellationToken cancel = default(CancellationToken))
		{
			if (input == null)
			{
				throw new ArgumentNullException("input");
			}
			return ReadFlatFileLearner.Learn(input, options, ReadFlatFileLearner.ProgramType.Csv, delimiter, null, null, skip, skipFooter, cancel) as CsvProgram;
		}

		// Token: 0x06008D28 RID: 36136 RVA: 0x001DA738 File Offset: 0x001D8938
		public static FwProgram LearnFw(TextReader input, LearningOptions options = null, TextReader fwSchema = null, IReadOnlyList<Record<int, int?>> fieldPositions = null, int? skip = null, int? skipFooter = null, CancellationToken cancel = default(CancellationToken))
		{
			if (input == null && fwSchema == null)
			{
				throw new ArgumentNullException("input and fwSchema are both null");
			}
			string text = ((fwSchema != null) ? fwSchema.ReadToEnd() : null);
			return ReadFlatFileLearner.Learn(input, options, ReadFlatFileLearner.ProgramType.Fw, null, text, fieldPositions, skip, skipFooter, cancel) as FwProgram;
		}

		// Token: 0x06008D29 RID: 36137 RVA: 0x001DA77C File Offset: 0x001D897C
		private static Program Learn(TextReader input, LearningOptions options, ReadFlatFileLearner.ProgramType type = ReadFlatFileLearner.ProgramType.Any, string delimiter = null, string fwSchema = null, IReadOnlyList<Record<int, int?>> fieldPositions = null, int? skip = null, int? skipFooter = null, CancellationToken cancel = default(CancellationToken))
		{
			if (options == null)
			{
				options = new LearningOptions();
			}
			Session session = new Session(null, null, null);
			if (input != null)
			{
				session.AddInput(input, options.LinesToLearn);
			}
			ReadFlatFileLearner.AddConstraints(session, type, options.ColumnNameCleaning, delimiter, fwSchema, fieldPositions, skip, skipFooter);
			return session.Learn(null, ReadFlatFileLearner.CreateTimeoutCancellationToken(options, cancel), null);
		}

		// Token: 0x06008D2A RID: 36138 RVA: 0x001DA7DC File Offset: 0x001D89DC
		private static Program Learn(string input, LearningOptions options, ReadFlatFileLearner.ProgramType type = ReadFlatFileLearner.ProgramType.Any, string delimiter = null, string fwSchema = null, IReadOnlyList<Record<int, int?>> fieldPositions = null, int? skip = null, int? skipFooter = null, CancellationToken cancel = default(CancellationToken))
		{
			if (options == null)
			{
				options = new LearningOptions();
			}
			Session session = new Session(null, null, null);
			if (input != null)
			{
				session.AddInput(input, options.LinesToLearn);
			}
			ReadFlatFileLearner.AddConstraints(session, type, options.ColumnNameCleaning, delimiter, fwSchema, fieldPositions, skip, skipFooter);
			return session.Learn(null, ReadFlatFileLearner.CreateTimeoutCancellationToken(options, cancel), null);
		}

		// Token: 0x06008D2B RID: 36139 RVA: 0x001DA83C File Offset: 0x001D8A3C
		private static CancellationToken CreateTimeoutCancellationToken(LearningOptions options, CancellationToken cancel = default(CancellationToken))
		{
			if (Debugger.IsAttached || options.TimeLimit == null)
			{
				return cancel;
			}
			CancellationTokenSource cancellationTokenSource = CancellationTokenSource.CreateLinkedTokenSource(new CancellationToken[] { cancel });
			cancellationTokenSource.CancelAfter(options.TimeLimit.Value);
			return cancellationTokenSource.Token;
		}

		// Token: 0x06008D2C RID: 36140 RVA: 0x001DA890 File Offset: 0x001D8A90
		private static void AddConstraints(Session session, ReadFlatFileLearner.ProgramType type, ColumnNameCleaningType columnNameCleaning, string delimiter = null, string fwSchema = null, IReadOnlyList<Record<int, int?>> fieldPositions = null, int? skip = null, int? skipFooter = null)
		{
			if (type == ReadFlatFileLearner.ProgramType.Csv)
			{
				session.Constraints.Add(Csv.Instance);
			}
			else if (type == ReadFlatFileLearner.ProgramType.Fw)
			{
				session.Constraints.Add(new FixedWidth(fwSchema));
			}
			session.Constraints.Add(new ColumnNameCleaning(columnNameCleaning));
			if (delimiter != null)
			{
				session.Constraints.Add(new Delimiter(delimiter));
			}
			if (fieldPositions != null)
			{
				session.Constraints.Add(new FieldPositions(fieldPositions));
			}
			if (skip != null)
			{
				session.Constraints.Add(new Skip(skip.Value));
			}
			if (skipFooter != null)
			{
				session.Constraints.Add(new SkipFooter(skipFooter.Value));
			}
		}

		// Token: 0x02001255 RID: 4693
		private enum ProgramType
		{
			// Token: 0x040039CE RID: 14798
			Any,
			// Token: 0x040039CF RID: 14799
			Csv,
			// Token: 0x040039D0 RID: 14800
			Fw
		}
	}
}
