using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Resources;
using Microsoft.Mashup.Engine.Host;
using Microsoft.Mashup.Engine.Interface;
using Microsoft.Mashup.Engine1.Language;
using Microsoft.Mashup.Engine1.Library.Json;

namespace Microsoft.Mashup.Engine1.Runtime
{
	// Token: 0x020015B4 RID: 5556
	internal class OptionRecordDefinition
	{
		// Token: 0x06008B32 RID: 35634 RVA: 0x001D4135 File Offset: 0x001D2335
		public OptionRecordDefinition(ResourceManager resourceManager, params OptionItem[] options)
			: this(options)
		{
			this.resourceManager = resourceManager;
		}

		// Token: 0x06008B33 RID: 35635 RVA: 0x001D4145 File Offset: 0x001D2345
		public OptionRecordDefinition(params OptionItem[] options)
		{
			this.options = options.ToArray<OptionItem>();
			this.optionsMap = options.ToDictionary((OptionItem option) => option.Key);
		}

		// Token: 0x06008B34 RID: 35636 RVA: 0x001D4184 File Offset: 0x001D2384
		private OptionRecordDefinition(IEnumerable<OptionItem> options)
		{
			this.options = options.ToArray<OptionItem>();
			this.optionsMap = options.ToDictionary((OptionItem option) => option.Key);
		}

		// Token: 0x06008B35 RID: 35637 RVA: 0x001D41C4 File Offset: 0x001D23C4
		public RecordValue FromJson(string optionsJson)
		{
			if (string.IsNullOrEmpty(optionsJson))
			{
				return null;
			}
			Value value = JsonModule.Json.Document.Invoke(TextValue.New(optionsJson));
			return this.FromJson(value);
		}

		// Token: 0x06008B36 RID: 35638 RVA: 0x001D41F4 File Offset: 0x001D23F4
		public RecordValue FromJson(Value optionValue)
		{
			if (optionValue.IsNull || (optionValue.IsRecord && optionValue.AsRecord.IsEmpty))
			{
				return null;
			}
			TransformTypesHelper transformTypesHelper = new TransformTypesHelper(EngineHost.Empty, new OptionRecordDefinition.InvariantCulture());
			RecordValue asRecord = optionValue.AsRecord;
			RecordBuilder recordBuilder = new RecordBuilder(asRecord.Count);
			for (int i = 0; i < asRecord.Count; i++)
			{
				string text = asRecord.Keys[i];
				OptionItem optionItem;
				if (!this.optionsMap.TryGetValue(text, out optionItem))
				{
					throw new ArgumentException(Strings.OptionsRecord_UnsupportedKey(text), text);
				}
				Value value = asRecord[i];
				value = transformTypesHelper.GetFunctionValueFromType(optionItem.Type, value.Type, true).Invoke(value);
				recordBuilder.Add(text, value, optionItem.Type);
			}
			return recordBuilder.ToRecord();
		}

		// Token: 0x06008B37 RID: 35639 RVA: 0x001D42CC File Offset: 0x001D24CC
		public RecordTypeValue CreateRecordType()
		{
			RecordBuilder recordBuilder = new RecordBuilder(this.options.Length);
			foreach (OptionItem optionItem in this.options)
			{
				if (!OptionRecordDefinition.HasAnyOption(optionItem, OptionItemOption.ForDsrRoundTripOnly | OptionItemOption.ForExtensibilityOnly | OptionItemOption.ExcludeFromOptionType))
				{
					bool flag = (optionItem.Options & OptionItemOption.RequiresActions) > OptionItemOption.None;
					TypeValue typeValue = LibraryDescriptions.AddOptionItemMetadata(optionItem.Type, optionItem.Key, optionItem.HelpQualifier, flag, this.resourceManager);
					recordBuilder.Add(optionItem.Key, RecordTypeValue.NewField(typeValue, LogicalValue.New(optionItem.Type.IsNullable)), TypeValue.Any);
				}
			}
			return RecordTypeValue.New(recordBuilder.ToRecord());
		}

		// Token: 0x06008B38 RID: 35640 RVA: 0x001D4370 File Offset: 0x001D2570
		public OptionRecordDefinition Remove(params string[] keys)
		{
			return new OptionRecordDefinition(this.options.Where((OptionItem o) => !keys.Contains(o.Key)));
		}

		// Token: 0x06008B39 RID: 35641 RVA: 0x001D43A6 File Offset: 0x001D25A6
		public OptionRecordDefinition Concatenate(OptionRecordDefinition other)
		{
			return new OptionRecordDefinition(this.options.Concat(other.options));
		}

