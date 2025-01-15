using System;
using Microsoft.Mashup.Common;

namespace Microsoft.Mashup.Evaluator.Interface
{
	// Token: 0x02001DCF RID: 7631
	public sealed class EvaluatorConfiguration
	{
		// Token: 0x0600BD08 RID: 48392 RVA: 0x00265F7C File Offset: 0x0026417C
		public EvaluatorConfiguration()
		{
			this.ContainerMaxCommitInMB = -1;
			this.ContainerMaxWorkingSetInMB = -1;
			this.SharedMaxCommitInMB = -1;
			this.SharedMaxWorkingSetInMB = -1;
		}

		// Token: 0x17002E7E RID: 11902
		// (get) Token: 0x0600BD09 RID: 48393 RVA: 0x00265FA0 File Offset: 0x002641A0
		// (set) Token: 0x0600BD0A RID: 48394 RVA: 0x00265FA8 File Offset: 0x002641A8
		public string Identity { get; set; }

		// Token: 0x17002E7F RID: 11903
		// (get) Token: 0x0600BD0B RID: 48395 RVA: 0x00265FB1 File Offset: 0x002641B1
		// (set) Token: 0x0600BD0C RID: 48396 RVA: 0x00265FB9 File Offset: 0x002641B9
		public bool IsRemote { get; set; }

		// Token: 0x17002E80 RID: 11904
		// (get) Token: 0x0600BD0D RID: 48397 RVA: 0x00265FC2 File Offset: 0x002641C2
		// (set) Token: 0x0600BD0E RID: 48398 RVA: 0x00265FCA File Offset: 0x002641CA
		public string ContainerExe { get; set; }

		// Token: 0x17002E81 RID: 11905
		// (get) Token: 0x0600BD0F RID: 48399 RVA: 0x00265FD3 File Offset: 0x002641D3
		// (set) Token: 0x0600BD10 RID: 48400 RVA: 0x00265FDB File Offset: 0x002641DB
		public int ContainerCount { get; set; }

		// Token: 0x17002E82 RID: 11906
		// (get) Token: 0x0600BD11 RID: 48401 RVA: 0x00265FE4 File Offset: 0x002641E4
		// (set) Token: 0x0600BD12 RID: 48402 RVA: 0x00265FEC File Offset: 0x002641EC
		public int ContainerMinCount { get; set; }

		// Token: 0x17002E83 RID: 11907
		// (get) Token: 0x0600BD13 RID: 48403 RVA: 0x00265FF5 File Offset: 0x002641F5
		// (set) Token: 0x0600BD14 RID: 48404 RVA: 0x00265FFD File Offset: 0x002641FD
		public int SharedMaxWorkingSetInMB { get; set; }

		// Token: 0x17002E84 RID: 11908
		// (get) Token: 0x0600BD15 RID: 48405 RVA: 0x00266006 File Offset: 0x00264206
		// (set) Token: 0x0600BD16 RID: 48406 RVA: 0x0026600E File Offset: 0x0026420E
		public int SharedMaxCommitInMB { get; set; }

		// Token: 0x17002E85 RID: 11909
		// (get) Token: 0x0600BD17 RID: 48407 RVA: 0x00266017 File Offset: 0x00264217
		// (set) Token: 0x0600BD18 RID: 48408 RVA: 0x0026601F File Offset: 0x0026421F
		public int ContainerMaxWorkingSetInMB { get; set; }

		// Token: 0x17002E86 RID: 11910
		// (get) Token: 0x0600BD19 RID: 48409 RVA: 0x00266028 File Offset: 0x00264228
		// (set) Token: 0x0600BD1A RID: 48410 RVA: 0x00266030 File Offset: 0x00264230
		public int ContainerMaxCommitInMB { get; set; }

		// Token: 0x17002E87 RID: 11911
		// (get) Token: 0x0600BD1B RID: 48411 RVA: 0x00266039 File Offset: 0x00264239
		// (set) Token: 0x0600BD1C RID: 48412 RVA: 0x00266041 File Offset: 0x00264241
		public TimeSpan? ContainerTimeToLive { get; set; }

		// Token: 0x17002E88 RID: 11912
		// (get) Token: 0x0600BD1D RID: 48413 RVA: 0x0026604A File Offset: 0x0026424A
		// (set) Token: 0x0600BD1E RID: 48414 RVA: 0x00266052 File Offset: 0x00264252
		public bool[] ContainerProcessorAffinity { get; set; }

		// Token: 0x17002E89 RID: 11913
		// (get) Token: 0x0600BD1F RID: 48415 RVA: 0x0026605B File Offset: 0x0026425B
		// (set) Token: 0x0600BD20 RID: 48416 RVA: 0x00266063 File Offset: 0x00264263
		public TimeSpan SessionTimeToLive { get; set; }

