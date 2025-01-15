using System;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001FD RID: 509
	public static class TopLevelHandler
	{
		// Token: 0x06000D80 RID: 3456 RVA: 0x0002F174 File Offset: 0x0002D374
		public static Exception Run(object caller, TopLevelHandlerOption options, Action action)
		{
			return TopLevelHandler.Run(caller, options, action, false);
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x0002F17F File Offset: 0x0002D37F
		public static Exception RunNoDump(object caller, TopLevelHandlerOption options, Action action)
		{
			return TopLevelHandler.Run(caller, options, action, true);
		}

		// Token: 0x06000D82 RID: 3458 RVA: 0x0002F18C File Offset: 0x0002D38C
		private static Exception Run(object caller, TopLevelHandlerOption options, Action action, bool noDump)
		{
			Exception ret = null;
			if (!ExtendedEnvironment.IsTest)
			{
				ExceptionFilters.TryFilterCatch(action, delegate(Exception ex)
				{
					TopLevelHandler.TopLevelHandlerPolicyDecision topLevelHandlerPolicyDecision2 = TopLevelHandler.EvaluateTopLevelHandlerPolicy(ex, options);
					if (topLevelHandlerPolicyDecision2 == TopLevelHandler.TopLevelHandlerPolicyDecision.Crash)
					{
						ExtendedEnvironment.FailSlow(caller, ex);
						return ExceptionDisposition.ContinueSearch;
					}
					if (topLevelHandlerPolicyDecision2 == TopLevelHandler.TopLevelHandlerPolicyDecision.Pass)
					{
						if (!noDump && !ExceptionUtility.IsBenign(ex))
						{
							ExtendedEnvironment.CreateMemoryDump(ex, false);
						}
						return ExceptionDisposition.ContinueSearch;
					}
					return ExceptionDisposition.ExecuteHandler;
				}, delegate(Exception ex)
				{
					ret = ex;
					TraceSourceBase<UtilsTrace>.Tracer.Trace(TraceVerbosity.Warning, "Exception swallowed by top-level handler: {0}", new object[] { ex });
					if (!noDump && !ExceptionUtility.IsBenign(ex))
					{
						ExtendedEnvironment.CreateMemoryDump(ex, false);
					}
				});
			}
			else
			{
				try
				{
					action();
				}
				catch (Exception ex)
				{
					Exception ex2;
					TopLevelHandler.TopLevelHandlerPolicyDecision topLevelHandlerPolicyDecision = TopLevelHandler.EvaluateTopLevelHandlerPolicy(ex2, options);
					if (topLevelHandlerPolicyDecision == TopLevelHandler.TopLevelHandlerPolicyDecision.Crash)
					{
						if (ex2 is CrashException)
						{
							throw;
						}
						ExtendedEnvironment.FailSlow(caller, ex2);
					}
					else
					{
						if (topLevelHandlerPolicyDecision == TopLevelHandler.TopLevelHandlerPolicyDecision.Pass)
						{
							if (!noDump && !ExceptionUtility.IsBenign(ex2))
							{
								ExtendedEnvironment.CreateMemoryDump(ex2, false);
							}
							throw;
						}
						if (!noDump && !ExceptionUtility.IsBenign(ex2))
						{
							ExtendedEnvironment.CreateMemoryDump(ex2, false);
						}
						ret = ex2;
					}
				}
			}
			return ret;
		}

		// Token: 0x06000D83 RID: 3459 RVA: 0x0002F270 File Offset: 0x0002D470
		public static Exception Run(object caller, Action action)
		{
			return TopLevelHandler.Run(caller, TopLevelHandlerOption.SwallowBenign, action);
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x0002F27C File Offset: 0x0002D47C
		private static TopLevelHandler.TopLevelHandlerPolicyDecision EvaluateTopLevelHandlerPolicy(Exception ex, TopLevelHandlerOption options)
		{
			bool flag = ExceptionUtility.IsFatal(ex);
			bool flag2 = ExceptionUtility.IsBenign(ex);
			int num = (flag ? 2 : (flag2 ? 0 : 1));
			return TopLevelHandler.s_topLevelHandlerPolicyDecisionTable[num, (int)options];
		}

		// Token: 0x0400054C RID: 1356
		private static TopLevelHandler.TopLevelHandlerPolicyDecision[,] s_topLevelHandlerPolicyDecisionTable = new TopLevelHandler.TopLevelHandlerPolicyDecision[,]
		{
			{
				TopLevelHandler.TopLevelHandlerPolicyDecision.Crash,
				TopLevelHandler.TopLevelHandlerPolicyDecision.Swallow,
				TopLevelHandler.TopLevelHandlerPolicyDecision.Swallow,
				TopLevelHandler.TopLevelHandlerPolicyDecision.Pass,
				TopLevelHandler.TopLevelHandlerPolicyDecision.Swallow,
				TopLevelHandler.TopLevelHandlerPolicyDecision.Pass
			},
			{
				TopLevelHandler.TopLevelHandlerPolicyDecision.Crash,
				TopLevelHandler.TopLevelHandlerPolicyDecision.Crash,
				TopLevelHandler.TopLevelHandlerPolicyDecision.Swallow,
				TopLevelHandler.TopLevelHandlerPolicyDecision.Pass,
				TopLevelHandler.TopLevelHandlerPolicyDecision.Pass,
				TopLevelHandler.TopLevelHandlerPolicyDecision.Crash
			},
			{
				TopLevelHandler.TopLevelHandlerPolicyDecision.Crash,
				TopLevelHandler.TopLevelHandlerPolicyDecision.Crash,
				TopLevelHandler.TopLevelHandlerPolicyDecision.Crash,
				TopLevelHandler.TopLevelHandlerPolicyDecision.Crash,
				TopLevelHandler.TopLevelHandlerPolicyDecision.Crash,
				TopLevelHandler.TopLevelHandlerPolicyDecision.Crash
			}
		};

		// Token: 0x0200069F RID: 1695
		private enum TopLevelHandlerPolicyDecision
		{
			// Token: 0x040012C4 RID: 4804
			Crash,
			// Token: 0x040012C5 RID: 4805
			Pass,
			// Token: 0x040012C6 RID: 4806
			Swallow
		}
	}
}