		// Token: 0x06008B3A RID: 35642 RVA: 0x001D43C0 File Offset: 0x001D25C0
		public OptionsRecord CreateOptions(string dataSourceName, Value options)
		{
			KeysBuilder keysBuilder = new KeysBuilder(this.options.Length);
			Value[] array = new Value[this.options.Length];
			object[] array2 = new object[this.options.Length];
			foreach (OptionItem optionItem in this.options)
			{
				object obj;
				if (!optionItem.Default.IsNull && optionItem.TryConvert(optionItem.Default, out obj))
				{
					int count = keysBuilder.Count;
					keysBuilder.Add(optionItem.Key);
					array[count] = optionItem.Default;
					array2[count] = obj;
				}
			}
			if (!options.IsNull)
			{
				RecordValue asRecord = options.AsRecord;
				for (int j = 0; j < asRecord.Count; j++)
				{
					string text = asRecord.Keys[j];
					OptionItem optionItem2;
					object obj2;
					if (!this.optionsMap.TryGetValue(text, out optionItem2) || !optionItem2.TryConvert(asRecord[j], out obj2))
					{
						throw ValueException.NewDataSourceError<Message2>(DataSourceException.DataSourceMessage(dataSourceName, Strings.UnsupportedQueryOption(text, asRecord[j].ToSource())), asRecord[j], null);
					}
					int num = keysBuilder.IndexOf(text);
					if (num < 0)
					{
						num = keysBuilder.Count;
						keysBuilder.Add(text);
					}
					array[num] = asRecord[j];
					array2[num] = obj2;
				}
			}
			if (keysBuilder.Count < array.Length)
			{
				Array.Resize<Value>(ref array, keysBuilder.Count);
				Array.Resize<object>(ref array2, keysBuilder.Count);
			}
			return new OptionsRecord(keysBuilder.ToKeys(), array, array2);
		}

		// Token: 0x06008B3B RID: 35643 RVA: 0x001D4560 File Offset: 0x001D2760
		public RecordValue ValidateOptions(Value options, string functionName, bool extensibilityEnabled = false, bool validateValue = false)
		{
			if (options.IsNull)
			{
				return RecordValue.Empty;
			}
			for (int i = 0; i < options.AsRecord.Count; i++)
			{
				string text = options.AsRecord.Keys[i];
				OptionItem optionItem;
				if (!this.optionsMap.TryGetValue(text, out optionItem) || (OptionRecordDefinition.HasOption(optionItem, OptionItemOption.ForExtensibilityOnly) && !extensibilityEnabled))
				{
					string[] array = (from o in this.options
						where !OptionRecordDefinition.HasOption(o, OptionItemOption.ForDsrRoundTripOnly) && !OptionRecordDefinition.HasOption(o, OptionItemOption.ForExtensibilityOnly)
						select o.Key into s
						orderby s
						select s).ToArray<string>();
					throw ValueException.NewExpressionError((array.Length != 0) ? Strings.InvalidOption(text, functionName, string.Join(", ", array)).ToString() : Strings.InvalidOptionWithNoValidOptions(text), Value.Null, null);
				}
				object obj;
				if (validateValue && !optionItem.TryConvert(options.AsRecord[i], out obj))
				{
					throw ValueException.NewExpressionError<Message1>(Strings.InvalidOptionValue(text), options.AsRecord[i], null);
				}
			}
			return options.AsRecord;
		}

		// Token: 0x06008B3C RID: 35644 RVA: 0x001D46BD File Offset: 0x001D28BD
		private static bool HasOption(OptionItem optionItem, OptionItemOption option)
		{
			return (optionItem.Options & option) == option;
		}

		// Token: 0x06008B3D RID: 35645 RVA: 0x001D46CA File Offset: 0x001D28CA
		private static bool HasAnyOption(OptionItem optionItem, OptionItemOption options)
		{
			return (optionItem.Options & options) > OptionItemOption.None;
		}

		// Token: 0x04004C3D RID: 19517
		public static readonly OptionRecordDefinition Empty = new OptionRecordDefinition(Array.Empty<OptionItem>());

		// Token: 0x04004C3E RID: 19518
		public static readonly OptionRecordDefinition HierarchicalNavigation = new OptionRecordDefinition(new OptionItem[]
		{
			new OptionItem("HierarchicalNavigation", NullableTypeValue.Logical, Value.Null, OptionItemOption.ForDsrRoundTripOnly, null, null)
		});

		// Token: 0x04004C3F RID: 19519
		private readonly OptionItem[] options;

		// Token: 0x04004C40 RID: 19520
		private readonly Dictionary<string, OptionItem> optionsMap;

		// Token: 0x04004C41 RID: 19521
		private readonly ResourceManager resourceManager;

		// Token: 0x020015B5 RID: 5557
		private class InvariantCulture : ICulture
		{
			// Token: 0x170024AC RID: 9388
			// (get) Token: 0x06008B3F RID: 35647 RVA: 0x001D471E File Offset: 0x001D291E
			// (set) Token: 0x06008B40 RID: 35648 RVA: 0x001D4726 File Offset: 0x001D2926
			public string Name { get; set; }

			// Token: 0x170024AD RID: 9389
			// (get) Token: 0x06008B41 RID: 35649 RVA: 0x001D472F File Offset: 0x001D292F
			// (set) Token: 0x06008B42 RID: 35650 RVA: 0x001D4737 File Offset: 0x001D2937
			public CultureInfo Value { get; set; }

			// Token: 0x06008B43 RID: 35651 RVA: 0x001D4740 File Offset: 0x001D2940
			public InvariantCulture()
			{
				this.Name = CultureInfo.InvariantCulture.Name;
				this.Value = CultureInfo.InvariantCulture;
			}
		}
	}
}
