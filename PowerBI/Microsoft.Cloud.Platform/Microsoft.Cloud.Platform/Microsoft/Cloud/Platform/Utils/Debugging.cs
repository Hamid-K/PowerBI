using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Microsoft.Cloud.Platform.Utils
{
	// Token: 0x020001CF RID: 463
	public static class Debugging
	{
		// Token: 0x06000BD7 RID: 3031 RVA: 0x00029190 File Offset: 0x00027390
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public static string EnableTracingInDebugger()
		{
			if (ExtendedTraceListener.SetWriteTraceListenerEnabled(true))
			{
				return "Writing traces to the debugger is already enabled. If you can't see them look elsewhere for the cause";
			}
			return "Writing traces to the debugger has been enabled";
		}

		// Token: 0x06000BD8 RID: 3032 RVA: 0x000291A8 File Offset: 0x000273A8
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public static string[] GetContext()
		{
			TraceDump traceDump = new TraceDump();
			ContextManager.Dump(traceDump, new int[0]);
			return traceDump.Lines.ToArray<string>();
		}

		// Token: 0x06000BD9 RID: 3033 RVA: 0x000291D2 File Offset: 0x000273D2
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public static IEnumerable<string> GetActivityStack()
		{
			yield return "Activity stack: ";
			IEnumerable<string> activityStackAsStrings = UtilsContext.GetActivityStackAsStrings(UtilsContext.Current.GetActivityStack());
			foreach (string text in activityStackAsStrings)
			{
				yield return text;
			}
			IEnumerator<string> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000BDA RID: 3034 RVA: 0x000291DC File Offset: 0x000273DC
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		public static string[] GetAsyncResultStack(object ar)
		{
			object obj = ar;
			TraceDump traceDump = new TraceDump();
			for (;;)
			{
				traceDump.Add("----------------------------------------");
				AsyncResult asyncResult = obj as AsyncResult;
				if (asyncResult != null)
				{
					asyncResult.Dump(traceDump);
					Delegate @delegate = (Delegate)Debugging.s_asyncResultCallback.GetValue(obj);
					if (@delegate == null)
					{
						break;
					}
					obj = @delegate.Target;
					traceDump.Add("(Below is the AsyncResult's callback's object)");
				}
				else
				{
					if (obj is IAsyncResult)
					{
						goto Block_3;
					}
					Sequencer sequencer = obj as Sequencer;
					if (sequencer == null)
					{
						goto IL_00CC;
					}
					sequencer.Dump(traceDump);
					obj = Debugging.s_sequencerFlowResult.GetValue(obj);
					if (obj == null)
					{
						goto IL_00BF;
					}
					traceDump.Add("(Below is the sequencer's m_flowResult)");
				}
			}
			traceDump.Add("(Callback points to null, so can't continue walking)");
			goto IL_00F8;
			Block_3:
			traceDump.Add(obj.GetType().ToString() + " (IAsyncResult)");
			traceDump.Add("(Cannot extract the callback from an arbitrary IAsyncResult, so can't continue walking)");
			goto IL_00F8;
			IL_00BF:
			traceDump.Add("(The sequencer's m_flowResult is null, so can't continue walking)");
			goto IL_00F8;
			IL_00CC:
			if (obj == null)
			{
				traceDump.Add("(null value given as input, so can't walk");
			}
			else
			{
				traceDump.Add(obj.GetType().ToString());
				traceDump.Add("(This type is not automatically-recognized by GetAsyncResultStack)");
			}
			IL_00F8:
			return traceDump.Lines.ToArray<string>();
		}

		// Token: 0x04000492 RID: 1170
		private static readonly FieldInfo s_asyncResultCallback = typeof(AsyncResult).GetField("m_callback", BindingFlags.Instance | BindingFlags.NonPublic);

		// Token: 0x04000493 RID: 1171
		private static readonly FieldInfo s_sequencerFlowResult = typeof(Sequencer).GetField("m_flowResult", BindingFlags.Instance | BindingFlags.NonPublic);
	}
}
