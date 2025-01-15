using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Runtime;

namespace Microsoft.Mashup.Engine1.BuiltInStrings
{
	// Token: 0x02000236 RID: 566
	internal class BuiltInStringsModule : Module
	{
		// Token: 0x17000C9C RID: 3228
		// (get) Token: 0x060018E3 RID: 6371 RVA: 0x00030CE2 File Offset: 0x0002EEE2
		public override Keys ExportKeys
		{
			get
			{
				if (BuiltInStringsModule.exportKeys == null)
				{
					BuiltInStringsModule.exportKeys = Keys.New(1, delegate(int index)
					{
						if (index == 0)
						{
							return "UICulture.GetString";
						}
						throw new InvalidOperationException(Strings.UnreachableCodePath);
					});
				}
				return BuiltInStringsModule.exportKeys;
			}
		}

		// Token: 0x17000C9D RID: 3229
		// (get) Token: 0x060018E4 RID: 6372 RVA: 0x00030D1A File Offset: 0x0002EF1A
		public override string Name
		{
			get
			{
				return "BuiltInStringsModule";
			}
		}

		// Token: 0x060018E5 RID: 6373 RVA: 0x00030D21 File Offset: 0x0002EF21
		protected override RecordValue GetSharedExports(RecordValue environment, IEngineHost hostEnvironment)
		{
			return RecordValue.New(this.ExportKeys, delegate(int index)
			{
				if (index == 0)
				{
					return BuiltInStringsModule.UICulture.GetString;
				}
				throw new InvalidOperationException(Strings.UnreachableCodePath);
			});
		}

		// Token: 0x0400069A RID: 1690
		private static Keys exportKeys;

		// Token: 0x02000237 RID: 567
		private enum Exports
		{
			// Token: 0x0400069C RID: 1692
			UICulture_GetString,
			// Token: 0x0400069D RID: 1693
			Count
		}

		// Token: 0x02000238 RID: 568
		private static class UICulture
		{
			// Token: 0x0400069E RID: 1694
			public static readonly FunctionValue GetString = new BuiltInStringsModule.UICulture.GetStringFunctionValue();

			// Token: 0x02000239 RID: 569
			private class GetStringFunctionValue : NativeFunctionValue2<TextValue, TextValue, Value>
			{
				// Token: 0x060018E8 RID: 6376 RVA: 0x00030D59 File Offset: 0x0002EF59
				public GetStringFunctionValue()
					: base(TypeValue.Text, 1, "name", TypeValue.Text, "args", NullableTypeValue.List)
				{
				}

				// Token: 0x060018E9 RID: 6377 RVA: 0x00030D7C File Offset: 0x0002EF7C
				public override TextValue TypedInvoke(TextValue name, Value args)
				{
					if (args.IsNull)
					{
						return TextValue.New(Strings.ResourceManager.GetString(name.String));
					}
					List<object> list = new List<object>();
					foreach (IValueReference valueReference in args.AsList)
					{
						list.Add(valueReference.Value);
					}
					return TextValue.New(string.Format(CultureInfo.CurrentCulture, Strings.ResourceManager.GetString(name.String), list.ToArray()));
				}
			}
		}
	}
}
