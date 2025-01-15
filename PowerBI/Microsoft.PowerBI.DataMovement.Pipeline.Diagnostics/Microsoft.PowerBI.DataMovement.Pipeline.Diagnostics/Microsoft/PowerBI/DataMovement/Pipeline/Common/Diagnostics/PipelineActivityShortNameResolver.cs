using System;
using System.Collections.Concurrent;
using System.Globalization;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics;
using Microsoft.PowerBI.DataMovement.Pipeline.Diagnostics.Internal;

namespace Microsoft.PowerBI.DataMovement.Pipeline.Common.Diagnostics
{
	// Token: 0x02000008 RID: 8
	[NullableContext(1)]
	[Nullable(0)]
	internal sealed class PipelineActivityShortNameResolver
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000020B3 File Offset: 0x000002B3
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal string Resolve(PipelineActivityType pipelineActivityType)
		{
			return this.m_activityTypeMap.GetOrAdd(pipelineActivityType, new Func<PipelineActivityType, string>(this.CreateFromPipelineActivityType));
		}

		// Token: 0x06000008 RID: 8 RVA: 0x000020CD File Offset: 0x000002CD
		[MethodImpl(MethodImplOptions.AggressiveInlining)]
		internal PipelineActivityType Resolve(string shortName)
		{
			PipelineActivityShortNameResolver.ValidateShortName(shortName);
			return this.m_shortNameMap.GetOrAdd(shortName, new Func<string, PipelineActivityType>(this.CreateFromShortName));
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020F0 File Offset: 0x000002F0
		private string CreateFromPipelineActivityType(PipelineActivityType pipelineActivityType)
		{
			string text = string.Empty;
			if (pipelineActivityType > (PipelineActivityType)0)
			{
				char[] array = new char[4];
				for (int i = 0; i < array.Length; i++)
				{
					int num = (int)((pipelineActivityType >> ((8 * (3 - i)) & 31)) & (PipelineActivityType)255);
					array[i] = Convert.ToChar(num);
				}
				text = new string(array);
				PipelineActivityShortNameResolver.ValidateShortName(text);
			}
			return text;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x00002148 File Offset: 0x00000348
		private PipelineActivityType CreateFromShortName(string shortName)
		{
			int num = 0;
			for (int i = 0; i < shortName.Length; i++)
			{
				byte b = Convert.ToByte(shortName[i]);
				num = (num << 8) | (int)b;
			}
			return (PipelineActivityType)num;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x0000217C File Offset: 0x0000037C
		private static void ValidateShortName(string shortName)
		{
			RuntimeChecks.CheckValue(shortName, "shortName");
			if (!PipelineActivityShortNameResolver.s_shortNameRegex.IsMatch(shortName))
			{
				string text = string.Format(CultureInfo.InvariantCulture, "Activity short name '{0}' is not valid", shortName);
				throw RuntimeChecks.ArgumentOutOfRange("shortName", shortName, text);
			}
		}

		// Token: 0x04000015 RID: 21
		private static Regex s_shortNameRegex = new Regex("^[A-Z0-9]{4}$", RegexOptions.Compiled);

		// Token: 0x04000016 RID: 22
		private readonly ConcurrentDictionary<PipelineActivityType, string> m_activityTypeMap = new ConcurrentDictionary<PipelineActivityType, string>();

		// Token: 0x04000017 RID: 23
		private readonly ConcurrentDictionary<string, PipelineActivityType> m_shortNameMap = new ConcurrentDictionary<string, PipelineActivityType>();
	}
}
