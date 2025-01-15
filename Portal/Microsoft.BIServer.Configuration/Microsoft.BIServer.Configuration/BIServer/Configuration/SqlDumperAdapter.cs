using System;
using Microsoft.SqlServer.SqlDumper;

namespace Microsoft.BIServer.Configuration
{
	// Token: 0x02000029 RID: 41
	public sealed class SqlDumperAdapter : IDumperAdapter
	{
		// Token: 0x0600016C RID: 364 RVA: 0x000060BB File Offset: 0x000042BB
		void IDumperAdapter.Prepare()
		{
			if (!DumpClient.IsInitialized())
			{
				DumpClient.Initialize();
			}
		}

		// Token: 0x0600016D RID: 365 RVA: 0x000060CC File Offset: 0x000042CC
		string IDumperAdapter.Dump(DumpInstructions instructions)
		{
			string dumpResultText;
			using (Dumper dumper = DumpClient.GetDumper())
			{
				dumper.SetMiniDumpFlags(MiniDumpFlags.DataSegs | MiniDumpFlags.UnloadedModules | MiniDumpFlags.ProcessThreadData, MiniDumpFlags.Normal);
				dumper.SetFlags(SqlDumperAdapter.ConvertFlags(instructions.Flags), DumperFlags.Default);
				dumper.SetBucket(instructions.FramesToInclude, 0);
				if (!string.IsNullOrWhiteSpace(instructions.Location))
				{
					dumper.SetDirectory(instructions.Location);
				}
				if (!string.IsNullOrWhiteSpace(instructions.LogFileToInclude))
				{
					dumper.SetLogFile(instructions.LogFileToInclude, instructions.SizeOfLogFileToInclude);
				}
				if (!string.IsNullOrWhiteSpace(instructions.InstanceName))
				{
					dumper.SetInstanceName(instructions.InstanceName);
				}
				if (!string.IsNullOrWhiteSpace(instructions.ServiceName))
				{
					dumper.SetServiceName(instructions.ServiceName);
				}
				if (!string.IsNullOrWhiteSpace(instructions.ErrorText))
				{
					dumper.SetErrorText(instructions.ErrorText);
				}
				dumper.Dump();
				dumpResultText = dumper.GetDumpResultText();
			}
			return dumpResultText;
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000061B8 File Offset: 0x000043B8
		private static DumperFlags ConvertFlags(DumperFlags flags)
		{
			DumperFlags dumperFlags = DumperFlags.Default;
			if (flags.HasFlag(DumperFlags.AllMemory))
			{
				dumperFlags |= DumperFlags.AllMemory;
			}
			if (flags.HasFlag(DumperFlags.AllThreads))
			{
				dumperFlags |= DumperFlags.AllThreads;
			}
			if (flags.HasFlag(DumperFlags.CriticalClr))
			{
				dumperFlags |= DumperFlags.CriticalClr;
			}
			if (flags.HasFlag(DumperFlags.Default))
			{
				dumperFlags |= DumperFlags.Default;
			}
			if (flags.HasFlag(DumperFlags.DeleteFiles))
			{
				dumperFlags |= DumperFlags.DeleteFiles;
			}
			if (flags.HasFlag(DumperFlags.DoubleDump))
			{
				dumperFlags |= DumperFlags.DoubleDump;
			}
			if (flags.HasFlag(DumperFlags.Filtered))
			{
				dumperFlags |= DumperFlags.Filtered;
			}
			if (flags.HasFlag(DumperFlags.ForceUserThread))
			{
				dumperFlags |= DumperFlags.ForceUserThread;
			}
			if (flags.HasFlag(DumperFlags.ForceWatson))
			{
				dumperFlags |= DumperFlags.ForceWatson;
			}
			if (flags.HasFlag(DumperFlags.LocalOnly))
			{
				dumperFlags |= DumperFlags.LocalOnly;
			}
			if (flags.HasFlag(DumperFlags.MatchFilename))
			{
				dumperFlags |= DumperFlags.MatchFilename;
			}
			if (flags.HasFlag(DumperFlags.MatchSignatureTime))
			{
				dumperFlags |= DumperFlags.MatchSignatureTime;
			}
			if (flags.HasFlag(DumperFlags.MaximumDump))
			{
				dumperFlags |= DumperFlags.MaximumDump;
			}
			if (flags.HasFlag(DumperFlags.NoMiniDump))
			{
				dumperFlags |= DumperFlags.NoMiniDump;
			}
			if (flags.HasFlag(DumperFlags.NoRegistry))
			{
				dumperFlags |= DumperFlags.NoRegistry;
			}
			if (flags.HasFlag(DumperFlags.ReferencedMemory))
			{
				dumperFlags |= DumperFlags.ReferencedMemory;
			}
			if (flags.HasFlag(DumperFlags.SendToWatson))
			{
				dumperFlags |= DumperFlags.SendToWatson;
			}
			if (flags.HasFlag(DumperFlags.ShowUI))
			{
				dumperFlags |= DumperFlags.ShowUI;
			}
			if (flags.HasFlag(DumperFlags.SyncSubmitToWatson))
			{
				dumperFlags |= DumperFlags.SyncSubmitToWatson;
			}
			if (flags.HasFlag(DumperFlags.UseDefault))
			{
				dumperFlags |= DumperFlags.UseDefault;
			}
			if (flags.HasFlag(DumperFlags.Verbose))
			{
				dumperFlags |= DumperFlags.Verbose;
			}
			if (flags.HasFlag(DumperFlags.WaitAtExit))
			{
				dumperFlags |= DumperFlags.WaitAtExit;
			}
			return dumperFlags;
		}
	}
}