		// Token: 0x17002E8A RID: 11914
		// (get) Token: 0x0600BD21 RID: 48417 RVA: 0x0026606C File Offset: 0x0026426C
		// (set) Token: 0x0600BD22 RID: 48418 RVA: 0x00266074 File Offset: 0x00264274
		public string ContainerLogFolderPath { get; set; }

		// Token: 0x17002E8B RID: 11915
		// (get) Token: 0x0600BD23 RID: 48419 RVA: 0x0026607D File Offset: 0x0026427D
		// (set) Token: 0x0600BD24 RID: 48420 RVA: 0x00266085 File Offset: 0x00264285
		public TimeSpan SoftCancelTimeout { get; set; }

		// Token: 0x17002E8C RID: 11916
		// (get) Token: 0x0600BD25 RID: 48421 RVA: 0x0026608E File Offset: 0x0026428E
		// (set) Token: 0x0600BD26 RID: 48422 RVA: 0x00266096 File Offset: 0x00264296
		public string TargetFrameworkName { get; set; }

		// Token: 0x0600BD27 RID: 48423 RVA: 0x0026609F File Offset: 0x0026429F
		public static string GetContainerFileName(ClrVersion version)
		{
			return EvaluatorConfiguration.containerFileNames[(int)version];
		}

		// Token: 0x0600BD28 RID: 48424 RVA: 0x002660A8 File Offset: 0x002642A8
		public EvaluatorConfiguration Clone()
		{
			EvaluatorConfiguration evaluatorConfiguration = new EvaluatorConfiguration();
			evaluatorConfiguration.Identity = this.Identity;
			evaluatorConfiguration.IsRemote = this.IsRemote;
			evaluatorConfiguration.ContainerExe = this.ContainerExe;
			evaluatorConfiguration.ContainerCount = this.ContainerCount;
			evaluatorConfiguration.ContainerMinCount = this.ContainerMinCount;
			evaluatorConfiguration.SharedMaxWorkingSetInMB = this.SharedMaxWorkingSetInMB;
			evaluatorConfiguration.SharedMaxCommitInMB = this.SharedMaxCommitInMB;
			evaluatorConfiguration.ContainerMaxWorkingSetInMB = this.ContainerMaxWorkingSetInMB;
			evaluatorConfiguration.ContainerMaxCommitInMB = this.ContainerMaxCommitInMB;
			evaluatorConfiguration.ContainerTimeToLive = this.ContainerTimeToLive;
			bool[] containerProcessorAffinity = this.ContainerProcessorAffinity;
			evaluatorConfiguration.ContainerProcessorAffinity = (bool[])((containerProcessorAffinity != null) ? containerProcessorAffinity.Clone() : null);
			evaluatorConfiguration.SessionTimeToLive = this.SessionTimeToLive;
			evaluatorConfiguration.ContainerLogFolderPath = this.ContainerLogFolderPath;
			evaluatorConfiguration.SoftCancelTimeout = this.SoftCancelTimeout;
			evaluatorConfiguration.TargetFrameworkName = this.TargetFrameworkName;
			return evaluatorConfiguration;
		}

		// Token: 0x0600BD29 RID: 48425 RVA: 0x0026617F File Offset: 0x0026437F
		public string GetTargetFrameworkName()
		{
			if (this.TargetFrameworkName != null)
			{
				return this.TargetFrameworkName;
			}
			if (FxVersionDetector.FxVersion == ClrVersion.Net45 && this.ContainerExe.EndsWith("Microsoft.Mashup.Container.Loader.exe", StringComparison.OrdinalIgnoreCase))
			{
				return ".NETFramework,Version=v4.5";
			}
			return null;
		}

		// Token: 0x0400606B RID: 24683
		public const string TargetFrameWorkNameArg = "--targetframework";

		// Token: 0x0400606C RID: 24684
		public const string Container35FileName = "Microsoft.Mashup.Container.exe";

		// Token: 0x0400606D RID: 24685
		public const string Container40FileName = "Microsoft.Mashup.Container.NetFX40.exe";

		// Token: 0x0400606E RID: 24686
		public const string Container45FileName = "Microsoft.Mashup.Container.NetFX45.exe";

		// Token: 0x0400606F RID: 24687
		public const string ContainerCoreFileName = "Microsoft.Mashup.Container.exe";

		// Token: 0x04006070 RID: 24688
		public const string NativeContainerLoaderFileName = "Microsoft.Mashup.Container.Loader.exe";

		// Token: 0x04006071 RID: 24689
		public const string DefaultTargetFrameworkName = ".NETFramework,Version=v4.5";

		// Token: 0x04006072 RID: 24690
		private static readonly string[] containerFileNames = new string[] { "Microsoft.Mashup.Container.exe", "Microsoft.Mashup.Container.NetFX40.exe", "Microsoft.Mashup.Container.NetFX45.exe", "Microsoft.Mashup.Container.exe" };
	}
}
