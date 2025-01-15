using System;
using System.Collections.Generic;
using dotless.Core.Input;
using dotless.Core.Loggers;
using dotless.Core.Plugins;

namespace dotless.Core.configuration
{
	// Token: 0x020000C0 RID: 192
	public class DotlessConfiguration
	{
		// Token: 0x0600057C RID: 1404 RVA: 0x00017F98 File Offset: 0x00016198
		public static DotlessConfiguration GetDefault()
		{
			return new DotlessConfiguration();
		}

		// Token: 0x0600057D RID: 1405 RVA: 0x00017F9F File Offset: 0x0001619F
		public static DotlessConfiguration GetDefaultWeb()
		{
			return new DotlessConfiguration
			{
				Web = true
			};
		}

		// Token: 0x0600057E RID: 1406 RVA: 0x00017FB0 File Offset: 0x000161B0
		public DotlessConfiguration()
		{
			this.LessSource = typeof(FileReader);
			this.MinifyOutput = false;
			this.Debug = false;
			this.CacheEnabled = true;
			this.HttpExpiryInMinutes = 10080;
			this.Web = false;
			this.SessionMode = DotlessSessionStateMode.Disabled;
			this.SessionQueryParamName = "sstate";
			this.Logger = null;
			this.LogLevel = LogLevel.Error;
			this.Optimization = 1;
			this.Plugins = new List<IPluginConfigurator>();
			this.MapPathsToWeb = true;
			this.HandleWebCompression = true;
			this.KeepFirstSpecialComment = false;
			this.RootPath = "";
			this.StrictMath = false;
		}

		// Token: 0x0600057F RID: 1407 RVA: 0x00018054 File Offset: 0x00016254
		public DotlessConfiguration(DotlessConfiguration config)
		{
			this.LessSource = config.LessSource;
			this.MinifyOutput = config.MinifyOutput;
			this.Debug = config.Debug;
			this.CacheEnabled = config.CacheEnabled;
			this.Web = config.Web;
			this.SessionMode = config.SessionMode;
			this.SessionQueryParamName = config.SessionQueryParamName;
			this.Logger = null;
			this.LogLevel = config.LogLevel;
			this.Optimization = config.Optimization;
			this.Plugins = new List<IPluginConfigurator>();
			this.Plugins.AddRange(config.Plugins);
			this.MapPathsToWeb = config.MapPathsToWeb;
			this.DisableUrlRewriting = config.DisableUrlRewriting;
			this.InlineCssFiles = config.InlineCssFiles;
			this.ImportAllFilesAsLess = config.ImportAllFilesAsLess;
			this.HandleWebCompression = config.HandleWebCompression;
			this.DisableParameters = config.DisableParameters;
			this.KeepFirstSpecialComment = config.KeepFirstSpecialComment;
			this.RootPath = config.RootPath;
			this.StrictMath = config.StrictMath;
		}

		// Token: 0x170000F3 RID: 243
		// (get) Token: 0x06000580 RID: 1408 RVA: 0x00018162 File Offset: 0x00016362
		// (set) Token: 0x06000581 RID: 1409 RVA: 0x0001816A File Offset: 0x0001636A
		public bool KeepFirstSpecialComment { get; set; }

		// Token: 0x170000F4 RID: 244
		// (get) Token: 0x06000582 RID: 1410 RVA: 0x00018173 File Offset: 0x00016373
		// (set) Token: 0x06000583 RID: 1411 RVA: 0x0001817B File Offset: 0x0001637B
		public bool DisableParameters { get; set; }

