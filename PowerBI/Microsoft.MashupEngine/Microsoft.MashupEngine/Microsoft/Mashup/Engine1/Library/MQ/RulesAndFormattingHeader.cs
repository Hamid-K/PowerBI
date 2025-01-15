using System;
using System.Collections.Generic;
using System.Reflection;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.Library.MQ
{
	// Token: 0x02000953 RID: 2387
	public sealed class RulesAndFormattingHeader : MqHeader
	{
		// Token: 0x0600444F RID: 17487 RVA: 0x000E5DF7 File Offset: 0x000E3FF7
		public RulesAndFormattingHeader()
		{
			base.RealValue = RulesAndFormattingHeader.Constructor.Invoke(null);
		}

		// Token: 0x06004450 RID: 17488 RVA: 0x000E3AC4 File Offset: 0x000E1CC4
		public RulesAndFormattingHeader(object header)
			: base(header)
		{
		}

		// Token: 0x170015D5 RID: 5589
		// (get) Token: 0x06004451 RID: 17489 RVA: 0x000E5E10 File Offset: 0x000E4010
		// (set) Token: 0x06004452 RID: 17490 RVA: 0x000E5E17 File Offset: 0x000E4017
		public static Type RealType { get; private set; } = AssemblyLoader.HisConnectors.GetType("Microsoft.HostIntegration.MqClient.RulesAndFormattingHeader");

		// Token: 0x170015D6 RID: 5590
		// (get) Token: 0x06004453 RID: 17491 RVA: 0x000E5E1F File Offset: 0x000E401F
		// (set) Token: 0x06004454 RID: 17492 RVA: 0x000E5E37 File Offset: 0x000E4037
		public RulesAndFormattingHeader.RulesAndFormattingFlag Flags
		{
			get
			{
				return (RulesAndFormattingHeader.RulesAndFormattingFlag)RulesAndFormattingHeader.FlagsInfo.GetValue(base.RealValue, null);
			}
			set
			{
				RulesAndFormattingHeader.FlagsInfo.SetValue(base.RealValue, value, null);
			}
		}

		// Token: 0x170015D7 RID: 5591
		// (get) Token: 0x06004455 RID: 17493 RVA: 0x000E5E50 File Offset: 0x000E4050
		public Value FlagsString
		{
			get
			{
				RulesAndFormattingHeader.RulesAndFormattingFlag rulesAndFormattingFlag = (RulesAndFormattingHeader.RulesAndFormattingFlag)RulesAndFormattingHeader.FlagsInfo.GetValue(base.RealValue, null);
				string text;
				if (RulesAndFormattingHeader.rulesAndFormattingFlagStrings.TryGetValue(rulesAndFormattingFlag, out text))
				{
					return TextValue.New(text);
				}
				return Value.Null;
			}
		}

		// Token: 0x170015D8 RID: 5592
		// (get) Token: 0x06004456 RID: 17494 RVA: 0x000E5E8F File Offset: 0x000E408F
		public Dictionary<string, string> NamesToValues
		{
			get
			{
				return (Dictionary<string, string>)RulesAndFormattingHeader.NamesToValuesInfo.GetValue(base.RealValue, null);
			}
		}

		// Token: 0x06004457 RID: 17495 RVA: 0x000E5EA8 File Offset: 0x000E40A8
		public override Value GetRecordValue(Value binaryDisplay, TypeValue binaryDisplayTypeValue)
		{
			if (this.NamesToValues == null || this.NamesToValues.Count > 0)
			{
				return Value.Null;
			}
			KeysBuilder keysBuilder = new KeysBuilder(this.NamesToValues.Count);
			Value[] array = new Value[this.NamesToValues.Count];
			int num = 0;
			foreach (KeyValuePair<string, string> keyValuePair in this.NamesToValues)
			{
				keysBuilder.Add(keyValuePair.Key);
				array[num] = TextValue.NewOrNull(keyValuePair.Value);
				num++;
			}
			return RecordValue.New(keysBuilder.ToKeys(), array);
		}

		// Token: 0x0400242B RID: 9259
		private static readonly ConstructorInfo Constructor = RulesAndFormattingHeader.RealType.GetConstructor(Type.EmptyTypes);

		// Token: 0x0400242C RID: 9260
		private static readonly PropertyInfo FlagsInfo = RulesAndFormattingHeader.RealType.GetProperty("Flags");

		// Token: 0x0400242D RID: 9261
		private static readonly PropertyInfo NamesToValuesInfo = RulesAndFormattingHeader.RealType.GetProperty("NamesToValues");

		// Token: 0x0400242F RID: 9263
		private static readonly Dictionary<RulesAndFormattingHeader.RulesAndFormattingFlag, string> rulesAndFormattingFlagStrings = new Dictionary<RulesAndFormattingHeader.RulesAndFormattingFlag, string> { 
		{
			RulesAndFormattingHeader.RulesAndFormattingFlag.None,
			"None"
		} };

		// Token: 0x02000954 RID: 2388
		[Flags]
		public enum RulesAndFormattingFlag
		{
			// Token: 0x04002431 RID: 9265
			None = 0
		}
	}
}