		// Token: 0x170000F5 RID: 245
		// (get) Token: 0x06000584 RID: 1412 RVA: 0x00018184 File Offset: 0x00016384
		// (set) Token: 0x06000585 RID: 1413 RVA: 0x0001818C File Offset: 0x0001638C
		public bool DisableUrlRewriting { get; set; }

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x06000586 RID: 1414 RVA: 0x00018195 File Offset: 0x00016395
		// (set) Token: 0x06000587 RID: 1415 RVA: 0x0001819D File Offset: 0x0001639D
		public string RootPath { get; set; }

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x06000588 RID: 1416 RVA: 0x000181A6 File Offset: 0x000163A6
		// (set) Token: 0x06000589 RID: 1417 RVA: 0x000181AE File Offset: 0x000163AE
		[Obsolete("The Variable Redefines feature has been removed to align with less.js")]
		public bool DisableVariableRedefines { get; set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600058A RID: 1418 RVA: 0x000181B7 File Offset: 0x000163B7
		// (set) Token: 0x0600058B RID: 1419 RVA: 0x000181BF File Offset: 0x000163BF
		[Obsolete("The Color Compression feature has been removed to align with less.js")]
		public bool DisableColorCompression { get; set; }

		// Token: 0x170000F9 RID: 249
		// (get) Token: 0x0600058C RID: 1420 RVA: 0x000181C8 File Offset: 0x000163C8
		// (set) Token: 0x0600058D RID: 1421 RVA: 0x000181D0 File Offset: 0x000163D0
		public bool InlineCssFiles { get; set; }

		// Token: 0x170000FA RID: 250
		// (get) Token: 0x0600058E RID: 1422 RVA: 0x000181D9 File Offset: 0x000163D9
		// (set) Token: 0x0600058F RID: 1423 RVA: 0x000181E1 File Offset: 0x000163E1
		public bool ImportAllFilesAsLess { get; set; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000590 RID: 1424 RVA: 0x000181EA File Offset: 0x000163EA
		// (set) Token: 0x06000591 RID: 1425 RVA: 0x000181F2 File Offset: 0x000163F2
		public bool MapPathsToWeb { get; set; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000592 RID: 1426 RVA: 0x000181FB File Offset: 0x000163FB
		// (set) Token: 0x06000593 RID: 1427 RVA: 0x00018203 File Offset: 0x00016403
		public bool MinifyOutput { get; set; }

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000594 RID: 1428 RVA: 0x0001820C File Offset: 0x0001640C
		// (set) Token: 0x06000595 RID: 1429 RVA: 0x00018214 File Offset: 0x00016414
		public bool Debug { get; set; }

		// Token: 0x170000FE RID: 254
		// (get) Token: 0x06000596 RID: 1430 RVA: 0x0001821D File Offset: 0x0001641D
		// (set) Token: 0x06000597 RID: 1431 RVA: 0x00018225 File Offset: 0x00016425
		public bool CacheEnabled { get; set; }

		// Token: 0x170000FF RID: 255
		// (get) Token: 0x06000598 RID: 1432 RVA: 0x0001822E File Offset: 0x0001642E
		// (set) Token: 0x06000599 RID: 1433 RVA: 0x00018236 File Offset: 0x00016436
		public int HttpExpiryInMinutes { get; set; }

		// Token: 0x17000100 RID: 256
		// (get) Token: 0x0600059A RID: 1434 RVA: 0x0001823F File Offset: 0x0001643F
		// (set) Token: 0x0600059B RID: 1435 RVA: 0x00018247 File Offset: 0x00016447
		public Type LessSource { get; set; }

		// Token: 0x17000101 RID: 257
		// (get) Token: 0x0600059C RID: 1436 RVA: 0x00018250 File Offset: 0x00016450
		// (set) Token: 0x0600059D RID: 1437 RVA: 0x00018258 File Offset: 0x00016458
		public bool Web { get; set; }

		// Token: 0x17000102 RID: 258
		// (get) Token: 0x0600059E RID: 1438 RVA: 0x00018261 File Offset: 0x00016461
		// (set) Token: 0x0600059F RID: 1439 RVA: 0x00018269 File Offset: 0x00016469
		public DotlessSessionStateMode SessionMode { get; set; }

		// Token: 0x17000103 RID: 259
		// (get) Token: 0x060005A0 RID: 1440 RVA: 0x00018272 File Offset: 0x00016472
		// (set) Token: 0x060005A1 RID: 1441 RVA: 0x0001827A File Offset: 0x0001647A
		public string SessionQueryParamName { get; set; }

		// Token: 0x17000104 RID: 260
		// (get) Token: 0x060005A2 RID: 1442 RVA: 0x00018283 File Offset: 0x00016483
		// (set) Token: 0x060005A3 RID: 1443 RVA: 0x0001828B File Offset: 0x0001648B
		public Type Logger { get; set; }

		// Token: 0x17000105 RID: 261
		// (get) Token: 0x060005A4 RID: 1444 RVA: 0x00018294 File Offset: 0x00016494
		// (set) Token: 0x060005A5 RID: 1445 RVA: 0x0001829C File Offset: 0x0001649C
		public LogLevel LogLevel { get; set; }

		// Token: 0x17000106 RID: 262
		// (get) Token: 0x060005A6 RID: 1446 RVA: 0x000182A5 File Offset: 0x000164A5
		// (set) Token: 0x060005A7 RID: 1447 RVA: 0x000182AD File Offset: 0x000164AD
		public int Optimization { get; set; }

		// Token: 0x17000107 RID: 263
		// (get) Token: 0x060005A8 RID: 1448 RVA: 0x000182B6 File Offset: 0x000164B6
		// (set) Token: 0x060005A9 RID: 1449 RVA: 0x000182BE File Offset: 0x000164BE
		public bool HandleWebCompression { get; set; }

		// Token: 0x17000108 RID: 264
		// (get) Token: 0x060005AA RID: 1450 RVA: 0x000182C7 File Offset: 0x000164C7
		// (set) Token: 0x060005AB RID: 1451 RVA: 0x000182CF File Offset: 0x000164CF
		public List<IPluginConfigurator> Plugins { get; private set; }

		// Token: 0x17000109 RID: 265
		// (get) Token: 0x060005AC RID: 1452 RVA: 0x000182D8 File Offset: 0x000164D8
		// (set) Token: 0x060005AD RID: 1453 RVA: 0x000182E0 File Offset: 0x000164E0
		public bool StrictMath { get; set; }

		// Token: 0x04000119 RID: 281
		public const string DEFAULT_SESSION_QUERY_PARAM_NAME = "sstate";

		// Token: 0x0400011A RID: 282
		public const int DefaultHttpExpiryInMinutes = 10080;
	}
}
